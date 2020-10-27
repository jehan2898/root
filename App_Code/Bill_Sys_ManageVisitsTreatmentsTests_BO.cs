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
/// Summary description for Bill_Sys_ManageVisitsTreatmentsTests_BO
/// </summary>
public class Bill_Sys_ManageVisitsTreatmentsTests_BO
{

    private string strsqlCon = null;
	public Bill_Sys_ManageVisitsTreatmentsTests_BO()
	{
		//
		// TODO: Add constructor logic here
		//
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetDoctorSpecificTypeList(ArrayList arraylist)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arraylist[0].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", "");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", arraylist[2].ToString());
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

    public DataSet GetDoctorSpecific_TypeList(ArrayList arraylist)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arraylist[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", arraylist[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", arraylist[3].ToString());
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

    public DataSet GetReferringProcCodeList(ArrayList arraylist)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_TXN_GET_REFERRING_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", arraylist[1].ToString());
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

    public DataSet GetAllProcCodeLHR(string sz_company_id)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_TXN_GET_REFERRING_PROC_CODE_LHR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
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
