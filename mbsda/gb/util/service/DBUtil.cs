using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace gb.mbs.da.service.util
{
    public class DBUtil
    {
        public static string ConnectionString
        {
            get{
                return System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            }
        }

        public static DataSet DataSet(string p_ProcedureName,List<SqlParameter> p_oListParameter)
        {
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            DataSet ds = new DataSet();
            
            try
            {
                oConnection.Open();
                SqlCommand oCommand = new SqlCommand(p_ProcedureName, oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandTimeout = 0;
                oCommand.Parameters.AddRange(p_oListParameter.ToArray());
                SqlDataAdapter adapter = new SqlDataAdapter(oCommand);
                adapter.Fill(ds);

                if (ds == null)
                {
                    throw new dataaccess.exception.NoDataFoundException("No data returned");
                }
            }
            catch (Exception io)
            {
                throw io;
            }
            finally
            {
                if (oConnection != null)
                {
                    oConnection.Close();
                    oConnection = null;
                }
            }
            return ds;
        }
    }
}