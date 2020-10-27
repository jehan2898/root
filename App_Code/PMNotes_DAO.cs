using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PMNotes
{
    public class PMNotes_DAO
    {
        private string _PatientName;

        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        private DateTime _dtDateOfAccident;

        public DateTime DateOfAccident
        {
            get { return _dtDateOfAccident; }
            set { _dtDateOfAccident = value; }
        }

        private DateTime _dtVisitDate;

        public DateTime VisitDate
        {
            get { return _dtVisitDate; }
            set { _dtVisitDate = value; }
        }

        private DateTime _dtDtDOB;

        public DateTime DOB
        {
            get { return _dtDtDOB; }
            set { _dtDtDOB = value; }
        }

        private string _PatientAddress;

        public string PatientAddress
        {
            get { return _PatientAddress; }
            set { _PatientAddress = value; }
        }

        private string _CurrentAllergies;

        public string CurrentAllergies
        {
            get { return _CurrentAllergies; }
            set { _CurrentAllergies = value; }
        }

        private string _CurrentMedications;

        public string CurrentMedications
        {
            get { return _CurrentMedications; }
            set { _CurrentMedications = value; }
        }

        private string _Height;

        public string Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private string _Weight;

        public string Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }

        private string _Medical;

        public string Medical
        {
            get { return _Medical; }
            set { _Medical = value; }
        }

        private string _Injuries;

        public string Injuries
        {
            get { return _Injuries; }
            set { _Injuries = value; }
        }

        private string _Surguries;

        public string Surguries
        {
            get { return _Surguries; }
            set { _Surguries = value; }
        }

        private string _SocialHistory;

        public string SocialHistory
        {
            get { return _SocialHistory; }
            set { _SocialHistory = value; }
        }

        private string _SmokingStatus;

        public string SmokingStatus
        {
            get { return _SmokingStatus; }
            set { _SmokingStatus = value; }
        }

        private string _AlcoholUse;

        public string AlcoholUse
        {
            get { return _AlcoholUse; }
            set { _AlcoholUse = value; }
        }

        private string _DrugUse;

        public string DrugUse
        {
            get { return _DrugUse; }
            set { _DrugUse = value; }
        }

        private string _Employement;

        public string Employement
        {
            get { return _Employement; }
            set { _Employement = value; }
        }

        private string _ChiefComplaint;

        public string ChiefComplaint
        {
            get { return _ChiefComplaint; }
            set { _ChiefComplaint = value; }
        }

        private string _ReasonForVisit;

        public string ReasonForVisit
        {
            get { return _ReasonForVisit; }
            set { _ReasonForVisit = value; }
        }

        private string _ROS;

        public string ROS
        {
            get { return _ROS; }
            set { _ROS = value; }
        }

        private string _PainDescription;

        public string PainDescription
        {
            get { return _PainDescription; }
            set { _PainDescription = value; }
        }

        private string _PainLevel;

        public string PainLevel
        {
            get { return _PainLevel; }
            set { _PainLevel = value; }
        }

        private string _Location;

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        private string _Quality;

        public string Quality
        {
            get { return _Quality; }
            set { _Quality = value; }
        }

        private string _Severity;

        public string Severity
        {
            get { return _Severity; }
            set { _Severity = value; }
        }

        private string _ModifyingFactors;

        public string ModifyingFactors
        {
            get { return _ModifyingFactors; }
            set { _ModifyingFactors = value; }
        }

        private string _AssociatedSymptoms;

        public string AssociatedSymptoms
        {
            get { return _AssociatedSymptoms; }
            set { _AssociatedSymptoms = value; }
        }

        private string _PainDescriptionTwo;

        public string PainDescriptionTwo
        {
            get { return _PainDescriptionTwo; }
            set { _PainDescriptionTwo = value; }
        }

        private string _PainLevelTwo;

        public string PainLevelTwo
        {
            get { return _PainLevelTwo; }
            set { _PainLevelTwo = value; }
        }

        private string _LocationTwo;

        public string LocationTwo
        {
            get { return _LocationTwo; }
            set { _LocationTwo = value; }
        }

        private string _QualityTwo;

        public string QualityTwo
        {
            get { return _QualityTwo; }
            set { _QualityTwo = value; }
        }

        private string _SeverityTwo;

        public string SeverityTwo
        {
            get { return _SeverityTwo; }
            set { _SeverityTwo = value; }
        }

        private string _ModifyingFactorsTwo;

        public string ModifyingFactorsTwo
        {
            get { return _ModifyingFactorsTwo; }
            set { _ModifyingFactorsTwo = value; }
        }

        private string _AssociatedSymptomsTwo;

        public string AssociatedSymptomsTwo
        {
            get { return _AssociatedSymptomsTwo; }
            set { _AssociatedSymptomsTwo = value; }
        }

        private string _ModifyingFactorsExam;

        public string ModifyingFactorsExam
        {
            get { return _ModifyingFactorsExam; }
            set { _ModifyingFactorsExam = value; }
        }

        private string _AssociatedSymptomsExam;

        public string AssociatedSymptomsExam
        {
            get { return _AssociatedSymptomsExam; }
            set { _AssociatedSymptomsExam = value; }
        }

        private string _Neck;

        public string Neck
        {
            get { return _Neck; }
            set { _Neck = value; }
        }

        private string _Back;

        public string Back
        {
            get { return _Back; }
            set { _Back = value; }
        }

        private string _Assesment;

        public string Assesment
        {
            get { return _Assesment; }
            set { _Assesment = value; }
        }

        private string _DiagnosisCode;

        public string DiagnosisCode
        {
            get { return _DiagnosisCode; }
            set { _DiagnosisCode = value; }
        }

        private string _Plain;

        public string Plain
        {
            get { return _Plain; }
            set { _Plain = value; }
        }

        private string _FlexionNormalOne;

        public string FlexionNormalOne
        {
            get { return _FlexionNormalOne; }
            set { _FlexionNormalOne = value; }
        }

        private string _FlexionObservedOne;

        public string FlexionObservedOne
        {
            get { return _FlexionObservedOne; }
            set { _FlexionObservedOne = value; }
        }

        private string _FlexionNormalTwo;

        public string FlexionNormalTwo
        {
            get { return _FlexionNormalTwo; }
            set { _FlexionNormalTwo = value; }
        }

        private string _FlexionObservedTwo;

        public string FlexionObservedTwo
        {
            get { return _FlexionObservedTwo; }
            set { _FlexionObservedTwo = value; }
        }

        private string _ExtensionObservedOne;

        public string ExtensionObservedOne
        {
            get { return _ExtensionObservedOne; }
            set { _ExtensionObservedOne = value; }
        }

        private string _ExtensionNormalOne;

        public string ExtensionNormalOne
        {
            get { return _ExtensionNormalOne; }
            set { _ExtensionNormalOne = value; }
        }

        private string _ExtensionNormalTwo;

        public string ExtensionNormalTwo
        {
            get { return _ExtensionNormalTwo; }
            set { _ExtensionNormalTwo = value; }
        }

        private string _ExtensionObservedTwo;

        public string ExtensionObservedTwo
        {
            get { return _ExtensionObservedTwo; }
            set { _ExtensionObservedTwo = value; }
        }

        private string _RotationNormalOne;

        public string RotationNormalOne
        {
            get { return _RotationNormalOne; }
            set { _RotationNormalOne = value; }
        }

        private string _RotationObservedOne;

        public string RotationObservedOne
        {
            get { return _RotationObservedOne; }
            set { _RotationObservedOne = value; }
        }

        private string _RotationNormalTwo;

        public string RotationNormalTwo
        {
            get { return _RotationNormalTwo; }
            set { _RotationNormalTwo = value; }
        }

        private string _RotationObservedTwo;

        public string RotationObservedTwo
        {
            get { return _RotationObservedTwo; }
            set { _RotationObservedTwo = value; }
        }

        private string _SZ_PATIENT_SIGN_PATH;
        public string SZ_PATIENT_SIGN_PATH
        {
            get { return _SZ_PATIENT_SIGN_PATH; }
            set { _SZ_PATIENT_SIGN_PATH = value; }
        }

        private string _SZ_DOCTOR_SIGN_PATH;
        public string SZ_DOCTOR_SIGN_PATH
        {
            get { return _SZ_DOCTOR_SIGN_PATH; }
            set { _SZ_DOCTOR_SIGN_PATH = value; }
        }

        private string _bt_pat_sign_success;
        public string bt_pat_sign_success
        {
            get { return _bt_pat_sign_success; }
            set { _bt_pat_sign_success = value; }
        }

        private string _bt_doc_sign_success;
        public string bt_doc_sign_success
        {
            get { return _bt_doc_sign_success; }
            set { _bt_doc_sign_success = value; }
        }
    }

    public class PMNotes_EVENTID_DAO
    {
        string SZ_BILL_NUMBER1;
        string I_EVENT_ID1;
        ArrayList ar1;

        public string SZ_BILL_NUMBER
        {
            get { return this.SZ_BILL_NUMBER1; }
            set { this.SZ_BILL_NUMBER1 = value; }
        }

        public string I_EVENT_ID
        {
            get { return this.I_EVENT_ID1; }
            set { this.I_EVENT_ID1 = value; }
        }

        public ArrayList ar
        {
            get { return this.ar1; }
            set { this.ar1 = value; }
        }
    }

    public class PMNotes_ProcCode_DAO
    {
        string code1;
        string SZ_TYPE_CODE_ID1;
        string SZ_PROC_CODE1;
        ArrayList ar1;
        ArrayList arpc1;

        public string code
        {
            get { return this.code1; }
            set { this.code1 = value; }
        }

        public string SZ_TYPE_CODE_ID
        {
            get { return this.SZ_TYPE_CODE_ID1; }
            set { this.SZ_TYPE_CODE_ID1 = value; }
        }

        public string SZ_PROC_CODE
        {
            get { return this.SZ_PROC_CODE1; }
            set { this.SZ_PROC_CODE1 = value; }
        }

        public ArrayList ar
        {
            get { return this.ar1; }
            set { this.ar1 = value; }
        }

        public ArrayList arpc
        {
            get { return this.arpc1; }
            set { this.arpc1 = value; }
        }

    }
}
