using System;
using System.Collections.Generic;
using System.Text;
using gbmodel=gb.mbs.da.model;
using gb.mbs.da.model.common;

namespace gb.mbs.da.model.intakeprovider
{
   public class IntakeProvider
    {
       public string Id { get; set; }// if null  -- alwayz 0 so type=string     
       public string Name { get; set; }
       public string Address { get; set; }
       public string City { get; set; }      
       public string Zip { get; set; }
       public string Phone { get; set; }
       public DateTime CreatedDate { get; set; }
       public DateTime UpdatedDate { get; set; }
       public string Email { get; set; }
      
      

    }
}
