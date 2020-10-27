using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

public class per_procedure_payment_DAO
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public per_procedure_payment_DAO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    private string _SZ_PROCEDURE_ID;
    public string SZ_PROCEDURE_ID
    {
        get { return _SZ_PROCEDURE_ID; }
        set { _SZ_PROCEDURE_ID = value; }
    }

    private string _SZ_BILL_TXN_DETAIL_ID;
    public string SZ_BILL_TXN_DETAIL_ID
    {
        get { return _SZ_BILL_TXN_DETAIL_ID; }
        set { _SZ_BILL_TXN_DETAIL_ID = value; }
    }

    private string _sz_case_id;
    public string sz_case_id
    {
        get { return _sz_case_id; }
        set { _sz_case_id = value; }
    }

    private string _sz_bill_no;
    public string sz_bill_no
    {
        get { return _sz_bill_no; }
        set { _sz_bill_no = value; }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get { return _sz_company_id; }
        set { _sz_company_id = value; }
    }

    private string _sz_proc_code;
    public string sz_proc_code
    {
        get { return _sz_proc_code; }
        set { _sz_proc_code = value; }
    }

    private string _date_of_service;
    public string date_of_service
    {
        get { return _date_of_service; }
        set { _date_of_service = value; }
    }

    private string _eligible_amount;
    public string eligible_amount
    {
        get { return _eligible_amount; }
        set { _eligible_amount = value; }
    }

    private string _amount_allowed;
    public string amount_allowed
    {
        get { return _amount_allowed; }
        set { _amount_allowed = value; }
    }

    private string _explanation_codes;
    public string explanation_codes
    {
        get { return _explanation_codes; }
        set { _explanation_codes = value; }
    }

    private string _sz_denial_reason;
    public string sz_denial_reason
    {
        get { return _sz_denial_reason; }
        set { _sz_denial_reason = value; }
    }

    private string _i_denial_reason_id;
    public string i_denial_reason_id
    {
        get { return _i_denial_reason_id; }
        set { _i_denial_reason_id = value; }
    }

    private string _sz_user_id;
    public string sz_user_id
    {
        get { return _sz_user_id; }
        set { _sz_user_id = value; }
    }

    private string _dt_check_date;
    public string dt_check_date
    {
        get { return _dt_check_date; }
        set { _dt_check_date = value; }
    }

    private string _sz_check_number;
    public string sz_check_number
    {
        get { return _sz_check_number; }
        set { _sz_check_number = value; }
    }

    private int _i_paid_by;
    public int i_paid_by
    {
        get { return _i_paid_by; }
        set { _i_paid_by = value; }
    }

    private int _i_settle;
    public int i_settle
    {
        get { return _i_settle; }
        set { _i_settle = value; }
    }

    private string _sz_copay;
    public string sz_copay
    {
        get { return _sz_copay; }
        set { _sz_copay = value; }
    }

    private string _sz_notes;
    public string sz_notes
    {
        get { return _sz_notes; }
        set { _sz_notes = value; }
    }

    private string _sz_payment_id;
    public string sz_payment_id
    {
        get { return _sz_payment_id; }
        set { _sz_payment_id = value; }
    }


    public void InsertPerProcedurePayment(ArrayList arrlist)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            string Id = "";
            for (int i = 0; i < arrlist.Count; i++)
            {
                per_procedure_payment_DAO o_dao = new per_procedure_payment_DAO();
                o_dao = (per_procedure_payment_DAO)arrlist[i];
                if (o_dao.sz_bill_no != null)
                {
                    comm = new SqlCommand("sp_save_per_procedure_payment", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@sz_case_id", o_dao.sz_case_id);
                    comm.Parameters.AddWithValue("@sz_bill_no", o_dao.sz_bill_no);
                    comm.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
                    comm.Parameters.AddWithValue("@sz_proc_code", o_dao.sz_proc_code);
                    comm.Parameters.AddWithValue("@date_of_service", o_dao.date_of_service);
                    comm.Parameters.AddWithValue("@eligible_amount", o_dao.eligible_amount);
                    comm.Parameters.AddWithValue("@amount_allowed", o_dao.amount_allowed);
                    comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", o_dao.SZ_PROCEDURE_ID);
                    comm.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", o_dao.SZ_BILL_TXN_DETAIL_ID);
                    comm.Parameters.AddWithValue("@sz_user_id", o_dao.sz_user_id);
                    comm.Parameters.AddWithValue("@check_date", o_dao.dt_check_date);
                    comm.Parameters.AddWithValue("@check_number", o_dao.sz_check_number);
                    comm.Parameters.AddWithValue("@c_paid_by", o_dao.i_paid_by);
                    comm.Parameters.AddWithValue("@c_settlement_type", o_dao.i_settle);
                    comm.Parameters.AddWithValue("@m_copay_deductable", o_dao.sz_copay);
                    comm.Parameters.AddWithValue("@sz_notes", o_dao.sz_notes);
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Id = dr[0].ToString();
                        }
                    }
                    dr.Close();
                }
                else
                {
                    comm = new SqlCommand("sp_save_Denial_Reason_per_proc_payment", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@i_per_procedure_payment_id", Id);
                    comm.Parameters.AddWithValue("@i_denial_reason_id", o_dao.i_denial_reason_id);
                    comm.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
                    comm.Parameters.AddWithValue("@sz_user_id", o_dao.sz_user_id);
                    comm.ExecuteNonQuery();
                }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetDenialReson(string sz_company_id)
    {
        DataSet dataSet = new DataSet();
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        try
        {

            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MST_DENIAL", connection);
            selectCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id", sz_company_id);
            selectCommand.Parameters.AddWithValue("@FLAG", "DENIAL_LIST");
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        return dataSet;
    }

    public void UpdatePaymentRecordType(string sz_bill_no, string sz_company_id, string payment_type)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("sp_update_payment_record_type", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@sz_bill_number", sz_bill_no);
            comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            comm.Parameters.AddWithValue("@payment_record_type", payment_type);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void UpdateHasDenial(string sz_bill_no, string sz_company_id, string Denial_bit)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("sp_update_has_denial", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@sz_bill_number", sz_bill_no);
            comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            comm.Parameters.AddWithValue("@has_denial", Denial_bit);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeletePayment(per_procedure_payment_DAO o_dao)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_delete_per_procedure_payment", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_case_id", o_dao.sz_case_id);
            comm.Parameters.AddWithValue("@sz_bill_no", o_dao.sz_bill_no);
            comm.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            comm.Parameters.AddWithValue("@sz_payment_id", o_dao.sz_payment_id);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetPerProcBillDetail(string szBillNo)
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_per_procedure_payment", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
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

    public void BillTrasaction(string szBillNo, string CheckNo, string checkdate, string ReceivedBill, string companyId, string userid, string flag)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
            comm.Parameters.AddWithValue("@SZ_CHECK_NUMBER", CheckNo);
            comm.Parameters.AddWithValue("@DT_CHECK_DATE", checkdate);
            comm.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", ReceivedBill);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            comm.ExecuteNonQuery();
        }
        catch (SqlException ex)
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
}
