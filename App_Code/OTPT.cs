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

public class OTPT
{
    string strConn = "";
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    public OTPT()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public void Save_Patient_Info(OTPT_PATIENT_DAO obj_OTPT_PATIENT_DAO)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_OTPT_PATIENT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", obj_OTPT_PATIENT_DAO.SZ_PATIENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj_OTPT_PATIENT_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", obj_OTPT_PATIENT_DAO.SZ_BILL_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_INJURY_PLACE", obj_OTPT_PATIENT_DAO.SZ_PATIENT_INJURY_PLACE);
            sqlCmd.Parameters.AddWithValue("@SZ_TIME_OF_INJURY", obj_OTPT_PATIENT_DAO.SZ_TIME_OF_INJURY);
            sqlCmd.Parameters.AddWithValue("@SZ_TREATEMNET_UNDER", obj_OTPT_PATIENT_DAO.SZ_TREATEMNET_UNDER);
            sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_PRV_HISTROY", obj_OTPT_PATIENT_DAO.DT_DATE_OF_PRV_HISTROY);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj_OTPT_PATIENT_DAO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_TIME_TYPE", obj_OTPT_PATIENT_DAO.SZ_TIME_TYPE);
            sqlCmd.Parameters.AddWithValue("@SZ_REFFERING_PHYSICIAN", obj_OTPT_PATIENT_DAO.SZ_REFFERING_PHYSICIAN);
            sqlCmd.Parameters.AddWithValue("@SZ_REFFERING_PHYSICIAN_ADDRESS", obj_OTPT_PATIENT_DAO.SZ_REFFERING_PHYSICIAN_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_REFFERING_TELEPHONE_NO", obj_OTPT_PATIENT_DAO.SZ_REFFERING_TELEPHONE_NO);
            //sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
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
    public void Save_History_Info(OTPT_HISTROY_DAO obj_OTPT_HISTROY_DAO)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_OTPT_HISTORY_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", obj_OTPT_HISTROY_DAO.SZ_PATIENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj_OTPT_HISTROY_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", obj_OTPT_HISTROY_DAO.SZ_BILL_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_PHYSICIAN_REFERRING", obj_OTPT_HISTROY_DAO.SZ_PHYSICIAN_REFERRING);
            sqlCmd.Parameters.AddWithValue("@SZ_PRE_INJURY_HISTROY", obj_OTPT_HISTROY_DAO.SZ_PRE_INJURY_HISTROY);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj_OTPT_HISTROY_DAO.SZ_COMPANY_ID);
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
    public void Patient_Report_Info(OTPT_DAO obj_OTPT_DAO)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_OTPT_REPORT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", obj_OTPT_DAO.SZ_PATIENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj_OTPT_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", obj_OTPT_DAO.SZ_BILL_NO);
            sqlCmd.Parameters.AddWithValue("@BT_OCCUPATIOANL_THERP_REPORT", obj_OTPT_DAO.SZ_BT_OCCUPATIONAL);
            sqlCmd.Parameters.AddWithValue("@BT_PHYSICAL_THERP_REPORT", obj_OTPT_DAO.SZ_BT_PHYSICAL);
            sqlCmd.Parameters.AddWithValue("@BT_48HR_INI_REPORT", obj_OTPT_DAO.SZ_BT_48_HOUR);
            sqlCmd.Parameters.AddWithValue("@BT_15DAY_INI_REPORT", obj_OTPT_DAO.SZ_BT_15_DAY);
            sqlCmd.Parameters.AddWithValue("@BT_90DAY_PRO_REPORT", obj_OTPT_DAO.SZ_BT_90_DAY);
            sqlCmd.Parameters.AddWithValue("@BT_PPO", obj_OTPT_DAO.SZ_BT_PPO);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj_OTPT_DAO.SZ_COMPANY_ID);
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
    public void Patient_Treatment_Info(OTPT_TREATMENT_DAO obj_OTPT_Treatment_DAO)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_OT_PT_EVALUATION_TREATMENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", obj_OTPT_Treatment_DAO.SZ_PATIENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj_OTPT_Treatment_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", obj_OTPT_Treatment_DAO.SZ_BILL_NO);
            sqlCmd.Parameters.AddWithValue("@BT_REFRERAL_FOR", obj_OTPT_Treatment_DAO.BT_REFRERAL_FOR);
            sqlCmd.Parameters.AddWithValue("@SZ_EVALUTION_DESC", obj_OTPT_Treatment_DAO.SZ_EVALUTION_DESC);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_CONDITION", obj_OTPT_Treatment_DAO.SZ_PATIENT_CONDITION);
            sqlCmd.Parameters.AddWithValue("@SZ_TREATMENT", obj_OTPT_Treatment_DAO.SZ_TREATMENT);
            sqlCmd.Parameters.AddWithValue("@BT_TREATMENT_PLAN", obj_OTPT_Treatment_DAO.BT_TREATMENT_PLAN);
            sqlCmd.Parameters.AddWithValue("@SZ_FREQUENCY", obj_OTPT_Treatment_DAO.SZ_FREQUENCY);
            sqlCmd.Parameters.AddWithValue("@SZ_PERIOD", obj_OTPT_Treatment_DAO.SZ_PERIOD);
            sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_VISIT_REPORT", obj_OTPT_Treatment_DAO.DT_DATE_OF_VISIT_REPORT);
            sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_FIRST_VISIT", obj_OTPT_Treatment_DAO.DT_DATE_OF_FIRST_VISIT);
            sqlCmd.Parameters.AddWithValue("@BT_PATIENT_SEEN_AGAIN", obj_OTPT_Treatment_DAO.BT_PATIENT_SEEN_AGAIN);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_SEEN_YES", obj_OTPT_Treatment_DAO.SZ_PATIENT_SEEN_YES);
            sqlCmd.Parameters.AddWithValue("@BT_PATIENT_ATTENDING_DOCTOR", obj_OTPT_Treatment_DAO.BT_PATIENT_ATTENDING_DOCTOR);
            sqlCmd.Parameters.AddWithValue("@BT_PATIENT_WORKING", obj_OTPT_Treatment_DAO.BT_PATIENT_WORKING);
            sqlCmd.Parameters.AddWithValue("@SZ_LIMITED_WORK", obj_OTPT_Treatment_DAO.SZ_LIMITED_WORK);
            sqlCmd.Parameters.AddWithValue("@SZ_REGULAR_WORK", obj_OTPT_Treatment_DAO.SZ_REGULAR_WORK);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj_OTPT_Treatment_DAO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@BT_ADDITIONAL_SPACE", obj_OTPT_Treatment_DAO.BT_ADDITIONAL_SPACE);
        
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
    public void Patient_Therapist_Info(OTPT_THERAPIST_DAO obj_OTPT_THERAPIST_DAO)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_OTPT_THERAPIST_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj_OTPT_THERAPIST_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", obj_OTPT_THERAPIST_DAO.SZ_BILL_NO);
            sqlCmd.Parameters.AddWithValue("@BT_DOCTOR_TYPE", obj_OTPT_THERAPIST_DAO.SZ_BT_SSN_EIN);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj_OTPT_THERAPIST_DAO.SZ_COMPANY_ID);
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
    public DataSet GET_Therapist_Info_OTPT(string sz_doctorid)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_OTPT_THERAPIST_INFORMATION_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_doctorid);
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_OCT_Bills_Table(string szBillId)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_WC_OCT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "CODE");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Patient_Info_OTPT(string szCaseID)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_OTPT_PATIENT_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            // comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Patient_Information_OTPT(string sz_case_id, string sz_company_id)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_OTPT_PATIENT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Report_Information_OTPT(string sz_case_id, string sz_company_id)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_OTPT_REPORT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Histroy_Information_OTPT(string sz_case_id, string sz_company_id)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_OTPT_HISTORY_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Evalution_and_Treatment_Information_OTPT(string sz_case_id, string sz_company_id)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_OTPT_EVALUTION_AND_TREATMENT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
    public DataSet GET_Therapist_Information_OTPT(string sz_case_id, string sz_company_id)
    {
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCmd = new SqlCommand("SP_GET_OTPT_THERAPIST_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(sqlCmd);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dsReturn;

    }
}
public class OTPT_PATIENT_DAO
{
    private string _SZ_PATIENT_ID = string.Empty;
    public string SZ_PATIENT_ID
    {
        get
        {
            return _SZ_PATIENT_ID;
        }
        set
        {
            _SZ_PATIENT_ID = value;
        }
    }

