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

/// <summary>
/// Summary description for Bill_Sys_Menu
/// </summary>
/// 
public class Bill_Sys_DeleteBO
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
 
    SqlDataReader dr;

    public Bill_Sys_DeleteBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    
    public bool deleteRecord(string p_szSPName,string p_szIDName,string p_szIDValue)
    {
        String szValue = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand(p_szSPName, conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue(p_szIDName, p_szIDValue);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return false;
    }


    public bool deleteRecord(string p_szSPName, string p_szIDName, string p_szIDValue , string p_szKey)
    {
        String szValue = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand(p_szSPName, conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue(p_szIDName, p_szIDValue);
            comm.Parameters.AddWithValue("@FLAG", p_szKey);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return false;
    }


    public bool checkForDelete(string p_szCompanyID, string p_szRoleID)
    {
        String szValue = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_TXN_USER_CONFIGURATION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_ROLE_ID", p_szRoleID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "CHECK_DELETE");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();
            }
            if (szValue!= "")
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return false;
    }

    public bool deleteRecord(string p_szSPName, string p_szIDName, string p_szIDValue, string p_szKey,string szCompanyID)
    {
        String szValue = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand(p_szSPName, conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue(p_szIDName, p_szIDValue);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", p_szKey);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return false;
    }

    public DataSet GetImageIDLhr(string I_EVENT_PROC_ID, string SZ_FILE_NAME)
    {
        //SqlParameter sqlParam = new SqlParameter();
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsImageID = new DataSet();
        string szImageId = "";
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_IMAGEID_LHR", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", I_EVENT_PROC_ID);
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", SZ_FILE_NAME);
            
            da = new SqlDataAdapter(comm);
            da.Fill(dsImageID);
           
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
        return dsImageID;
    }

    public DataSet DeleteDocuments(string szEventProcID, string szFileName, string szImageID, string szFilePath)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        string szImageId = "";
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_LHR_DELETE_DOCUMENTS", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventProcID);
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", szFileName);
            comm.Parameters.AddWithValue("@I_IMAGE_ID", szImageID);
            comm.Parameters.AddWithValue("@SZ_FILE_PATH", szFilePath);

            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public bool deleteEmployerRecord(string p_szIDValue, string p_szCompanyID)
    {
        String szValue = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_MST_EMPLOYER_PROCEDURE_CODES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", p_szIDValue);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return false;
    }
    public void DeleteEventSchedular(DeleteSchedule objSchedule)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "PROC_DELETE_VISIT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@Index ", objSchedule.Index);
            comm.Parameters.AddWithValue("@AppointMentID", objSchedule.AppointmentID);
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    
}
