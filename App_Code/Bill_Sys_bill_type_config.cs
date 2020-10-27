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



public class Bill_Sys_bill_type_config
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    public Bill_Sys_bill_type_config()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    

    public void AddCasetypewithbill(string sz_company_id, string sz_casetype_id, string sz_user_id, string sz_billabbriid, string sz_billabbrivation, string sz_bitbillabbrivation)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_UPDATE_CASE_TYPE_BILL_CONFIG", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            comm.Parameters.AddWithValue("@SZ_BILLTYPE_ABBRIVATION_ID", sz_billabbriid);
            comm.Parameters.AddWithValue("@SZ_BILLTYPE_ABBRIVATION", sz_billabbrivation);
            comm.Parameters.AddWithValue("@BT_BILLTYPE_ABBRIVATION", sz_bitbillabbrivation);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public DataSet GetCaseTypewithbillList(string sz_CompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_CASETYPE_WITH_BILL", sqlCon);
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

    public void UpdateCasetypewithbill(string sz_company_id, string sz_casetype_id, string sz_billabbriid, string sz_billabbrivation, string sz_bitbillabbrivation)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_UPDATE_CASE_TYPE_BILL_CONFIG", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
           //comm.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            comm.Parameters.AddWithValue("@SZ_BILLTYPE_ABBRIVATION_ID", sz_billabbriid);
            comm.Parameters.AddWithValue("@SZ_BILLTYPE_ABBRIVATION", sz_billabbrivation);
            comm.Parameters.AddWithValue("@BT_BILLTYPE_ABBRIVATION", sz_bitbillabbrivation);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
        //Method End


    }

    public void DeleteCasetypewithbill(string sz_company_id, string sz_casetype_id)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_UPDATE_CASE_TYPE_BILL_CONFIG", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_casetype_id);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { sqlCon.Close(); }
        //Method End


    }

}
