using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for Bill_Sys_C4
/// </summary>
//public class Bill_Sys_C4
//{
//    public Bill_Sys_C4()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

namespace testXFAItextSharp.DAO//Bill_Sys_C4DAO
{
    public class Bill_Sys_C4DAO
    {
        private string sLastName;
        public string LastName
        {
            get { return this.sLastName; }
            set { this.sLastName = value; }
        }

        private string sFirstName;
        public string FirstName
        {
            get { return this.sFirstName; }
            set { this.sFirstName = value; }
        }

        private string sMiddleInitial;
        public string middleInitial
        {
            get { return this.sMiddleInitial; }
            set { this.sMiddleInitial = value; }
        }

        private string sSocialSecNumber;
        public string socialSecNumber
        {
            get { return this.sSocialSecNumber; }
            set { this.sSocialSecNumber = value; }
        }

        private string sPatientHomePhone1;
        public string patientHomePhone1
        {
            get { return this.sPatientHomePhone1; }
            set { this.sPatientHomePhone1 = value; }
        }

        private string sPatientHomePhone2;
        public string patientHomePhone2
        {
            get { return this.sPatientHomePhone2; }
            set { this.sPatientHomePhone2 = value; }
        }

        private string sWCBNumber;
        public string WCBNumber
        {
            get { return this.sWCBNumber; }
            set { this.sWCBNumber = value; }
        }

        private string sCarrierCaseNo;
        public string carrierCaseNo
        {
            get { return this.sCarrierCaseNo; }
            set { this.sCarrierCaseNo = value; }
        }

        private string sPatientAddress;
        public string patientAddress
        {
            get { return this.sPatientAddress; }
            set { this.sPatientAddress = value; }
        }

        private string sPatientCity;
        public string patientCity
        {
            get { return this.sPatientCity; }
            set { this.sPatientCity = value; }
        }

        private string sPatientState;
        public string patientState
        {
            get { return this.sPatientState; }
            set { this.sPatientState = value; }
        }

        private string sPatientZip;
        public string patientZip
        {
            get { return this.sPatientZip; }
            set { this.sPatientZip = value; }
        }

        private string sAccidentDate;
        public string accidentDate
        {
            get { return this.sAccidentDate; }
            set { this.sAccidentDate = value; }
        }

        private string sAccidentMonth;
        public string accidentMonth
        {
            get { return this.sAccidentMonth; }
            set { this.sAccidentMonth = value; }
        }

        private string sAccidentYear;
        public string accidentYear
        {
            get { return this.sAccidentYear; }
            set { this.sAccidentYear = value; }
        }

        private string sDOBDate;
        public string DOBDate
        {
            get { return this.sDOBDate; }
            set { this.sDOBDate = value; }
        }

        private string sDOBMonth;
        public string DOBMonth
        {
            get { return this.sDOBMonth; }
            set { this.sDOBMonth = value; }
        }

        private string sDOBYear;
        public string DOBYear
        {
            get { return this.sDOBYear; }
            set { this.sDOBYear = value; }
        }

        private string sJobTitle;
        public string jobTitle
        {
            get { return this.sJobTitle; }
            set { this.sJobTitle = value; }
        }

        private string sWorkActivity;
        public string workActivity
        {
            get { return this.sWorkActivity; }
            set { this.sWorkActivity = value; }
        }

        private string sWorkActivity1;
        public string workActivity1
        {
            get { return this.sWorkActivity1; }
            set { this.sWorkActivity1 = value; }
        }

        private string sAatientAccountNo;
        public string patientAccountNo
        {
            get { return this.sAatientAccountNo; }
            set { this.sAatientAccountNo = value; }
        }

        private string sEmployerName;
        public string employerName
        {
            get { return this.sEmployerName; }
            set { this.sEmployerName = value; }
        }

        private string sEmployerPhone1;
        public string employerPhone1
        {
            get { return this.sEmployerPhone1; }
            set { this.sEmployerPhone1 = value; }
        }

        private string _SZ_CARRIER_CODE;
        public string SZ_CARRIER_CODE
        {
            get { return this._SZ_CARRIER_CODE; }
            set { this._SZ_CARRIER_CODE = value; }
        }
        //SZ_DIAGNOSIS_CODE


        //BY NIRMAL-------------------------------------------------------------------
        private string _txt_F_4_Bruising;
        public string txt_F_4_Bruising
        {
            get { return this._txt_F_4_Bruising; }
            set { this._txt_F_4_Bruising = value; }
        }

        private string _txt_F_4_Burns;
        public string txt_F_4_Burns
        {
            get { return this._txt_F_4_Burns; }
            set { this._txt_F_4_Burns = value; }
        }

        private string _txt_F_4_Crepitation;
        public string txt_F_4_Crepitation
        {
            get { return this._txt_F_4_Crepitation; }
            set { this._txt_F_4_Crepitation = value; }
        }

        private string _txt_F_4_Deformity;
        public string txt_F_4_Deformity
        {
            get { return this._txt_F_4_Deformity; }
            set { this._txt_F_4_Deformity = value; }
        }

        private string _txt_F_4_Edema;
        public string txt_F_4_Edema
        {
            get { return this._txt_F_4_Edema; }
            set { this._txt_F_4_Edema = value; }
        }

        private string _txt_F_4_Hematoma;
        public string txt_F_4_Hematoma
        {
            get { return this._txt_F_4_Hematoma; }
            set { this._txt_F_4_Hematoma = value; }
        }

        private string _txt_F_4_JointEffusion;
        public string txt_F_4_JointEffusion
        {
            get { return this._txt_F_4_JointEffusion; }
            set { this._txt_F_4_JointEffusion = value; }
        }

        private string _txt_F_4_Laceration;
        public string txt_F_4_Laceration
        {
            get { return this._txt_F_4_Laceration; }
            set { this._txt_F_4_Laceration = value; }
        }

        public string _txt_F_4_Pain;
        public string txt_F_4_Pain
        {
            get { return this._txt_F_4_Pain; }
            set { this._txt_F_4_Pain = value; }
        }

        private string _txt_F_4_Scar;
        public string txt_F_4_Scar
        {
            get { return this._txt_F_4_Scar; }
            set { this._txt_F_4_Scar = value; }
        }

        private string _txt_F_4_OtherFindings;
        public string txt_F_4_OtherFindings
        {
            get { return this._txt_F_4_OtherFindings; }
            set { this._txt_F_4_OtherFindings = value; }
        }

        private string _txt_F_4_ActiveROM;
        public string txt_F_4_ActiveROM
        {
            get { return this._txt_F_4_ActiveROM; }
            set { this._txt_F_4_ActiveROM = value; }
        }

        private string _txt_F_4_PassiveROM;
        public string txt_F_4_PassiveROM
        {
            get { return this._txt_F_4_PassiveROM; }
            set { this._txt_F_4_PassiveROM = value; }
        }

        private string _txt_F_4_Gait;
        public string txt_F_4_Gait
        {
            get { return this._txt_F_4_Gait; }
            set { this._txt_F_4_Gait = value; }
        }

        private string _txt_F_4_Palpable;
        public string txt_F_4_Palpable
        {
            get { return this._txt_F_4_Palpable; }
            set { this._txt_F_4_Palpable = value; }
        }

        private string _txt_F_4_Reflexes;
        public string txt_F_4_Reflexes
        {
            get { return this._txt_F_4_Reflexes; }
            set { this._txt_F_4_Reflexes = value; }
        }

        private string _txt_F_4_Sensation;
        public string txt_F_4_Sensation
        {
            get { return this._txt_F_4_Sensation; }
            set { this._txt_F_4_Sensation = value; }
        }

        private string _txt_F_4_Strength;
        public string txt_F_4_Strength
        {
            get { return this._txt_F_4_Strength; }
            set { this._txt_F_4_Strength = value; }
        }

        private string _txt_F_4_Wasting;
        public string txt_F_4_Wasting
        {
            get { return this._txt_F_4_Wasting; }
            set { this._txt_F_4_Wasting = value; }
        }

        private string _SZ_DIAGNOSTIC_TEST;
        public string SZ_DIAGNOSTIC_TEST
        {
            get { return this._SZ_DIAGNOSTIC_TEST; }
            set { this._SZ_DIAGNOSTIC_TEST = value; }
        }

        private string _SZ_DIAGNOSTIC_TEST_1;
        public string SZ_DIAGNOSTIC_TEST_1
        {
            get { return this._SZ_DIAGNOSTIC_TEST_1; }
            set { this._SZ_DIAGNOSTIC_TEST_1 = value; }
        }

        private string _SZ_TREATMENT;
        public string SZ_TREATMENT
        {
            get { return this._SZ_TREATMENT; }
            set { this._SZ_TREATMENT = value; }
        }

        private string _SZ_TREATMENT_1;
        public string SZ_TREATMENT_1
        {
            get { return this._SZ_TREATMENT_1; }
            set { this._SZ_TREATMENT_1 = value; }
        }

        private string _SZ_PROGNOSIS_RECOVERY;
        public string SZ_PROGNOSIS_RECOVERY
        {
            get { return this._SZ_PROGNOSIS_RECOVERY; }
            set { this._SZ_PROGNOSIS_RECOVERY = value; }
        }

        private string _SZ_PROGNOSIS_RECOVERY_1;
        public string SZ_PROGNOSIS_RECOVERY_1
        {
            get { return this._SZ_PROGNOSIS_RECOVERY_1; }
            set { this._SZ_PROGNOSIS_RECOVERY_1 = value; }
        }

        private string _SZ_PROGNOSIS_RECOVERY_2;
        public string SZ_PROGNOSIS_RECOVERY_2
        {
            get { return this._SZ_PROGNOSIS_RECOVERY_2; }
            set { this._SZ_PROGNOSIS_RECOVERY_2 = value; }
        }

