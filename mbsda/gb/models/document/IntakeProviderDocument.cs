using System;
using System.Collections.Generic;
using System.Text;
using gbmodel=gb.mbs.da.model;

namespace gb.mbs.da.model.document
{
    public class IntakeProviderDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        //public DateTime CreatedDate {get;set;}

        ///public Account Company { get; set; }
        //public User CreatedBy { get; set; }
        public gbmodel.casetype.CaseType CaseType { get; set; }


    }
}
