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
using ManualEntry;
/// <summary>
/// Summary description for LHR_Visit_Import
/// </summary>
public class LHR_Visit_Import
{
    string strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

	public LHR_Visit_Import()
	{
        strConn =Convert.ToString(ConfigurationManager.AppSettings["Connection_String"]);
	}

    public DataSet FetchState()
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_STATE", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_STATE_ID", null);
            cmd.Parameters.AddWithValue("@SZ_STATE_CODE", null);
            cmd.Parameters.AddWithValue("@SZ_STATE_NAME", null);
            cmd.Parameters.AddWithValue("@FLAG", "List");

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
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

    public DataSet FetchLocation(string CompanyID)
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_LOCATION", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            cmd.Parameters.AddWithValue("@SZ_LOCATION_ID", null);
            cmd.Parameters.AddWithValue("@SZ_LOCATION_NAME", null);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            cmd.Parameters.AddWithValue("@FLAG", "LOCATION_LIST");
            cmd.Parameters.AddWithValue("@ID", CompanyID);
            cmd.Parameters.AddWithValue("@SZ_ADDRESS", null);
            cmd.Parameters.AddWithValue("@SZ_CITY", null);
            cmd.Parameters.AddWithValue("@SZ_STATE_ID", null);
            cmd.Parameters.AddWithValue("@SZ_ZIP", null);
            cmd.Parameters.AddWithValue("@SZ_BILL_DISPLAY_NAME", null);
            cmd.Parameters.AddWithValue("@BT_BILL_DISPLAY_NAME", null);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
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

    public DataSet FetchModality(string CompanyID)
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_FETCH_MODALITY", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@sz_company_ID", CompanyID);
            

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
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

    public string saveLHRData(LHR_Visit_ImportDAO objLHR)
    {
        conn=new SqlConnection (strConn);
        ds=new DataSet ();
        bool manualImport = false;
        SqlTransaction tr;
        conn.Open();
        tr=conn.BeginTransaction();
           string strResult="";

        try
        {
            comm=new SqlCommand("sp_insert_lhr_import",conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType=CommandType.StoredProcedure;
            comm.Transaction=tr;
            comm.Parameters.AddWithValue("@sz_First_Name",objLHR.SZ_First_Name);
            comm.Parameters.AddWithValue("@sz_Last_Name",objLHR.SZ_Last_Name);     
            comm.Parameters.AddWithValue("@sz_Patient_ID",objLHR.SZ_Patient_ID);       
            comm.Parameters.AddWithValue("@sz_Date_Of_Birth",objLHR.SZ_Date_Of_Birth);     
            comm.Parameters.AddWithValue("@sz_Gender",objLHR.SZ_Gender);     
            comm.Parameters.AddWithValue("@sz_Address",objLHR.SZ_Address);     
            comm.Parameters.AddWithValue("@sz_Address2",objLHR.SZ_Address2);     
            comm.Parameters.AddWithValue("@sz_City",objLHR.SZ_City);     
            comm.Parameters.AddWithValue("@sz_State",objLHR.SZ_State);     
            comm.Parameters.AddWithValue("@sz_Zip",objLHR.SZ_Zip);      
            comm.Parameters.AddWithValue("@sz_Case_Type",objLHR.SZ_Case_Type);      
            comm.Parameters.AddWithValue("@sz_SSNO",objLHR.SZ_SSNO);     
            comm.Parameters.AddWithValue("@sz_Date_Of_Appointment",objLHR.SZ_Date_Of_Appointment);     
            comm.Parameters.AddWithValue("@sz_Visit_Time",objLHR.SZ_Visit_Time);     
            comm.Parameters.AddWithValue("@sz_Procedure_Code",objLHR.SZ_Procedure_Code);     
            comm.Parameters.AddWithValue("@sz_Procedure_Desc",objLHR.SZ_Procedure_Desc);        
            comm.Parameters.AddWithValue("@sz_Date_Of_Accident",objLHR.SZ_Date_Of_Accident);     
            comm.Parameters.AddWithValue("@sz_Book_Facility",objLHR.SZ_Book_Facility);     
            comm.Parameters.AddWithValue("@sz_Modality",objLHR.SZ_Modality);      
            comm.Parameters.AddWithValue("@CaseID",objLHR.SZ_Case_ID);
            comm.Parameters.AddWithValue("@sz_Company_ID", objLHR.Company_ID);
            comm.Parameters.AddWithValue("@sz_User_ID", objLHR.User_ID);
            dr=comm.ExecuteReader();

         
            while(dr.Read())
            {
                strResult=dr[0].ToString();
              
            }
            dr.Close();
            if(strResult=="inserted")
            {
                
                 comm=new SqlCommand("sp_get_data_to_import",conn);
                 comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                 comm.CommandType=CommandType.StoredProcedure;
                 comm.Transaction=tr;
                 comm.Parameters.AddWithValue("@CaseID",objLHR.SZ_Case_ID);
                 da = new SqlDataAdapter(comm);
                 da.Fill(ds);
                 
                 ManualEntry.AddManualVisits  objMan=new    ManualEntry.AddManualVisits();
                 manualImport = objMan.ScanAndAddPatient(ds,objLHR.Company_ID);
                

                 comm=new SqlCommand("sp_upadte_import_status",conn);
                 comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                 comm.CommandType=CommandType.StoredProcedure;
                 comm.Transaction = tr;
                 comm.Parameters.AddWithValue("@sz_update_status","Success");
                 comm.Parameters.AddWithValue("@CaseID",objLHR.SZ_Case_ID);
                 comm.ExecuteNonQuery();

                 if (manualImport == false)
                 {
                     tr.Rollback();
                 }
                 else
                 tr.Commit();
            }
            else
            {
                tr.Rollback();
                
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            tr.Rollback();

        }
        finally
        {
            if(conn.State== ConnectionState.Open)
            {
                conn.Close();
            }
        }

         return strResult;
    }



}