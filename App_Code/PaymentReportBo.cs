using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;

public class PaymentReportBo
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataReader dr;

    public PaymentReportBo()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    

    public DataSet PaymentReportgrid(string CompanyID, string FromDate, string Todate, string FromVisitDate, string ToVisitDate, string Specialty, string provider, string BillNo)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_all_type_payment", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.Parameters.AddWithValue("@SZ_FROM_BILL_DATE", FromDate);
            comm.Parameters.AddWithValue("@SZ_TO_BILL_DATE", Todate);
            comm.Parameters.AddWithValue("@SZ_FROM_VISIT_DATE", FromVisitDate);
            comm.Parameters.AddWithValue("@SZ_TO_VISIT_DATE", ToVisitDate);
            comm.Parameters.AddWithValue("@SZ_SPECIALTY", Specialty);
            comm.Parameters.AddWithValue("@SZ_PROVIDER", provider);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNo);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
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

}