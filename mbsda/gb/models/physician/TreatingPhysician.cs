using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.physician
{
    public class TreatingPhysician
    {
        public string DoctorID { get; set; }
        public string DoctorName { set; get; }
        public string Title { set; get; }
        public gbmodel.physician.Physician Physician { set; get; }
        public gbmodel.provider.Provider Provider { set; get; }
        public string LicenseNumber { set; get; }
        public string WCBAuthorization { set; get; }
        public string WCBRatingCode{ set; get; }
        public string NPI { set; get; }
        public gbmodel.specialty.Specialty Specialty { get; set; }
        public string FederalTaxID { set; get; }
        public string EmployeeType { set; get; }
        public string BitTaxIDType { set; get; }
        public string IsReferral { set; get; }
        public string IsUnBilled { set; get; }
        public string IsSupervisingDoctor { set; get; }
    }
}
