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



public class Bill_Sys_BillingCompanyDetails_BO
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_BillingCompanyDetails_BO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public int iSearchRecordCount;


        public int SearchRowCount()
        {
            return iSearchRecordCount;
        }

    public ArrayList getCompanyDetails(String p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_BILL_COMPANY_REPORT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));
                _return.Add(Convert.ToString(dr[3]));
                _return.Add(Convert.ToString(dr[4]));
                _return.Add(Convert.ToString(dr[5]));
                _return.Add(Convert.ToString(dr[6]));
                _return.Add(Convert.ToString(dr[7]));
                _return.Add(Convert.ToString(dr[8]));
                _return.Add(Convert.ToString(dr[9]));
                _return.Add(Convert.ToString(dr[10]));
                _return.Add(Convert.ToString(dr[11]));
                _return.Add(Convert.ToString(dr[12]));
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;

    }

    public void UpdateBillTransaction(string billid, int writeoff, string comment)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_LATEST_BILL_TRANSACTIONS", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@BIT_WRITE_OFF_FLAG", writeoff);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billid);
            comm.Parameters.AddWithValue("@SZ_COMMENT", comment);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet SearchBills(string flag,string companyid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_CASE_MASTER", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;         
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", flag);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetPaymentList(string id)
    {
       
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BULKPAYMENT_TRANSACTIONS", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ARRAY_ID", id);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return null;
    }

    public void UpdatePaymentList(string SZ_BILL_ID, string SZ_PAYMENT_ID, string SZ_CHECK_NUMBER, DateTime DT_CHECK_DATE, decimal FLT_CHECK_AMOUNT, int I_PAYMENT_STATE,string SZ_COMMENT, string SZ_COMPANY_ID)
    {

        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_PAYMENT_ID", SZ_PAYMENT_ID);
            comm.Parameters.AddWithValue("@SZ_BILL_ID", SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_CHECK_NUMBER", SZ_CHECK_NUMBER);
            comm.Parameters.AddWithValue("@DT_CHECK_DATE", DT_CHECK_DATE);
            comm.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", FLT_CHECK_AMOUNT);
            comm.Parameters.AddWithValue("@I_PAYMENT_STATE", I_PAYMENT_STATE);
            comm.Parameters.AddWithValue("@SZ_COMMENT", SZ_COMMENT);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.ExecuteNonQuery();
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public ArrayList GetDifference(string id)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_PAYMENT_TRANSACTIONS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", "GETDIFFERENCE");
            comm.Parameters.AddWithValue("@SZ_BILL_ID", id);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public decimal GetBalance(string id)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            decimal _return =0;
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_BALANCE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", id);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
               if (dr[0]!=DBNull.Value){_return=Convert.ToDecimal(dr[0]);}
               
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
             
        }
        
        finally { conn.Close(); }
        return 0;
    }

    public void SaveData(ArrayList arrParam)
    {
        conn = new SqlConnection(strConn);
        DataSet  _dataSet;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", arrParam[0].ToString());
            comm.Parameters.AddWithValue("@SZ_CHECK_NUMBER", arrParam[1].ToString());
            comm.Parameters.AddWithValue("@DT_CHECK_DATE", arrParam[2].ToString());
            comm.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", arrParam[3].ToString());
            comm.Parameters.AddWithValue("@CHR_PAYMENT_TYPE", arrParam[4].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrParam[5].ToString());
       

            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void UpdatePaymentTransaction(ArrayList arrParam)
    {
        conn = new SqlConnection(strConn);
        DataSet _dataSet;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_PAYMENT_ID", arrParam[0].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_ID", arrParam[1].ToString());
            comm.Parameters.AddWithValue("@SZ_CHECK_NUMBER", arrParam[2].ToString());
            comm.Parameters.AddWithValue("@DT_CHECK_DATE", arrParam[3].ToString());
            comm.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", arrParam[4].ToString());
            comm.Parameters.AddWithValue("@CHR_PAYMENT_TYPE", arrParam[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arrParam[6].ToString());


            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetLatestBillID(string id)
    {
        string strBillID = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", id);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            return strBillID;
        }
        catch (Exception ex)
        {
            
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return null;
    }
    public string GetCaseID(string SZ_BILL_ID, string id)
    {
        string strCaseID = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", "GETCASEID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", id);
            comm.Parameters.AddWithValue("@SZ_BILL_ID", SZ_BILL_ID);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strCaseID = Convert.ToString(dr[0]);
            }
            return strCaseID;
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return null;
    }

    public void SaveIC9CodeData(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILL_IC9_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_IC9_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@I_UNIT", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@FLT_AMOUNT", _arrayList[3].ToString());

            comm.Parameters.AddWithValue("@SZ_DESCRIPTION", _arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[5].ToString());


            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();


        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public ArrayList GetIC9CodeData(string id)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILL_IC9_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            ArrayList _return = new ArrayList();


            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            comm.Parameters.AddWithValue("@SZ_BILL_ID", id);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));
                _return.Add(Convert.ToString(dr[3]));
                _return.Add(Convert.ToString(dr[4]));
                _return.Add(Convert.ToString(dr[5]));
                _return.Add(Convert.ToString(dr[6]));

            }

            return _return;
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet Litigation_WriteOff_Desk(string companyid, int flag,string BillStatus)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_LITIGATION_WRITEOFF_DESK", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", flag);//
            comm.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", BillStatus);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

   

    public void UpdateIC9CodeList(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILL_IC9_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_IC9_CODE_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_IC9_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_ID", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@I_UNIT", _arrayList[3].ToString());
            comm.Parameters.AddWithValue("@FLT_AMOUNT", _arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_DESCRIPTION", _arrayList[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[6].ToString());


            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet  BindIC9CodeData(string id)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILL_IC9_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", id);
            comm.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetPerDayUpcomingEvent(string p_szcaseid,string p_szcompanyid,string p_szDate)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            string _return = "";//"<table width='100%'>";
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szcaseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szcompanyid);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_szDate);
            comm.Parameters.AddWithValue("@FLAG", "GETEVENTLIST_COUNT");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) 
                {
                    if (dr[0].ToString() != "0")
                    {
                        _return += "<a href='#' style='text-decoration:none;color:WhiteSmoke;' onclick=javascript:OpenDayViewCalender('" + p_szDate + "');><img src='Images/appmnt.jpg' style='cursor:pointer; text-decoration:none;' border='0'/> ";
                        _return +=  dr[0].ToString() + "</a>";
                        //_return += "</td></tr>";
                    }
                }
            }

            //_return += "</table>";

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetPerDayWithTimeEvent(string p_szcaseid, string p_szcompanyid, string p_szDate,int time)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            string _return = "";//"<table width='100%'>";
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szcaseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szcompanyid);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_szDate);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", time);
            comm.Parameters.AddWithValue("@FLAG", "GETEVENTLIST_COUNTWITHTIME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        _return += "<a href='#' style='text-decoration:none;color:WhiteSmoke;' onclick=javascript:OpenDayViewCalender('" + p_szDate + "');><img src='Images/appmnt.jpg' style='cursor:pointer; text-decoration:none;' border='0'/> ";
                        _return += dr[0].ToString()+"</a>";
                        //_return += "</td></tr>";
                    }
                }
            }

            //_return += "</table>";
            return _return;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetPerDayEvent(string p_szcaseid, string p_szcompanyid, string p_szDate)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            string _return = "";//"<table width='100%'>";
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szcaseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szcompanyid);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_szDate);
            comm.Parameters.AddWithValue("@FLAG", "GETEVENTLIST_COUNT");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        _return += "<img src='Images/appmnt.jpg' style='cursor:pointer; text-decoration:none;' border='0'/> ";
                        _return += dr[0].ToString();
                        //_return += "</td></tr>";
                    }
                }
            }

            //_return += "</table>";

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetPerDayEvent(string p_szcaseid, string p_szcompanyid, string p_szDate,string doctorName,string providerName)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            string _return = "";//"<table width='100%'>";
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szcaseid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szcompanyid);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_szDate);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorName);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", providerName);
            comm.Parameters.AddWithValue("@FLAG", "GETEVENTLIST_COUNT");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        _return += "<img src='Images/appmnt.jpg' style='cursor:pointer; text-decoration:none;' border='0'/> ";
                        _return += dr[0].ToString();
                        //_return += "</td></tr>";
                    }
                }
            }

            //_return += "</table>";

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetPerDayEventForCompany(string p_szcompanyid, string p_szDate)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            string _return = "<table width='100%'>";
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szcompanyid);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_szDate);
            comm.Parameters.AddWithValue("@FLAG", "COMPANY_GETEVENTLIST_COUNT");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                   //     _return += "<tr><td width='100%'><img src='Images/appmnt.jpg' onclick=javascript:OpenDayViewCalender('" + p_szDate + "'); style='cursor:pointer;' /> ";
                        _return += "<tr><td width='100%'><a onclick=javascript:OpenDayViewCalender('" + p_szDate + "'); style='cursor:pointer;' ><img src='Images/appmnt.jpg' />";
                        _return += dr[0].ToString();
                        _return += "</a></td></tr>";
                    }
                }
            }

            _return += "</table>";

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetInsuranceCompanyID(string billnumber,string storeproc,string flag)
    {
        string strBillID = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = storeproc;
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            return strBillID;
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }

        finally { conn.Close(); }
        return null;
    }

    public DataSet GetDignosisCodeList(string billnumber,string flag)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_BILLING_INFORMATION", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billnumber);
            comm.Parameters.AddWithValue("@FLAG", flag);//"");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public Bill_Sys_BillingCompanyObject getCompanyDetailsOfCase(string p_szCaseID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            Bill_Sys_BillingCompanyObject _return = new Bill_Sys_BillingCompanyObject(); ; 
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_COMPANY_DETAILS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
              
                _return.SZ_COMPANY_ID = Convert.ToString(dr[0]);
                _return.SZ_COMPANY_NAME = Convert.ToString(dr[1]);
                _return.SZ_PREFIX = Convert.ToString(dr[2]);
                _return.BT_REFERRING_FACILITY= Convert.ToBoolean(dr[3]);
               
            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;

    }

    public DataSet getBillingDesk(string companyid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_BILLING_DESK", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
          
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet getUnsentNF2(string p_szCompanyID, string p_szStatus)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UNSENT_NF2", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@I_STATUS", p_szStatus);
            
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }
    public DataSet getUnsentNF2(string p_szCompanyID,string p_szStatus,string LocationId,string caseTypeId)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UNSENT_NF2", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@I_STATUS", p_szStatus);
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", LocationId);
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", caseTypeId);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    iSearchRecordCount = ds.Tables[0].Rows.Count;
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public string getNF2CaseType(string p_szCompanyID)
    {
        conn = new SqlConnection(strConn);
        string caseTypeId = "";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_CASE_TYPE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NF2_CASE_TYPE");
            sqlda = new SqlDataAdapter(comm);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        caseTypeId += dr[0].ToString();
                    }
                }
            }
            return caseTypeId;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet Litigation_BillNumber_search(string _companyID, string _billNumber, string flag)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_LITIGATION_WRITEOFF_DESK", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", _billNumber);
            comm.Parameters.AddWithValue("@FLAG", flag);
        
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally
        {
            conn.Close();
        }
        return null;
    }

    public DataSet getPrintPOM(string p_szCompanyID)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_PRINT_POM", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet getPrintPOM(string p_szCompanyID,string p_szLocationID)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_PRINT_POM", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", p_szLocationID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    
    public DataSet GetBillingCompanyInfo(string CompanyId)
    {
        conn = new SqlConnection(strConn);
        try 
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_BILLING_COMPANY", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        return null;
    }


    public DataSet GetOfficeWisePatientInfo(string P_Company_Id, string P_StartDate, string P_EndDate, string P_OfficeId, string P_DocorId, string P_Status)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("GET_OFFICEWISE_SHOW_REPORT", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_StartDate != "" && P_StartDate != "NA") { comm.Parameters.AddWithValue("@DT_START_DATE", P_StartDate); }
            if (P_EndDate != "" && P_EndDate != "NA") { comm.Parameters.AddWithValue("@DT_END_DATE", P_EndDate); }
            if (P_OfficeId != "" && P_OfficeId != "NA" && P_OfficeId != "--- Select ---") { comm.Parameters.AddWithValue("@SZ_OFFICE_ID", P_OfficeId); }
            if (P_DocorId != "" && P_DocorId != "NA" && P_DocorId != "---Select---") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", P_DocorId); }
            if (P_Status != "" && P_Status != "NA") { comm.Parameters.AddWithValue("@I_STATUS", P_Status); } 
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        return null;
    }



    public DataSet GetTypeWisePatientCount(string P_SZ_OFFICE_ID, string P_SZ_PROCEDURE_GROUP_ID, string P_I_STATUS, string P_StartDate, string P_EndDate, string P_DocorId)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("GET_VISIT_TYPE_WISE_COUNT", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
             
            if (P_SZ_OFFICE_ID != "") { comm.Parameters.AddWithValue("@SZ_OFFICE_ID", P_SZ_OFFICE_ID); }
            if (P_SZ_PROCEDURE_GROUP_ID != "" && P_SZ_PROCEDURE_GROUP_ID != "NA") { comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", P_SZ_PROCEDURE_GROUP_ID); }
            if (P_I_STATUS != "" && P_I_STATUS != "NA") { comm.Parameters.AddWithValue("@I_STATUS", P_I_STATUS); }
            if (P_StartDate != "" && P_StartDate != "NA") { comm.Parameters.AddWithValue("@DT_START_DATE", P_StartDate); }
            if (P_EndDate != "" && P_EndDate != "NA") { comm.Parameters.AddWithValue("@DT_END_DATE", P_EndDate); }
            if (P_DocorId != "" && P_DocorId != "NA" && P_DocorId != "---Select---") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", P_DocorId); }
      
            comm.Parameters.AddWithValue("@FLAG", "COUNTTYPE");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        return null;
    }

    public DataSet GetCompanyWiseRoomCount(string P_Company_Id)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("GET_VISIT_TYPE_WISE_COUNT", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            comm.Parameters.AddWithValue("@FLAG", "COUNTROOM");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        return null;
    }

    public DataSet GetProviderAddress(string S_Office_Id)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_PROVIDER_ADDRESS", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", S_Office_Id);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        return null;
    }

   /* public int InsertUserMasterDetails(string sz_UserName, string userrole,string password,string emailId,string billid)
    {
        conn = new SqlConnection(strConn);
        int result=0;
        try
        {
           
            conn.Open();
            comm = new SqlCommand("SP_MST_USERS_Output", conn);
           
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", sz_UserName);
            comm.Parameters.AddWithValue("@SZ_PASSWORD", password);
            comm.Parameters.AddWithValue("@SZ_USER_ROLE", userrole);
           // sqlCmd.Parameters.AddWithValue(" @SZ_PROVIDER_ID", userrole);
            comm.Parameters.AddWithValue("@SZ_EMAIL", emailId);
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", billid);
            result= comm.ExecuteNonQuery();
         
           
        }
               
        catch (Exception _ex)
        {
            _ex.Message.ToString();
        }
    
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
              
            }
        }

        return result;
    }*/

    public int InsertUserMasterDetails(ArrayList array)
    {
        conn = new SqlConnection(strConn);
        int result = 0;
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_MST_USERS_Output", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", array[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PASSWORD", array[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ROLE", array[1].ToString());
            comm.Parameters.AddWithValue("@SZ_EMAIL", array[3].ToString());
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", array[4].ToString());
            try{
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", array[5].ToString());
            }
            catch{}
            comm.Parameters.AddWithValue("@IS_PROVIDER", array[6].ToString());
            comm.Parameters.AddWithValue("@BT_VALIDATE_AND_SHOW", array[7].ToString());
            if (array.Count > 8)
            {
                comm.Parameters.AddWithValue("@sz_created_by", array[8].ToString());
            }
            //comm.Parameters.AddWithValue("@sz_reffprov_id", array[8].ToString());
                result = comm.ExecuteNonQuery();
            if (result == 1)
            {
                return result;
            }
            else if (result == -1)
            {
                return result;
            }

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

        return result;
    }

  

    public int UpdateUserMasterDetails(ArrayList array)
    {
        conn = new SqlConnection(strConn);
        int result = 0;
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_MST_USERS_UPDATE", conn);

            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", array[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PASSWORD", array[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ROLE", array[1].ToString());
            comm.Parameters.AddWithValue("@SZ_EMAIL", array[3].ToString());
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", array[4].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ID", array[5].ToString());
            try{
                comm.Parameters.AddWithValue("@SZ_OFFICE_ID", array[6].ToString());
            }
            catch{}
            comm.Parameters.AddWithValue("@BT_VALIDATE_AND_SHOW", array[7].ToString());
            result = comm.ExecuteNonQuery();
            
            if (result == 1)
            {
                return result;
            }
            else if (result == -1)
            {
                return result;
            }
           
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

        return result;
        
    }



    public void InsertBillPaymentImages(ArrayList array)
    {
        conn = new SqlConnection(strConn);
      
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_TXN_BILL_PAYMENT_IMAGES", conn);          
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", array[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", array[1].ToString());
            comm.Parameters.AddWithValue("@I_IMAGE_ID", array[2].ToString());
            comm.Parameters.AddWithValue("@SZ_CREATED_USER_ID", array[3].ToString());
            comm.Parameters.AddWithValue("@SZ_PAYMENT_ID", array[4].ToString());
             comm.ExecuteNonQuery();
           

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

    public string GetBillStatusID(string sz_Company_Id,string sz_Bill_Status_Code)
    {
        string StatusID = "";
        
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_BILL_STAATUS_ID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            comm.Parameters.AddWithValue("@SZ_BILL_SATAUS_CODE", sz_Bill_Status_Code);
            dr = comm.ExecuteReader();
            while(dr.Read())
            {
                StatusID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); dr.Close(); }
        return StatusID;
    }

    public string GetBillStatusCode(string sz_Company_Id, string sz_BillNumber)
    {
        string StatusID = "";

        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_CURRENT_BILL_STATUS_CODE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANYID", sz_Company_Id);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_BillNumber);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                StatusID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); dr.Close(); }
        return StatusID;
    }

    public string GetRefDocID(string sz_Company_Id, string sz_UserID)
    {
        string StatusID = "";

        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_REF_OFF_ID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", sz_Company_Id);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                StatusID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        finally { conn.Close(); dr.Close(); }
        return StatusID;
    }

    public void UpdateDiagnosysPage(string SZ_USER_ID, string SZ_DIAGNOSYS_PAGE)
    {
        conn = new SqlConnection(strConn);
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_GET_USER_DIAGNOSYS_PAGE_FLAG", conn);

            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ID", SZ_USER_ID);
            comm.Parameters.AddWithValue("@BT_DIAGNOSYS_PAGE", SZ_DIAGNOSYS_PAGE);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
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

    public DataSet CompanyInfo(string companyid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_COMPANY_IFO", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }

    public void UpdateCompanyInfo( string SZ_ADDRESS_STREET, string SZ_ADDRESS_STATE, string SZ_ADDRESS_CITY, string SZ_ADDRESS_ZIP, string SZ_OFFICE_PHONE, string SZ_OFFICE_EMAIL, string SZ_CONTACT_FIRST_NAME, string SZ_CONTACT_LAST_NAME, string SZ_ADMIN_EMAIL, string SZ_CONTACT_OFFICE_PHONE, string SZ_CONTACT_OFFICE_EXTN, string SZ_CONTACT_EMAIL, string SZ_COMPANY_ID)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UPDATE_COMPANY", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_ADDRESS_STREET", SZ_ADDRESS_STREET);
            comm.Parameters.AddWithValue("@SZ_ADDRESS_STATE", SZ_ADDRESS_STATE);
            comm.Parameters.AddWithValue("@SZ_ADDRESS_CITY", SZ_ADDRESS_CITY);
            comm.Parameters.AddWithValue("@SZ_ADDRESS_ZIP", SZ_ADDRESS_ZIP);
            comm.Parameters.AddWithValue("@SZ_OFFICE_PHONE", SZ_OFFICE_PHONE);
            comm.Parameters.AddWithValue("@SZ_OFFICE_EMAIL", SZ_OFFICE_EMAIL);
            comm.Parameters.AddWithValue("@SZ_CONTACT_FIRST_NAME", SZ_CONTACT_FIRST_NAME);
            comm.Parameters.AddWithValue("@SZ_CONTACT_LAST_NAME", SZ_CONTACT_LAST_NAME);
            comm.Parameters.AddWithValue("@SZ_ADMIN_EMAIL", SZ_ADMIN_EMAIL);
            comm.Parameters.AddWithValue("@SZ_CONTACT_OFFICE_PHONE", SZ_CONTACT_OFFICE_PHONE);
            comm.Parameters.AddWithValue("@SZ_CONTACT_OFFICE_EXTN", SZ_CONTACT_OFFICE_EXTN);
            comm.Parameters.AddWithValue("@SZ_CONTACT_EMAIL", SZ_CONTACT_EMAIL);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public DataSet GetValidateandallowfordoctor(string sz_companyid, string sz_user_id)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_validate_allow_details", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet getRefferingProviderList(string companyid)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_OFFICE_REFF", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "OFFICE_LIST");

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public void InsertUserReffProvider(ArrayList objReffProv, string latestUserID, string companyID, string userID, string operation)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            for (int i = 0; i < objReffProv.Count; i++)
            {
                comm = new SqlCommand("sp_save_user_provider_connection", conn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@sz_latest_user_id", latestUserID);
                comm.Parameters.AddWithValue("@sz_reffering_provider_id", objReffProv[i].ToString());
                comm.Parameters.AddWithValue("@sz_company_id", companyID);
                comm.Parameters.AddWithValue("@sz_user_id", userID);
                comm.Parameters.AddWithValue("@flag", operation);

                comm.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet getUserRefferingProviderList(string userID)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_user_reff_providers", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_user_id", userID);
            
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally { conn.Close(); }
        return null;
    }

    public void DeleteUserReffProvider(string companyID, string userID)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand("sp_delete_user_reff_provider_connection", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_user_id", userID);
            
            comm.ExecuteNonQuery();
        
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet BindProviders(string companyID)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_OFFICE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", companyID);
            comm.Parameters.AddWithValue("@flag", "OFFICE_LIST_PROVIDER");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }

        finally { conn.Close(); }
        return null;
    }

    public DataSet getProviders(string companyID, string UserID)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("getUserProvider", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_user_id", UserID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        
        finally { conn.Close(); }
        return null;
    }

    public int SaveJFKBilligCompany(ArrayList array)
    {
        conn = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {

            conn.Open();
            comm = new SqlCommand("proc_mst_invoice_billing_company", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_name", array[0].ToString());
            comm.Parameters.AddWithValue("@sz_address", array[1].ToString());
            comm.Parameters.AddWithValue("@sz_city", array[2].ToString());
            comm.Parameters.AddWithValue("@sz_state_name", array[3].ToString());
            comm.Parameters.AddWithValue("@i_state_id", array[4].ToString());
            comm.Parameters.AddWithValue("@sz_zip", array[5].ToString());
            comm.Parameters.AddWithValue("@sz_phone", array[6].ToString());
            comm.Parameters.AddWithValue("@sz_fax", array[7].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", array[8].ToString());
            comm.Parameters.AddWithValue("@flag", array[9].ToString());
            iReturn= comm.ExecuteNonQuery();


        }

        catch (Exception ex)
        {
            iReturn = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();

            }
        }
        return iReturn;

    }
    public int SaveUpdateJFKBilligCompany(ArrayList array)
    {
        conn = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {

            conn.Open();
            comm = new SqlCommand("proc_mst_invoice_billing_company", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_name", array[0].ToString());
            comm.Parameters.AddWithValue("@sz_address", array[1].ToString());
            comm.Parameters.AddWithValue("@sz_city", array[2].ToString());
            comm.Parameters.AddWithValue("@sz_state_name", array[3].ToString());
            comm.Parameters.AddWithValue("@i_state_id", array[4].ToString());
            comm.Parameters.AddWithValue("@sz_zip", array[5].ToString());
            comm.Parameters.AddWithValue("@sz_phone", array[6].ToString());
            comm.Parameters.AddWithValue("@sz_fax", array[7].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", array[8].ToString());
            comm.Parameters.AddWithValue("@i_id", array[9].ToString());
            comm.Parameters.AddWithValue("@flag", array[10].ToString());
            iReturn= comm.ExecuteNonQuery();


        }

        catch (Exception ex)
        {
            iReturn = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();

            }
        }
        return iReturn;

    }
    public int DeleteJFKBilligCompany(ArrayList array)
    {
        conn = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {

            conn.Open();
            comm = new SqlCommand("proc_mst_invoice_billing_company", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@i_id", array[0].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", array[1].ToString());
            comm.Parameters.AddWithValue("@flag", array[2].ToString());
            iReturn=comm.ExecuteNonQuery();


        }

        catch (Exception ex)
        {
            iReturn = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();

            }
        }
        return iReturn;

    }
    public DataSet SelectJFKBilligCompany(ArrayList array)  
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {

            conn.Open();
            comm = new SqlCommand("proc_mst_invoice_billing_company", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
       
            comm.Parameters.AddWithValue("@sz_company_id", array[0].ToString());
            comm.Parameters.AddWithValue("@flag", array[1].ToString());
            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);


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