        private string _SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION;
        public string SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION
        {
            get { return this._SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION; }
            set { this._SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION = value; }
        }

        private string _SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1;
        public string SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1
        {
            get { return this._SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1; }
            set { this._SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1 = value; }
        }

        private string _FT_TEMPORARY_IMPAIRMENT_PERCENTAGE;
        public string FT_TEMPORARY_IMPAIRMENT_PERCENTAGE
        {
            get { return this._FT_TEMPORARY_IMPAIRMENT_PERCENTAGE; }
            set { this._FT_TEMPORARY_IMPAIRMENT_PERCENTAGE = value; }
        }

        private string _SZ_TEST_RESULTS;
        public string SZ_TEST_RESULTS
        {
            get { return this._SZ_TEST_RESULTS; }
            set { this._SZ_TEST_RESULTS = value; }
        }

        private string _SZ_TEST_RESULTS_1;
        public string SZ_TEST_RESULTS_1
        {
            get { return this._SZ_TEST_RESULTS_1; }
            set { this._SZ_TEST_RESULTS_1 = value; }
        }

        private string _SZ_PROPOSED_TREATEMENT;
        public string SZ_PROPOSED_TREATEMENT
        {
            get { return this._SZ_PROPOSED_TREATEMENT; }
            set { this._SZ_PROPOSED_TREATEMENT = value; }
        }

        private string _SZ_PROPOSED_TREATEMENT_1;
        public string SZ_PROPOSED_TREATEMENT_1
        {
            get { return this._SZ_PROPOSED_TREATEMENT_1; }
            set { this._SZ_PROPOSED_TREATEMENT_1 = value; }
        }

        private string _SZ_PROPOSED_TREATEMENT_3;
        public string SZ_PROPOSED_TREATEMENT_3
        {
            get { return this._SZ_PROPOSED_TREATEMENT_3; }
            set { this._SZ_PROPOSED_TREATEMENT_3 = value; }
        }

        private string _SZ_MEDICATIONS_PRESCRIBED;
        public string SZ_MEDICATIONS_PRESCRIBED
        {
            get { return this._SZ_MEDICATIONS_PRESCRIBED; }
            set { this._SZ_MEDICATIONS_PRESCRIBED = value; }
        }

        private string _SZ_MEDICATIONS_ADVISED;
        public string SZ_MEDICATIONS_ADVISED
        {
            get { return this._SZ_MEDICATIONS_ADVISED; }
            set { this._SZ_MEDICATIONS_ADVISED = value; }
        }

        private string _SZ_MEDICATION_RESTRICTIONS_DESCRIPTION;
        public string SZ_MEDICATION_RESTRICTIONS_DESCRIPTION
        {
            get { return this._SZ_MEDICATION_RESTRICTIONS_DESCRIPTION; }
            set { this._SZ_MEDICATION_RESTRICTIONS_DESCRIPTION = value; }
        }

        private string _Text226;
        public string Text226
        {
            get { return this._Text226; }
            set { this._Text226 = value; }
        }

        private string _chk_F_7_None;
        public string chk_F_7_None
        {
            get { return this._chk_F_7_None; }
            set { this._chk_F_7_None = value; }
        }

        private string _chk_F_4_Bruising;
        public string chk_F_4_Bruising
        {
            get { return this._chk_F_4_Bruising; }
            set { this._chk_F_4_Bruising = value; }
        }

        private string _chk_F_4_Burns;
        public string chk_F_4_Burns
        {
            get { return this._chk_F_4_Burns; }
            set { this._chk_F_4_Burns = value; }
        }

        private string _chk_F_4_Crepitation;
        public string chk_F_4_Crepitation
        {
            get { return this._chk_F_4_Crepitation; }
            set { this._chk_F_4_Crepitation = value; }
        }

        private string _chk_F_4_Deformity;
        public string chk_F_4_Deformity
        {
            get { return this._chk_F_4_Deformity; }
            set { this._chk_F_4_Deformity = value; }
        }

        private string _chk_F_4_Edema;
        public string chk_F_4_Edema
        {
            get { return this._chk_F_4_Edema; }
            set { this._chk_F_4_Edema = value; }
        }

        private string _chk_F_4_Hematoma;
        public string chk_F_4_Hematoma
        {
            get { return this._chk_F_4_Hematoma; }
            set { this._chk_F_4_Hematoma = value; }
        }

        private string _chk_F_4_JointEffusion;
        public string chk_F_4_JointEffusion
        {
            get { return this._chk_F_4_JointEffusion; }
            set { this._chk_F_4_JointEffusion = value; }
        }

        private string _chk_F_4_Laceration;
        public string chk_F_4_Laceration
        {
            get { return this._chk_F_4_Laceration; }
            set { this._chk_F_4_Laceration = value; }
        }

        private string _chk_F_4_Pain;
        public string chk_F_4_Pain
        {
            get { return this._chk_F_4_Pain; }
            set { this._chk_F_4_Pain = value; }
        }

        private string _chk_F_4_Scar;
        public string chk_F_4_Scar
        {
            get { return this._chk_F_4_Scar; }
            set { this._chk_F_4_Scar = value; }
        }

        private string _chk_F_4_OtherFindings;
        public string chk_F_4_OtherFindings
        {
            get { return this._chk_F_4_OtherFindings; }
            set { this._chk_F_4_OtherFindings = value; }
        }

        private string _chk_F_4_Neuromuscular;
        public string chk_F_4_Neuromuscular
        {
            get { return this._chk_F_4_Neuromuscular; }
            set { this._chk_F_4_Neuromuscular = value; }
        }

        private string _chk_F_4_Abnormal;
        public string chk_F_4_Abnormal
        {
            get { return this._chk_F_4_Abnormal; }
            set { this._chk_F_4_Abnormal = value; }
        }

        private string _chk_F_4_ActiveROM;
        public string chk_F_4_ActiveROM
        {
            get { return this._chk_F_4_ActiveROM; }
            set { this._chk_F_4_ActiveROM = value; }
        }

        private string _chk_F_4_PassiveROM;
        public string chk_F_4_PassiveROM
        {
            get { return this._chk_F_4_PassiveROM; }
            set { this._chk_F_4_PassiveROM = value; }
        }

        private string _chk_F_4_Gait;
        public string chk_F_4_Gait
        {
            get { return this._chk_F_4_Gait; }
            set { this._chk_F_4_Gait = value; }
        }

        private string _chk_F_4_Palpable;
        public string chk_F_4_Palpable
        {
            get { return this._chk_F_4_Palpable; }
            set { this._chk_F_4_Palpable = value; }
        }

        private string _chk_F_4_Reflexes;
        public string chk_F_4_Reflexes
        {
            get { return this._chk_F_4_Reflexes; }
            set { this._chk_F_4_Reflexes = value; }
        }

        private string _chk_F_4_Sensation;
        public string chk_F_4_Sensation
        {
            get { return this._chk_F_4_Sensation; }
            set { this._chk_F_4_Sensation = value; }
        }

        private string _chk_F_4_Strength;
        public string chk_F_4_Strength
        {
            get { return this._chk_F_4_Strength; }
            set { this._chk_F_4_Strength = value; }
        }

        private string _chk_F_4_Wasting;
        public string chk_F_4_Wasting
        {
            get { return this._chk_F_4_Wasting; }
            set { this._chk_F_4_Wasting = value; }
        }

        private string _chk_F_8_Yes;
        public string chk_F_8_Yes
        {
            get { return this._chk_F_8_Yes; }
            set { this._chk_F_8_Yes = value; }
        }

        private string _chk_F_8_No;
        public string chk_F_8_No
        {
            get { return this._chk_F_8_No; }
            set { this._chk_F_8_No = value; }
        }

        private string _chk_G_1_Yes;
        public string chk_G_1_Yes
        {
            get { return this._chk_G_1_Yes; }
            set { this._chk_G_1_Yes = value; }
        }

        private string _chk_G_1_No;
        public string chk_G_1_No
        {
            get { return this._chk_G_1_No; }
            set { this._chk_G_1_No = value; }
        }

        private string _chk_G_2_Yes;
        public string chk_G_2_Yes
        {
            get { return this._chk_G_2_Yes; }
            set { this._chk_G_2_Yes = value; }
        }

        private string _chk_G_2_No;
        public string chk_G_2_No
        {
            get { return this._chk_G_2_No; }
            set { this._chk_G_2_No = value; }
        }

        private string _chk_G_3_Yes;
        public string chk_G_3_Yes
        {
            get { return this._chk_G_3_Yes; }
            set { this._chk_G_3_Yes = value; }
        }

        private string _chk_G_3_No;
        public string chk_G_3_No
        {
            get { return this._chk_G_3_No; }
            set { this._chk_G_3_No = value; }
        }

        private string _chk_G_3_NA;
        public string chk_G_3_NA
        {
            get { return this._chk_G_3_NA; }
            set { this._chk_G_3_NA = value; }
        }

        private string _chk_H_2_None;
        public string chk_H_2_None
        {
            get { return this._chk_H_2_None; }
            set { this._chk_H_2_None = value; }
        }

        private string _chk_H_2_Other;
        public string chk_H_2_Other
        {
            get { return this._chk_H_2_Other; }
            set { this._chk_H_2_Other = value; }
        }

        private string _txt_H_3_MRI;
        public string txt_H_3_MRI
        {
            get { return this._txt_H_3_MRI; }
            set { this._txt_H_3_MRI = value; }
        }

