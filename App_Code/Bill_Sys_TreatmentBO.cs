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
/// Summary description for Bill_Sys_TreatmentBO
/// </summary>
public class Bill_Sys_TreatmentBO
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    private ArrayList _arrayList;
    public Bill_Sys_TreatmentBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }



    public DataSet GetDoctorProcedureList(string companyId, string doctorid, string p_szProcedureType)
    {
        string szProcedureType = "GETALLDOCTORTREATMENTS";

        if (p_szProcedureType.ToLower().CompareTo("visits") == 0)
        {
            szProcedureType = "GETALLDOCTORVISITS";
        }
        else if (p_szProcedureType.ToLower().CompareTo("treatments") == 0)
        {
            szProcedureType = "GETALLDOCTORTREATMENTS";
        }
        else if (p_szProcedureType.ToLower().CompareTo("tests") == 0)
        {
            szProcedureType = "GETALLDOCTORTESTS";
        }

        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@FLAG", szProcedureType);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public DataSet GetTreatmentList(string companyId,string doctorid,string szPatientID, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientID);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORTREATMENTS");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public void SaveTreatment(ArrayList arraylist)
    {
        sqlCon = new SqlConnection(strsqlCon);
        
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", arraylist[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arraylist[1].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_DATE", arraylist[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", arraylist[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[4].ToString());
  
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
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

    public ArrayList GetDoctorTreatmentCount(string companyid, string doctorid, string szPatientId, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        _arrayList = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientId);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORTREATMENTSCOUNT");

            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                _arrayList.Add(dr[0].ToString());
                _arrayList.Add(dr[1].ToString());
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
        return _arrayList;
    }
    
    public ArrayList GetLatestDoctorTreatmentCount(string companyid, string patientid, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        _arrayList = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETPATIENTTREATMENTCOUNT");

            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                _arrayList.Add(dr[0].ToString());
                _arrayList.Add(dr[1].ToString());
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
        return _arrayList;
    }

    public DataSet GetLatestTreatmentList(string companyId, string patientid,string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG","LATESTLIST" );
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public DataSet GetSummaryTreatmentList(string companyId, string patientid, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public void UpdateBillableNonBillable(Boolean btBillableNonBillable, string szTYPE_CODE_ID, string sz_COMPANY_ID)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MAKEBILLABLE_NONBILLABLE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BT_Bilable", btBillableNonBillable);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", szTYPE_CODE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_COMPANY_ID);
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


    public DataSet GetSceduledTreatmentList(string companyId, string patientid, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "SCEDULEDLIST");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public DataSet GetBilledTreatmentList(string companyId, string patientid, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", "BILLEDLIST");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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

    public string GetTreatmentListCount(string companyId, string patientid, string flag,string sql_flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        string iCount = "0";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", flag);
            sqlCmd.Parameters.AddWithValue("@FLAG", sql_flag);
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                iCount = dr[0].ToString();
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
}
