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
using System.IO;


/// <summary>
/// Summary description for Bill_Sys_RequiredDocumentBO
/// </summary>
namespace RequiredDocuments
{
    public class Bill_Sys_RequiredDocumentBO
    {
        String strsqlCon;
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        DataTable dt;
        REQUIREDDOCUMENT_EO _reqDocumentEO;
        public Bill_Sys_RequiredDocumentBO()
        {
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        }

        private ArrayList LoadPath(ArrayList objarr)
        {
            ArrayList objpatharr = new ArrayList();
            DataTable dt = new DataTable();
            string _ischeck = "";
            string _NodeName = "";
            try
            {
                // here call the store proc and get all doucment id , name and path . this information store in object and per object store in one array list 
                // and that arraylist sent back to method 
                dt = GetAllReceiveDocument(objarr);
                foreach (DataRow dr in dt.Rows)
                {
                    if (_ischeck == "")
                    {
                        _reqDocumentEO = new REQUIREDDOCUMENT_EO();
                        _NodeName = dr["NodeName"].ToString();
                        _reqDocumentEO.SZ_DOCUMENT_ID = dr["NodeID"].ToString();
                        _reqDocumentEO.SZ_DOCUMENT_NAME = dr["NodeName"].ToString();
                        _reqDocumentEO.SZ_DOCUMENT_PATH = dr["PhysicalBasePath"] + dr["FilePath"].ToString();
                        objpatharr.Add(_reqDocumentEO);
                        _ischeck = "1";
                    }

                    if (_NodeName != dr["NodeName"].ToString())
                    {
                        _reqDocumentEO = new REQUIREDDOCUMENT_EO();
                        _reqDocumentEO.SZ_DOCUMENT_ID = dr["NodeID"].ToString();
                        _reqDocumentEO.SZ_DOCUMENT_NAME = dr["NodeName"].ToString();
                        _reqDocumentEO.SZ_DOCUMENT_PATH = dr["PhysicalBasePath"] + dr["FilePath"].ToString();
                        objpatharr.Add(_reqDocumentEO);
                        _NodeName = dr["NodeName"].ToString();

                    }

                }


            }
            catch (Exception ex)
            { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
            return objpatharr;

        }

        public REQUIREDDOCUMENT_EO CheckExists(ArrayList objarr)
        {
            _reqDocumentEO = new REQUIREDDOCUMENT_EO();
            try
            {
                ArrayList objlist = new ArrayList();
                Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
                objlist = LoadPath(objarr);
                string _doucumentPath = "";
                bool _ischeck = false;
                String szSourceFile1 = "";
                String szSourceFile1_FullPath = "";
                String szSourceFile2 = "";
                String szSourceFile2_FullPath = "";
                String szOpenFilePath = "";
                String szBasePhysicalPath = _objTemp.getPhysicalPath();
                string szerrmsg = "";


                // this objlist arraylist store all information of document and check every document path is correct or not 
                // suppose file not found on specific path that time show the error msg otherwise merge document and sent back the openfile path 
                for (int i = 0; i < objlist.Count; i++)
                {

                    //if check file is exist or not
                    if (!System.IO.File.Exists(((REQUIREDDOCUMENT_EO)(objlist[i])).SZ_DOCUMENT_PATH.ToString()))
                    {
                        _ischeck = true;

                        szerrmsg = szerrmsg + ((REQUIREDDOCUMENT_EO)(objlist[i])).SZ_DOCUMENT_NAME.ToString() + ",";

                    }

                }
                if (szerrmsg != "")
                {
                    _reqDocumentEO.SZ_ERROR_MSG = szerrmsg + " does not exists. Do you want to proceed?";
                }

            }
            catch (Exception ex)
            { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
            return _reqDocumentEO;
        }

        public REQUIREDDOCUMENT_EO MergeDocument(ArrayList objarr)
        {
            _reqDocumentEO = new REQUIREDDOCUMENT_EO();
            try
            {
                ArrayList objlist = new ArrayList();
                ArrayList objFilelist = new ArrayList();
                Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
                objFilelist = LoadPath(objarr);
                string _doucumentPath = "";
                bool _ischeck = false;
                String szSourceFile1 = "";
                String szSourceFile1_FullPath = "";
                String szSourceFile2 = "";
                String szSourceFile2_FullPath = "";
                String szOpenFilePath = "";
                String szBasePhysicalPath = _objTemp.getPhysicalPath();
                string szerrmsg = "";

                for (int i = 0; i < objFilelist.Count; i++)
                {

                    //if check file is exist or not
                    if (System.IO.File.Exists(((REQUIREDDOCUMENT_EO)(objFilelist[i])).SZ_DOCUMENT_PATH.ToString()))
                    {
                        objlist.Add(objFilelist[i]);

                    }

                }


                //                        CompanyName                       caseID
                String szDefaultPath = objarr[3].ToString() + "/" + objarr[0].ToString() + "/Packet Document/";
                string _fullPath = szBasePhysicalPath + szDefaultPath;
                if (!Directory.Exists(_fullPath))
                {
                    Directory.CreateDirectory(_fullPath);
                }
                if (objlist.Count > 1)
                {

                    for (int j = 0; j < objlist.Count; j++)
                    {

                        if (szSourceFile1 == "")
                        {


                            szSourceFile1 = getFileName("Packet") + ".pdf";
                            szSourceFile1_FullPath = Convert.ToString(((REQUIREDDOCUMENT_EO)(objlist[j])).SZ_DOCUMENT_PATH);
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }
                        else
                        {

                            szSourceFile2_FullPath = Convert.ToString(((REQUIREDDOCUMENT_EO)(objlist[j])).SZ_DOCUMENT_PATH);
                            MergePDF.MergePDFFiles(szSourceFile1_FullPath, szSourceFile2_FullPath, szBasePhysicalPath + szDefaultPath + szSourceFile1);

                            szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }
                    }


                }
                else
                {
                    szSourceFile1 = getFileName("Packet") + ".pdf";
                    szSourceFile1_FullPath = Convert.ToString(((REQUIREDDOCUMENT_EO)(objlist[0])).SZ_DOCUMENT_PATH);
                    szOpenFilePath = szDefaultPath + szSourceFile1;
                    File.Copy(Convert.ToString(((REQUIREDDOCUMENT_EO)(objlist[0])).SZ_DOCUMENT_PATH), szBasePhysicalPath + szDefaultPath + szSourceFile1);

                }
                _reqDocumentEO.SZ_OPEN_FILE_PATH = szOpenFilePath;



            }
            catch (Exception ex)
            { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
            return _reqDocumentEO;
        }
        private string getFileName(string startName)
        {
            String szStartName = "";
            szStartName = startName;
            String szFileName;
            DateTime currentDate;
            currentDate = DateTime.Now;
            szFileName = szStartName + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
            return szFileName;
        }
        private string getRandomNumber()
        {
            System.Random objRandom = new Random();
            return objRandom.Next(1, 10000).ToString();
        }

        private DataTable GetAllReceiveDocument(ArrayList _pObjArr)
        {
            dt = new DataTable();

            try
            {
                sqlCon = new SqlConnection(strsqlCon);
                sqlCmd = new SqlCommand("GET_PACKET_DOCUMENTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _pObjArr[0].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _pObjArr[1].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _pObjArr[2].ToString());
                sqlda = new SqlDataAdapter(sqlCmd);

                sqlda.Fill(dt);



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
            return dt;

        }





        #region "Search Method"
        public DataSet GetBillReports(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_From_Date); }
                if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_ToDate); }
                if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
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

        public DataSet GetBillReportsByDate(string P_Company_Id, string P_From_Date, string P_ToDate)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_From_Date); }
                if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_ToDate); }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
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


        public DataSet GetBillReportsByDateAndSpecialty(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_AND_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_From_Date); }
                if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_ToDate); }
                if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
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


        public DataSet GetBillReportsBySpecialty(string P_Company_Id, string P_Speciality)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
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


        public DataSet GetBillReportsByServiceDate(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality, string P_Speciality_FromDate, string P_Speciality_ToDate)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            sqlCon.Open();
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality == "" || P_Speciality == "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SERVICE_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality == "" || P_Speciality == "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_ALLDATES", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_AND_SERVICE_DATE ", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_AND_ALLDATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                if (P_Company_Id != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id); }
                if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_From_Date); }
                if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_ToDate); }
                if (P_Speciality != "" && P_Speciality.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
                if (P_Speciality_FromDate != "") { sqlCmd.Parameters.AddWithValue("@DT_SERVICE_START_DATE", P_Speciality_FromDate); }
                if (P_Speciality_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_SERVICE_END_DATE", P_Speciality_ToDate); }
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                sqlCon.Close();
            }
            return ds;
        }

        public DataSet GetBillReportsByBillStatus(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality, string P_Speciality_FromDate, string P_Speciality_ToDate, string P_Bill_Status)
        {
            sqlCon = new SqlConnection(strsqlCon);
            ds = new DataSet();
            sqlCon.Open();
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality == "" || P_Speciality == "NA") && (P_Speciality_FromDate == "" && P_Speciality_ToDate == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_BILLSTATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality == "" || P_Speciality == "NA") && (P_Speciality_FromDate == "" && P_Speciality_ToDate == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_BILL_DATE_BILLSTATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Bill Date
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality == "" || P_Speciality == "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SERVICE_DATE_BILLSTATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Visit Date
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate == "" && P_Speciality_ToDate == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_AND_BILL_STATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Speciality
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_ALLDATE_BILLSTATUS ", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Speciality and Visit Date And Bill Date
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate == "" && P_Speciality_ToDate == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_Bill_DATE_BILLSTATUS ", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Speciality  And Bill Date
            if ((P_From_Date == "" && P_ToDate == "") && (P_Speciality != "" && P_Speciality.ToString() != "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_SPECIALTY_VISIT_DATE_BILLSTATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Speciality  And Visit Date
            if ((P_From_Date != "" && P_ToDate != "") && (P_Speciality == "" || P_Speciality.ToString() == "NA") && (P_Speciality_FromDate != "" && P_Speciality_ToDate != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_FOR_PACKET_ALLDATE_BILLSTATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//Search With Bill Status and Bill Date  And Visit Date

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                if (P_Company_Id != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id); }
                if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_From_Date); }
                if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_ToDate); }
                if (P_Speciality != "" && P_Speciality.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
                if (P_Speciality_FromDate != "") { sqlCmd.Parameters.AddWithValue("@DT_SERVICE_START_DATE", P_Speciality_FromDate); }
                if (P_Speciality_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_SERVICE_END_DATE", P_Speciality_ToDate); }
                if (P_Bill_Status != "") { sqlCmd.Parameters.AddWithValue("@BILL_STATUS", P_Bill_Status); }
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                sqlCon.Close();
            }
            return ds;
        }

        #endregion


        DAO_NOTES_EO _DAO_NOTES_EO = null;
        DAO_NOTES_BO _DAO_NOTES_BO = null;

        public void DeleteRequiredDocuments(ArrayList p_List)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(strsqlCon);
                sqlCon.Open();

                Bill_Sys_RequiredDAO dao;
                string szFilePath = "";
                for (int i = 0; i < p_List.Count; i++)
                {
                    dao = (Bill_Sys_RequiredDAO)p_List[i];
                    SqlCommand sqlCmd = new SqlCommand("sp_delete_required_documents", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@i_image_id", dao.ImageID);
                    sqlCmd.Parameters.AddWithValue("@i_txn_id", dao.TransactionID);
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", dao.CaseId);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", dao.CompanyId);
                    SqlParameter paramImageID = new SqlParameter("@sz_file_path", SqlDbType.NVarChar);
                    paramImageID.Direction = ParameterDirection.Output;
                    paramImageID.Size = 255;
                    sqlCmd.Parameters.Add(paramImageID);
                    sqlCmd.ExecuteNonQuery();
                    szFilePath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    if (szFilePath != null)
                    {
                        if (szFilePath != "")
                        {
                            if (System.IO.File.Exists(szFilePath))
                            {
                                try
                                {
                                    System.IO.File.Move(szFilePath, szFilePath + ".deleted.req");
                                }
                                catch (Exception ex)
                                {
                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                                }
                            }
                        }
                    }
                    #region Activity_Log
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "DOC_DELETED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Document Id  : " + dao.ImageID + " Deleted . ";
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)HttpContext.Current.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)HttpContext.Current.Session["CASE_OBJECT"]).SZ_CASE_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    #endregion
                }
            }
            catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon = null;
                }
            }
        }

        public string GetNodePath(string p_szNodeID, string p_szCaseID, string p_szCompanyID)
        {
            sqlCon = new SqlConnection(strsqlCon);
            SqlDataReader dr;
            String szConfigValue = "";
            ds = new DataSet();
            string szPath = "";
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_REQUIRED_DOCUMENTS_GET_FULL_NODE_PATH", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_NODEID", p_szNodeID);
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        szPath = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                }
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
            return szPath;
        }

        #region "chages after paketing -- atul"
        public string Get_PacketId(string sz_company_id)
        {

            sqlCon = new SqlConnection(strsqlCon);
            SqlDataReader dr;

            ds = new DataSet();
            string szPaketId = "";
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("GET_BILL_STATUS_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_CODE", "PKT");
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);

                dr = sqlCmd.ExecuteReader();
                while (dr.Read())
                {
                    szPaketId = dr[0].ToString();
                }


            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return szPaketId;
        }

        public void Set_PacketId(string sz_company_id, string sz_bill_no, string sz_bill_status_id, string sz_user_id)
        {

            sqlCon = new SqlConnection(strsqlCon);
            SqlDataReader dr;

            ds = new DataSet();
            string szPaketId = "";
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_SET_BILL_STATUS_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_no);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
                sqlCmd.Parameters.AddWithValue("@SZ_SZ_BILL_STATUS_ID", sz_bill_status_id);
                sqlCmd.Parameters.AddWithValue("@SZ_UPDATED_USER_ID", sz_user_id);
                sqlCmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                throw;
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


        //Wrote Transaction To Save Bill Packet Request:TUSHAR
        public string GetBillPacketRequest(string P_User_Id, string P_Company_Id, ArrayList objArr)
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlTransaction transaction;
            string PacketId = "";
            transaction = sqlCon.BeginTransaction();
            try
            {

                #region "Save Bill Packet Rquest In MST_PACKET_REQUEST"
                sqlCmd.CommandText = "SP_SAVE_MST_PACKET_REQUEST";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id.ToString());
                sqlCmd.Parameters.AddWithValue("@DT_REQUEST_DATE", DateTime.Now.ToString("MM/dd/yyyy"));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id.ToString());
                sqlCmd.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", P_Company_Id.ToString());
                sqlCmd.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", SqlDbType.VarChar);
                sqlCmd.Parameters["@I_PACKET_REQUEST_ID"].Direction = ParameterDirection.ReturnValue;
                sqlCmd.ExecuteNonQuery();
                PacketId = sqlCmd.Parameters["@I_PACKET_REQUEST_ID"].Value.ToString();
                #endregion

                #region "Save Bill Packet Rquest In TXN_PACKET_REQUEST"           
                Bill_Sys_Bill_Packet_Request _BillPacketRequest;
                foreach (object obj in objArr)
                {
                    sqlCmd = new SqlCommand();
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                    sqlCmd.CommandText = "SP_SAVE_TXN_PACKET_REQUEST";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _BillPacketRequest.SZ_BILL_NUMBER.ToString());
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _BillPacketRequest.SZ_CASE_ID.ToString());
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id.ToString());
                    sqlCmd.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", PacketId.ToString());
                    sqlCmd.ExecuteNonQuery();
                }
                #endregion

                transaction.Commit();


                #region "Save Bill Packet Rquest In TXN_PACKET_ERROR To Call Procedure"
                sqlCmd = new SqlCommand();
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandText = "SP_SAVE_MST_PACKET_ERROR";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", PacketId.ToString());
                sqlCmd.ExecuteNonQuery();
                #endregion
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                transaction.Rollback();
            }
            finally
            {
                sqlCon.Close();
            }
            return PacketId;

        }
        //End

        public string GetImageFullPath(string szImageId)
        {
            string sReturn = "";
            ds = new DataSet();
            try
            {

                sqlCon = new SqlConnection(strsqlCon);
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_PATH_OF_IMAGE_USING_IMAGE_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ImageID", szImageId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sReturn = ds.Tables[0].Rows[0][0].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
            return sReturn;



        }

        //Function To Hit Database After Every 10 Secons
        public DataSet GetBillPacketRequestErrorStatus(string P_Packet_Id)
        {
            ArrayList objarray = new ArrayList();
            DataSet ds = new DataSet();
            try
            {
                sqlCon = new SqlConnection(strsqlCon);
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_MST_PACKET_REQUEST_STATUS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", P_Packet_Id);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                sqlCon.Close();
            }
            return ds;
        }
        //end Of Code

    }


    public class Bill_Sys_RequiredDAO
    {
        private string i_image_id;
        private string i_txn_id;
        private string szCaseId;
        private string szCompanyId;

        public string ImageID
        {
            get
            {
                return this.i_image_id;
            }

            set
            {
                this.i_image_id = value;
            }
        }

        public string TransactionID
        {
            get
            {
                return this.i_txn_id;
            }

            set
            {
                this.i_txn_id = value;
            }
        }

        public string CaseId
        {
            get
            {
                return this.szCaseId;
            }

            set
            {
                this.szCaseId = value;
            }
        }

        public string CompanyId
        {
            get
            {
                return this.szCompanyId;
            }

            set
            {
                this.szCompanyId = value;
            }
        }
    }

    public class REQUIREDDOCUMENT_EO
    {
        private string _szDoucmentID = "";
        public string SZ_DOCUMENT_ID
        {
            get
            {
                return _szDoucmentID;
            }
            set
            {
                _szDoucmentID = value;
            }
        }

        private string _szDocumentName = "";
        public string SZ_DOCUMENT_NAME
        {
            get
            {
                return _szDocumentName;
            }
            set
            {
                _szDocumentName = value;
            }
        }

        private string _szDocumentPath = "";
        public string SZ_DOCUMENT_PATH
        {
            get
            {
                return _szDocumentPath;
            }
            set
            {
                _szDocumentPath = value;
            }
        }

        private string _szErrMsg = "";
        public string SZ_ERROR_MSG
        {
            get
            {
                return _szErrMsg;
            }
            set
            {
                _szErrMsg = value;
            }
        }

        private string _szOpenFilePath = "";
        public string SZ_OPEN_FILE_PATH
        {
            get
            {
                return _szOpenFilePath;
            }
            set
            {
                _szOpenFilePath = value;
            }
        }



    }








}
