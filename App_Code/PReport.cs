using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
/// <summary>
/// Summary description for PReport
/// </summary>
public class PReport
{
    string strcon;
	public PReport()
	{
        strcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}

    public DataSet loadsearch(ArrayList arr)
    {
        SqlDataAdapter da;
        SqlConnection conn;
        SqlCommand comm;
        DataSet ds=new DataSet();
        conn = new SqlConnection();
        try
        {            
            conn.Open();
            comm = new SqlCommand("SP_GET_VISIT_OF_REF_OFF_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            for(int i=0;i<arr.Count;i++)
            {
                PReportDAO obj=new PReportDAO();
                obj=(PReportDAO)arr[i];
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID",obj.SCOMPANY);
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
}