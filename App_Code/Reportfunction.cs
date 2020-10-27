using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for Reportfunction
/// </summary>
public class Reportfunction
{
    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter adpt;
    DataSet ds;

	public Reportfunction()
	{
		//
		// TODO: Add constructor logic here
		//
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}


    public DataSet getsubtitle()
    {
        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_SubTitleDetails", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
           // comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }
    public DataSet load_office(string companyid)
    {
        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_REF_OFF_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadCaseStatus(string companyid)
    {
        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_case_status", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    
    }

    public DataSet loadName(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_name", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
      
    }

    public DataSet loadCaseType(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_case_type", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }
   
    public DataSet loadCarrier(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_carrier", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadProvider(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_provider", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadAttorney(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_attorney", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadDoctor(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_doctor", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadSpecialty(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_specialty", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadLocation(string companyid)
    {

        conn = new SqlConnection(strsqlcon);
        ds = new DataSet();
        adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_location", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public DataSet loadspeciality(string flag, string id)
    {
        conn = new SqlConnection(strsqlcon);
        adpt = null;
        ds = new DataSet();
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@id", id);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public void Save_Report(ArrayList arrlist)
    {
        DataTable dt = new DataTable();
        dt = new DataTable("Report");
       
        dt.Columns.Add("CONTROL_KEY", typeof(string));
        dt.Columns.Add("CONTROL_VALUE", typeof(string));

        for (int i = 0; i < arrlist.Count; i++)
        {
            PReportDAO obj = new PReportDAO();
            obj = (PReportDAO)arrlist[i];
            DataRow row = dt.NewRow();
            row["CONTROL_KEY"] = obj.SZCONTROLKEY;
            row["CONTROL_VALUE"] = obj.SZCONTROLVALUE;
            dt.Rows.Add(row);
        }
        conn = new SqlConnection(strsqlcon);
        try
        {

            {
                conn.Open();
                comm = new SqlCommand("SP_PREPORT", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                SqlParameter tblvaluetype = comm.Parameters.AddWithValue("@preport", dt);  //Passing table value parameter
                tblvaluetype.SqlDbType = SqlDbType.Structured; // This one is used to tell ADO.NET we are passing Table value Parameter
                int result = comm.ExecuteNonQuery();

            }
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
    
    public DataSet loadsearch(ArrayList arr)
    {
        SqlDataAdapter da;
        SqlConnection conn;
        SqlCommand comm;
        DataSet ds = new DataSet();
        conn = new SqlConnection(strsqlcon);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_VISIT_OF_REF_OFF_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            for (int i = 0; i < arr.Count; i++)
            {
                PReportDAO obj = new PReportDAO();
                obj = (PReportDAO)arr[i];
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SCOMPANY);
                comm.Parameters.AddWithValue("@SZ_OFFICE_ID", obj.SOFFICE);
                comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", obj.SSPECIALITY);
                //comm.Parameters.AddWithValue("", obj.DTTYPE);
                comm.Parameters.AddWithValue("@DT_FROM_VISIT_DATE", obj.DTFROM);
                comm.Parameters.AddWithValue("@DT_TO_VISIT_DATE", obj.DTTO);

            }
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {

        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    //public int addnotes(dao.ContentNotesDAO obj )
    //{
    //    conn = new SqlConnection(strsqlcon);
    //    int result = 0;
    //    try
    //    {

    //        conn.Open();
    //        comm = new SqlCommand("sp_Add_notes", conn);
    //        comm.CommandTimeout = 500;
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Connection = conn;
    //        comm.Parameters.AddWithValue("@sz_companyid", obj.IContentID.ToString());
    //        comm.Parameters.AddWithValue("@sz_title", obj.STitle.ToString());
    //        comm.Parameters.AddWithValue("@sz_process", obj.SProcess.ToString());
    //        comm.Parameters.AddWithValue("@sz_User_Id", obj.SCreatedBy.ToString());
    //        comm.Parameters.AddWithValue("@")
    //        result = comm.ExecuteNonQuery();
    //        if (result == 1)
    //        {
    //            return result;
    //        }
    //        else if (result == -1)
    //        {
    //            return result;
    //        }

    //    }

    //   catch (Exception ex)
    //    {
    //        _ex.Message.ToString();
    //    }

    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open)
    //        {
    //            conn.Close();

    //        }
    //    }

    //    return result;
    //}

    public Result SaveNotesTransaction(dao.ContentNotesDAO obj)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlcon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {
            comm.CommandText = "sp_Add_notes";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@sz_title", obj.STitle.ToString());
            comm.Parameters.AddWithValue("@sz_process", obj.SProcess.ToString());
            comm.Parameters.AddWithValue("@sz_filename", obj.SFileName.ToString());
            comm.Parameters.AddWithValue("@sz_user_id",obj.SCreatedBy.ToString() );
            comm.Parameters.AddWithValue("@sz_companyid", obj.Scompanyid.ToString());
            //comm.ExecuteNonQuery();

            SqlDataReader dr = comm.ExecuteReader();
            string IContentID = "";
            while (dr.Read())
            {
                IContentID = Convert.ToString(dr[0]);
            }
            dr.Close();
            ArrayList arr = obj.GetContain();
            if (IContentID != null)
            {
                if (arr != null)
                {
                    if (arr.Count > 0)
                    {
                        for (int i = 0; i < arr.Count; i++)
                        {
                            dao.ContentSubNotesDAO obj1 = (dao.ContentSubNotesDAO)arr[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "sp_add_detail_notes";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;

                            comm.Parameters.AddWithValue("@id_mst_notes", IContentID);
                            comm.Parameters.AddWithValue("@sz_userid", obj1.SCreatedBy.ToString());
                            comm.Parameters.AddWithValue("@sz_sub_title", obj1.SSubTitle.ToString());
                            comm.Parameters.AddWithValue("@sz_description", obj1.SSubDescription.ToString());
                            comm.ExecuteNonQuery();

                        }
                    }
                }
            }
             transaction.Commit();
           }
        catch (Exception ex)

        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }
    //public DataSet GetIns(ReConseileDAO objReConseileDAO)
    //{
    //    conn = new SqlConnection(strsqlcon);
    //    ds = new DataSet();
    //    try
    //    {
    //        conn.Open();
    //        comm = new SqlCommand("SP_MST_INSURANCE_COMPANY", conn);
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Parameters.AddWithValue("@ID", objReConseileDAO.CompanyID);
    //        comm.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");

    //        adpt = new SqlDataAdapter(comm);
    //        adpt.Fill(ds);

    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open)
    //        {
    //            conn.Close();
    //        }
    //    }
    //    return ds;
    //}

    //public DataSet GetData(ReConseileDAO objReConseileDAO)
    //{
    //    conn = new SqlConnection(strsqlcon);
    //    ds = new DataSet();
    //    try
    //    {
    //        conn.Open();
    //        comm = new SqlCommand("sp_get_report_recon", conn);
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Parameters.AddWithValue("@sz_company_id", objReConseileDAO.CompanyID);
    //        comm.Parameters.AddWithValue("@sz_doctor_id", objReConseileDAO.DocterID);
    //        comm.Parameters.AddWithValue("@sz_procedure_group_id", objReConseileDAO.ProcedureGroupID);
    //        comm.Parameters.AddWithValue("@sz_office_id", objReConseileDAO.OfficeID);
    //        comm.Parameters.AddWithValue("@dt_from_visit_date", objReConseileDAO.VisitFromDate);
    //        comm.Parameters.AddWithValue("@dt_to_visit_date", objReConseileDAO.VisitToDate);
    //        comm.Parameters.AddWithValue("@dt_from_bill_date", objReConseileDAO.BillFromDate);
    //        comm.Parameters.AddWithValue("@dt_to_bill_date", objReConseileDAO.BillToDate);
    //        comm.Parameters.AddWithValue("@sz_insurance_id", objReConseileDAO.InsuranceID);
    //        comm.Parameters.AddWithValue("@sz_paid_unpaid", objReConseileDAO.PaidStatus);
    //        comm.Parameters.AddWithValue("@sz_recon", objReConseileDAO.ReconcileStatus);

    //        adpt = new SqlDataAdapter(comm);
    //        adpt.Fill(ds);

    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open)
    //        {
    //            conn.Close();
    //        }
    //    }
    //    return ds;
    //}
}