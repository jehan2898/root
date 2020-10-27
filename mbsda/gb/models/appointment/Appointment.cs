using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.appointment
{
  public  class Appointment
    {
        public long ID { set; get; }
        public string Date { set; get; }
        public string LastVisitDate { set; get; }
        public string Status { set; get; }
        public string Type { set; get; }
        public string TypeID { set; get; }
        public bool IsFinalized { set; get; }
        public bool HasDoctorSigned { set; get; }
        public bool HasPatientSigned { set; get; }
        public bool HasProcedure { set; get; }
        public bool HasDiagnosis { set; get; }
        public bool IsBilled { set; get; }
        public gbmodel.patient.Patient Patient { set; get; }
        public gbmodel.speciality.Speciality Speciality { set; get; }
        public gbmodel.physician.Physician Physician { get; set; }
        public gbmodel.appointment.note.Note Note { set; get; }
        public gbmodel.user.User User { set; get; }        
    }
}