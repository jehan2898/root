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



public class Bill_Sys_NotesBO
{
    private int _I_NOTE_ID = 0;
    public int I_NOTE_ID
    {
        get
        {
            return _I_NOTE_ID;
        }
        set
        {
            _I_NOTE_ID = value;
        }
    }
    private string _SZ_NOTE_CODE = string.Empty;
    public string SZ_NOTE_CODE
    {
        get
        {
            return _SZ_NOTE_CODE;
        }
        set
        {
            _SZ_NOTE_CODE = value;
        }
    }

    private string _SZ_COMPANY_ID = string.Empty;
    public string SZ_COMPANY_ID
    {
        get
        {
            return _SZ_COMPANY_ID;
        }
        set
        {
            _SZ_COMPANY_ID = value;
        }
    }

    private string _SZ_CASE_ID = string.Empty;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }

    private string _SZ_USER_ID = string.Empty;
    public string SZ_USER_ID
    {
        get
        {
            return _SZ_USER_ID;
        }
        set
        {
            _SZ_USER_ID = value;
        }
    }

    private DateTime _DT_ADDED = System.DateTime.Today;
    public DateTime DT_ADDED
    {
        get
        {
            return _DT_ADDED;
        }
        set
        {
            _DT_ADDED = value;
        }
    }

    private string _SZ_NOTE_TYPE = string.Empty;
    public string SZ_NOTE_TYPE
    {
        get
        {
            return _SZ_NOTE_TYPE;
        }
        set
        {
            _SZ_NOTE_TYPE = value;
        }
    }

    private string _SZ_NOTE_DESCRIPTION = string.Empty;
    public string SZ_NOTE_DESCRIPTION
    {
        get
        {
            return _SZ_NOTE_DESCRIPTION;
        }
        set
        {
            _SZ_NOTE_DESCRIPTION = value;
        }
    }


    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_NotesBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void SaveNotes(ArrayList p_objArrayList)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", p_objArrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_objArrayList[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", p_objArrayList[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", p_objArrayList[5].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
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

    public void SaveNotes()
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("TXN_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Int);
            // sqlParam.Direction = ParameterDirection.ReturnValue;

            //sqlCmd.Parameters.AddWithValue("@I_NOTE_ID", "");
            if (SZ_NOTE_CODE != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", SZ_NOTE_CODE); }
            if (SZ_COMPANY_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID); }
            if (SZ_CASE_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID); }
            if (SZ_USER_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", SZ_USER_ID); }
            sqlCmd.Parameters.AddWithValue("@DT_ADDED", DT_ADDED);
            if (SZ_NOTE_TYPE != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", SZ_NOTE_TYPE); }
            if (SZ_NOTE_DESCRIPTION != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", ""); }
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            //return Convert.ToInt32(sqlCmd.Parameters["@Return"].Value);
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

    public void UpdateBillStatusForVerification(string billNumber, int i_VERIFICATION, string sz_notes)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("Update_Bill_Verification", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_VERIFICATION", i_VERIFICATION);
            sqlCmd.Parameters.AddWithValue("@SZ_Bill_NUMBER", billNumber);
            if (sz_notes != "") { sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_NOTES", sz_notes); }
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

    public DataSet GET_VERIFICATION_SENT_BILLS(string sz_company_id, string sz_case_Id)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_VERIFICATION_SENT_BILLS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            
            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet GetBillDetailsVerificationPopUp(string sz_Company_ID, string SZ_Bill_No)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TXN_BILL_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }

    public DataSet GetBillDetailsFillGrid(string sz_Company_ID, string SZ_Bill_No)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_DESCRIPTION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }

    public DataSet GetBillDetailsFillGridNew(string sz_Company_ID, string SZ_Bill_No)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_DESCRIPTION_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }


    public DataSet Populategrid(string sz_Company_ID, string SZ_Bill_No)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_RECIEVED_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet PopulateDenialGrid(string sz_Company_ID, string SZ_Bill_No)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_DENIAL_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet GetNotesDetails(string sz_case_id, string sz_Company_ID)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_VIEW_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

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
        return ds;
    }

    public int AddUnBilledReason(ArrayList arr, string UnBilledReason, string strReasonValue)
    {



        sqlCon = new SqlConnection(strConn);

        SqlTransaction transaction;

        sqlCon.Open();
        int iReturn = 0;

        transaction = sqlCon.BeginTransaction();
        try
        {
            for (int i = 0; i < arr.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_ADD_UNBILLIED_REASON", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_EVENT_PROC_ID", arr[i].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_UNBILLABLE_REASON", UnBilledReason);
                sqlCmd.Parameters.AddWithValue("@SZ_CHECK_UNBILLABLE", strReasonValue);
                iReturn = sqlCmd.ExecuteNonQuery();

            }
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            iReturn = 0;

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }

        return iReturn;

    }

    public DataSet GetUnBilledReason(string sz_proc_id)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_UNBILLIED_REASON", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_PROC_ID", sz_proc_id);

            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

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
        return ds;
    }


    public DataSet GetBillDetailsFillGridDenial(string sz_Company_ID, string SZ_Bill_No)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_DESCRIPTION_DENIAL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }

    public DataSet GetPatientDenial(string sz_Company_ID, string SZ_caseid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DENIAL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", SZ_caseid);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

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
        return ds;
    }

    public DataSet GetBillDetailsFillGridVerification(string sz_Company_ID, string SZ_Bill_No)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_DESCRIPTION_VERIFICATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_Bill_No);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.ExecuteNonQuery();
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }
}
