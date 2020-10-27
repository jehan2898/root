using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for OutSchedule
/// </summary>
public class OutSchedule
{
	public OutSchedule()
	{
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
	}

    public DataSet GetRoomName(string sz_reffering_company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("sp_get_room", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_reffering_id", sz_reffering_company_id);
            
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetRoomAppointment(string sz_reffering_company_id, string appointmentDate,string interval,string UserID, string companyID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("sp_get_room_appointments", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@dt_event_date", appointmentDate);
            comm.Parameters.AddWithValue("@sz_interval", interval);
            comm.Parameters.AddWithValue("@sz_user_id", UserID);
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_refering_company_id", sz_reffering_company_id);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetReferingFacilities(string companyID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_TXN_REFERRING_FACILITY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyID);
            comm.Parameters.AddWithValue("@FLAG", "REFERRING_FACILITY_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetPatientDataListNEW(string strcompanyid, string sz_user_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("sp_find_patient_for_calendar", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@FLAG", "LIST");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetPatientData(string strcompanyid, string sz_user_id, string patientID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("sp_find_patient_for_calendar", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@FLAG", "LOAD_PATIENT");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            comm.Parameters.AddWithValue("@sz_patient_id", patientID);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetInsuranceCompany(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_INSURANCE_COMPANY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", company_id);
            comm.Parameters.AddWithValue("@flag", "INSURANCE_LIST");
            
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetState()
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_STATE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@flag", "GET_STATE_LIST");
            
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetReferringFacility(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_TXN_REFERRING_FACILITY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "REFERRING_FACILITY_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetReferringDoctor(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "newGETDOCTORLIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetMedicalOffice(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_OFFICE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "OFFICE_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetCaseType(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_CASE_TYPE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "CASETYPE_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetCaseStatus(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_CASE_STATUS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "CASESTATUS_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetReferringDoctorForOffice(string OfficeId, string companyID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_GET_REF_DOC", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", OfficeId);
            comm.Parameters.AddWithValue("@flag", companyID);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetTransport(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_TRANSPOTATION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "GET_TRANSPORT_LIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetReferringDoctorByOffice(string company_id)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", company_id);
            comm.Parameters.AddWithValue("@flag", "newGETREFFERDOCTORLIST");

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataSet GetRoomAppointmentWithCopiedPatient(string sz_reffering_company_id, string appointmentDate, string interval, string UserID, string companyID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("sp_get_room_appointments_for_copy_patient", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@dt_event_date", appointmentDate);
            comm.Parameters.AddWithValue("@sz_interval", interval);
            comm.Parameters.AddWithValue("@sz_user_id", UserID);
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_refering_company_id", sz_reffering_company_id);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }
}