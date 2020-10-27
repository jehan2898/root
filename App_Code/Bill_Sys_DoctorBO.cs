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
/// <summary>
/// Summary description for Bill_Sys_Doctor
/// </summary>
public class Bill_Sys_DoctorBO
{
  
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    SqlDataReader dr;

    public Bill_Sys_DoctorBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet BindDataGrid(string doctorId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "SPECIALITY_LIST");
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            //sqlCmd.ExecuteNonQuery();

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

    public void SaveDoctorSpeciality(string doctorId, string specialityID, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
   
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", specialityID);
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
    public string GetDoctorSpecialityID(string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string _return="";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETMAXID");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            dr=sqlCmd.ExecuteReader();
            while (dr.Read())
            {
               _return= dr[0].ToString();
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
        return _return;
    }
    public string GetPTOwner( string companyID, string officeID, string specialityID)
    {

        sqlCon = new SqlConnection(strConn);
        string _return = "";
        try
        {
            
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("PROC_CHECK_PT_OWNER", sqlCon);
            //sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlCon;
           
            cmd.Parameters.AddWithValue("SZ_SPECIALITY_ID", specialityID);
            cmd.Parameters.AddWithValue("SZ_OFFICE_ID", officeID);
            cmd.Parameters.AddWithValue("SZ_COMPANY_ID", companyID);
           
         
            dr = cmd.ExecuteReader();
              while(dr.Read())
            {
                _return = dr[0].ToString();
            }
            dr.Close();
        }
        catch (Exception ex)
        {

        }

        return _return;
    }

    public DataSet GetDoctorList(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_DOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
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

    public DataSet GetReadingDoctorList(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_READINGDOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
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
    

    public string GetLatestUserID(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader reader = null;
        string Curuserid = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_Doctor_UserID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETCURRENTUSERID");
            reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                Curuserid = reader.GetValue(0).ToString();
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
        return Curuserid;
        //Method End


    }

    public void InsertDoctorUserDetails(string sz_CompanyID, string sz_UserID, string sz_DoctorID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_Doctor_UserID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
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
        //Method End


    }

    public void DelDocUserID(string sz_CompanyID, string sz_UserID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_Doctor_UserID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
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
        //Method End


    }

    public DataSet GetDoctorID(string sz_CompanyID, string sz_UserID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_Doctor_UserID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORID");
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


    public void AddProcedureCode(string sz_CompanyID, string sz_DoctorID, string sz_ProcedureCode)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_SVDOCTOR_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", sz_ProcedureCode);
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
        //Method End


    }

    public DataSet GetProcedureCodes(string sz_CompanyID, string sz_DoctorID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_SVDOCTOR_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
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
        //Method End

    }


    public DataSet GetReferralDoctorList(string sz_CompanyID, string Flag)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_DOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", Flag);
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

    public void DelProcCode(string sz_CompanyID, string sz_DoctorID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_SVDOCTOR_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
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
        //Method End


    }


    public int  InsertDoctorMaster(string sz_Doctor_Name, string sz_Assign_no, string sz_Doc_Lic_No,string sz_Off_ID,string sz_Proc_id,string sz_Company_ID)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_MST_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doctor_Name);
            sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", sz_Assign_no);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_Doc_Lic_No);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_Off_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID",sz_Proc_id );
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
          i=  sqlCmd.ExecuteNonQuery();
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
        //Method End

    }

    public int UpdateDoctorMaster(string sz_Doctor_Name, string sz_Assign_no, string sz_Doc_Lic_No, string sz_Off_ID, string sz_Proc_id, string sz_Company_ID,string sz_Doc_Id)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_MST_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doctor_Name);
            sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", sz_Assign_no);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_Doc_Lic_No);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_Off_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Proc_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_Doc_Id);
            i = sqlCmd.ExecuteNonQuery();
        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            i = 0;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return i;
        //Method End

    }

    public int InsertOfficeMaster(string SZ_OFFICE, string SZ_COMPANY_ID, string SZ_OFFICE_ADDRESS, string SZ_OFFICE_CITY, string SZ_OFFICE_STATE, string SZ_OFFICE_ZIP, string SZ_OFFICE_PHONE, string SZ_BILLING_ADDRESS, string SZ_BILLING_CITY, string SZ_BILLING_STATE, string SZ_BILLING_ZIP, string SZ_BILLING_PHONE, string SZ_NPI, string SZ_FEDERAL_TAX_ID, string SZ_OFFICE_FAX, string SZ_OFFICE_EMAIL, string SZ_OFFICE_STATE_ID, string SZ_BILLING_STATE_ID, string BIT_IS_BILLING, string SZ_PREFIX, string SZ_LOCATION_ID, int BT_SoftFee, string szSoftwareFee, string sz_office_code)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_OFFICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE",SZ_OFFICE );
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID",SZ_COMPANY_ID );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS",SZ_OFFICE_ADDRESS );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_CITY",SZ_OFFICE_CITY );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE",SZ_OFFICE_STATE );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ZIP",SZ_OFFICE_ZIP );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_PHONE", SZ_OFFICE_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ADDRESS",SZ_BILLING_ADDRESS );
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_CITY",SZ_BILLING_CITY );
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE",SZ_BILLING_STATE );
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ZIP",SZ_BILLING_ZIP );
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_PHONE",SZ_BILLING_PHONE );
            sqlCmd.Parameters.AddWithValue("@SZ_NPI", SZ_NPI);
            sqlCmd.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID",SZ_FEDERAL_TAX_ID );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_FAX",SZ_OFFICE_FAX );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_EMAIL",SZ_OFFICE_EMAIL );
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE_ID",SZ_OFFICE_STATE_ID );
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE_ID",SZ_BILLING_STATE_ID );
            
            sqlCmd.Parameters.AddWithValue("@BIT_IS_BILLING",BIT_IS_BILLING );
            sqlCmd.Parameters.AddWithValue("@SZ_PREFIX",SZ_PREFIX );
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", SZ_LOCATION_ID);
            // added by Kapil 05 Jan 2012
            sqlCmd.Parameters.AddWithValue("@SZ_IS_SOFTWARE_FEE_ADDED",BT_SoftFee);
            sqlCmd.Parameters.AddWithValue("@SZ_MN_SOFTWARE_FEE",szSoftwareFee);
            sqlCmd.Parameters.AddWithValue("@sz_place_of_service", sz_office_code);
            sqlCmd.Parameters.AddWithValue("@FLAG","ADD" );
          


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
        //Method End


    }

    public int UpdateOfficeMaster(string SZ_OFFICE, string SZ_COMPANY_ID, string SZ_OFFICE_ADDRESS, string SZ_OFFICE_CITY, string SZ_OFFICE_STATE, string SZ_OFFICE_ZIP, string SZ_OFFICE_PHONE, string SZ_BILLING_ADDRESS, string SZ_BILLING_CITY, string SZ_BILLING_STATE, string SZ_BILLING_ZIP, string SZ_BILLING_PHONE, string SZ_NPI, string SZ_FEDERAL_TAX_ID, string SZ_OFFICE_FAX, string SZ_OFFICE_EMAIL, string SZ_OFFICE_STATE_ID, string SZ_BILLING_STATE_ID, string BIT_IS_BILLING, string SZ_PREFIX, string SZ_LOCATION_ID, string SZ_OFFICE_ID, int BT_SoftFee, string szSoftwareFee, string sz_office_code)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_OFFICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", SZ_OFFICE);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS", SZ_OFFICE_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_CITY", SZ_OFFICE_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE", SZ_OFFICE_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ZIP", SZ_OFFICE_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_PHONE", SZ_OFFICE_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ADDRESS", SZ_BILLING_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_CITY", SZ_BILLING_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE", SZ_BILLING_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ZIP", SZ_BILLING_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_PHONE", SZ_BILLING_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_NPI", SZ_NPI);
            sqlCmd.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", SZ_FEDERAL_TAX_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_FAX", SZ_OFFICE_FAX);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_EMAIL", SZ_OFFICE_EMAIL);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE_ID", SZ_OFFICE_STATE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE_ID", SZ_BILLING_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@BIT_IS_BILLING", BIT_IS_BILLING);
            sqlCmd.Parameters.AddWithValue("@SZ_PREFIX", SZ_PREFIX);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", SZ_LOCATION_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", SZ_OFFICE_ID);
            // added by Kapil 05 Jan 2012
            sqlCmd.Parameters.AddWithValue("@SZ_IS_SOFTWARE_FEE_ADDED",BT_SoftFee);
            sqlCmd.Parameters.AddWithValue("@SZ_MN_SOFTWARE_FEE",szSoftwareFee);
            sqlCmd.Parameters.AddWithValue("@sz_place_of_service", sz_office_code);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");



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
        //Method End


    }

    public string CheckAssihnNo(string companyId,string assignno)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string _return = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_ASSIGN_NUMBER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", assignno);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                _return = ds.Tables[0].Rows[0][0].ToString();
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
        return _return;
    }

    public int saveLeave(string company_Id, string doctor_Id, string doctor_name, string reason, string sz_leave_date)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_LEAVE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctor_Id);
            sqlCmd.Parameters.AddWithValue("@DT_LEAVE_DATE", sz_leave_date);
            sqlCmd.Parameters.AddWithValue("@SZ_LEAVE_REASON", reason);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_NAME", doctor_name);
            sqlCmd.Parameters.AddWithValue("@SZ_FLAG", "ADD");
            iReturn = sqlCmd.ExecuteNonQuery();

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
        return iReturn;
    }

    public int DeleteEvent( string sz_companyid, string sz_leaveid,string sz_doctorid)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            
            

                sqlCmd = new SqlCommand("SP_TXN_DOCTOR_LEAVE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
                sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctorid);
                sqlCmd.Parameters.AddWithValue("@I_LEAVE_ID", sz_leaveid);
                sqlCmd.Parameters.AddWithValue("@SZ_FLAG", "DELETE");
                int Cnt = sqlCmd.ExecuteNonQuery();
                iReturn = Cnt + iReturn;
            


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
        return iReturn;
    }

   



    public DataSet GetDoctorLeave(string sz_CompanyID, string sz_DoctorID,string leave_date)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_LEAVE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
            sqlCmd.Parameters.AddWithValue("@DT_LEAVE_DATE", leave_date);
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
    public DataSet GetDoctorChange(string sz_CompanyID, string sz_DoctorID, string leave_date)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_LEAVE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorID);
            sqlCmd.Parameters.AddWithValue("@DT_LEAVE_DATE", leave_date);
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





    public DataSet GetReadingDoctorAll(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LHR_READNIGDOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
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
    public DataSet GetSecondaryInsuranceCompany(string sz_CompanyID, string sz_CaseID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SECONDRY_INS_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
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

    public DataSet GetSecondaryInsuranceComp(string sz_CompanyID, string sz_CaseID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SECONDARY_INSURANCE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
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

    public void Updatesecondayinsurance(string sz_CaseID, string sz_CompanyID, string sz_InsuranceID, string sz_InsuranceaddID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_SECONDARY_INS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", sz_CaseID);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_InsuranceID);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_InsuranceaddID);
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
        //Method End


    }
    public void UpdateEventWithCase( string szCaseId,string szComapnyId, string szInsId, string szInsAddId, string szClaimNo, string szPolicyHolder, string szPolicyNo, string szDOA, string szCaseTypeId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("UPDTE_EVENT_WITH_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ID", szInsId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADD_ID", szInsAddId);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimNo);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", szPolicyNo);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER", szPolicyHolder);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szCaseTypeId);
            sqlCmd.Parameters.AddWithValue("@DT_DOA", szDOA);
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
        //Method End


    }


    public void UpdateEventInsWithCase(string szCaseId, string szComapnyId, string szInsId, string szInsAddId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("UPDTE_EVENT_INS_WITH_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ID", szInsId);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADD_ID", szInsAddId);
        
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
        //Method End


    }

    public void UpdateDoctorinfoc4_2(string szComapnyId, string sz_bill_no, string szBillinggroup)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DOCTOR_INFORMATION_C4_2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_BIIL_NO", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_GROUP", szBillinggroup);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
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
        //Method End


    }

    public DataSet GetDoctorinfoc4_2(string sz_CompanyID, string sz_bill_no, string szBillinggroup)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DOCTOR_INFORMATION_C4_2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_BIIL_NO", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_GROUP", szBillinggroup);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
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

    public DataSet GetDoctorinfofortaxid(string Sz_Doctorid_,string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DOCTOR_INFO_C4_2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", Sz_Doctorid_);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
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

    public void UpdateBillinfoppoc4_2(string sz_bill_no, string BtPpo)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_BILL_INFO_PPO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@BT_PPO", BtPpo);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
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
        //Method End


    }
    public void Insertsecondayinsurance(string sz_CaseID, string sz_CompanyID, string sz_InsuranceID, string sz_InsuranceaddID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_SECONDARY_INS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", sz_CaseID);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_InsuranceID);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_InsuranceaddID);
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
        //Method End


    }


    public DataSet GetRefDocForSetting(string CompanyId,string OfficeID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_reffering_doctor_text", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyId);
            sqlCmd.Parameters.AddWithValue("@sz_ofiice_id", OfficeID);
            //sqlCmd.ExecuteNonQuery();

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

    public string GetDoctorForBill(string sz_CompanyID, string sz_bill_no)
    {
        sqlCon = new SqlConnection(strConn);
        string strDoctorName = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DOCTOR_INFORMATION_FOR_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_BIIL_NO", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strDoctorName = dr[0].ToString();
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
        return strDoctorName;
    }


}

