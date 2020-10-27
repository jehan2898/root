using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.IO;


/// <summary>
/// Summary description for LF_PateintSearch
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class LF_PateintSearch : System.Web.Services.WebService
{

    public LF_PateintSearch()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetPatientName(string prefixText, int count, string contextKey)
    {
        List<String> items = new List<String>();

        string[] rState = new string[10];
        //List<STRING> titleArList = new List<STRING>();
        // ILog log = LogManager.GetLogger(typeof(PatientSearch));

        DataSet returnState = new DataSet();
        try
        {
            string[] strfirst = prefixText.Split(' ');
            string strFirstName = strfirst[0];
            string strLastName = null;
            if (strfirst != null)
            {
                if (strfirst.Length > 1)
                {
                    strLastName = strfirst[1];
                }
            }

            DataTable dt = GetRecords(strFirstName, strLastName, contextKey);

            string[] r = new string[2];
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName = dt.Rows[i][0].ToString();
                    items.Add(strName);
                }
                if (items.Count == 0)
                {
                    items.Insert(0, "No suggestions found for your search");
                }
            }
            else
            {
                if (items.Count == 0)
                {
                    items.Insert(0, "No suggestions found for your search");
                }
            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //log.Debug("Patient ajax search : " + ex.Message);
            if (items.Count == 0)
            {
                items.Insert(0, "Error fetching search results");
            }
            return items.ToArray();
        }
    }

    private DataTable GetRecords(string strFirstName, string strLastName, string CompanyId)
    {
        SqlConnection sqlCon;
        DataSet ds;
        String strsqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            if (strLastName != null)
            {
                if (strLastName.Trim().Length == 0)
                {
                    strLastName = null;
                }
            }
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LF_PATIENT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ASSIGNED_LAW_FIRM_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@sz_first_name", strFirstName);
            sqlCmd.Parameters.AddWithValue("@sz_last_name", strLastName);
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            return ds.Tables[0];
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

  
   
   

}

