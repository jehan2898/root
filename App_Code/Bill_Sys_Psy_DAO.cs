using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ProcedureCode;

namespace Bill_Sys_Psy_DAO
{
    public class PsychologistsReport
    {
        public PsychologistsReport()
        {

        }
    }
    public class PsychologistBill
    {

        string PPOYes1;
        string _48HR1;
        string _15Days1;
        string _90Days1;
        string WCBCaseNo1;
        string CarrierCaseNo1;
        string DateOfInjury1;
        string Time1;
        string AddressWhereInjuryOccured1;
        string InjuredPersonSSN1;
        string InjuredPersonFirstName1;
        string InjuredPersonMI1;
        string InjuredPersonLastName1;
        string InjuredPersonAddress1;
        string InjuredPersonTelephoneNo1;
        string EmployerName1;
        string EmployerAddress1;
        string PatientDateOfBirth1;
        string InsuranceCarrierName1;
        string InsuranceCarrierAddress1;
        string ReferringPhysicianName1;
        string ReferringPhysicianAddress1;
        string ReferringPhysicianTelephoneNo1;
        string TreatmentUnderCHKBox1;
        string PreviousHistoryDate1;
        string IncidentHistory1;
        string HistoryPreExistingInjury1;
        string ReferralCHKBox1;
        string YourEvaluation1;
        string PatientConditionAndProgress1;
        string PlannedFutureTreatment1;
        string TreatmentB2CHKBox1;
        string DateOfVisitOnWichReportIsBased1;
        string DateOfFirstVisit1;
        string PatientSeenAgainCHKBox1;
        string WhenWillPatientSeenAgain1;
        string PatientAttendingDoctor1;
        string PatienWorking1;
        string ResumedLimitedWork1;
        string ResumedRegularWork1;
        string OccurenceCauseInjuryCHKBox1;
        string AdditionalPertinentInformation1;
        string DiagCodeA1;
        string DiagCodeB1;
        string DiagCodeC1;
        string DiagCodeD1;
        string FederalTaxIDNO1;
        string SSNCHKBox1;
        string EINCHKBox1;
        string WCBAuthorizationNo1;
        string PatientAccountNo1;
        string TotalCharges1;
        string AmountPaid1;
        string SignatureOfTreatingPsychologist1;
        string BalanceDue1;
        string PsychologistNameAdressPhoneNO1;
        string BillingNameAdressPhoneNO1;
        string AttendingTime1;
        string ssn1;
        string ein1;
        public ArrayList ar1;
        ArrayList DiagAr1;

        public ArrayList DiagAr
        {
            get { return this.DiagAr1; }
            set { this.DiagAr1 = value; }
        }
        public ArrayList ar
        {
            get { return this.ar1; }
            set { this.ar1 = value; }
        }

        public string PPOYes
        {

            get { return this.PPOYes1; }
            set { this.PPOYes1 = value; }
        }

        public string AttendingTime
        {

            get { return this.AttendingTime1; }
            set { this.AttendingTime1 = value; }
        }
        public string _48HR
        {

            get { return this._48HR1; }
            set { this._48HR1 = value; }
        }

        public string _15Days
        {

            get { return this._15Days1; }
            set { this._15Days1 = value; }
        }

        public string _90Days
        {

            get { return this._90Days1; }
            set { this._90Days1 = value; }
        }
        public string WCBCaseNo
        {

            get { return this.WCBCaseNo1; }
            set { this.WCBCaseNo1 = value; }
        }


        public string CarrierCaseNo
        {
            get { return this.CarrierCaseNo1; }
            set { this.CarrierCaseNo1 = value; }
        }


        public string DateOfInjury
        {
            get { return this.DateOfInjury1; }
            set { this.DateOfInjury1 = value; }
        }


        public string Time
        {
            get { return this.Time1; }
            set { this.Time1 = value; }
        }


        public string AddressWhereInjuryOccured
        {
            get { return this.AddressWhereInjuryOccured1; }
            set { this.AddressWhereInjuryOccured1 = value; }
        }


