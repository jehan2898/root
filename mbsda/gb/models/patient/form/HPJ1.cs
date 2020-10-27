using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;
using gb.mbs.da.common;
using gbmodel = gb.mbs.da.model;
namespace gb.mbs.da.model.patient.form
{
     public class HPJ1
    {
         public gbmodel.employer.Employer Employer { get; set; }
         public gbmodel.provider.Provider Provider { set; get; }
         public gbmodel.user.User User { set; get; }
         public gbmodel.carrier.Carrier Carrier { set; get; }
         public gbmodel.patient.Patient Patient { set; get; }
         public gbmodel.bill.Bill Bill { set; get; }

         public string WCBAuthNumber { set; get; }

         public string InsuredIdNumber { set; get; }
         public string InjuredOccured { set; get; }

         public string ProviderAddress { set; get; }
         public string ProviderState { set; get; }
         public string ProviderCity { set; get; }
         public string ProviderZip { set; get; }

         public string PhyCompDate { set; get; }
         public string AllOthCompDate { set; get; }

         public string ProviderEmpty { set; get; }
         public string CarrierEmpty { set; get; }

         public string WSignText   { set; get; }
         public string WSignText2  { set; get; }
         public string StateText   { set; get; }
         public string SSText      { set; get; }
         public string CountryOf   { set; get; }
         public string BeingText   { set; get; }
         public string HeistheText { set; get; }
         public string DayText     { set; get; }
         public string DayOffText  { set; get; }

    }
}
