using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.intake
{
    public class IntakeForm
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public string Value { set; get; }
        public string DataType { set; get; }        
    }
}
