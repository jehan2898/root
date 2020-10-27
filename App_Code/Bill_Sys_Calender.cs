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
/// Summary description for Bill_Sys_Calender
/// </summary>
public class Bill_Sys_Calender
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_Calender()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataTable GET_Schedule_Visits(ArrayList obj)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("SP_SCHEDULE_VISIT_REPORT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@FROM_DATE", obj[0].ToString());
            comm.Parameters.AddWithValue("@TO_DATE", obj[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[2].ToString());
            if (obj.Count >= 4) 
            {
                if(obj[3].ToString()!="")
                {
                    comm.Parameters.AddWithValue("@SZ_ROOM_ID", obj[3].ToString()); 
                }
            }
            //ADDED LOCATION ID PARAMETER
            if (obj.Count == 5)
            {
                                
                    comm.Parameters.AddWithValue("@SZ_LOCATION_ID", obj[4].ToString());
                 
            }
            //END OF CODE
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        return null;
    }

    public DataTable GET_Visit_Types(String strCompanyID)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("SP_GET_VIST_TYPES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", strCompanyID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return null;
    }

    public DataTable GET_EVENT_DETAIL(string id)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_EVENT_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", id);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        return null;
    }
    
    public DataTable GET_DAY_EVENT(ArrayList obj)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_DAY_EVENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            if (obj[0].ToString() != "") { comm.Parameters.AddWithValue("@SZ_PATIENT_ID", obj[0].ToString()); }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", obj[2].ToString());
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return null;
    }

    public DataTable GET_DAY_SHEDULED_EVENT(ArrayList obj)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_DAY_SHEDULED_EVENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            if (obj[0].ToString() != "") { comm.Parameters.AddWithValue("@SZ_PATIENT_ID", obj[0].ToString()); }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_FROM_DATE", obj[2].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TO_DATE", obj[3].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", obj[4].ToString());
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        return null;
    }

    public string GetSystemSettingValue(ArrayList obj)
    {
        try
        {
            string _value = "";
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_SYS_SETTING_VALUE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY", obj[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[1].ToString());
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] !=DBNull.Value)
                {
                    _value=dr[0].ToString();
                }
            }
            return _value;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        return null;
    }

    public decimal GET_EVENT_PERCENTAGE(ArrayList obj)
    {
        try
        {
            decimal _value=new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("GET_EVENT_PERCENTAGE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", obj[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", obj[2].ToString());

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { _value = Convert.ToDecimal(dr[0].ToString()); }
            }

            return _value;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return 0;
    }

    public DataSet GetReferringProcCodeList(String companyId, String doctorId, string p_szFlag, string p_szCaseID, string p_szEventID)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("Sp_Doctor_Proceudere", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
    public bool CheckVisitForVisitType(String patientId, String doctorId, String visitTypeId, String companyId)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_CHECK_VISIT_FOR_VISIT_TYPE", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientId);
            sqlCmd.Parameters.AddWithValue("@VISIT_TYPE_ID", visitTypeId);
            return bool.Parse(sqlCmd.ExecuteScalar().ToString());
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
        return false;
    }
    public DataSet GetReferringProcCodeList(String companyId, String doctorId)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("Sp_Doctor_Proceudere", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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

    public void SaveEventWithAutoVisitType(ArrayList onjAdd, string userid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_visit_create_auto_visit_type";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_case_id ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@dt_event_date", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@dt_event_time", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@sz_event_notes", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@sz_doctor_id", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@sz_type_code_id", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@dt_event_time_type", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@dt_event_end_time", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@dt_event_end_time_type", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@sz_user_id", userid);
            comm.Parameters.AddWithValue("@sz_patient_id", onjAdd[10].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }
    public string GetCaseIDByPatient(string PatientID)
    {
        conn = new SqlConnection(strConn);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandText = "Select SZ_CASE_ID from MST_CASE_MASTER WHere SZ_PATIENT_ID='" + PatientID + "'";
        comm.CommandType = CommandType.Text;
        comm.Connection = conn;
        object res = comm.ExecuteScalar();
        conn.Close();
        return res.ToString();
    }
    public void SaveEvent(ArrayList onjAdd, string userid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            if (onjAdd.Count>=11) { comm.Parameters.AddWithValue("@I_STATUS", onjAdd[10].ToString()); }
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            if (onjAdd.Count >= 12) { comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", onjAdd[11].ToString()); }
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            //comm.Parameters.AddWithValue("@AppointmentID", onjAdd[12].ToString());
            //comm.Parameters.AddWithValue("@Index", onjAdd[13].ToString());
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void SaveEventFromSchedular(ArrayList onjAdd, string userid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            if (onjAdd.Count >= 11) { comm.Parameters.AddWithValue("@I_STATUS", onjAdd[10].ToString()); }
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            if (onjAdd.Count >= 12) { comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", onjAdd[11].ToString()); }
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            comm.Parameters.AddWithValue("@AppointmentID", onjAdd[12].ToString());
            comm.Parameters.AddWithValue("@Index", onjAdd[13].ToString());
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void UPDATEEvent(ArrayList onjAdd,string userid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[10].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[11].ToString()); 
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", onjAdd[12].ToString());
            if (onjAdd.Count >= 14) { comm.Parameters.AddWithValue("@I_RE_EVENT_ID", onjAdd[13].ToString()); }
            if (onjAdd.Count >= 15) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_DATE", onjAdd[14].ToString()); }
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void UPDATEEventByAppointmentId(ArrayList onjAdd, string userid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_CALENDAR_EVENT_BY_APPOINTMENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[10].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", onjAdd[11].ToString());
            //if (onjAdd.Count >= 14) { comm.Parameters.AddWithValue("@I_RE_EVENT_ID", onjAdd[12].ToString()); }
            //if (onjAdd.Count >= 15) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_DATE", onjAdd[13].ToString()); }
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            comm.Parameters.AddWithValue("@AppointmentID", onjAdd[12].ToString());
            comm.Parameters.AddWithValue("@Index", onjAdd[13].ToString());
            comm.Parameters.AddWithValue("@UpdateAppointmentID", onjAdd[14].ToString());

            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public bool CHECKVISIT_FOR_APPOINTMENT(int AppointmentID, int Index,  String companyId)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("CHECK_VISIT_FOR_APPOINTMENT", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            sqlCmd.Parameters.AddWithValue("@Index", Index);
           
            return bool.Parse(sqlCmd.ExecuteScalar().ToString());
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
        return false;
    }

    public int Save_Event(ArrayList onjAdd,string userid)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@SZ_REFERENCE_ID", onjAdd[10].ToString());
            comm.Parameters.AddWithValue("@BT_STATUS", onjAdd[11].ToString());

            // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin       
                         
            comm.Parameters.AddWithValue("@BT_TRANSPORTATION", onjAdd[12]);
            //========================================================================================================
            // 15 April 2010 add coloumn in txn_calender_event table and save value in that table -- sachin
            comm.Parameters.AddWithValue("@I_TRANSPORTATION_COMPANY", onjAdd[13]);
            //========================================================================================================
            if (onjAdd.Count > 14)
            {
                comm.Parameters.AddWithValue("@SZ_OFFICE_ID", onjAdd[14].ToString()); 
            }

            //if (onjAdd.Count >= 13) { comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[13].ToString()); }
            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
             parmReturnValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(parmReturnValue);
            comm.Parameters.AddWithValue("@FLAG", "ADD2");
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);

            comm.ExecuteNonQuery();
             returnvalue = (int)comm.Parameters["@RETURN"].Value;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }



    public int Update_Doctor_Id(ArrayList onjAdd)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@FLAG", "UPDATEDOCTORID");

            comm.ExecuteNonQuery();
       }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }


    public int Update_Office_Id(ArrayList onjAdd)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_OFFICE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", "UPDATEOFFICEID");

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }

    public void UPDATE_Event_Status(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "UPDATE_EVENT_STATUS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@BT_STATUS", onjAdd[1].ToString());
           // comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER",onjAdd[2].ToString());
            if (onjAdd.Count >= 3) { comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString()); }
            if (onjAdd.Count >= 4) { comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", onjAdd[3].ToString()); }
            if (onjAdd.Count >= 5) { comm.Parameters.AddWithValue("@DT_BILL_DATE", onjAdd[4].ToString()); }
            if (onjAdd.Count >= 6) { comm.Parameters.AddWithValue("@DT_BILL_DATE", onjAdd[5].ToString()); }
            comm.ExecuteNonQuery();
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        
    }


    public void UPDATE_EventNotes_Status(ArrayList onjAdd,string Userid) // Add sz_event_notes parameter for update notes filed
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "UPDATE_EVENT_STATUS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@BT_STATUS", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ID", Userid);
            // comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER",onjAdd[2].ToString());
            //if (onjAdd.Count >= 3) { comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString()); }
            //if (onjAdd.Count >= 4) { comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", onjAdd[3].ToString()); }
            //if (onjAdd.Count >= 5) { comm.Parameters.AddWithValue("@DT_BILL_DATE", onjAdd[4].ToString()); }
            //if (onjAdd.Count >= 6) { comm.Parameters.AddWithValue("@DT_BILL_DATE", onjAdd[5].ToString()); }
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }


    public void UPDATE_Event1(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_STATUS ", "2");
          
            comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER",onjAdd[1].ToString());
           
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public int UPDATE_Event(ArrayList onjAdd)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@SZ_REFERENCE_ID", onjAdd[10].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[11].ToString());
            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
            parmReturnValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(parmReturnValue);
            

            comm.ExecuteNonQuery();
            returnvalue = (int)comm.Parameters["@RETURN"].Value;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }
    
    public void Save_Event_RefferPrcedure(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            if (onjAdd.Count>=4 && onjAdd[3].ToString()!=""){comm.Parameters.AddWithValue("@DT_RESCHEDULE_DATE", onjAdd[3].ToString());}
            if (onjAdd.Count>=5){comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME", onjAdd[4].ToString());}
            if (onjAdd.Count>=6){comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME_TYPE", onjAdd[5].ToString());}
            if (onjAdd.Count>=7){comm.Parameters.AddWithValue("@I_RESCHEDULE_ID", onjAdd[6].ToString());}
            if (onjAdd.Count >= 8) { comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[7].ToString()); }
            if (onjAdd.Count >= 9) { comm.Parameters.AddWithValue("@SZ_NOTES", onjAdd[8].ToString()); }
			
            comm.ExecuteNonQuery();
          }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public void Save_Event_OtherVType(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_NOTES", onjAdd[4].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void Update_ReShedule_Info(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_REFERRAL_PROC_CODE_INFO";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_NOTES", onjAdd[4].ToString());
            if (onjAdd[5].ToString() != "") { comm.Parameters.AddWithValue("@DT_RESHEDULE_DATE", onjAdd[5].ToString()); }
            if (onjAdd[6].ToString()!="") {comm.Parameters.AddWithValue("@SZ_RESHEDULE_TIME", onjAdd[6].ToString());}
            if (onjAdd[7].ToString() != "") { comm.Parameters.AddWithValue("@SZ_RESHEDULE_TIME_TYPE", onjAdd[7].ToString()); }
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public void UpdateProcedure(string p_szCompanyID, string p_szDoctorID, string p_szProcCode, bool p_btIsComplete, string p_szEventID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_ASSOCIATE_PROCEDURE_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID ", p_szCompanyID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", p_szProcCode);
            comm.Parameters.AddWithValue("@BT_COMPLETE", p_btIsComplete);
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }



    

    public ArrayList getDayDetails(DateTime _date, int _hour, string caseid, string companyid, string flag)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL; 
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_DAY_CALENDER";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@CURRENT_DATE", _date);
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", caseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_HOUR", _hour);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                day_DETAIL = new Day_DETAIL();
                if (dr[0]!=DBNull.Value){day_DETAIL.EVENT_PATIENT=Convert.ToString(dr[0]);};
                if (dr[1] != DBNull.Value) { day_DETAIL.EVENT_DOCTOR = Convert.ToString(dr[1]); };
                if (dr[2] != DBNull.Value) { day_DETAIL.EVENT_TIME= Convert.ToString(dr[2]); };
                if (dr[3] != DBNull.Value) { day_DETAIL.EVENT_NOTES= Convert.ToString(dr[3]); };
                if (dr[4] != DBNull.Value) { day_DETAIL.EVENT_COUNT = Convert.ToInt32(dr[4]); };
                _return.Add(day_DETAIL);
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public ArrayList getMonthDetails(DateTime _date, int _day,string caseid,string companyid,string flag)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_MONTH_CALENDER";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@CURRENT_DATE", _date);
            comm.Parameters.AddWithValue("@SZ_CASE_ID ",caseid );
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@I_DAY", _day);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                day_DETAIL = new Day_DETAIL();
                if (dr[0] != DBNull.Value) { day_DETAIL.EVENT_PATIENT = Convert.ToString(dr[0]); };
                if (dr[1] != DBNull.Value) { day_DETAIL.EVENT_DOCTOR = Convert.ToString(dr[1]); };
                if (dr[2] != DBNull.Value) { day_DETAIL.EVENT_TIME = Convert.ToString(dr[2]); };
                if (dr[3] != DBNull.Value) { day_DETAIL.EVENT_NOTES = Convert.ToString(dr[3]); };
                if (dr[4] != DBNull.Value) { day_DETAIL.EVENT_COUNT = Convert.ToInt32(dr[4]); };
                _return.Add(day_DETAIL);
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;

    }

    public DataSet GetEventDetailList(DateTime date, string flag,string caseid,string companyid,int time)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_EVENT_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@DT_DATE", date);
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", caseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (time > 0)
            {
                comm.Parameters.AddWithValue("@DT_TIME", time);
            }          

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetEventDetailList(DateTime date, string flag, string caseid, string companyid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_EVENT_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@DT_DATE", date);
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", caseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);


            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetDoctorlList(string companyid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
        finally { conn.Close(); }
    }

    public bool CheckReferralExists(string p_szDoctorID, string p_szCompanyID)
    {
        bool szFlag = false;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CHECK_REFERRAL_EXISTS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szDoctorID != "") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID); }
            if (p_szCompanyID != "") { comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        szFlag = true;
                    }
                }

            }

            return szFlag;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 

        }
        finally { conn.Close(); }
        return false;
    }

    public int GetEventID(ArrayList onjAdd)
    {
        SqlDataReader dr;
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_EVENT_ID";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());            
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[2].ToString());
            dr =comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { returnvalue = (int)dr[0]; }
            }        
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }

    public DataTable GET_DoctorSpeciality(String strDoctorID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("SP_Doctor_Speciality", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@DoctorID", strDoctorID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        return null;
    }

    public DataSet GetReferringDoctorProcedureCodeList(String companyId, String doctorId)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("Sp_Doctor_ProcedureforVisits", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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


    public DataSet GetAssociatedProcCodeList(String companyId, String doctorId, string p_szFlag, string p_szCaseID, string p_szEventID)
    {
        conn = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_ASSOCIATE_PROCEDURE_CODE", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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


    public void Update_Event_RefferPrcedure(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            if (onjAdd.Count >= 4) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_DATE", onjAdd[3].ToString()); }
            if (onjAdd.Count >= 5) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME", onjAdd[4].ToString()); }
            if (onjAdd.Count >= 6) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME_TYPE", onjAdd[5].ToString()); }
            if (onjAdd.Count >= 7) { comm.Parameters.AddWithValue("@I_RESCHEDULE_ID", onjAdd[6].ToString()); }

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void Update_Event_RefferPrcedure(ArrayList onjAdd,string p_szOldEventID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@I_OLD_EVENT_ID", p_szOldEventID);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public int UPDATE_Event_Referral(ArrayList onjAdd, string userid)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPDATE_TXN_CALENDAR_EVENT_REFERAL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            comm.Parameters.AddWithValue("@SZ_REFERENCE_ID", onjAdd[10].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[11].ToString()); 
            // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin

            comm.Parameters.AddWithValue("@BT_TRANSPORTATION", onjAdd[12].ToString());
            //===============================================
            //15 April 2010 add transport company field --- sachin
            comm.Parameters.AddWithValue("@I_TRANSPORTATION_COMPANY", onjAdd[13]);
            //=======================================================
            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
            parmReturnValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(parmReturnValue);
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);

            comm.ExecuteNonQuery();
            returnvalue = (int)comm.Parameters["@RETURN"].Value;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }

    public void Delete_Event_RefferPrcedure(int eventId)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_DELETE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void Delete_SchedhuledEvent(int eventId)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_DELETE_CHECKIN_LIST";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void Delete_PatientOfficeID(string sz_patient_id, string sz_office_id, int eventId, string sz_CompanyId)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_DELETE_PATIENT_OFFICE_ID";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", sz_patient_id);
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_office_id);
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public void savePatientForReferringFacility(ArrayList objAL)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_PATIENT_DATA_ENTRY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[2].ToString());
            if (objAL[3].ToString() != "")
                comm.Parameters.AddWithValue("@I_PATIENT_AGE", objAL[3].ToString());
            if (objAL[4].ToString() != "")
                comm.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", objAL[4].ToString());
            if (objAL[5].ToString() != "")
                comm.Parameters.AddWithValue("@SZ_PATIENT_CITY", objAL[5].ToString());
            if (objAL[6].ToString() != "")
                comm.Parameters.AddWithValue("@SZ_PATIENT_PHONE", objAL[6].ToString());
            if (objAL[7].ToString() != "" && objAL[7].ToString() != "NA")
                comm.Parameters.AddWithValue("@SZ_PATIENT_STATE_ID", objAL[7].ToString());
            
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[8].ToString());

            if (objAL[9].ToString() != "")
                comm.Parameters.AddWithValue("@MI", objAL[9].ToString());

            if (objAL[10].ToString() != "")
                comm.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", objAL[10].ToString());

            if (objAL[11].ToString() != "")
                comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[11].ToString());

            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public string GetOpenCaseStatus(String szCompanyID)
    {
        String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        string caseStatusID = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + szCompanyID + "'", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                caseStatusID = Convert.ToString(dr[0].ToString());
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return caseStatusID;
    }
    public void UPDATE_TransportationCompany_Event(ArrayList onjAdd)
    {
        //int returnvalue = 0;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            //ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@BT_TRANSPORTATION", onjAdd[1]);
            comm.Parameters.AddWithValue("@I_TRANSPORTATION_COMPANY", onjAdd[2]);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE_TRANSPORT_COMPANY");
            //SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
            //parmReturnValue.Direction = ParameterDirection.ReturnValue;
            //comm.Parameters.Add(parmReturnValue);


            comm.ExecuteNonQuery();
            //returnvalue = (int)comm.Parameters["@RETURN"].Value;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        //return returnvalue;
    }


    // Modified By BowandBaan for Include Delete option - Modification Starts
    public int Delete_Event(int eventId,string p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_DELETE_EVENTS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            int iCount = comm.ExecuteNonQuery();
            return iCount;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;
        }
        finally { conn.Close(); }
        return 0;
    }


    public DataTable GET_DAY_SHEDULED_EVENT_FOR_SEARCH_BY_DOCTOR(ArrayList obj)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_DAY_SHEDULED_EVENT_SEARCH_BYDOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            if (obj[0].ToString() != "") { comm.Parameters.AddWithValue("@SZ_PATIENT_ID", obj[0].ToString()); }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[1].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_FROM_DATE", obj[2].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_TO_DATE", obj[3].ToString());
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", obj[4].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj[5].ToString());
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        return null;
    }
    // Modified By BowandBaan for Include Delete option - Modification Ends

    public void Save_Reffering_info(ArrayList objReff)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            Day_DETAIL day_DETAIL;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_save_visit_reff_info";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", objReff[0].ToString());
            if (objReff.Count > 1) { comm.Parameters.AddWithValue("@sz_reffering_doctor_id", objReff[1].ToString()); }
            if (objReff.Count > 2) { comm.Parameters.AddWithValue("@sz_reffering_office_id", objReff[2].ToString()); }
            
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


}

public class DeleteSchedule
{
    public int Index { get; set; }
    public int AppointmentID { get; set; }
}

public class Day_DETAIL
{
    private string _eVENT_TIME = "";
    public string EVENT_TIME
    {
        get
        {
            return _eVENT_TIME;
        }
        set
        {
            _eVENT_TIME = value;
        }
    }

    private string _eVENT_NOTES = "";
    public string EVENT_NOTES
    {
        get
        {
            return _eVENT_NOTES;
        }
        set
        {
            _eVENT_NOTES = value;
        }
    }
    private string _eVENT_PATIENT = "";
    public string EVENT_PATIENT
    {
        get
        {
            return _eVENT_PATIENT;
        }
        set
        {
            _eVENT_PATIENT = value;
        }
    }
    private string _eVENT_DOCTOR = "";
    public string EVENT_DOCTOR
    {
        get
        {
            return _eVENT_DOCTOR;
        }
        set
        {
            _eVENT_DOCTOR = value;
        }
    }
    private int _eVENT_COUNT = 0;
    public int EVENT_COUNT
    {
        get
        {
            return _eVENT_COUNT;
        }
        set
        {
            _eVENT_COUNT = value;
        }
    }
}
