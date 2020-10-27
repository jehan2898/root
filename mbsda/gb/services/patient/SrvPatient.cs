using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.service.util;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.dbconstant;
using gb.mbs.da.model.common;
//using gb.mbs.da.model.patient;
using gbmodel=gb.mbs.da.model;

namespace gb.mbs.da.service.patient
{
    public class SrvPatient
    {
        public ArrayList Search(gbmodel.user.User p_oUser, SearchParameters p_oSearchParameter)
        {
            List<SqlParameter> oParams = new List<SqlParameter>();
            ArrayList oResult = null;
            
            oParams.Add(new SqlParameter("@sz_company_id", p_oUser.Account.ID));
            oParams.Add(new SqlParameter("@i_start_index", p_oSearchParameter.StartIndex));
            oParams.Add(new SqlParameter("@i_end_index", p_oSearchParameter.EndIndex));
            oParams.Add(new SqlParameter("@sz_order_by", p_oSearchParameter.OrderBy));
            oParams.Add(new SqlParameter("@sz_search_text", p_oSearchParameter.SearchText));

            DataSet ds = null;
            try
            {
                ds = DBUtil.DataSet(Procedures.PR_SEARCH_PATIENT, oParams);
            }
            catch (Exception io)
            {

            }

            oResult = new ArrayList(2);
            List<gbmodel.patient.Patient> oPatientList = new List<gbmodel.patient.Patient>();
            gbmodel.patient.Patient oPatient = null;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                oPatient = new gbmodel.patient.Patient();
                oPatient.RowID = Convert.ToInt32(ds.Tables[0].Rows[i]["rowid"].ToString());
                oPatient.CaseID = Convert.ToInt32(ds.Tables[0].Rows[i]["sz_case_id"].ToString());
                oPatient.CaseNo = Convert.ToInt32(ds.Tables[0].Rows[i]["sz_case_no"].ToString());
                oPatient.ClaimNumber = ds.Tables[0].Rows[i]["sz_claim_number"].ToString();
                oPatient.Name = ds.Tables[0].Rows[i]["sz_patient_name"].ToString();

                gbmodel.carrier.Carrier oCarrier = new gbmodel.carrier.Carrier();
                oCarrier.Name = ds.Tables[0].Rows[i]["sz_insurance_company"].ToString();

                oPatient.Carrier = oCarrier;
                oPatient.ID = ds.Tables[0].Rows[i]["sz_patient_id"].ToString();
                oPatient.DOA = ds.Tables[0].Rows[i]["dt_accident_date"].ToString();

                gbmodel.account.Account oAccount = new gbmodel.account.Account();
                oAccount.ID = ds.Tables[0].Rows[i]["sz_company_id"].ToString();
                
                oPatient.Account = oAccount;
                oPatient.FirstName = ds.Tables[0].Rows[i]["sz_patient_first_name"].ToString();
                oPatient.LastName = ds.Tables[0].Rows[i]["sz_patient_last_name"].ToString();

                oPatientList.Add(oPatient);
            }

            oResult.Add(oPatientList.ToArray());
            
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                oResult.Add(ds.Tables[1].Rows[i]["count"].ToString());
            }
            return oResult;
        }

        /**
            Required input: Patient.CaseID, Patient.Account.AccountID
            This method is used on patient desk to show notes added across specialties for a patient
        **/
        public List<gbmodel.patient.SpecialtyNote> SelectSpecialtyNote(gbmodel.patient.Patient p_oPatient)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            //ArrayList list = new ArrayList();
            List<gbmodel.patient.SpecialtyNote> oList = new List<gbmodel.patient.SpecialtyNote>();

            gbmodel.patient.SpecialtyNote sNote = new gbmodel.patient.SpecialtyNote();
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_pdesk_specialty_note", connection);
                selectCommand.Parameters.AddWithValue("@i_case_id", p_oPatient.CaseID);
                selectCommand.Parameters.AddWithValue("@sz_company_id", p_oPatient.Account.ID);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                ds = new DataSet();
                new SqlDataAdapter(selectCommand).Fill(ds);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        sNote = new gbmodel.patient.SpecialtyNote();
                        sNote.Speciality = new gbmodel.speciality.Speciality();
                        sNote.Patient = new gbmodel.patient.Patient();
                        sNote.Account = new gbmodel.account.Account();
                        sNote.CreatedBy = new gbmodel.user.User();
                        sNote.UpdatedBy = new gbmodel.user.User();

                        sNote.Text = dr["Text"].ToString();
                        sNote.Speciality.ID = dr["SpecialtyID"].ToString();
                        sNote.Patient.CaseID = Convert.ToInt32(dr["CaseID"]);
                        sNote.CreatedBy.ID = dr["CreatedBy"].ToString();
                        sNote.Account.ID = dr["CompanyID"].ToString();
                        sNote.UpdatedBy.ID = dr["UpdatedBy"].ToString();
                        sNote.Created = Convert.ToDateTime(dr["Created"]);
                        //sNote.Updated = Convert.ToDateTime(dr["Updated"]);
                        oList.Add(sNote);
                    }
              
            }
           finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            /*
                call procedure sp_select_pdesk_specialty_note and pass @i_case_id and @sz_company_id
                from the returned result set all fields of SpecialtyNote and return the list
                write code between try and finally block. dont catch error. we will do it after we implement logging
            */
            return oList;
        }

        /**
            Required input: gbmodel.patient.SpecialtyNote
            This method is used on patient desk to create notes added on a specialty for a patient
        **/
        public int CreateSpecialtyNote(gbmodel.patient.SpecialtyNote p_oSpecialty)
        {
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            
            int iRowsAffected = 0;
            try
            {
                connection.Open();
                SqlCommand Command = new SqlCommand("sp_create_pdesk_specialty_note", connection);
                Command.Parameters.AddWithValue("@s_text", p_oSpecialty.Text);
                Command.Parameters.AddWithValue("@sz_specialty_id", p_oSpecialty.Speciality.ID);
                Command.Parameters.AddWithValue("@i_case_id", p_oSpecialty.Patient.CaseID);
                Command.Parameters.AddWithValue("@sz_user_id", p_oSpecialty.CreatedBy.ID);
                Command.Parameters.AddWithValue("@sz_company_id", p_oSpecialty.Account.ID);
                Command.Parameters.AddWithValue("@sz_updated_by", p_oSpecialty.UpdatedBy.ID);
                Command.Parameters.AddWithValue("@dt_updated", p_oSpecialty.Updated);
                Command.CommandType = CommandType.StoredProcedure;
                iRowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Specialty ID or Case ID missing");
            }
           finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return iRowsAffected;
            /*
                Call procedure sp_create_pdesk_specialty_note and pass all fields for SpecialtyNote
                write code between try and finally block. dont catch error. we will do it after we implement logging
                return the count of affected rows
                parameter required by the procedure 
	            @s_text NVARCHAR(500),
	            @sz_specialty_id NVARCHAR(20),
	            @i_case_id INT,
	            @sz_user_id NVARCHAR(20),
	            @sz_company_id NVARCHAR(20),
	            @sz_updated_by NVARCHAR(20),
	            @dt_updated DATETIME
            */
           
        }
    }
}