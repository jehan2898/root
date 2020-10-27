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
/// Summary description for Bill_Sys_NF3_Template
/// </summary>
public class Patient_TVBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public Patient_TVBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void savePatientInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
                sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@I_PATIENT_AGE", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STREET", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_CITY", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ZIP", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_EMAIL", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_PHONE", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_PHONE_EXTENSION", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@MI", objAL[12].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_WCB_NO", objAL[13].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", objAL[13].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", objAL[14].ToString());
            if (objAL[15].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_DOB", objAL[15].ToString()); }
            sqlCmd.Parameters.AddWithValue("@SZ_GENDER", objAL[16].ToString());
            if (objAL[17].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_INJURY", objAL[17].ToString()); }
            sqlCmd.Parameters.AddWithValue("@SZ_JOB_TITLE", objAL[18].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_ACTIVITIES", objAL[19].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STATE", objAL[20].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_CASE_NO", objAL[21].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_NAME", objAL[22].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_PHONE", objAL[23].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ADDRESS", objAL[24].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_CITY", objAL[25].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_STATE", objAL[26].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ZIP", objAL[27].ToString());
            
           
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[29].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[30].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_WRONG_PHONE", objAL[31].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_TRANSPORTATION", objAL[32].ToString());
            if (objAL[33].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_STATE_ID", objAL[33].ToString());

            if (objAL[34].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STATE_ID", objAL[34].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHART_NO", objAL[35].ToString());
            if (objAL[36].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FIRST_TREATMENT", objAL[36].ToString()); }
            //sqlCmd.Parameters.AddWithValue("@BT_SINGLE", objAL[37].ToString());
            //sqlCmd.Parameters.AddWithValue("@BT_MARRIED", objAL[38].ToString());
            //sqlCmd.Parameters.AddWithValue("@BT_OTHER", objAL[39].ToString());
            //sqlCmd.Parameters.AddWithValue("@BT_EMPLOYEE", objAL[40].ToString());
            //sqlCmd.Parameters.AddWithValue("@BT_FULLTIME", objAL[41].ToString());
            //sqlCmd.Parameters.AddWithValue("@BT_PARTTIME", objAL[42].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_RELATION", objAL[37].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STATUS", objAL[38].ToString());

            //-- add cell Phone #
            if (objAL.Count >= 40)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_CELLNO", objAL[39].ToString());
            }

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

    public void savePatientAccidentInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_ACCIDENT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_INFO_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PLATE_NO", objAL[2].ToString());
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_ACCIDENT_DATE", objAL[3].ToString()); }
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_NO", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FROM_CAR", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[9].ToString());                   
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[10].ToString());
            if (objAL[11].ToString() != "NA")
            {
                sqlCmd.Parameters.AddWithValue("SZ_ACCIDENT_STATE_ID", objAL[11].ToString());
            }
            
            if (objAL[12].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TYPE", objAL[12].ToString());
            }
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

    public void saveCaseInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_CASE_MASTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NUMBER", objAL[9].ToString());
            if (objAL[10].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_ACCIDENT", objAL[10].ToString()); }
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_ASSOCIATE_DIAGNOSIS_CODE", objAL[12].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", objAL[13].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[14].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER", objAL[15].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[16].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_wcb_no", objAL[17].ToString());

            if (objAL.Count > 18) { sqlCmd.Parameters.AddWithValue("@sz_policy_holder_address", objAL[18].ToString()); }
            if (objAL.Count > 19) { sqlCmd.Parameters.AddWithValue("@sz_policy_holder_city", objAL[19].ToString()); }
            if (objAL.Count > 20) { sqlCmd.Parameters.AddWithValue("@sz_policy_holder_state_id", objAL[20].ToString()); }
            if (objAL.Count > 21) { sqlCmd.Parameters.AddWithValue("@sz_policy_holder_zip", objAL[21].ToString()); }
            if (objAL.Count > 22) { sqlCmd.Parameters.AddWithValue("@sz_policy_holder_phone", objAL[22].ToString()); }
            //if (objAL.Count > 23) { sqlCmd.Parameters.AddWithValue("@dt_policy_holder_dob", objAL[23].ToString()); }
            if (objAL.Count > 23){ sqlCmd.Parameters.AddWithValue("@SZ_RELACTION_WITH_PATIENT", objAL[23].ToString());}
            if (objAL.Count > 24){ sqlCmd.Parameters.AddWithValue("@SZ_MARRIED_STATUS", objAL[24].ToString());}
            if (objAL.Count > 25) { sqlCmd.Parameters.AddWithValue("@dt_policy_holder_dob", objAL[25].ToString()); }
            
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

    public string getPhysicalPath()
    {
        String szParamValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            

            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlCon);
            cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }
            
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
        return szParamValue;
    }

    public int getDiagnosisCodeCount(string p_szBillID)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        int iCount = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_NF3_TEMPLATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDIGNOSISCODECOUNT");
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                iCount = Convert.ToInt32(dr["Diag_Count"].ToString());
            }
            
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
        return iCount;
    }

    public DataSet GetPatientDataList(string strcompanyid)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
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

    public DataSet GetPatientDataListNEW(string strcompanyid,string sz_user_id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
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

    public DataSet GetSelectedPatientDataList(string strcompanyid, string strpatientid)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_LIST_FOR_APPOINTMENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;    
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", strpatientid);
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

    public DataSet GetSelectedPatientDataListNEW(string strcompanyid, string strpatientid,string sz_user_id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_LIST_FOR_APPOINTMENT_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;    
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", strcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", strpatientid);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
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

    public DataSet GetAppointPatientDetails(int eventId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_APPOINTMENT_PATIENT_DETAIL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", eventId);
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
    public DataSet GetAppointPatientDetails_For_Reminder(int eventId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_APPOINTMENT_PATIENT_DETAIL_FOR_REMINDER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", eventId);
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

    public DataSet GetAppointProcCode(int eventId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_APPOINTMENT_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", eventId);
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


    public void UpdateInsurancetoCase(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_CASE_MASTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NUMBER", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[8].ToString());
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

    public DataSet GetAttornyInfo(string attornyid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", attornyid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ATTORNEY_INFO");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public Boolean getScheduleStatus(string p_szScheduleID)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        string szResult="";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SCHEDULE_STATUS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_SCHEDULE_ID", p_szScheduleID);
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                szResult = dr["RESULT"].ToString();
            }

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
        if (szResult == "FOUND")
            return true;
        else
            return false;
    }





    public void saveAccidentInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PLATE_NO", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_SPECIALTY", objAL[13].ToString());
            if (objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_ACCIDENT_DATE", objAL[2].ToString()); }
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_NO", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FROM_CAR", objAL[6].ToString());
            if (objAL[7].ToString() != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE_ID", objAL[7].ToString());
            }
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_NAME", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_ADDRESS", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIBE_INJURY", objAL[10].ToString());
            if (objAL[11].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_ADMISSION_DATE", objAL[11].ToString()); }
            if (objAL[12].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TYPE", objAL[12].ToString());
            }
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
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

    public DataSet GetAccidentInformation(string p_szPatientID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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


    public DataSet getOutscheduleDetail(int eventId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_OUTSCHEDULE_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", eventId);
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

    public string SavetoSaveToAllowed(string sz_Case_no, string sz_company_id ,string sz_case_id,string flag)
    {
        SqlParameter sqlParam = new SqlParameter();
        SqlDataReader sqlreader;
        sqlCon = new SqlConnection(strConn);
        String _return = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_check_insurance_same", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Text);
            sqlParam.Direction = ParameterDirection.ReturnValue;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("SZ_CASE_NO", sz_Case_no);
            sqlCmd.Parameters.AddWithValue("FLAG", flag);
            //sqlreader = sqlCmd.ExecuteReader();

            sqlreader = sqlCmd.ExecuteReader();
            while (sqlreader.Read())
            {
                _return = sqlreader[0].ToString();
            }

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
        return _return;
    }


    public string UpdatetoSaveToAllowed(string sz_Case_no, string sz_company_id, string sz_case_id, string flag)
    {
        SqlParameter sqlParam = new SqlParameter();
        SqlDataReader sqlreader;
        sqlCon = new SqlConnection(strConn);
        String _return = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_update_associate", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Text);
            sqlParam.Direction = ParameterDirection.ReturnValue;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("SZ_CASE_NO", sz_Case_no);
            sqlCmd.Parameters.AddWithValue("FLAG", flag);
            //sqlreader = sqlCmd.ExecuteReader();

            if (flag.Equals("InsAddressUpdate"))
            {
                sqlCmd.ExecuteNonQuery();
            }
            else
            {
                sqlreader = sqlCmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    _return = sqlreader[0].ToString();
                }
            }

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
        return _return;
    }

    public DataSet GetPatientListForAttorny(string sz_att_user_id, string sz_case_no, string sz_patient_name)
    {
        DataSet dsearch = new DataSet();
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_GET_PATIENT_INFO_FOR_ATTORNY";
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_att_user_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", sz_case_no);
            sqlCmd.Parameters.AddWithValue("@SZ_PATENT_NAME", sz_patient_name);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlda = new SqlDataAdapter(sqlCmd);
            
            sqlda.Fill(dsearch);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }

        return dsearch;
    }

    public void Save_Attorny_User(string sz_user_id, string sz_attorny_id, string sz_company_id)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ATTORNY_USERS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNY_ID", sz_attorny_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

            sqlCmd.ExecuteNonQuery();
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
    }

    public void Update_Attorny_User(string sz_user_id, string sz_attorny_id, string sz_company_id, string id)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ATTORNY_USERS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNY_ID", sz_attorny_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@ID", id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");

            sqlCmd.ExecuteNonQuery();
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
    }

    public void Delete_Attorny_User(string sz_company_id, string id)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ATTORNY_USERS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@ID", id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            sqlCmd.ExecuteNonQuery();
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
    }


    public void SaveAttorneyinfo(string sz_attorney_id, string sz_attorney_first_name, string sz_attorney_last_name, string sz_attorney_city, string sz_attorney_state, string sz_attorney_zip, string sz_attorney_phone, string sz_attorney_fax, string sz_attorney_email, string sz_comapny_id, string sz_attorney_state_id, string sz_attorney_address, string attorney_type_id)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", sz_attorney_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", sz_attorney_first_name);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_LAST_NAME", sz_attorney_last_name);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_CITY", sz_attorney_city);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE", sz_attorney_state);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ZIP", sz_attorney_zip);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_PHONE", sz_attorney_phone);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FAX", sz_attorney_fax);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_EMAIL", sz_attorney_email);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_comapny_id);
            if (sz_attorney_state_id.ToString() == "NA")
            {

            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE_ID", sz_attorney_state_id);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ADDRESS", sz_attorney_address);
            if (attorney_type_id.ToString() == "NA")
            {

            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@ATTORNEY_TYPE_ID", attorney_type_id);
            }

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

            sqlCmd.ExecuteNonQuery();
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
    }

    public DataSet GetAttornyInfoForUpdate(string attornyid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", attornyid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ATTORNEY_INFO");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public void UpdateAttornyInfo(string sz_attorny_id, string sz_attorny_first_name, string sz_attorny_last_name, string sz_attorney_city, string sz_attorny_state, string sz_attorny_zip, string sz_attorny_phone, string sz_attorny_fax, string sz_attorny_email, string sz_company_id, string sz_attorny_state_id, string sz_attorny_address, string sz_attorny_type_id)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", sz_attorny_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", sz_attorny_first_name);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_LAST_NAME", sz_attorny_last_name);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_CITY", sz_attorney_city);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE", sz_attorny_state);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ZIP", sz_attorny_zip);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_PHONE", sz_attorny_phone);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FAX", sz_attorny_fax);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_EMAIL", sz_attorny_email);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            if (sz_attorny_state_id.ToString() == "NA")
            {

            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE_ID", sz_attorny_state_id);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ADDRESS", sz_attorny_address);
            //sqlCmd.Parameters.AddWithValue("@BT_DEFAULT", bt_attorny_default);
            if (sz_attorny_type_id.ToString() == "NA")
            {

            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@ATTORNEY_TYPE_ID", sz_attorny_type_id);
            }


            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");

            sqlCmd.ExecuteNonQuery();
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
    }

    public string GetUserIdForAttornyUser(string sz_CompanyID, string sz_AttornyId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader sqlreader;

        string szReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_USERID_FOR_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNY_ID", sz_AttornyId);
            sqlreader = sqlCmd.ExecuteReader();
            while (sqlreader.Read())
            {
                szReturn = sqlreader[0].ToString();

            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = "";

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return szReturn;
    }

    public void SaveAttorneyUser(string sz_user_id, string sz_attorny_id, string sz_company_id, string sz_case_id, string bt_allowtoaccess_doc)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_ATTORNEY_USERID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", sz_attorny_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@BT_ALLOWTOACCESS_DOCUMENT", bt_allowtoaccess_doc);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

            sqlCmd.ExecuteNonQuery();
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
    }

    public void DeleteCaseAttorny(string sz_attorny_id, string sz_company_id, string id)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_CASE_ATTORNY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", sz_attorny_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@ID", id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            sqlCmd.ExecuteNonQuery();
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
    }

    public DataTable Get_Tab_TestInformation_TEMP_ATT(string p_szCaseID, string p_szCompanyID)
    {
        DataTable dt = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TAB_TESTINFORMATION_TEMP_ATT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);



            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_Outschedule_Tab_Information_ATT(string p_szCaseID, string p_szCompanyID)
    {
        DataTable dt = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_OUSCHEDULE_TAB_INFORMATION_ATT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }
    public void UpdateAdutsterInfo(string sz_aduster_id, string sz_aduster_name, string sz_phone, string sz_extension, string sz_fax, string sz_email, string sz_company_id)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", sz_aduster_id);
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_NAME", sz_aduster_name);
            sqlCmd.Parameters.AddWithValue("@SZ_PHONE", sz_phone);
            sqlCmd.Parameters.AddWithValue("@SZ_EXTENSION", sz_extension);
            sqlCmd.Parameters.AddWithValue("@SZ_FAX", sz_fax);
            sqlCmd.Parameters.AddWithValue("@SZ_EMAIL", sz_email);

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);



            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");

            sqlCmd.ExecuteNonQuery();
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
    }
    public DataSet GetAdjusterInfoForUpdate(string adjusterid, string szCompanyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID ", adjusterid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID ", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_ADJUSTER_DETAIL");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
}