        private string _txt_H_3_Labs;
        public string txt_H_3_Labs
        {
            get { return this._txt_H_3_Labs; }
            set { this._txt_H_3_Labs = value; }
        }

        private string _txt_H_3_XRay;
        public string txt_H_3_XRay
        {
            get { return this._txt_H_3_XRay; }
            set { this._txt_H_3_XRay = value; }
        }

        private string _txt_H_3_Left_Other;
        public string txt_H_3_Left_Other
        {
            get { return this._txt_H_3_Left_Other; }
            set { this._txt_H_3_Left_Other = value; }
        }

        private string _txt_H_3_Specialist;
        public string txt_H_3_Specialist
        {
            get { return this._txt_H_3_Specialist; }
            set { this._txt_H_3_Specialist = value; }
        }

        private string _txt_H_3_Right_Other;
        public string txt_H_3_Right_Other
        {
            get { return this._txt_H_3_Right_Other; }
            set { this._txt_H_3_Right_Other = value; }
        }

        private string _txt_H_4_Other;
        public string txt_H_4_Other
        {
            get { return this._txt_H_4_Other; }
            set { this._txt_H_4_Other = value; }
        }

        private string _txt_H_5_Month;
        public string txt_H_5_Month
        {
            get { return this._txt_H_5_Month; }
            set { this._txt_H_5_Month = value; }
        }

        private string _DT_PATIENT_MISSED_WORK_DATE_DD;
        public string DT_PATIENT_MISSED_WORK_DATE_DD
        {
            get { return this._DT_PATIENT_MISSED_WORK_DATE_DD; }
            set { this._DT_PATIENT_MISSED_WORK_DATE_DD = value; }
        }

        private string _DT_PATIENT_MISSED_WORK_DATE_MM;
        public string DT_PATIENT_MISSED_WORK_DATE_MM
        {
            get { return this._DT_PATIENT_MISSED_WORK_DATE_MM; }
            set { this._DT_PATIENT_MISSED_WORK_DATE_MM = value; }
        }

        private string _DT_PATIENT_MISSED_WORK_DATE_YY;
        public string DT_PATIENT_MISSED_WORK_DATE_YY
        {
            get { return this._DT_PATIENT_MISSED_WORK_DATE_YY; }
            set { this._DT_PATIENT_MISSED_WORK_DATE_YY = value; }
        }

        private string _SZ_PATIENT_RETURN_WORK_DESCRIPTION;
        public string SZ_PATIENT_RETURN_WORK_DESCRIPTION
        {
            get { return this._SZ_PATIENT_RETURN_WORK_DESCRIPTION; }
            set { this._SZ_PATIENT_RETURN_WORK_DESCRIPTION = value; }
        }

        private string _SZ_PATIENT_RETURN_WORK_LIMITATION_DD;
        public string SZ_PATIENT_RETURN_WORK_LIMITATION_DD
        {
            get { return this._SZ_PATIENT_RETURN_WORK_LIMITATION_DD; }
            set { this._SZ_PATIENT_RETURN_WORK_LIMITATION_DD = value; }
        }

        private string _SZ_PATIENT_RETURN_WORK_LIMITATION_MM;
        public string SZ_PATIENT_RETURN_WORK_LIMITATION_MM
        {
            get { return this._SZ_PATIENT_RETURN_WORK_LIMITATION_MM; }
            set { this._SZ_PATIENT_RETURN_WORK_LIMITATION_MM = value; }
        }

        private string _SZ_PATIENT_RETURN_WORK_LIMITATION_YY;
        public string SZ_PATIENT_RETURN_WORK_LIMITATION_YY
        {
            get { return this._SZ_PATIENT_RETURN_WORK_LIMITATION_YY; }
            set { this._SZ_PATIENT_RETURN_WORK_LIMITATION_YY = value; }
        }

        private string _Text249_DD;
        public string Text249_DD
        {
            get { return this._Text249_DD; }
            set { this._Text249_DD = value; }
        }

        private string _Text249_MM;
        public string Text249_MM
        {
            get { return this._Text249_MM; }
            set { this._Text249_MM = value; }
        }

        private string _Text249_YY;
        public string Text249_YY
        {
            get { return this._Text249_YY; }
            set { this._Text249_YY = value; }
        }

        private string _SZ_LIMITATION_DURATION;
        public string SZ_LIMITATION_DURATION
        {
            get { return this._SZ_LIMITATION_DURATION; }
            set { this._SZ_LIMITATION_DURATION = value; }
        }

        private string _SZ_QUANIFY_LIMITATION;
        public string SZ_QUANIFY_LIMITATION
        {
            get { return this._SZ_QUANIFY_LIMITATION; }
            set { this._SZ_QUANIFY_LIMITATION = value; }
        }

        private string _SZ_QUANIFY_LIMITATION_1;
        public string SZ_QUANIFY_LIMITATION_1
        {
            get { return this._SZ_QUANIFY_LIMITATION_1; }
            set { this._SZ_QUANIFY_LIMITATION_1 = value; }
        }

        private string _SZ_QUANIFY_LIMITATION_2;
        public string SZ_QUANIFY_LIMITATION_2
        {
            get { return this._SZ_QUANIFY_LIMITATION_2; }
            set { this._SZ_QUANIFY_LIMITATION_2 = value; }
        }

        private string _SZ_OFFICE_NAME;
        public string SZ_OFFICE_NAME
        {
            get { return this._SZ_OFFICE_NAME; }
            set { this._SZ_OFFICE_NAME = value; }
        }

        private string _SZ_SPECIALITY;
        public string SZ_SPECIALITY
        {
            get { return this._SZ_SPECIALITY; }
            set { this._SZ_SPECIALITY = value; }
        }

        public string _SZ_READINGDOCTOR;
        public string SZ_READINGDOCTOR
        {
            get { return this._SZ_READINGDOCTOR; }
            set { this._SZ_READINGDOCTOR = value; }
        }

        private string _SZ_PHYSICIAN_SIGN;
        public string SZ_PHYSICIAN_SIGN
        {
            get { return this._SZ_PHYSICIAN_SIGN; }
            set { this._SZ_PHYSICIAN_SIGN = value; }
        }

        private string _txtTodayDate_DD;
        public string txtTodayDate_DD
        {
            get { return this._txtTodayDate_DD; }
            set { this._txtTodayDate_DD = value; }
        }

        private string _txtTodayDate_MM;
        public string txtTodayDate_MM
        {
            get { return this._txtTodayDate_MM; }
            set { this._txtTodayDate_MM = value; }
        }

        private string _txtTodayDate_YY;
        public string txtTodayDate_YY
        {
            get { return this._txtTodayDate_YY; }
            set { this._txtTodayDate_YY = value; }
        }

        private string _chk_H_3_Yes;
        public string chk_H_3_Yes
        {
            get { return this._chk_H_3_Yes; }
            set { this._chk_H_3_Yes = value; }
        }

        private string _chk_H_3_No;
        public string chk_H_3_No
        {
            get { return this._chk_H_3_No; }
            set { this._chk_H_3_No = value; }
        }

        private string _chk_H_3_CTScan;
        public string chk_H_3_CTScan
        {
            get { return this._chk_H_3_CTScan; }
            set { this._chk_H_3_CTScan = value; }
        }

        private string _chk_H_3_EMGNCS;
        public string chk_H_3_EMGNCS
        {
            get { return this._chk_H_3_EMGNCS; }
            set { this._chk_H_3_EMGNCS = value; }
        }

        private string _chk_H_3_MRI;
        public string chk_H_3_MRI
        {
            get { return this._chk_H_3_MRI; }
            set { this._chk_H_3_MRI = value; }
        }

        private string _chk_H_3_Labs;
        public string chk_H_3_Labs
        {
            get { return this._chk_H_3_Labs; }
            set { this._chk_H_3_Labs = value; }
        }

        private string _chk_H_3_XRay;
        public string chk_H_3_XRay
        {
            get { return this._chk_H_3_XRay; }
            set { this._chk_H_3_XRay = value; }
        }

        private string _chk_H_3_Left_Other;
        public string chk_H_3_Left_Other
        {
            get { return this._chk_H_3_Left_Other; }
            set { this._chk_H_3_Left_Other = value; }
        }

        private string _chk_H_3_Chiropractor;
        public string chk_H_3_Chiropractor
        {
            get { return this._chk_H_3_Chiropractor; }
            set { this._chk_H_3_Chiropractor = value; }
        }

        private string _chk_H_3_Interist;
        public string chk_H_3_Interist
        {
            get { return this._chk_H_3_Interist; }
            set { this._chk_H_3_Interist = value; }
        }

        private string _chk_H_3_Occupational;
        public string chk_H_3_Occupational
        {
            get { return this._chk_H_3_Occupational; }
            set { this._chk_H_3_Occupational = value; }
        }

        private string _chk_H_3_Physical;
        public string chk_H_3_Physical
        {
            get { return this._chk_H_3_Physical; }
            set { this._chk_H_3_Physical = value; }
        }

        private string _chk_H_3_Specialist;
        public string chk_H_3_Specialist
        {
            get { return this._chk_H_3_Specialist; }
            set { this._chk_H_3_Specialist = value; }
        }

        private string _chk_H_3_Right_Other;
        public string chk_H_3_Right_Other
        {
            get { return this._chk_H_3_Right_Other; }
            set { this._chk_H_3_Right_Other = value; }
        }

        private string _chk_H_4_Cane;
        public string chk_H_4_Cane
        {
            get { return this._chk_H_4_Cane; }
            set { this._chk_H_4_Cane = value; }
        }

        private string _chk_H_4_Crutches;
        public string chk_H_4_Crutches
        {
            get { return this._chk_H_4_Crutches; }
            set { this._chk_H_4_Crutches = value; }
        }

