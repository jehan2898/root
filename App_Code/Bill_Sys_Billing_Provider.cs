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


public class Bill_Sys_Billing_Provider
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    public Bill_Sys_Billing_Provider()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet BillingProvider(string szcompayid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_BILLING_PROVIDER_CASETYPE", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet BillingProviderAddress(string szcasetypeid, string szcompayid, string providerid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_BILLING_PROVIDER_ADDRESS", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szcasetypeid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", providerid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public void AddBillingaddress(string sz_company_id, string sz_casetype_id, string sz_provider_address, string sz_provider_city, string sz_provider_id, string sz_provider_name, string sz_provider_phone, string sz_provider_state, string sz_provider_zip, string sz_provider_fax)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_SAVE_BILLING_PROVIDER", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ADDRESS", sz_provider_address);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_CITY", sz_provider_city);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", sz_provider_id);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_NAME", sz_provider_name);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_PHONE", sz_provider_phone);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_STATE", sz_provider_state);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ZIP", sz_provider_zip);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_FAX", sz_provider_fax);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public void RemoveBillingaddresscasetype(string sz_company_id, string sz_casetype_id, string sz_provider_id)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_DELETE_BILLING_PROVIDER_ADDRESS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", sz_provider_id);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public DataSet billing(string szcompayid, string billing)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_OFFICE_NEW_PROVIDER", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@BIT_IS_BILLING", billing);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet PatientDetailsProvider(string szcompayid, string szcaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_PATIENT_DETAILS_PROVIDER", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet ProviderName(string szcompayid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_BILLING_PROVIDER_NAME", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet getGrandTotal(string szofficeid, string szcompayid, string szcaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_CASE_GRAND_TOTAL", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", szofficeid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet getCaseNotes(string szofficeid, string szcompayid, string szcaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_CASE_NOTES", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", szofficeid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet getAttorneyDetails(string szcompayid, string szcaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_ATTORNEY_INFO_FOR_CASE", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet getAdjusterDetails(string szcompayid, string szcaseid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_ADJUSTER_INFO_FOR_CASE", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet GetReadingDoctorList(string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_LHR_READINGDOCTORS", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet GetInsuranceList(string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_MST_INSURANCE_COMPANY", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet GetCaseTypeList(string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_MST_CASE_TYPE", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "CASETYPE_LIST");
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public void AddExcludingBill(string sz_company_id, string sz_readingdoctor_id, string sz_readingdoctor, string sz_insuranceid, string sz_insurancename, string sz_casetype_id, string sz_casetypname)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_SAVE_EXCLUDING_BILL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", sz_readingdoctor_id);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR", sz_readingdoctor);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insuranceid);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_NAME", sz_insurancename);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", sz_casetypname);
           
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public DataSet GetExcludingBillDeatils(string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_EXCLUDING_BILL_DETAILS", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public void RemoveexcludingBill(string sz_ReadingDoctor, string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_DELETE_EXCLUDING_BILL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR", sz_ReadingDoctor);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public void UpdateExcludingBill(string sz_company_id, string sz_readingdoctor_id, string sz_readingdoctor, string sz_insuranceid, string sz_insurancename, string sz_casetype_id, string sz_casetypname)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_UPDATE_EXCLUDING_BILL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", sz_readingdoctor_id);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR", sz_readingdoctor);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insuranceid);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_NAME", sz_insurancename);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", sz_casetypname);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public DataSet GetAllDoctorList(string sz_ReadingDoctor, string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_REDING_DOC_LIST", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_ReadingDoctor);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet GetAllDoctorIDList(string sz_ReadingDoctor, string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_REDING_DOCID_LIST", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR", sz_ReadingDoctor);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }
}
