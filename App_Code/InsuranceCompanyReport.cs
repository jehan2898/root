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
/// Summary description for InsuranceCompanyReport
/// </summary>
public class InsuranceCompanyReport
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public InsuranceCompanyReport()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
   
    public DataSet GetCaseID(string procname, string fromdate, string todate, string companyid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlCmd.CommandTimeout = 0;
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
    public DataSet GetResult(string procname, string caseid, string companyid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@CASEID", caseid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);


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
    public string GetInsuranceCompanyName(string caseid, string companyid)
    {
        try
        {
            string _value = "";
            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INSURANCE_COMPANY_NAME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@CASEID", caseid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    _value = dr[0].ToString();
                }
            }
            return _value;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
    }

    public DataSet GetStatusID(string procname, string companyid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlCmd.CommandTimeout = 0;
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

    //Nirmalkumar to get insurance comp for listbox
    public DataSet GetinsuranceCompnaies(string companyid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_COMPANY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", companyid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");

            sqlCmd.CommandTimeout = 0;
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

    public DataSet GetCollectionReport(string procname, string fromdate, string todate, string companyid,string InsuranceId,string caseStatusId,string billStatusID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (fromdate != "" && fromdate != null && todate != "" && todate != null)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_START_DATE", fromdate);
                sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", todate);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (InsuranceId != "" && InsuranceId != null){ sqlCmd.Parameters.AddWithValue("@i_insurance_id", InsuranceId); }
            if (caseStatusId != "" && caseStatusId != null) { sqlCmd.Parameters.AddWithValue("@i_casestatus_id", caseStatusId); }
            if (billStatusID != "" && billStatusID != null) { sqlCmd.Parameters.AddWithValue("@i_billstatus_id", billStatusID); }

            sqlCmd.CommandTimeout = 0;
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
}
