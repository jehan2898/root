using System;
using System.Collections.Generic;
using gb.mbs.da.model;

namespace gb.mbs.da.model.patient.note
{
    public class Note
    {
        public patient.Patient Patient { set; get; }
        public carrier.Carrier Carrier { set; get; }
        public appointment.Appointment Appointment { set; get; }
    }
}
