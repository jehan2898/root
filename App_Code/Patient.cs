using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webapi.doctor.da.gb.model.account;

namespace webapi.doctor.da.gb.model.patient
{
    public class Patient
    {
        public int RowID { set; get; }
        public int CaseID { set; get; }
        public int CaseNo { set; get; }
        public string ClaimNumber { set; get; }
        public string Name { set; get; }
        public string ID { set; get; }
        public string DOA { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string DOB { set; get; }
        public string VisitDate { set; get; }
        public string Age { set; get; }
        public string Gender { set; get; }
        public Account Account { set; get; }
       
    }
}