    private string _SZ_BILL_NO = string.Empty;
    public string SZ_BILL_NO
    {
        get
        {
            return _SZ_BILL_NO;
        }
        set
        {
            _SZ_BILL_NO = value;
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
    private string _SZ_PATIENT_INJURY_PLACE = string.Empty;
    public string SZ_PATIENT_INJURY_PLACE
    {
        get
        {
            return _SZ_PATIENT_INJURY_PLACE;
        }
        set
        {
            _SZ_PATIENT_INJURY_PLACE = value;
        }
    }

    private string _SZ_TIME_OF_INJURY = string.Empty;
    public string SZ_TIME_OF_INJURY
    {
        get
        {
            return _SZ_TIME_OF_INJURY;
        }
        set
        {
            _SZ_TIME_OF_INJURY = value;
        }
    }



    private string _SZ_TREATEMNET_UNDER = string.Empty;
    public string SZ_TREATEMNET_UNDER
    {
        get
        {
            return _SZ_TREATEMNET_UNDER;
        }
        set
        {
            _SZ_TREATEMNET_UNDER = value;
        }
    }
    private string _DT_DATE_OF_PRV_HISTROY = string.Empty;
    public string DT_DATE_OF_PRV_HISTROY
    {
        get
        {
            return _DT_DATE_OF_PRV_HISTROY;
        }
        set
        {
            _DT_DATE_OF_PRV_HISTROY = value;
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

    private string _SZ_TIME_TYPE = string.Empty;
    public string SZ_TIME_TYPE
    {
        get
        {
            return _SZ_TIME_TYPE;
        }
        set
        {
            _SZ_TIME_TYPE = value;
        }
    }
    private string _SZ_REFFERING_PHYSICIAN = string.Empty;
    public string SZ_REFFERING_PHYSICIAN
    {
        get
        {
            return _SZ_REFFERING_PHYSICIAN;
        }
        set
        {
            _SZ_REFFERING_PHYSICIAN = value;
        }
    }
    private string _SZ_REFFERING_PHYSICIAN_ADDRESS = string.Empty;
    public string SZ_REFFERING_PHYSICIAN_ADDRESS
    {
        get
        {
            return _SZ_REFFERING_PHYSICIAN_ADDRESS;
        }
        set
        {
            _SZ_REFFERING_PHYSICIAN_ADDRESS = value;
        }
    }
    private string _SZ_REFFERING_TELEPHONE_NO = string.Empty;
    public string SZ_REFFERING_TELEPHONE_NO
    {
        get
        {
            return _SZ_REFFERING_TELEPHONE_NO;
        }
        set
        {
            _SZ_REFFERING_TELEPHONE_NO = value;
        }
    }
    
    

}

public class OTPT_HISTROY_DAO
{
    private string _SZ_PATIENT_ID = string.Empty;
    public string SZ_PATIENT_ID
    {
        get
        {
            return _SZ_PATIENT_ID;
        }
        set
        {
            _SZ_PATIENT_ID = value;
        }
    }

    private string _SZ_BILL_NO = string.Empty;
    public string SZ_BILL_NO
    {
        get
        {
            return _SZ_BILL_NO;
        }
        set
        {
            _SZ_BILL_NO = value;
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
    private string _SZ_PHYSICIAN_REFERRING = string.Empty;
    public string SZ_PHYSICIAN_REFERRING
    {
        get
        {
            return _SZ_PHYSICIAN_REFERRING;
        }
        set
        {
            _SZ_PHYSICIAN_REFERRING = value;
        }
    }
    private string _SZ_PRE_INJURY_HISTROY = string.Empty;
    public string SZ_PRE_INJURY_HISTROY
    {
        get
        {
            return _SZ_PRE_INJURY_HISTROY;
        }
        set
        {
            _SZ_PRE_INJURY_HISTROY = value;
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

}

public class OTPT_DAO
{
    private string _SZ_PATIENT_ID = string.Empty;
    public string SZ_PATIENT_ID
    {
        get
        {
            return _SZ_PATIENT_ID;
        }
        set
        {
            _SZ_PATIENT_ID = value;
        }
    }

    private string _SZ_BILL_NO = string.Empty;
    public string SZ_BILL_NO
    {
        get
        {
            return _SZ_BILL_NO;
        }
        set
        {
            _SZ_BILL_NO = value;
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
    private string _SZ_BT_OCCUPATIONAL = string.Empty;
    public string SZ_BT_OCCUPATIONAL
    {
        get
        {
            return _SZ_BT_OCCUPATIONAL;
        }
        set
        {
            _SZ_BT_OCCUPATIONAL = value;
        }
    }
    private string _SZ_BT_PHYSICAL = string.Empty;
    public string SZ_BT_PHYSICAL
    {
        get
        {
            return _SZ_BT_PHYSICAL;
        }
        set
        {
            _SZ_BT_PHYSICAL = value;
        }
    }
    private string _SZ_BT_48_HOUR = string.Empty;
    public string SZ_BT_48_HOUR
    {
        get
        {
            return _SZ_BT_48_HOUR;
        }
        set
        {
            _SZ_BT_48_HOUR = value;
        }
    }
    private string _SZ_BT_15_DAY = string.Empty;
    public string SZ_BT_15_DAY
    {
        get
        {
            return _SZ_BT_15_DAY;
        }
        set
        {
            _SZ_BT_15_DAY = value;
        }
    }
    private string _SZ_BT_90_DAY = string.Empty;
    public string SZ_BT_90_DAY
    {
        get
        {
            return _SZ_BT_90_DAY;
        }
        set
        {
            _SZ_BT_90_DAY = value;
        }
    }
    private string _SZ_BT_PPO = string.Empty;
    public string SZ_BT_PPO
    {
        get
        {
            return _SZ_BT_PPO;
        }
        set
        {
            _SZ_BT_PPO = value;
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
}

public class OTPT_TREATMENT_DAO
{
    private string _SZ_PATIENT_ID = string.Empty;
    public string SZ_PATIENT_ID
    {
        get
        {
            return _SZ_PATIENT_ID;
        }
        set
        {
            _SZ_PATIENT_ID = value;
        }
    }

    private string _SZ_BILL_NO = string.Empty;
    public string SZ_BILL_NO
    {
        get
        {
            return _SZ_BILL_NO;
        }
        set
        {
            _SZ_BILL_NO = value;
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
    private string _BT_REFRERAL_FOR = string.Empty;
    public string BT_REFRERAL_FOR
    {
        get
        {
            return _BT_REFRERAL_FOR;
        }
        set
        {
            _BT_REFRERAL_FOR = value;
        }
    }

    private string _SZ_EVALUTION_DESC = string.Empty;
    public string SZ_EVALUTION_DESC
    {
        get
        {
            return _SZ_EVALUTION_DESC;
        }
        set
        {
            _SZ_EVALUTION_DESC = value;
        }
    }
    private string _SZ_PATIENT_CONDITION = string.Empty;
    public string SZ_PATIENT_CONDITION
    {
        get
        {
            return _SZ_PATIENT_CONDITION;
        }
        set
        {
            _SZ_PATIENT_CONDITION = value;
        }
    }
    private string _SZ_TREATMENT = string.Empty;
    public string SZ_TREATMENT
    {
        get
        {
            return _SZ_TREATMENT;
        }
        set
        {
            _SZ_TREATMENT = value;
        }
    }
    private string _BT_TREATMENT_PLAN = string.Empty;
    public string BT_TREATMENT_PLAN
    {
        get
        {
            return _BT_TREATMENT_PLAN;
        }
        set
        {
            _BT_TREATMENT_PLAN = value;
        }
    }
    private string _SZ_FREQUENCY = string.Empty;
    public string SZ_FREQUENCY
    {
        get
        {
            return _SZ_FREQUENCY;
        }
        set
        {
            _SZ_FREQUENCY = value;
        }
    }
    private string _SZ_PERIOD = string.Empty;
    public string SZ_PERIOD
    {
        get
        {
            return _SZ_PERIOD;
        }
        set
        {
            _SZ_PERIOD = value;
        }
    }
    private string _DT_DATE_OF_VISIT_REPORT = string.Empty;
    public string DT_DATE_OF_VISIT_REPORT
    {
        get
        {
            return _DT_DATE_OF_VISIT_REPORT;
        }
        set
        {
            _DT_DATE_OF_VISIT_REPORT = value;
        }
    }
    private string _DT_DATE_OF_FIRST_VISIT = string.Empty;
    public string DT_DATE_OF_FIRST_VISIT
    {
        get
        {
            return _DT_DATE_OF_FIRST_VISIT;
        }
        set
        {
            _DT_DATE_OF_FIRST_VISIT = value;
        }
    }
    private string _BT_PATIENT_SEEN_AGAIN = string.Empty;
    public string BT_PATIENT_SEEN_AGAIN
    {
        get
        {
            return _BT_PATIENT_SEEN_AGAIN;
        }
        set
        {
            _BT_PATIENT_SEEN_AGAIN = value;
        }
    }
    private string _SZ_PATIENT_SEEN_YES = string.Empty;
    public string SZ_PATIENT_SEEN_YES
    {
        get
        {
            return _SZ_PATIENT_SEEN_YES;
        }
        set
        {
            _SZ_PATIENT_SEEN_YES = value;
        }
    }
    private string _BT_PATIENT_ATTENDING_DOCTOR = string.Empty;
    public string BT_PATIENT_ATTENDING_DOCTOR
    {
        get
        {
            return _BT_PATIENT_ATTENDING_DOCTOR;
        }
        set
        {
            _BT_PATIENT_ATTENDING_DOCTOR = value;
        }
    }
    private string _BT_PATIENT_WORKING = string.Empty;
    public string BT_PATIENT_WORKING
    {
        get
        {
            return _BT_PATIENT_WORKING;
        }
        set
        {
            _BT_PATIENT_WORKING = value;
        }
    }
    private string _SZ_LIMITED_WORK = string.Empty;
    public string SZ_LIMITED_WORK
    {
        get
        {
            return _SZ_LIMITED_WORK;
        }
        set
        {
            _SZ_LIMITED_WORK = value;
        }
    }
    private string _SZ_REGULAR_WORK = string.Empty;
    public string SZ_REGULAR_WORK
    {
        get
        {
            return _SZ_REGULAR_WORK;
        }
        set
        {
            _SZ_REGULAR_WORK = value;
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
    private string _BT_ADDITIONAL_SPACE = string.Empty;
    public string BT_ADDITIONAL_SPACE
    {
        get
        {
            return _BT_ADDITIONAL_SPACE;
        }
        set
        {
            _BT_ADDITIONAL_SPACE = value;
        }
    }
}

public class OTPT_THERAPIST_DAO
{
    private string _SZ_BILL_NO = string.Empty;
    public string SZ_BILL_NO
    {
        get
        {
            return _SZ_BILL_NO;
        }
        set
        {
            _SZ_BILL_NO = value;
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
    private string _SZ_BT_SSN_EIN = string.Empty;
    public string SZ_BT_SSN_EIN
    {
        get
        {
            return _SZ_BT_SSN_EIN;
        }
        set
        {
            _SZ_BT_SSN_EIN = value;
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
    
}