        private string _chk_H_4_Orthotics;
        public string chk_H_4_Orthotics
        {
            get { return this._chk_H_4_Orthotics; }
            set { this._chk_H_4_Orthotics = value; }
        }

        private string _chk_H_4_Walker;
        public string chk_H_4_Walker
        {
            get { return this._chk_H_4_Walker; }
            set { this._chk_H_4_Walker = value; }
        }

        private string _chk_H_4_WheelChair;
        public string chk_H_4_WheelChair
        {
            get { return this._chk_H_4_WheelChair; }
            set { this._chk_H_4_WheelChair = value; }
        }

        private string _chk_H_4_Other;
        public string chk_H_4_Other
        {
            get { return this._chk_H_4_Other; }
            set { this._chk_H_4_Other = value; }
        }

        private string _chk_H_5_WithinWeek;
        public string chk_H_5_WithinWeek
        {
            get { return this._chk_H_5_WithinWeek; }
            set { this._chk_H_5_WithinWeek = value; }
        }

        private string _chk_H_5_1_2_Week;
        public string chk_H_5_1_2_Week
        {
            get { return this._chk_H_5_1_2_Week; }
            set { this._chk_H_5_1_2_Week = value; }
        }

        private string _chk_H_5_3_4_Week;
        public string chk_H_5_3_4_Week
        {
            get { return this._chk_H_5_3_4_Week; }
            set { this._chk_H_5_3_4_Week = value; }
        }

        private string _chk_H_5_5_6_Week;
        public string chk_H_5_5_6_Week
        {
            get { return this._chk_H_5_5_6_Week; }
            set { this._chk_H_5_5_6_Week = value; }
        }

        private string _chk_H_5_7_8_Week;
        public string chk_H_5_7_8_Week
        {
            get { return this._chk_H_5_7_8_Week; }
            set { this._chk_H_5_7_8_Week = value; }
        }

        private string _chk_H_5_Month;
        public string chk_H_5_Month
        {
            get { return this._chk_H_5_Month; }
            set { this._chk_H_5_Month = value; }
        }

        private string _chk_H_5_Return;
        public string chk_H_5_Return
        {
            get { return this._chk_H_5_Return; }
            set { this._chk_H_5_Return = value; }
        }

        private string _chk_I_1_Yes;
        public string chk_I_1_Yes
        {
            get { return this._chk_I_1_Yes; }
            set { this._chk_I_1_Yes = value; }
        }

        private string _chk_I_1_No;
        public string chk_I_1_No
        {
            get { return this._chk_I_1_No; }
            set { this._chk_I_1_No = value; }
        }

        private string _chk_I_1_PatientWorkingYes;
        public string chk_I_1_PatientWorkingYes
        {
            get { return this._chk_I_1_PatientWorkingYes; }
            set { this._chk_I_1_PatientWorkingYes = value; }
        }

        private string _chk_I_1_PatientWorkingNo;
        public string chk_I_1_PatientWorkingNo
        {
            get { return this._chk_I_1_PatientWorkingNo; }
            set { this._chk_I_1_PatientWorkingNo = value; }
        }

        private string _chk_I_1_PatientReturnYes;
        public string chk_I_1_PatientReturnYes
        {
            get { return this._chk_I_1_PatientReturnYes; }
            set { this._chk_I_1_PatientReturnYes = value; }
        }

        private string _chk_I_1_PatientReturnNo;
        public string chk_I_1_PatientReturnNo
        {
            get { return this._chk_I_1_PatientReturnNo; }
            set { this._chk_I_1_PatientReturnNo = value; }
        }

        private string _chk_I_2_a;
        public string chk_I_2_a
        {
            get { return this._chk_I_2_a; }
            set { this._chk_I_2_a = value; }
        }

        private string _chk_I_2_b;
        public string chk_I_2_b
        {
            get { return this._chk_I_2_b; }
            set { this._chk_I_2_b = value; }
        }

        private string _chk_I_2_C_Lifting;
        public string chk_I_2_C_Lifting
        {
            get { return this._chk_I_2_C_Lifting; }
            set { this._chk_I_2_C_Lifting = value; }
        }

        private string _chk_I_2_C_Bending;
        public string chk_I_2_C_Bending
        {
            get { return this._chk_I_2_C_Bending; }
            set { this._chk_I_2_C_Bending = value; }
        }

        private string _chk_I_2_C_Climbing;
        public string chk_I_2_C_Climbing
        {
            get { return this._chk_I_2_C_Climbing; }
            set { this._chk_I_2_C_Climbing = value; }
        }

        private string _chk_I_2_C_EnvironmentalCond;
        public string chk_I_2_C_EnvironmentalCond
        {
            get { return this._chk_I_2_C_EnvironmentalCond; }
            set { this._chk_I_2_C_EnvironmentalCond = value; }
        }

        private string _chk_I_2_C_Kneeling;
        public string chk_I_2_C_Kneeling
        {
            get { return this._chk_I_2_C_Kneeling; }
            set { this._chk_I_2_C_Kneeling = value; }
        }

        private string _chk_I_2_C_Other;
        public string chk_I_2_C_Other
        {
            get { return this._chk_I_2_C_Other; }
            set { this._chk_I_2_C_Other = value; }
        }


        //public string _chk_I_2_C_Lifting;
        //public string chk_I_2_C_Lifting
        //{
        //    get { return this._chk_I_2_C_Lifting; }
        //    set { this._chk_I_2_C_Lifting = value; }
        //}

        private string _chk_I_2_C_OperatingHeavy;
        public string chk_I_2_C_OperatingHeavy
        {
            get { return this._chk_I_2_C_OperatingHeavy; }
            set { this._chk_I_2_C_OperatingHeavy = value; }
        }

        private string _chk_I_2_C_OpMotorVehicle;
        public string chk_I_2_C_OpMotorVehicle
        {
            get { return this._chk_I_2_C_OpMotorVehicle; }
            set { this._chk_I_2_C_OpMotorVehicle = value; }
        }

        private string _chk_I_2_C_PersonalProtectiveEq;
        public string chk_I_2_C_PersonalProtectiveEq
        {
            get { return this._chk_I_2_C_PersonalProtectiveEq; }
            set { this._chk_I_2_C_PersonalProtectiveEq = value; }
        }


        private string _chk_I_2_C_Sitting;
        public string chk_I_2_C_Sitting
        {
            get { return this._chk_I_2_C_Sitting; }
            set { this._chk_I_2_C_Sitting = value; }
        }

        private string _chk_I_2_C_Standing;
        public string chk_I_2_C_Standing
        {
            get { return this._chk_I_2_C_Standing; }
            set { this._chk_I_2_C_Standing = value; }
        }

        private string _chk_I_2_C_UsePublicTrans;
        public string chk_I_2_C_UsePublicTrans
        {
            get { return this._chk_I_2_C_UsePublicTrans; }
            set { this._chk_I_2_C_UsePublicTrans = value; }
        }

        private string _chk_I_2_C_UseOfUpperExtremeities;
        public string chk_I_2_C_UseOfUpperExtremeities
        {
            get { return this._chk_I_2_C_UseOfUpperExtremeities; }
            set { this._chk_I_2_C_UseOfUpperExtremeities = value; }
        }

        private string _chk_H_2_1_2_Days;
        public string chk_H_2_1_2_Days
        {
            get { return this._chk_H_2_1_2_Days; }
            set { this._chk_H_2_1_2_Days = value; }
        }

        private string _chk_H_2_3_7_Days;
        public string chk_H_2_3_7_Days
        {
            get { return this._chk_H_2_3_7_Days; }
            set { this._chk_H_2_3_7_Days = value; }
        }

        private string _chk_H_2_8_14_Days;
        public string chk_H_2_8_14_Days
        {
            get { return this._chk_H_2_8_14_Days; }
            set { this._chk_H_2_8_14_Days = value; }
        }

        private string _chk_H_2_15P_Days;
        public string chk_H_2_15P_Days
        {
            get { return this._chk_H_2_15P_Days; }
            set { this._chk_H_2_15P_Days = value; }
        }

        private string _chk_H_2_Unknown;
        public string chk_H_2_Unknown
        {
            get { return this._chk_H_2_Unknown; }
            set { this._chk_H_2_Unknown = value; }
        }

        private string _chk_H_2_NA;
        public string chk_H_2_NA
        {
            get { return this._chk_H_2_NA; }
            set { this._chk_H_2_NA = value; }
        }

        private string _chk_H_3_Patient;
        public string chk_H_3_Patient
        {
            get { return this._chk_H_3_Patient; }
            set { this._chk_H_3_Patient = value; }
        }

        private string _chk_H_3_PatientsEmployer;
        public string chk_H_3_PatientsEmployer
        {
            get { return this._chk_H_3_PatientsEmployer; }
            set { this._chk_H_3_PatientsEmployer = value; }
        }

        private string _chk_H_4_NA;
        public string chk_H_4_NA
        {
            get { return this._chk_H_4_NA; }
            set { this._chk_H_4_NA = value; }
        }

        private string _chk_H_Provider_1;
        public string chk_H_Provider_1
        {
            get { return this._chk_H_Provider_1; }
            set { this._chk_H_Provider_1 = value; }
        }

        private string _chk_H_Provider_2;
        public string chk_H_Provider_2
        {
            get { return this._chk_H_Provider_2; }
            set { this._chk_H_Provider_2 = value; }
        }

