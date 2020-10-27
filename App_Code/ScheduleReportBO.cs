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


public class ScheduleReportBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public ScheduleReportBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet getScheduleReport(string p_szStartDate,string p_szEndDate,string p_szCompanyID,string p_szFlag)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
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
        return ds;
    }

    public void setDone(string p_szEventID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DONE");
            sqlCmd.ExecuteNonQuery();
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
    }


    public void delete(string p_szEventID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
            sqlCmd.ExecuteNonQuery();
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
    }




    public DataSet getCalenderScheduleReport(string p_szStartDate, string p_szEndDate, string p_szCompanyID,string p_szDoctorID, string p_szStatus)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CALENDER_SCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@DT_END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
            sqlCmd.Parameters.AddWithValue("@I_STATUS", p_szStatus);
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
        return ds;
    }

    public DataSet getCalenderOutScheduleReport(string p_szStartDate, string p_szEndDate, string p_szCompanyID, string p_szReferingID, string p_szDoctorID, string p_szStatus, string p_szOfficeID, string p_szSort, string p_name, string p_szCaseNO, string p_szCharNo)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();          
            sqlCmd = new SqlCommand("SP_CALENDER_OUTSCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@DT_END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERENCE_ID", p_szReferingID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
            sqlCmd.Parameters.AddWithValue("@I_STATUS", p_szStatus);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", p_szOfficeID);

             sqlCmd.Parameters.AddWithValue("@chart_no", p_szCharNo);
             sqlCmd.Parameters.AddWithValue("@patient_name", p_name.Trim());
             sqlCmd.Parameters.AddWithValue("@Sz_Case_no", p_szCaseNO);
          
           
            if (!p_szSort.Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SORT_EXPRESSION", p_szSort);
            }
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
        return ds;
    }
    public DataSet getCalenderOutScheduleReportNEW(string p_szStartDate, string p_szEndDate, string p_szCompanyID, string p_szReferingID, string p_szDoctorID, string p_szStatus, string p_szOfficeID, string p_szSort, string p_name, string p_szCaseNO, string p_szCharNo,string sz_user_id)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();          
            sqlCmd = new SqlCommand("SP_CALENDER_OUTSCHEDULE_REPORT_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_START_DATE", p_szStartDate);
            sqlCmd.Parameters.AddWithValue("@DT_END_DATE", p_szEndDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERENCE_ID", p_szReferingID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
            sqlCmd.Parameters.AddWithValue("@I_STATUS", p_szStatus);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", p_szOfficeID);

             sqlCmd.Parameters.AddWithValue("@chart_no", p_szCharNo);
             sqlCmd.Parameters.AddWithValue("@patient_name", p_name.Trim());
             sqlCmd.Parameters.AddWithValue("@Sz_Case_no", p_szCaseNO);
            sqlCmd.Parameters.AddWithValue("@sz_user_id", sz_user_id);
           
            if (!p_szSort.Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SORT_EXPRESSION", p_szSort);
            }
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
        return ds;
    }

    public void DeleteEvent(string p_szEventID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_EVENT_FOR_BILLING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            sqlCmd.ExecuteNonQuery();
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
    }
}
