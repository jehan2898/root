using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.model.speciality;
using gb.mbs.da.model.patient;
using gb.mbs.da.model.user;
using gb.mbs.da.model.account;
namespace gb.mbs.da.model.patient
{
    public class SpecialtyNote
    {
        public int ID { get; set;}
        public string Text { get; set;}
        public Speciality Speciality { get; set; }
        public Patient Patient { get; set; }
        public User CreatedBy { get; set; }
        public Account Account { get; set; }
        public DateTime Created { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        /*
            TODO
	        i_id INT,
	        s_text NVARCHAR(500),
	        sz_specialty_id NVARCHAR(20) specialty object,
	        i_case_id INT patient object,
	        sz_user_id NVARCHAR(20) user object
	        sz_company_id NVARCHAR(20) account object,
	        dt_created DATETIME ,
	        sz_updated_by NVARCHAR(20) user object
	        dt_updated DATETIME,
        */
    }
}