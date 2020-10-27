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

public class BillSearchDAO
{

    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public BillSearchDAO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}


    public DataSet getBillSearchList(ArrayList objAL)
    {
        DataSet objDS = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();

            //objAL.Add(txtBillNumber.Text);
            //objAL.Add(txtCaseNO.Text);
            //objAL.Add(txtFromDate.Text);
            //objAL.Add(txtToDate.Text);
            //objAL.Add(txtCompanyID.Text);
            sqlCmd = new SqlCommand("SP_BILL_SEARCH", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            if(objAL[0].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());

            if (objAL[1].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());

            if (objAL[2].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objAL[2].ToString());

            if (objAL[3].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE_TO", objAL[3].ToString());

            if (objAL[4].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[4].ToString());

            if (objAL[5].ToString() != "")
                sqlCmd.Parameters.AddWithValue("@SZ_PREFIX", objAL[5].ToString());

         //   sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            
            
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }

    public int BillTrasaction(string szBillNo, string CheckNo,
      string checkdate, string checkamount, string companyId,
      string Comment, string userid, string Billstatus,
       string flag, string PaymentId)
    {
        int i = 0;
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
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
            sqlCmd.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", checkamount);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMMENT", Comment);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", userid);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", Billstatus);
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", PaymentId);


            //sqlCmd.ExecuteNonQuery();

            i = sqlCmd.ExecuteNonQuery();

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
        return i;
    }



  
    public DataSet paymenttype(string szBillNo, string companyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PAYMENT_TYPE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlda = new SqlDataAdapter(sqlCmd);
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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
   
    public string BillName(string sz_Bill_No, string sz_CompanyId,string flag)
    {
        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INS_NAME_USING_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", sz_Bill_No);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
         
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

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
        return szReturn;
    }


    public string InsertNotes(string szBillNotes, string BillNo)
    {

        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_BILL_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NOTES", szBillNotes);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNo);


            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

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
        return szReturn;

    }

    public string GetInsertTransport(string sztransportname, string transportid, string sz_companyid, string sz_caseid, string dt_trasdate, string dt_trastimehh,string dt_trastimemm,string sz_trantype)
    {

        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_DELETE_TRANS_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_NAME", sztransportname);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_ID", transportid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_caseid);
            sqlCmd.Parameters.AddWithValue("@DT_TRANS_DATE", dt_trasdate);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_TIME_HH", dt_trastimehh);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_TIME_MM", dt_trastimemm);
            sqlCmd.Parameters.AddWithValue("@SZ_TRANS_TIME_TYPE", sz_trantype);
            sqlCmd.Parameters.AddWithValue("@SZ_FALG", "ADD");


            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

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
        return szReturn;

    }

    public DataSet getTransportinfo(string szCaseid, string companyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_DELETE_TRANS_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_FALG", "LIST");
            sqlda = new SqlDataAdapter(sqlCmd);
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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }

    public void Delete_Trans_Data(ArrayList arrTransID, string szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction sqltran;
        sqltran = sqlCon.BeginTransaction();
        try
        {
            for (int i = 0; i < arrTransID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_ADD_DELETE_TRANS_INFO", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqltran;
                sqlCmd.Parameters.AddWithValue("@I_TRANS_ID", arrTransID[i].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@SZ_FALG", "DELETE");
                sqlCmd.ExecuteNonQuery();
            }
            sqltran.Commit();
        }
        catch(Exception ex)
        {
            sqltran.Rollback();
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



    public string GetInsertNotes(string szBillNotes, string BillNo)
    {

        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NOTES", szBillNotes);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNo);


            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

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
        return szReturn;

    }

    public string getDefaultPayer(string sz_CompanyId)
    {
        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_default_payer", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

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
        return szReturn;
    }
}
