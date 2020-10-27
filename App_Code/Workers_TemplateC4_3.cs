using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

public class Workers_TemplateC4_3
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Workers_TemplateC4_3()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetDignosisCodeList(string billnumber, string flag)
    {
        ds = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILLING_INFORMATION_CFOUR_THREE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", flag);//"");
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


    public void SaveData(ArrayList arraylist)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PERMANENT_IMPAIREMENT_TRANSACTION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_IMPAIRMENT_RATING", arraylist[0].ToString());            
            comm.Parameters.AddWithValue("@SZ_BODY_PART", arraylist[1].ToString());
            if (arraylist[2].ToString() != "")
            {
                comm.Parameters.AddWithValue("@FLT_IMPAIRMENT_PERCENT", Convert.ToDecimal(arraylist[2].ToString()));
            }
            comm.Parameters.AddWithValue("@SZ_RELEVANT_DIAGNOSIS_TEST", arraylist[3].ToString());
            comm.Parameters.AddWithValue("@SZ_IMPAIRMENT_PERCENTAGE_DESC", arraylist[4].ToString());
            comm.Parameters.AddWithValue("@SZ_DISFIGUREMENT_DESC", arraylist[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMBINED_AGGREGATE_IMPAIRMENT", arraylist[6].ToString());
            comm.Parameters.AddWithValue("@SZ_COMBINED_AGGREGATE_PERCENT_DESC", arraylist[7].ToString());
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", arraylist[8].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteData( string impairmentid)
    {
         conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PERMANENT_IMPAIREMENT_TRANSACTION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", impairmentid);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetImpairmentData(string impairmentid)
    {

        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PERMANENT_IMPAIREMENT_TRANSACTION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", impairmentid);
            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");//"");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public DataSet GetTestImpairmentTransactionDetailList(string impairmentid)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PERMANENT_IMPAIREMENT_TEST_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", impairmentid);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

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

    public DataSet GetWorkStatusLimitation(string impairmentid)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PERMANENT_IMPAIREMENT_TEST_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", impairmentid);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

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

    public void SavePatientLimitations(ArrayList p_objAL)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PERMANENT_IMPAIREMENT_TEST_TRANSACTION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PERMANANT_IMPAIREMENT_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_LIMITATIONS_ID", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());            
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.Parameters.AddWithValue("@SZ_DESCRIPTION", p_objAL[5].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetWorkStatusLatestID(string billnumber)
    {
        string strPlanofcareid = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_MST_PERMANENT_IMPAIREMENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", "GETIMPAIRMENTID");
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strPlanofcareid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return strPlanofcareid;
    }
}
