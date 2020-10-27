using System;
using System.Collections.Generic;

using System.Web;

/// <summary>
/// Summary description for AddMG2Casewise
/// </summary>
/// 

namespace MG2PDF.DataAccessObject
{
    public class AddMG2Casewise
    {
        public AddMG2Casewise()
        { 
            //
            // TODO: Add constructor logic here
            //
        }
        private string _sz_PatientID;
        public string PatientID
        {
            get { return _sz_PatientID; }
            set { _sz_PatientID = value; }
        }

        private string _sz_BillNo;
        public string sz_BillNo
        {
            get { return _sz_BillNo; }
            set { _sz_BillNo = value; }
        }

        private string _sz_DoctorID;
        public string sz_DoctorID
        {
            get { return _sz_DoctorID; }
            set { _sz_DoctorID = value; }
        }

        private string _sz_CompanyID;
        public string sz_CompanyID
        {
            get { return _sz_CompanyID; }
            set { _sz_CompanyID = value; }
        }

        private string _sz_CaseID;
        public string sz_CaseID
        {
            get { return _sz_CaseID; }
            set { _sz_CaseID = value; }
        }

        private string _sz_UserID;
        public string sz_UserID
        {
            get { return _sz_UserID; }
            set { _sz_UserID = value; }
        }

        string sWCBCaseNumber;
        public string WCBCaseNumber
        {
            get { return this.sWCBCaseNumber; }
            set { this.sWCBCaseNumber = value; }
        }

        string sCarrierCaseNumber;
        public string carrierCaseNumber
        {
            get { return this.sCarrierCaseNumber; }
            set { this.sCarrierCaseNumber = value; }
        }


        string sDateOfInjury;
        public string dateOfInjury
        {
            get { return this.sDateOfInjury; }
            set { this.sDateOfInjury = value; }
        }

        string sFirstName;
        public string firstName
        {
            get { return this.sFirstName; }
            set { this.sFirstName = value; }
        }

        string sMiddleName;
        public string middleName
        {
            get { return this.sMiddleName; }
            set { this.sMiddleName = value; }
        }

        string sLastName;
        public string lastName
        {
            get { return this.sLastName; }
            set { this.sLastName = value; }
        }

        string sSocialSecurityNumber;
        public string socialSecurityNumber
        {
            get { return this.sSocialSecurityNumber; }
            set { this.sSocialSecurityNumber = value; }
        }

        string sPatientAddress;
        public string patientAddress
        {
            get { return this.sPatientAddress; }
            set { this.sPatientAddress = value; }
        }

        string sEmployerNameAddress;
        public string employerNameAddress
        {
            get { return this.sEmployerNameAddress; }
            set { this.sEmployerNameAddress = value; }
        }

        string sInsuranceNameAddress;
        public string insuranceNameAddress
        {
            get { return this.sInsuranceNameAddress; }
            set { this.sInsuranceNameAddress = value; }
        }

        string sAttendingDoctorNameAddress;
        public string attendingDoctorNameAddress
        {
            get { return this.sAttendingDoctorNameAddress; }
            set { this.sAttendingDoctorNameAddress = value; }
        }

        string sProviderWCBNumber;
        public string providerWCBNumber
        {
            get { return this.sProviderWCBNumber; }
            set { this.sProviderWCBNumber = value; }
        }

        string sDoctorPhone;
        public string doctorPhone
        {
            get { return this.sDoctorPhone; }
            set { this.sDoctorPhone = value; }
        }

        string sDoctorFax;
        public string doctorFax
        {
            get { return this.sDoctorFax; }
            set { this.sDoctorFax = value; }
        }

        string sBodyInitial;
        public string bodyInitial
        {
            get { return this.sBodyInitial; }
            set { this.sBodyInitial = value; }
        }

        string sGuidelineSection;
        public string guidelineSection
        {
            get { return this.sGuidelineSection; }
            set { this.sGuidelineSection = value; }
        }

        string sApprovalRequest;
        public string approvalRequest
        {
            get { return this.sApprovalRequest; }
            set { this.sApprovalRequest = value; }
        }

        string sDateOfService;
        public string dateOfService
        {
            get { return this.sDateOfService; }
            set { this.sDateOfService = value; }
        }

        string sDatesOfDeniedRequest;
        public string datesOfDeniedRequest
        {
            get { return this.sDatesOfDeniedRequest; }
            set { this.sDatesOfDeniedRequest = value; }
        }

        string sChkDid;
        public string chkDid
        {
            get { return this.sChkDid; }
            set { this.sChkDid = value; }
        }

        string sChkDidNot;
        public string chkDidNot
        {
            get { return this.sChkDidNot; }
            set { this.sChkDidNot = value; }
        }

        string sContactDate;
        public string contactDate
        {
            get { return this.sContactDate; }
            set { this.sContactDate = value; }
        }

        string sPersonContacted;
        public string personContacted
        {
            get { return this.sPersonContacted; }
            set { this.sPersonContacted = value; }
        }

        string sChkCopySent;
        public string chkCopySent
        {
            get { return this.sChkCopySent; }
            set { this.sChkCopySent = value; }
        }

        string sFaxEmail;
        public string faxEmail
        {
            get { return this.sFaxEmail; }
            set { this.sFaxEmail = value; }
        }

        string sChkCopyNotSent;
        public string chkCopyNotSent
        {
            get { return this.sChkCopyNotSent; }
            set { this.sChkCopyNotSent = value; }
        }

        string sIndicatedFaxEmail;
        public string indicatedFaxEmail
        {
            get { return this.sIndicatedFaxEmail; }
            set { this.sIndicatedFaxEmail = value; }
        }

        string sProviderSign;
        public string providerSign
        {
            get { return this.sProviderSign; }
            set { this.sProviderSign = value; }
        }

