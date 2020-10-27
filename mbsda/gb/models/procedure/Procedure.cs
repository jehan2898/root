using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.procedure
{
    public class Procedure
    {
        public string ID { set; get; }
        public string TypeID { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public double Amount { set; get; }
        public specialty.Specialty Specialty { set; get; }
    }
}