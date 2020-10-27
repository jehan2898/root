using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Delivery_Reciept
/// </summary>
public class Delivery_Reciept
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

	public Delivery_Reciept()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet Get_DeliveryReciept(string szCompayid,string szProcedureGroupid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_PROC_CODE_USING_SPECIALTY", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompayid);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupid);

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

    public DataSet Get_PrintDeliveryReciept(string szCaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_PRINT_DELIVERY_RECEIPT", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseid);

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
