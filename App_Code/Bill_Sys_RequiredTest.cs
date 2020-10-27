/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill Sys Required Test
/*Purpose              :       To Add and Edit Room & Required Test
/*Author               :       Bhilendra Y
/*Date of creation     :       29 Nov 2009
/*Modified By          :      
/*Modified Date        :       
/************************************************************/

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
/// Summary description for Bill_Sys_RoomDays
/// </summary>
public class Bill_Sys_RequiredTest
{
    String strConn;
    SqlConnection sqlCon;
    SqlTransaction _Transaction;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_RequiredTest()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    #region Required Test Information
    public void saveRequiredTestInformation(DataTable dtRequiredTest)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_REQUIRED_TEST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@REQUIRED_TEST_ID", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@CASE_ID", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@PROCEDUREGROUP_ID", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@TEST_DATE", SqlDbType.DateTime);
            //sqlCmd.Parameters.Add("@COMPANY_ID", SqlDbType.NVarChar, 2);
            sqlCmd.Parameters.Add("@FLAG", SqlDbType.NVarChar, 50);

            sqlCmd.Parameters["@REQUIRED_TEST_ID"].SourceColumn = "REQUIRED_TEST_ID";
            sqlCmd.Parameters["@CASE_ID"].SourceColumn = "CASE_ID";
            sqlCmd.Parameters["@PROCEDUREGROUP_ID"].SourceColumn = "PROCEDUREGROUP_ID";
            sqlCmd.Parameters["@TEST_DATE"].SourceColumn = "TEST_DATE";
            //sqlCmd.Parameters["@COMPANY_ID"].SourceColumn = "COMPANY_ID";
            sqlCmd.Parameters["@FLAG"].Value = "ADD";


            sqlCmd.Transaction = _Transaction;

            sqlda = new SqlDataAdapter();

            sqlda.InsertCommand = sqlCmd;

            sqlda.Update(dtRequiredTest);

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
    }

    #endregion
    
    #region Get Required Test Details

    public void GetRequiredTestDetails(string strCompanyID, String StrCaseID, ref DataTable dtRequiredTestDetails)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {          
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_REQUIRED_TEST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@CASE_ID", StrCaseID);
            sqlCmd.Parameters.AddWithValue("@COMPANY_ID", strCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "NEW_REQUIRED_TEST_LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dtRequiredTestDetails);
            
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

    public void GetExist_RequiredTestDetails(string strPATIENT_ID, ref DataTable dtRequiredTestDetails)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_REQUIRED_TEST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@CASE_ID", strPATIENT_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "EXIST_REQUIRED_TEST_LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dtRequiredTestDetails);

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
    #endregion

    #region Get Required Test Details
    public int getPatientId(string strPATIENT_ID)
    {        
        sqlCon = new SqlConnection(strConn);
        int intCount=0;
        try
        {
            sqlCon.Open();
            string strQuerys = "SELECT Count(*) FROM MST_REQUIRED_TEST WHERE PATIENT_ID = '" + strPATIENT_ID  + "'";
            sqlCmd = new SqlCommand(strQuerys, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.Text;

            intCount = (int)sqlCmd.ExecuteScalar();


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
        return intCount;
    }
    #endregion

}
