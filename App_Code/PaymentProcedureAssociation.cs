using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for PaymentProcedureAssociation
/// </summary>
public class PaymentProcedureAssociation
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    //public Payment_Procedure_Association_BO;

    public PaymentProcedureAssociation()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GetPaymentProcedureAssociation(Payment_Procedure_Association_DO objPayment_Proc_DO)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        ds = new DataSet();
        try
        {
          
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS_PROC_MAPPING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objPayment_Proc_DO.SZ_BILL_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objPayment_Proc_DO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(sqlCmd);

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

    public void SaveCode(ArrayList arrObj)
    {
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {
            Payment_Procedure_Association_DO objDel= new Payment_Procedure_Association_DO();
            objDel = (Payment_Procedure_Association_DO)arrObj[0];
            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS_PROC_MAPPING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Transaction = tr;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objDel.SZ_BILL_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", objDel.SZ_PAYMENT_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DEL");
            sqlCmd.ExecuteNonQuery();

            for (int i = 0; i < arrObj.Count; i++)
			{
                Payment_Procedure_Association_DO objAdd = new Payment_Procedure_Association_DO();
                objAdd = (Payment_Procedure_Association_DO)arrObj[i];

             sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS_PROC_MAPPING", sqlCon);
             sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Transaction = tr;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAdd.SZ_BILL_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", objAdd.SZ_PAYMENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAdd.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_CODE_ID", objAdd.SZ_PROC_CODE);
            sqlCmd.Parameters.AddWithValue("@SZ_AMOUNT_PAID", objAdd.SZ_AMOUNT_PAID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
			}

            tr.Commit();
        }
        catch (SqlException ex)
        {
            tr.Rollback();
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ;
    }

    public DataSet GetPaymentCode(Payment_Procedure_Association_DO objPayment_Proc_DO)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        ds = new DataSet();
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS_PROC_MAPPING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objPayment_Proc_DO.SZ_BILL_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", objPayment_Proc_DO.SZ_PAYMENT_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            sqlda=new SqlDataAdapter(sqlCmd);
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
    public void DeleteCodes(ArrayList arrObj)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        ds = new DataSet();
        
        try
        {
            sqlCon.Open();
            //Payment_Procedure_Association_DO objDel = new Payment_Procedure_Association_DO();
            Payment_Procedure_Association_DO objDel = (Payment_Procedure_Association_DO)arrObj[0];
            sqlCmd = new SqlCommand("SP_DELETE_ALL_PAYMENT_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", objDel.SZ_PAYMENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objDel.SZ_COMPANY_ID);
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
}