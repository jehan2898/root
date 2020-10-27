using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using gb.mbs.da.model.account;
using System.Data.SqlClient;
using System.Configuration;
using gb.mbs.da.service.util;

using SharedCache.WinServiceCommon.Provider.Cache;

namespace gb.mbs.da.services.diagnosis
{
    public class SrvDiagnosis
    {
        public DataTable Select(Account p_oAccount)
        {
            DataTable dtDiagnosis = new DataTable();
            DataTable dtUniqueCache = new DataTable(); 
            DataTable dtCache = null;
            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(DBUtil.ConnectionString);
            sqlConnection.Open();
            try
            {
                SqlCommand selectCommand = new SqlCommand("sp_get_diagnosis_codes", sqlConnection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", p_oAccount.ID);
                new SqlDataAdapter(selectCommand).Fill(dtDiagnosis);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection = null;
            }
            return dtDiagnosis;
        }
    }
}