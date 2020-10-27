using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.physician
{
    public class Physician
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string PhoneNo { set; get; }
        public string FaxNo { set; get; }
        public account.Account Account { set; get; }
        public specialty.Specialty Specialty { set; get; }
    }
}