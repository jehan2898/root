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
using System.IO;

/// <summary>
/// Summary description for Bill_Sys_AssociateDocForMedical
/// </summary>
public class Bill_Sys_AssociateDocForMedical
{
	public Bill_Sys_AssociateDocForMedical()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    private string _CaseID;

    public string CaseID
    {
        get { return _CaseID; }
        set { _CaseID = value; }
    }

    private string _ImageID;

    public string ImageID
    {
        get { return _ImageID; }
        set { _ImageID = value; }
    }
    private string _EventID;

    public string EventID
    {
        get { return _EventID; }
        set { _EventID = value; }
    }


    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();

    public string SaveUploadedDocInDocMng(ArrayList arr)
    {
        string retMsg = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_DOC_IN_DM", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", arr[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", arr[5].ToString());
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", arr[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", arr[7].ToString());
            
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
        retMsg = "Success";
        return retMsg;
    }

    public DataSet getGridDetail(ArrayList arr)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ASSOCIATE_DOC_FOR_MEDIACL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@i_event_id", arr[1].ToString());
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

    public string DeleteDoc(ArrayList arr)
    {
        string retMsg = "";
        sqlCon = new SqlConnection(strsqlCon);
         
         SqlTransaction sqltr;
         sqlCon.Open();
         sqltr = sqlCon.BeginTransaction();
         
        try
        {
            for (int i = 0; i < arr.Count; i++)
			{
                Bill_Sys_AssociateDocForMedical obj= new Bill_Sys_AssociateDocForMedical ();
                obj=(Bill_Sys_AssociateDocForMedical)arr[i];
                sqlCmd = new SqlCommand("SP_DELETE_ASSOCIATE_DOC_FOR_MEDIACL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandTimeout = 0;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@i_image_id ", obj.ImageID);
                sqlCmd.Parameters.AddWithValue("@I_EVENT_ID ", obj.EventID);
                sqlCmd.Transaction = sqltr;
                sqlCmd.ExecuteNonQuery();
			 
			}

            sqltr.Commit();
            retMsg = "Success";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sqltr.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
       
        return retMsg;
    }

}