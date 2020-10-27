using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;

/// <summary>
/// Summary description for WorkerTemplate
/// </summary>
public class WorkerTemplate
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter objSqlDataAdap;
    DataSet objDataSet;

    SqlDataReader dr;

    public WorkerTemplate()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }


    public Boolean CheckPatientExist(string p_szSPName, string p_szPatientID)
    {
        Boolean _blReturn = false;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = p_szSPName;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "CHECKPATIENT");
            dr = comm.ExecuteReader();
            _blReturn = false;
            while (dr.Read())
            {
                _blReturn = true;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return _blReturn;
    }

    

    public Boolean PatientExistCheckForWC4(string p_szSPName,string p_szBillNumber )
    {
        Boolean _blReturn = false;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = p_szSPName;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "CHECKPATIENT");
            dr = comm.ExecuteReader();
            _blReturn = false;
            while (dr.Read())
            {
                _blReturn = true;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return _blReturn;
    }

    public void deletePatientComplaints(string p_szPatientID)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void deletePatientComplaints_New(string p_szBILLNUMBER)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBILLNUMBER);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void savePatientComplaints(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPLAINT_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPLAINT_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void savePatientComplaintsNEW(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPLAINT_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPLAINT_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objAL[5].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public DataSet getPatientComplaints(string p_szPatientID)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
    public DataSet getPatientComplaints_NEW(string p_szBillNumber)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_COMPLAINTS_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }

    public void deletePatientInjury(string p_szPatientID)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void deletePatientInjury_New(string p_szBillNumber)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void savePatientInjury(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INJURY_TYPE_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_INJURY_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void savePatientInjury_New(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INJURY_TYPE_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_INJURY_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objAL[5].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }


    public DataSet getPatientInjuryType(string p_szPatientID)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }
    public DataSet getPatientInjuryType_New(string p_szBillNumber)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_INJURY_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }

    public void deletePatientPhysicalExam(string p_szPatientID)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void deletePatientPhysicalExam_New(string p_szBillNumber)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "DELETEALL");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public void savePatientPhysicalExam(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PHYSICAL_EXAMINATION_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PHYSICAL_EXAMINATION_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void savePatientPhysicalExam_New(ArrayList p_objAL)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PHYSICAL_EXAMINATION_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PHYSICAL_EXAMINATION_DESCRIPTION", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objAL[5].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public DataSet getPatientPhysicalExam(string p_szPatientID)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        objSqlDataAdap.Fill(objDataSet);
        return objDataSet;
    }
    public DataSet getPatientPhysicalExam_New(string p_szBillNumber)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_PATIENT_PHYSICAL_EXAMINATION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        objSqlDataAdap.Fill(objDataSet);
        return objDataSet;
    }


    #region "Work Status"
    public void savePatientLimitations(ArrayList p_objAL)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_LIMITATIONS_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[3].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally { conn.Close(); }
    }
    public void savePatientLimitationsWC4(ArrayList p_objAL)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_LIMITATIONS_ID", p_objAL[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objAL[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objAL[2].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objAL[3].ToString());
            comm.Parameters.AddWithValue("@FLAG", p_objAL[4].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally { conn.Close(); }
    }

    public DataSet getWorkStatusLimitation(string p_szPatientID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);
            objDataSet = new DataSet();
            objSqlDataAdap.Fill(objDataSet);
            return objDataSet;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally { conn.Close(); }
    }
    public DataSet getWorkStatusLimitation_New(string p_szBillNumber)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_WORK_STATUS_LIMITATION_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);
            objDataSet = new DataSet();
            objSqlDataAdap.Fill(objDataSet);
            return objDataSet;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally { conn.Close(); }
    }

    #endregion
    public DataSet GetDoctorsbySpeciality(string p_szBillNumber, string p_szCompanyId)
    {
        objDataSet = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_DOCTORS_BY_BILL_SPECIALITY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyId);
            comm.Parameters.AddWithValue("@FLAG", "GETDATA");

            objSqlDataAdap = new SqlDataAdapter(comm);

            objSqlDataAdap.Fill(objDataSet);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return objDataSet;
    }


}
