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
using System.Collections;


public class SchedulingBO
{
    string strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public SchedulingBO()
	{
		strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet getSpecialityCountForDay(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SPECIALITY_COUNT_FOR_DAY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@dt_event_date", objAL[1].ToString());
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
        //return ds.Tables[1];
        return ds;
    }

    public DataSet GetCaledarVisitsByTime(string szVisitDate, string szCompanyId, string Time, string Time_Type, string EndTime, string LastTimeType)
    {
        sqlCon = new SqlConnection(strConn);
        DataSet dsVisits = new DataSet();
        try
        {
            sqlCmd = new SqlCommand("SP_GET_VISITS_FOR_TIME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", szVisitDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME", Time);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", Time_Type);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME", EndTime);
            sqlCmd.Parameters.AddWithValue("@DT_END_TIME_TYPE", LastTimeType);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(dsVisits);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dsVisits;
    }

}