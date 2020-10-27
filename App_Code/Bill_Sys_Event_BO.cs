using System;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;

public class Bill_Sys_Event_BO
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;

    DataSet ds;

    public Bill_Sys_Event_BO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public string SaveEvent(ArrayList arrEventInfo)
    {
        string szResult = "";
        conn = new SqlConnection(strConn);
        conn.Open();
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();

        try
        {

            for (int icount = 0; icount < arrEventInfo.Count; icount++)
            {
                Bill_Sys_Event_DAO objEvent = new Bill_Sys_Event_DAO();

                objEvent = (Bill_Sys_Event_DAO)arrEventInfo[icount];

                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_TXN_CALENDAR_EVENT";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@SZ_CASE_ID ", objEvent.SZ_CASE_ID);
                comm.Parameters.AddWithValue("@DT_EVENT_DATE", objEvent.DT_EVENT_DATE);
                comm.Parameters.AddWithValue("@DT_EVENT_TIME", objEvent.DT_EVENT_TIME);
                comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", objEvent.SZ_EVENT_NOTES);
                comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objEvent.SZ_DOCTOR_ID);
                comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objEvent.SZ_TYPE_CODE_ID);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", objEvent.DT_EVENT_TIME_TYPE);
                comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", objEvent.DT_EVENT_END_TIME);
                comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", objEvent.DT_EVENT_END_TIME_TYPE);
                comm.Parameters.AddWithValue("@FLAG", "ADD");
                comm.Parameters.AddWithValue("@I_STATUS", "0");
                comm.Parameters.AddWithValue("@SZ_USER_ID", objEvent.SZ_USER_ID);
                comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", objEvent.SZ_VISIT_TYPE);
                comm.ExecuteNonQuery();
                int eventID = 0;
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "GET_EVENT_ID";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@SZ_CASE_ID ", objEvent.SZ_CASE_ID);
                comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objEvent.SZ_DOCTOR_ID);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    if (dr[0] != DBNull.Value) { eventID = (int)dr[0]; }
                }
                dr.Close();
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                if (_bill_Sys_Calender.CheckReferralExists(objEvent.SZ_DOCTOR_ID, objEvent.SZ_COMPANY_ID) == true)
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "UPDATE_EVENT_STATUS";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@I_EVENT_ID ", eventID.ToString());
                    comm.Parameters.AddWithValue("@BT_STATUS", "false");

                    comm.Parameters.AddWithValue("@I_STATUS", "0");
                    comm.ExecuteNonQuery();
                }

                string szActiveDesc = "Date : " + objEvent.DT_EVENT_DATE + objEvent.DT_EVENT_TIME + objEvent.DT_EVENT_TIME_TYPE;
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "XML/ActivityNotesXML.xml");
                XmlNodeList nl = doc.SelectNodes("NOTES/" + "APPOINTMENT_ADDED" + "/MESSAGE");
                string strMessage = szActiveDesc + " " + nl.Item(0).InnerText;
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_TXN_NOTES";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;

                comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                comm.Parameters.AddWithValue("@SZ_CASE_ID", objEvent.SZ_CASE_ID);
                comm.Parameters.AddWithValue("@SZ_USER_ID", objEvent.SZ_USER_ID);
                comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                comm.Parameters.AddWithValue("@IS_DENIED", objEvent.IS_DENIED);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                comm.Parameters.AddWithValue("@FLAG", "ADD");
                comm.ExecuteNonQuery();


            }
            transaction.Commit();
            szResult = "success";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return szResult;
    }
    public DataSet GetVisits(string szCasId, string szCompanyId)
    {
        DataSet dsVisits = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            comm = new SqlCommand("SP_GET_SHEDUAL_VISITS_FOR_CASE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID   ", szCasId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);

            SqlDataAdapter sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(dsVisits);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dsVisits;
    }

    public DataSet GetCaledarVisits(string szVisitDate, string szCompanyId, string szProcedure_Id, string szDoctorId, string sz_Patient_name)
    {
        DataSet dsVisits = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            comm = new SqlCommand("SP_GET_VISITS_FOR_DAY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", szVisitDate);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedure_Id);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
            comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", sz_Patient_name);

            SqlDataAdapter sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(dsVisits);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dsVisits;
    }

    public DataSet GetTransportVisits(string szCompanyId, string szCaseid)
    {
        DataSet dsVisits = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            comm = new SqlCommand("sp_get_transport_visits", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseid);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(dsVisits);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dsVisits;
    }

    public DataSet GetDoctorlList(string companyid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_LOGIN_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");

            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
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

    public string SaveDocEvent(ArrayList arrEventInfo)
    {
        string szResult = "";
        conn = new SqlConnection(strConn);
        conn.Open();
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            for (int icount = 0; icount < arrEventInfo.Count; icount++)
            {
                Bill_Sys_Event_DAO objEvent = new Bill_Sys_Event_DAO();

                objEvent = (Bill_Sys_Event_DAO)arrEventInfo[icount];

                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_ADD_DOCTOR_VISIT";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@sz_case_id", objEvent.SZ_CASE_ID);
                comm.Parameters.AddWithValue("@dt_event_date", objEvent.DT_EVENT_DATE);
                comm.Parameters.AddWithValue("@dt_event_time", objEvent.DT_EVENT_TIME);
                comm.Parameters.AddWithValue("@sz_event_notes", objEvent.SZ_EVENT_NOTES);
                comm.Parameters.AddWithValue("@sz_doctor_id", objEvent.SZ_DOCTOR_ID);
                comm.Parameters.AddWithValue("@sz_type_code_id", objEvent.SZ_TYPE_CODE_ID);
                comm.Parameters.AddWithValue("@sz_company_id", objEvent.SZ_COMPANY_ID);
                comm.Parameters.AddWithValue("@dt_event_time_type", objEvent.DT_EVENT_TIME_TYPE);
                comm.Parameters.AddWithValue("@dt_event_end_time", objEvent.DT_EVENT_END_TIME);
                comm.Parameters.AddWithValue("@dt_event_end_time_type", objEvent.DT_EVENT_END_TIME_TYPE);
                comm.Parameters.AddWithValue("@SZ_BILLER_ID", objEvent.SZ_BILLER_ID);
                comm.Parameters.AddWithValue("@sz_user_id", objEvent.SZ_USER_ID);
                SqlDataReader dr = comm.ExecuteReader();
                string szEventID = "";
                while (dr.Read())
                {
                    szEventID = dr[0].ToString();
                }
                dr.Close();
                if (szEventID != "")
                {
                    DataSet dsInfo = new DataSet();
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "SP_GET_PATIENT_DETAILS_FOR_DOC_VISIT";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@SZ_CASE_ID", objEvent.SZ_CASE_ID);
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dsInfo);

                    if (dsInfo.Tables.Count > 0)
                    {
                        if (dsInfo.Tables[0].Rows.Count > 0)
                        {
                            string szCaseNo = dsInfo.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
                            string szPatientName = dsInfo.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                            string szInsName = dsInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                            string szDOA = dsInfo.Tables[0].Rows[0]["DT_ACCIDENT"].ToString();
                            string szClaimNo = dsInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                            string szPatientFirstName = dsInfo.Tables[0].Rows[0]["FIRST_NAME"].ToString();
                            string szPatientLastName = dsInfo.Tables[0].Rows[0]["LAST_NAME"].ToString();
                            string szPatientId = dsInfo.Tables[0].Rows[0]["SZ_PATIENT_ID"].ToString();

                            if (objEvent.SZ_GROUP_CODE == "PT")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_MST_PT_NOTES";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", szPatientName);
                                comm.Parameters.AddWithValue("@SZ_CASE_NO", szCaseNo);
                                comm.Parameters.AddWithValue("@DT_DATE_OF_ACCIDENT", szDOA);
                                comm.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY", szInsName);
                                comm.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimNo);
                                comm.Parameters.AddWithValue("@DT_DATE", objEvent.DT_EVENT_DATE);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                            }
                            else if (objEvent.SZ_GROUP_CODE == "CH")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_MST_CH_NOTES";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", szPatientName);
                                comm.Parameters.AddWithValue("@SZ_CASE_NO", szCaseNo);
                                comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", objEvent.SZ_DOCTOR_NAME);
                                comm.Parameters.AddWithValue("@DT_DATE", objEvent.DT_EVENT_DATE);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                            }
                            else if (objEvent.SZ_GROUP_CODE == "AC")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_ACCU_FOLLOWUP";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", szPatientLastName);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", szPatientFirstName);
                                comm.Parameters.AddWithValue("@DT_DOA", szDOA);
                                comm.Parameters.AddWithValue("@DT_CURRENT_DATE", objEvent.DT_EVENT_DATE);

                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@SZ_CASE_ID", objEvent.SZ_CASE_ID);

                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                            }
                            else if (objEvent.SZ_GROUP_CODE == "SYN")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_SYN_FOLLOWUP";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", szPatientLastName);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", szPatientFirstName);
                                comm.Parameters.AddWithValue("@DT_DOA", szDOA);
                                comm.Parameters.AddWithValue("@DT_CURRENT_DATE", objEvent.DT_EVENT_DATE);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();
                            }
                            else if (objEvent.SZ_GROUP_CODE == "LMT")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_MST_LMT";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");

                            }

                            else if (objEvent.SZ_GROUP_CODE == "WB" || objEvent.SZ_GROUP_CODE == "AQU")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_MST_WB_NOTES";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", szPatientName);
                                comm.Parameters.AddWithValue("@SZ_CASE_NO", szCaseNo);
                                comm.Parameters.AddWithValue("@TREATMENT_DATE", objEvent.DT_EVENT_DATE);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                            }
                            else if (objEvent.SZ_GROUP_CODE == "IM")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_SAVE_MST_IM_FOLLOWING";
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
                                comm.Parameters.AddWithValue("@SZ_CASE_ID", objEvent.SZ_CASE_ID);
                                comm.Parameters.AddWithValue("@SZ_CREATED_USER_ID", objEvent.SZ_USER_ID);
                                comm.Parameters.AddWithValue("@SZ_UPDATED_USER_ID", objEvent.SZ_USER_ID);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientId);
                                comm.Parameters.AddWithValue("@DT_EVENT_DATE", objEvent.DT_EVENT_DATE);
                                comm.ExecuteNonQuery();

                            }
                        }
                    }


                }

            }

            transaction.Commit();
            szResult = "success";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally { conn.Close(); }
        return szResult;

    }

    public void UpdateRescheduledoctorvisits(string szeventid, string szeventsnotes, string dt_event_date, string sz_groupcode, string time, string endtime, string endHr, string timetype)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_RESCHEDULE_DOCTOR_VISITS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", szeventid);
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", szeventsnotes);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", dt_event_date);
            comm.Parameters.AddWithValue("@SZ_GROUP_CODE", sz_groupcode);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", time);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", endtime);
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", endHr);
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", timetype);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public string UpdateVisitTime(ArrayList arrevent)
    {
        conn = new SqlConnection(strConn);
        conn.Open();
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        string szResult = "";
        try
        {

            for (int icount = 0; icount < arrevent.Count; icount++)
            {
                Bill_Sys_Event_DAO objEvent = new Bill_Sys_Event_DAO();

                objEvent = (Bill_Sys_Event_DAO)arrevent[icount];

                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_CHANGE_EVENT_TIME";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@I_EVENT_ID", objEvent.I_EVENT_ID);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEvent.SZ_COMPANY_ID);
                comm.Parameters.AddWithValue("@DT_EVENT_TIME", objEvent.DT_EVENT_TIME);
                comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", objEvent.DT_EVENT_TIME_TYPE);
                comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", objEvent.DT_EVENT_END_TIME);
                comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", objEvent.DT_EVENT_END_TIME_TYPE);
                comm.ExecuteNonQuery();

            }

            transaction.Commit();
            szResult = "success";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally { conn.Close(); }
        return szResult;
    }

    public DataSet GetDoctorSpecialtyVisits(string companyid, string dteventfromdate, string dteventtodate, string sz_pro_group_id, string szdoctor_id, string flag)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_SCHEDUAL_VISIT_COUNTS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@DT_EVENT_FROM_DATE", dteventfromdate);
            comm.Parameters.AddWithValue("@DT_EVENT_TO_DATE", dteventtodate);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_pro_group_id);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", szdoctor_id);
            comm.Parameters.AddWithValue("@FLAG", flag);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
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

    public DataSet GetVisitByType(ArrayList arr)
    {
        try
        {
            conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            comm = new SqlCommand("proc_get_visit_by_type", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.Add("@I_STATUS ", arr[0].ToString());
            comm.Parameters.Add("@SZ_COMPANY_ID", arr[1].ToString());
            comm.Parameters.Add("@SZ_OFFICE_ID", arr[2].ToString());
            comm.Parameters.Add("@SZ_DOCTOR_ID", arr[3].ToString());
            comm.Parameters.Add("@SZ_CASE_TYPE", arr[4].ToString());
            comm.Parameters.Add("@SZ_CASE_STATUS", arr[5].ToString());
            comm.Parameters.Add("@SZ_INSURANCE_ID", arr[6].ToString());
            comm.Parameters.Add("@DT_VISIT_FROM_DATE", arr[7].ToString());
            comm.Parameters.Add("@DT_VISIT_TO_DATE", arr[8].ToString());

            
            SqlDataAdapter da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return ds;
    }
    public DataSet GetUnseenReport(string companyid, string count, string unseendate)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("proc_getnot_visited_patient", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", companyid);
            comm.Parameters.AddWithValue("@i_count", count);
            comm.Parameters.AddWithValue("@Unseen_Date", unseendate);
            comm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return ds;
    }
    public DataSet GetCaseWiserReport(string caseid, string compnayid, string Status)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("proc_get_visit_by_type_by_case", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", compnayid);
            comm.Parameters.AddWithValue("@I_STATUS", Status);
            comm.Parameters.AddWithValue("@sz_case_id", caseid);
            comm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return ds;
    }
}