        //BY MOHAN
        private string sEmployerPhone2;
        public string employerPhone2
        {
            get { return this.sEmployerPhone2; }
            set { this.sEmployerPhone2 = value; }
        }
        private string _SZ_EMPLOYER_ADDRESS;
        public string SZ_EMPLOYER_ADDRESS
        {
            get { return this._SZ_EMPLOYER_ADDRESS; }
            set { this._SZ_EMPLOYER_ADDRESS = value; }
        }
        private string _SZ_EMPLOYER_CITY;
        public string SZ_EMPLOYER_CITY
        {
            get { return this._SZ_EMPLOYER_CITY; }
            set { this._SZ_EMPLOYER_CITY = value; }
        }


        private string _chkMale;
        public string chkMale
        {
            get { return this._chkMale; }
            set { this._chkMale = value; }
        }
        private string _chkFemale;
        public string chkFemale
        {
            get { return this._chkFemale; }
            set { this._chkFemale = value; }
        }
        private string _chk_SSN;
        public string chk_SSN
        {
            get { return this._chk_SSN; }
            set { this._chk_SSN = value; }
        }

        private string _chk_EIN;
        public string chk_EIN
        {
            get { return this._chk_EIN; }
            set { this._chk_EIN = value; }
        }
        private string _chkPhysician;
        public string chkPhysician
        {
            get { return this._chkPhysician; }
            set { this._chkPhysician = value; }
        }
        private string _chkPodiatrist;
        public string chkPodiatrist
        {
            get { return this._chkPodiatrist; }
            set { this._chkPodiatrist = value; }
        }

        private string _chkChiropractor;
        public string chkChiropractor
        {
            get { return this._chkChiropractor; }
            set { this._chkChiropractor = value; }
        }
        private string _SZ_EMPLOYER_STATE;
        public string SZ_EMPLOYER_STATE
        {
            get { return this._SZ_EMPLOYER_STATE; }
            set { this._SZ_EMPLOYER_STATE = value; }
        }
        private string _SZ_EMPLOYER_ZIP;
        public string SZ_EMPLOYER_ZIP
        {
            get { return this._SZ_EMPLOYER_ZIP; }
            set { this._SZ_EMPLOYER_ZIP = value; }
        }

        private string _SZ_DOCTOR_NAME;
        public string SZ_DOCTOR_NAME
        {
            get { return this._SZ_DOCTOR_NAME; }
            set { this._SZ_DOCTOR_NAME = value; }
        }
        public string _SZ_WCB_AUTHORIZATION;
        public string SZ_WCB_AUTHORIZATION
        {
            get { return this._SZ_WCB_AUTHORIZATION; }
            set { this._SZ_WCB_AUTHORIZATION = value; }
        }
        private string _SZ_WCB_RATING_CODE;
        public string SZ_WCB_RATING_CODE
        {
            get { return this._SZ_WCB_RATING_CODE; }
            set { this._SZ_WCB_RATING_CODE = value; }
        }

        private string _SZ_FEDERAL_TAX_ID;
        public string SZ_FEDERAL_TAX_ID
        {
            get { return this._SZ_FEDERAL_TAX_ID; }
            set { this._SZ_FEDERAL_TAX_ID = value; }
        }

        private string _SZ_OFFICE_ADDRESS;
        public string SZ_OFFICE_ADDRESS
        {
            get { return this._SZ_OFFICE_ADDRESS; }
            set { this._SZ_OFFICE_ADDRESS = value; }
        }
        private string _SZ_OFFICE_CITY;
        public string SZ_OFFICE_CITY
        {
            get { return this._SZ_OFFICE_CITY; }
            set { this._SZ_OFFICE_CITY = value; }
        }
        private string _SZ_OFFICE_STATE;
        public string SZ_OFFICE_STATE
        {
            get { return this._SZ_OFFICE_STATE; }
            set { this._SZ_OFFICE_STATE = value; }
        }
        private string _SZ_OFFICE_ZIP;
        public string SZ_OFFICE_ZIP
        {
            get { return this._SZ_OFFICE_ZIP; }
            set { this._SZ_OFFICE_ZIP = value; }
        }

        private string _SZ_BILLING_GROUP_NAME;
        public string SZ_BILLING_GROUP_NAME
        {
            get { return this._SZ_BILLING_GROUP_NAME; }
            set { this._SZ_BILLING_GROUP_NAME = value; }
        }
        private string _SZ_BILLING_ADDRESS;
        public string SZ_BILLING_ADDRESS
        {
            get { return this._SZ_BILLING_ADDRESS; }
            set { this._SZ_BILLING_ADDRESS = value; }
        }
        private string _SZ_BILLING_CITY;
        public string SZ_BILLING_CITY
        {
            get { return this._SZ_BILLING_CITY; }
            set { this._SZ_BILLING_CITY = value; }
        }

        private string _SZ_BILLING_STATE;
        public string SZ_BILLING_STATE
        {
            get { return this._SZ_BILLING_STATE; }
            set { this._SZ_BILLING_STATE = value; }
        }
        private string _SZ_BILLING_ZIP;
        public string SZ_BILLING_ZIP
        {
            get { return this._SZ_BILLING_ZIP; }
            set { this._SZ_BILLING_ZIP = value; }
        }
        private string _SZ_OFFICE_PHONE1;
        public string SZ_OFFICE_PHONE1
        {
            get { return this._SZ_OFFICE_PHONE1; }
            set { this._SZ_OFFICE_PHONE1 = value; }
        }

        private string _SZ_OFFICE_PHONE2;
        public string SZ_OFFICE_PHONE2
        {
            get { return this._SZ_OFFICE_PHONE2; }
            set { this._SZ_OFFICE_PHONE2 = value; }
        }
        private string _SZ_BILLING_PHONE1;
        public string SZ_BILLING_PHONE1
        {
            get { return this._SZ_BILLING_PHONE1; }
            set { this._SZ_BILLING_PHONE1 = value; }
        }
        private string _SZ_BILLING_PHONE2;
        public string SZ_BILLING_PHONE2
        {
            get { return this._SZ_BILLING_PHONE2; }
            set { this._SZ_BILLING_PHONE2 = value; }
        }

        private string _SZ_NPI;
        public string SZ_NPI
        {
            get { return this._SZ_NPI; }
            set { this._SZ_NPI = value; }
        }
        private string _SZ_INSURANCE_NAME;
        public string SZ_INSURANCE_NAME
        {
            get { return this._SZ_INSURANCE_NAME; }
            set { this._SZ_INSURANCE_NAME = value; }
        }
        private string _SZ_INSURANCE_STREET;
        public string SZ_INSURANCE_STREET
        {
            get { return this._SZ_INSURANCE_STREET; }
            set { this._SZ_INSURANCE_STREET = value; }
        }

        private string _SZ_INSURANCE_CITY;
        public string SZ_INSURANCE_CITY
        {
            get { return this._SZ_INSURANCE_CITY; }
            set { this._SZ_INSURANCE_CITY = value; }
        }
        private string _SZ_INSURANCE_STATE;
        public string SZ_INSURANCE_STATE
        {
            get { return this._SZ_INSURANCE_STATE; }
            set { this._SZ_INSURANCE_STATE = value; }
        }
        private string _SZ_INSURANCE_ZIP;
        public string SZ_INSURANCE_ZIP
        {
            get { return this._SZ_INSURANCE_ZIP; }
            set { this._SZ_INSURANCE_ZIP = value; }
        }
        ///

        private string _SZ_DIAGNOSIS_CODE;
        public string SZ_DIAGNOSIS_CODE
        {
            get { return this._SZ_DIAGNOSIS_CODE; }
            set { this._SZ_DIAGNOSIS_CODE = value; }
        }


        private string _SZ_BILL_ID;
        public string SZ_BILL_ID
        {
            get { return this._SZ_BILL_ID; }
            set { this._SZ_BILL_ID = value; }
        }
        private string _FROM_MM_1;
        public string FROM_MM_1
        {
            get { return this._FROM_MM_1; }
            set { this._FROM_MM_1 = value; }
        }
        private string _FROM_MM_2;
        public string FROM_MM_2
        {
            get { return this._FROM_MM_2; }
            set { this._FROM_MM_2 = value; }
        }

        private string _FROM_MM_3;
        public string FROM_MM_3
        {
            get { return this._FROM_MM_3; }
            set { this._FROM_MM_3 = value; }
        }
        private string _FROM_MM_4;
        public string FROM_MM_4
        {
            get { return this._FROM_MM_4; }
            set { this._FROM_MM_4 = value; }
        }
        private string _FROM_MM_5;
        public string FROM_MM_5
        {
            get { return this._FROM_MM_5; }
            set { this._FROM_MM_5 = value; }
        }
        private string _FROM_MM_6;
        public string FROM_MM_6
        {
            get { return this._FROM_MM_6; }
            set { this._FROM_MM_6 = value; }
        }

        private string _FROM_DD_1;
        public string FROM_DD_1
        {
            get { return this._FROM_DD_1; }
            set { this._FROM_DD_1 = value; }
        }
        private string _FROM_DD_2;
        public string FROM_DD_2
        {
            get { return this._FROM_DD_2; }
            set { this._FROM_DD_2 = value; }
        }
        private string _FROM_DD_3;
        public string FROM_DD_3
        {
            get { return this._FROM_DD_3; }
            set { this._FROM_DD_3 = value; }
        }
        private string _FROM_DD_4;
        public string FROM_DD_4
        {
            get { return this._FROM_DD_4; }
            set { this._FROM_DD_4 = value; }
        }

        private string _FROM_DD_5;
        public string FROM_DD_5
        {
            get { return this._FROM_DD_5; }
            set { this._FROM_DD_5 = value; }
        }
        private string _FROM_DD_6;
        public string FROM_DD_6
        {
            get { return this._FROM_DD_6; }
            set { this._FROM_DD_6 = value; }
        }
        private string _FROM_YY_1;
        public string FROM_YY_1
        {
            get { return this._FROM_YY_1; }
            set { this._FROM_YY_1 = value; }
        }
        private string _FROM_YY_2;
        public string FROM_YY_2
        {
            get { return this._FROM_YY_2; }
            set { this._FROM_YY_2 = value; }
        }

