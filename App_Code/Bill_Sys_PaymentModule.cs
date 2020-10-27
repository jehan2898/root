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


public class Bill_Sys_PaymentModule
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_PaymentModule()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public int BillTrasaction(string szBillNo, string CheckNo, string checkdate, float ReceivedBill, float ReceivedInterest, string companyId, string Comment, string userid, string Billstatus, string flag, string PaymentId, string PaymentType, string PaymentValue, string sz_paid_type)
    {
        int i = 0;        
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NUMBER", CheckNo);
            sqlCmd.Parameters.AddWithValue("@DT_CHECK_DATE", checkdate);
            sqlCmd.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", ReceivedBill);
            sqlCmd.Parameters.AddWithValue("@mn_received_as_interest", ReceivedInterest);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMMENT", Comment);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", userid);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", Billstatus);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", PaymentId);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_TYPE", PaymentType);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_VALUE", PaymentValue);
            sqlCmd.Parameters.AddWithValue("@SZ_PAID_TYPE", sz_paid_type);

            //sqlCmd.ExecuteNonQuery();

            i = sqlCmd.ExecuteNonQuery();

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
        return i;
    }

    public bool deleteRecord(string p_szIDValue)
    {
        String szValue = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", p_szIDValue);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
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
        return true;
    }
    public void AddSoftwareFeeForPaymenttransction(string szBillNo, string CheckNo, string checkdate, float ReceivedBill, float ReceivedInterest, string companyId, string Comment, string userid, string Billstatus, string flag, string PaymentId, string PaymentType, string PaymentValue, string MN_SOFTWARE_FEE)
    {

        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_SOFWARE_FEE_TXN_PAYMENT_TRANSCTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NUMBER", CheckNo);
            sqlCmd.Parameters.AddWithValue("@DT_CHECK_DATE", checkdate);
            sqlCmd.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", ReceivedBill);
            sqlCmd.Parameters.AddWithValue("@mn_received_as_interest", ReceivedInterest);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMMENT", Comment);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", userid);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", Billstatus);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", PaymentId);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_TYPE", PaymentType);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_VALUE", PaymentValue);
            sqlCmd.Parameters.AddWithValue("@MN_SOFTWARE_FEE", MN_SOFTWARE_FEE);

            sqlCmd.ExecuteNonQuery();

       

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
      
    }

    public void DeleteRecordForSoftwareFee(string sz_payment_id, string sz_bill_no, string sz_company_id)
    {
        
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_ADD_SOFWARE_FEE_TXN_PAYMENT_TRANSCTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", sz_payment_id);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
            sqlCmd.ExecuteNonQuery();
            
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
    }

    public void BillTrasactionForWriteOff(string szBillNo, string CheckNo, string checkdate, float ReceivedBill, float ReceivedInterest, string companyId, string Comment, string userid, string Billstatus, string flag, string PaymentId, string PaymentType, string PaymentValue, string sz_writeoffvalue, string sz_paid_type)
    {
        
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS_FOR_WRITEOFF", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NUMBER", CheckNo);
            sqlCmd.Parameters.AddWithValue("@DT_CHECK_DATE", checkdate);
            sqlCmd.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", ReceivedBill);
            sqlCmd.Parameters.AddWithValue("@mn_received_as_interest", ReceivedInterest);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMMENT", Comment);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", userid);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", Billstatus);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", PaymentId);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_TYPE", PaymentType);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_VALUE", PaymentValue);
            sqlCmd.Parameters.AddWithValue("@SZ_WRITE_OFF_VALUE", sz_writeoffvalue);
            sqlCmd.Parameters.AddWithValue("@SZ_PAID_TYPE", sz_paid_type);
            sqlCmd.ExecuteNonQuery();


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
       
    }


}
