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
using log4net;
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for Bill_Sys_OCT_Bills
/// </summary>
public class Bill_Sys_OCT_Bills
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;

    public Bill_Sys_OCT_Bills()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GET_OCT_Bills_detail(string szBillId)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(strsqlCon);

        try
        {
            //comm = new SqlCommand("SP_GET_OCT_BILLS", conn);
            try
            {
                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                {
                    comm = new SqlCommand("SP_OTPT_Pdf_SEC", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                }
                else
                {
                    comm = new SqlCommand("SP_OTPT_Pdf", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                }
            }
            catch
            { 
                comm = new SqlCommand("SP_OTPT_Pdf", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }

            comm.CommandType = CommandType.StoredProcedure;
             comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillId);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }
    public DataSet GET_OCT_Bills_detail(string szBillId,ServerConnection conn)
    {
        DataSet dsReturn = new DataSet();
       

        try
        {
            string Query = "";
            
            //comm = new SqlCommand("SP_GET_OCT_BILLS", conn);
            try
            {
                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                {
                    Query = Query + "Exec SP_OTPT_Pdf_SEC ";
                   
                }
                else
                {
                    Query = Query + "Exec SP_OTPT_Pdf ";
                   
                }
            }
            catch
            {
                Query = Query + "Exec SP_OTPT_Pdf ";
               
            }
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillId, ",");
            
            Query = Query.TrimEnd(',');
            dsReturn = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
           
        }
        return dsReturn;
    }
    public DataSet GET_OCT_Bills_Table(string szBillId)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(strsqlCon);
        try
        {
            comm = new SqlCommand("SP_GET_PROCEDURE_WC_OCT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillId);
            comm.Parameters.AddWithValue("@FLAG", "CODE");
            SqlDataAdapter obj_da = new SqlDataAdapter(comm);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
       
    }

    public DataSet GET_OCT_Bills_Table(string szBillId,ServerConnection conn)
    {
        DataSet dsReturn = new DataSet();
       
        try
        {
            string Query = "";
            Query = Query + "Exec SP_GET_PROCEDURE_WC_OCT ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "CODE", ",");
            Query = Query.TrimEnd(',');
            dsReturn = conn.ExecuteWithResults(Query);
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
           
        }
        return dsReturn;

    }
    public DataSet GET_Patient_Info_OTPT(string szCaseID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(strsqlCon);
        try
        {
            comm = new SqlCommand("SP_OTPT_PATIENT_INFORMATION", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
           // comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            SqlDataAdapter obj_da = new SqlDataAdapter(comm);
            obj_da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
       
    }
}
