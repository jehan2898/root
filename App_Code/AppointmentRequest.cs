using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.doctor.da.gb.model.appointment;

namespace webapi.doctor.models
{
    public class AppointmentRequest
    {
        public webapi.doctor.da.gb.model.user.User User { set; get; }
        public Appointment Appointment { set; get; }
    }
}

