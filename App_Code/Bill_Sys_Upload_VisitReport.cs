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
using System.Data;
using Ionic.Zip;
using log4net;
using iTextSharp;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Aquaforest.PDF;
using System.Collections.Generic;
using Hangfire;
/// <summary>
/// Summary description for Bill_Sys_Upload_VisitReport
/// </summary>
///          
public class Bill_Sys_Upload_VisitReport
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    private static ILog log = LogManager.GetLogger("App_Code:Bill_Sys_Upload_VisitReport");

    public Bill_Sys_Upload_VisitReport()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GetNodeIDandPath(ArrayList onjAdd)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            sqlda = new SqlDataAdapter("EXEC SP_UPLOAD_DOCUMENT_FOR_VISIT @SZ_CASE_ID='" + onjAdd[0].ToString() + "', @SZ_COMPANY_ID='" + onjAdd[1].ToString() + "', @SZ_DOCTOR_ID='" + onjAdd[2].ToString() + "', @SZ_REPORT_TYPE='" + onjAdd[3].ToString() + "', @SZ_PROCEDURE_GROUP_ID='" + onjAdd[9].ToString() + "'", conn);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    #region Update Download Link
    public void UpdateDownloadLink(string RequestID, string downloadlink, string CompanyID)
    {
        try
        {

            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "PROC_UpdateDownloadLink";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@RequestID", RequestID);
            comm.Parameters.AddWithValue("@DownloadLink", downloadlink);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    #endregion
    #region Get Data From Hangfire
    public DataSet GetDataFromHangfire(string SZ_COMPANY_ID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("PROC_GetHangfireData", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    #endregion

    #region Get Dataset For Envelop and POM
    public DataSet GetDataForPOMEnv(string CompanyID, string SpecilityID, string BillNumbers)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_NEW1", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            //comm.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", BillStatus);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", SpecilityID);
            //comm.Parameters.AddWithValue("@I_START_INDEX", 1);
            //comm.Parameters.AddWithValue("@I_END_INDEX", 500);
            comm.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            comm.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBERS", BillNumbers);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    #endregion

    #region Get Data For Bills
    public DataSet GetAllBillsByPacketRequestID(int I_PACKET_REQUEST_ID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("PROC_GetAllBillsByPacketRequestID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", I_PACKET_REQUEST_ID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    #endregion

    #region Packet Details
    public DataSet GetAllBillsPacketDetails(int I_PACKET_REQUEST_ID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("STP_SelectPacketDetails", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@PacketRequestID", I_PACKET_REQUEST_ID);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    #endregion
    public String Upload_Report_For_Visit(ArrayList onjAdd)
    {
        try
        {
            DataSet ds1 = GetNodeIDandPath(onjAdd);
            String Msg = SaveFileAndUpdateDocManager(ds1, onjAdd);
            return Msg;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return "Failed";
        }
        finally { conn.Close(); }
    }
    public String SaveFileAndUpdateDocManager(DataSet ds2, ArrayList onjAdd)
    {
        Boolean flag = true;
        try
        {
            if ((ds2.Tables[0].Rows[0][0] == null) || (ds2.Tables[0].Rows[0][0] == null))
            {
                flag = false;
            }
            else
            {
                if ((ds2.Tables[1].Rows[0][0] == "") || (ds2.Tables[1].Rows[0][0] == ""))
                {
                    flag = false;
                }
            }
            if (flag = false)
                return "Failed";
            string Path = ds2.Tables[0].Rows[0]["Path"].ToString().Replace("/", "\\") + onjAdd[4].ToString() + "\\" + ds2.Tables[1].Rows[0][0].ToString().Replace("/", "\\") + "\\";
            string FilePath = onjAdd[4].ToString() + "/" + ds2.Tables[1].Rows[0][0].ToString().Replace("\\", "/") + "/";
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_WS_UPLOAD_REPORT_FOR_VISIT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", onjAdd[5].ToString());
            comm.Parameters.AddWithValue("@SZ_FILE_PATH", FilePath);
            comm.Parameters.AddWithValue("@I_TAG_ID", ds2.Tables[0].Rows[0]["NodeID"].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", onjAdd[7].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ID", onjAdd[6].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_ID", onjAdd[8].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", onjAdd[9].ToString());
            comm.ExecuteNonQuery();
            return Path;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return "Failed";
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public string GetDoctorSpecialty(string DoctorId, string CompanyId)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            sqlda = new SqlDataAdapter("SELECT SZ_PROCEDURE_GROUP_ID FROM TXN_DOCTOR_SPECIALITY WHERE SZ_DOCTOR_ID='" + DoctorId + "' AND SZ_COMPANY_ID='" + CompanyId + "'", conn);
            ds = new DataSet();
            sqlda.Fill(ds);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return "";
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public string GetNodeType(string szCompanyId, string szDocType, string szProcedureGroupId)
    {
        string szReturn = "";
        try
        {

            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_UPLOADED_NODE_TYPE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_TYPE", szDocType);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupId);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return szReturn;
    }

    public string DeleteFile(string szCompanyId, string szImageId, string szFlag)
    {
        string szReturn = "";
        try
        {

            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_HIDE_NODES";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@I_NODEID", szImageId);
            comm.Parameters.AddWithValue("@FLAG", szFlag);
            SqlParameter objOutParameter = new SqlParameter("@sz_output", SqlDbType.VarChar, 500);
            objOutParameter.Direction = ParameterDirection.Output;
            comm.Parameters.Add(objOutParameter);
            comm.ExecuteNonQuery();
            szReturn = objOutParameter.Value.ToString();
            if (szReturn != "")
            {
                szReturn = "YES";
            }
            else
            {
                szReturn = "NO";
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return szReturn;
    }
    public string DeleteFile(string szCompanyId, string szImageId, string szFlag, string CaseId, string msg, string UserID)
    {
        string szReturn = "";
        conn = new SqlConnection(strConn);
        try
        {


            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_HIDE_NODES";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@I_NODEID", szImageId);
            comm.Parameters.AddWithValue("@FLAG", szFlag);
            SqlParameter objOutParameter = new SqlParameter("@sz_output", SqlDbType.VarChar, 500);
            objOutParameter.Direction = ParameterDirection.Output;
            comm.Parameters.Add(objOutParameter);
            comm.ExecuteNonQuery();
            szReturn = objOutParameter.Value.ToString();
            if (szReturn != "")
            {
                szReturn = "YES";

                comm = new SqlCommand("SP_TXN_NOTES", conn);
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                comm.Parameters.AddWithValue("@SZ_CASE_ID", CaseId);
                comm.Parameters.AddWithValue("@SZ_USER_ID", UserID);
                comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", msg);
                comm.Parameters.AddWithValue("@IS_DENIED", "");
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                comm.Parameters.AddWithValue("@FLAG", "ADD");
                comm.ExecuteNonQuery();
            }
            else
            {
                szReturn = "NO";
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return szReturn;
    }

    public string GetBillPacketRequest(string P_User_Id, string P_Company_Id, ArrayList objArr)
    {
        //log.Debug("Start GetBillPacketRequest method.");
        conn = new SqlConnection(strConn);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        string PacketId = "";
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Packet Rquest In MST_PACKET_REQUEST"
            comm.CommandText = "SP_SAVE_MST_PACKET_REQUEST";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id.ToString());
            //log.Debug("User ID : " + P_User_Id.ToString());
            comm.Parameters.AddWithValue("@DT_REQUEST_DATE", DateTime.Now.ToString("MM/dd/yyyy"));
            //log.Debug("Request Date : " + DateTime.Now.ToString("MM/dd/yyyy"));
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id.ToString());
            //log.Debug("Company ID " + P_Company_Id.ToString());
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", P_Company_Id.ToString());
            //log.Debug("Packet Request " + P_Company_Id.ToString());
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", SqlDbType.VarChar);
            comm.Parameters["@I_PACKET_REQUEST_ID"].Direction = ParameterDirection.ReturnValue;
            comm.ExecuteNonQuery();
            PacketId = comm.Parameters["@I_PACKET_REQUEST_ID"].Value.ToString();
            //log.Debug("Packet ID : " + PacketId);

            #endregion

            #region "Save Bill Packet Rquest In TXN_PACKET_REQUEST"
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            foreach (object obj in objArr)
            {
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                comm.CommandText = "SP_SAVE_TXN_PACKET_REQUEST";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", _BillPacketRequest.SZ_BILL_NUMBER.ToString());
                //log.Debug("Bill Number : " + _BillPacketRequest.SZ_BILL_NUMBER.ToString());
                comm.Parameters.AddWithValue("@SZ_CASE_ID", _BillPacketRequest.SZ_CASE_ID.ToString());
                //log.Debug("Case ID : " + _BillPacketRequest.SZ_CASE_ID.ToString());
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id.ToString());
                //log.Debug("Company Id : " + P_Company_Id.ToString());
                comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", PacketId.ToString());
                //log.Debug("packet id : " + PacketId.ToString());
                comm.ExecuteNonQuery();
            }
            #endregion

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            //log.Debug("Error inside catch GetBillPacketRequest method: " + ex.ToString());
        }
        finally
        {
            conn.Close();
        }
        //log.Debug("Return packetID : " + PacketId);
        return PacketId;
        //log.Debug("End GetBillPacketRequest method.");
    }

    public void UpdateBillPacket(string PacketRequestID, int JobID, string SpecialityID)
    {
        //log.Debug("Start GetBillPacketRequest method.");
        conn = new SqlConnection(strConn);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Packet Rquest In MST_PACKET_REQUEST"
            comm.CommandText = "[SP_UPDATE_MST_PACKET_REQUEST]";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", PacketRequestID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", SpecialityID);
            comm.Parameters.AddWithValue("@JOBID", JobID);
            comm.ExecuteNonQuery();
            #endregion
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            //log.Debug("Error inside catch GetBillPacketRequest method: " + ex.ToString());
        }
        finally
        {
            conn.Close();
        }
        //log.Debug("Return packetID : " + PacketId);
        //log.Debug("End GetBillPacketRequest method.");
    }

    public void InsertPacketErrors(string PacketRequestID, int JobID, string Message)
    {
        //log.Debug("Start GetBillPacketRequest method.");
        conn = new SqlConnection(strConn);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {
            #region "Save Bill Packet Rquest In MST_PACKET_REQUEST"
            comm.CommandText = "[STP_InsertPacketDetails]";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", PacketRequestID);
            comm.Parameters.AddWithValue("@MessageDesc", Message.Length > 8000 ? Message.Substring(0, 7999) : Message);
            comm.Parameters.AddWithValue("@JOBID", JobID);
            comm.ExecuteNonQuery();
            #endregion
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
            //log.Debug("Error inside catch GetBillPacketRequest method: " + ex.ToString());
        }
        finally
        {
            conn.Close();
        }
        //log.Debug("Return packetID : " + PacketId);
        //log.Debug("End GetBillPacketRequest method.");
    }
    [AutomaticRetry(Attempts = 0)]
    public string CreatePacket(string szCompnyId, ArrayList objArr, string szSpecilaty, string RequestID)
    {
        string szReturn = "";
        string szBillNo = "";
        try
        {
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            ArrayList FileToZip = new ArrayList();
            foreach (object obj in objArr)
            {
                ArrayList arfiles = new ArrayList();
                ArrayList arMissingMergefiles = new ArrayList();
                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                DataSet ds = new DataSet();
                ds = GetBillPath(_BillPacketRequest.SZ_BILL_NUMBER);
                string StartPath = ApplicationSettings.GetParameterValue("PacketPath");
                string CompanyName = GetCompanyName(szCompnyId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["PATH"].ToString() != "")
                    {
                        string FilePath = ds.Tables[0].Rows[0]["PhysicalBasePath"] + ds.Tables[0].Rows[0]["PATH"].ToString();
                        if (File.Exists(FilePath))
                        {
                            arfiles.Add(FilePath);
                            DataSet dsFiles = GetFilePath(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, szSpecilaty, szCompnyId);
                            if (dsFiles != null)
                            {
                                if (dsFiles.Tables[0].Rows.Count > 0)
                                {
                                    for (int iCout = 0; iCout < dsFiles.Tables[0].Rows.Count; iCout++)
                                    {
                                        string FilePath1 = dsFiles.Tables[0].Rows[iCout][1].ToString() + dsFiles.Tables[0].Rows[iCout][0].ToString();
                                        if (File.Exists(FilePath1))
                                        {
                                            if (IsValidPdf(FilePath1))
                                            {
                                                log.Debug("File Get Read");
                                                arfiles.Add(FilePath1);
                                            }
                                            else
                                            {
                                                InsertPacketErrors(RequestID, 0, "Invalid or corrupt file " + FilePath1 + "");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    InsertPacketErrors(RequestID, 0, "Invalid file path for Bill number " + _BillPacketRequest.SZ_BILL_NUMBER + ".Line no:-363 ");
                    throw new InvalidOperationException("Create packet line 623" + szReturn);
                }
                szBillNo = _BillPacketRequest.SZ_BILL_NUMBER;
                if (arfiles != null)
                {
                    string Des1 = "";
                    if (arfiles.Count > 0)
                    {
                        string cmpname = GetCompanyName(szCompnyId);
                        string basepath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        string basePacketPath = ApplicationSettings.GetParameterValue("PacketPath");
                        int cout = 1;
                        List<string> pdfList = new List<string>();
                        for (int arrCout = 0; arrCout < arfiles.Count; arrCout++)
                        {
                            if (arrCout == 0)
                            {
                                Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileNameCount(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, arrCout.ToString());
                                if (!Directory.Exists(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/"))
                                {
                                    Directory.CreateDirectory(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                }
                                if (!File.Exists(Des1))
                                {
                                    File.Copy(arfiles[arrCout].ToString(), Des1);
                                    PDFValidator pdf = new PDFValidator(Des1);

                                    if (pdf.IsValid)
                                        pdfList.Add(Des1);
                                    else
                                    {
                                        InsertPacketErrors(RequestID, 0, "Invalid or corrupt file " + Des1 + "");
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    string szOldFile = Des1;
                                    Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileNameCount(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, arrCout.ToString());

                                    PDFValidator pdf = new PDFValidator(arfiles[arrCout].ToString());

                                    if (pdf.IsValid)
                                        pdfList.Add(arfiles[arrCout].ToString());
                                    else
                                    {
                                        InsertPacketErrors(RequestID, 0, "Invalid or corrupt file " + Des1 + "");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    InsertPacketErrors(RequestID, 0, "Line no:-676." + _BillPacketRequest.SZ_BILL_NUMBER + " ");
                                    throw new InvalidOperationException("Create packet", ex);
                                }
                            }
                        }
                        PDFToolkit.LicenseKey = "testkey";
                        PDFMerger merger = new PDFMerger();
                        merger.MergePDFs(pdfList, Des1);
                    }
                    if (Des1 != "")
                    {
                        FileToZip.Add(Des1);
                    }
                    else
                    {
                        InsertPacketErrors(RequestID, 0, "Line no:-691." + _BillPacketRequest.SZ_BILL_NUMBER + " ");
                        throw new InvalidOperationException("Create packet added to zip line 728 " + Des1);
                    }
                    if (arMissingMergefiles.Count > 0)
                    {
                        for (int i = 0; i < arMissingMergefiles.Count; i++)
                        {
                            try
                            {
                                FileToZip.Add(arMissingMergefiles[i].ToString());
                            }
                            catch (Exception ex)
                            {
                                InsertPacketErrors(RequestID, 0, "Error to add file in zip for " + _BillPacketRequest.SZ_BILL_NUMBER + ".Line no:-704.");
                                throw new InvalidOperationException("Create packet", ex);
                            }
                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    InsertPacketErrors(RequestID, 0, "No files in array.Line no:-713.");
                    throw new InvalidOperationException("Create packet line 752 " + szReturn);
                }
            }

            if (FileToZip != null)
            {
                string cmpname2 = GetCompanyName(szCompnyId);
                string basepath2 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                string basePacketPath2 = ApplicationSettings.GetParameterValue("PacketPath");
                ZipFile zip = new ZipFile();

                Directory.SetCurrentDirectory(basePacketPath2 + cmpname2);
                for (int iZipCnt = 0; iZipCnt < FileToZip.Count; iZipCnt++)
                {
                    try
                    {
                        string[] str = FileToZip[iZipCnt].ToString().Split('/');
                        if (FileToZip[iZipCnt].ToString().Contains("Not Merged"))
                        {
                            zip.AddFile(str[str.Length - 3] + "/" + str[str.Length - 2] + "/" + str[str.Length - 1]);
                        }
                        else
                        {
                            zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                        }
                    }
                    catch (Exception ex)
                    {
                        InsertPacketErrors(RequestID, 0, "Line no:-742.");
                        throw new InvalidOperationException("Create packet", ex);
                    }
                }
                string cmpname1 = GetCompanyName(szCompnyId);
                string basepath1 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                string basePacketPath1 = ApplicationSettings.GetParameterValue("PacketPath");
                Directory.SetCurrentDirectory(basepath1 + cmpname1);
                DateTime currentDate = DateTime.Now;
                string filename = new Random().Next(1000, 9999).ToString() + currentDate.ToString("yyyyMMddHHmmssfff") + ".zip";
                Directory.SetCurrentDirectory(basePacketPath1 + cmpname1 + "/");
                try
                {
                    zip.Save(filename);
                    log.Debug("Zip saved.");
                }
                catch (Exception savefile)
                {
                    InsertPacketErrors(RequestID, 0, "Line no:-760.Save zip file exception");
                    throw new InvalidOperationException("Create packet", savefile);
                }
                szReturn = ApplicationSettings.GetParameterValue("PACKET_DOC_URL") + cmpname1 + "/" + filename;
                UpdateDownloadLink(RequestID, szReturn, szCompnyId);
            }
            else
            {
                InsertPacketErrors(RequestID, 0, "Line no:-768 " + szReturn + "");
                throw new InvalidOperationException("Create packet ");
            }
        }
        catch (Exception ex)
        {
            InsertPacketErrors(RequestID, 0, "Line no:-774");
            throw new InvalidOperationException("Create packet", ex);
        }

        if (szReturn.Contains("ERROR"))
        {
            InsertPacketErrors(RequestID, 0, "Line no:-780");
            throw new InvalidOperationException("Create packet line no-780");
        }
        else
        {
            BillStatusUpdateError(RequestID, "ERROR");
        }
        return szReturn;
    }

    public string CreatePacket(string szCompnyId, ArrayList objArr, string szSpecilaty)
    {
        string szReturn = "";
        string szBillNo = "";
        try
        {
            ////log.Debug("Inside CreatePacket method.");
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            ArrayList FileToZip = new ArrayList();
            foreach (object obj in objArr)
            {
                ArrayList arfiles = new ArrayList();
                ArrayList arMissingMergefiles = new ArrayList();
                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                DataSet ds = new DataSet();
                ////log.Debug("Give call to GetBillPath method. Bill No.: " + _BillPacketRequest.SZ_BILL_NUMBER);
                ds = GetBillPath(_BillPacketRequest.SZ_BILL_NUMBER);
                string StartPath = ApplicationSettings.GetParameterValue("PacketPath");
                string CompanyName = GetCompanyName(szCompnyId);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ////log.Debug("Bill Path : " + ds.Tables[0].Rows[0]["PATH"].ToString());
                    if (ds.Tables[0].Rows[0]["PATH"].ToString() != "")
                    {
                        string FilePath = ds.Tables[0].Rows[0]["PhysicalBasePath"] + ds.Tables[0].Rows[0]["PATH"].ToString();
                        ////log.Debug("FilePath : " + FilePath);
                        if (File.Exists(FilePath))
                        {
                            //log.Debug("Inside if file exist(FilePath)");
                            arfiles.Add(FilePath);
                            ////log.Debug("Give call to GetFilePath method.");
                            DataSet dsFiles = GetFilePath(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, szSpecilaty, szCompnyId);
                            if (dsFiles != null)
                            {
                                ////log.Debug("Inside if dataset of GetFilePath is not null.");
                                if (dsFiles.Tables[0].Rows.Count > 0)
                                {
                                    ////log.Debug("Inside GetFilePath dataset.");
                                    for (int iCout = 0; iCout < dsFiles.Tables[0].Rows.Count; iCout++)
                                    {
                                        //string FilePath1 = getPhysicalPath() + dsFiles.Tables[0].Rows[iCout][0].ToString();
                                        string FilePath1 = dsFiles.Tables[0].Rows[iCout][1].ToString() + dsFiles.Tables[0].Rows[iCout][0].ToString();
                                        ////log.Debug("File Path : " + FilePath1);
                                        if (File.Exists(FilePath1))
                                        {
                                            ////log.Debug("Add file to arrayList arfiles File Name : " + FilePath1);
                                            if (IsValidPdf(FilePath1))
                                            {
                                                ////log.Debug("File Get Read");
                                                arfiles.Add(FilePath1);

                                                ////log.Debug("File Copy successfull. In Arryfile");
                                            }
                                            else
                                            {
                                                string Copy = StartPath + CompanyName + "/" + _BillPacketRequest.SZ_CASE_ID + "/Not Merged/" + getBillFileNameCount(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, "M_" + arMissingMergefiles.Count.ToString());
                                                ////log.Debug("File not Read");

                                                ////log.Debug("Copy to " + Copy);

                                                if (!Directory.Exists(StartPath + CompanyName + "/" + _BillPacketRequest.SZ_CASE_ID + "/Not Merged/"))
                                                {
                                                    ////log.Debug(StartPath + CompanyName + "/" + _BillPacketRequest.SZ_CASE_ID + "/Not Merged/   Directory not exists" );  

                                                    Directory.CreateDirectory(StartPath + CompanyName + "/" + _BillPacketRequest.SZ_CASE_ID + "/Not Merged/");
                                                    ////log.Debug(StartPath + CompanyName + "/" + _BillPacketRequest.SZ_CASE_ID + "/   Directory created");
                                                }
                                                if (!File.Exists(Copy))
                                                {
                                                    try
                                                    {
                                                        File.Copy(FilePath1, Copy);
                                                        arMissingMergefiles.Add(Copy);
                                                        ////log.Debug("File Copy successfull. In Missing");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ////log.Debug("erro to add file " + ex.ToString());
                                                    }

                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    ////log.Debug("Return ERROR : " + szReturn);
                }
                szBillNo = _BillPacketRequest.SZ_BILL_NUMBER;
                //log.Debug("Arralist count : " + arfiles.Count);
                if (arfiles != null)
                {
                    string Des1 = "";

                    ////log.Debug("Inside (arfiles != null).");
                    if (arfiles.Count > 0)
                    {
                        ////log.Debug("Inside (arfiles.Count > 0). ");
                        string cmpname = GetCompanyName(szCompnyId);
                        ////log.Debug("Company name : " + cmpname);
                        string basepath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        ////log.Debug("Base Path : " + basepath);
                        string basePacketPath = ApplicationSettings.GetParameterValue("PacketPath");
                        ////log.Debug("Base Packet path : " + basePacketPath);
                        int cout = 1;

                        for (int arrCout = 0; arrCout < arfiles.Count; arrCout++)
                        {
                            ////log.Debug("Inside if array List count is greater than 0.");
                            if (arrCout == 0)
                            {
                                ////log.Debug("arrCout==0 : ");
                                //  Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileNameCount(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, arrCout.ToString());
                                ////log.Debug("File copy to  : " + Des1);
                                if (!Directory.Exists(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/"))
                                {
                                    ////log.Debug("Directory not exist Directory path :" + basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    Directory.CreateDirectory(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    ////log.Debug("Create directory successful.");
                                }
                                if (!File.Exists(Des1))
                                {
                                    ////log.Debug("Inside if file not exist.");
                                    ////log.Debug("file " + arfiles[arrCout].ToString());
                                    ////log.Debug("Copy to  " + Des1);
                                    File.Copy(arfiles[arrCout].ToString(), Des1);
                                    ////log.Debug("File Copy successfull.");
                                }
                            }
                            else
                            {
                                ////log.Debug("arrCout== : "+ arrCout.ToString());
                                ////log.Debug("Inside if arrayList(arfiles) is empty");
                                try
                                {
                                    string szOldFile = Des1;
                                    // Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                    Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileNameCount(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, arrCout.ToString());
                                    ////log.Debug("Give Call to MergePDF.MergePDFFiles method ");
                                    ////log.Debug("file " + szOldFile);
                                    ////log.Debug("and file " + arfiles[arrCout].ToString());
                                    ////log.Debug("Merge to  " + Des1);
                                    MergePDF.MergePDFFiles(szOldFile, arfiles[arrCout].ToString(), Des1);
                                    ////log.Debug("MergePDF.MergePDFFiles method successful."); 
                                }
                                catch (Exception ex)
                                {
                                    ////log.Debug("Inside Catch :" +ex.ToString());
                                }
                            }

                        }
                    }
                    if (Des1 != "")
                    {
                        ////log.Debug("File " + Des1+"added to zip");
                        FileToZip.Add(Des1);
                    }
                    if (arMissingMergefiles.Count > 0)
                    {
                        ////log.Debug("Adding file whch are not in merge");
                        for (int i = 0; i < arMissingMergefiles.Count; i++)
                        {
                            try
                            {
                                ////log.Debug("Befor Add file" + arMissingMergefiles[i].ToString());
                                FileToZip.Add(arMissingMergefiles[i].ToString());
                                ////log.Debug("Added file" + arMissingMergefiles[i].ToString());
                            }
                            catch (Exception ex)
                            {
                                ////log.Debug("Error to add file in zip   " + ex.ToString());
                            }
                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    ////log.Debug("Return ERR :" + szReturn);
                }
            }

            if (FileToZip != null)
            {
                ////log.Debug("Inside (FileToZip != null)");
                string cmpname2 = GetCompanyName(szCompnyId);
                ////log.Debug("Company name 2 : " + cmpname2);
                string basepath2 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                ////log.Debug("Base Path 2 :" + basepath2);
                string basePacketPath2 = ApplicationSettings.GetParameterValue("PacketPath");
                ////log.Debug("Base Packet path 2 :" + basePacketPath2);
                ZipFile zip = new ZipFile();

                Directory.SetCurrentDirectory(basePacketPath2 + cmpname2);
                for (int iZipCnt = 0; iZipCnt < FileToZip.Count; iZipCnt++)
                {
                    try
                    {
                        string[] str = FileToZip[iZipCnt].ToString().Split('/');
                        if (FileToZip[iZipCnt].ToString().Contains("Not Merged"))
                        {
                            zip.AddFile(str[str.Length - 3] + "/" + str[str.Length - 2] + "/" + str[str.Length - 1]);
                        }
                        else
                        {

                            //zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                            zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                        }

                        //zip.AddFile(str[str.Length-2]+"/"+str[str.Length-1]);
                    }
                    catch (Exception ex)
                    {
                        ////log.Debug("Inside Catch exception :" + ex.ToString());
                    }
                }
                string cmpname1 = GetCompanyName(szCompnyId);
                ////log.Debug("Company Name1 : " + cmpname1);
                string basepath1 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                ////log.Debug("Base Path1 :" + basepath1);
                string basePacketPath1 = ApplicationSettings.GetParameterValue("PacketPath");
                ////log.Debug("Base Packet PAth1 : " + basePacketPath1);
                Directory.SetCurrentDirectory(basepath1 + cmpname1);
                DateTime currentDate = DateTime.Now;
                string filename = new Random().Next(1000, 9999).ToString() + currentDate.ToString("yyyyMMddHHmmssfff") + ".zip";
                ////log.Debug("File Name :" + filename);
                Directory.SetCurrentDirectory(basePacketPath1 + cmpname1 + "/");
                ////log.Debug("After Directory.SetCurrentDirectory");
                try
                {
                    zip.Save(filename);
                    ////log.Debug("Zip saved.");
                }
                catch (Exception savefile)
                {
                    ////log.Debug("Save zip file exception : " + savefile.ToString());
                }
                szReturn = cmpname1 + "/" + filename;
                ////log.Debug("Return full path : " + szReturn);
            }
            else
            {
                szReturn = "ERROR," + szBillNo;
                return szReturn;
                ////log.Debug("Failed : " + szReturn);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturn;
    }

    public string CreateBillPacket(string szCompnyId, ArrayList objArr)
    {
        string szReturn = "";
        string szBillNo = "";
        try
        {
            ////log.Debug("Inside CreatePacket method.");
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            ArrayList FileToZip = new ArrayList();
            foreach (object obj in objArr)
            {
                ArrayList arfiles = new ArrayList();

                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                DataSet ds = new DataSet();
                ////log.Debug("Give call to GetBillPath method. Bill No.: " + _BillPacketRequest.SZ_BILL_NUMBER);
                ds = GetBillPath(_BillPacketRequest.SZ_BILL_NUMBER);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //log.Debug("Bill Path : " + ds.Tables[0].Rows[0]["PATH"].ToString());
                    if (ds.Tables[0].Rows[0]["PATH"].ToString() != "")
                    {
                        string FilePath = ds.Tables[0].Rows[0]["PhysicalBasePath"] + ds.Tables[0].Rows[0]["PATH"].ToString();
                        //log.Debug("FilePath : " + FilePath);
                        if (File.Exists(FilePath))
                        {
                            //log.Debug("Inside if file exist(FilePath)");
                            arfiles.Add(FilePath);

                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    //log.Debug("Return ERROR : " + szReturn);
                }
                szBillNo = _BillPacketRequest.SZ_BILL_NUMBER;
                //log.Debug("Arralist count : " + arfiles.Count);
                if (arfiles != null)
                {
                    string Des1 = "";

                    //log.Debug("Inside (arfiles != null).");
                    if (arfiles.Count > 0)
                    {
                        //log.Debug("Inside (arfiles.Count > 0). ");
                        string cmpname = GetCompanyName(szCompnyId);
                        //log.Debug("Company name : " + cmpname);
                        string basepath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        //log.Debug("Base Path : " + basepath);
                        string basePacketPath = ApplicationSettings.GetParameterValue("PacketPath");
                        //log.Debug("Base Packet path : " + basePacketPath);
                        int cout = 1;
                        List<string> pdfList = new List<string>();
                        for (int arrCout = 0; arrCout < arfiles.Count; arrCout++)
                        {
                            //log.Debug("Inside if array List count is greater than 0.");
                            if (arrCout == 0)
                            {
                                //  Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                //log.Debug("Destination file path Des1 : " + Des1);
                                if (!Directory.Exists(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/"))
                                {
                                    //log.Debug("Directory not exist Directory path :" + basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    Directory.CreateDirectory(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    //log.Debug("Create directory successful.");
                                }
                                if (!File.Exists(Des1))
                                {
                                    //log.Debug("Inside if file not exist.");
                                    File.Copy(arfiles[arrCout].ToString(), Des1);
                                    pdfList.Add(arfiles[arrCout].ToString());
                                    //log.Debug("File Copy successfull.");
                                }
                            }
                            else
                            {
                                //log.Debug("Inside if arrayList(arfiles) is empty");
                                try
                                {
                                    string szOldFile = Des1;
                                    // Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                    Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                    pdfList.Add(arfiles[arrCout].ToString());
                                    //log.Debug("Give Call to MergePDF.MergePDFFiles method ");
                                    //MergePDF.MergePDFFiles(szOldFile, arfiles[arrCout].ToString(), Des1);
                                    //log.Debug("MergePDF.MergePDFFiles method successful.");
                                }
                                catch (Exception ex)
                                {
                                    //log.Debug("Inside Catch :" + ex.ToString());
                                }
                            }

                        }


                        PDFToolkit.LicenseKey = ConfigurationManager.AppSettings["PDFLicense"].ToString();
                        PDFMerger merger = new PDFMerger();
                        merger.MergePDFs(pdfList, Des1);
                    }
                    if (Des1 != "")
                    {
                        FileToZip.Add(Des1);
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    //log.Debug("Return ERR :" + szReturn);
                }
            }

            if (FileToZip != null)
            {
                //log.Debug("Inside (FileToZip != null)");
                string cmpname2 = GetCompanyName(szCompnyId);
                //log.Debug("Company name 2 : " + cmpname2);
                string basepath2 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path 2 :" + basepath2);
                string basePacketPath2 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet path 2 :" + basePacketPath2);
                ZipFile zip = new ZipFile();

                Directory.SetCurrentDirectory(basePacketPath2 + cmpname2);
                for (int iZipCnt = 0; iZipCnt < FileToZip.Count; iZipCnt++)
                {

                    try
                    {
                        string[] str = FileToZip[iZipCnt].ToString().Split('/');
                        zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                    }
                    catch (Exception ex)
                    {
                        //log.Debug("Inside Catch exception :" + ex.ToString());
                        throw;
                    }
                }
                string cmpname1 = GetCompanyName(szCompnyId);
                //log.Debug("Company Name1 : " + cmpname1);
                string basepath1 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path1 :" + basepath1);
                string basePacketPath1 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet PAth1 : " + basePacketPath1);
                Directory.SetCurrentDirectory(basepath1 + cmpname1);
                DateTime currentDate = DateTime.Now;
                string filename = currentDate.ToString("yyyyMMddHHmmssms") + ".zip";
                //log.Debug("File Name :" + filename);
                Directory.SetCurrentDirectory(basePacketPath1 + cmpname1 + "/");
                try
                {
                    zip.Save(filename);
                    //log.Debug("Zip saved.");
                }
                catch (Exception savefile)
                {
                    //log.Debug("Save zip file exception : " + savefile.ToString());
                }
                szReturn = cmpname1 + "/" + filename;
                //log.Debug("Return full path : " + szReturn);
            }
            else
            {
                szReturn = "ERROR," + szBillNo;
                return szReturn;
                //log.Debug("Failed : " + szReturn);
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturn;
    }
    public string CreateBillPacketDocument(string szCompnyId, ArrayList objArr)
    {
        string szReturn = "";
        string szBillNo = "";
        try
        {
            //log.Debug("Inside CreatePacket method.");
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            ArrayList FileToZip = new ArrayList();
            foreach (object obj in objArr)
            {
                ArrayList arfiles = new ArrayList();

                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;

                DataSet dsFiles = GetFilePath(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, _BillPacketRequest.SZ_SPECIALTY, szCompnyId);
                if (dsFiles != null)
                {
                    //log.Debug("Inside if dataset of GetFilePath is not null.");
                    if (dsFiles.Tables[0].Rows.Count > 0)
                    {
                        //log.Debug("Inside GetFilePath dataset.");
                        for (int iCout = 0; iCout < dsFiles.Tables[0].Rows.Count; iCout++)
                        {
                            string FilePath1 = dsFiles.Tables[0].Rows[iCout][1].ToString() + dsFiles.Tables[0].Rows[iCout][0].ToString();
                            //log.Debug("File Path : " + FilePath1);
                            if (File.Exists(FilePath1))
                            {
                                //log.Debug("Add file to arrayList arfiles File Name : " + FilePath1);
                                arfiles.Add(FilePath1);
                            }
                        }
                    }
                }
                szBillNo = _BillPacketRequest.SZ_BILL_NUMBER;
                //log.Debug("Arralist count : " + arfiles.Count);
                if (arfiles != null)
                {
                    string Des1 = "";

                    //log.Debug("Inside (arfiles != null).");
                    if (arfiles.Count > 0)
                    {
                        //log.Debug("Inside (arfiles.Count > 0). ");
                        string cmpname = GetCompanyName(szCompnyId);
                        //log.Debug("Company name : " + cmpname);
                        string basepath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        //log.Debug("Base Path : " + basepath);
                        string basePacketPath = ApplicationSettings.GetParameterValue("PacketPath");
                        //log.Debug("Base Packet path : " + basePacketPath);
                        int cout = 1;

                        for (int arrCout = 0; arrCout < arfiles.Count; arrCout++)
                        {
                            //log.Debug("Inside if array List count is greater than 0.");
                            if (arrCout == 0)
                            {
                                //  Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                //log.Debug("Destination file path Des1 : " + Des1);
                                if (!Directory.Exists(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/"))
                                {
                                    //log.Debug("Directory not exist Directory path :" + basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    Directory.CreateDirectory(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    //log.Debug("Create directory successful.");
                                }
                                if (!File.Exists(Des1))
                                {
                                    //log.Debug("Inside if file not exist.");
                                    File.Copy(arfiles[arrCout].ToString(), Des1);
                                    //log.Debug("File Copy successfull.");
                                }


                            }
                            else
                            {
                                //log.Debug("Inside if arrayList(arfiles) is empty");
                                try
                                {
                                    string szOldFile = Des1;
                                    // Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                    Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                    //log.Debug("Give Call to MergePDF.MergePDFFiles method ");
                                    MergePDF.MergePDFFiles(szOldFile, arfiles[arrCout].ToString(), Des1);
                                    //log.Debug("MergePDF.MergePDFFiles method successful.");
                                }
                                catch (Exception ex)
                                {
                                    //log.Debug("Inside Catch :" + ex.ToString());
                                }
                            }

                        }
                    }
                    if (Des1 != "")
                    {
                        FileToZip.Add(Des1);
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    //log.Debug("Return ERR :" + szReturn);
                }
            }

            if (FileToZip != null)
            {
                //log.Debug("Inside (FileToZip != null)");
                string cmpname2 = GetCompanyName(szCompnyId);
                //log.Debug("Company name 2 : " + cmpname2);
                string basepath2 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path 2 :" + basepath2);
                string basePacketPath2 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet path 2 :" + basePacketPath2);
                ZipFile zip = new ZipFile();

                Directory.SetCurrentDirectory(basePacketPath2 + cmpname2);
                for (int iZipCnt = 0; iZipCnt < FileToZip.Count; iZipCnt++)
                {

                    try
                    {
                        string[] str = FileToZip[iZipCnt].ToString().Split('/');
                        zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                    }
                    catch (Exception ex)
                    {
                        //log.Debug("Inside Catch exception :" + ex.ToString());
                        throw;
                    }
                }
                string cmpname1 = GetCompanyName(szCompnyId);
                //log.Debug("Company Name1 : " + cmpname1);
                string basepath1 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path1 :" + basepath1);
                string basePacketPath1 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet PAth1 : " + basePacketPath1);
                Directory.SetCurrentDirectory(basepath1 + cmpname1);
                DateTime currentDate = DateTime.Now;
                string filename = new Random().Next(1000, 9999).ToString() + currentDate.ToString("yyyyMMddHHmmssms") + ".zip";
                //log.Debug("File Name :" + filename);
                Directory.SetCurrentDirectory(basePacketPath1 + cmpname1 + "/");
                try
                {
                    zip.Save(filename);
                    //log.Debug("Zip saved.");
                }
                catch (Exception savefile)
                {
                    //log.Debug("Save zip file exception : " + savefile.ToString());
                }
                szReturn = cmpname1 + "/" + filename;
                //log.Debug("Return full path : " + szReturn);
            }
            else
            {
                szReturn = "ERROR," + szBillNo;
                return szReturn;
                //log.Debug("Failed : " + szReturn);
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturn;
    }
    public string CreateBothPacket(string szCompnyId, ArrayList objArr)
    {
        string szReturn = "";
        string szBillNo = "";
        try
        {
            //log.Debug("Inside CreatePacket method.");
            Bill_Sys_Bill_Packet_Request _BillPacketRequest;
            ArrayList FileToZip = new ArrayList();
            foreach (object obj in objArr)
            {

                ArrayList arfiles = new ArrayList();

                _BillPacketRequest = (Bill_Sys_Bill_Packet_Request)obj;
                DataSet ds = new DataSet();
                //log.Debug("Give call to GetBillPath method. Bill No.: " + _BillPacketRequest.SZ_BILL_NUMBER);
                ds = GetBillPath(_BillPacketRequest.SZ_BILL_NUMBER);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //log.Debug("Bill Path : " + ds.Tables[0].Rows[0]["PATH"].ToString());
                    if (ds.Tables[0].Rows[0]["PATH"].ToString() != "")
                    {
                        string FilePath = ds.Tables[0].Rows[0]["PhysicalBasePath"] + ds.Tables[0].Rows[0]["PATH"].ToString();
                        //log.Debug("FilePath : " + FilePath);
                        if (File.Exists(FilePath))
                        {
                            //log.Debug("Inside if file exist(FilePath)");
                            if (IsValidPdf(FilePath))
                            {
                                arfiles.Add(FilePath);
                            }
                            //log.Debug("Give call to GetFilePath method.");
                            DataSet dsFiles = GetFilePath(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID, _BillPacketRequest.SZ_SPECIALTY, szCompnyId);
                            if (dsFiles != null)
                            {
                                //log.Debug("Inside if dataset of GetFilePath is not null.");
                                if (dsFiles.Tables[0].Rows.Count > 0)
                                {
                                    //log.Debug("Inside GetFilePath dataset.");
                                    for (int iCout = 0; iCout < dsFiles.Tables[0].Rows.Count; iCout++)
                                    {
                                        string FilePath1 = dsFiles.Tables[0].Rows[iCout][1].ToString() + dsFiles.Tables[0].Rows[iCout][0].ToString();
                                        //log.Debug("File Path : " + FilePath1);
                                        if (File.Exists(FilePath1))
                                        {
                                            //log.Debug("Add file to arrayList arfiles File Name : " + FilePath1);
                                            if (IsValidPdf(FilePath))
                                            {
                                                arfiles.Add(FilePath1);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    //log.Debug("Return ERROR : " + szReturn);
                }
                szBillNo = _BillPacketRequest.SZ_BILL_NUMBER;
                //log.Debug("Arralist count : " + arfiles.Count);
                if (arfiles != null)
                {
                    string Des1 = "";

                    //log.Debug("Inside (arfiles != null).");
                    if (arfiles.Count > 0)
                    {
                        //log.Debug("Inside (arfiles.Count > 0). ");
                        string cmpname = GetCompanyName(szCompnyId);
                        //log.Debug("Company name : " + cmpname);;
                        string basepath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        //log.Debug("Base Path : " + basepath);
                        string basePacketPath = ApplicationSettings.GetParameterValue("PacketPath");
                        //log.Debug("Base Packet path : " + basePacketPath);
                        int cout = 1;
                        List<string> pdfList = new List<string>();
                        for (int arrCout = 0; arrCout < arfiles.Count; arrCout++)
                        {
                            //log.Debug("Inside if array List count is greater than 0.");
                            if (arrCout == 0)
                            {
                                //  Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                //log.Debug("Destination file path Des1 : " + Des1);
                                if (!Directory.Exists(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/"))
                                {
                                    //log.Debug("Directory not exist Directory path :" + basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    Directory.CreateDirectory(basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/");
                                    //log.Debug("Create directory successful.");
                                }
                                if (!File.Exists(Des1))
                                {

                                    File.Copy(arfiles[arrCout].ToString(), Des1);
                                    PDFValidator pdf = new PDFValidator(Des1);

                                    if (pdf.IsValid)
                                        pdfList.Add(Des1);//log.Debug("Inside if file not exist.");
                                    //log.Debug("File Copy successfull.");
                                }


                            }
                            else
                            {
                                //log.Debug("Inside if arrayList(arfiles) is empty");
                                try
                                {
                                    string szOldFile = Des1;
                                    // Des1 = basepath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/Packet Document/" + getFileName("MRG", _BillPacketRequest.SZ_CASE_ID);
                                    Des1 = basePacketPath + cmpname + "/" + _BillPacketRequest.SZ_CASE_ID + "/" + getBillFileName(_BillPacketRequest.SZ_BILL_NUMBER, _BillPacketRequest.SZ_CASE_ID);
                                    PDFValidator pdf = new PDFValidator(arfiles[arrCout].ToString());

                                    if (pdf.IsValid)
                                        pdfList.Add(arfiles[arrCout].ToString());
                                    //log.Debug("Give Call to MergePDF.MergePDFFiles method ");
                                    MergePDF.MergePDFFiles(szOldFile, arfiles[arrCout].ToString(), Des1);
                                    //log.Debug("MergePDF.MergePDFFiles method successful.");
                                }
                                catch (Exception ex)
                                {
                                    //log.Debug("Inside Catch :" + ex.ToString());
                                }
                            }

                        }
                        PDFToolkit.LicenseKey = "testkey";
                        PDFMerger merger = new PDFMerger();
                        merger.MergePDFs(pdfList, Des1);
                    }
                    if (Des1 != "")
                    {
                        FileToZip.Add(Des1);
                    }
                }
                else
                {
                    szReturn = "ERROR," + _BillPacketRequest.SZ_BILL_NUMBER;
                    return szReturn;
                    //log.Debug("Return ERR :" + szReturn);
                }
            }

            if (FileToZip != null)
            {
                //log.Debug("Inside (FileToZip != null)");
                string cmpname2 = GetCompanyName(szCompnyId);
                //log.Debug("Company name 2 : " + cmpname2);
                string basepath2 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path 2 :" + basepath2);
                string basePacketPath2 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet path 2 :" + basePacketPath2);
                ZipFile zip = new ZipFile();

                Directory.SetCurrentDirectory(basePacketPath2 + cmpname2);
                for (int iZipCnt = 0; iZipCnt < FileToZip.Count; iZipCnt++)
                {

                    try
                    {
                        string[] str = FileToZip[iZipCnt].ToString().Split('/');
                        zip.AddFile(str[str.Length - 2] + "/" + str[str.Length - 1]);
                    }
                    catch (Exception ex)
                    {
                        //log.Debug("Inside Catch exception :" + ex.ToString());
                        throw;
                    }
                }
                string cmpname1 = GetCompanyName(szCompnyId);
                //log.Debug("Company Name1 : " + cmpname1);
                string basepath1 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                //log.Debug("Base Path1 :" + basepath1);
                string basePacketPath1 = ApplicationSettings.GetParameterValue("PacketPath");
                //log.Debug("Base Packet PAth1 : " + basePacketPath1);
                Directory.SetCurrentDirectory(basepath1 + cmpname1);
                DateTime currentDate = DateTime.Now;
                string filename = new Random().Next(1000, 9999).ToString() + currentDate.ToString("yyyyMMddHHmmssfff") + ".zip";
                //log.Debug("File Name :" + filename);
                Directory.SetCurrentDirectory(basePacketPath1 + cmpname1 + "/");
                try
                {
                    zip.Save(filename);
                    //log.Debug("Zip saved.");
                }
                catch (Exception savefile)
                {
                    //log.Debug("Save zip file exception : " + savefile.ToString());
                }
                szReturn = cmpname1 + "/" + filename;
                //log.Debug("Return full path : " + szReturn);
            }
            else
            {
                szReturn = "ERROR," + szBillNo;
                return szReturn;
                //log.Debug("Failed : " + szReturn);
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturn;
    }

    public DataSet GetBillPath(string szBillNO)
    {
        //log.Debug("Inside GetBillPath method");
        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();

        try
        {
            comm = new SqlCommand("SP_GET_BILL_PATH", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            //log.Debug("GetBillPath method Bill Number : " + szBillNO);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNO);

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
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
        //log.Debug("End of GetBillPath method.");
    }
    public string getPhysicalPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlCon);
            cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
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
        return szParamValue;
    }

    #region Get Bill Status
    public DataSet GetCurrentBillStatus(string BillNumber)
    {
        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        try
        {
            comm = new SqlCommand("PROC_GetCurrentStatus", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNumber);
            SqlDataAdapter da = new SqlDataAdapter(comm);
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
    #endregion

    public DataSet GetFilePath(string szBillNO, string szCaseID, string szSpecialaty, string szCompnyID)
    {
        //log.Debug("Inside GetFilePath method.");
        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_GET_PACKET_DOC", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNO);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompnyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpecialaty);
            SqlDataAdapter da = new SqlDataAdapter(comm);
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
        //log.Debug("End of GetFilePath method.");
    }



    private string getFileName(string startName, string caseid)
    {
        String szStartName = "";
        szStartName = startName;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = "MRG" + "_" + GetPatientName(caseid) + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
        return szFileName;
    }

    private string getBillFileName(string billNumber, string caseid)
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = billNumber + "_" + GetPatientName(caseid) + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
        return szFileName;
    }

    private string getBillFileNameCount(string billNumber, string caseid, string count)
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = billNumber + "_" + GetPatientName(caseid) + "_" + currentDate.ToString("yyyyMMddHHmmssms") + "_" + count + ".pdf";
        return szFileName;
    }


    public string GetPatientName(string szCaseid)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {

            SqlCommand sqlCmd = new SqlCommand("SP_GET_PATIENT_NAME_USING_CASEID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseid);
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
            return dt.Rows[0][0].ToString();
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
        return dt.Rows[0][0].ToString();
    }

    public string GetCompanyName(string szCompanyId)
    {

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        string szCompanyName = "";
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("GET_COMPANY_NAME", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            szCompanyName = ds.Tables[0].Rows[0][0].ToString();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return szCompanyName;
    }

    public void BillStatusUpdateError(string sz_req_id, string error)
    {
        conn = new SqlConnection(strConn);
        //log.Debug("Inside BillStatusUpdateError method.");
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UPDATE_PACKET_NEW", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", sz_req_id);
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE_PATH", "");
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE", "");
            comm.Parameters.AddWithValue("@SZ_ERROR_MESSAGE", error);
            comm.Parameters.AddWithValue("@FLAG", "DONE");
            comm.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //log.Debug("End of BillStatusUpdateError method.");
    }
    public void BillStatusUpdateErrorForPacket(string sz_req_id, string error)
    {
        conn = new SqlConnection(strConn);
        //log.Debug("Inside BillStatusUpdateError method.");
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UPDATE_PACKET_NEW1", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", sz_req_id);
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE_PATH", "");
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE", "");
            comm.Parameters.AddWithValue("@SZ_ERROR_MESSAGE", error);
            comm.Parameters.AddWithValue("@FLAG", "DONE");
            comm.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //log.Debug("End of BillStatusUpdateError method.");
    }
    public void BillStatusUpdatePNG(string sz_req_id, string error)
    {
        conn = new SqlConnection(strConn);
        //log.Debug("Inside BillStatusUpdateError method.");
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UPDATE_PACKET_NEW", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_PACKET_REQUEST_ID", sz_req_id);
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE_PATH", "");
            comm.Parameters.AddWithValue("@SZ_PACKET_FILE", "");
            comm.Parameters.AddWithValue("@SZ_ERROR_MESSAGE", error);
            comm.Parameters.AddWithValue("@FLAG", "PNG");
            comm.ExecuteNonQuery();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //log.Debug("End of BillStatusUpdateError method.");
    }

    public string getPacketPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'Packet Path'", sqlCon);
            cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
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
        return szParamValue;
    }

    public string GetFiledDocType(string FileName, string szProcId)
    {

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(constring);
        string szDoctypeId = "";
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_GET_SZ_DOCUMENT_TYPE_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szProcId);
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", FileName);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {



                szDoctypeId = ds.Tables[0].Rows[0][0].ToString();
            }


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return szDoctypeId;
    }

    public string GetFiledCaseDocType(string FileName, string szProcId)
    {

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(constring);
        string szDoctypeId = "";
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_GET_CASE_SZ_DOCUMENT_TYPE_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szProcId);
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", FileName);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {



                szDoctypeId = ds.Tables[0].Rows[0][0].ToString();
            }


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return szDoctypeId;
    }


    public string UploadFileForRf(ArrayList arr)
    {

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(constring);
        string sz_return = "";
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_UPLOAD_DOCUMENT_FOR_VISIT_REFERRAL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", arr[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[2].ToString());

            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {



                sz_return = ds.Tables[0].Rows[0][0].ToString();
            }


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return sz_return;
    }

    public int SaveFileINDB(ArrayList arr)
    {

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(constring);
        int i_return = 0;
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_UPLOAD_REPORT_FOR_VISIT_REFERRAL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", arr[0].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[1].ToString());
            comm.Parameters.AddWithValue("@SZ_FILE_NAME", arr[2].ToString());
            comm.Parameters.AddWithValue("@SZ_FILE_PATH", arr[3].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", arr[4].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ID", arr[5].ToString());
            comm.Parameters.AddWithValue("@SZ_EVENT_ID", arr[6].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[7].ToString());
            i_return = comm.ExecuteNonQuery();



        }
        catch (Exception ex)
        {
            i_return = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return i_return;
    }

    public string GetDoctorSpecialtyCode(string DoctorId, string CompanyId)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            sqlda = new SqlDataAdapter("SELECT SZ_PROCEDURE_GROUP_CODE FROM mst_procedure_group WHERE SZ_PROCEDURE_GROUP_ID=(SELECT SZ_PROCEDURE_GROUP_ID FROM TXN_DOCTOR_SPECIALITY WHERE SZ_DOCTOR_ID='" + DoctorId + "' AND SZ_COMPANY_ID='" + CompanyId + "')", conn);
            ds = new DataSet();
            sqlda.Fill(ds);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return "";
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public bool IsValidPdf(string fileName)
    {
        PDFValidator pdf = new PDFValidator(fileName);
        return pdf.IsValid;
    }
}

