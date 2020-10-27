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
/// Summary description for Bill_Sys_import_visit
/// </summary>
public class Bill_Sys_import_visit
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public Bill_Sys_import_visit()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetViewVisitInfo(string CompanyID,string frmDate,string toDate,string caseNo,string noOfdays,string location,string casetype,string searchText)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_IMPORT_VISITS_COUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            if (frmDate != ""){ sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", frmDate); }
            if (toDate != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", toDate); }
            if (caseNo != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", caseNo); }
            if (noOfdays != "") { sqlCmd.Parameters.AddWithValue("@SZ_NUMBER_OF_DAYS", noOfdays); }
            if (location != "" && location !="NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", location); }
            if (casetype != "" && casetype != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", casetype); }
            if (searchText != "") { sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", searchText); }
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

    public DataSet GetLastImportInfo()
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LAST_IMPORT_DATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

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