using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;
using gb.mbs.da.common;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.patient.form
{
    public class MG2
    {
        
        public gbmodel.employer.Employer Employer { get; set; }
        public gbmodel.adjuster.Adjuster Adjuster { get; set; }
        public gbmodel.carrier.Carrier Carrier { set; get; }
        public gbmodel.attorney.Attorney Attorny { set; get; }
        public gbmodel.account.Account Account { set; get; }
        public gbmodel.patient.Patient Patient { set; get; }
        public gbmodel.physician.Physician Doctor { set; get; }
        public gbmodel.provider.Provider Provider { set; get; }
        public gbmodel.user.User User { set; get; }
         
        public string BodyInitial { set; get; }
        public string GuidelineSection{ set; get; }
        public string ApprovalRequest{ set; get; }
        public string DateOfService{ set; get; }
        public string DatesOfDeniedRequest{ set; get; }
        public string ChkDid{ set; get; }
        public string ChkDidNot{ set; get; }
        public string ContactDate{ set; get; }
        public string PersonContacted{ set; get; }
        public string ChkCopySent{ set; get; }
        public string FaxEmail{ set; get; }
        public string ChkCopyNotSent{ set; get; }
        public string IndicatedFaxEmail{ set; get; }
        
        public string ChkNoticeGiven{ set; get; }
        public string PrintCarrierEmployerNoticeName{ set; get; }
        public string NoticeTitle{ set; get; }
        public string NoticeCarrierSign{ set; get; }
        public string NoticeCarrierSignDate{ set; get; }
        public string ChkGranted{ set; get; }
        public string ChkGrantedInParts{ set; get; }
        public string ChkWithoutPrejudice{ set; get; }
        public string ChkDenied{ set; get; }
        public string ChkBurden{ set; get; }
        public string ChkSubstantiallySimilar{ set; get; }
        public string CarrierDenial{ set; get; }
        public string MedicalProfessional{ set; get; }
        public string ChkMedicalArbitrator{ set; get; }
        public string ChkWCBHearing{ set; get; }
        public string PrintCarrierEmployerResponseName{ set; get; }
        public string ResponseTitle{ set; get; }
        public string ResponseCarrierSign{ set; get; }
        public string ResponseCarrierSignDate{ set; get; }
        public string PrintDenialCarrierName{ set; get; }
        public string DenialTitle{ set; get; }
        public string DenialCarrierSign{ set; get; }
        public string DenialCarrierSignDate{ set; get; }
        public string ChkRequestWC{ set; get; }
        public string ChkMedicalArbitratorByWC{ set; get; }
        public string ChkWCBHearingByWC{ set; get; }
        public string ClaimantSign{ set; get; }
        public string ClaimantSignDate{ set; get; }



    }
}
