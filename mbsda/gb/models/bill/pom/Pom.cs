using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.bill.pom
{
   public class POM
    {
       public string Id { get; set; }
       public string POMStatus { get; set; }
       public gbmodel.specialty.Specialty Specialty { get; set; }
    }
}
