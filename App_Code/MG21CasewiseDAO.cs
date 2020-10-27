using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for MG21CasewiseDAO
/// </summary>
public class MG21CasewiseDAO
{
        private int _I_ID;
        public int I_ID
        {
            get { return _I_ID; }
            set { _I_ID = value; }
        }
        private string _I_EVENT_ID;
        public string I_EVENT_ID
        {
            get { return _I_EVENT_ID; }
            set { _I_EVENT_ID = value; }
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
        private string _sz_modified_by;
        public string sz_modified_by
        {
            get { return _sz_modified_by; }
            set { _sz_modified_by = value; }
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
        private string _sz_doctor_Name;
        public string sz_doctor_Name
        {
            get { return _sz_doctor_Name; }
            set { _sz_doctor_Name = value; }
        }
        private string _sz_doctor_id;
        public string sz_doctor_id
        {
            get { return _sz_doctor_id; }
            set { _sz_doctor_id = value; }
        }
        string sPatientAddress;
        public string patientAddress
        {
            get { return this.sPatientAddress; }
            set { this.sPatientAddress = value; }
        }
        string sSocialSecurityNumber;
        public string socialSecurityNumber
        {
            get { return this.sSocialSecurityNumber; }
            set { this.sSocialSecurityNumber = value; }
        }
        string sInsuranceNameAddress;
        public string insuranceNameAddress
        {
            get { return this.sInsuranceNameAddress; }
            set { this.sInsuranceNameAddress = value; }
        }
        string _sz_doctorWCBAuth_number;
        public string sz_doctorWCBAuth_number
        {
            get { return _sz_doctorWCBAuth_number; }
            set { _sz_doctorWCBAuth_number = value; }
        }

        string _sz_guidelines_reference2;
        public string sz_guidelines_reference2
        {
            get { return _sz_guidelines_reference2; }
            set { _sz_guidelines_reference2 = value; }
        }
        string _sz_guidelines_reference22;
        public string sz_guidelines_reference22
        {
            get { return _sz_guidelines_reference22; }
            set { _sz_guidelines_reference22 = value; }
        }
        string _sz_guidelines_reference23;
        public string sz_guidelines_reference23
        {
            get { return _sz_guidelines_reference23; }
            set { _sz_guidelines_reference23 = value; }
        }
        string _sz_guidelines_reference24;
        public string sz_guidelines_reference24
        {
            get { return _sz_guidelines_reference24; }
            set { _sz_guidelines_reference24 = value; }
        }
        string _sz_guidelines_reference25;
        public string sz_guidelines_reference25
        {
            get { return _sz_guidelines_reference25; }
            set { _sz_guidelines_reference25 = value; }
        }
        string _dt_DateOfService2;
        public string dt_DateOfService2
        {
            get { return _dt_DateOfService2; }
            set { _dt_DateOfService2 = value; }
        }
        string _dt_DateOfPrevious2;
        public string dt_DateOfPrevious2
        {
            get { return _dt_DateOfPrevious2; }
            set { _dt_DateOfPrevious2 = value; }
        }
        string sApprovalRequest2;
        public string approvalRequest2
        {
            get { return this.sApprovalRequest2; }
            set { this.sApprovalRequest2 = value; }
        }
        string _sz_MedicalNecessity2;
        public string sz_MedicalNecessity2
        {
            get { return _sz_MedicalNecessity2; }
            set { _sz_MedicalNecessity2 = value; }
        }

        string _sz_guidelines_reference3;
        public string sz_guidelines_reference3
        {
            get { return _sz_guidelines_reference3; }
            set { _sz_guidelines_reference3 = value; }
        }
        string _sz_guidelines_reference32;
        public string sz_guidelines_reference32
        {
            get { return _sz_guidelines_reference32; }
            set { _sz_guidelines_reference32 = value; }
        }
        string _sz_guidelines_reference33;
        public string sz_guidelines_reference33
        {
            get { return _sz_guidelines_reference33; }
            set { _sz_guidelines_reference33 = value; }
        }
        string _sz_guidelines_reference34;
        public string sz_guidelines_reference34
        {
            get { return _sz_guidelines_reference34; }
            set { _sz_guidelines_reference34 = value; }
        }
        string _sz_guidelines_reference35;
        public string sz_guidelines_reference35
        {
            get { return _sz_guidelines_reference35; }
            set { _sz_guidelines_reference35 = value; }
        }
        string _dt_DateOfService3;
        public string dt_DateOfService3
        {
            get { return _dt_DateOfService3; }
            set { _dt_DateOfService3 = value; }
        }
        string _dt_DateOfPrevious3;
        public string dt_DateOfPrevious3
        {
            get { return _dt_DateOfPrevious3; }
            set { _dt_DateOfPrevious3 = value; }
        }
        string sApprovalRequest3;
        public string approvalRequest3
        {
            get { return this.sApprovalRequest3; }
            set { this.sApprovalRequest3 = value; }
        }
        string _sz_MedicalNecessity3;
        public string sz_MedicalNecessity3
        {
            get { return _sz_MedicalNecessity3; }
            set { _sz_MedicalNecessity3 = value; }
        }

        string _sz_guidelines_reference4;
        public string sz_guidelines_reference4
        {
            get { return _sz_guidelines_reference4; }
            set { _sz_guidelines_reference4 = value; }
        }
        string _sz_guidelines_reference42;
        public string sz_guidelines_reference42
        {
            get { return _sz_guidelines_reference42; }
            set { _sz_guidelines_reference42 = value; }
        }
        string _sz_guidelines_reference43;
        public string sz_guidelines_reference43
        {
            get { return _sz_guidelines_reference43; }
            set { _sz_guidelines_reference43 = value; }
        }
        string _sz_guidelines_reference44;
        public string sz_guidelines_reference44
        {
            get { return _sz_guidelines_reference44; }
            set { _sz_guidelines_reference44 = value; }
        }
        string _sz_guidelines_reference45;
        public string sz_guidelines_reference45
        {
            get { return _sz_guidelines_reference45; }
            set { _sz_guidelines_reference45 = value; }
        }
        string _dt_DateOfService4;
        public string dt_DateOfService4
        {
            get { return _dt_DateOfService4; }
            set { _dt_DateOfService4 = value; }
        }
        string _dt_DateOfPrevious4;
        public string dt_DateOfPrevious4
        {
            get { return _dt_DateOfPrevious4; }
            set { _dt_DateOfPrevious4 = value; }
        }
        string sApprovalRequest4;
        public string approvalRequest4
        {
            get { return this.sApprovalRequest4; }
            set { this.sApprovalRequest4 = value; }
        }
        string _sz_MedicalNecessity4;
        public string sz_MedicalNecessity4
        {
            get { return _sz_MedicalNecessity4; }
            set { _sz_MedicalNecessity4 = value; }
        }

        string _sz_guidelines_reference5;
        public string sz_guidelines_reference5
        {
            get { return _sz_guidelines_reference5; }
            set { _sz_guidelines_reference5 = value; }
        }
        string _sz_guidelines_reference52;
        public string sz_guidelines_reference52
        {
            get { return _sz_guidelines_reference52; }
            set { _sz_guidelines_reference52 = value; }
        }
        string _sz_guidelines_reference53;
        public string sz_guidelines_reference53
        {
            get { return _sz_guidelines_reference53; }
            set { _sz_guidelines_reference53 = value; }
        }
        string _sz_guidelines_reference54;
        public string sz_guidelines_reference54
        {
            get { return _sz_guidelines_reference54; }
            set { _sz_guidelines_reference54 = value; }
        }
        string _sz_guidelines_reference55;
        public string sz_guidelines_reference55
        {
            get { return _sz_guidelines_reference55; }
            set { _sz_guidelines_reference55= value; }
        }
        string _dt_DateOfService5;
        public string dt_DateOfService5
        {
            get { return _dt_DateOfService5; }
            set { _dt_DateOfService5 = value; }
        }
        string _dt_DateOfPrevious5;
        public string dt_DateOfPrevious5
        {
            get { return _dt_DateOfPrevious5; }
            set { _dt_DateOfPrevious5 = value; }
        }
        string sApprovalRequest5;
        public string approvalRequest5
        {
            get { return this.sApprovalRequest5; }
            set { this.sApprovalRequest5 = value; }
        }
        string _sz_MedicalNecessity5;
        public string sz_MedicalNecessity5
        {
            get { return _sz_MedicalNecessity5; }
            set { _sz_MedicalNecessity5 = value; }
        }

        string sWCBCaseNumber2;
        public string WCBCaseNumber2
        {
            get { return this.sWCBCaseNumber2; }
            set { this.sWCBCaseNumber2 = value; }
        }
        string sDateOfInjury2;
        public string dateOfInjury2
        {
            get { return this.sDateOfInjury2; }
            set { this.sDateOfInjury2 = value; }
        }

        private int _bt_did;
        public int bt_did
        {
            get { return _bt_did; }
            set { _bt_did = value; }
        }
        private int _bt_not_did;
        public int bt_not_did
        {
            get { return _bt_not_did; }
            set { _bt_not_did = value; }
        }
        string _dt_Tele_Date;
        public string dt_Tele_Date
        {
            get { return _dt_Tele_Date; }
            set { _dt_Tele_Date = value; }
        }
        private string _sz_spoke_anyone;
        public string sz_spoke_anyone
        {
            get { return _sz_spoke_anyone; }
            set { _sz_spoke_anyone = value; }
        }
        private int _bt_a_copy;
        public int bt_a_copy
        {
            get { return _bt_a_copy; }
            set { _bt_a_copy = value; }
        }
        private string _sz_Fax;
        public string sz_Fax
        {
            get { return _sz_Fax; }
            set { _sz_Fax = value; }
        }
        string sProviderSignDate;
        public string providerSignDate
        {
            get { return this.sProviderSignDate; }
            set { this.sProviderSignDate = value; }
        }
        private int _bt_granted2;
        public int bt_granted2
        {
            get { return _bt_granted2; }
            set { _bt_granted2 = value; }
        }
        private int _bt_granted_in_part2;
        public int bt_granted_in_part2
        {
            get { return _bt_granted_in_part2; }
            set { _bt_granted_in_part2 = value; }
        }
        private int _bt_denied2;
        public int bt_denied2
        {
            get { return _bt_denied2; }
            set { _bt_denied2 = value; }
        }
        private int _bt_burden2;
        public int bt_burden2
        {
            get { return _bt_burden2; }
            set { _bt_burden2 = value; }
        }
        private int _bt_substantialy2;
        public int bt_substantialy2
        {
            get { return _bt_substantialy2; }
            set { _bt_substantialy2 = value; }
        }
        private int _bt_without_prejudice2;
        public int bt_without_prejudice2
        {
            get { return _bt_without_prejudice2; }
            set { _bt_without_prejudice2 = value; }
        }


        private int _bt_granted3;
        public int bt_granted3
        {
            get { return _bt_granted3; }
            set { _bt_granted3 = value; }
        }
        private int _bt_granted_in_part3;
        public int bt_granted_in_part3
        {
            get { return _bt_granted_in_part3; }
            set { _bt_granted_in_part3 = value; }
        }
        private int _bt_denied3;
        public int bt_denied3
        {
            get { return _bt_denied3; }
            set { _bt_denied3 = value; }
        }
        private int _bt_burden3;
        public int bt_burden3
        {
            get { return _bt_burden3; }
            set { _bt_burden3 = value; }
        }
        private int _bt_substantialy3;
        public int bt_substantialy3
        {
            get { return _bt_substantialy3; }
            set { _bt_substantialy3 = value; }
        }
        private int _bt_without_prejudice3;
        public int bt_without_prejudice3
        {
            get { return _bt_without_prejudice3; }
            set { _bt_without_prejudice3 = value; }
        }


        private int _bt_granted4;
        public int bt_granted4
        {
            get { return _bt_granted4; }
            set { _bt_granted4 = value; }
        }
        private int _bt_granted_in_part4;
        public int bt_granted_in_part4
        {
            get { return _bt_granted_in_part4; }
            set { _bt_granted_in_part4 = value; }
        }
        private int _bt_denied4;
        public int bt_denied4
        {
            get { return _bt_denied4; }
            set { _bt_denied4 = value; }
        }
        private int _bt_burden4;
        public int bt_burden4
        {
            get { return _bt_burden4; }
            set { _bt_burden4 = value; }
        }
        private int _bt_substantialy4;
        public int bt_substantialy4
        {
            get { return _bt_substantialy4; }
            set { _bt_substantialy4 = value; }
        }
        private int _bt_without_prejudice4;
        public int bt_without_prejudice4
        {
            get { return _bt_without_prejudice4; }
            set { _bt_without_prejudice4 = value; }
        }


        private int _bt_granted5;
        public int bt_granted5
        {
            get { return _bt_granted5; }
            set { _bt_granted5 = value; }
        }
        private int _bt_granted_in_part5;
        public int bt_granted_in_part5
        {
            get { return _bt_granted_in_part5; }
            set { _bt_granted_in_part5 = value; }
        }
        private int _bt_denied5;
        public int bt_denied5
        {
            get { return _bt_denied5; }
            set { _bt_denied5 = value; }
        }
        private int _bt_burden5;
        public int bt_burden5
        {
            get { return _bt_burden5; }
            set { _bt_burden5 = value; }
        }
        private int _bt_substantialy5;
        public int bt_substantialy5
        {
            get { return _bt_substantialy5; }
            set { _bt_substantialy5 = value; }
        }
        private int _bt_without_prejudice5;
        public int bt_without_prejudice5
        {
            get { return _bt_without_prejudice5; }
            set { _bt_without_prejudice5 = value; }
        }


        private string _sz_Carrier;
        public string sz_Carrier
        {
            get { return _sz_Carrier; }
            set { _sz_Carrier = value; }
        }
        private string _sz_NameOfMedProfessional;
        public string sz_NameOfMedProfessional
        {
            get { return _sz_NameOfMedProfessional; }
            set { _sz_NameOfMedProfessional = value; }
        }
        private int _bt_byMedArb;
        public int bt_byMedArb
        {
            get { return _bt_byMedArb; }
            set { _bt_byMedArb = value; }
        }
        private int _bt_byChair;
        public int bt_byChair
        {
            get { return _bt_byChair; }
            set { _bt_byChair = value; }
        }
        private string _sz_print_name_D;
        public string sz_print_name_D
        {
            get { return _sz_print_name_D; }
            set { _sz_print_name_D = value; }
        }
        private string _sz_title_D;
        public string sz_title_D
        {
            get { return _sz_title_D; }
            set { _sz_title_D = value; }
        }
        private string _dt_date_D;
        public string dt_date_D
        {
            get { return _dt_date_D; }
            set { _dt_date_D = value; }
        }
        private int _bt_IRequest;
        public int bt_IRequest
        {
            get { return _bt_IRequest; }
            set { _bt_IRequest = value; }
        }
        private int _bt_IRequest1;
        public int bt_IRequest1
        {
            get { return _bt_IRequest1; }
            set { _bt_IRequest1 = value; }
        }
        private int _bt_IRequest2;
        public int bt_IRequest2
        {
            get { return _bt_IRequest2; }
            set { _bt_IRequest2 = value; }
        }
        private int _bt_IRequest3;
        public int bt_IRequest3
        {
            get { return _bt_IRequest3; }
            set { _bt_IRequest3 = value; }
        }
        private int _bt_IRequest4;
        public int bt_IRequest4
        {
            get { return _bt_IRequest4; }
            set { _bt_IRequest4 = value; }
        }
        private int _bt_IRequest5;
        public int bt_IRequest5
        {
            get { return _bt_IRequest5; }
            set { _bt_IRequest5 = value; }
        }
        private int _bt_byMedArb2;
        public int bt_byMedArb2
        {
            get { return _bt_byMedArb2; }
            set { _bt_byMedArb2 = value; }
        }
        private int _bt_Atwcb;
        public int bt_Atwcb
        {
            get { return _bt_Atwcb; }
            set { _bt_Atwcb = value; }
        }
        private string sClaimantSignDate;
        public string claimantSignDate
        {
            get { return this.sClaimantSignDate; }
            set { this.sClaimantSignDate = value; }
        }
        private string _sz_BillNo;
        public string sz_BillNo
        {
            get { return _sz_BillNo; }
            set { _sz_BillNo = value; }
        }
        private string _sz_PatientID;
        public string PatientID
        {
            get { return _sz_PatientID; }
            set { _sz_PatientID = value; }
        }

        private string _sz_pdf_url;
        public string sz_pdf_url
        {
            get { return _sz_pdf_url; }
            set { _sz_pdf_url = value; }
        }

        private string _sz_procedure_group_id;
        public string sz_procedure_group_id
        {
            get { return _sz_procedure_group_id; }
            set { _sz_procedure_group_id = value; }
        }

        private int _bt_DenIRequest2;
        public int bt_DenIRequest2
        {
            get { return _bt_DenIRequest2; }
            set { _bt_DenIRequest2 = value; }
        }
        private int _bt_DenIRequest3;
        public int bt_DenIRequest3
        {
            get { return _bt_DenIRequest3; }
            set { _bt_DenIRequest3 = value; }
        }
        private int _bt_DenIRequest4;
        public int bt_DenIRequest4
        {
            get { return _bt_DenIRequest4; }
            set { _bt_DenIRequest4 = value; }
        }
        private int _bt_DenIRequest5;
        public int bt_DenIRequest5
        {
            get { return _bt_DenIRequest5; }
            set { _bt_DenIRequest5 = value; }
        }

        private string _sz_print_name_Den;
        public string sz_print_name_Den
        {
            get { return _sz_print_name_Den; }
            set { _sz_print_name_Den = value; }
        }

        private string _sz_title_Den;
        public string sz_title_Den
        {
            get { return _sz_title_Den; }
            set { _sz_title_Den = value; }
        }
        private string _dt_date_Den;
        public string dt_date_Den
        {
            get { return _dt_date_Den; }
            set { _dt_date_Den = value; }
        }

        private int _bt_employer;
        public int bt_employer
        {
            get { return _bt_employer; }
            set { _bt_employer = value; }
        }
        private int _bt_CarrierReq2;
        public int bt_CarrierReq2
        {
            get { return _bt_CarrierReq2; }
            set { _bt_CarrierReq2 = value; }
        }
        private int _bt_CarrierReq3;
        public int bt_CarrierReq3
        {
            get { return _bt_CarrierReq3; }
            set { _bt_CarrierReq3 = value; }
        }
        private int _bt_CarrierReq4;
        public int bt_CarrierReq4
        {
            get { return _bt_CarrierReq4; }
            set { _bt_CarrierReq4 = value; }
        }
        private int _bt_CarrierReq5;
        public int bt_CarrierReq5
        {
            get { return _bt_CarrierReq5; }
            set { _bt_CarrierReq5 = value; }
        }
        private string _sz_CarrierPrintName;
        public string sz_CarrierPrintName 
        {
            get { return _sz_CarrierPrintName; }
            set { _sz_CarrierPrintName = value; }
        }
        private string _sz_CarrierPrintTitle;
        public string sz_CarrierPrintTitle
        {
            get { return _sz_CarrierPrintTitle; }
            set { _sz_CarrierPrintTitle = value; }
        }
        private string _sz_CarrierPrintDate;
        public string sz_CarrierPrintDate
        {
            get { return _sz_CarrierPrintDate; }
            set { _sz_CarrierPrintDate = value; }
        }

        private string _sz_DateOfService2;
        public string sz_DateOfService2
        {
            get { return _sz_DateOfService2; }
            set { _sz_DateOfService2 = value; }
        }

        private string _sz_DateOfPrevious2;
        public string sz_DateOfPrevious2
        {
            get { return _sz_DateOfPrevious2; }
            set { _sz_DateOfPrevious2 = value; }
        }

        private string _sz_DateOfService3;
        public string sz_DateOfService3
        {
            get { return _sz_DateOfService3; }
            set { _sz_DateOfService3 = value; }
        }

        private string _sz_DateOfPrevious3;
        public string sz_DateOfPrevious3
        {
            get { return _sz_DateOfPrevious3; }
            set { _sz_DateOfPrevious3 = value; }
        }

        private string _sz_DateOfService4;
        public string sz_DateOfService4
        {
            get { return _sz_DateOfService4; }
            set { _sz_DateOfService4 = value; }
        }

        private string _sz_DateOfPrevious4;
        public string sz_DateOfPrevious4
        {
            get { return _sz_DateOfPrevious4; }
            set { _sz_DateOfPrevious4 = value; }
        }

        private string _sz_DateOfService5;
        public string sz_DateOfService5
        {
            get { return _sz_DateOfService5; }
            set { _sz_DateOfService5 = value; }
        }

        private string _sz_DateOfPrevious5;
        public string sz_DateOfPrevious5
        {
            get { return _sz_DateOfPrevious5; }
            set { _sz_DateOfPrevious5 = value; }
        }

}

