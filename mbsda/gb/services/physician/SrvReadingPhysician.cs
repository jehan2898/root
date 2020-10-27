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
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.services.physician
{
    public class SrvReadingPhysician
    {
        public int Create(model.physician.ReadingPhysician p_oPhysician, string Flag)
        {
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            int iRowsAffected = 0;

            try
            {
                connection.Open();
                SqlCommand Command = new SqlCommand("SP_MST_READINGDOCTOR", connection);
                Command.Parameters.AddWithValue("@FLAG", Flag);
                Command.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_oPhysician.ID);
                Command.Parameters.AddWithValue("@SZ_DOCTOR_NAME", p_oPhysician.Name);

                if (p_oPhysician.Specialty.ID != "" && p_oPhysician.Specialty.ID != null)
                    Command.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_oPhysician.Specialty.ID);
                else
                    Command.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", DBNull.Value);

                if (p_oPhysician.Title != "" && p_oPhysician.Title != null)
                    Command.Parameters.AddWithValue("@SZ_TITLE", p_oPhysician.Title);
                else
                    Command.Parameters.AddWithValue("@SZ_TITLE", DBNull.Value);

                if (p_oPhysician.LicenseNumber != "" && p_oPhysician.LicenseNumber != null)
                    Command.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", p_oPhysician.LicenseNumber);
                else
                    Command.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", DBNull.Value);

                if (p_oPhysician.AssignmentNumber != "" && p_oPhysician.AssignmentNumber != null)
                    Command.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", p_oPhysician.AssignmentNumber);
                else
                    Command.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", DBNull.Value);

                if (p_oPhysician.WCBAuthorization != "" && p_oPhysician.WCBAuthorization != null)
                    Command.Parameters.AddWithValue("@SZ_WCB_AUTHORIZATION", p_oPhysician.WCBAuthorization);
                else
                    Command.Parameters.AddWithValue("@SZ_WCB_AUTHORIZATION", DBNull.Value);
                if (p_oPhysician.WCBRatingCode != "" && p_oPhysician.WCBRatingCode != null)
                    Command.Parameters.AddWithValue("@SZ_WCB_RATING_CODE", p_oPhysician.WCBRatingCode);
                else
                    Command.Parameters.AddWithValue("@SZ_WCB_RATING_CODE", DBNull.Value);

                if (p_oPhysician.WorkType != null)
                    Command.Parameters.AddWithValue("@BT_IS_OWNER", p_oPhysician.WorkType);
                else
                    Command.Parameters.AddWithValue("@BT_IS_OWNER", DBNull.Value);

                if (p_oPhysician.FederalTaxID != "" && p_oPhysician.FederalTaxID != null)
                    Command.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", p_oPhysician.FederalTaxID);
                else
                    Command.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", DBNull.Value);

                if (p_oPhysician.BitTaxIDType != null)
                    Command.Parameters.AddWithValue("@BIT_TAX_ID_TYPE", p_oPhysician.BitTaxIDType);
                else
                    Command.Parameters.AddWithValue("@BIT_TAX_ID_TYPE", DBNull.Value);

                if (p_oPhysician.Account.ID != "" && p_oPhysician.Account.ID != null)
                    Command.Parameters.AddWithValue("@SZ_COMPANY_ID", p_oPhysician.Account.ID);
                else
                    Command.Parameters.AddWithValue("@SZ_COMPANY_ID", DBNull.Value);

                if (p_oPhysician.Provider.Id != "" && p_oPhysician.Provider.Id != null)
                    Command.Parameters.AddWithValue("@SZ_PROVIDER_ID", p_oPhysician.Provider.Id);
                else
                    Command.Parameters.AddWithValue("@SZ_PROVIDER_ID", DBNull.Value);

                if (p_oPhysician.Patient.ID != "" && p_oPhysician.Patient.ID != null)
                    Command.Parameters.AddWithValue("@SZ_PATIENT_ID", p_oPhysician.Patient.ID);
                else
                    Command.Parameters.AddWithValue("@SZ_PATIENT_ID", DBNull.Value);

                if (p_oPhysician.Office.ID != "" && p_oPhysician.Office.ID != null)
                    Command.Parameters.AddWithValue("@SZ_OFFICE_ID", p_oPhysician.Office.ID);
                else
                    Command.Parameters.AddWithValue("@SZ_OFFICE_ID", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_OFFICE_CITY", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_OFFICE_STATE", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_OFFICE_ZIP", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_OFFICE_PHONE", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_BILLING_ADDRESS", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_BILLING_CITY", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_BILLING_STATE", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_BILLING_ZIP", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_BILLING_PHONE", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_NPI", DBNull.Value);
                Command.Parameters.AddWithValue("@FLT_KOEL", DBNull.Value);
                Command.Parameters.AddWithValue("@SZ_DOCTOR_TYPE", DBNull.Value);
                Command.CommandType = CommandType.StoredProcedure;
                iRowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                // log and throw error
                throw x;
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
        }

        public gbmodel.physician.ReadingPhysician Select(gbmodel.account.Account p_Account, gbmodel.physician.ReadingPhysician p_ReadingPhysician)
        {
            gbmodel.physician.ReadingPhysician oReadingPhysician = new gbmodel.physician.ReadingPhysician();

            try
            {
                SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
                DataSet ds = new DataSet();

                SqlCommand sqlCommand = new SqlCommand("sp_select_reading_doctor_single", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddWithValue("@sz_doctor_id", p_ReadingPhysician.ID);
                sqlCommand.Parameters.AddWithValue("@sz_company_id", p_Account.ID);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oReadingPhysician = new gbmodel.physician.ReadingPhysician();
                    oReadingPhysician.Specialty = new gbmodel.specialty.Specialty();
                    oReadingPhysician.Office = new gbmodel.office.Office();
                    if (dr["SZ_TITLE"] != DBNull.Value)
                        oReadingPhysician.Title = dr["SZ_TITLE"].ToString();

                    if (dr["SZ_DOCTOR_NAME"] != DBNull.Value)
                        oReadingPhysician.Name = dr["SZ_DOCTOR_NAME"].ToString();
                    if (dr["SZ_DOCTOR_LICENSE_NUMBER"] != DBNull.Value)
                        oReadingPhysician.LicenseNumber = dr["SZ_DOCTOR_LICENSE_NUMBER"].ToString();
                    if (dr["SZ_ASSIGN_NUMBER"] != DBNull.Value)
                        oReadingPhysician.AssignmentNumber = dr["SZ_ASSIGN_NUMBER"].ToString();
                    if (dr["BT_IS_OWNER"] != DBNull.Value)
                        oReadingPhysician.WorkType = Convert.ToInt32(dr["BT_IS_OWNER"]);
                    if (dr["SZ_PROCEDURE_GROUP_ID"] != DBNull.Value)
                        oReadingPhysician.Specialty.ID = dr["SZ_PROCEDURE_GROUP_ID"].ToString();

                    if (dr["SZ_OFFICE_ID"] != DBNull.Value)
                        oReadingPhysician.Office.ID = dr["SZ_OFFICE_ID"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }


            return oReadingPhysician;
        }

        public List<gbmodel.physician.ReadingPhysician> Select(gbmodel.account.Account p_Account)
        {
            DataSet ds;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            List<gbmodel.physician.ReadingPhysician> sList = new List<gbmodel.physician.ReadingPhysician>();
            gbmodel.physician.ReadingPhysician o_ReadingPhysician = new gbmodel.physician.ReadingPhysician();

            try
            {
                connection.Open();
                command = new SqlCommand("SP_MST_READINGDOCTOR", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SZ_COMPANY_ID", p_Account.ID);
                command.Parameters.AddWithValue("@FLAG", "LIST");
                ds = new DataSet();
                new SqlDataAdapter(command).Fill(ds);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o_ReadingPhysician = new gbmodel.physician.ReadingPhysician();
                        o_ReadingPhysician.Specialty = new gbmodel.specialty.Specialty();
                        o_ReadingPhysician.Office = new gbmodel.office.Office();
                        o_ReadingPhysician.Provider = new gbmodel.provider.Provider();
                        o_ReadingPhysician.Account = new gbmodel.account.Account();

                        if (dr["SZ_ASSIGN_NUMBER"] != DBNull.Value)
                            o_ReadingPhysician.AssignmentNumber = dr["SZ_ASSIGN_NUMBER"].ToString();

                        if (dr["SZ_DOCTOR_LICENSE_NUMBER"] != DBNull.Value)
                            o_ReadingPhysician.LicenseNumber = dr["SZ_DOCTOR_LICENSE_NUMBER"].ToString();

                        if (dr["BT_IS_OWNER"] != DBNull.Value)
                            o_ReadingPhysician.WorkType = Convert.ToInt32(dr["BT_IS_OWNER"]);

                        if (dr["SZ_TITLE"] != DBNull.Value)
                            o_ReadingPhysician.Title = dr["SZ_TITLE"].ToString();

                        if (dr["SZ_DOCTOR_NAME"] != DBNull.Value)
                            o_ReadingPhysician.Name = dr["SZ_DOCTOR_NAME"].ToString();

                        if (dr["SZ_DOCTOR_TYPE"] != DBNull.Value)
                              o_ReadingPhysician.DoctorType = dr["SZ_DOCTOR_TYPE"].ToString();

                        if (dr["SZ_DOCTOR_TYPE_ID"] != DBNull.Value)
                            o_ReadingPhysician.DoctorTypeID = dr["SZ_DOCTOR_TYPE_ID"].ToString();

                        if (dr["SZ_PROCEDURE_GROUP_ID"] != DBNull.Value)
                            o_ReadingPhysician.Specialty.ID = dr["SZ_PROCEDURE_GROUP_ID"].ToString();

                        if (dr["SZ_PROCEDURE_GROUP"] != DBNull.Value)
                            o_ReadingPhysician.Specialty.Name = dr["SZ_PROCEDURE_GROUP"].ToString();

                        if (dr["SZ_OFFICE_ID"] != DBNull.Value)
                            o_ReadingPhysician.Office.ID = dr["SZ_OFFICE_ID"].ToString();

                        if (dr["SZ_OFFICE"] != DBNull.Value)
                            o_ReadingPhysician.Office.Name = dr["SZ_OFFICE"].ToString();

                        if (dr["SZ_DOCTOR_ID"] != DBNull.Value)
                            o_ReadingPhysician.ID = dr["SZ_DOCTOR_ID"].ToString();

                        if (dr["SZ_COMPANY_ID"] != DBNull.Value)
                            o_ReadingPhysician.Account.ID = dr["SZ_COMPANY_ID"].ToString();

                        if (dr["SZ_PROVIDER_ID"] != DBNull.Value)
                            o_ReadingPhysician.Provider.Id = dr["SZ_PROVIDER_ID"].ToString();

                        if (dr["SZ_WCB_AUTHORIZATION"] != DBNull.Value)
                            o_ReadingPhysician.WCBAuthorization = dr["SZ_WCB_AUTHORIZATION"].ToString();

                        if (dr["SZ_WCB_RATING_CODE"] != DBNull.Value)
                            o_ReadingPhysician.WCBRatingCode = dr["SZ_WCB_RATING_CODE"].ToString();

                        if (dr["SZ_FEDERAL_TAX_ID"] != DBNull.Value)
                            o_ReadingPhysician.FederalTaxID = dr["SZ_FEDERAL_TAX_ID"].ToString();

                        if (dr["BIT_TAX_ID_TYPE"] != DBNull.Value)
                            o_ReadingPhysician.BitTaxIDType = Convert.ToBoolean(dr["BIT_TAX_ID_TYPE"]);

                        if (dr["bt_is_disabled"] != DBNull.Value)
                            o_ReadingPhysician.IsDisabled = Convert.ToBoolean(dr["bt_is_disabled"]);
                        sList.Add(o_ReadingPhysician);
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
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
            return sList;
        }

        public int Delete(string sz_doctor_id, string sz_company_id, int IsActive)
        {
            int iResult = 0;

            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);

            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_toggle_reading_doctor_single", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                sqlCommand.Parameters.AddWithValue("@sz_doctor_id", sz_doctor_id);
                sqlCommand.Parameters.AddWithValue("@bt_is_disabled", IsActive);
                iResult = Convert.ToInt16(sqlCommand.ExecuteNonQuery());
            }
            catch (Exception x)
            {
                //log and throw
                throw x;
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
            return iResult;
        }
    }
}