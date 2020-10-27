using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.dbconstant
{
    public class Procedures
    {
        //public static readonly string PR_AUTHENTICATION = "sp_mod_doctor_authentication"; 
        public static readonly string PR_AUTHENTICATION = "ValidateLogin"; //
        public static readonly string PR_DOCTOR_LOGIN_PARAMETERS = "sp_user_login";

        public static readonly string PR_SEARCH_PATIENT = "sp_app_patient_search";                                         
        public static readonly string PR_PROCEDURE_PATIENT = "SP_MST_PATIENT";
        public static readonly string PR_INTAKE_DOCUMENT = "sp_select_intake_provider_document";

        public static readonly string PR_ADJUSTER_SELECT = "SP_MST_ADJUSTER";
        public static readonly string PR_CARRIER_SELECT = "SP_MST_INSURANCE_COMPANY";
        public static readonly string PR_ATTORNEY_SELECT = "SP_MST_ATTORNEY";
        public static readonly string PR_PROVIDER_SELECT = "SP_MST_OFFICE";        
        public static readonly string PR_PATIENT_DATA_ENTRY = "sp_mst_patient_data_entry";
        public static readonly string PR_CASE_STATUS = "SP_MST_CASE_STATUS";
        public static readonly string PR_INTAKE_INSERT = "sp_intapp_intake_create";
        public static readonly string PR_SELECT_CASETYPE = "sp_intapp_select_casetype";
        public static readonly string PR_DECLARATION = "sp_intapp_declaration_create";
        public static readonly string PR_AOB_ALL = "sp_intapp_aob_create";
        public static readonly string PR_LIEN_ALL = "sp_intapp_lien_create";
        public static readonly string PR_HIPPA_ALL = "sp_intapp_hipaa_create";
        public static readonly string PR_CONSET_ALL = "sp_intapp_consent_create";
        public static readonly string PR_CONSET_INFORMED_ALL = "sp_intapp_consent_informed";

        public static readonly string PR_OCA_CREATE  = "sp_intapp_oca_create";
        public static readonly string PR_NYS_CREATE = "sp_intapp_nys_create";
        public static readonly string PR_EMPLOYEECLAIM_CREATE = "sp_intapp_employeeclaim_create";

        //procedures for Select forms        
        public static readonly string PR_INTAKE_SELECT = "sp_intapp_select_intake";
        public static readonly string PR_DECLARATION_SELECT = "sp_intapp_select_declaration";
        public static readonly string PR_AOB_SELECT = "sp_intapp_select_aob";
        public static readonly string PR_LIEN_SELECT = "sp_intapp_select_lien";
        public static readonly string PR_HIPAA_SELECT = "sp_intapp_select_hipaa";
        public static readonly string PR_CONSET_SELECT = "sp_intapp_select_consent";
        public static readonly string PR_CONSET_INFORMED_SELECT = "sp_intapp_select_consentinformed";

        public static readonly string PR_NYS_SELECT = "sp_intapp_select_nys";
        public static readonly string PR_OCA_SELECT = "sp_intapp_select_oca";
        public static readonly string PR_C3_SELECT = "sp_intapp_select_employeeclaim";     
   
        // for referring office report
        public static readonly string PR_PROCEDURE_CODE_SELECT = "sp_select_procedure_codes_for_specialty";   
    }
}