        private string _FROM_YY_3;
        public string FROM_YY_3
        {
            get { return this._FROM_YY_3; }
            set { this._FROM_YY_3 = value; }
        }
        private string _FROM_YY_4;
        public string FROM_YY_4
        {
            get { return this._FROM_YY_4; }
            set { this._FROM_YY_4 = value; }
        }
        private string _FROM_YY_5;
        public string FROM_YY_5
        {
            get { return this._FROM_YY_5; }
            set { this._FROM_YY_5 = value; }
        }
        private string _FROM_YY_6;
        public string FROM_YY_6
        {
            get { return this._FROM_YY_6; }
            set { this._FROM_YY_6 = value; }
        }

        private string _TO_MM_1;
        public string TO_MM_1
        {
            get { return this._TO_MM_1; }
            set { this._TO_MM_1 = value; }
        }
        private string _TO_MM_2;
        public string TO_MM_2
        {
            get { return this._TO_MM_2; }
            set { this._TO_MM_2 = value; }
        }
        private string _TO_MM_3;
        public string TO_MM_3
        {
            get { return this._TO_MM_3; }
            set { this._TO_MM_3 = value; }
        }
        private string _TO_MM_4;
        public string TO_MM_4
        {
            get { return this._TO_MM_4; }
            set { this._TO_MM_4 = value; }
        }

        private string _TO_MM_5;
        public string TO_MM_5
        {
            get { return this._TO_MM_5; }
            set { this._TO_MM_5 = value; }
        }
        private string _TO_MM_6;
        public string TO_MM_6
        {
            get { return this._TO_MM_6; }
            set { this._TO_MM_6 = value; }
        }
        private string _TO_DD_1;
        public string TO_DD_1
        {
            get { return this._TO_DD_1; }
            set { this._TO_DD_1 = value; }
        }
        private string _TO_DD_2;
        public string TO_DD_2
        {
            get { return this._TO_DD_2; }
            set { this._TO_DD_2 = value; }
        }

        private string _TO_DD_3;
        public string TO_DD_3
        {
            get { return this._TO_DD_3; }
            set { this._TO_DD_3 = value; }
        }
        private string _TO_DD_4;
        public string TO_DD_4
        {
            get { return this._TO_DD_4; }
            set { this._TO_DD_4 = value; }
        }
        private string _TO_DD_5;
        public string TO_DD_5
        {
            get { return this._TO_DD_5; }
            set { this._TO_DD_5 = value; }
        }
        private string _TO_DD_6;
        public string TO_DD_6
        {
            get { return this._TO_DD_6; }
            set { this._TO_DD_6 = value; }
        }

        private string _TO_YY_1;
        public string TO_YY_1
        {
            get { return this._TO_YY_1; }
            set { this._TO_YY_1 = value; }
        }
        private string _TO_YY_2;
        public string TO_YY_2
        {
            get { return this._TO_YY_2; }
            set { this._TO_YY_2 = value; }
        }
        private string _TO_YY_3;
        public string TO_YY_3
        {
            get { return this._TO_YY_3; }
            set { this._TO_YY_3 = value; }
        }
        private string _TO_YY_4;
        public string TO_YY_4
        {
            get { return this._TO_YY_4; }
            set { this._TO_YY_4 = value; }
        }

        private string _TO_YY_5;
        public string TO_YY_5
        {
            get { return this._TO_YY_5; }
            set { this._TO_YY_5 = value; }
        }
        private string _TO_YY_6;
        public string TO_YY_6
        {
            get { return this._TO_YY_6; }
            set { this._TO_YY_6 = value; }
        }
        private string _POS_1;
        public string POS_1
        {
            get { return this._POS_1; }
            set { this._POS_1 = value; }
        }
        private string _POS_2;
        public string POS_2
        {
            get { return this._POS_2; }
            set { this._POS_2 = value; }
        }

        private string _POS_3;
        public string POS_3
        {
            get { return this._POS_3; }
            set { this._POS_3 = value; }
        }
        private string _POS_4;
        public string POS_4
        {
            get { return this._POS_4; }
            set { this._POS_4 = value; }
        }
        private string _POS_5;
        public string POS_5
        {
            get { return this._POS_5; }
            set { this._POS_5 = value; }
        }
        private string _POS_6;
        public string POS_6
        {
            get { return this._POS_6; }
            set { this._POS_6 = value; }
        }

        private string _CPT_1;
        public string CPT_1
        {
            get { return this._CPT_1; }
            set { this._CPT_1 = value; }
        }
        private string _CPT_2;
        public string CPT_2
        {
            get { return this._CPT_2; }
            set { this._CPT_2 = value; }
        }
        private string _CPT_3;
        public string CPT_3
        {
            get { return this._CPT_3; }
            set { this._CPT_3 = value; }
        }
        private string _CPT_4;
        public string CPT_4
        {
            get { return this._CPT_4; }
            set { this._CPT_4 = value; }
        }

        private string _CPT_5;
        public string CPT_5
        {
            get { return this._CPT_5; }
            set { this._CPT_5 = value; }
        }
        private string _CPT_6;
        public string CPT_6
        {
            get { return this._CPT_6; }
            set { this._CPT_6 = value; }
        }
        private string _MODIFIER_1;
        public string MODIFIER_1
        {
            get { return this._MODIFIER_1; }
            set { this._MODIFIER_1 = value; }
        }
        private string _MODIFIER_2;
        public string MODIFIER_2
        {
            get { return this._MODIFIER_2; }
            set { this._MODIFIER_2 = value; }
        }

        private string _MODIFIER_3;
        public string MODIFIER_3
        {
            get { return this._MODIFIER_3; }
            set { this._MODIFIER_3 = value; }
        }
        private string _MODIFIER_4;
        public string MODIFIER_4
        {
            get { return this._MODIFIER_4; }
            set { this._MODIFIER_4 = value; }
        }
        private string _MODIFIER_5;
        public string MODIFIER_5
        {
            get { return this._MODIFIER_5; }
            set { this._MODIFIER_5 = value; }
        }
        private string _MODIFIER_6;
        public string MODIFIER_6
        {
            get { return this._MODIFIER_6; }
            set { this._MODIFIER_6 = value; }
        }

        private string _SZ_MODIFIER_B1;
        public string SZ_MODIFIER_B1
        {
            get { return this._SZ_MODIFIER_B1; }
            set { this._SZ_MODIFIER_B1 = value; }
        }
        private string _SZ_MODIFIER_B2;
        public string SZ_MODIFIER_B2
        {
            get { return this._SZ_MODIFIER_B2; }
            set { this._SZ_MODIFIER_B2 = value; }
        }
        private string _SZ_MODIFIER_B3;
        public string SZ_MODIFIER_B3
        {
            get { return this._SZ_MODIFIER_B3; }
            set { this._SZ_MODIFIER_B3 = value; }
        }

        //
        private string _SZ_MODIFIER_B4;
        public string SZ_MODIFIER_B4
        {
            get { return this._SZ_MODIFIER_B4; }
            set { this._SZ_MODIFIER_B4 = value; }
        }
        private string _SZ_MODIFIER_B5;
        public string SZ_MODIFIER_B5
        {
            get { return this._SZ_MODIFIER_B5; }
            set { this._SZ_MODIFIER_B5 = value; }
        }

        private string _SZ_MODIFIER_B6;
        public string SZ_MODIFIER_B6
        {
            get { return this._SZ_MODIFIER_B6; }
            set { this._SZ_MODIFIER_B6 = value; }
        }
        private string _DC_1;
        public string DC_1
        {
            get { return this._DC_1; }
            set { this._DC_1 = value; }
        }
        private string _DC_2;
        public string DC_2
        {
            get { return this._DC_2; }
            set { this._DC_2 = value; }
        }
        private string _DC_3;
        public string DC_3
        {
            get { return this._DC_3; }
            set { this._DC_3 = value; }
        }

        private string _DC_4;
        public string DC_4
        {
            get { return this._DC_4; }
            set { this._DC_4 = value; }
        }
        private string _DC_5;
        public string DC_5
        {
            get { return this._DC_5; }
            set { this._DC_5 = value; }
        }
        private string _DC_6;
        public string DC_6
        {
            get { return this._DC_6; }
            set { this._DC_6 = value; }
        }
        private string _CHARGE_1;
        public string CHARGE_1
        {
            get { return this._CHARGE_1; }
            set { this._CHARGE_1 = value; }
        }

        private string _CHARGE_2;
        public string CHARGE_2
        {
            get { return this._CHARGE_2; }
            set { this._CHARGE_2 = value; }
        }
        private string _CHARGE_3;
        public string CHARGE_3
        {
            get { return this._CHARGE_3; }
            set { this._CHARGE_3 = value; }
        }
        private string _CHARGE_4;
        public string CHARGE_4
        {
            get { return this._CHARGE_4; }
            set { this._CHARGE_4 = value; }
        }
        private string _CHARGE_5;
        public string CHARGE_5
        {
            get { return this._CHARGE_5; }
            set { this._CHARGE_5 = value; }
        }

