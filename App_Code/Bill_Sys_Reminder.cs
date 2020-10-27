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
using Reminders;

/// <summary>
/// Summary description for Bill_Sys_Reminder
/// </summary>
namespace Reminders
{
 public class ReminderTypes
 {
  public static string PatientVisit = "PATIENT_VISIT";
  public static string PatientWorkarea = "PATIENT_WORKAREA";
 }
 
 public class ReminderBO
 {
     String strConn;
     SqlConnection conn;
     SqlCommand sqlCmd;
     SqlDataAdapter sqlda;
     SqlDataReader dr;
     DataSet ds;
     SqlTransaction transaction;



     public ReminderBO()
     {
         strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
     }
     public DataSet GetReminderList(string CompanyId )
     {
            ds = new DataSet();
            conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_MST_USERS", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;

             sqlCmd.Parameters.AddWithValue("@ID", CompanyId);

             sqlCmd.Parameters.AddWithValue("@FLAG", "GETUSERLIST");

             sqlda = new SqlDataAdapter(sqlCmd);

             sqlda.Fill(ds);

         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
            return ds;
        }
      
     public DataSet LoadReminderDetails(string strUserId, DateTime dtCurrent_Date)
     {
        conn = new SqlConnection(strConn);
         try
         {
            conn.Open();
             sqlCmd = new SqlCommand("SP_REMINDER_DISMISS_LOAD", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@s_a_user_id", strUserId);
             sqlCmd.Parameters.AddWithValue("@dt_a_current_date", dtCurrent_Date);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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


     public DataSet SetReminderDetailsAdd(string str_description, string str_assigned_to, string str_assigned_by, string strReminderStatus, DateTime dt_start_date, DateTime dt_end_date, int i_is_recurrence, int i_recurrence_type, int i_occurrence_end_count, int i_day_option, int i_d_day_count, int i_d_every_weekday, int i_w_recur_week_count, int i_w_sunday, int i_w_monday, int i_w_tuesday, int i_w_wednesday, int i_w_thursday, int i_w_friday, int i_w_saturday, int i_month_option, int i_m_day, int i_m_month_count, int i_m_term, int i_m_term_week, int i_m_every_month_count, int i_year_option, int i_y_month, int i_y_day, int i_y_term, int i_y_term_week, int i_y_every_month_count,string sz_Doctorid, string sz_Caseid, string sz_Company_Id, string SZ_SHOW_ON,string sz_reminder_type_id,string sz_reminder_type)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_SET_REMINDER_ADD", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.CommandTimeout = 0;
             sqlCmd.Parameters.AddWithValue("@s_a_description", str_description);
             sqlCmd.Parameters.AddWithValue("@s_a_doctor_id", sz_Doctorid);
             sqlCmd.Parameters.AddWithValue("@s_a_case_id", sz_Caseid);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_to", str_assigned_to);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_by", str_assigned_by);
             sqlCmd.Parameters.AddWithValue("@s_a_reminder_status", strReminderStatus);
             sqlCmd.Parameters.AddWithValue("@dt_a_start_date", dt_start_date);
             sqlCmd.Parameters.AddWithValue("@dt_a_end_date", dt_end_date);
             sqlCmd.Parameters.AddWithValue("@i_a_is_recurrence", i_is_recurrence);
             sqlCmd.Parameters.AddWithValue("@i_a_recurrence_type", i_recurrence_type);
             sqlCmd.Parameters.AddWithValue("@i_a_occurrence_end_count", i_occurrence_end_count);
             sqlCmd.Parameters.AddWithValue("@i_day_option", i_day_option);
             sqlCmd.Parameters.AddWithValue("@i_a_d_day_count", i_d_day_count);
             sqlCmd.Parameters.AddWithValue("@i_a_d_every_weekday", i_d_every_weekday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_recur_week_count", i_w_recur_week_count);
             sqlCmd.Parameters.AddWithValue("@i_a_w_sunday", i_w_sunday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_monday", i_w_monday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_tuesday", i_w_tuesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_wednesday", i_w_wednesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_thursday", i_w_thursday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_friday", i_w_friday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_saturday", i_w_saturday);
             sqlCmd.Parameters.AddWithValue("@i_month_option", i_month_option);
             sqlCmd.Parameters.AddWithValue("@i_a_m_day", i_m_day);
             sqlCmd.Parameters.AddWithValue("@i_a_m_month_count", i_m_month_count);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term", i_m_term);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term_week", i_m_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_m_every_month_count", i_m_every_month_count);
             sqlCmd.Parameters.AddWithValue("@i_year_option", i_year_option);
             sqlCmd.Parameters.AddWithValue("@i_a_y_month", i_y_month);
             sqlCmd.Parameters.AddWithValue("@i_a_y_day", i_y_day);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term", i_y_term);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term_week", i_y_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_y_every_month_count", i_y_every_month_count);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_Company_Id);
             sqlCmd.Parameters.AddWithValue("@sz_show_on", SZ_SHOW_ON);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE_ID", sz_reminder_type_id);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE", sz_reminder_type);
             //sqlCmd.Parameters.AddWithValue("@END_DATE_COUNT", i_date_count);
            // sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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


     public DataSet DismissReminder(int iRecurrence_id, int iReminderID, string strDismissReason)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_DISMISS_REMINDER", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@i_a_recurrence_id", iRecurrence_id);
             sqlCmd.Parameters.AddWithValue("@i_a_reminder_id", iReminderID);
             sqlCmd.Parameters.AddWithValue("@SZ_DISMISS_REASON", strDismissReason);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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
     //For update reminder
     public DataSet getFilterReminderList(string p_szStatusID, string p_szReminderDate, string p_szAssignBy, string p_szAssignTo)
     {
         DataSet objDataSet = new DataSet();
         try
         {
             conn = new SqlConnection(strConn);
             conn.Open();
             sqlCmd = new SqlCommand();
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandText = "SP_LIST_TXN_REMINDER_FILTER";
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Connection = conn;

             if (p_szStatusID != "NA")
             {
                 sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_STATUS_ID", p_szStatusID);
             }

             sqlCmd.Parameters.AddWithValue("@DT_REMINDER_DATE", p_szReminderDate);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_ASSIGN_BY", p_szAssignBy);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_ASSIGN_TO", p_szAssignTo);

             SqlDataAdapter objSqlAdap = new SqlDataAdapter(sqlCmd);
             objSqlAdap.Fill(objDataSet);
             return objDataSet;
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
         return objDataSet;
     }
     //For update reminder
     public DataSet getAssignReminderList(string p_szUserID)
     {
         DataSet objDataSet = new DataSet();
         try
         {
             conn = new SqlConnection(strConn);
             conn.Open();
             sqlCmd = new SqlCommand();
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandText = "SP_LIST_TXN_REMINDER_ASSIGNED";
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Connection = conn;
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_ASSIGN_TO", p_szUserID);

             SqlDataAdapter objSqlAdap = new SqlDataAdapter(sqlCmd);
             objSqlAdap.Fill(objDataSet);
             return objDataSet;
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
         return objDataSet;
     }
     //For update reminder
     public DataSet getCreatedReminderList(string p_szUserID)
     {
         DataSet objDataSet = new DataSet();
         try
         {
             conn = new SqlConnection(strConn);
             conn.Open();
             sqlCmd = new SqlCommand();
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandText = "SP_LIST_TXN_REMINDER";
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Connection = conn;
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_ASSIGN_BY", p_szUserID);

             SqlDataAdapter objSqlAdap = new SqlDataAdapter(sqlCmd);
             objSqlAdap.Fill(objDataSet);
             return objDataSet;
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
         return objDataSet;
     }
     //For update reminder
     public void UpdateAssignReminder(string p_iID, string p_szStatusID)
     {
         conn = new SqlConnection(strConn);

         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_UPDATE_TXN_REMINDER", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_STATUS_ID", p_szStatusID);
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_ID", p_iID);
             sqlCmd.ExecuteNonQuery();
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
     }
     //For update reminder
     public void RemoveAssignReminder(int iRemindreId)
     {
         conn = new SqlConnection(strConn);

         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_REMOVE_TXN_REMINDER", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_ID", iRemindreId);
             sqlCmd.ExecuteNonQuery();
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally { conn.Close(); }
     }


     public DataSet LoadCheckimDetails(string sz_companyid)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("CHECK_IM_VISITS", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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

     public DataSet IMSetting(string sz_setting_key_id,string sz_companyid)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("IM_VISIT_SETTING", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", sz_setting_key_id);
             sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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
     public DataSet LoadReminderDetailsForCaseDeatils(string strUserId, DateTime dtCurrent_Date, string SzCaseID)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_REMINDER_DISMISS_LOAD_CASE", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@s_a_user_id", strUserId);
             sqlCmd.Parameters.AddWithValue("@dt_a_current_date", dtCurrent_Date);
             sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", SzCaseID);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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
     public DataSet SetReminderDetailsForCase(string str_description, string str_assigned_to, string str_assigned_by, string strReminderStatus, DateTime dt_start_date, DateTime dt_end_date, int i_is_recurrence, int i_recurrence_type, int i_occurrence_end_count, int i_day_option, int i_d_day_count, int i_d_every_weekday, int i_w_recur_week_count, int i_w_sunday, int i_w_monday, int i_w_tuesday, int i_w_wednesday, int i_w_thursday, int i_w_friday, int i_w_saturday, int i_month_option, int i_m_day, int i_m_month_count, int i_m_term, int i_m_term_week, int i_m_every_month_count, int i_year_option, int i_y_month, int i_y_day, int i_y_term, int i_y_term_week, int i_y_every_month_count, string sz_Doctorid, string sz_Caseid, string sz_Company_Id, string sz_Show_On, string sz_Reminder_Type_Id, string sz_Reminder_Type)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_SET_REMINDER_ADD_FOR_CASE", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.CommandTimeout = 0;
             sqlCmd.Parameters.AddWithValue("@s_a_description", str_description);
             sqlCmd.Parameters.AddWithValue("@s_a_doctor_id", sz_Doctorid);
             sqlCmd.Parameters.AddWithValue("@s_a_case_id", sz_Caseid);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_to", str_assigned_to);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_by", str_assigned_by);
             sqlCmd.Parameters.AddWithValue("@s_a_reminder_status", strReminderStatus);
             sqlCmd.Parameters.AddWithValue("@dt_a_start_date", dt_start_date);
             sqlCmd.Parameters.AddWithValue("@dt_a_end_date", dt_end_date);
             sqlCmd.Parameters.AddWithValue("@i_a_is_recurrence", i_is_recurrence);
             sqlCmd.Parameters.AddWithValue("@i_a_recurrence_type", i_recurrence_type);
             sqlCmd.Parameters.AddWithValue("@i_a_occurrence_end_count", i_occurrence_end_count);
             sqlCmd.Parameters.AddWithValue("@i_day_option", i_day_option);
             sqlCmd.Parameters.AddWithValue("@i_a_d_day_count", i_d_day_count);
             sqlCmd.Parameters.AddWithValue("@i_a_d_every_weekday", i_d_every_weekday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_recur_week_count", i_w_recur_week_count);
             sqlCmd.Parameters.AddWithValue("@i_a_w_sunday", i_w_sunday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_monday", i_w_monday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_tuesday", i_w_tuesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_wednesday", i_w_wednesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_thursday", i_w_thursday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_friday", i_w_friday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_saturday", i_w_saturday);
             sqlCmd.Parameters.AddWithValue("@i_month_option", i_month_option);
             sqlCmd.Parameters.AddWithValue("@i_a_m_day", i_m_day);
             sqlCmd.Parameters.AddWithValue("@i_a_m_month_count", i_m_month_count);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term", i_m_term);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term_week", i_m_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_m_every_month_count", i_m_every_month_count);
             sqlCmd.Parameters.AddWithValue("@i_year_option", i_year_option);
             sqlCmd.Parameters.AddWithValue("@i_a_y_month", i_y_month);
             sqlCmd.Parameters.AddWithValue("@i_a_y_day", i_y_day);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term", i_y_term);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term_week", i_y_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_y_every_month_count", i_y_every_month_count);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_Company_Id);
             sqlCmd.Parameters.AddWithValue("@sz_show_on", sz_Show_On);
             sqlCmd.Parameters.AddWithValue("@sz_reminder_type_id", sz_Reminder_Type_Id);
             sqlCmd.Parameters.AddWithValue("@sz_reminder_type", sz_Reminder_Type);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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

     public DataSet LoadReminderDetailsforAdd(string strUserId, DateTime dtCurrent_Date,string sz_company_id)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_ADD_REMINDER_DISMISS_LOAD", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@s_a_user_id", strUserId);
             sqlCmd.Parameters.AddWithValue("@dt_a_current_date", dtCurrent_Date);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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

     public void RemoveAddReminder(string iRemindreId, string sz_company_id)
     {
         conn = new SqlConnection(strConn);

         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("sp_delete_add_reminder", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_ID", iRemindreId);
             sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
             sqlCmd.ExecuteNonQuery();
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally { conn.Close(); }
     }

     public void SetReminderDetailsUpdate(string str_description, string str_assigned_to, string str_assigned_by, string strReminderStatus, DateTime dt_start_date, DateTime dt_end_date, int i_is_recurrence, int i_recurrence_type, int i_occurrence_end_count, int i_day_option, int i_d_day_count, int i_d_every_weekday, int i_w_recur_week_count, int i_w_sunday, int i_w_monday, int i_w_tuesday, int i_w_wednesday, int i_w_thursday, int i_w_friday, int i_w_saturday, int i_month_option, int i_m_day, int i_m_month_count, int i_m_term, int i_m_term_week, int i_m_every_month_count, int i_year_option, int i_y_month, int i_y_day, int i_y_term, int i_y_term_week, int i_y_every_month_count, string sz_Doctorid, string sz_Caseid, string sz_Company_Id, string SZ_SHOW_ON, string sz_reminder_type_id, string sz_reminder_type, string iRemindreId, int i_date_count)
     {
         conn = new SqlConnection(strConn);
         conn.Open();
         transaction = conn.BeginTransaction();
         //ds = new DataSet();
         try
         {

             sqlCmd = new SqlCommand("sp_delete_account_reminder", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Connection = conn;
             sqlCmd.Transaction = transaction;
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_ID", iRemindreId);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_Company_Id);
             sqlCmd.ExecuteNonQuery();

             sqlCmd = new SqlCommand("SP_SET_REMINDER_UPDATE", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Connection = conn;
             sqlCmd.Transaction = transaction;
             sqlCmd.CommandTimeout = 0;
             sqlCmd.Parameters.AddWithValue("@s_a_description", str_description);
             sqlCmd.Parameters.AddWithValue("@s_a_doctor_id", sz_Doctorid);
             sqlCmd.Parameters.AddWithValue("@s_a_case_id", sz_Caseid);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_to", str_assigned_to);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_by", str_assigned_by);
             sqlCmd.Parameters.AddWithValue("@s_a_reminder_status", strReminderStatus);
             sqlCmd.Parameters.AddWithValue("@dt_a_start_date", dt_start_date);
             sqlCmd.Parameters.AddWithValue("@dt_a_end_date", dt_end_date);
             sqlCmd.Parameters.AddWithValue("@i_a_is_recurrence", i_is_recurrence);
             sqlCmd.Parameters.AddWithValue("@i_a_recurrence_type", i_recurrence_type);
             sqlCmd.Parameters.AddWithValue("@i_a_occurrence_end_count", i_occurrence_end_count);
             sqlCmd.Parameters.AddWithValue("@i_day_option", i_day_option);
             sqlCmd.Parameters.AddWithValue("@i_a_d_day_count", i_d_day_count);
             sqlCmd.Parameters.AddWithValue("@i_a_d_every_weekday", i_d_every_weekday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_recur_week_count", i_w_recur_week_count);
             sqlCmd.Parameters.AddWithValue("@i_a_w_sunday", i_w_sunday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_monday", i_w_monday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_tuesday", i_w_tuesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_wednesday", i_w_wednesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_thursday", i_w_thursday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_friday", i_w_friday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_saturday", i_w_saturday);
             sqlCmd.Parameters.AddWithValue("@i_month_option", i_month_option);
             sqlCmd.Parameters.AddWithValue("@i_a_m_day", i_m_day);
             sqlCmd.Parameters.AddWithValue("@i_a_m_month_count", i_m_month_count);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term", i_m_term);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term_week", i_m_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_m_every_month_count", i_m_every_month_count);
             sqlCmd.Parameters.AddWithValue("@i_year_option", i_year_option);
             sqlCmd.Parameters.AddWithValue("@i_a_y_month", i_y_month);
             sqlCmd.Parameters.AddWithValue("@i_a_y_day", i_y_day);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term", i_y_term);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term_week", i_y_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_y_every_month_count", i_y_every_month_count);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_Company_Id);
             sqlCmd.Parameters.AddWithValue("@sz_show_on", SZ_SHOW_ON);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE_ID", sz_reminder_type_id);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE", sz_reminder_type);
             sqlCmd.Parameters.AddWithValue("@END_DATE_COUNT", i_date_count);
             //sqlCmd.Parameters.AddWithValue("@I_REMINDER_ID", SZ_REMINDER_ID);
             //sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
             sqlCmd.ExecuteNonQuery();
             //sqlda = new SqlDataAdapter(sqlCmd);
            
             //sqlda.Fill(ds);
             //return ds;
             transaction.Commit();
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                transaction.Rollback();
            }
            finally
         {
             if (conn.State == ConnectionState.Open)
             {
                 conn.Close();
             }
         }
         //return ds;
     }

     public DataSet SetReminderDetailsAddaccount(string str_description, string str_assigned_to, string str_assigned_by, string strReminderStatus, DateTime dt_start_date, DateTime dt_end_date, int i_is_recurrence, int i_recurrence_type, int i_occurrence_end_count, int i_day_option, int i_d_day_count, int i_d_every_weekday, int i_w_recur_week_count, int i_w_sunday, int i_w_monday, int i_w_tuesday, int i_w_wednesday, int i_w_thursday, int i_w_friday, int i_w_saturday, int i_month_option, int i_m_day, int i_m_month_count, int i_m_term, int i_m_term_week, int i_m_every_month_count, int i_year_option, int i_y_month, int i_y_day, int i_y_term, int i_y_term_week, int i_y_every_month_count, string sz_Doctorid, string sz_Caseid, string sz_Company_Id, string SZ_SHOW_ON, string sz_reminder_type_id, string sz_reminder_type,int i_date_count)
     {
         conn = new SqlConnection(strConn);
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("SP_SET_REMINDER_ADD_FOR_ACCOUNT", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.CommandTimeout = 0;
             sqlCmd.Parameters.AddWithValue("@s_a_description", str_description);
             sqlCmd.Parameters.AddWithValue("@s_a_doctor_id", sz_Doctorid);
             sqlCmd.Parameters.AddWithValue("@s_a_case_id", sz_Caseid);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_to", str_assigned_to);
             sqlCmd.Parameters.AddWithValue("@s_a_assigned_by", str_assigned_by);
             sqlCmd.Parameters.AddWithValue("@s_a_reminder_status", strReminderStatus);
             sqlCmd.Parameters.AddWithValue("@dt_a_start_date", dt_start_date);
             sqlCmd.Parameters.AddWithValue("@dt_a_end_date", dt_end_date);
             sqlCmd.Parameters.AddWithValue("@i_a_is_recurrence", i_is_recurrence);
             sqlCmd.Parameters.AddWithValue("@i_a_recurrence_type", i_recurrence_type);
             sqlCmd.Parameters.AddWithValue("@i_a_occurrence_end_count", i_occurrence_end_count);
             sqlCmd.Parameters.AddWithValue("@i_day_option", i_day_option);
             sqlCmd.Parameters.AddWithValue("@i_a_d_day_count", i_d_day_count);
             sqlCmd.Parameters.AddWithValue("@i_a_d_every_weekday", i_d_every_weekday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_recur_week_count", i_w_recur_week_count);
             sqlCmd.Parameters.AddWithValue("@i_a_w_sunday", i_w_sunday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_monday", i_w_monday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_tuesday", i_w_tuesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_wednesday", i_w_wednesday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_thursday", i_w_thursday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_friday", i_w_friday);
             sqlCmd.Parameters.AddWithValue("@i_a_w_saturday", i_w_saturday);
             sqlCmd.Parameters.AddWithValue("@i_month_option", i_month_option);
             sqlCmd.Parameters.AddWithValue("@i_a_m_day", i_m_day);
             sqlCmd.Parameters.AddWithValue("@i_a_m_month_count", i_m_month_count);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term", i_m_term);
             sqlCmd.Parameters.AddWithValue("@i_a_m_term_week", i_m_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_m_every_month_count", i_m_every_month_count);
             sqlCmd.Parameters.AddWithValue("@i_year_option", i_year_option);
             sqlCmd.Parameters.AddWithValue("@i_a_y_month", i_y_month);
             sqlCmd.Parameters.AddWithValue("@i_a_y_day", i_y_day);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term", i_y_term);
             sqlCmd.Parameters.AddWithValue("@i_a_y_term_week", i_y_term_week);
             sqlCmd.Parameters.AddWithValue("@i_a_y_every_month_count", i_y_every_month_count);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_Company_Id);
             sqlCmd.Parameters.AddWithValue("@sz_show_on", SZ_SHOW_ON);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE_ID", sz_reminder_type_id);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE", sz_reminder_type);
             sqlCmd.Parameters.AddWithValue("@END_DATE_COUNT", i_date_count);
             // sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
             sqlda = new SqlDataAdapter(sqlCmd);
             ds = new DataSet();
             sqlda.Fill(ds);
             return ds;
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

        public DataSet GetAddReminderList(string CompanyId)
        {
          
            ds = new DataSet();
            conn = new SqlConnection(strConn);
            try
            {
                conn.Open();
                sqlCmd = new SqlCommand("SP_GET_USER_LIST_REMINDER", conn);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ID", CompanyId);
                sqlCmd.Parameters.AddWithValue("@FLAG", "GETUSERLIST");
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally { conn.Close(); }
            return ds;
        }

     public string AddReminderType(string sz_company_id, string sz_reminder_type, string sz_user_id)
     {
         conn = new SqlConnection(strConn);
         string results = "";
         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("sp_add_reminder_type", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE", sz_reminder_type);
             sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
             dr = sqlCmd.ExecuteReader();
             while (dr.Read())
             {
                 results = Convert.ToString(dr[0]);
             }
             return results;
         }
         catch (Exception ex)
         {
             throw ex;
         }
         finally { conn.Close(); }
     }

     public void RemoveReminderType(string iremindertypeid, string sz_company_id,string sz_user_id)
     {
         conn = new SqlConnection(strConn);

         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("sp_delete_reminder_type", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_TYPE_ID", iremindertypeid);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
             sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
             sqlCmd.ExecuteNonQuery();
         }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally { conn.Close(); }
     }

     public void UpdateReminderType(string sz_reminder_type, string sz_company_id, string sz_user_id, string sz_reminder_type_id)
     {
         conn = new SqlConnection(strConn);

         try
         {
             conn.Open();
             sqlCmd = new SqlCommand("sp_update_reminder_type", conn);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
             sqlCmd.CommandType = CommandType.StoredProcedure;
             sqlCmd.Parameters.AddWithValue("@SZ_REMINDER_TYPE", sz_reminder_type);
             sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
             sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
             sqlCmd.Parameters.AddWithValue("@I_REMINDER_TYPE_ID", sz_reminder_type_id);
             sqlCmd.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
         finally { conn.Close(); }
     }
   
        //  public void SetReminderDetailsAdd(ArrayList ArrLst)
    //{
    //    conn = new SqlConnection(strConn);
    //    try
    //    {
    //         ReminderDAO Reminderobj = null;
    //         Reminderobj = (ReminderDAO)ArrLst[0];
    //        conn.Open();
    //        sqlCmd = new SqlCommand("", conn);
    //        sqlCmd.CommandType = CommandType.StoredProcedure;

    //        sqlCmd.Parameters.AddWithValue("@sz_description", Reminderobj.sz_description);
    //        sqlCmd.Parameters.AddWithValue("@sz_assigned_to", Reminderobj.sz_assigned_to);
    //        sqlCmd.Parameters.AddWithValue("@sz_assigned_by", Reminderobj.sz_assigned_by);
    //        sqlCmd.Parameters.AddWithValue("@sz_reminder_status", Reminderobj.sz_reminder_status);
    //        sqlCmd.Parameters.AddWithValue("@sz_start_date", Reminderobj.sz_start_date);
    //        sqlCmd.Parameters.AddWithValue("@sz_end_date", Reminderobj.sz_end_date);
    //        sqlCmd.Parameters.AddWithValue("@sz_is_recurrence ", Reminderobj.sz_is_recurrence);
    //        sqlCmd.Parameters.AddWithValue("@sz_recurrence_type", Reminderobj.sz_recurrence_type);
    //        sqlCmd.Parameters.AddWithValue("@sz_occurrence_end_count ", Reminderobj.sz_occurrence_end_count);
    //        sqlCmd.Parameters.AddWithValue("@sz_day_option", Reminderobj.sz_day_option);
    //        sqlCmd.Parameters.AddWithValue("@sz_d_day_count", Reminderobj.sz_d_day_count);
    //        sqlCmd.Parameters.AddWithValue("@sz_d_every_weekday", Reminderobj.sz_d_every_weekday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_recur_week_count", Reminderobj.sz_w_recur_week_count);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_sunday", Reminderobj.sz_w_sunday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_monday", Reminderobj.sz_w_monday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_tuesday", Reminderobj.sz_w_tuesday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_wednesday", Reminderobj.sz_w_wednesday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_thursday", Reminderobj.sz_w_thursday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_friday", Reminderobj.sz_w_friday);
    //        sqlCmd.Parameters.AddWithValue("@sz_w_saturday", Reminderobj.sz_w_saturday);
    //        sqlCmd.Parameters.AddWithValue("@sz_month_option", Reminderobj.sz_month_option);
    //        sqlCmd.Parameters.AddWithValue("@sz_m_day", Reminderobj.sz_m_day);
    //        sqlCmd.Parameters.AddWithValue("@sz_m_month_count", Reminderobj.sz_m_month_count);
    //        sqlCmd.Parameters.AddWithValue("@sz_m_term", Reminderobj.sz_m_term);
    //        sqlCmd.Parameters.AddWithValue("@sz_m_term_week", Reminderobj.sz_m_term_week);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_every_month_count", Reminderobj.sz_m_month_count);
    //        sqlCmd.Parameters.AddWithValue("@sz_year_option", Reminderobj.sz_year_option);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_month", Reminderobj.sz_y_month);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_day", Reminderobj.sz_y_day);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_term", Reminderobj.sz_y_term);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_term_week", Reminderobj.sz_y_term_week);
    //        sqlCmd.Parameters.AddWithValue("@sz_y_every_month_count", Reminderobj.sz_y_every_month_count);
    //        sqlCmd.ExecuteNonQuery();
    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open)
    //        {
    //            conn.Close();
    //        }
    //    }
      
    //}
     



 
 }
 
 public class ReminderDAO
 {

     private string _sz_description = "";

     public string sz_description
     {
         get
         {
             return _sz_description;
         }
         set
         {
             _sz_description = value;
         }
     }


     private string _sz_assigned_to = "";

     public string sz_assigned_to
     {
         get
         {
             return _sz_assigned_to;
         }
         set
         {
             _sz_assigned_to = value;
         }
     }

     private string _sz_assigned_by = "";

     public string sz_assigned_by
     {
         get
         {
             return _sz_assigned_by;
         }
         set
         {
             _sz_assigned_by = value;
         }
     }

     private string _sz_reminder_status = "";

     public string sz_reminder_status
     {
         get
         {
             return _sz_reminder_status;
         }
         set
         {
             _sz_reminder_status = value;
         }
     }

     private string _sz_start_date = "";

     public string sz_start_date
     {
         get
         {
             return _sz_start_date;
         }
         set
         {
             _sz_start_date = value;
         }
     }



     private string _sz_end_date = "";

     public string sz_end_date
     {
         get
         {
             return _sz_end_date;
         }
         set
         {
             _sz_end_date = value;
         }
     }




     private string _sz_is_recurrence = "";

     public string sz_is_recurrence 
     {
         get
         {
             return _sz_is_recurrence;
         }
         set
         {
             _sz_is_recurrence = value;
         }
     }


     private string _sz_recurrence_type = "";

     public string sz_recurrence_type
     {
         get
         {
             return _sz_recurrence_type;
         }
         set
         {
             _sz_recurrence_type = value;
         }
     }
     private string _sz_occurrence_end_count = "";

     public string sz_occurrence_end_count 
     {
         get
         {
             return _sz_occurrence_end_count;
         }
         set
         {
             _sz_occurrence_end_count = value;
         }
     }



     private string _sz_day_option = "";

     public string sz_day_option
     {
         get
         {
             return _sz_day_option;
         }
         set
         {
             _sz_day_option = value;
         }
     }

     private string _sz_d_day_count = "";

     public string sz_d_day_count
     {
         get
         {
             return _sz_d_day_count;
         }
         set
         {
             _sz_d_day_count = value;
         }
     }


     private string _sz_d_every_weekday = "";

     public string sz_d_every_weekday
     {
         get
         {
             return _sz_d_every_weekday;
         }
         set
         {
             _sz_d_every_weekday = value;
         }
     }

     private string _sz_w_recur_week_count = "";

     public string sz_w_recur_week_count
     {
         get
         {
             return _sz_w_recur_week_count;
         }
         set
         {
             sz_w_recur_week_count = value;
         }
     }

     private string _sz_w_sunday = "";

     public string sz_w_sunday
     {
         get
         {
             return _sz_w_sunday;
         }
         set
         {
             _sz_w_sunday = value;
         }
     }


     private string _sz_w_monday = "";

     public string sz_w_monday
     {
         get
         {
             return _sz_w_monday;
         }
         set
         {
             _sz_w_monday = value;
         }

     }



     private string _sz_w_tuesday = "";

     public string sz_w_tuesday
     {
         get
         {
             return _sz_w_tuesday;
         }
         set
         {
             _sz_w_tuesday = value;
         }

     }


     private string _sz_w_wednesday = "";

     public string sz_w_wednesday
     {
         get
         {
             return _sz_w_wednesday;
         }
         set
         {
             _sz_w_wednesday = value;
         }
     }

     private string _sz_w_thursday = "";

     public string sz_w_thursday
     {
         get
         {
             return _sz_w_thursday;
         }
         set
         {
             _sz_w_thursday = value;
         }
     }

     private string _sz_w_friday = "";

     public string sz_w_friday
     {
         get
         {
             return _sz_w_friday;
         }
         set
         {
             _sz_w_friday = value;
         }
     }

     private string _sz_w_saturday = "";

     public string sz_w_saturday
     {
         get
         {
             return _sz_w_saturday;
         }
         set
         {
             _sz_w_saturday = value;
         }
     }



     private string _sz_month_option = "";

     public string sz_month_option
     {
         get
         {
             return _sz_month_option;
         }
         set
         {
             _sz_month_option = value;
         }
     }



     private string _sz_m_day = "";

     public string sz_m_day	
     {
         get
         {
             return _sz_m_day;
         }
         set
         {
             _sz_m_day = value;
         }
     }

     
     private string _sz_m_month_count = "";

     public string sz_m_month_count
     {
         get
         {
             return _sz_m_month_count;
         }
         set
         {
             _sz_m_month_count = value;
         }
     }

     private string _sz_m_term = "";

     public string sz_m_term
     {
         get
         {
             return _sz_m_term;
         }
         set
         {
             _sz_m_term = value;
         }
     }


     private string _sz_m_term_week = "";

     public string sz_m_term_week
     {
         get
         {
             return _sz_m_term_week;
         }
         set
         {
             _sz_m_term_week = value;
         }
     }

     private string _sz_m_every_month_count = "";

     public string sz_m_every_month_count
     {
         get
         {
             return _sz_m_every_month_count;
         }
         set
         {
             _sz_m_every_month_count = value;
         }
     }



     private string _sz_year_option = "";

     public string sz_year_option
     {
         get
         {
             return _sz_year_option;
         }
         set
         {
             _sz_year_option = value;
         }
     }

     private string _sz_y_month = "";

     public string sz_y_month
     {
         get
         {
             return _sz_y_month;
         }
         set
         {
             _sz_y_month = value;
         }
     }





     private string _sz_y_day = "";

     public string sz_y_day
     {
         get
         {
             return _sz_y_day;
         }
         set
         {
             _sz_y_day = value;
         }
     }


     private string _sz_y_term = "";

     public string sz_y_term	
     {
         get
         {
             return _sz_y_term;
         }
         set
         {
             _sz_y_term = value;
         }
     }


     private string _sz_y_term_week = "";

     public string sz_y_term_week
     {
         get
         {
             return _sz_y_term_week;
         }
         set
         {
             _sz_y_term_week = value;
         }
     }


     private string _sz_y_every_month_count = "";

     public string sz_y_every_month_count
     {
         get
         {
             return sz_y_every_month_count;
         }
         set
         {
             _sz_y_every_month_count = value;
         }
     }

     private string _SZ_case_id  = "";

     public string  SZ_case_id
     {
         get
         {
             return _SZ_case_id;
         }
         set
         {
             _SZ_case_id = value;
         }
     }



     private string _SZ_doctor_id = "";

     public string SZ_doctor_id
     {
         get
         {
             return _SZ_doctor_id;
         }
         set
         {
             _SZ_doctor_id = value;
         }
     }

 }
   

}
 
