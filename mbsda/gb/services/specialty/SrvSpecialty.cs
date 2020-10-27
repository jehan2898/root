using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.service.specialty
{
    public class SrvSpecialty
    {
        public DataSet IsSpecialtyNodeConfigured(DataTable dataTable, List<gbmodel.specialty.Specialty> p_oSpecialty, model.account.Account p_oAccount)
        {
            DataSet dataSet = null;
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            oConnection.Open();

            DataTable oDTProcedure = null;
            if (p_oSpecialty != null && p_oSpecialty.Count > 0)
            {
                oDTProcedure = gbmodel.specialty.type.TypeSpecialty.FillDBType(p_oSpecialty);
            }
            try
            {
                SqlCommand com = new SqlCommand();
                com = new SqlCommand("sp_ifexists_specialty_node", oConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@tvp_specialty", oDTProcedure);
                com.Parameters.AddWithValue("@sz_process_name", "POM");
                com.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(com);
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                }
            }
            return dataSet;
        }
    }
}
