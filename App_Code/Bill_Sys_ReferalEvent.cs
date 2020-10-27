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
public class Bill_Sys_ReferalEvent
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_ReferalEvent()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public String AddDoctor(ArrayList arrObj)
    {
        String returnvalue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_DOCTOR";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", arrObj[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", arrObj[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrObj[2].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.NVarChar);
            parmReturnValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(parmReturnValue);
            SqlDataReader dr;
            dr=comm.ExecuteReader();
            while (dr.Read())
            {
                returnvalue = dr[0].ToString();
            }
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return returnvalue;
    }

    public void AddDoctorAmount(ArrayList arrObj)
    {
        String returnvalue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_DOCTOR_PROCEDURE_AMOUNT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", arrObj[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", arrObj[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrObj[2].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", arrObj[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADDREGERRING");
           
            comm.ExecuteNonQuery();
           
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
       
    }

    public void AddPatientProc(ArrayList arrObj)
    {
        String returnvalue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PROCEDURE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", arrObj[0].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", arrObj[1].ToString());
            comm.Parameters.AddWithValue("@DT_DATE", arrObj[2].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", arrObj[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrObj[4].ToString());
            comm.Parameters.AddWithValue("@SZ_CODE", arrObj[6].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

      public void AddPatient(ArrayList arrayList)
    {
        conn = new SqlConnection(strConn);
        try
        {
            
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_PATIENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", arrayList[0].ToString());
            comm.Parameters.AddWithValue("@MI", arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", arrayList[2].ToString());
            comm.Parameters.AddWithValue("@I_PATIENT_AGE", arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_PHONE", arrayList[5].ToString());
            comm.Parameters.AddWithValue("@DT_DOB", arrayList[6].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_STATE", arrayList[7].ToString());
           // comm.Parameters.AddWithValue("@BT_TRANSPORTATION", arrayList[8]);
            comm.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", arrayList[8].ToString());
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[9].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void AddPatientCase(ArrayList arrayList)
    {
        conn = new SqlConnection(strConn);
        try
        {

            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CASE_MASTER";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arrayList[4].ToString());
            
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetOfficeName(string p_szOfficeID)
    {
        string szOfficeName = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_REFERAL_OFFICE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.Parameters.AddWithValue("@SZ_REFERAL_OFFICE_ID", p_szOfficeID);
            comm.Parameters.AddWithValue("@FLAG", "REFERAL_OFFICE_NAME");
            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szOfficeName = (dr[0].ToString());
            }
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { conn.Close(); }
        return szOfficeName;
    }

    public DataSet GetProcedureCode(string groupID, string companyID)
    {
        ds = new DataSet();
        string szOfficeName = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PROCEDURE_CODES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", groupID);
            comm.Parameters.AddWithValue("@FLAG", "GROUP_PROCEDURE_CODE_LIST");
            comm.CommandType = CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(ds);

           

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { conn.Close(); }
        return ds;
    }


    public DataSet GetProcedureCodeAndDescription(string groupID, string companyID)
    {
        ds = new DataSet();
        string szOfficeName = "";
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PROCEDURE_CODES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", groupID);
            comm.Parameters.AddWithValue("@FLAG", "GROUP_PROCEDURE_CODE_DESCRIPTION_LIST");
            comm.CommandType = CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(comm);

            sqlda.Fill(ds);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { conn.Close(); }
        return ds;
    }
}
