using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.master
{
  public class PlaceOfService
    {
       
        public Int32   Id{ set; get; }
        public string AddressType { set; get; }
        public string Address{ set; get; }
        public string  Address1{ set; get; }
        public string Address2{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Zipcode{ set; get; }
        public string CompanyId{ set; get; }
        public string Code{ set; get; }
        public string UserId{ set; get; }
        public DateTime CreatedDate{ set; get; }
        public DateTime ModifiedDate{ set; get; }
        public string  CreatedBy{ set; get; }
        public string ModifiedBy{ set; get; }
        public bool bt_is_active { set; get; }
    }
  }
