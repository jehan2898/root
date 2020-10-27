using Microsoft.SqlServer.Management.Common;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;

public class Bill_Sys_NF3_Template
{
    private string strConn;

    private SqlConnection sqlCon;

    private SqlCommand sqlCmd;

    private SqlDataAdapter sqlda;

    private DataSet ds;

    public Bill_Sys_NF3_Template()
    {
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet getBillList(string p_szBillNumber)
    {
        DataSet dataSet = new DataSet();
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_BILL_LIST", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public DataSet getBillPath(string p_szBillNumber)
    {
        DataSet dataSet = new DataSet();
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("get_bill_path", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public string GetCompanyName(string p_szCompanyID)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        string str = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_COMPANY_NAME", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader[0] == DBNull.Value)
                    {
                        continue;
                    }
                    str = sqlDataReader[0].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public int getDiagnosisCodeCount(string p_szBillID)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        int num = 0;
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_NF3_TEMPLATE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETDIGNOSISCODECOUNT");
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    num = Convert.ToInt32(sqlDataReader["Diag_Count"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return num;
    }
    public int getDiagnosisCodeCount(string p_szBillID, ServerConnection conn)
    {
        SqlDataReader sqlDataReader = null;
        int num = 0;
        try
        {
            try
            {
                String Query = "";
                Query = Query + "Exec SP_NF3_TEMPLATE ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", p_szBillID, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GETDIGNOSISCODECOUNT", ",");
                Query = Query.TrimEnd(',');
                sqlDataReader = conn.ExecuteReader(Query);
                while (sqlDataReader.Read())
                {
                    num = Convert.ToInt32(sqlDataReader["Diag_Count"].ToString());
                }
                sqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        finally
        {
            if (sqlDataReader != null && !sqlDataReader.IsClosed)
            {

                sqlDataReader.Close();
            }
        }
        return num;
    }

    public string getGroup(string p_szBillNumber)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_PROCEDURE_GROUP", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["GROUP"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }
    public string getGroup(string p_szBillNumber, ServerConnection conn)
    {
        string str = "";
        SqlDataReader sqlDataReader = null;

        try
        {
            string Query = "";
            Query = Query + "Exec SP_GET_PROCEDURE_GROUP ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", p_szBillNumber, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GET_PATIENT_ID", ",");
            Query = Query.TrimEnd(',');
            sqlDataReader = conn.ExecuteReader(Query);
            while (sqlDataReader.Read())
            {
                str = sqlDataReader["GROUP"].ToString();
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message, ex);
        }

        finally
        {
            if (sqlDataReader != null && !sqlDataReader.IsClosed) { sqlDataReader.Close(); }
        }
        return str;
    }
    public string GetImageID(string szFileName, string szFilePath, string szFilePath2)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_IMAGEID_FROM_FILE_FILEPATH", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", szFileName);
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", szFilePath);
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH2", szFilePath2);
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["ImageID"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public string getPhysicalPath()
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                SqlDataReader sqlDataReader = (new SqlCommand("select PhysicalBasePath from tblBasePath where BasePathId=(Select ParameterValue from tblapplicationsettings where parametername = 'BasePathId')", this.sqlCon)).ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["PhysicalBasePath"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public DataSet GetPomCaseId(string szPomId, string szCompanyId)
    {
        DataSet dataSet = new DataSet();
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_BILL_POM_CASE_ID ", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@I_POM_ID", szPomId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetVerification_Answer(string szBillNo, string szCompanyId)
    {
        DataSet dataSet = new DataSet();
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_input_bill_number", szBillNo);
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@bt_operation", 2);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public string getVersion(string p_szBillNumber)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VERSION_NUMBER", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["SZ_VERSION"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public void InsertPaymentImage(string _billNumber, string _companyId, string _imgId, string _userId, string _verificationID, string flag)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _billNumber);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_IMAGE_ID", _imgId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _userId);
                this.sqlCmd.Parameters.AddWithValue("@I_TRANSACTION_ID", _verificationID);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void PomInsertImage(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.sqlCon.Open();
        try
        {
            try
            {
                this.sqlCmd = new SqlCommand("sp_insert_pom_imagd_id", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_case_id", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_user_name", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@i_image_id", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SOURCE_ID", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_ip_address", objAL[5].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public string SaveDocumentData(ArrayList arrayList)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        string str = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REQ_DOCUMENT", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", arrayList[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_NAME", arrayList[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", arrayList[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", arrayList[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", arrayList[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_MST_NODE_ID", arrayList[6].ToString());
                SqlParameter sqlParameter = new SqlParameter("@i_image_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                str = this.sqlCmd.Parameters["@i_image_id"].Value.ToString();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }
    public void saveGeneratedBillContractPath(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_CONTRACT_GENERATED", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
    public void saveGeneratedBillPath(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_GENERATED", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void saveGeneratedBillPath(ArrayList objAL, ServerConnection conn)
    {
        //this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec SP_TXN_BILL_GENERATED ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", objAL[0].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_PATH", objAL[1].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objAL[2].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", objAL[3].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NAME", objAL[4].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_FILE_PATH", objAL[5].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_USER_NAME", objAL[6].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_SPECIALITY", objAL[7].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@CASE_TYPE", objAL[8].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_NO", objAL[9].ToString(), ",");

                Query = Query.TrimEnd(',');
                conn.ExecuteNonQuery(Query);
            }
            catch (SqlException ex)
            {
                throw ex;
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (this.sqlCon.State == ConnectionState.Open)
            //{
            //    this.sqlCon.Close();
            //}
        }
    }

    public void saveGeneratedBillPath_New(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_GENERATED_NEW", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", objAL[10].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
    public void saveGeneratedBillContractPath_New(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_CONTRACT_GENERATED_NEW", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", objAL[10].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
    public void saveGeneratedBillPath_New(ArrayList objAL, ServerConnection conn)
    {
        // this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                //this.sqlCon.Open();
                string Query = "";
                Query = Query + "Exec SP_TXN_BILL_GENERATED_NEW ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", objAL[0].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_PATH", objAL[1].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objAL[2].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", objAL[3].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NAME", objAL[4].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_FILE_PATH", objAL[5].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_USER_NAME", objAL[6].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_SPECIALITY", objAL[7].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@CASE_TYPE", objAL[8].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_NO", objAL[9].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_NODE_TYPE", objAL[10].ToString(), ",");

                Query = Query.TrimEnd(',');
                conn.ExecuteNonQuery(Query);
            }
            catch (SqlException ex)
            {
                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (this.sqlCon.State == ConnectionState.Open)
            //{
            //    this.sqlCon.Close();
            //}
        }
    }
    public void saveGeneratedNF3File(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_FILE_LIST", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
    public void saveGeneratedNF3File(ArrayList objAL, ServerConnection conn)
    {

        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec SP_MST_FILE_LIST ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_FILE_NAME", objAL[0].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", objAL[1].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", objAL[2].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objAL[3].ToString(), ",");
                Query = Query.TrimEnd(',');
                conn.ExecuteNonQuery(Query);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        finally
        {

        }
    }

    public void saveOutScheduleReportInDocumentManager(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_SHEDULE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public int saveReportInDocumentManager(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_AOB(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_AOB", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_INTAKE_MISC(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_INTAKE_MISC", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_MISC(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_MISC", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_NFCA(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFCA", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_NFHC(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFHC", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_NFLF(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFLF", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }

    public int saveReportInDocumentManager_Referral(ArrayList objAL)
    {
        int value = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_REFERRAL", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
                if (!objAL[5].ToString().Equals("X-RAY"))
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
                }
                else
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
                }
                SqlParameter sqlParameter = new SqlParameter()
                {
                    Direction = ParameterDirection.ReturnValue
                };
                this.sqlCmd.Parameters.Add(sqlParameter);
                this.sqlCmd.ExecuteNonQuery();
                value = (int)sqlParameter.Value;
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return value;
    }
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    public int SetVerification_Answer(ArrayList objAL, string szCompanyId, string szUserName)
    {
        int num = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        this.sqlCon.Open();
        SqlTransaction sqlTransaction = this.sqlCon.BeginTransaction();
        try
        {
            try
            {
                for (int i = 0; i < objAL.Count; i++)
                {
                    Bill_Sys_Verification_Desc billSysVerificationDesc = new Bill_Sys_Verification_Desc();
                    billSysVerificationDesc = (Bill_Sys_Verification_Desc)objAL[i];
                    this.sqlCmd = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        Transaction = sqlTransaction,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@i_verification_id", billSysVerificationDesc.sz_verification_id);
                    this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", billSysVerificationDesc.sz_bill_no);
                    this.sqlCmd.Parameters.AddWithValue("@sz_case_id", billSysVerificationDesc.sz_case_id);
                    this.sqlCmd.Parameters.AddWithValue("@sz_user_name", szUserName);
                    this.sqlCmd.Parameters.AddWithValue("@sz_answer", billSysVerificationDesc.sz_answer);
                    this.sqlCmd.Parameters.AddWithValue("@sz_company_id", szCompanyId);
                    this.sqlCmd.Parameters.AddWithValue("@bt_operation", 1);
                    this.sqlCmd.ExecuteNonQuery();
                    #region Activity_Log
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "VER_SENT";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "for  Bill Id : " + billSysVerificationDesc.sz_bill_no + " and Verfication Id " + billSysVerificationDesc.sz_verification_id;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)HttpContext.Current.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = billSysVerificationDesc.sz_case_id;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    #endregion
                }
                sqlTransaction.Commit();
                num = 1;
            }
            catch (SqlException ex)
            {

                sqlTransaction.Rollback();
                num = 0;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return num;
    }

    public void UpdateDocMgr(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_WS_UPLOAD_DOCUMENT", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_TAG_ID", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[5].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void UpdateLitigantion(ArrayList arr, string szCompanyId)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < arr.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_UPDATE_BILL_STATUS", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", "LT");
                    this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", string.Concat("'", arr[i].ToString(), "'"));
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                    this.sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void UpdateReportPomPath(string fileName, string filePath, string userId, string pomId, string recimgid, string status)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_UPDATE_REPORT_POM_PATH", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_POM_ID", pomId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_FILE_NAME", fileName);
                this.sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_FILE_PATH", filePath);
                this.sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_USER_ID", userId);
                this.sqlCmd.Parameters.AddWithValue("@I_RECEIVED_IMAGE_ID", recimgid);
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS", status);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
}