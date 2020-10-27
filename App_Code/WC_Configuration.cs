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

/// <summary>
/// Summary description for WC_Configuration
/// </summary>

public class WC_Configuration
{
    public string[] datevalues;
    public string[] phonevalues;
    public string[] formatcode;
    string strCon;
    SqlConnection SqlCon;
    SqlCommand SqlCmd;
    SqlDataAdapter sqlda;

    public WC_Configuration()
    {
        //initialize all the formats
        InitializeFormat();
        InitialiseValues();
        strCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    //Insert all formats into this method
    public void InitializeFormat()
    {
        formatcode = new string[] { "Date", "Phone" };
    }

    public void InitialiseValues()
    {
        //initializ date format
        datevalues = new string[] { "dd/MM/yyyy", "MM/dd/yyyy", "MMM-dd-yyyy", "dd-MMM-yyyy" };
        //initializ phone format
        phonevalues = new string[] { "(xxx)xxx-xxxx","xxx-xxx-xxxx" };
    }

    //get the values of a particalur format code
    public ArrayList GetValues(string value)
    {
        ArrayList AL = new ArrayList();
        //AL.Add("--Select--");
        if (value == "Date")
        {
            foreach (string s in datevalues)
            {
                if ((s != "") && (s != null))
                {
                    AL.Add(s);
                }
            }
        }else
        if (value == "Phone")
        {
            foreach (string s in phonevalues)
            {
                if ((s != "") && (s != null))
                {
                    AL.Add(s);
                }
            }
        }
        return AL;
    }

    //save the values of a format code
    public Boolean SaveConfiguration(string FormatCode, string FormatValue, string Type, string sz_companyID, string sz_configurationType)
    {
        bool saveConfig = false;
        try
        {
            
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();

            SqlCmd = new SqlCommand("SP_WC_CONFIGURATION", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandTimeout = 0;
            SqlCmd.Parameters.AddWithValue("@SZ_FORMAT_CODE", FormatCode);
            SqlCmd.Parameters.AddWithValue("@SZ_FORMAT_VALUE", FormatValue);
            SqlCmd.Parameters.AddWithValue("@sz_case_type", Type);
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyID);
            SqlCmd.Parameters.AddWithValue("@sz_configuration_type", sz_configurationType);
            SqlCmd.ExecuteNonQuery();
            return true;//transaction succesfull
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally
        {
            SqlCon.Close();
        }
        return saveConfig;
    }

    public void DeleteConfiguration(string i_configuration_id, string sz_companyID)
    {
        try
        {

            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();

            SqlCmd = new SqlCommand("sp_delete_bill_configuration", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandTimeout = 0;
            SqlCmd.Parameters.AddWithValue("@I_CONFIGURATION_ID", i_configuration_id);
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyID);
            SqlCmd.ExecuteNonQuery();
           // return true;//transaction succesfull

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            SqlCon.Close();
        }
    }
    public void SaveBlocklocation(string sz_location_id,string sz_company_id)
    {
        SqlCon = new SqlConnection(strCon);

        try
        {
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_TXN_BILL_BLOCK_LOCATION", SqlCon);
            SqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", sz_location_id);
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            SqlCmd.Parameters.AddWithValue("@SZ_FLAG", "ADD");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { SqlCon.Close(); }
    }
    public void DeleteBlocklocation(string sz_companyid)
    {
        SqlCon = new SqlConnection(strCon);

        try
        {
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_TXN_BILL_BLOCK_LOCATION", SqlCon);

            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);

            SqlCmd.Parameters.AddWithValue("@SZ_FLAG", "DELETEALL");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { SqlCon.Close(); }
    }

    public DataSet GetBlockLocation(string sz_CompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_TXN_BILL_BLOCK_LOCATION", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID ", sz_CompanyID);
            SqlCmd.Parameters.AddWithValue("@SZ_FLAG", "LIST");
            sqlda = new SqlDataAdapter(SqlCmd);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { SqlCon.Close(); }
        return null;
    }


    public DataSet GetBillConfigurationForMandatoryCheck(string sz_CompanyID, string sz_MandatoryField)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_GET_BILL_CONFIGURATION_FOR_MANDATORY_CHECK", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@sz_company_id ", sz_CompanyID);
            SqlCmd.Parameters.AddWithValue("@sz_mandatory_field ", sz_MandatoryField);
            sqlda = new SqlDataAdapter(SqlCmd);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { SqlCon.Close(); }
        return null;
    }

    public DataSet GetBillConfigurationForMandatoryField(string sz_CompanyID, string sz_ConfigurationType)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_GET_BILL_CONFIGURATION_FOR_MANDATORY_FIELD", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@sz_company_id ", sz_CompanyID);
            SqlCmd.Parameters.AddWithValue("@sz_configuration_type ", sz_ConfigurationType);
            sqlda = new SqlDataAdapter(SqlCmd);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { SqlCon.Close(); }
        return null;
    }
    public DataSet GetBillConfigurationForDateForAccident(string sz_CompanyID, string sz_caseid)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_GET_BILL_CONFIGURATION_FOR_DATE_OF_ACCIDENT", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@sz_company_id ", sz_CompanyID);
            SqlCmd.Parameters.AddWithValue("@sz_case_id ", sz_caseid);
            sqlda = new SqlDataAdapter(SqlCmd);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { SqlCon.Close();
        }
        return null;
    }
    public void SaveC4AMRConfiguration(string sz_npi, string sz_service_location_zip, string sz_chk_place_service, string sz_place_of_service, string sz_company_id, string sz_show_balance_due)
    {
        SqlCon = new SqlConnection(strCon);

        try
        {
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_WC_C4MAR_CONFIGURATION", SqlCon);
            SqlCmd.Parameters.AddWithValue("@SZ_NPI", sz_npi);
            SqlCmd.Parameters.AddWithValue("@SZ_SERVICE_LOCATION_ZIP_CODE", sz_service_location_zip);
            SqlCmd.Parameters.AddWithValue("@SZ_CHK_PLACE_SERVICE", sz_chk_place_service);
            SqlCmd.Parameters.AddWithValue("@SZ_PLACE_OF_SERVICE", sz_place_of_service);
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            SqlCmd.Parameters.AddWithValue("@SZ_SHOW_BALANCE_DUE", sz_show_balance_due);
           
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { SqlCon.Close(); }
    }

    public DataSet GetC4AMRConfiguration(string sz_company_id)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCon = new SqlConnection(strCon);
            SqlCon.Open();
            SqlCmd = new SqlCommand("SP_GE_C4MAR_CONFIG", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@sz_company_id ", sz_company_id);
            sqlda = new SqlDataAdapter(SqlCmd);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { SqlCon.Close(); }
        return null;
    }

   

    


}