        public string InjuredPersonSSN
        {
            get { return this.InjuredPersonSSN1; }
            set { this.InjuredPersonSSN1 = value; }
        }


        public string InjuredPersonFirstName
        {
            get { return this.InjuredPersonFirstName1; }
            set { this.InjuredPersonFirstName1 = value; }
        }


        public string InjuredPersonMI
        {
            get { return this.InjuredPersonMI1; }
            set { this.InjuredPersonMI1 = value; }
        }


        public string InjuredPersonLastName
        {
            get { return this.InjuredPersonLastName1; }
            set { this.InjuredPersonLastName1 = value; }
        }


        public string InjuredPersonAddress
        {
            get { return this.InjuredPersonAddress1; }
            set { this.InjuredPersonAddress1 = value; }
        }


        public string InjuredPersonTelephoneNo
        {
            get { return this.InjuredPersonTelephoneNo1; }
            set { this.InjuredPersonTelephoneNo1 = value; }
        }


        public string EmployerName
        {
            get { return this.EmployerName1; }
            set { this.EmployerName1 = value; }
        }


        public string EmployerAddress
        {
            get { return this.EmployerAddress1; }
            set { this.EmployerAddress1 = value; }
        }


        public string PatientDateOfBirth
        {
            get { return this.PatientDateOfBirth1; }
            set { this.PatientDateOfBirth1 = value; }
        }


        public string InsuranceCarrierName
        {
            get { return this.InsuranceCarrierName1; }
            set { this.InsuranceCarrierName1 = value; }
        }


        public string InsuranceCarrierAddress
        {
            get { return this.InsuranceCarrierAddress1; }
            set { this.InsuranceCarrierAddress1 = value; }
        }


        public string ReferringPhysicianName
        {
            get { return this.ReferringPhysicianName1; }
            set { this.ReferringPhysicianName1 = value; }
        }


        public string ReferringPhysicianAddress
        {
            get { return this.ReferringPhysicianAddress1; }
            set { this.ReferringPhysicianAddress1 = value; }
        }


        public string ReferringPhysicianTelephoneNo
        {
            get { return this.ReferringPhysicianTelephoneNo1; }
            set { this.ReferringPhysicianTelephoneNo1 = value; }
        }


        public string TreatmentUnderCHKBox
        {
            get { return this.TreatmentUnderCHKBox1; }
            set { this.TreatmentUnderCHKBox1 = value; }
        }

        public string PreviousHistoryDate
        {
            get { return this.PreviousHistoryDate1; }
            set { this.PreviousHistoryDate1 = value; }
        }


        public string IncidentHistory
        {
            get { return this.IncidentHistory1; }
            set { this.IncidentHistory1 = value; }
        }


        public string HistoryPreExistingInjury
        {
            get { return this.HistoryPreExistingInjury1; }
            set { this.HistoryPreExistingInjury1 = value; }
        }


        public string ReferralCHKBox
        {
            get { return this.ReferralCHKBox1; }
            set { this.ReferralCHKBox1 = value; }
        }

        public string YourEvaluation
        {
            get { return this.YourEvaluation1; }
            set { this.YourEvaluation1 = value; }
        }


        public string PatientConditionAndProgress
        {
            get { return this.PatientConditionAndProgress1; }
            set { this.PatientConditionAndProgress1 = value; }
        }


        public string PlannedFutureTreatment
        {
            get { return this.PlannedFutureTreatment1; }
            set { this.PlannedFutureTreatment1 = value; ; }
        }

        public string TreatmentB2CHKBox
        {
            get { return this.TreatmentB2CHKBox1; }
            set { this.TreatmentB2CHKBox1 = value; ; }
        }

        public string DateOfVisitOnWichReportIsBased
        {
            get { return this.DateOfVisitOnWichReportIsBased1; }
            set { this.DateOfVisitOnWichReportIsBased1 = value; }
        }


