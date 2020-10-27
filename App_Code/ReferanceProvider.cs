using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ReferanceProvider
/// </summary>
public class ReferanceProvider
{
    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
	public ReferanceProvider()
	{

        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}

    public DataSet getspeciality(string SZ_Company_ID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(strsqlcon);

        try
        {
            comm = new SqlCommand("sp_get_specialty_for_reff_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", SZ_Company_ID);
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

    public DataSet getCaseType(string SZ_Company_ID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("sp_get_casetype_for_reff_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", SZ_Company_ID);
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

    public DataSet getBillStatus(String SZ_Company_ID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("sp_get_billstatus_for_reff_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", SZ_Company_ID);
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

    public DataSet getCaseStatus(string SZ_Company_ID)
    {
        DataSet dscase = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {

            comm = new SqlCommand("sp_get_casestatus_for_reff_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", SZ_Company_ID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dscase);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dscase;
    }

    public DataSet getLocation(string SZ_Company_ID)
    {
        DataSet dslocation = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {

            comm = new SqlCommand("sp_get_location_for_reff_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", SZ_Company_ID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dslocation);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dslocation;
     }

    public DataSet getPatientList(RefferingProvioderPatientDAO objPatientDao)
    {
        DataSet dsPatient = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {

            comm = new SqlCommand("SP_GET_PATIENT_FOR_REFERING_PROVIDER", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objPatientDao.companyID);
            comm.Parameters.AddWithValue("@SZ_REFFERING_OFFICE_ID", objPatientDao.reffOfficeID);
            comm.Parameters.AddWithValue("@SZ_CASE_NO", objPatientDao.CaseNo);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE", objPatientDao.CaseType);
            comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", objPatientDao.PatientName);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", objPatientDao.Insurance);
            comm.Parameters.AddWithValue("@SZ_SSNO", objPatientDao.ssnNO);
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", objPatientDao.location);
            comm.Parameters.AddWithValue("@DT_ACCIDENT_DATE", objPatientDao.accidentDate);
            comm.Parameters.AddWithValue("@DT_OF_BIRTH", objPatientDao.birthDate);
            comm.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objPatientDao.claimNumber);
            comm.Parameters.AddWithValue("@SZ_CASE_STATUS", objPatientDao.CaseStatus);
            
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsPatient);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsPatient;
    }
    
    }

