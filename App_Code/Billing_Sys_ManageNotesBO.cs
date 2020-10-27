using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Billing_Sys_ManageNotes
/// </summary>
public class Billing_Sys_ManageNotesBO
{String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;	
	public Billing_Sys_ManageNotesBO()
	{
	 strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();	
	}
    public ArrayList GetPopupNotesDesc(String p_szCaseID, String p_szCompanyID)
    {
        ArrayList _return = new ArrayList();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_NOTES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_POPUP_MSG_LIST");
            
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            if (ds.Tables[0] != null)
            {
                for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
                {
                    _return.Add(ds.Tables[0].Rows[i].ItemArray.GetValue(0));
                }
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return _return;
    }
    public void UpdateManageNotes(int p_szNoteID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_NOTES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_NOTE_ID", p_szNoteID);

            comm.Parameters.AddWithValue("@FLAG", "UPDATE_MANAGE_NOTES");
            comm.ExecuteNonQuery();          
            
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    public DataSet GetDataManageNotes(String p_szCaseID, String p_szCompanyID)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_NOTES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);    
            comm.Parameters.AddWithValue("@FLAG", "GET_MANAGE_NOTES");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }


    public string GetPatientLatestID()
    {
        string strPatientID = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_PATIENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@FLAG", "GET_LATEST_PATIENT_ID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strPatientID = dr[0].ToString();
            } 
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return strPatientID;
    }

    public DataSet GetCaseDetails(string patientid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PATIENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            comm.Parameters.AddWithValue("@FLAG", "GET_CASE_DETAILS");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetCaseDetails(string patientid, string companyId)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("GET_CASE_DETAILS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }
}
