using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webapi.doctor.da.gb.model.patient;
using webapi.doctor.da.gb.model.physician;

using webapi.doctor.da.gb.model.specialty;

namespace webapi.doctor.da.gb.model.appointment
{
    public class Appointment
    {
        public long ID { set; get; }
        public string Date { set; get; }
        public string LastVisitDate { set; get; }
        public string Status { set; get; }
        public string Type { set; get; }
        public bool IsFinalized { set; get; }
        public bool HasDoctorSigned { set; get; }
        public bool HasPatientSigned { set; get; }
        public bool HasProcedure { set; get; }
        public bool HasDiagnosis { set; get; }
        public bool IsBilled { set; get; }
        public string VisitType { set; get; }
        public Patient Patient { set; get; }
        public Specialty Speciality { set; get; }
        public Physician Physician { get; set; }

        
        public string  CaseType {get;set;}
    }
}