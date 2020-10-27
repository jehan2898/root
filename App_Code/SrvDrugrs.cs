using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;

public class SrvDrugrs
    {


        public DataSet GetDeliveryOrderList()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());


            DataSet ds = new DataSet();
            //string bill_no = "OZ5097";
            //string Company_ID = "CO00023";
            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("get_delivery_report_order_list", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
              

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return ds;
        }

       public DataSet GetDrugReports(string Case_ID,string Company_ID)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());


            DataSet ds= new DataSet();
            //string bill_no = "OZ5097";
            //string Company_ID = "CO00023";
            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("get_delivery_report", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", Case_ID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", Company_ID);

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
             
            }
            return ds;
    }

       public string Saveprintdelireceipt(string sz_company_Id, string sz_Case_Id ,string sz_User_Id, ArrayList arrPrint)
       {
           string szReturn = "";


          
           SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
           conn.Open();
           SqlTransaction transaction;
           transaction = conn.BeginTransaction();
           try
           {
               SqlCommand sqlCmd = new SqlCommand();
               sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
               sqlCmd.CommandText = "SP_INSERT_MST_PRINT_DELIVERY_RECEIPT";
               sqlCmd.CommandType = CommandType.StoredProcedure;
               sqlCmd.Connection = conn;
               sqlCmd.Transaction = transaction;
               sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_Case_Id);
               sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
               sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_User_Id);
              
               SqlDataReader dr;
               dr = sqlCmd.ExecuteReader();
               string id = "";
               while (dr.Read())
               {
                   id = dr[0].ToString();
               }
               dr.Close();
               for (int i = 0; i < arrPrint.Count; i++)
               {
                   print_delivery_receipt _print_delivery_receipt = new print_delivery_receipt();
                   _print_delivery_receipt  = (print_delivery_receipt)arrPrint[i];

                   SqlCommand sqlCmd1 = new SqlCommand();
                   sqlCmd1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                   sqlCmd1.CommandText = "SP_INSERT_TXN_PRINT_DELIVERY_RECEIPT";
                   sqlCmd1.CommandType = CommandType.StoredProcedure;
                   sqlCmd1.Connection = conn;
                   sqlCmd1.Transaction = transaction;
                   sqlCmd1.Parameters.AddWithValue("@SZ_PRINT_DELIVERY_ID", id);
                   sqlCmd1.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", _print_delivery_receipt.szProcCodeID);
                   sqlCmd1.Parameters.AddWithValue("@SZ_QUANTITY", _print_delivery_receipt.quantity);
                   sqlCmd1.Parameters.AddWithValue("@SZ_RECIEVE_DATE", _print_delivery_receipt.Date);
                   sqlCmd1.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
                   sqlCmd1.Parameters.AddWithValue("@SZ_CASE_ID", sz_Case_Id);
                   sqlCmd1.Parameters.AddWithValue("@SZ_USER_ID", sz_User_Id);


                   sqlCmd1.ExecuteNonQuery();
               }

              

               szReturn = "sucsess";
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

       public void RemovePrintdelivery(string sz_company_Id, string sz_Case_Id, string sz_User_Id, string PrintDeliveryID, string DeliveryRecieptNo)
       {
         



           SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
           conn.Open();
          
           
           try
           {
               SqlCommand sqlCmd = new SqlCommand();
               sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
               sqlCmd.CommandText = "sp_remove_delivery_receipt_code";
               sqlCmd.CommandType = CommandType.StoredProcedure;
               sqlCmd.Connection = conn;

               sqlCmd.Parameters.AddWithValue("@i_case_id", sz_Case_Id);
               sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_Id);
               sqlCmd.Parameters.AddWithValue("@sz_user_id", sz_User_Id);
               sqlCmd.Parameters.AddWithValue("@i_print_delivery_id", PrintDeliveryID);
              // sqlCmd.Parameters.AddWithValue("@sz_procedure_code_id", szProcCodeID);
               sqlCmd.Parameters.AddWithValue("@sz_devlivery_receipt_number", DeliveryRecieptNo);

               sqlCmd.ExecuteNonQuery();

           }
           catch (Exception ex)
           {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
           finally
           {
               conn.Close();
           }

          

       }


    }

