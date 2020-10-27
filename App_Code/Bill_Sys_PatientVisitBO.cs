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

public class Bill_Sys_PatientVisitBO
{


    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    private ArrayList _arrayList;
    public Bill_Sys_PatientVisitBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetVisitList(string companyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETVISITSDETAIL");            
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet GetDoctorVisitList(string companyId,string doctorid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORVISITLIST");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

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
        return ds;
    }
    public ArrayList  GetDoctorTotalCount(string companyId, string doctorid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        _arrayList = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORVISITCOUNT");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                _arrayList.Add(dr[0]);
                _arrayList.Add(dr[1]);
            }
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
        return _arrayList;
    }

    public ArrayList GetPatientTotalCount(string companyId, string patientid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        _arrayList = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETPATIENTVISITCOUNT");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                _arrayList.Add(dr[0]);
                _arrayList.Add(dr[1]);
            }
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
        return _arrayList;
    }
    public DataSet GetPatientVisitList(string companyId,string patientId,string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientId);

            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

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
        return ds;
    }

    //
    public void SaveVisitDetails(ArrayList arraylist)
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

    #region "Get default Visit Type as 'C'"

    public string GetDefaultVisitType(string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        string szDefaultVisitType = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VISIT_TYPE_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_DEFAULT_VALUE");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szDefaultVisitType = dr[0].ToString();
            }
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return szDefaultVisitType;
    }

    public string GetVisitType(string p_szCompanyID, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        string szDefaultVisitType = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VISIT_TYPE_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szDefaultVisitType = dr[0].ToString();
            }
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return szDefaultVisitType;
    }


    public DataSet Getinsuanceinfoevent(string szCaseId, string szCompanyId, string szInsId,string szInsAddId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_INS_FOR_EVENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ID", szInsId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADD_ID", szInsAddId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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

    #endregion

}
