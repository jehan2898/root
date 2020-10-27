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
/// <summary>
/// Summary description for Bill_Sys_PatientList
/// </summary>
public class Bill_Sys_PatientDeskList
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_PatientDeskList()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    //SP_GET_PATIENT_DESK_DETAILS
    public DataSet  GetPatienDeskList(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public DataSet GetVisitInsInfo(string szCaseId, string szCompanyId,string szEvetID,string szFlag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_VISIT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", szEvetID);
            sqlCmd.Parameters.AddWithValue("@FLAG", szFlag);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public void UpdateInsInfoevent(string ComapnyId, string sz_insurance_id, string sz_insu_add_id, string sz_eventid, string szCaseID, string szClaimno, string szpolicyno, string szcasetypeid, string szpolicyholder, string dateofaccident, string szpatientphoneno)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_EVENT_WITH_INS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insurance_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADD_ID", sz_insu_add_id);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_eventid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimno);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", szpolicyno);
            sqlCmd.Parameters.AddWithValue("@SZ_CASETYPE_ID", szcasetypeid);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICYHOLDER", szpolicyholder);
            sqlCmd.Parameters.AddWithValue("@DT_DATEOF_ACCIDENT", dateofaccident);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE_NO", szpatientphoneno);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void UpdateSecondayInsuranceForEvent(string sz_CaseID, string sz_CompanyID, string sz_InsuranceID, string sz_InsuranceaddID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_SECONDARY_INS_WITH_EVENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", sz_CaseID);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_InsuranceID);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_InsuranceaddID);
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
}