        private string _CHARGE_6;
        public string CHARGE_6
        {
            get { return this._CHARGE_6; }
            set { this._CHARGE_6 = value; }
        }
        private string _UNIT_1;
        public string UNIT_1
        {
            get { return this._UNIT_1; }
            set { this._UNIT_1 = value; }
        }
        private string _UNIT_2;
        public string UNIT_2
        {
            get { return this._UNIT_2; }
            set { this._UNIT_2 = value; }
        }
        private string _UNIT_3;
        public string UNIT_3
        {
            get { return this._UNIT_3; }
            set { this._UNIT_3 = value; }
        }
        private string _UNIT_4;
        public string UNIT_4
        {
            get { return this._UNIT_4; }
            set { this._UNIT_4 = value; }
        }

        private string _UNIT_5;
        public string UNIT_5
        {
            get { return this._UNIT_5; }
            set { this._UNIT_5 = value; }
        }
        private string _UNIT_6;
        public string UNIT_6
        {
            get { return this._UNIT_6; }
            set { this._UNIT_6 = value; }
        }
        private string _COB_1;
        public string COB_1
        {
            get { return this._COB_1; }
            set { this._COB_1 = value; }
        }
        private string _COB_2;
        public string COB_2
        {
            get { return this._COB_2; }
            set { this._COB_2 = value; }
        }

        private string _COB_3;
        public string COB_3
        {
            get { return this._COB_3; }
            set { this._COB_3 = value; }
        }
        private string _COB_4;
        public string COB_4
        {
            get { return this._COB_4; }
            set { this._COB_4 = value; }
        }
        private string _COB_5;
        public string COB_5
        {
            get { return this._COB_5; }
            set { this._COB_5 = value; }
        }
        private string _COB_6;
        public string COB_6
        {
            get { return this._COB_6; }
            set { this._COB_6 = value; }
        }

        private string _ZIP_1;
        public string ZIP_1
        {
            get { return this._ZIP_1; }
            set { this._ZIP_1 = value; }
        }
        private string _ZIP_2;
        public string ZIP_2
        {
            get { return this._ZIP_2; }
            set { this._ZIP_2 = value; }
        }
        private string _ZIP_3;
        public string ZIP_3
        {
            get { return this._ZIP_3; }
            set { this._ZIP_3 = value; }
        }
        private string _ZIP_4;
        public string ZIP_4
        {
            get { return this._ZIP_4; }
            set { this._ZIP_4 = value; }
        }

        private string _ZIP_5;
        public string ZIP_5
        {
            get { return this._ZIP_5; }
            set { this._ZIP_5 = value; }
        }
        private string _ZIP_6;
        public string ZIP_6
        {
            get { return this._ZIP_6; }
            set { this._ZIP_6 = value; }
        }
        private string _TOTAL_BILL_AMOUNT;
        public string TOTAL_BILL_AMOUNT
        {
            get { return this._TOTAL_BILL_AMOUNT; }
            set { this._TOTAL_BILL_AMOUNT = value; }
        }
        private string _TOTAL_PAID_AMOUNT;
        public string TOTAL_PAID_AMOUNT
        {
            get { return this._TOTAL_PAID_AMOUNT; }
            set { this._TOTAL_PAID_AMOUNT = value; }
        }
        private string _TOTAL_BAL_AMOUNT;
        public string TOTAL_BAL_AMOUNT
        {
            get { return this._TOTAL_BAL_AMOUNT; }
            set { this._TOTAL_BAL_AMOUNT = value; }
        }

        private string _SZ_INJURY_ILLNESS_DETAIL;
        public string SZ_INJURY_ILLNESS_DETAIL
        {
            get { return this._SZ_INJURY_ILLNESS_DETAIL; }
            set { this._SZ_INJURY_ILLNESS_DETAIL = value; }
        }
        private string _SZ_INJURY_ILLNESS_DETAIL_1;
        public string SZ_INJURY_ILLNESS_DETAIL_1
        {
            get { return this._SZ_INJURY_ILLNESS_DETAIL_1; }
            set { this._SZ_INJURY_ILLNESS_DETAIL_1 = value; }
        }
        private string _SZ_INJURY_ILLNESS_DETAIL_2;
        public string SZ_INJURY_ILLNESS_DETAIL_2
        {
            get { return this._SZ_INJURY_ILLNESS_DETAIL_2; }
            set { this._SZ_INJURY_ILLNESS_DETAIL_2 = value; }
        }
        private string _SZ_INJURY_LEARN_SOURCE_DESCRIPTION;
        public string SZ_INJURY_LEARN_SOURCE_DESCRIPTION
        {
            get { return this._SZ_INJURY_LEARN_SOURCE_DESCRIPTION; }
            set { this._SZ_INJURY_LEARN_SOURCE_DESCRIPTION = value; }
        }

        private string _SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION;
        public string SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION
        {
            get { return this._SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION; }
            set { this._SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION = value; }
        }
        private string _DT_PREVIOUSLY_TREATED_DATE;
        public string DT_PREVIOUSLY_TREATED_DATE
        {
            get { return this._DT_PREVIOUSLY_TREATED_DATE; }
            set { this._DT_PREVIOUSLY_TREATED_DATE = value; }
        }
        private string _DT_DATE_OF_EXAMINATION;
        public string DT_DATE_OF_EXAMINATION
        {
            get { return this._DT_DATE_OF_EXAMINATION; }
            set { this._DT_DATE_OF_EXAMINATION = value; }
        }
        private string _txt_F_Numbness;
        public string txt_F_Numbness
        {
            get { return this._txt_F_Numbness; }
            set { this._txt_F_Numbness = value; }
        }

        private string _txt_F_Pain;
        public string txt_F_Pain
        {
            get { return this._txt_F_Pain; }
            set { this._txt_F_Pain = value; }
        }
        private string _txt_F_Stiffness;
        public string txt_F_Stiffness
        {
            get { return this._txt_F_Stiffness; }
            set { this._txt_F_Stiffness = value; }
        }
        private string _txt_F_Swelling;
        public string txt_F_Swelling
        {
            get { return this._txt_F_Swelling; }
            set { this._txt_F_Swelling = value; }
        }
        private string _txt_F_Weakness;
        public string txt_F_Weakness
        {
            get { return this._txt_F_Weakness; }
            set { this._txt_F_Weakness = value; }
        }

        private string _txt_F_Other;
        public string txt_F_Other
        {
            get { return this._txt_F_Other; }
            set { this._txt_F_Other = value; }
        }
        private string _txt_F_3_Abrasion;
        public string txt_F_3_Abrasion
        {
            get { return this._txt_F_3_Abrasion; }
            set { this._txt_F_3_Abrasion = value; }
        }
        private string _txt_F_3_Amputation;
        public string txt_F_3_Amputation
        {
            get { return this._txt_F_3_Amputation; }
            set { this._txt_F_3_Amputation = value; }
        }
        private string _txt_F_3_Avulsion;
        public string txt_F_3_Avulsion
        {
            get { return this._txt_F_3_Avulsion; }
            set { this._txt_F_3_Avulsion = value; }
        }
        private string _txt_F_3_Bite;
        public string txt_F_3_Bite
        {
            get { return this._txt_F_3_Bite; }
            set { this._txt_F_3_Bite = value; }
        }

        private string _txt_F_3_Burn;
        public string txt_F_3_Burn
        {
            get { return this._txt_F_3_Burn; }
            set { this._txt_F_3_Burn = value; }
        }
        private string _txt_F_3_Contusion;
        public string txt_F_3_Contusion
        {
            get { return this._txt_F_3_Contusion; }
            set { this._txt_F_3_Contusion = value; }
        }
        private string _txt_F_3_CurshInjury;
        public string txt_F_3_CurshInjury
        {
            get { return this._txt_F_3_CurshInjury; }
            set { this._txt_F_3_CurshInjury = value; }
        }
        private string _txt_F_3_Dermatitis;
        public string txt_F_3_Dermatitis
        {
            get { return this._txt_F_3_Dermatitis; }
            set { this._txt_F_3_Dermatitis = value; }
        }

        private string _txt_F_3_Dislocation;
        public string txt_F_3_Dislocation
        {
            get { return this._txt_F_3_Dislocation; }
            set { this._txt_F_3_Dislocation = value; }
        }
        private string _txt_F_3_Fracture;
        public string txt_F_3_Fracture
        {
            get { return this._txt_F_3_Fracture; }
            set { this._txt_F_3_Fracture = value; }
        }
        private string _txt_F_3_HearingLoss;
        public string txt_F_3_HearingLoss
        {
            get { return this._txt_F_3_HearingLoss; }
            set { this._txt_F_3_HearingLoss = value; }
        }
        private string _txt_F_3_Hernia;
        public string txt_F_3_Hernia
        {
            get { return this._txt_F_3_Hernia; }
            set { this._txt_F_3_Hernia = value; }
        }

        private string _txt_F_3_Other;
        public string txt_F_3_Other
        {
            get { return this._txt_F_3_Other; }
            set { this._txt_F_3_Other = value; }
        }
        private string _txt_F_3_Other_1;
        public string txt_F_3_Other_1
        {
            get { return this._txt_F_3_Other_1; }
            set { this._txt_F_3_Other_1 = value; }
        }
        private string _txt_F_3_InfectiousDisease;
        public string txt_F_3_InfectiousDisease
        {
            get { return this._txt_F_3_InfectiousDisease; }
            set { this._txt_F_3_InfectiousDisease = value; }
        }
        private string _txt_F_3_Inhalation_Exposure;
        public string txt_F_3_Inhalation_Exposure
        {
            get { return this._txt_F_3_Inhalation_Exposure; }
            set { this._txt_F_3_Inhalation_Exposure = value; }
        }

