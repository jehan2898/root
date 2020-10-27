using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for AddMG11CaseWiseDAO
/// </summary>
public class AddMG11CaseWiseDAO
{
	public AddMG11CaseWiseDAO()
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
    public string MiddleName
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

    string sz_DoctorName;
    public string sz_Doctor_Name
    {
        get { return this.sz_DoctorName; }
        set { this.sz_DoctorName = value; }
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

    string sdate_request_submitted;
    public string date_request_submitted
    {
        get { return this.sdate_request_submitted; }
        set { this.sdate_request_submitted = value; }
    }

    string sProcedureRequest;
    public string ProcedureRequest
    {
        get { return this.sProcedureRequest; }
        set { this.sProcedureRequest = value; }
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
    public string MedicalProfessional
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

    private string sMedicalReport;
    public string dt_supporting_medical_on
    {
        get { return sMedicalReport; }
        set { sMedicalReport = value; }
    }

    string sCaseNumber;
    public string CaseNumber
    {
        get { return this.sCaseNumber; }
        set { this.sCaseNumber = value; }
    }

    
    string sDoctorName;
    public string DoctorName
    {
        get { return this.sDoctorName; }
        set { this.sDoctorName = value; }
    }
    
    string sDoctorWCBNumber;
    public string DoctorWCBNumber
    {
        get { return this.sDoctorWCBNumber;}
        set { this.sDoctorWCBNumber = value; }
    }

    string sTreatment_One;
    public string TreatmentOne
    {
        get { return this.sTreatment_One; }
        set { this.sTreatment_One = value; }
    }

    string sGuidelineOne;
    public string GuidelineOne
    {
        get { return this.sGuidelineOne; }
        set { this.sGuidelineOne = value; }
    }

    string sGuidelineBoxOne;
    public string GuidelineBoxOne
    {
        get { return this.sGuidelineBoxOne; }
        set { this.sGuidelineBoxOne = value; }
    }

    string sGuidelineBoxTwo;
    public string GuidelineBoxTwo
    {
        get { return this.sGuidelineBoxTwo; }
        set { this.sGuidelineBoxTwo = value; }
    }

    string sGuidelineBoxThree;
    public string GuidelineBoxThree
    {
        get { return this.sGuidelineBoxThree; }
        set { this.sGuidelineBoxThree = value; }
    }

    string sGuidelineBoxfour;
    public string GuidelineBoxfour
    {
        get { return this.sGuidelineBoxfour; }
        set { this.sGuidelineBoxfour = value; }
    }

    string sDateOfServiceOne;
    public string dateOfServiceOne
    {
        get { return this.sDateOfServiceOne; }
        set { this.sDateOfServiceOne = value; }
    }

    private string sz_Comments_One;
    public string sz_Comments
    {
        get { return sz_Comments_One; }
        set { sz_Comments_One = value; }
    }

    string sCbGranted_One;
    public string cbGranted_One
    {
        get { return this.sCbGranted_One; }
        set { this.sCbGranted_One = value; }
    }

    string sCbGrantedPrejudice_One;
    public string CbGrantedPrejudice_One
    {
        get { return this.sCbGrantedPrejudice_One; }
        set { this.sCbGrantedPrejudice_One = value; }
    }

    string sCbDenied_One;
    public string CbDenied_One
    {
        get { return this.sCbDenied_One; }
        set { this.sCbDenied_One = value; }
    }
    //----------------------------------------------------------------------
    string sTreatment_Two;
    public string TreatmentTwo
    {
        get { return this.sTreatment_Two; }
        set { this.sTreatment_Two = value; }
    }

    string sGuidelineTwo;
    public string GuidelineTwo
    {
        get { return this.sGuidelineTwo; }
        set { this.sGuidelineTwo = value; }
    }

    string sGuidelineBoxFive;
    public string GuidelineFive
    {
        get { return this.sGuidelineBoxFive; }
        set { this.sGuidelineBoxFive = value; }
    }

    string sGuidelineBoxSix;
    public string GuidelineBoxSix
    {
        get { return this.sGuidelineBoxSix; }
        set { this.sGuidelineBoxSix = value; }
    }

    string sGuidelineBoxSeven;
    public string GuidelineBoxSeven
    {
        get { return this.sGuidelineBoxSeven; }
        set { this.sGuidelineBoxSeven = value; }
    }

    string sGuidelineBoxEight;
    public string GuidelineBoxEight
    {
        get { return this.sGuidelineBoxEight; }
        set { this.sGuidelineBoxEight = value; }
    }

    string sDateOfServiceTwo;
    public string dateOfServiceTwo
    {
        get { return this.sDateOfServiceTwo; }
        set { this.sDateOfServiceTwo = value; }
    }

    private string sz_Comments_Two;
    public string sz_Comments_two
    {
        get { return sz_Comments_Two; }
        set { sz_Comments_Two = value; }
    }

    string sCbGranted_Two;
    public string cbGranted_Two
    {
        get { return this.sCbGranted_Two; }
        set { this.sCbGranted_Two = value; }
    }

    string sCbGrantedPrejudice_Two;
    public string CbGrantedPrejudice_Two
    {
        get { return this.sCbGrantedPrejudice_Two; }
        set { this.sCbGrantedPrejudice_Two = value; }
    }

    string sCbDenied_Two;
    public string CbDenied_Two
    {
        get { return this.sCbDenied_Two; }
        set { this.sCbDenied_Two = value; }
    }
    //-----------------------------------------------------------------
    string sTreatment_Three;
    public string TreatmentThree
    {
        get { return this.sTreatment_Three; }
        set { this.sTreatment_Three = value; }
    }

    string sGuidelineThree;
    public string GuidelineThree
    {
        get { return this.sGuidelineThree; }
        set { this.sGuidelineThree = value; }
    }

    string sGuidelineBoxNine;
    public string GuidelineBoxNine
    {
        get { return this.sGuidelineBoxNine; }
        set { this.sGuidelineBoxNine = value; }
    }

    string sGuidelineBoxTen;
    public string GuidelineBoxTen
    {
        get { return this.sGuidelineBoxTen; }
        set { this.sGuidelineBoxTen = value; }
    }

    string sGuidelineBoxEleven;
    public string GuidelineBoxeleven
    {
        get { return this.sGuidelineBoxEleven; }
        set { this.sGuidelineBoxEleven = value; }
    }

    string sGuidelineBoxtwelve;
    public string GuidelineBoxtwelve
    {
        get { return this.sGuidelineBoxtwelve; }
        set { this.sGuidelineBoxtwelve = value; }
    }

    string sDateOfServiceThree;
    public string dateOfServiceThree
    {
        get { return this.sDateOfServiceThree; }
        set { this.sDateOfServiceThree = value; }
    }

    private string sz_Comments_Three;
    public string sz_Comments_three
    {
        get { return sz_Comments_Three; }
        set { sz_Comments_Three = value; }
    }

    string sCbGranted_Three;
    public string cbGranted_Three
    {
        get { return this.sCbGranted_Three; }
        set { this.sCbGranted_Three = value; }
    }

    string sCbGrantedPrejudice_Three;
    public string CbGrantedPrejudice_Three
    {
        get { return this.sCbGrantedPrejudice_Three; }
        set { this.sCbGrantedPrejudice_Three = value; }
    }

    string sCbDenied_Three;
    public string CbDenied_Three
    {
        get { return this.sCbDenied_Three; }
        set { this.sCbDenied_Three = value; }
    }
    //-----------------------------------------------------------------
    public string sTreatment_Four;
    public string TreatmentFour
    {
        get { return this.sTreatment_Four; }
        set { this.sTreatment_Four = value; }
    }

    string sGuidelineFour;
    public string GuidelineFour
    {
        get { return this.sGuidelineFour; }
        set { this.sGuidelineFour = value; }
    }

    string sGuidelineBoxthirteen;
    public string GuidelineThirteen
    {
        get { return this.sGuidelineBoxthirteen; }
        set { this.sGuidelineBoxthirteen = value; }
    }

    string sGuidelineBoxFourteen;
    public string GuidelineBoxfourteen
    {
        get { return this.sGuidelineBoxFourteen; }
        set { this.sGuidelineBoxFourteen = value; }
    }

    string sGuidelineBoxfifteen;
    public string GuidelineBoxfifteen
    {
        get { return this.sGuidelineBoxfifteen; }
        set { this.sGuidelineBoxfifteen = value; }
    }

    string sGuidelineBoxsixteen;
    public string GuidelineBoxsixteen
    {
        get { return this.sGuidelineBoxsixteen; }
        set { this.sGuidelineBoxsixteen = value; }
    }

    string sDateOfServiceFour;
    public string dateOfServiceFour
    {
        get { return this.sDateOfServiceFour; }
        set { this.sDateOfServiceFour = value; }
    }

    private string sz_Comments_Four;
    public string sz_Comments_four
    {
        get { return sz_Comments_Four; }
        set { sz_Comments_Four = value; }
    }

    string sCbGranted_Four;
    public string cbGranted_Four
    {
        get { return this.sCbGranted_Four; }
        set { this.sCbGranted_Four = value; }
    }

    string sCbGrantedPrejudice_Four;
    public string CbGrantedPrejudice_Four
    {
        get { return this.sCbGrantedPrejudice_Four; }
        set { this.sCbGrantedPrejudice_Four = value; }
    }

    string sCbDenied_Four;
    public string CbDenied_Four
    {
        get { return this.sCbDenied_Four; }
        set { this.sCbDenied_Four = value; }
    }

    string sCbContact_One;
    public string CbContactOne
    {
        get { return this.sCbContact_One; }
        set { this.sCbContact_One = value; }
    }

    string sCbContact_two;
    public string CbContacttwo
    {
        get { return this.sCbContact_two; }
        set { this.sCbContact_two = value; }
    }

    string sCarrier_One;
    public string Carrier_One
    {
        get { return this.sCarrier_One; }
        set { this.sCarrier_One = value; }
    }

    string sCarrier_two;
    public string Carrier_two
    {
        get { return this.sCarrier_two; }
        set { this.sCarrier_two = value; }
    }

    string sCbCarrier_One;
    public string CbCarrier_One
    {
        get { return this.sCbCarrier_One; }
        set { this.sCbCarrier_One = value; }
    }

    string sCarrier_three;
    public string Carrier_three
    {
        get { return this.sCarrier_three; }
        set { this.sCarrier_three = value; }
    }

    string sCarrierDate;
    public string CarrierDate
    {
        get { return this.sCarrierDate; }
        set { this.sCarrierDate = value; }
    }

    string sEmployer;
    public string Employer
    {
        get { return this.sEmployer; }
        set { this.sEmployer = value; }
    }

    string sPrintNameOne;
    public string PrintNameOne
    {
        get { return this.sPrintNameOne; }
        set { this.sPrintNameOne = value; }
    }

    string sTitle_One;
    public string TitleOne
    {
        get { return this.sTitle_One; }
        set { this.sTitle_One = value; }
    }

    string sEmployerDate;
    public string EmployerDate
    {
        get { return this.sEmployerDate; }
        set { this.sEmployerDate = value; }
    }

    string sCBProvider;
    public string CBProvider
    {
        get { return this.sCBProvider; }
        set { this.sCBProvider = value; }
    }

    string sMedicalDate;
    public string MedicalDate
    {
        get { return this.sMedicalDate; }
        set { this.sMedicalDate = value; }
    }

    string sCBMedical_request_two;
    public string Medical_request_two
    {
        get { return this.sCBMedical_request_two; }
        set { this.sCBMedical_request_two = value; }
    }

    string sCBMedical_request_three;
    public string MedicalDate_three
    {
        get { return this.sCBMedical_request_three; }
        set { this.sCBMedical_request_three = value; }
    }

    string sMedicalDate_four;
    public string CBMedical_request_four
    {
        get { return this.sMedicalDate_four; }
        set { this.sMedicalDate_four = value; }
    }

    string sCBMedical_request_five;
    public string MedicalDate_five
    {
        get { return this.sCBMedical_request_five; }
        set { this.sCBMedical_request_five = value; }
    }

    string sProviderDate;
    public string ProviderDate
    {
        get { return this.sProviderDate; }
        set { this.sProviderDate = value; }
    }

    string sCbProvider_request;
    public string CBProvider_request
    {
        get { return this.sCbProvider_request; }
        set { this.sCbProvider_request = value; }
    }

    string sProvider_request;
    public string Provider_request
    {
        get { return this.sProvider_request; }
        set { this.sProvider_request = value; }
    }

    string sCB_request_two;
    public string CB_request_two
    {
        get { return this.sCB_request_two; }
        set { this.sCB_request_two = value; }
    }

    string sCB_request_three;
    public string CB_request_three
    {
        get { return this.sCB_request_three; }
        set { this.sCB_request_three = value; }
    }

    string sCB_request_four;
    public string CB_request_four
    {
        get { return this.sCB_request_four; }
        set { this.sCB_request_four = value; }
    }

    string sCB_request_five;
    public string CB_request_five
    {
        get { return this.sCB_request_five; }
        set { this.sCB_request_five = value; }
    }

    string sPrintName_two;
    public string Print_Name_two
    {
        get { return this.sPrintName_two; }
        set { this.sPrintName_two = value; }
    }

    string sTitle_two;
    public string Title_two
    {
        get { return this.sTitle_two; }
        set { this.sTitle_two = value; }
    }

    string sEmployer_Date_two;
    public string EmployerDate_two
    {
        get { return this.sEmployer_Date_two; }
        set { this.sEmployer_Date_two = value; }
    }
}
    