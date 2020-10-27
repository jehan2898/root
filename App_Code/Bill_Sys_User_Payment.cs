using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;

public class Bill_Sys_User_Payment
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    public Bill_Sys_User_Payment()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet Get_PaymetInfo(string szBillNo)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_BILL_AND_PAYMENT_DETIALS", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_NO", szBillNo);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dsResult;
    }
}