        private string _txt_F_3_Laceration;
        public string txt_F_3_Laceration
        {
            get { return this._txt_F_3_Laceration; }
            set { this._txt_F_3_Laceration = value; }
        }
        private string _txt_F_3_NeedleStick;
        public string txt_F_3_NeedleStick
        {
            get { return this._txt_F_3_NeedleStick; }
            set { this._txt_F_3_NeedleStick = value; }
        }
        private string _txt_F_3_Poisoning;
        public string txt_F_3_Poisoning
        {
            get { return this._txt_F_3_Poisoning; }
            set { this._txt_F_3_Poisoning = value; }
        }
        //

        private string _txt_F_3_Psychological;
        public string txt_F_3_Psychological
        {
            get { return this._txt_F_3_Psychological; }
            set { this._txt_F_3_Psychological = value; }
        }
        private string _txt_F_3_PuntureWound;
        public string txt_F_3_PuntureWound
        {
            get { return this._txt_F_3_PuntureWound; }
            set { this._txt_F_3_PuntureWound = value; }
        }
        private string _txt_F_3_RepetitiveStrainInjury;
        public string txt_F_3_RepetitiveStrainInjury
        {
            get { return this._txt_F_3_RepetitiveStrainInjury; }
            set { this._txt_F_3_RepetitiveStrainInjury = value; }
        }
        private string _txt_F_3_SpinalCordInjury;
        public string txt_F_3_SpinalCordInjury
        {
            get { return this._txt_F_3_SpinalCordInjury; }
            set { this._txt_F_3_SpinalCordInjury = value; }
        }

        private string _txt_F_3_Sprain;
        public string txt_F_3_Sprain
        {
            get { return this._txt_F_3_Sprain; }
            set { this._txt_F_3_Sprain = value; }
        }
        private string _txt_F_3_Torn;
        public string txt_F_3_Torn
        {
            get { return this._txt_F_3_Torn; }
            set { this._txt_F_3_Torn = value; }
        }
        private string _txt_F_3_VisionLoss;
        public string txt_F_3_VisionLoss
        {
            get { return this._txt_F_3_VisionLoss; }
            set { this._txt_F_3_VisionLoss = value; }
        }
        private string _chkService;
        public string chkService
        {
            get { return this._chkService; }
            set { this._chkService = value; }
        }
        private string _chk_H_Patient;
        public string chk_H_Patient
        {
            get { return this._chk_H_Patient; }
            set { this._chk_H_Patient = value; }
        }

        private string _chk_H_MedicalRecords;
        public string chk_H_MedicalRecords
        {
            get { return this._chk_H_MedicalRecords; }
            set { this._chk_H_MedicalRecords = value; }
        }
        private string _chk_H_Other;
        public string chk_H_Other
        {
            get { return this._chk_H_Other; }
            set { this._chk_H_Other = value; }
        }
        private string _chk_H_Yes;
        public string chk_H_Yes
        {
            get { return this._chk_H_Yes; }
            set { this._chk_H_Yes = value; }
        }
        private string _chk_H_No;
        public string chk_H_No
        {
            get { return this._chk_H_No; }
            set { this._chk_H_No = value; }
        }

        private string _chk_H_4_Yes;
        public string chk_H_4_Yes
        {
            get { return this._chk_H_4_Yes; }
            set { this._chk_H_4_Yes = value; }
        }
        private string _chk_H_4_No;
        public string chk_H_4_No
        {
            get { return this._chk_H_4_No; }
            set { this._chk_H_4_No = value; }
        }
        private string _chk_F_Numbness;
        public string chk_F_Numbness
        {
            get { return this._chk_F_Numbness; }
            set { this._chk_F_Numbness = value; }
        }
        private string _chk_F_Pain;
        public string chk_F_Pain
        {
            get { return this._chk_F_Pain; }
            set { this._chk_F_Pain = value; }
        }

        private string _chk_F_Stiffness;
        public string chk_F_Stiffness
        {
            get { return this._chk_F_Stiffness; }
            set { this._chk_F_Stiffness = value; }
        }
        private string _chk_F_Swelling;
        public string chk_F_Swelling
        {
            get { return this._chk_F_Swelling; }
            set { this._chk_F_Swelling = value; }
        }
        private string _chk_F_Weakness;
        public string chk_F_Weakness
        {
            get { return this._chk_F_Weakness; }
            set { this._chk_F_Weakness = value; }
        }
        private string _chk_F_Other;
        public string chk_F_Other
        {
            get { return this._chk_F_Other; }
            set { this._chk_F_Other = value; }
        }

        private string _chk_F_3_Abrasion;
        public string chk_F_3_Abrasion
        {
            get { return this._chk_F_3_Abrasion; }
            set { this._chk_F_3_Abrasion = value; }
        }
        private string _chk_F_3_Amputation;
        public string chk_F_3_Amputation
        {
            get { return this._chk_F_3_Amputation; }
            set { this._chk_F_3_Amputation = value; }
        }
        private string _chk_F_3_Avulsion;
        public string chk_F_3_Avulsion
        {
            get { return this._chk_F_3_Avulsion; }
            set { this._chk_F_3_Avulsion = value; }
        }
        private string _chk_F_3_Bite;
        public string chk_F_3_Bite
        {
            get { return this._chk_F_3_Bite; }
            set { this._chk_F_3_Bite = value; }
        }
        private string _chk_F_3_Burn;
        public string chk_F_3_Burn
        {
            get { return this._chk_F_3_Burn; }
            set { this._chk_F_3_Burn = value; }
        }

        private string _chk_F_3_Contusion;
        public string chk_F_3_Contusion
        {
            get { return this._chk_F_3_Contusion; }
            set { this._chk_F_3_Contusion = value; }
        }
        private string _chk_F_3_CurshInjury;
        public string chk_F_3_CurshInjury
        {
            get { return this._chk_F_3_CurshInjury; }
            set { this._chk_F_3_CurshInjury = value; }
        }
        private string _chk_F_3_Dermatitis;
        public string chk_F_3_Dermatitis
        {
            get { return this._chk_F_3_Dermatitis; }
            set { this._chk_F_3_Dermatitis = value; }
        }
        private string _chk_F_3_Dislocation;
        public string chk_F_3_Dislocation
        {
            get { return this._chk_F_3_Dislocation; }
            set { this._chk_F_3_Dislocation = value; }
        }

        private string _chk_F_3_Fracture;
        public string chk_F_3_Fracture
        {
            get { return this._chk_F_3_Fracture; }
            set { this._chk_F_3_Fracture = value; }
        }
        private string _chk_F_3_HearingLoss;
        public string chk_F_3_HearingLoss
        {
            get { return this._chk_F_3_HearingLoss; }
            set { this._chk_F_3_HearingLoss = value; }
        }
        private string _chk_F_3_Hernia;
        public string chk_F_3_Hernia
        {
            get { return this._chk_F_3_Hernia; }
            set { this._chk_F_3_Hernia = value; }
        }
        private string _chk_F_3_Other;
        public string chk_F_3_Other
        {
            get { return this._chk_F_3_Other; }
            set { this._chk_F_3_Other = value; }
        }

        private string _chk_F_3_InfectiousDisease;
        public string chk_F_3_InfectiousDisease
        {
            get { return this._chk_F_3_InfectiousDisease; }
            set { this._chk_F_3_InfectiousDisease = value; }
        }
        private string _chk_F_3_Inhalation_Exposure;
        public string chk_F_3_Inhalation_Exposure
        {
            get { return this._chk_F_3_Inhalation_Exposure; }
            set { this._chk_F_3_Inhalation_Exposure = value; }
        }
        private string _chk_F_3_Laceration;
        public string chk_F_3_Laceration
        {
            get { return this._chk_F_3_Laceration; }
            set { this._chk_F_3_Laceration = value; }
        }
        private string _chk_F_3_NeedleStick;
        public string chk_F_3_NeedleStick
        {
            get { return this._chk_F_3_NeedleStick; }
            set { this._chk_F_3_NeedleStick = value; }
        }

        private string _chk_F_3_Poisoning;
        public string chk_F_3_Poisoning
        {
            get { return this._chk_F_3_Poisoning; }
            set { this._chk_F_3_Poisoning = value; }
        }
        private string _chk_F_3_Psychological;
        public string chk_F_3_Psychological
        {
            get { return this._chk_F_3_Psychological; }
            set { this._chk_F_3_Psychological = value; }
        }
        private string _chk_F_3_PuntureWound;
        public string chk_F_3_PuntureWound
        {
            get { return this._chk_F_3_PuntureWound; }
            set { this._chk_F_3_PuntureWound = value; }
        }

        private string _chk_F_3_RepetitiveStrainInjury;
        public string chk_F_3_RepetitiveStrainInjury
        {
            get { return this._chk_F_3_RepetitiveStrainInjury; }
            set { this._chk_F_3_RepetitiveStrainInjury = value; }
        }
        private string _chk_F_3_SpinalCordInjury;
        public string chk_F_3_SpinalCordInjury
        {
            get { return this._chk_F_3_SpinalCordInjury; }
            set { this._chk_F_3_SpinalCordInjury = value; }
        }
        private string _chk_F_3_Sprain;
        public string chk_F_3_Sprain
        {
            get { return this._chk_F_3_Sprain; }
            set { this._chk_F_3_Sprain = value; }
        }
        private string _chk_F_3_Torn;
        public string chk_F_3_Torn
        {
            get { return this._chk_F_3_Torn; }
            set { this._chk_F_3_Torn = value; }
        }

        private string _chk_F_3_VisionLoss;
        public string chk_F_3_VisionLoss
        {
            get { return this._chk_F_3_VisionLoss; }
            set { this._chk_F_3_VisionLoss = value; }
        }

        private string _chk_I_2_c;
        public string chk_I_2_c
        {
            get { return this._chk_I_2_c; }
            set { this._chk_I_2_c = value; }
        }
    }
}
