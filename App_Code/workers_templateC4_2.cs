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
public class workers_templateC4_2
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter objSqlDataAdap;
    DataSet objDataSet;

    SqlDataReader dr;


    public workers_templateC4_2()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public string GetExaminationLatestID(string billnumber)
    {
        string strTreatmentid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_EXAMINATION_TREATMENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETTREATMENTID");
            dr = comm.ExecuteReader();
           
            while (dr.Read())
            {
                strTreatmentid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strTreatmentid;
    }
    public void DeleteExaminationDetails(string examinationid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_EXAMINATION_REFERRAL_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_EXAMINATION_ID", examinationid);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public string GetPlanOfCareLatestID(string patientid)
    {
        string strPlanofcareid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_PLAN_OF_CARE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            comm.Parameters.AddWithValue("@FLAG", "GETPLANOFCAREID");
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strPlanofcareid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strPlanofcareid;
    }
    public string GetPlanOfCareLatestID_New(string billnumber)
    {
        string strPlanofcareid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_PLAN_OF_CARE_NEW", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETPLANOFCAREID");
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strPlanofcareid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strPlanofcareid;
    }

    public string GetDoctorOpinionLatestID(string billnumber)
    {
        string strPlanofcareid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_DOCTORS_OPINION_CFOUR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETOPINIONID");
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strPlanofcareid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strPlanofcareid;
    }
    public DataSet GetExaminationDetailList(string examinationid)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_EXAMINATION_REFERRAL_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_EXAMINATION_ID", examinationid);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }

    public DataSet GetTestTransactionDetailList(string planofcareid)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_TEST_REFERRAL_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PLAN_OF_CARE_ID", planofcareid);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
    public DataSet GetTestTransactionDetailList_New(string planofcareid, string billnumber)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_TEST_REFERRAL_TRANSACTION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_PLAN_OF_CARE_ID", planofcareid);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");


            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }

    public DataSet GetAssistiveDeviceDetailList(string planofcareid)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_ASSISTIVE_DEVICE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PLAN_OF_CARE_ID", planofcareid);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
    public DataSet GetAssistiveDeviceDetailListNewWC4(string planofcareid, string planofcarebillno)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_ASSISTIVE_DEVICE_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_PLAN_OF_CARE_ID", planofcareid);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", planofcarebillno);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
    public void DeletePlanOfCareDetails(string planofcareid, string procname)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = procname;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PLAN_OF_CARE_ID", planofcareid);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }
    public void DeletePlanOfCareDetailsNewWC4(string procname, string planofcarebillno)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = procname;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;          
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", planofcarebillno);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetWorkStatusLatestID(string billnumber)
    {
        string strPlanofcareid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_WORK_STATUS_CFOUR", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETWORKSTATUSID");
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strPlanofcareid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strPlanofcareid;
    }

    public void SavePatientLimitations(ArrayList p_objAL)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION_CFOUR";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_WORK_STATUS_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_LIMITATIONS_ID", p_objAL[1].ToString());
            
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DESCRIPTION", p_objAL[4].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[5].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetWorkStatusLimitation(string p_szPatientID)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION_CFOUR";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
}
