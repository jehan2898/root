using gb.mbs.da.service.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using dataaccess = gb.mbs.da.dataaccess;

namespace gb.mbs.da.gb.services.patient.note
{
    public class SrvCHNotesV2
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);

        public DataSet GetObjectiveFindings(model.patient.Patient p_oPatient)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_objective_findings", connection);
                selectCommand.Parameters.AddWithValue("@sz_company_id", p_oPatient.Account.ID);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                ds = new DataSet();
                new SqlDataAdapter(selectCommand).Fill(ds);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return ds;
        }
    }
}
