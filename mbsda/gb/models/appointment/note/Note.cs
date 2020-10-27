using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.appointment.note
{
    public class Note
    {
        public List<model.appointment.note.ch.ObjectiveFinding> ObjectiveFinding { set; get; }
        public List<model.appointment.note.ch.TreatmentPlan> TreatmentPlan { set; get; }
    }
}