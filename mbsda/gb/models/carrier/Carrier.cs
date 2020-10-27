using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.common;

namespace gb.mbs.da.model.carrier
{
    public class Carrier
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public string CaseNumber { set; get; }
        public Address Address { set; get; }


    }
}
