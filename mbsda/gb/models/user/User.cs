using System;
using System.Collections.Generic;
using System.Text;
using gbmodel=gb.mbs.da.model;

namespace gb.mbs.da.model.user
{
    public class User
    {
        public gbmodel.account.Account Account { set; get; }
        //public Specialty Specialty { set; get; }
        public Role Role { set; get; }
        //public Physician Physician { set; get; }

        public string ID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Domain { set; get; }
        public string Email { set; get; }
        public string Token { set; get; }
        public gbmodel.provider.Provider Provider { set; get; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        public gbmodel.office.Office Office { set; get; }
        public bool IsDisabled { set; get; }
        public bool IsEmailVerified { set; get; }
        public bool IsOTPDisabled { set; get; }
        public bool IsShowPreviousVisitByDefault { set; get; }
    }
}