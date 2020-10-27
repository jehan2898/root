using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using webapi.doctor.da.gb.model.physician;
using webapi.doctor.da.gb.model.account;
using webapi.doctor.da.gb.model.specialty;
using webapi.doctor.da.gb.model.user;

namespace webapi.doctor.da.gb.model.user
{
    public class User
    {
        public Account Account {set;get;}
        public Specialty Specialty { set; get; }
        public Role Role { set; get; }
        public Physician Physician { set; get; }

        public string ID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Domain { set; get; }
        public string Email { set; get; }
        public string Token { set; get; }
    }
}