        public string DateOfFirstVisit
        {
            get { return this.DateOfFirstVisit1; }
            set { this.DateOfFirstVisit1 = value; }
        }

        public string PatientSeenAgainCHKBox
        {
            get { return this.PatientSeenAgainCHKBox1; }
            set { this.PatientSeenAgainCHKBox1 = value; }
        }

        public string WhenWillPatientSeenAgain
        {
            get { return this.WhenWillPatientSeenAgain1; }
            set { this.WhenWillPatientSeenAgain1 = value; }
        }

        public string PatientAttendingDoctor
        {
            get { return this.PatientAttendingDoctor1; }
            set { this.PatientAttendingDoctor1 = value; }
        }

        public string PatienWorking
        {
            get { return this.PatienWorking1; }
            set { this.PatienWorking1 = value; }
        }
        public string ResumedLimitedWork
        {
            get { return this.ResumedLimitedWork1; }
            set { this.ResumedLimitedWork1 = value; }
        }


        public string ResumedRegularWork
        {
            get { return this.ResumedRegularWork1; }
            set { this.ResumedRegularWork1 = value; }
        }

        public string OccurenceCauseInjuryCHKBox
        {
            get { return this.OccurenceCauseInjuryCHKBox1; }
            set { this.OccurenceCauseInjuryCHKBox1 = value; }
        }

        public string AdditionalPertinentInformation
        {
            get { return this.AdditionalPertinentInformation1; }
            set { this.AdditionalPertinentInformation1 = value; }
        }

        public string DiagCodeA
        {
            get { return this.DiagCodeA1; }
            set { this.DiagCodeA1 = value; }
        }
        public string DiagCodeB
        {
            get { return this.DiagCodeB1; }
            set { this.DiagCodeB1 = value; }
        }

        public string DiagCodeC
        {
            get { return this.DiagCodeC1; }
            set { this.DiagCodeC1 = value; }
        }

        public string DiagCodeD
        {
            get { return this.DiagCodeD1; }
            set { this.DiagCodeD1 = value; }
        }

        public string FederalTaxIDNO
        {
            get { return this.FederalTaxIDNO1; }
            set { this.FederalTaxIDNO1 = value; }
        }


        public string SSNCHKBox
        {
            get { return this.SSNCHKBox1; }
            set { this.SSNCHKBox1 = value; }
        }

        public string EINCHKBox
        {
            get { return this.EINCHKBox1; }
            set { this.EINCHKBox1 = value; }
        }

        public string WCBAuthorizationNo
        {
            get { return this.WCBAuthorizationNo1; }
            set { this.WCBAuthorizationNo1 = value; }
        }

        public string PatientAccountNo
        {
            get { return this.PatientAccountNo1; }
            set { this.PatientAccountNo1 = value; }
        }


        public string TotalCharges
        {
            get { return this.TotalCharges1; }
            set { this.TotalCharges1 = value; }
        }


        public string AmountPaid
        {
            get { return this.AmountPaid1; }
            set { this.AmountPaid1 = value; }
        }


        public string BalanceDue
        {
            get { return this.BalanceDue1; }
            set { this.BalanceDue1 = value; }
        }


        public string SignatureOfTreatingPsychologist
        {
            get { return this.SignatureOfTreatingPsychologist1; }
            set { this.SignatureOfTreatingPsychologist1 = value; }
        }



        public string PsychologistNameAdressPhoneNO
        {
            get { return this.PsychologistNameAdressPhoneNO1; }
            set { this.PsychologistNameAdressPhoneNO1 = value; }
        }


        public string BillingNameAdressPhoneNO
        {
            get { return this.BillingNameAdressPhoneNO1; }
            set { this.BillingNameAdressPhoneNO1 = value; }
        }

        public string ssn
        {
            get { return this.ssn1; }
            set { this.ssn1 = value; }
        }

        public string ein
        {
            get { return this.ein1; }
            set { this.ein1 = value; }
        }

    }

}
