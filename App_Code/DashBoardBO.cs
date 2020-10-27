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


public class DashBoardBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    SqlDataReader dr;

    public DashBoardBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public String getAppoinmentCount(string p_szStartDate,string p_szEndDate,string p_szCompanyID,string p_szFlag)
    {
        String szMsg = "";
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szMsg = dr["MESSAGE"].ToString();
            }
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
        return szMsg;
    }


    public String getBilledUnbilledProcCode(string p_szCompanyID, string p_szFlag)
    {
        String szMsg = "";
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szMsg = dr["MESSAGE"].ToString();
            }
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
        return szMsg;
    }


    public DataTable GetConfigDashBoard(String p_szUserRoleID)
    {
        DataTable dt = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_szUserRoleID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_CONFINDASHBOARD");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
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
        return dt;
    }
    
    public String getTotalVisits(string p_szCompanyID, string p_szFlag)
    {
        String szMsg = "";
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szMsg = dr["MESSAGE"].ToString();
            }
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
        return szMsg;
    }


    public DataTable getVisitDetails(String p_szCompanyID,string p_szFlag)
    {
        DataTable dt = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VISIT_COUNT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
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
        return dt;
    }

    public DataTable getMissingSpecialityList(string sz_CompanyID)

	    {
        DataTable dtble = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASHBOARD_MISSING_SPECIALITY_PATIENT_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            SqlDataAdapter dadtp = new SqlDataAdapter(sqlCmd);
            dadtp.Fill(dtble);
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
        return dtble;

    }

    //8 April patient visit status block - sachin

    public String getPatientVisitStatusCount( string p_szRefCompanyID, string p_szFlag)
    {
        String szMsg = "";
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
       
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szRefCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szMsg = dr["MESSAGE"].ToString();
            }
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
        return szMsg;
    }

    public DataSet AllDashBoardData(string p_szStartDate, string p_szEndDate, string weekstartdt, string weekenddt, string p_szCompanyID, string p_szUserRoleID, string p_szFlag,string sz_user_id)
    {
        String szMsg = "";
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        SqlDataAdapter da;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_szUserRoleID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            sqlCmd.Parameters.AddWithValue("@sz_user_id",sz_user_id);
            da=new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
            DataTable dt = new DataTable();
            if ((weekenddt != "") && (weekstartdt != ""))
            {
                sqlCmd.Parameters["@START_DATE"].Value = weekstartdt;
                sqlCmd.Parameters["@END_DATE"].Value = weekenddt;
                sqlCmd.Parameters["@FLAG"].Value = "GET_APPOINTMENT_DASHBOARD";
                dt = new DataTable();
                da.Fill(dt);
                ds.Tables.Add(dt);
            }
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
        return ds;
    }
   




}
