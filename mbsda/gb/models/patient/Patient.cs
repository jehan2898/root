using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;
using gb.mbs.da.common;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.patient
{
    public class Patient
    {
        public int RowID { set; get; }
        public int CaseID { set; get; }
        public int CaseNo { set; get; }
        public string ClaimNumber { set; get; }
        public string WCBNumber { set; get; } // c3 needed
        public string PolicyNumber { set; get; } // NYS Needed
        public string Name { set; get; }
        public string ID { set; get; }
        public string Patient_ID { set; get; }
        public string DOA { set; get; }
        public string DOB { set; get; } //used in patient search

        public int Age { set; get; }
        public string Email { set; get; }
        public string Gender { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string JobTitle { set; get; }

        //YC Added
        public gbmodel.casetype.CaseType CaseType { set; get; }
        public string Occupation { get; set; }
        public Address Address { set; get; }
        public string MailingAddress { set; get; }

        public string WorkPhone { set; get; }
        public string SSN { set; get; }
        public string HomePhoneNumber { set; get; }
        public string CellNumber { set; get; }
        public string ExtensionNumber { set; get; }


        public gbmodel.employer.Employer Employer { get; set; }
        public gbmodel.adjuster.Adjuster Adjuster { get; set; }
        public gbmodel.carrier.Carrier Carrier { set; get; }
        public gbmodel.attorney.Attorney Attorny { set; get; }


        public gbmodel.account.Account Account { set; get; }
        public SearchParameters SearchParameters { set; get; }
    }
}