using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;

namespace gb.mbs.da.model.attorney
{
    public class Attorney
    {
        public string Id { get; set; }
        public string FirmName {  get; set; }
        public string ContactName { get; set; }
        public string Name { get; set; } //added new ...not removing firstname.contactname
        public string EmailID { get; set; }
        public string Faxnumber { get; set; }
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
