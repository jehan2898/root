using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.IO;



/// <summary>
/// Summary description for LF_ClaimNo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class LF_ClaimNo : System.Web.Services.WebService
{

    public LF_ClaimNo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] GetClaimNo(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();

        string[] rIns = new string[10];
        //List<STRING> titleArList = new List<STRING>();
        // ILog log = LogManager.GetLogger(typeof(PatientSearch));

        DataSet returnState = new DataSet();
        try
        {

            DataTable dtIns = InsuranceRecord(prefixText, contextKey);

            string[] r = new string[2];
            for (int i = 0; i < dtIns.Rows.Count; i++)
            {
                string strName = dtIns.Rows[i][0].ToString();
                //rState = dt.Rows[i][1].ToString();
                Ins.Add(strName);
                // rIns.SetValue(dtIns.Rows[i][1], i);


            }
            if (Ins.Count == 0)
            {
                Ins.Insert(0, "No suggestions found for your search");
            }
            return Ins.ToArray();


        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);

            returnState = null;
            return Ins.ToArray();
        }
        // return lst.ToArray();
        // return items.ToArray();

        //string[] r = new string[2];
        //r.SetValue("John Smith", 0);
        //r.SetValue("Smith John", 1);
        //return r;
    }
    public DataTable InsuranceRecord(string strName, string CompanyId)
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

            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_GET_LF_CLAIM_NUMBER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", strName);

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

