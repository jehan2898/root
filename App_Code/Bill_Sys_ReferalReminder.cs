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
public class Bill_Sys_ReferalReminder
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_ReferalReminder()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void updateReminder(string p_szScheduledReferalID, string p_szUpdatedUserID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_REMINDER_LIST";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_SCHEDULED_REFERAL_ID", p_szScheduledReferalID);
            comm.Parameters.AddWithValue("@SZ_LAST_UPDATED_USER_ID", p_szUpdatedUserID);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetReminderList(ArrayList arrayList)
    {
        ds = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_REMINDER_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_REFERAL_OFFICE_ID", arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURAL_CODE_ID", arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[2].ToString());
            
            comm.Parameters.AddWithValue("@FLAG", "LIST");

            sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public void saveReminder(ArrayList arrayList)
    {
        conn = new SqlConnection(strConn);
        try
        {
            
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_SCHEDULED_REFERAL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_REFERAL_OFFICE_ID", arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURAL_CODE_ID", arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_REMINDER_DATE", arrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_SCHEDULED_DATE", arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_LAST_UPDATED_USER_ID", arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_CREATED_USER_ID", arrayList[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[6].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetOfficeName(string p_szOfficeID)
    {
        string szOfficeName = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_REFERAL_OFFICE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.Parameters.AddWithValue("@SZ_REFERAL_OFFICE_ID", p_szOfficeID);
            comm.Parameters.AddWithValue("@FLAG", "REFERAL_OFFICE_NAME");
            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szOfficeName = (dr[0].ToString());
            }
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { conn.Close(); }
        return szOfficeName;
    }

    public DataSet GetProcedureCode(string groupID, string companyID)
    {
        ds = new DataSet();
        string szOfficeName = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PROCEDURE_CODES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", groupID);
            
            comm.Parameters.AddWithValue("@FLAG", "GROUP_PROCEDURE_CODE_LIST");
            comm.CommandType = CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(ds);

           

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { conn.Close(); }
        return ds;
    }
}
