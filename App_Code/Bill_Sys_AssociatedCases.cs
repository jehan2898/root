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
/// Summary description for Bill_Sys_AssociatedCases
/// </summary>
public class Bill_Sys_AssociatedCases
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public Bill_Sys_AssociatedCases()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public ArrayList GetAssociatedCases(String p_szCaseID, String p_szCompanyID)
    {
        ArrayList _return = new ArrayList();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_ASSOCIATED_CASE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            if (ds.Tables[0] != null)
            {
                for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
                {
                    _return.Add(ds.Tables[0].Rows[i].ItemArray.GetValue(0));
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return _return;
    }

    public DataSet GetAssociatedProcedure(String p_szCaseID, String p_szCompanyID)
    {
        // ArrayList _return = new ArrayList();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            
            comm = new SqlCommand("GET_ASSCIATE_PROCEDURE_CODES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            
            sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(ds);
          
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public void InsertAssociatedCases(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_DIAGNOSIS_CODE_PROCEDURE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[2].ToString());

            comm.Parameters.AddWithValue("@FLAG", "ADD");       
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }
}
