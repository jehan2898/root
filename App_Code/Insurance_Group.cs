using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;


/// <summary>
/// Summary description for Insurance_Group
/// </summary>
public class Insurance_Group
{
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
    public Insurance_Group()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();  
    }
    public DataSet Get_Insurance_Group(string CompanyId)
    {
        try
        {
            ds = new DataSet();
          
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Get Office Detail"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_INSURANCE_COMPANY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@ID", CompanyId);
            comm.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return ds;
    }

    public void Save_Insurance_Group(string [] _objArr,string CompanyID,string GroupName,string UserID)
    {
        SqlTransaction trans = null;
        try
        {
             SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
           
         trans = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_DELETE_INSURANCE_GROUP";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
          comm.Transaction = trans;
            comm.Parameters.AddWithValue("@sz_group_name", GroupName);
            comm.Parameters.AddWithValue("@sz_company_id", CompanyID);
            comm.ExecuteNonQuery();
            foreach (string objid in _objArr)
            {
                //InsuranceSave _objSave = (InsuranceSave)_objArr[i];
                if (objid != "")
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "SP_ADD_INSURANCE_GROUP";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = trans;
                    comm.Parameters.AddWithValue("@sz_insurance_id", objid);
                    comm.Parameters.AddWithValue("@sz_group_name", GroupName);
                    comm.Parameters.AddWithValue("@sz_company_id", CompanyID);
                    comm.Parameters.AddWithValue("@sz_user_id", UserID);
                    comm.ExecuteNonQuery();
                }
            }
            trans.Commit();
    
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            trans.Rollback();
        }

        finally
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
        }

    }

    public DataSet BindGrid(string sz_CompanyID)
    {
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_GET_INSURANCE_GROUPS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            //sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

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
        return ds;
    }
}
public class InsuranceSave
{
    private string _sz_insuarance_id;
    public string InsuranceId
    {
        get
        {
            return _sz_insuarance_id;
        }
        set
        {
            _sz_insuarance_id = value;
        }
    }
}
