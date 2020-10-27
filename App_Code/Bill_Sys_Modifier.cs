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
/// Summary description for Bill_Sys_Modifier
/// </summary>
public class Bill_Sys_Modifier
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    public Bill_Sys_Modifier()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetModifierData(string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsModifier = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_get_modifier_details", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyId);
            
            da = new SqlDataAdapter(comm);
            da.Fill(dsModifier);

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
        return dsModifier;
    }

    public string ModifierData(string companyID, string userID, string Modifier, string Id_Code, string ModifierDesc, string ModifierID, string flag)
    {
        //SqlParameter sqlParam = new SqlParameter();
        string msg = "";
        int i = 0;
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsMessege = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_modifier_details", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_user_id", userID);
            comm.Parameters.AddWithValue("@sz_modifier_id", ModifierID);
            comm.Parameters.AddWithValue("@sz_modifier", Modifier);
            comm.Parameters.AddWithValue("@sz_modifier_code", Id_Code);
            comm.Parameters.AddWithValue("@sz_modifier_desc", ModifierDesc);
            comm.Parameters.AddWithValue("@flag", flag);
            //i = comm.ExecuteNonQuery();
            //if (i > 0)
            //{
            //    msg = "SUCCESS";
            //}
            //else
            //{
            //    msg = "Modifier already exist.";
            //}
            da = new SqlDataAdapter(comm);
            da.Fill(dsMessege);
            if (dsMessege.Tables.Count > 0 && dsMessege.Tables[0].Rows.Count > 0)
            {
                msg = dsMessege.Tables[0].Rows[0][0].ToString();
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
        return msg;
    }

    public DataSet SearchModifierData(string companyID, string userID, string Modifier, string Id_Code, string ModifierDesc, string flag)
    {
        //SqlParameter sqlParam = new SqlParameter();
        string msg = "";
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsModifier = new DataSet();
        string szImageId = "";
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_modifier_details", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_user_id", userID);
            comm.Parameters.AddWithValue("@sz_modifier", Modifier);
            comm.Parameters.AddWithValue("@sz_modifier_code", Id_Code);
            comm.Parameters.AddWithValue("@sz_modifier_desc", ModifierDesc);
            comm.Parameters.AddWithValue("@flag", flag);

            da = new SqlDataAdapter(comm);
            da.Fill(dsModifier);

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
        return dsModifier;
    }

    public string DeleteModifierData(string companyID, string ModifierIDs, string flag)
    {
        //SqlParameter sqlParam = new SqlParameter();
        string msg = "";
        int i = 0;
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsImageID = new DataSet();
        string szImageId = "";
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_modifier_details", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyID);
            comm.Parameters.AddWithValue("@sz_modifier_id", ModifierIDs);
            comm.Parameters.AddWithValue("@flag", flag);
            i = comm.ExecuteNonQuery();
            if (i > 0)
            {
                msg = "SUCCESS";
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
        return msg;
    }

}
