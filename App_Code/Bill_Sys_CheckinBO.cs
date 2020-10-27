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
using log4net;

public class Bill_Sys_CheckinBO
{

    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    SqlDataReader dr;
    
	public Bill_Sys_CheckinBO()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet getDoctorList(String p_szCompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public DataSet getDoctorSpeciality(String p_szCompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_GET_DOCTORSPECIALITY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public DataSet getDoctorList_Acc_Speciality(String p_szCompanyID,String p_szProcedureGroupID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_szProcedureGroupID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST_SPECIALITYWISE");
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public DataSet getCheckinStatus(string p_szCaseID, string p_szCompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            comm = new SqlCommand("SP_GET_CHECKIN_STATUS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public void saveVisits(ArrayList p_objArrayList)
    {
        conn = new SqlConnection(strsqlCon);
        try
        {
            conn.Open();
            #region "Save Visits Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            //comm.Parameters.AddWithValue("@SZ_PROC_CODE", p_objArrayList[0].ToString());
            //comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[1].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public Bill_Sys_CheckBO SaveVisitByDoctor(ArrayList p_objArrayList)
    {
        string sReturn = "";
        string strSuccess = "";
        string strError = "";
        Bill_Sys_CheckBO objReturn = new Bill_Sys_CheckBO();

        
        SqlTransaction tr;
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandTimeout = 0;
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            tr = conn.BeginTransaction();
        try
        {
         
         
            for (int i = 0; i < p_objArrayList.Count; i++)
            {
                Bill_Sys_CheckBO obj = new Bill_Sys_CheckBO();
                obj = (Bill_Sys_CheckBO)p_objArrayList[i];
                DataSet ds = new DataSet();
                string bt_validate = "";
                string evntid = "";
                string ProcId = "";
                string CH_event_Id = "";

                //conn.Open();
                if (obj.SZ_PROCEDURE_GROUP == "AC")
                {
                    //Code for bit check and save the data

                    if (obj.SZ_USER_ID != "")
                    {
                        DataSet Bitds = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GET_BIT_FOR_VALIDATE_AND_SHOW_PREVIOUS_VISIT", conn);
                        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        cmd.CommandTimeout = 0;
                        cmd.Transaction = tr;
                        cmd.Parameters.AddWithValue("@SZ_USER_ID", obj.SZ_USER_ID);
                        cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(Bitds);
                        bt_validate = Bitds.Tables[0].Rows[0]["BT_VALIDATE_AND_SHOW"].ToString().ToLower();
                    }
                    if (bt_validate == "true")
                    {
                        comm = new SqlCommand("SP_GET_PREVIOUS_VISIT_AC_INFO_USING_CASEID", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandTimeout = 0;
                        comm.Transaction = tr;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                        comm.Connection = conn;
                        comm.ExecuteNonQuery();
                        evntid = Convert.ToString(comm.ExecuteScalar());
                        if (evntid != "")
                        {
                            DataTable dsReturn = new DataTable();
                            comm = new SqlCommand("SP_ACCU_FOLLOWUP", conn);
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandTimeout = 0;
                            comm.Transaction = tr;
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@I_EVENT_ID", evntid);
                            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");
                            comm.Connection = conn;
                            comm.ExecuteNonQuery();
                            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
                            objDAdap.Fill(ds);
                            
                            obj.BT_TREATMENT_CODE_97810 = ds.Tables[0].Rows[0]["BT_TREATMENT_CODE_97810"].ToString().ToLower();
                            obj.BT_TREATMENT_CODE_97813 = ds.Tables[0].Rows[0]["BT_TREATMENT_CODE_97813"].ToString().ToLower();
                            obj.BT_TREATMENT_CODE_97811 = ds.Tables[0].Rows[0]["BT_TREATMENT_CODE_97811"].ToString().ToLower();
                            obj.BT_TREATMENT_CODE_97814 = ds.Tables[0].Rows[0]["BT_TREATMENT_CODE_97814"].ToString().ToLower();
                            obj.SZ_DOCTOR_NOTE = ds.Tables[0].Rows[0]["SZ_DOCTOR_NOTE"].ToString();
                            obj.bt_patient_reported = ds.Tables[0].Rows[0]["bt_patient_reported"].ToString();
                            obj.bt_patient_trated = ds.Tables[0].Rows[0]["bt_patient_trated"].ToString();
                            obj.bt_pain_grades = ds.Tables[0].Rows[0]["bt_pain_grades"].ToString();
                            obj.bt_head = ds.Tables[0].Rows[0]["bt_head"].ToString();
                            obj.bt_neck = ds.Tables[0].Rows[0]["bt_neck"].ToString();
                            obj.bt_throcic = ds.Tables[0].Rows[0]["bt_throcic"].ToString();
                            obj.bt_lumber = ds.Tables[0].Rows[0]["bt_lumber"].ToString();
                            obj.bt_rl_sh = ds.Tables[0].Rows[0]["bt_rl_sh"].ToString();
                            obj.bt_rl_wrist = ds.Tables[0].Rows[0]["bt_rl_wrist"].ToString();
                            obj.bt_rl_elow = ds.Tables[0].Rows[0]["bt_rl_elow"].ToString();
                            obj.bt_rl_hil = ds.Tables[0].Rows[0]["bt_rl_hil"].ToString();
                            obj.bt_rl_knee = ds.Tables[0].Rows[0]["bt_rl_knee"].ToString();
                            obj.bt_rl_ankle = ds.Tables[0].Rows[0]["bt_rl_ankle"].ToString();
                            obj.bt_patient_states = ds.Tables[0].Rows[0]["bt_patient_states"].ToString();
                            obj.bt_patient_states_little = ds.Tables[0].Rows[0]["bt_patient_states_little"].ToString();
                            obj.bt_patient_states_much = ds.Tables[0].Rows[0]["bt_patient_states_much"].ToString();
                            obj.bt_patient_tolerated = ds.Tables[0].Rows[0]["bt_patient_tolerated"].ToString();
                            obj.bt_patient_therapy = ds.Tables[0].Rows[0]["bt_patient_therapy"].ToString();
                            obj.sz_doctornote = ds.Tables[0].Rows[0]["sz_doctornote"].ToString();
                        }
                    }
                }
                
                  string str = "";
                    comm = new SqlCommand("SP_CHECK_DOCTOR_VISIT", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandTimeout = 0;
                    comm.Transaction = tr;
                    comm.Parameters.AddWithValue("@SZ_EVENT_DATE", obj.SZ_VISIT_DATE);
                    comm.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                    comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                    comm.CommandType = CommandType.StoredProcedure;
                    
                    dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        str = dr[0].ToString();
                    }
                    dr.Close();

                    if (str == "0")
                    {
                        comm = new SqlCommand("SP_GET_VISIT_TYPE_LIST", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Transaction = tr;
                        comm.Parameters.AddWithValue("@ID", obj.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@FLAG", "GET_FU_VALUE"); //flag
                        dr = comm.ExecuteReader();
                        string szDefaultVisitType = "";
                        while (dr.Read())
                        {
                            szDefaultVisitType = dr[0].ToString();
                        }
                        dr.Close();

                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "sp_add_visit_by_doctor";
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = tr;
                        comm.Parameters.AddWithValue("@SZ_CASE_ID ", obj.SZ_CASE_ID);

                        comm.Parameters.AddWithValue("@DT_EVENT_DATE", obj.SZ_VISIT_DATE);
                        comm.Parameters.AddWithValue("@DT_EVENT_TIME", obj.DT_EVENT_TIME);
                        comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", obj.SZ_EVENT_NOTES);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", obj.SZ_TYPE_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", obj.DT_EVENT_TIME_TYPE);
                        comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", obj.DT_EVENT_END_TIME);
                        comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", obj.DT_EVENT_END_TIME_TYPE);
                        comm.Parameters.AddWithValue("@SZ_VISIT_TYPE", szDefaultVisitType);
                        comm.Parameters.AddWithValue("@SZ_USER_ID", obj.SZ_USER_ID);

                        comm.ExecuteNonQuery();

                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "GET_EVENT_ID";
                        comm.CommandTimeout = 0;
                        comm.Transaction = tr;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("@SZ_CASE_ID ", obj.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                        dr = comm.ExecuteReader();
                        string eventId = "";
                        while (dr.Read())
                        {
                            if (dr[0] != DBNull.Value)
                            {
                                eventId = dr[0].ToString();
                            }
                        }
                        dr.Close();
                        //arrEvent.Add(eventId);

                        if (obj.SZ_PROCEDURE_GROUP == "AC")
                        {
                            int iAcFlag = 0;

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    iAcFlag = 1;
                                    comm = new SqlCommand();
                                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.CommandText = "SP_ACCU_FOLLOWUP";
                                    comm.CommandTimeout = 0;
                                    comm.CommandType = CommandType.StoredProcedure;
                                    comm.Connection = conn;
                                    comm.Transaction = tr;
                                    comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME ", obj.SZ_PATIENT_LAST_NAME);
                                    comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", obj.SZ_PATIENT_FIRST_NAME);
                                    comm.Parameters.AddWithValue("@DT_DOA", obj.DT_DATE_OF_ACCIDENT);
                                    comm.Parameters.AddWithValue("@DT_CURRENT_DATE", obj.SZ_VISIT_DATE);
                                    comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                    comm.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                                    int iBT_TREATMENT_CODE_97810 = 0;
                                    int iBT_TREATMENT_CODE_97813 = 0;
                                    int iBT_TREATMENT_CODE_97811 = 0;
                                    int iBT_TREATMENT_CODE_97814 = 0;
                                    if (obj.BT_TREATMENT_CODE_97810 =="false")
                                    {
                                        iBT_TREATMENT_CODE_97810 = 0;
                                    }
                                    else {
                                        iBT_TREATMENT_CODE_97810 = 1;
                                    }

                                    if (obj.BT_TREATMENT_CODE_97813 == "false")
                                    {
                                        iBT_TREATMENT_CODE_97813 = 0;
                                    }
                                    else
                                    {
                                        iBT_TREATMENT_CODE_97813 = 1;
                                    }

                                    if (obj.BT_TREATMENT_CODE_97811 == "false")
                                    {
                                        iBT_TREATMENT_CODE_97811 = 0;
                                    }
                                    else
                                    {
                                        iBT_TREATMENT_CODE_97811 = 1;
                                    }

                                    if (obj.BT_TREATMENT_CODE_97814 == "false")
                                    {
                                        iBT_TREATMENT_CODE_97814 = 0;
                                    }
                                    else
                                    {
                                        iBT_TREATMENT_CODE_97814 = 1;
                                    }

                                    comm.Parameters.AddWithValue("@BT_TREATMENT_CODE_97810", iBT_TREATMENT_CODE_97810);
                                    comm.Parameters.AddWithValue("@BT_TREATMENT_CODE_97813", iBT_TREATMENT_CODE_97813);
                                    comm.Parameters.AddWithValue("@BT_TREATMENT_CODE_97811", iBT_TREATMENT_CODE_97811);
                                    comm.Parameters.AddWithValue("@BT_TREATMENT_CODE_97814", iBT_TREATMENT_CODE_97814);
                                    comm.Parameters.AddWithValue("@SZ_DOCTOR_NOTE", obj.SZ_DOCTOR_NOTE);
                                    comm.Parameters.AddWithValue("@bt_patient_reported", obj.bt_patient_reported);
                                    comm.Parameters.AddWithValue("@bt_patient_trated", obj.bt_patient_trated);
                                    comm.Parameters.AddWithValue("@bt_pain_grades", obj.bt_pain_grades);
                                    comm.Parameters.AddWithValue("@bt_head", obj.bt_head);
                                    comm.Parameters.AddWithValue("@bt_neck", obj.bt_neck);
                                    comm.Parameters.AddWithValue("@bt_throcic", obj.bt_throcic);
                                    comm.Parameters.AddWithValue("@bt_lumber", obj.bt_lumber);
                                    comm.Parameters.AddWithValue("@bt_rl_sh", obj.bt_rl_sh);
                                    comm.Parameters.AddWithValue("@bt_rl_wrist", obj.bt_rl_wrist);
                                    comm.Parameters.AddWithValue("@bt_rl_elow", obj.bt_rl_elow);
                                    comm.Parameters.AddWithValue("@bt_rl_hil", obj.bt_rl_hil);
                                    comm.Parameters.AddWithValue("@bt_rl_knee", obj.bt_rl_knee);
                                    comm.Parameters.AddWithValue("@bt_rl_ankle", obj.bt_rl_ankle);
                                    comm.Parameters.AddWithValue("@bt_patient_states", obj.bt_patient_states);
                                    comm.Parameters.AddWithValue("@bt_patient_states_little", obj.bt_patient_states_little);
                                    comm.Parameters.AddWithValue("@bt_patient_states_much", obj.bt_patient_states_much);
                                    comm.Parameters.AddWithValue("@bt_patient_tolerated", obj.bt_patient_tolerated);
                                    comm.Parameters.AddWithValue("@bt_patient_therapy", obj.bt_patient_therapy);
                                    comm.Parameters.AddWithValue("@sz_doctornote", obj.sz_doctornote);
                                    comm.Parameters.AddWithValue("@FLAG", "ADD");
                                    comm.ExecuteNonQuery();
                                    
                                    comm = new SqlCommand();
                                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.Transaction = tr;
                                    comm.CommandText = "update MST_ACCU_FOLLOWUP set  SZ_PATIENT_SIGN_PATH='" + obj.SZ_SIGN_PATH + "',bt_pat_sign_success='" + obj.BIT_OF_SIGNPATH + "' where SZ_COMPANY_ID='" + obj.SZ_COMPANY_ID + "' and I_EVENT_ID='" + eventId + "'";
                                    comm.CommandType = CommandType.Text;
                                    comm.Connection = conn;
                                    comm.ExecuteNonQuery();

                                    DataSet ProcIdDs = new DataSet();
                                    comm = new SqlCommand("sp_get_Proccode_id", conn);
                                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.CommandTimeout = 0;
                                    comm.Transaction = tr;
                                    comm.Parameters.AddWithValue("@I_EVENT_ID", evntid);
                                    comm.Parameters.AddWithValue("@flag", "getProcCode");
                                    comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", "");
                                    comm.CommandType = CommandType.StoredProcedure;
                                    comm.ExecuteNonQuery();
                                    SqlDataAdapter sqlda = new SqlDataAdapter(comm);
                                    sqlda.Fill(ProcIdDs);
                                    if (ProcIdDs.Tables.Count > 0)
                                    {
                                        if (ProcIdDs.Tables[0].Rows.Count > 0)
                                        {
                                            for (int c = 0; c < ProcIdDs.Tables[0].Rows.Count; c++)
                                            {
                                                SqlCommand command = new SqlCommand();
                                                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                command.Transaction = tr;
                                                command.CommandText = "sp_insert_proc_code_id";
                                                comm.CommandTimeout = 0;
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Connection = conn;
                                                command.Parameters.AddWithValue("@SZ_PROC_CODE", ProcIdDs.Tables[0].Rows[c]["SZ_PROC_CODE"].ToString());
                                                command.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                                command.Parameters.AddWithValue("@flag", "BT_UPDATE");
                                                command.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    
                                    DataSet dset = new DataSet();
                                    SqlCommand sqlCmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", conn);
                                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.CommandTimeout = 0;
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Transaction = tr;
                                    sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", evntid);
                                    sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                    sqlCmd.Parameters.AddWithValue("@FLAG", "GET");
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.ExecuteNonQuery();
                                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                                    da.Fill(dset);

                                    if (dset.Tables.Count > 0)
                                    {
                                        if (dset.Tables[0].Rows.Count > 0)
                                        {
                                            for (int d = 0; d < dset.Tables[0].Rows.Count; d++)
                                            {
                                                sqlCmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", conn);
                                                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                                sqlCmd.CommandTimeout = 0;
                                                sqlCmd.Transaction = tr;
                                                sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", eventId);
                                                sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                                                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                                sqlCmd.Parameters.AddWithValue("@SZ_COMPLAINT_ID", dset.Tables[0].Rows[d]["i_complaint_id"].ToString());
                                                sqlCmd.Parameters.AddWithValue("@FLAG", "SAVE");
                                                sqlCmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }

                            if (iAcFlag == 0)
                            {

                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_ACCU_FOLLOWUP";
                                comm.CommandTimeout = 0;
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = tr;
                                comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME ", obj.SZ_PATIENT_LAST_NAME);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", obj.SZ_PATIENT_FIRST_NAME);
                                comm.Parameters.AddWithValue("@DT_DOA", obj.DT_DATE_OF_ACCIDENT);
                                comm.Parameters.AddWithValue("@DT_CURRENT_DATE", obj.SZ_VISIT_DATE);
                                comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.Transaction = tr;
                                comm.CommandText = "update MST_ACCU_FOLLOWUP set  SZ_PATIENT_SIGN_PATH='" + obj.SZ_SIGN_PATH + "',bt_pat_sign_success='" + obj.BIT_OF_SIGNPATH + "' where SZ_COMPANY_ID='" + obj.SZ_COMPANY_ID + "' and I_EVENT_ID='" + eventId + "'";
                                comm.CommandType = CommandType.Text;
                                comm.Connection = conn;
                                comm.ExecuteNonQuery();
                            }
                            if (strSuccess == "")
                            {
                                strSuccess = strSuccess + "AC-" + obj.SZ_VISIT_DATE;
                            }
                            else
                            {
                                strSuccess = strSuccess + ", AC-" + obj.SZ_VISIT_DATE;
                            }
                        }
                        else
                            if (obj.SZ_PROCEDURE_GROUP == "PT")
                            {
                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.CommandText = "SP_MST_PT_NOTES";
                                comm.CommandTimeout = 0;
                                comm.CommandType = CommandType.StoredProcedure;
                                comm.Connection = conn;
                                comm.Transaction = tr;
                                comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME ", obj.SZ_PATIENT_NAME);
                                comm.Parameters.AddWithValue("@SZ_CASE_NO", obj.SZ_CASE_NO);
                                comm.Parameters.AddWithValue("@DT_DATE_OF_ACCIDENT", obj.DT_DATE_OF_ACCIDENT);
                                comm.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY", obj.SZ_INSURANCE_COMPANY);
                                comm.Parameters.AddWithValue("@SZ_CLAIM_NO", obj.SZ_CLAIM_NO);
                                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                comm.Parameters.AddWithValue("@DT_DATE", obj.DT_DATE);
                                comm.Parameters.AddWithValue("@FLAG", "ADD");
                                comm.ExecuteNonQuery();

                                comm = new SqlCommand();
                                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                comm.Transaction = tr;
                                comm.CommandText = "update MST_PT_NOTES set  SZ_PATIENT_SIGN_PATH='" + obj.SZ_SIGN_PATH + "',bt_pat_sign_success='" + obj.BIT_OF_SIGNPATH + "' where SZ_COMPANY_ID='" + obj.SZ_COMPANY_ID + "' and I_EVENT_ID='" + eventId + "'";
                                comm.CommandType = CommandType.Text;
                                comm.Connection = conn;
                                comm.ExecuteNonQuery();

                                if (strSuccess == "")
                                {
                                    strSuccess = strSuccess + "PT-" + obj.SZ_VISIT_DATE;
                                }
                                else
                                {
                                    strSuccess = strSuccess + ", PT-" + obj.SZ_VISIT_DATE;
                                }
                                
                            }
                            else
                                if (obj.SZ_PROCEDURE_GROUP == "CH")
                                {
                                    int iCHFlag = 0;
                                    comm = new SqlCommand();
                                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.CommandText = "SP_MST_CH_NOTES";
                                    comm.CommandTimeout = 0;
                                    comm.CommandType = CommandType.StoredProcedure;
                                    comm.Connection = conn;
                                    comm.Transaction = tr;
                                    comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                    comm.Parameters.AddWithValue("@SZ_PATIENT_NAME ", obj.SZ_PATIENT_NAME);
                                    comm.Parameters.AddWithValue("@SZ_CASE_NO", obj.SZ_CASE_NO);
                                    comm.Parameters.AddWithValue("@DT_DATE", obj.DT_DATE);
                                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                    comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME ", obj.SZ_DOCTOR_NAME);
                                    
                                    comm.Parameters.AddWithValue("@FLAG", "ADD");
                                    comm.ExecuteNonQuery();

                                    comm = new SqlCommand();
                                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    comm.Transaction = tr;
                                    comm.CommandText = "update MST_CH_NOTES set  SZ_PATIENT_SIGN_PATH='" + obj.SZ_SIGN_PATH + "',bt_pat_sign_success='" + obj.BIT_OF_SIGNPATH + "' where SZ_COMPANY_ID='" + obj.SZ_COMPANY_ID + "' and I_EVENT_ID='" + eventId + "'";
                                    comm.CommandType = CommandType.Text;
                                    comm.Connection = conn;
                                    comm.ExecuteNonQuery();
                                    
                                    DataSet BitdsCH = new DataSet();
                                    SqlCommand cmd = new SqlCommand("SP_GET_BIT_FOR_VALIDATE_AND_SHOW_PREVIOUS_VISIT", conn);
                                    cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                    cmd.CommandTimeout = 0;
                                    cmd.Transaction = tr;
                                    cmd.Parameters.AddWithValue("@SZ_USER_ID", obj.SZ_USER_ID);
                                    cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                    SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                                    sqlda.Fill(BitdsCH);
                                    bt_validate = BitdsCH.Tables[0].Rows[0]["BT_VALIDATE_AND_SHOW"].ToString().ToLower();

                                    if (bt_validate == "true")
                                    {
                                        DataSet dsCH = new DataSet();
                                        cmd = new SqlCommand("SP_GET_PREVIOUS_VISIT_CH_INFO_USING_CASEID", conn);
                                        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                        cmd.CommandTimeout = 0;
                                        cmd.Transaction = tr;
                                        cmd.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                                        cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.ExecuteNonQuery();
                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                        da.Fill(dsCH);
                                        //string CH_eventId = dsCH.Tables[0].Rows[0]["I_EVENT_ID"].ToString();

                                        if (dsCH.Tables.Count > 0)
                                        {
                                            if (dsCH.Tables[0].Rows.Count > 0)
                                            {
                                                string CH_eventId = dsCH.Tables[0].Rows[0]["I_EVENT_ID"].ToString();
                                                DataSet dsUpdate = new DataSet();
                                                SqlCommand cmdUpdate = new SqlCommand("SP_UPDATE_CH_NOTES", conn);
                                                cmdUpdate.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                cmdUpdate.CommandTimeout = 0;
                                                cmdUpdate.Transaction = tr;
                                                cmdUpdate.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                                cmdUpdate.Parameters.AddWithValue("@SZ_CASE_ID", obj.SZ_CASE_ID);
                                                cmdUpdate.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                                cmdUpdate.Parameters.AddWithValue("@BT_NO_CHANGE_IN_MY_CONDITION", dsCH.Tables[0].Rows[0]["BT_NO_CHANGE_IN_MY_CONDITION"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CHANGE_IN_MY_CONDITION", dsCH.Tables[0].Rows[0]["BT_CHANGE_IN_MY_CONDITION"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MY_CONDITION_IS_ABOUT_SAME", dsCH.Tables[0].Rows[0]["BT_MY_CONDITION_IS_ABOUT_SAME"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MILD", dsCH.Tables[0].Rows[0]["BT_MILD"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MODERATE", dsCH.Tables[0].Rows[0]["BT_MODERATE"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_SEVERE", dsCH.Tables[0].Rows[0]["BT_SEVERE"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_VERY_SEVERE", dsCH.Tables[0].Rows[0]["BT_VERY_SEVERE"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HEADACHE_RIGHT", dsCH.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HEADACHE_LEFT", dsCH.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HEADACHE_BOTH", dsCH.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_NECK_RIGHT", dsCH.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_NECK_LEFT", dsCH.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_NECK_BOTH", dsCH.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MID_BACK_RIGHT", dsCH.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MID_BACK_LEFT", dsCH.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_MID_BACK_BOTH", dsCH.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOW_BACK_RIGHT", dsCH.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOW_BACK_LEFT", dsCH.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOW_BACK_BOTH", dsCH.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_JAW_RIGHT", dsCH.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_JAW_LEFT", dsCH.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_JAW_BOTH", dsCH.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_SHOULDER_RIGHT", dsCH.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_SHOULDER_LEFT", dsCH.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_SHOULDER_BOTH", dsCH.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_ELBOW_RIGHT", dsCH.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_ELBOW_LEFT", dsCH.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_ELBOW_BOTH", dsCH.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_WRIST_RIGHT", dsCH.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_WRIST_LEFT", dsCH.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_WRIST_BOTH", dsCH.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HAND_RIGHT", dsCH.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HAND_LEFT", dsCH.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HAND_BOTH", dsCH.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FINGERS_RIGHT", dsCH.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FINGERS_LEFT", dsCH.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FINGERS_BOTH", dsCH.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HIP_RIGHT", dsCH.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HIP_LEFT", dsCH.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_HIP_BOTH", dsCH.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_THIGH_RIGHT", dsCH.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_THIGH_LEFT", dsCH.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_THIGH_BOTH", dsCH.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_KNEE_RIGHT", dsCH.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_KNEE_LEFT", dsCH.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_KNEE_BOTH", dsCH.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOWER_LEG_RIGHT", dsCH.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOWER_LEG_LEFT", dsCH.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_LOWER_LEG_BOTH", dsCH.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FOOT_RIGHT", dsCH.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FOOT_LEFT", dsCH.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_FOOT_BOTH", dsCH.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_TOES_RIGHT", dsCH.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_TOES_LEFT", dsCH.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_TOES_BOTH", dsCH.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@SZ_SUBJECTIVE_ADDITIONAL_COMMENTS", dsCH.Tables[0].Rows[0]["SZ_SUBJECTIVE_ADDITIONAL_COMMENTS"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_FLEX", dsCH.Tables[0].Rows[0]["BT_CERVICAL_FLEX"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_EXT", dsCH.Tables[0].Rows[0]["BT_CERVICAL_EXT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_RT_ROT", dsCH.Tables[0].Rows[0]["BT_CERVICAL_RT_ROT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_LFT_ROT", dsCH.Tables[0].Rows[0]["BT_CERVICAL_LFT_ROT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_RT_LAT_FLEX", dsCH.Tables[0].Rows[0]["BT_CERVICAL_RT_LAT_FLEX"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_LFT_LAT_FLEX", dsCH.Tables[0].Rows[0]["BT_CERVICAL_LFT_LAT_FLEX"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_FLEX",dsCH.Tables[0].Rows[0]["BT_THORACIC_FLEX"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_RT_ROT",dsCH.Tables[0].Rows[0]["BT_THORACIC_RT_ROT"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_LFT_ROT",dsCH.Tables[0].Rows[0]["BT_THORACIC_LFT_ROT"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_FLEX",dsCH.Tables[0].Rows[0]["BT_LUMBAR_FLEX"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_EXT",dsCH.Tables[0].Rows[0]["BT_LUMBAR_EXT"].ToString());            
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_RT_LAT_FLEX",dsCH.Tables[0].Rows[0]["BT_LUMBAR_RT_LAT_FLEX"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_LFT_LAT_FLEX",dsCH.Tables[0].Rows[0]["BT_LUMBAR_LFT_LAT_FLEX"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@SZ_OBJECTIVE_ADDITIONAL_COMMENTS", dsCH.Tables[0].Rows[0]["SZ_OBJECTIVE_ADDITIONAL_COMMENTS"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_ASSESSMENT_NO_CHANGE",dsCH.Tables[0].Rows[0]["BT_ASSESSMENT_NO_CHANGE"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_ASSESSMENT_IMPROVING",dsCH.Tables[0].Rows[0]["BT_ASSESSMENT_IMPROVING"].ToString());            
                                                cmdUpdate.Parameters.AddWithValue("@BT_ASSESSMENT_FLAIR_UP",dsCH.Tables[0].Rows[0]["BT_ASSESSMENT_FLAIR_UP"].ToString());              
                                                cmdUpdate.Parameters.AddWithValue("@BT_ASSESSMENT_AS_EXPECTED",dsCH.Tables[0].Rows[0]["BT_ASSESSMENT_AS_EXPECTED"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_ASSESSMENT_SLOWER_THAN_EXPECTED",dsCH.Tables[0].Rows[0]["BT_ASSESSMENT_SLOWER_THAN_EXPECTED"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_STOP_ALL_ACTIVITES",dsCH.Tables[0].Rows[0]["BT_STOP_ALL_ACTIVITES"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_REDUCE_ALL_ACTIVITES",dsCH.Tables[0].Rows[0]["BT_REDUCE_ALL_ACTIVITES"].ToString());         
                                                cmdUpdate.Parameters.AddWithValue("@BT_RESUME_LIGHT_ACTIVITES",dsCH.Tables[0].Rows[0]["BT_RESUME_LIGHT_ACTIVITES"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_RESUME_ALL_ACTIVITES",dsCH.Tables[0].Rows[0]["BT_RESUME_ALL_ACTIVITES"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_CERVICAL",dsCH.Tables[0].Rows[0]["BT_TREATMENT_CERVICAL"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_THORACIC",dsCH.Tables[0].Rows[0]["BT_TREATMENT_THORACIC"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_LUMBAR",dsCH.Tables[0].Rows[0]["BT_TREATMENT_LUMBAR"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_DORSOLUMBAR",dsCH.Tables[0].Rows[0]["BT_TREATMENT_DORSOLUMBAR"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_SACROILIAC",dsCH.Tables[0].Rows[0]["BT_TREATMENT_SACROILIAC"].ToString());             
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_TEMPROMANDIBULAR_JOINT",dsCH.Tables[0].Rows[0]["BT_TREATMENT_TEMPROMANDIBULAR_JOINT"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_PROCEDURE_CODE_98940", dsCH.Tables[0].Rows[0]["BT_PROCEDURE_CODE_98940"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_PROCEDURE_CODE_98941", dsCH.Tables[0].Rows[0]["BT_PROCEDURE_CODE_98941"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_PROCEDURE_CODE_99203", dsCH.Tables[0].Rows[0]["BT_PROCEDURE_CODE_99203"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_PROCEDURE_CODE_9921_1", dsCH.Tables[0].Rows[0]["BT_PROCEDURE_CODE_9921_1"].ToString());
                                                cmdUpdate.Parameters.AddWithValue("@BT_PROCEDURE_CODE_9921_2", dsCH.Tables[0].Rows[0]["BT_PROCEDURE_CODE_9921_2"].ToString());   
                                                cmdUpdate.Parameters.AddWithValue("@BT_SPASM",dsCH.Tables[0].Rows[0]["BT_SPASM"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_EDEMA",dsCH.Tables[0].Rows[0]["BT_EDEMA"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_TRIGGER_POINTS",dsCH.Tables[0].Rows[0]["BT_TRIGGER_POINTS"].ToString());                
                                                cmdUpdate.Parameters.AddWithValue("@BT_FIXATION" ,dsCH.Tables[0].Rows[0]["BT_FIXATION"].ToString());              
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL" ,dsCH.Tables[0].Rows[0]["BT_CERVICAL"].ToString());              
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC" ,dsCH.Tables[0].Rows[0]["BT_THORACIC"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR" ,dsCH.Tables[0].Rows[0]["BT_LUMBAR"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_SACRUM" ,dsCH.Tables[0].Rows[0]["BT_SACRUM"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_PELVIS" ,dsCH.Tables[0].Rows[0]["BT_PELVIS"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_TRAPEZIUS" ,dsCH.Tables[0].Rows[0]["BT_TRAPEZIUS"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_RHOMBOIDS" ,dsCH.Tables[0].Rows[0]["BT_RHOMBOIDS"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_PIRIFORMIS" ,dsCH.Tables[0].Rows[0]["BT_PIRIFORMIS"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_QUAD" ,dsCH.Tables[0].Rows[0]["BT_QUAD"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_STERNOCLEIDOMASTOID" ,dsCH.Tables[0].Rows[0]["BT_STERNOCLEIDOMASTOID"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_QL" ,dsCH.Tables[0].Rows[0]["BT_QL"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_LEVATOR_SCAPULAE" ,dsCH.Tables[0].Rows[0]["BT_LEVATOR_SCAPULAE"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_CERVICAL_PARASPINAL" ,dsCH.Tables[0].Rows[0]["BT_CERVICAL_PARASPINAL"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_PARASPINAL" ,dsCH.Tables[0].Rows[0]["BT_THORACIC_PARASPINAL"].ToString());               
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_PARASPINAL" ,dsCH.Tables[0].Rows[0]["BT_LUMBAR_PARASPINAL"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_EXT",dsCH.Tables[0].Rows[0]["BT_THORACIC_EXT"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_RT_LAT_FLEX" ,dsCH.Tables[0].Rows[0]["BT_THORACIC_RT_LAT_FLEX"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_THORACIC_LFT_LAT_FLEX",dsCH.Tables[0].Rows[0]["BT_THORACIC_LFT_LAT_FLEX"].ToString());   
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_RT_ROT",dsCH.Tables[0].Rows[0]["BT_LUMBAR_RT_ROT"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_LUMBAR_LFT_ROT",dsCH.Tables[0].Rows[0]["BT_LUMBAR_LFT_ROT"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_CERVICOTHORACIC",dsCH.Tables[0].Rows[0]["BT_TREATMENT_CERVICOTHORACIC"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@BT_TREATMENT_LUMBOPELVIC",dsCH.Tables[0].Rows[0]["BT_TREATMENT_LUMBOPELVIC"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_HEADACHE",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_NECK",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_MID_BACK",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_LOW_BACK",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_JAW",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_SHOULDER",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_ELBOW",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_WRIST",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_HAND",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_FINGERS",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_HIP",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_THIGH",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_KNEE",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_LOWER_LEG",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_FOOT",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"].ToString());  
                                                cmdUpdate.Parameters.AddWithValue("@SZ_PAIN_LEVEL_TOES",dsCH.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"].ToString());
                                                cmdUpdate.CommandType = CommandType.StoredProcedure;
                                                cmdUpdate.ExecuteNonQuery();



                                                DataSet ProcIdCH = new DataSet();
                                                cmd = new SqlCommand("sp_get_Proccode_id", conn);
                                                cmdUpdate.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                cmd.CommandTimeout = 0;
                                                cmd.Transaction = tr;
                                                cmd.Parameters.AddWithValue("@I_EVENT_ID", CH_eventId);
                                                cmd.Parameters.AddWithValue("@flag", "getProcCode");
                                                cmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", "");
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.ExecuteNonQuery();
                                                SqlDataAdapter CHda = new SqlDataAdapter(cmd);
                                                CHda.Fill(ProcIdCH);

                                                if (ProcIdCH.Tables.Count > 0)
                                                {
                                                    if (ProcIdCH.Tables[0].Rows.Count > 0)
                                                    {
                                                        for (int c = 0; c < ProcIdCH.Tables[0].Rows.Count; c++)
                                                        {
                                                            //obj.SZ_PROC_CODE = ProcIdDs.Tables[0].Rows[0]["SZ_PROC_CODE"].ToString();
                                                            SqlCommand command = new SqlCommand();
                                                            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                            command.Transaction = tr;
                                                            command.CommandText = "sp_insert_proc_code_id";
                                                            command.CommandTimeout = 0;
                                                            command.CommandType = CommandType.StoredProcedure;
                                                            command.Connection = conn;
                                                            command.Parameters.AddWithValue("@SZ_PROC_CODE", ProcIdCH.Tables[0].Rows[c]["SZ_PROC_CODE"].ToString());
                                                            command.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                                                            command.Parameters.AddWithValue("@flag", "BT_UPDATE");
                                                            command.ExecuteNonQuery();
                                                        }
                                                    }
                                                }

                                                DataSet Getds = new DataSet();
                                                cmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", conn);
                                                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                cmd.CommandTimeout = 0;
                                                cmd.Transaction = tr;
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@SZ_EVENT_ID", CH_eventId);
                                                cmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                                                cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                                cmd.Parameters.AddWithValue("@FLAG", "GET");
                                                cmd.ExecuteNonQuery();
                                                SqlDataAdapter Getda = new SqlDataAdapter(cmd);
                                                Getda.Fill(Getds);

                                                if (Getds.Tables.Count > 0)
                                                {
                                                    if (Getds.Tables[0].Rows.Count > 0)
                                                    {
                                                        for (int d = 0; d < Getds.Tables[0].Rows.Count; d++)
                                                        {
                                                            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", conn);
                                                            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                                                            sqlCmd.CommandType = CommandType.StoredProcedure;
                                                            sqlCmd.CommandTimeout = 0;
                                                            sqlCmd.Transaction = tr;
                                                            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", eventId);
                                                            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", obj.SZ_DOCTOR_ID);
                                                            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                                                            sqlCmd.Parameters.AddWithValue("@SZ_COMPLAINT_ID", Getds.Tables[0].Rows[d]["i_complaint_id"].ToString());
                                                            sqlCmd.Parameters.AddWithValue("@FLAG", "SAVE");
                                                            sqlCmd.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                            }
                                            
                                        }
                                        
                                    }
                                    

                                    
                                    
                                    if (strSuccess == "")
                                    {
                                        strSuccess = strSuccess + "CH-" + obj.SZ_VISIT_DATE;
                                    }
                                    else
                                    {
                                        strSuccess = strSuccess + ", CH-" + obj.SZ_VISIT_DATE;
                                     }
                                }

                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_SET_CHECK_IN_USER_ID";
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = tr;
                        comm.Parameters.AddWithValue("@I_EVENT_ID ", eventId);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@SZ_CHECKIN_USER_ID", obj.SZ_CHECKIN_USER_ID);

                        comm.ExecuteNonQuery();
                    }
                    else
                    {
                        if (strError == "")
                        {
                            strError = strError + obj.SZ_PROCEDURE_GROUP + "-" + obj.SZ_VISIT_DATE;
                        }
                        else
                        {
                            strError = strError + ", " + obj.SZ_PROCEDURE_GROUP + "-" + obj.SZ_VISIT_DATE;
                        }
                        
                    }
                //Appointment (s) other than <"+obj.SZ_VISIT_DATE+"> were added successfully. The appointment (s) shown here already exist for the selected patient
            }

            tr.Commit();
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            strSuccess = "";
            strError = "";
            tr.Rollback();
        }
        finally { conn.Close(); }

        objReturn.SZ_PRINT_SUCCESS_MESSAGE = strSuccess;
        objReturn.SZ_PRINT_ERROR_MESSAGE = strError;
        return objReturn;
        
    }

    public DataSet GetSpecialityByEventID(string szEventID,string szCompanyId,string szPath)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_SPECIALITY_USING_EVENT_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dsReturn);

            if (dsReturn.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP"].ToString() == "AC")
            {
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "update MST_ACCU_FOLLOWUP set  SZ_PATIENT_SIGN_PATH='" + szPath + "',bt_pat_sign_success=1 where SZ_COMPANY_ID='" + szCompanyId + "' and I_EVENT_ID='" + szEventID + "'";
                comm.CommandType = CommandType.Text;
                comm.Connection = conn;
                comm.ExecuteNonQuery();
            }
            else
                if (dsReturn.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP"].ToString() == "PT")
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "update MST_PT_NOTES set  SZ_PATIENT_SIGN_PATH='" + szPath + "',bt_pat_sign_success=1 where SZ_COMPANY_ID='" + szCompanyId + "' and I_EVENT_ID='" + szEventID + "'";
                    comm.CommandType = CommandType.Text;
                    comm.Connection = conn;
                    comm.ExecuteNonQuery();
                }
                else
                    if (dsReturn.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP"].ToString() == "CH")
                    {
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "update MST_CH_NOTES set  SZ_PATIENT_SIGN_PATH='" + szPath + "',bt_pat_sign_success=1 where SZ_COMPANY_ID='" + szCompanyId + "' and I_EVENT_ID='" + szEventID + "'";
                        comm.CommandType = CommandType.Text;
                        comm.Connection = conn;
                        comm.ExecuteNonQuery();
                    }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }
    //public string GET_BIT_FOR_VALIDATE_AND_SHOW_PREVIOUS_VISIT(string sz_user_id, string sz_company_id)
    //{
    //    DataSet ds = new DataSet();
    //    string bt_validate = "";
    //    try
    //    {
    //        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
    //        conn.Open();
    //        SqlCommand cmd = new SqlCommand("SP_GET_BIT_FOR_VALIDATE_AND_SHOW_PREVIOUS_VISIT",conn);
    //        cmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
    //        cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
    //        sqlda.Fill(ds);
    //        bt_validate = ds.Tables[0].Rows[0]["BT_VALIDATE_AND_SHOW"].ToString().ToLower();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open)
    //        {
    //            conn.Close();
    //        }
    //    }
    //    return bt_validate;
    //}
}
