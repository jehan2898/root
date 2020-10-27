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
/// Summary description for Calender_Event
/// </summary>
public class Calender_Event
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public Calender_Event()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet GetReferringFacility(string szCompanyId)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_REFERRING_FACILITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "REFERRING_FACILITY_LIST");
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }

    public DataSet GetRoomInfo(string sz_Referring_Cmp_Id,string sz_Cmp_Id)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_room_info", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", sz_Referring_Cmp_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Cmp_Id);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }
}
