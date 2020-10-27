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
/// Summary description for Bill_Sys_VisitTreatmentBO
/// </summary>
public class Bill_Sys_VisitTreatmentBO
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    private ArrayList _arrayList;

    public Bill_Sys_VisitTreatmentBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public  DataSet GetTotalList(string companyID,string flag )
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet GetDoctorProcAmountList(string flag, string szTypeID, string szDoctorID, string szCompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", szTypeID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

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
        return ds;
    }

    public void SaveList(ArrayList arraylist)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arraylist[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", arraylist[1].ToString());       
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[2].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
            
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", arraylist[2].ToString());
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
        return ds;
    }


    public string GetDoctorName(string szDoctorID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        string szDoctorName = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_BILL_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorID);
  
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_DOCTOR_NAME");
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                szDoctorName = dr[0].ToString();
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
        return szDoctorName;

    }
    
}
