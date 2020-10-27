using System;
using System.Collections.Generic;
using System.Web;


public class Report_visit_configurationDAO
{
	public Report_visit_configurationDAO()
	{
    }
		private string sz_name;

     public string Sz_name
    {
        get
        {
            return sz_name;
        }

        set
        {
            sz_name = value;
        }
    }

     private string sz_company_id;
     public string CompanyID
     {
         get
         {
             return sz_company_id;
         }
         set
         {
             sz_company_id = value;
         }
     }
    
     private string sz_case_type;

     public string Sz_case_type
     {
         get { return sz_case_type; }
         set { sz_case_type = value; }
     }
     private string sz_case_status;

     public string Sz_case_status
     {
         get { return sz_case_status; }
         set { sz_case_status = value; }
     }

     private string sz_carrier;

     public string Sz_carrier
     {
         get { return sz_carrier; }
         set { sz_carrier = value; }
     }
     private string sz_shedule;

     public string Sz_shedule
     {
         get { return sz_shedule; }
         set { sz_shedule = value; }
     }
     private string sz_provider;

     public string Sz_provider
     {
         get { return sz_provider; }
         set { sz_provider = value; }
     }
     private string sz_attorney;

     public string Sz_attorney
     {
         get { return sz_attorney; }
         set { sz_attorney = value; }
     }
     private string sz_location;

     public string Sz_location
     {
         get { return sz_location; }
         set { sz_location = value; }
     }

     private string sz_specialty;

     public string Sz_specialty
     {
         get { return sz_specialty; }
         set { sz_specialty = value; }
     }

     private string sz_doctor;

     public string Sz_doctor
     {
         get { return sz_doctor; }
         set { sz_doctor = value; }
     }
     private string sz_accident_date;

     public string Sz_accident_date
     {
         get { return sz_accident_date; }
         set { sz_accident_date = value; }
     }


     private string sz_case_number;

     public string Sz_case_number
     {
         get { return sz_case_number; }
         set { sz_case_number = value; }
     }


     public void resetAll()
     {
         sz_name = string.Empty;
         sz_company_id = string.Empty;
         sz_case_type = string.Empty;
         sz_case_status = string.Empty;
         sz_carrier = string.Empty;
         sz_shedule = string.Empty;
         sz_provider = string.Empty;
         sz_attorney = string.Empty;
         sz_location = string.Empty;
         sz_specialty = string.Empty;
         sz_doctor = string.Empty;
         sz_accident_date = string.Empty;
         sz_case_number = string.Empty;
     }
 }
