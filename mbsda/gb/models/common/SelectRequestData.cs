using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.common
{
    public class SelectRequestData
    {
        public gbmodel.casetype.CaseType CaseType { get; set; }
        public List<gbmodel.intake.IntakeForm> FormField { get; set; }
        public gbmodel.intakeprovider.IntakeProvider IntakeProvider { get; set; }
        public gbmodel.patient.Patient Patient { get; set; }
    }
}
