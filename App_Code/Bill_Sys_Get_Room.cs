using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Bill_Sys_Room
/// </summary>
public class Bill_Sys_Get_Room
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_Get_Room()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet GetRoomDetails(string sz_CompanyID)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_MST_GET_ROOM_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
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
        return ds;
    }
}