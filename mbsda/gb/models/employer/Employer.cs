using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;

namespace gb.mbs.da.model.employer
{
    public class Employer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