        string sProviderSignDate;
        public string providerSignDate
        {
            get { return this.sProviderSignDate; }
            set { this.sProviderSignDate = value; }
        }

        string sPatientName;
        public string patientName
        {
            get { return this.sPatientName; }
            set { this.sPatientName = value; }
        }

        string sChkNoticeGiven;
        public string chkNoticeGiven
        {
            get { return this.sChkNoticeGiven; }
            set { this.sChkNoticeGiven = value; }
        }

        string sPrintCarrierEmployerNoticeName;
        public string printCarrierEmployerNoticeName
        {
            get { return this.sPrintCarrierEmployerNoticeName; }
            set { this.sPrintCarrierEmployerNoticeName = value; }
        }

        string sNoticeTitle;
        public string noticeTitle
        {
            get { return this.sNoticeTitle; }
            set { this.sNoticeTitle = value; }
        }

        string sNoticeCarrierSign;
        public string noticeCarrierSign
        {
            get { return this.sNoticeCarrierSign; }
            set { this.sNoticeCarrierSign = value; }
        }

        string sNoticeCarrierSignDate;
        public string noticeCarrierSignDate
        {
            get { return this.sNoticeCarrierSignDate; }
            set { this.sNoticeCarrierSignDate = value; }
        }

        string sChkGranted;
        public string chkGranted
        {
            get { return this.sChkGranted; }
            set { this.sChkGranted = value; }
        }

        string sChkGrantedInParts;
        public string chkGrantedInParts
        {
            get { return this.sChkGrantedInParts; }
            set { this.sChkGrantedInParts = value; }
        }

        string sChkWithoutPrejudice;
        public string chkWithoutPrejudice
        {
            get { return this.sChkWithoutPrejudice; }
            set { this.sChkWithoutPrejudice = value; }
        }

        string sChkDenied;
        public string chkDenied
        {
            get { return this.sChkDenied; }
            set { this.sChkDenied = value; }
        }

        string sChkBurden;
        public string chkBurden
        {
            get { return this.sChkBurden; }
            set { this.sChkBurden = value; }
        }

        string sChkSubstantiallySimilar;
        public string chkSubstantiallySimilar
        {
            get { return this.sChkSubstantiallySimilar; }
            set { this.sChkSubstantiallySimilar = value; }
        }

        string sCarrierDenial;
        public string carrierDenial
        {
            get { return this.sCarrierDenial; }
            set { this.sCarrierDenial = value; }
        }

        string sMedicalProfessional;
        public string medicalProfessional
        {
            get { return this.sMedicalProfessional; }
            set { this.sMedicalProfessional = value; }
        }

        string sChkMedicalArbitrator;
        public string chkMedicalArbitrator
        {
            get { return this.sChkMedicalArbitrator; }
            set { this.sChkMedicalArbitrator = value; }
        }

        string sChkWCBHearing;
        public string chkWCBHearing
        {
            get { return this.sChkWCBHearing; }
            set { this.sChkWCBHearing = value; }
        }

        string sPrintCarrierEmployerResponseName;
        public string printCarrierEmployerResponseName
        {
            get { return this.sPrintCarrierEmployerResponseName; }
            set { this.sPrintCarrierEmployerResponseName = value; }
        }

        string sResponseTitle;
        public string responseTitle
        {
            get { return this.sResponseTitle; }
            set { this.sResponseTitle = value; }
        }

        string sResponseCarrierSign;
        public string responseCarrierSign
        {
            get { return this.sResponseCarrierSign; }
            set { this.sResponseCarrierSign = value; }
        }

        string sResponseCarrierSignDate;
        public string responseCarrierSignDate
        {
            get { return this.sResponseCarrierSignDate; }
            set { this.sResponseCarrierSignDate = value; }
        }

        string sPrintDenialCarrierName;
        public string printDenialCarrierName
        {
            get { return this.sPrintDenialCarrierName; }
            set { this.sPrintDenialCarrierName = value; }
        }

        string sDenialTitle;
        public string denialTitle
        {
            get { return this.sDenialTitle; }
            set { this.sDenialTitle = value; }
        }

        string sDenialCarrierSign;
        public string denialCarrierSign
        {
            get { return this.sDenialCarrierSign; }
            set { this.sDenialCarrierSign = value; }
        }

        string sDenialCarrierSignDate;
        public string denialCarrierSignDate
        {
            get { return this.sDenialCarrierSignDate; }
            set { this.sDenialCarrierSignDate = value; }
        }

        string sChkRequestWC;
        public string chkRequestWC
        {
            get { return this.sChkRequestWC; }
            set { this.sChkRequestWC = value; }
        }

        string sChkMedicalArbitratorByWC;
        public string chkMedicalArbitratorByWC
        {
            get { return this.sChkMedicalArbitratorByWC; }
            set { this.sChkMedicalArbitratorByWC = value; }
        }

        string sChkWCBHearingByWC;
        public string chkWCBHearingByWC
        {
            get { return this.sChkWCBHearingByWC; }
            set { this.sChkWCBHearingByWC = value; }
        }

        string sClaimantSign;
        public string claimantSign
        {
            get { return this.sClaimantSign; }
            set { this.sClaimantSign = value; }
        }

        string sClaimantSignDate;
        public string claimantSignDate
        {
            get { return this.sClaimantSignDate; }
            set { this.sClaimantSignDate = value; }
        }

        private int _I_ID;
        public int I_ID
        {
            get { return _I_ID; }
            set { _I_ID = value; }
        }

        private string _sz_procedure_group_id;
        public string sz_procedure_group_id
        {
            get { return _sz_procedure_group_id; }
            set { _sz_procedure_group_id = value; }
        }

    }
}