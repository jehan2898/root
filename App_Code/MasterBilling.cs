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
public class MasterBilling
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public MasterBilling()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public string getProcedureCode(string szBillNumber)
    {
        string szReturnID = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();

        try
        {
            sqlCmd = new SqlCommand("sp_get_group_code", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_bill_number", szBillNumber);
            sqlCmd.CommandTimeout = 0;
            

            SqlDataReader objDR = sqlCmd.ExecuteReader();

            while (objDR.Read())
            {
                szReturnID = objDR["sz_procedure_group_code"].ToString();
            }
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
        return szReturnID;

    }

    public string getProcedureGroupID(string szBillNumber)
    {
        string szReturnID = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();

        try
        {
            sqlCmd = new SqlCommand("sp_get_group_name_using_bill_no", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_bill_number", szBillNumber);
            sqlCmd.CommandTimeout = 0;


            SqlDataReader objDR = sqlCmd.ExecuteReader();

            while (objDR.Read())
            {
                szReturnID = objDR["sz_speciality_id"].ToString();
            }
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
        return szReturnID;

    }
}