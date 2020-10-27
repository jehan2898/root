using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.physician
{
    public class ReadingPhysician : physician.Physician
    {
        public string Title { set; get; }
        public string AssignmentNumber { set; get; }
        public office.Office Office { set; get; }

        public patient.Patient Patient { set; get; }
        public provider.Provider Provider { set; get; }
        public string LicenseNumber { set; get; }
        public int WorkType { set; get; }
        public string WCBAuthorization { set; get; }
        public string WCBRatingCode { set; get; }
        public string FederalTaxID { set; get; }
        public bool BitTaxIDType { set; get; }
        public bool IsDisabled { set; get; }

        public string DoctorType { set; get;}
        public string DoctorTypeID { set; get; } 

        
    }
}
