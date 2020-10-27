using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ReportingDBIntegraion
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataReader dr;

    public string TokenGenration(string userId)
    {
        string token = null;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(strConn);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandText = "Report.CreateToken";
        comm.CommandTimeout = 0;
        comm.CommandType = CommandType.StoredProcedure;
        comm.Connection = conn;
        comm.Parameters.AddWithValue("@UserId", userId);
        dr = comm.ExecuteReader();
        while (dr.Read())
        {
            token = dr["Token"].ToString();
        }

            return token;
    }

}