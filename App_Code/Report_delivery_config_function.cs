using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Report_delivery_config_function
/// </summary>
public class Report_delivery_config_function
{
    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
    //SqlDataAdapter da;
    //DataSet ds;
    
	public Report_delivery_config_function()
	{
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}

    public void save_Delivery_report(ArrayList arraylist)
    {

        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(strsqlcon);
         
        try
        {
            conn.Open();
            comm = new SqlCommand("reporting_sp_add_delivery_configuration", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            
            for (int i = 0; i<arraylist.Count ; i++)
            {
                Report_delivery_configurationDAO rdobj = new Report_delivery_configurationDAO();
                rdobj = (Report_delivery_configurationDAO)arraylist[i];
                comm.Parameters.AddWithValue("@i_report_id",rdobj.ireportid);
                comm.Parameters.AddWithValue("@sz_user_id",rdobj.UserID);
                comm.Parameters.AddWithValue("@sz_company_id", rdobj.CompanyID);
                comm.Parameters.AddWithValue("@sz_type", rdobj.sztype);
                comm.Parameters.AddWithValue("@sz_months", rdobj.szmonths);
                //if (rdobj.szmonths != null)
                //{
                //    if (rdobj.szmonths.Trim().Length != 0)
                //    {
                //        comm.Parameters.AddWithValue("@sz_months", rdobj.szmonths);
                //    }
                //}

                //if (rdobj.szdays != null)
                //{
                //    if (rdobj.szdays.Trim().Length != 0)
                //    {
                //        comm.Parameters.AddWithValue("@sz_days", rdobj.szdays);
                //    }
                //}

                comm.Parameters.AddWithValue("@sz_days", rdobj.szdays);
                comm.Parameters.AddWithValue("@sz_time", rdobj.sztime);
                comm.Parameters.AddWithValue("@sz_format", rdobj.ReportDeliveryFormatText);
                comm.Parameters.AddWithValue("@sz_delivery",rdobj.ReportDeliveryTypeText);
                comm.Parameters.AddWithValue("@sz_created_by", rdobj.szcreatedby);
                comm.Parameters.AddWithValue("@sz_weekdays", rdobj.szweek);
                
                //if (rdobj.szweek != null)
                //{
                //    if (rdobj.szweek.Trim().Length != 0)
                //    {
                //        comm.Parameters.AddWithValue("@sz_weekdays", rdobj.szweek);
                //    }
                //}
                
                comm.ExecuteNonQuery();
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

    public DataSet  loadreport(int i_report_id)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn = new SqlConnection(strsqlcon);
        try
        {
            conn.Open();
            comm = new SqlCommand("reporting_sp_load_delivery_configuration", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@i_report_id", i_report_id);
            da = new SqlDataAdapter(comm);
         
            da.Fill(ds);
        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        
        }
        return ds;
    }

    public void Update_Delivery_report(int i_report_id, string sz_company_id, string sz_user_id,string sztype,string szmonths,string szdays,string sztime,string szformat,string szdelivery,string szcreatedby,string szweekdays)
    {
        conn = new SqlConnection(strsqlcon);

        try
        {

            {
                conn.Open();
                comm = new SqlCommand("reporting_sp_update_delivery_configuration", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                Report_delivery_configurationDAO rdobj = new Report_delivery_configurationDAO();
                comm.Parameters.AddWithValue("@i_report_id", i_report_id);
                comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                comm.Parameters.AddWithValue("@sz_user_id", sz_user_id);
                //comm.Parameters.AddWithValue("@sz_created_by",rdobj.szcreatedby);
                comm.Parameters.AddWithValue("@sz_type",sztype);
                comm.Parameters.AddWithValue("@sz_months", szmonths);
                comm.Parameters.AddWithValue("@sz_days", szdays);
                comm.Parameters.AddWithValue("@sz_time", sztime);
                comm.Parameters.AddWithValue("@sz_format",szformat);
                comm.Parameters.AddWithValue("@sz_delivery",szdelivery);
                comm.Parameters.AddWithValue("@sz_created_by",szcreatedby);
                comm.Parameters.AddWithValue("@sz_weekdays", szweekdays);
                comm.ExecuteNonQuery();

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
}