using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.patient;
using gb.mbs.da.common;
using gbmodel = gb.mbs.da.model; 

namespace gb.mbs.da.model.intake
{
    public class Intake
    {
        public gbmodel.intakeprovider.IntakeProvider IntakeProvider { get; set; }
        public List<gbmodel.intake.IntakeForm> FormField { get; set; }

        public gbmodel.casetype.CaseType CaseType { get; set; }
        public gbmodel.adjuster.Adjuster Adjuster { get; set; }
        public gbmodel.carrier.Carrier Carrier { set; get; }
        public gbmodel.attorney.Attorney Attorney { set; get; }
    }
}
