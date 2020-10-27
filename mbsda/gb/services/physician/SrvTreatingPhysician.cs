using gb.mbs.da.service.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.services.physician
{
    public class SrvTreatingPhysician
    {
        public int Create(gbmodel.physician.TreatingPhysician p_oPhysician, gbmodel.user.User p_oUser)
        {
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            int result=0;
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MST_DOCTOR", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_NAME", p_oPhysician.DoctorName);
                sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", p_oPhysician.LicenseNumber);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", p_oUser.Account.ID);
                sqlCommand.Parameters.AddWithValue("@SZ_OFFICE_ID", p_oPhysician.Provider.Id);
                sqlCommand.Parameters.AddWithValue("@SZ_WCB_AUTHORIZATION", p_oPhysician.WCBAuthorization);
                sqlCommand.Parameters.AddWithValue("@SZ_WCB_RATING_CODE", p_oPhysician.WCBRatingCode);
                sqlCommand.Parameters.AddWithValue("@SZ_NPI", p_oPhysician.NPI);
                sqlCommand.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", p_oPhysician.FederalTaxID);
                sqlCommand.Parameters.AddWithValue("@BIT_TAX_ID_TYPE", p_oPhysician.BitTaxIDType);
                sqlCommand.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_oPhysician.Specialty.ID);
                sqlCommand.Parameters.AddWithValue("@SZ_TITLE", p_oPhysician.Title);
                sqlCommand.Parameters.AddWithValue("@I_IS_EMPLOYEE", p_oPhysician.EmployeeType);
                sqlCommand.Parameters.AddWithValue("@IS_REFERRAL", p_oPhysician.IsReferral);
                sqlCommand.Parameters.AddWithValue("@BT_IS_UNBILLED", p_oPhysician.IsUnBilled);
                sqlCommand.Parameters.AddWithValue("@BT_SUPERVISING_DOCTOR", p_oPhysician.IsSupervisingDoctor);
                sqlCommand.Parameters.AddWithValue("@FLAG", "ADD");
                result = sqlCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

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

            return result;
        }

        public int Update(gbmodel.physician.TreatingPhysician p_oPhysician, gbmodel.user.User p_oUser)
        {
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            int result = 0;
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MST_DOCTOR", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_oPhysician.DoctorID);
                sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_NAME", p_oPhysician.DoctorName);
                sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", p_oPhysician.LicenseNumber);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", p_oUser.Account.ID);
                sqlCommand.Parameters.AddWithValue("@SZ_OFFICE_ID", p_oPhysician.Provider.Id);
                sqlCommand.Parameters.AddWithValue("@SZ_WCB_AUTHORIZATION", p_oPhysician.WCBAuthorization);
                sqlCommand.Parameters.AddWithValue("@SZ_WCB_RATING_CODE", p_oPhysician.WCBRatingCode);
                sqlCommand.Parameters.AddWithValue("@SZ_NPI", p_oPhysician.NPI);
                sqlCommand.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", p_oPhysician.FederalTaxID);
                sqlCommand.Parameters.AddWithValue("@BIT_TAX_ID_TYPE", p_oPhysician.BitTaxIDType);
                sqlCommand.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_oPhysician.Specialty.ID);
                sqlCommand.Parameters.AddWithValue("@SZ_TITLE", p_oPhysician.Title);
                sqlCommand.Parameters.AddWithValue("@I_IS_EMPLOYEE", p_oPhysician.EmployeeType);
                sqlCommand.Parameters.AddWithValue("@IS_REFERRAL", p_oPhysician.IsReferral);
                sqlCommand.Parameters.AddWithValue("@BT_IS_UNBILLED", p_oPhysician.IsUnBilled);
                sqlCommand.Parameters.AddWithValue("@BT_SUPERVISING_DOCTOR", p_oPhysician.IsSupervisingDoctor);
                sqlCommand.Parameters.AddWithValue("@FLAG", "UPDATE");
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

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

            return result;
        }

        public string CheckDoctorVisitExists(gbmodel.physician.TreatingPhysician p_oPhysician, gbmodel.user.User p_oUser)
        {
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            string result = "";
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_exists_doctor_visit", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_doctor_id", p_oPhysician.DoctorID);
                sqlCommand.Parameters.AddWithValue("@sz_company_id", p_oUser.Account.ID);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["RECORD EXISTS"].ToString();
                }
            }
            catch (Exception ex)
            {

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

            return result;
        }
    }
}
