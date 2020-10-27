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

public class Bill_Sys_DigosisCodeBO
{
  

    String  strsqlCon;
    SqlConnection sqlCon;
    SqlCommand  sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_DigosisCodeBO()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetDoctorTypeDignosisCode_List(ArrayList _arrayList)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
         ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType  = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GetList");
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_TYPE_ID", _arrayList[0].ToString()); 
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[1].ToString()); 
            //sqlCmd.ExecuteNonQuery();
            
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

    public void SaveDoctorTypeDignosisCode(ArrayList _arrayList)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_TYPE_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[2].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public void UpdateDoctorTypeDignosisCode(ArrayList _arrayList)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_TYPE_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[2].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATEDOCTORTYPE");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }

        //Method End

    }

    public string GetLatestDignosisCodeId(String companyid)
    {
        string latestid="";
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlCmd.Parameters.AddWithValue("@FLAG", "GetLatestId");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                latestid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }

        return latestid;


    }

    public DataSet GeProcedureCode_List(string companyID, string procedureCode, string proceduredescription,string doctorID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", procedureCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", proceduredescription);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ALLLIST");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorID);
            
            //sqlCmd.ExecuteNonQuery();

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

    public DataSet GeDignosisCode_List(string companyID,string diagnosisCode,string description)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "ALLList");
            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", diagnosisCode);
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description);
           
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            //sqlCmd.ExecuteNonQuery();

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

    public DataSet GeMultipleDignosisCode_List(string companyID, string diagnosisCode, string description)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_DIAGNOSIS_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "MULTIPLE_DIAG_CODE");
            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", diagnosisCode);
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description);

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            //sqlCmd.ExecuteNonQuery();

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


    public DataSet GetDiagnosisCodeReferalList(string companyID, string strtypeid)
    {        
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_REFERAL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

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

    public DataSet GetPreferredListDiagnosisCode(string companyID, string strtypeid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_PREFERRED_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

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

    public DataSet GetDiagnosisCodeReferalList(string companyID, string strtypeid,string code,string description)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_REFERAL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (strtypeid != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid); }
            if (code != "") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", code); }
            if (description != "") { sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description); }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

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

    public DataSet GetDiagnosisCodePreferredList(string companyID, string strtypeid, string code, string description)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_PREFERRED_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (strtypeid != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid); }
            if (code != "") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", code); }
            if (description != "") { sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description); }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

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



    public DataTable  GetDefaultDiagCodeList(string szCaseID,string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ASSOICIATE_DIAG_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
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
        return ds.Tables[0];
    }

    public void UpdatePreferredListForDiagnosisCode(ArrayList arrDiagnosisCodeID, string szCompanyID, string szPreferred)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrDiagnosisCodeID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_PREFERRED_BIT_FOR_DIAGNOSIS_CODE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", Convert.ToString(arrDiagnosisCodeID[i]));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szPreferred);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }


    }
    //Change added on 10/02/2015
    public DataSet GetSpecialtyGrid(string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");

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
    //Change added on 10/02/2015
    public DataSet GetDiagnosisCodeReferalList(string companyID, string strtypeid, string code, string description, string SZ_TYPE_ID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_REFERAL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (strtypeid != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid); }
            if (code != "") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", code); }
            if (description != "") { sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description); }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", SZ_TYPE_ID);

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

}
