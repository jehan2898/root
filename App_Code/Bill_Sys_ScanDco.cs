using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Bill_Sys_ScanDco
/// </summary>
public class Bill_Sys_ScanDco
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;

    SqlDataReader dr;
	public Bill_Sys_ScanDco()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet SearchDocument(string sz_company_id, string sz_from_date,string sz_to_date,string sz_case_no)
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_ALL_SCAN_DOCUMNETS", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@SZ_COMAPNY_ID",sz_company_id );
            cmd.Parameters.AddWithValue("@SZ_FROM_DATE",sz_from_date );
            cmd.Parameters.AddWithValue("@SZ_TO_DATE",sz_to_date );
            cmd.Parameters.AddWithValue("@SZ_CASE_NO", sz_case_no);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
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
        return ds;
    }

    public DataSet GetVisit(string sz_company_id, string sz_from_date, string sz_to_date, string sz_case_no,string sz_doctor_id,string sz_bill_type)
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_get_all_visits_for_doc", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@DT_FORM_VISIT_DATE", sz_from_date);
            cmd.Parameters.AddWithValue("@DT_TO_VISIT_DATE", sz_to_date);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            cmd.Parameters.AddWithValue("@SZ_CASE_NO", sz_case_no);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctor_id);
            cmd.Parameters.AddWithValue("@BT_STATUS", sz_bill_type);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
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
        return ds;
    }

    public DataSet GetProviderList(string sz_CompanyID)
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_OFFICE_REFF", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            cmd.Parameters.AddWithValue("@FLAG", "OFFICE_LIST");
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
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
        return ds;
    }

    public DataSet GetRefferingDoctorList(string sz_CompanyID, string sz_providerID)
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_OFFICE_REFF", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            cmd.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_providerID);
            cmd.Parameters.AddWithValue("@FLAG", "REFFERING_DOCTOR_LIST");
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
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
        return ds;
    }

    public string updateVisitRefferingOffice(string sz_CompanyID, string sz_EventID, string sz_ReffOfficeID, string sz_ReffDoctorID)
    {
        string msg = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_add_reffering_provider_to_visit", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventID);
            if (sz_ReffOfficeID.ToLower() != "na" && sz_ReffOfficeID != null && sz_ReffOfficeID != "")
            {
                cmd.Parameters.AddWithValue("@sz_reffering_office_id", sz_ReffOfficeID);
            }
            if (sz_ReffDoctorID.ToLower() != "na" && sz_ReffDoctorID != null && sz_ReffDoctorID != "")
            {
                cmd.Parameters.AddWithValue("@sz_reffering_doctor_id", sz_ReffDoctorID);
            }
            cmd.ExecuteNonQuery();
            msg = "success";
        }
       catch (Exception ex)
        {
            msg = ex.Message.ToString();
            ex.Message.ToString();
        }
        finally
        {
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return msg;

    } 
}