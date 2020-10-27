using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.bill
{
    public class Bill
    {
        public string Number { get; set; }       
        public gbmodel.specialty.Specialty Specialty { get; set; }
        public DateTime FirstVisitDate { get; set; }
        public DateTime LastVisitDate { get; set; }
        public int PomId { get; set; }
        public List<gbmodel.payment.Payment> Payment { get; set; }
        public gbmodel.bill.BillStatus BillStatus { get; set; }
        public double BillAmount { set; get; }
        public double PaidAmount { set; get; }
        public double OutstandingAmount { set; get; }
        public gbmodel.patient.Patient Patient { get; set; }
        public List<gbmodel.bill.denial.Denial> Denial { get; set; }
        public List<gbmodel.bill.verification.Verification> Verification { get; set; }
        public string Process { get; set; }
        public List<gbmodel.bill.pom.POM> Pom { get; set; }
    }
}
