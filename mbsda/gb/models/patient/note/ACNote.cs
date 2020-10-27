using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.patient.note;

namespace gb.mbs.da.model.patient.note
{
    public class ACNote : Note
    {
        public string DoctorSign  {set;get;}
        public string PatientSign { set; get; }
        
        //public string PatientName { set; get; }
        //public string scaseno { set; get; }
        //public string sdateofAccident { set; get; }
        //public string sInsuranceCompany { set; get; }
        //public string sClaimNo { set; get; }
        
        public string Date {set;get;}
        public string PatientReported {set;get;}
        public string PatientTreated {set;get;}
        public string PainGrades  {set;get;}
        public string Head {set;get;}
        public string Neck {set;get;}
        public string Thoracic {set;get;}
        public string Lumbar {set;get;}
        public string RLShoulder {set;get;}
        public string RLWrist {set;get;}
        public string RLElbow {set;get;}
        public string RLRLKnee {set;get;}
        public string RLRLAnkle {set;get;}
        public string RLHip {set;get;}
        public string Notes {set;get;}
        public string PatientStates {set;get;}
        public string PatientStates1 {set;get;}
        public string PatientStates2 {set;get;}
        public string ChkPatientTolerated {set;get;}
        public string Continue {set;get;}
        public string Acupuncture {set;get;}
        public string Code {set;get;}
        public string PatientTreatedAcupuncture {set;get;}
        public string PatientTreatedElectro {set;get;}
        public string PatientTreatedMoxa {set;get;}
        public string PatientTreatedCupping {set;get;}
        public string DoctorName { set; get; }
        public string EventId { set; get; }
    }
}
