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
using System.IO;  
using System.Collections;

/// <summary>
/// Summary description for Bill_Sys_invoice
/// </summary>
public class Bill_Sys_invoice
{
    private string strsqlCon = null;
    SqlConnection conn;
    SqlCommand sqlCmd;
    SqlTransaction transaction;
    DataSet ds;
   
   
	public Bill_Sys_invoice()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
		
	}
    public string Saveinvoice(string sz_company_Id, string User_Id, ArrayList arrinvoice, string sz_invoice_path, string sz_invoice_file_name, string sz_total_amount, string sz_user_name, string sz_ip_address)
    {
        string szReturn="";
        string szRetImg = "";
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        transaction = conn.BeginTransaction();
        try
        {
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_software_invoice";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", User_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_PATH", sz_invoice_path);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_FILE_NAME", sz_invoice_file_name);
            sqlCmd.Parameters.AddWithValue("@sz_total_amount", sz_total_amount);
            SqlDataReader dr;
            dr = sqlCmd.ExecuteReader();
            string id = "";
            while (dr.Read())
            {
                id = dr[0].ToString();
            }
            dr.Close();
            for (int i = 0; i < arrinvoice.Count; i++)
            {
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "sp_save_software_invoice_bill";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", id);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", arrinvoice[i].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
                sqlCmd.ExecuteNonQuery();
            }
               
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "sp_save_invoice_imageid";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@sz_file_name", sz_invoice_file_name);
                sqlCmd.Parameters.AddWithValue("@sz_file_path", sz_invoice_path);
                SqlDataReader drimageid;
                drimageid = sqlCmd.ExecuteReader();
                string imageid = "";
                while (drimageid.Read())
                {
                    imageid = drimageid[0].ToString();
                }
                drimageid.Close();

                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "sp_update_invoice_image_id";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", id);
                sqlCmd.Parameters.AddWithValue("@SZ_IMAGE_ID", imageid);
                sqlCmd.ExecuteNonQuery();


                sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "sp_get_distinct_case";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", id);
                //sqlCmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter sa = new SqlDataAdapter(sqlCmd);
                sa.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sqlCmd = new SqlCommand();
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandText = "sp_save_invoice_images_for_case";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Connection = conn;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@i_image_id", imageid);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", sz_user_name);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id",ds.Tables[0].Rows[i]["sz_company_id"].ToString());
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", ds.Tables[0].Rows[i]["sz_case_id"].ToString());
                    sqlCmd.Parameters.AddWithValue("@sz_ip_address", sz_ip_address);
                    sqlCmd.ExecuteNonQuery();
                }

            szReturn = "sucsess,"+id.ToString();
            transaction.Commit();


        }

        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            szReturn = "fail";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;

    }


   


    public DataSet GetInvoicegenerateDetails(String sz_invoice_id, String sz_CompanyId)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("sp_get_invoice", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@INVOICE_ID", sz_invoice_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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

    public void Updateinvoicegenerate(string sz_company_id, string sz_invoice_id)
    {
        conn = new SqlConnection(strsqlCon);
        try
        {
           
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "update_invoice_generate";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", sz_invoice_id);
            sqlCmd.ExecuteNonQuery();
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
       
        
    
    }


    public DataSet GetInvoiceInfo(String sz_biil_number)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("sp_get_invoice_information", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_biil_number);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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
    public string saveinvoicetransaction(string sz_company_id, string sz_trans_desc, string sz_mncost,string sz_transactionype)
    {
       
        string szReturn = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_invoice_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_DESC", sz_trans_desc);
            sqlCmd.Parameters.AddWithValue("@MN_COST", sz_mncost);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANSACTION_TYPE", sz_transactionype);
            sqlCmd.ExecuteNonQuery();
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szReturn;

    
    }

    public string UpdateinvoiceTransaction(string sz_company_id, string sztransid, string sz_invoicedesc, string sz_invoice_cost, string sz_transaction_type)
    {
        string szReturntrans = "";
       
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "update_invoice_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICETRANS_ID", sztransid);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_DESC", sz_invoicedesc);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_COST", sz_invoice_cost);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANSACTION_TYPE", sz_transaction_type);
            sqlCmd.ExecuteNonQuery();
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
        return szReturntrans;



    }
    public void deleteinvoiceTransaction(string sz_company_id, string sztransid)
    {
       

        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_DELETE_INVOICE_TRANSACTION";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@i_transaction_invoice_id", sztransid);
            sqlCmd.ExecuteNonQuery();
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
      



    }

    public DataSet GetInvoiceTransdetails(string sz_CompanyId, string sztrantype)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICE_TRANSINFO", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANSACTION_TYPE", sztrantype);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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
    public DataSet Getcompanydetails(String sz_CompanyId)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("SP_GET_COMPANY_INFO", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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

    public DataSet Getcompanyaddress(string sztype)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("SP_GET_COMPANY_ADDRESS", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_type", sztype);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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


    public string SaveStorageinvoice(string sz_company_Id, string User_Id, ArrayList arrinvoice, string sz_invoice_path, string sz_invoice_file_name,string sz_storage_total_amount,  string sz_user_name, string sz_ip_address)
    {
        string szReturn = "";
        string szRetImg = "";
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        transaction = conn.BeginTransaction();
        try
        {
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_storage_invoice";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", User_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_PATH", sz_invoice_path);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_FILE_NAME", sz_invoice_file_name);
            sqlCmd.Parameters.AddWithValue("@sz_storage_total_amount", sz_storage_total_amount);
            SqlDataReader dr;
            dr = sqlCmd.ExecuteReader();
            string id = "";
            while (dr.Read())
            {
                id = dr[0].ToString();
            }
            dr.Close();
            for (int i = 0; i < arrinvoice.Count; i++)
            {
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "sp_save_storage_invoice_bill";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_STORAGE_INVOICE_ID", id);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", arrinvoice[i].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
                sqlCmd.ExecuteNonQuery();
            }

            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_storage_invoice_imageid";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@sz_file_name", sz_invoice_file_name);
            sqlCmd.Parameters.AddWithValue("@sz_file_path", sz_invoice_path);
            SqlDataReader drimageid;
            drimageid = sqlCmd.ExecuteReader();
            string imageid = "";
            while (drimageid.Read())
            {
                imageid = drimageid[0].ToString();
            }
            drimageid.Close();

            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_update_storage_invoice_image_id";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_STORAGE_INVOICE_ID", id);
            sqlCmd.Parameters.AddWithValue("@SZ_IMAGE_ID", imageid);
            sqlCmd.ExecuteNonQuery();


            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_get_distinct_case_storage_invoice";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_STORAGE_INVOICE_ID", id);
            //sqlCmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter(sqlCmd);
            sa.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "sp_save_storage_invoice_images_for_case";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@i_image_id", imageid);
                sqlCmd.Parameters.AddWithValue("@sz_user_name", sz_user_name);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", ds.Tables[0].Rows[i]["sz_company_id"].ToString());
                sqlCmd.Parameters.AddWithValue("@sz_case_id", ds.Tables[0].Rows[i]["sz_case_id"].ToString());
                sqlCmd.Parameters.AddWithValue("@sz_ip_address", sz_ip_address);
                sqlCmd.ExecuteNonQuery();
            }

            szReturn = "sucsess," + id.ToString();
            transaction.Commit();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            szReturn = "fail";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;

    }

    public DataSet GetInvoiceStoragegenerateDetails(String sz_storage_invoice_id, String sz_CompanyId)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("sp_get_invoice_storage", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@STORAGEINVOICE_ID", sz_storage_invoice_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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

    public void Updatestoragegenerate(string sz_company_id, string sz_invoice_id)
    {
        conn = new SqlConnection(strsqlCon);
        try
        {

            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "update_storage_generate";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_STORAGE_INVOICE_ID", sz_invoice_id);
            sqlCmd.ExecuteNonQuery();
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



    }

    public string Saveinvoicepaymenttransaction(string sz_payment_amount, string sz_checkno, string sz_notes, string dt_payment_date, string sz_userid, ArrayList arrpaytrans, string szcompanyid , string sztotalamount)
    {
        string szReturn = "";
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        transaction = conn.BeginTransaction();
        try
        {
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_invoice_payment_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_AMOUNT", sz_payment_amount);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NO", sz_checkno);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTES", sz_notes);
            sqlCmd.Parameters.AddWithValue("@DT_PAYMENT_DATE", dt_payment_date);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_userid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_TOTAL_AMOUNT", sztotalamount);
            SqlDataReader dr;
            dr = sqlCmd.ExecuteReader();
            string paymentid = "";
            while (dr.Read())
            {
                paymentid = dr[0].ToString();
            }
            dr.Close();

            for (int i = 0; i < arrpaytrans.Count; i++)
            {
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "SP_ADD_PAYMET_ID";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@i_invoice_id", Convert.ToInt32(arrpaytrans[i].ToString()));
                sqlCmd.Parameters.AddWithValue("@sz_company_id", szcompanyid);
                sqlCmd.Parameters.AddWithValue("@i_payment_id", paymentid.Trim());
                sqlCmd.ExecuteNonQuery();
            }

           

            szReturn = "sucsess," + paymentid.ToString();
            transaction.Commit();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            szReturn = "fail";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;

    }

    public string Savestoragepaymenttransaction(string sz_payment_amount, string sz_checkno, string sz_notes, string dt_payment_date, string sz_userid, ArrayList arrpaytranstorage, string szcompanyid,string sztotalamount)
    {
        string szReturn = "";
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        transaction = conn.BeginTransaction();
        try
        {
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_save_storage_payment_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_AMOUNT", sz_payment_amount);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NO", sz_checkno);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTES", sz_notes);
            sqlCmd.Parameters.AddWithValue("@DT_PAYMENT_DATE", dt_payment_date);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_userid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_TOTAL_AMOUNT", sztotalamount);
            SqlDataReader dr;
            dr = sqlCmd.ExecuteReader();
            string storagepaymentid = "";
            while (dr.Read())
            {
                storagepaymentid = dr[0].ToString();
            }
            dr.Close();

            for (int i = 0; i < arrpaytranstorage.Count; i++)
            {
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "SP_ADD_PAYMET_ID_FOR_STORAGE_PAYMENT_TRANSACTION";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@i_storage_invoice_id", Convert.ToInt32(arrpaytranstorage[i].ToString()));
                sqlCmd.Parameters.AddWithValue("@sz_company_id", szcompanyid);
                sqlCmd.Parameters.AddWithValue("@i_payment_id", storagepaymentid.Trim());
                sqlCmd.ExecuteNonQuery();
            }

            szReturn = "sucsess," + storagepaymentid.ToString();
            transaction.Commit();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            szReturn = "fail";

        }
        finally
        {
            conn.Close();
        }

        return szReturn;

    }

    public DataSet GetInvoicepaymnetDetails(string i_invoice_id)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("sp_get_invoice_payment_transaction", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_invoice_id", i_invoice_id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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


    public DataSet GetStorageInvoicepaymnetDetails(string i_storage_invoice_id)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("sp_get_storage_invoice_payment_transaction", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_storage_invoice_id", i_storage_invoice_id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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

    public void Updateinvoicepaymnettransaction(string sz_payment_amount, string dt_payment_date, string sz_checkno, string sz_notes, string i_softwarepaymentid)
    {
        conn = new SqlConnection(strsqlCon);
        try
        {

            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_update_payment_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_AMOUNT", sz_payment_amount);
            sqlCmd.Parameters.AddWithValue("@DT_PAYMENT_DATE", dt_payment_date);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NO", sz_checkno);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTES", sz_notes);
            sqlCmd.Parameters.AddWithValue("@I_SOFTWARE_PAYMENT_ID", i_softwarepaymentid);
            sqlCmd.ExecuteNonQuery();
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



    }


    public void Updatestorageinvoicepaymnettransaction(string sz_payment_amount, string dt_payment_date, string sz_checkno, string sz_notes, string i_storagepaymentid)
    {
        conn = new SqlConnection(strsqlCon);
        try
        {

            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_update_storage_payment_transaction";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_AMOUNT", sz_payment_amount);
            sqlCmd.Parameters.AddWithValue("@DT_PAYMENT_DATE", dt_payment_date);
            sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NO", sz_checkno);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTES", sz_notes);
            sqlCmd.Parameters.AddWithValue("@I_STORAGE_PAYMENT_ID", i_storagepaymentid);
            sqlCmd.ExecuteNonQuery();
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



    }


    public void deletesoftwarepaymentTransaction(string sz_paymentid, string sz_company_id)
    {


        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_DELETE_SOFTWARE_PAYMENT";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@I_PAYMENT_ID", sz_paymentid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.ExecuteNonQuery();
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




    }


    public void deletestoragepaymentTransaction(string sz_paymentid, string sz_company_id)
    {


        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_DELETE_STORAGE_PAYMENT";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@I_PAYMENT_ID", sz_paymentid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.ExecuteNonQuery();
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




    }

    public void deletesoftwareinvoice(string sz_invoiceid, string sz_company_id)
    {


        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_delete_software_invoice";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@i_invoice_id", sz_invoiceid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.ExecuteNonQuery();
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




    }

    public void deletestorageinvoice(string sz_storage_invoice_id, string sz_company_id)
    {


        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "sp_delete_storage_invoice";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;
            sqlCmd.Parameters.AddWithValue("@i_storage_invoice_id", sz_storage_invoice_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.ExecuteNonQuery();
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




    }

    public DataSet GetsoftwareInvoiceId(string i_payment_id,string sz_company_id)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICE_ID_USING_PAYMENT_ID", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", i_payment_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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

    public DataSet GetStorageInvoiceId(string i_payment_id, string sz_company_id)
    {
        conn = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            conn.Open();
            sqlCmd = new SqlCommand("SP_GET_STORAGE_INVOICE_ID_USING_PAYMENT_ID", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", i_payment_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
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
