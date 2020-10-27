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
/// Summary description for Bill_Sys_BillSearchDocuments
/// </summary>
public class Bill_Sys_BillSearchDocuments
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public Bill_Sys_BillSearchDocuments()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GET_BillSearch_Documents(string billNumber,int documentType)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_bill_search_docs", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billNumber);
            sqlCmd.Parameters.AddWithValue("@I_DOCUMENT_TYPE", documentType);
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
}