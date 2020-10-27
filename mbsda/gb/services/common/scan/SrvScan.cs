using System;
using System.Collections.Generic;
using System.Text;
using model = gb.mbs.da;
using dataaccess = gb.mbs.da.dataaccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Collections;
using gb.mbs.da.model.common;
using gb.mbs.da.services;
using File = System.IO.File;

namespace gb.mbs.da.services.common
{
    public class SrvScanUpload
    {
        model.common.DocumentNode c_oNode = null;
        model.patient.Patient c_oPatient = null;

        private string sSQLCon = string.Empty;
        string NodePath = string.Empty;
        string PhysicalPath = string.Empty;
        ArrayList c_lstSourceFile = null;

        //TODO: Convert this parameter to object

        public SrvScanUpload(model.patient.Patient p_oPatient, model.common.DocumentNode p_oNode, ArrayList p_lstSourceFile)
        {
            this.c_oNode = p_oNode;
            this.c_oPatient = p_oPatient;
            this.c_lstSourceFile = p_lstSourceFile;
            this.sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
        }
        public void ToRequiredDocuments(model.document.RequiredDocument p_oRequiredDocument)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            model.common.ApplicationSettings p_oApplicationSettings = null;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                sqlCmd = new SqlCommand("SP_REQUIRED_DOCUMENTS_GET_FULL_NODE_PATH", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqlTran;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@SZ_NODEID", c_oNode.ID);
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                sqlDa = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlDa.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NodePath = ds.Tables[0].Rows[0][0].ToString(); ;
                    }
                }

                NodePath = NodePath + "\\";

                NodePath = NodePath.Replace(@"\", "/");

                p_oApplicationSettings = new model.common.ApplicationSettings();
                PhysicalPath = p_oApplicationSettings.GetParameterValue("PhysicalBasePath");
                PhysicalPath = PhysicalPath.Replace(@"\", "/");
                // File.WriteAllBytes(string path, byte[] bytes);
                if (!Directory.Exists(PhysicalPath + NodePath))
                {
                    Directory.CreateDirectory(PhysicalPath + NodePath);
                }
                ArrayList p_lstDestinationFile = new ArrayList();
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    model.common.File p_oFile = new model.common.File();
                    model.common.File p_oDestinationFile = new model.common.File();

                    p_oFile = (model.common.File)c_lstSourceFile[i];

                    File.Copy(p_oFile.Path + p_oFile.Name, PhysicalPath + NodePath + p_oFile.Name, true);
                    p_oDestinationFile.Name = p_oFile.Name;
                    p_oDestinationFile.Path = NodePath;
                    p_oDestinationFile.ShortPath = NodePath.Replace(c_oPatient.Account.Name + @"/", "");
                    p_lstDestinationFile.Add(p_oDestinationFile);
                }
                DataTable tblTypeData = new DataTable();
                tblTypeData.Columns.Add("i_case_id");
                tblTypeData.Columns.Add("sz_company_id");
                tblTypeData.Columns.Add("sz_file_name");
                tblTypeData.Columns.Add("sz_file_path");
                tblTypeData.Columns.Add("sz_file_short_path");
                tblTypeData.Columns.Add("i_node_id");
                tblTypeData.Columns.Add("i_txn_document_id");
                tblTypeData.Columns.Add("i_txn_document_type_id");
                tblTypeData.Columns.Add("sz_document_type");
                tblTypeData.Columns.Add("sz_notes");
                tblTypeData.Columns.Add("sz_assign_to");
                tblTypeData.Columns.Add("sz_user_id");
                tblTypeData.Columns.Add("sz_user_name");
                tblTypeData.Columns.Add("sz_user_role_id");
                string RequiredDocumentID = p_oRequiredDocument.ID.ToString();
                for (int i = 0; i < p_lstDestinationFile.Count; i++)
                {
                    model.common.File p_oFile = new model.common.File();
                    p_oFile = (model.common.File)p_lstDestinationFile[i];

                    DataRow rowTypeData = tblTypeData.NewRow();
                    rowTypeData["i_case_id"] = c_oPatient.CaseID;
                    rowTypeData["sz_company_id"] = c_oPatient.Account.ID;
                    rowTypeData["sz_file_name"] = p_oFile.Name;
                    rowTypeData["sz_file_path"] = p_oFile.Path;
                    rowTypeData["sz_file_short_path"] = p_oFile.ShortPath;
                    rowTypeData["i_node_id"] = c_oNode.ID;
                    rowTypeData["i_txn_document_id"] = RequiredDocumentID;
                    rowTypeData["i_txn_document_type_id"] = p_oRequiredDocument.Type;
                    rowTypeData["sz_document_type"] = "";
                    rowTypeData["sz_notes"] = p_oRequiredDocument.Note;
                    rowTypeData["sz_assign_to"] = p_oRequiredDocument.AssignedTo.ID;
                    rowTypeData["sz_user_id"] = p_oRequiredDocument.UpdatedBy.ID;
                    rowTypeData["sz_user_name"] = p_oRequiredDocument.UpdatedBy.UserName;
                    rowTypeData["sz_user_role_id"] = p_oRequiredDocument.UpdatedBy.Role.ID;
                    tblTypeData.Rows.Add(rowTypeData);

                    sqlCmd = new SqlCommand("sp_mbs_create_required_document", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Transaction = sqlTran;
                    SqlParameter tvpParam = sqlCmd.Parameters.AddWithValue("@tvp_data", tblTypeData);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "typ_create_required_document";
                    sqlDa = new SqlDataAdapter(sqlCmd);
                    ds = new DataSet();
                    sqlDa.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RequiredDocumentID = ds.Tables[0].Rows[0][0].ToString(); ;
                        }
                    }
                    tblTypeData.Clear();

                    string strMessage = "REQUIRED_DOCUMENT-File " + p_oFile.Name + " is add by user " + p_oRequiredDocument.UpdatedBy.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Node ID " + c_oNode.ID.ToString() + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oRequiredDocument.UpdatedBy.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                if (RequiredDocumentID != "")
                {
                    sqlCmd = new SqlCommand("sp_mbs_update_required_document", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@i_txn_document_id", RequiredDocumentID);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@i_case_id", c_oPatient.CaseID);
                    sqlCmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                sqlTran.Rollback();
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

        public void ToOldRequiredDocuments(model.document.RequiredDocument p_oRequiredDocument)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            model.common.ApplicationSettings p_oApplicationSettings = null;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                sqlCmd = new SqlCommand("SP_REQUIRED_DOCUMENTS_GET_FULL_NODE_PATH", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqlTran;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@SZ_NODEID", c_oNode.ID);
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                sqlDa = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlDa.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NodePath = ds.Tables[0].Rows[0][0].ToString(); ;
                    }
                }

                NodePath = NodePath + "\\";

                NodePath = NodePath.Replace(@"\", "/");

                p_oApplicationSettings = new model.common.ApplicationSettings();
                PhysicalPath = p_oApplicationSettings.GetParameterValue("PhysicalBasePath");
                PhysicalPath = PhysicalPath.Replace(@"\", "/");
                // File.WriteAllBytes(string path, byte[] bytes);
                if (!Directory.Exists(PhysicalPath + NodePath))
                {
                    Directory.CreateDirectory(PhysicalPath + NodePath);
                }
                ArrayList p_lstDestinationFile = new ArrayList();

                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    // model.common.File p_oFile = new model.common.File();
                    model.common.File p_oDestinationFile = new model.common.File();
                    model.common.File p_oFile = new model.common.File();
                    p_oFile = (model.common.File)c_lstSourceFile[i];

                    File.Copy(p_oFile.Path + p_oFile.Name, PhysicalPath + NodePath + p_oFile.Name, true);
                    p_oDestinationFile.Name = p_oFile.Name;
                    p_oDestinationFile.Path = NodePath;
                    p_oDestinationFile.ShortPath = NodePath.Replace(c_oPatient.Account.Name + @"/", "");
                    p_lstDestinationFile.Add(p_oDestinationFile);


                    sqlCmd = new SqlCommand("SP_SAVE_REQ_DOCUMENT", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NODE_NAME", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", NodePath);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", p_oRequiredDocument.UpdatedBy.UserName);
                    sqlCmd.Parameters.AddWithValue("@I_MST_NODE_ID", c_oNode.ID);
                    SqlParameter parameter = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;

                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.ExecuteNonQuery();
                    string str_imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();
                    string RequiredDocumentID = p_oRequiredDocument.ID.ToString();
                    if (RequiredDocumentID != "" && RequiredDocumentID != "0")
                    {

                        sqlCmd = new SqlCommand("SP_REQ_DOCUMENTS", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_DOCUMENT_ID", p_oRequiredDocument.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "");
                        sqlCmd.Parameters.AddWithValue("@I_DOCUMENT_TYPE_ID", p_oRequiredDocument.Type);
                        sqlCmd.Parameters.AddWithValue("@I_RECIEVED", 1);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTES", p_oRequiredDocument.Note);
                        sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_TO", p_oRequiredDocument.AssignedTo.ID);
                        sqlCmd.Parameters.AddWithValue("@DT_ASSIGN_ON", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_UPDATED_BY", p_oRequiredDocument.UpdatedBy.ID);
                        sqlCmd.Parameters.AddWithValue("@DT_UPDATED_ON", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_STATUS", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", str_imageId);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
                        sqlCmd.ExecuteNonQuery();

                        string strMessage = "PAYMENT-File " + p_oFile.Name + " is add by user " + p_oRequiredDocument.UpdatedBy.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Node " + c_oNode.ID.ToString() + ".";

                        sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oRequiredDocument.UpdatedBy.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                        sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();

                    }
                    else
                    {
                        sqlCmd = new SqlCommand("SP_REQ_DOCUMENTS", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_DOCUMENT_ID", p_oRequiredDocument.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "");
                        sqlCmd.Parameters.AddWithValue("@I_DOCUMENT_TYPE_ID", p_oRequiredDocument.Type);
                        sqlCmd.Parameters.AddWithValue("@I_RECIEVED", 1);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTES", p_oRequiredDocument.Note);
                        sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_TO", p_oRequiredDocument.AssignedTo.ID);
                        sqlCmd.Parameters.AddWithValue("@DT_ASSIGN_ON", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_UPDATED_BY", p_oRequiredDocument.UpdatedBy.ID);
                        sqlCmd.Parameters.AddWithValue("@DT_UPDATED_ON", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_STATUS", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", str_imageId);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();

                        string strMessage = "PAYMENT-File " + p_oFile.Name + " is add by user " + p_oRequiredDocument.UpdatedBy.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Node " + c_oNode.ID.ToString() + ".";

                        sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oRequiredDocument.UpdatedBy.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                        sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();
                    }
                }

                sqlTran.Commit();

            }
            catch (Exception)
            {
                sqlTran.Rollback();
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

        public void ToAppointment(model.appointment.Appointment p_oAppointment, model.user.User p_oUser)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();

                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    sqlCmd = new SqlCommand("sp_create_scanupload_visit_document", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@i_event_id", p_oAppointment.ID);
                    sqlCmd.Parameters.AddWithValue("@i_case_id", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@i_case_no", c_oPatient.CaseNo);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_username", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", p_oAppointment.Speciality.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_visit_type_id", p_oAppointment.TypeID);
                    sqlCmd.Parameters.AddWithValue("@sz_visit_type", p_oAppointment.Type);
                    sqlCmd.Parameters.AddWithValue("@sz_source", "PATIENT-DESK");
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_file_path", p_oFile.Path);
                    sqlDa = new SqlDataAdapter(sqlCmd);
                    ds = new DataSet();
                    sqlDa.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Filepath = ds.Tables[0].Rows[0]["Path"].ToString();
                            FileName = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }

                    string strMessage = "VISIT-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Event " + p_oAppointment.ID.ToString() + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }
                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }


                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToPayment(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.payment.Payment> lstPayment = p_oBill.Payment;
                    model.payment.Payment p_oBillPayment = lstPayment[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    sqlCmd = new SqlCommand("sp_mbs_create_payment_document", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@i_case_id", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", p_oBill.Specialty.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_company_name", c_oPatient.Account.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_bill_no", p_oBill.Number);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_payment_id", p_oBillPayment.Id);
                    sqlCmd.Parameters.AddWithValue("@node_type", c_oNode.Type);
                    sqlCmd.Parameters.AddWithValue("@sz_source_id", "");
                    sqlCmd.Parameters.AddWithValue("@sz_ip_address", "");
                    SqlParameter sqlparamFilePath = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter sqlparamImgId = new SqlParameter("@i_image_id", SqlDbType.Int);
                    sqlparamFilePath.Direction = ParameterDirection.Output;
                    sqlparamImgId.Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add(sqlparamFilePath);
                    sqlCmd.Parameters.Add(sqlparamImgId);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    FileName = p_oFile.Name;
                    string strMessage = "PAYMENT-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + "For Bill " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }
                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToPOM(model.bill.Bill p_oBill, model.user.User p_oUser)
        {

            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_POM_CASE_ID", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqlTran;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                sqlCmd.Parameters.AddWithValue("@I_POM_ID", p_oBill.PomId);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                string PathFlag = "";
                sqlDa.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable uniqueCols = ds.Tables[0].DefaultView.ToTable(true, "SZ_CASE_ID");

                        if (uniqueCols.Rows.Count > 1)
                        {
                            PathFlag = @"Common Folder\POM\";
                        }
                        for (int iCount = 0; iCount < ds.Tables[0].Rows.Count; iCount++)
                        {
                            for (int i = 0; i < c_lstSourceFile.Count; i++)
                            {


                                string Filepath = "";
                                string FileName = "";
                                model.common.File p_oFile = new model.common.File();
                                p_oFile = (model.common.File)c_lstSourceFile[i];
                                sqlCmd = new SqlCommand("sp_mbs_create_pom_document", sqlCon);
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Transaction = sqlTran;
                                sqlCmd.CommandTimeout = 0;
                                sqlCmd.Parameters.AddWithValue("@i_case_id", ds.Tables[0].Rows[iCount]["SZ_CASE_ID"].ToString());
                                sqlCmd.Parameters.AddWithValue("@sz_specialty_id", ds.Tables[0].Rows[iCount]["sz_speciality_id"].ToString());
                                sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                                sqlCmd.Parameters.AddWithValue("@sz_company_name", c_oPatient.Account.Name);
                                sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                                sqlCmd.Parameters.AddWithValue("@sz_path_flag", PathFlag);
                                sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                                sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                                sqlCmd.Parameters.AddWithValue("@i_pom_id", p_oBill.PomId);
                                sqlCmd.Parameters.AddWithValue("@sz_bill_status", p_oBill.BillStatus.Code);
                                sqlCmd.Parameters.AddWithValue("@sz_source_id", "");
                                sqlCmd.Parameters.AddWithValue("@sz_ip_address", "");
                                SqlParameter sqlparamFilePath = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                                SqlParameter sqlparamImgId = new SqlParameter("@i_image_id", SqlDbType.Int);
                                sqlparamFilePath.Direction = ParameterDirection.Output;
                                sqlparamImgId.Direction = ParameterDirection.Output;
                                sqlCmd.Parameters.Add(sqlparamFilePath);
                                sqlCmd.Parameters.Add(sqlparamImgId);
                                sqlCmd.ExecuteNonQuery();
                                Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                                FileName = p_oFile.Name;

                                string strMessage = "POM-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For POM " + p_oBill.PomId.ToString() + ".";

                                sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                                sqlCmd.Transaction = sqlTran;
                                sqlCmd.CommandTimeout = 0;
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", ds.Tables[0].Rows[iCount]["SZ_CASE_ID"].ToString());
                                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                                sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                                sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                                sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                                sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                                sqlCmd.ExecuteNonQuery();
                                model.common.ApplicationSettings p_oApplicationSettings = new model.common.ApplicationSettings();
                                PhysicalPath = p_oApplicationSettings.GetParameterValue("PhysicalBasePath");
                                if (Filepath != "" && FileName != "")
                                {
                                    if (!Directory.Exists(PhysicalPath+ Filepath))
                                    {
                                        Directory.CreateDirectory(PhysicalPath +Filepath);
                                    }
                                    File.Copy(p_oFile.Path + p_oFile.Name, PhysicalPath+ Filepath + FileName, true);
                                }

                            }

                        }

                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToDocMgr(model.user.User p_oUser)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();

                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    sqlCmd = new SqlCommand("sp_mbs_create_doc_mgr_document", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@i_node_id", c_oNode.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_source_process", "DOC-MGR");
                    sqlCmd.Parameters.AddWithValue("@sz_ip_address", "");
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID.ToString());

                    sqlDa = new SqlDataAdapter(sqlCmd);
                    ds = new DataSet();
                    sqlDa.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Filepath = ds.Tables[0].Rows[0]["Path"].ToString();
                            FileName = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }

                    string strMessage = "DOC-MGR-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Node " + c_oNode.ID.ToString() + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }
                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToSaveVisitDocument(model.user.User p_oUser)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();


            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string sPath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();

                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    sqlCmd = new SqlCommand("proc_save_jfk_visit_documents", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_NAME", c_oPatient.Account.Name);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID.ToString());
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@I_visit_id", c_oNode.ID.ToString());

                    sqlDa = new SqlDataAdapter(sqlCmd);
                    ds = new DataSet();
                    sqlDa.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sPath = ds.Tables[0].Rows[0][0].ToString();
                            FileName = p_oFile.Name;
                        }
                    }
                    string strMessage = "JFK-VISIT-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Visit Id " + c_oNode.ID.ToString() + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();
                    model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                    string sBasePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");

                    if (sPath != "" && FileName != "")
                    {
                        if (!Directory.Exists(sBasePath + sPath))
                        {
                            Directory.CreateDirectory(sBasePath + sPath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, sBasePath + sPath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToDenial(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.denial.Denial> lstDenial = p_oBill.Denial;
                    model.bill.denial.Denial p_oBillDenial = lstDenial[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];

                    string str = "";
                    int num = 0;
                    model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                    string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string specialty = p_oBill.Specialty.ID;
                    string pathFlag = @"Common Folder\" + caseId + @"\" + "Denials" + @"\";


                    sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                    sqlCmd.Parameters.AddWithValue("@sz_process", "DEN");
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                    if (pathFlag != "" && pathFlag != null)
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                    }
                    SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    parameter2.Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.Parameters.Add(parameter2);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();


                    SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                    command2.Transaction = sqlTran;
                    command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_oBill.Number);
                    command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                    command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillDenial.Id);
                    command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONPOPUP");
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.ExecuteNonQuery();

                    FileName = p_oFile.Name;

                    Filepath = basePath + Filepath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "DENIAL-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToAppointmentWithReferringFacility(model.appointment.Appointment p_oAppointment, model.user.User p_oUser)
        {
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            //sqlTran = sqlCon.BeginTransaction();

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    model.common.File p_oFile = new model.common.File();
                    string Filepath = "";
                    string FileName = "";

                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    //string Path = ds2.Tables[0].Rows[0]["Path"].ToString().Replace("/", "\\") + c_oPatient.Account.Name + "\\" + ds2.Tables[1].Rows[0][0].ToString().Replace("/", "\\") + "\\";
                    //string FilePath = c_oPatient.Account.Name + "/" + ds2.Tables[1].Rows[0][0].ToString().Replace("\\", "/") + "/";
                    DataSet ds1 = GetNodeIDandPath(p_oAppointment);
                    String Msg = SaveFileAndUpdateDocManager(ds1, p_oAppointment, p_oUser, p_oFile);
                    if (Msg != "Failed")
                    {
                        Filepath = ds1.Tables[1].Rows[0][0].ToString();
                        FileName = p_oFile.Name;
                        model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                        string BasePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");

                        string strMessage = "VISIT-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Visit Id " + p_oAppointment.ID.ToString() + ".";

                        sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                        sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();

                        if (Filepath != "" && FileName != "")
                        {
                            if (!Directory.Exists(BasePath + c_oPatient.Account.Name + "/" + Filepath))
                            {
                                Directory.CreateDirectory(BasePath + c_oPatient.Account.Name + "/" + Filepath);
                            }
                            File.Copy(p_oFile.Path + p_oFile.Name, BasePath + c_oPatient.Account.Name + "/" + Filepath + "/" + FileName, true);
                        }
                        else
                        {
                            throw new Exception("Unable to save document");
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to save document");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public DataSet GetNodeIDandPath(model.appointment.Appointment p_oAppointment)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            DataSet ds;
            SqlDataAdapter sqlDa;
            try
            {
                ArrayList _return = new ArrayList();
                sqlDa = new SqlDataAdapter("EXEC SP_UPLOAD_DOCUMENT_FOR_VISIT_REFERRAL @SZ_CASE_ID='" + c_oPatient.CaseID + "', @SZ_COMPANY_ID='" + c_oPatient.Account.ID + "', @SZ_PROCEDURE_GROUP_ID='" + p_oAppointment.Speciality.ID + "'", sqlCon);
                ds = new DataSet();
                sqlDa.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlCon.Close(); }
        }

        public String SaveFileAndUpdateDocManager(DataSet ds2, model.appointment.Appointment p_oAppointment, model.user.User p_oUser, model.common.File p_oFile)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            DataSet ds;
            SqlDataAdapter sqlDa;
            string Filepath = "";

            Boolean flag = true;
            try
            {
                if ((ds2.Tables[0].Rows[0][0].ToString() == "") || (ds2.Tables[0].Rows[0][0].ToString() == null))
                {
                    flag = false;
                }
                else
                {
                    if ((ds2.Tables[1].Rows[0][0].ToString() == "") || (ds2.Tables[1].Rows[0][0].ToString() == null))
                    {
                        flag = false;
                    }
                }
                if (flag == false)
                {
                    return "Failed";
                }
                else
                {
                    //string BasePath = ds2.Tables[0].Rows[0][0].ToString();
                    string casePath = ds2.Tables[1].Rows[0][0].ToString();
                    string file_Path = c_oPatient.Account.Name + "/" + casePath + "/";
                    sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SP_WS_UPLOAD_REPORT_FOR_VISIT";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID ", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", file_Path);
                    sqlCmd.Parameters.AddWithValue("@I_TAG_ID", ds2.Tables[0].Rows[0]["NodeID"].ToString());
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", p_oAppointment.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_oAppointment.Speciality.ID);
                    sqlCmd.ExecuteNonQuery();

                    return Filepath;
                }
            }
            catch (Exception ex)
            {
                return "Failed";
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
        }

        public void ToVerification(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";
            string pathFlag = "";

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.verification.Verification> lstVerification = p_oBill.Verification;
                    model.bill.verification.Verification p_oBillVerification = lstVerification[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    model.common.ApplicationSettings p_oApplicationSettings = new ApplicationSettings();
                    string basePath = p_oApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string specialty = p_oBill.Specialty.ID;

                    string[] sSpecialties = System.Text.RegularExpressions.Regex.Split(specialty, @"\,");
                    for (int j = 1; j < sSpecialties.Length; j++)
                    {
                        string arr1 = sSpecialties[j].ToString();
                        string arr2 = sSpecialties[j - 1].ToString();
                        if (arr1 != arr2)
                        {
                            pathFlag = "Common Folder" + "\\" + caseId + "\\" + ConfigurationManager.AppSettings["VR"].ToString() + "\\";
                            break;
                        }
                    }

                    sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                    sqlCmd.Parameters.AddWithValue("@sz_process", "VR");
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                    if (pathFlag != null && pathFlag != "")
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                    }
                    SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    parameter2.Direction = ParameterDirection.Output;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.Parameters.Add(parameter2);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                    string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(p_oBill.Number, @"\,");
                    for (int j = 0; j < sBillNumber.Length; j++)
                    {
                        SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", sBillNumber[j].ToString());
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillVerification.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONSENT");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();
                    }

                    FileName = p_oFile.Name;

                    Filepath = basePath + Filepath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "VERIFICATION-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
        public void ToEOR(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";
            string pathFlag = "";

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.verification.Verification> lstVerification = p_oBill.Verification;
                    model.bill.verification.Verification p_oBillVerification = lstVerification[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    model.common.ApplicationSettings p_oApplicationSettings = new ApplicationSettings();
                    string basePath = p_oApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string specialty = p_oBill.Specialty.ID;

                    string[] sSpecialties = System.Text.RegularExpressions.Regex.Split(specialty, @"\,");
                    for (int j = 1; j < sSpecialties.Length; j++)
                    {
                        string arr1 = sSpecialties[j].ToString();
                        string arr2 = sSpecialties[j - 1].ToString();
                        if (arr1 != arr2)
                        {
                            pathFlag = "Common Folder" + "\\" + caseId + "\\" + ConfigurationManager.AppSettings["VR"].ToString() + "\\";
                            break;
                        }
                    }

                    sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                    sqlCmd.Parameters.AddWithValue("@sz_process", "EOR");
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                    if (pathFlag != null && pathFlag != "")
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                    }
                    SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    parameter2.Direction = ParameterDirection.Output;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.Parameters.Add(parameter2);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                    string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(p_oBill.Number, @"\,");
                    for (int j = 0; j < sBillNumber.Length; j++)
                    {
                        SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", sBillNumber[j].ToString());
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillVerification.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONSENT");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();
                    }

                    FileName = p_oFile.Name;

                    Filepath = basePath + Filepath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "EOR-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToVerificationSent(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";
            string pathFlag = "";

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.verification.Verification> lstVerification = p_oBill.Verification;
                    model.bill.verification.Verification p_oBillVerification = lstVerification[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];
                    model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                    string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string specialty = p_oBill.Specialty.ID;

                    string[] sSpecialties = System.Text.RegularExpressions.Regex.Split(specialty, @"\,");
                    for (int j = 1; j < sSpecialties.Length; j++)
                    {
                        string arr1 = sSpecialties[j].ToString();
                        string arr2 = sSpecialties[j - 1].ToString();
                        if (arr1 != arr2)
                        {
                            pathFlag = "Common Folder" + "\\" + caseId + "\\" + ConfigurationManager.AppSettings["VR"].ToString() + "\\";
                            break;
                        }
                    }

                    sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                    sqlCmd.Parameters.AddWithValue("@sz_process", "VR");
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                    if (pathFlag != null && pathFlag != "")
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                    }
                    SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    parameter2.Direction = ParameterDirection.Output;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.Parameters.Add(parameter2);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                    string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(p_oBill.Number, @"\,");
                    for (int j = 0; j < sBillNumber.Length; j++)
                    {
                        SqlCommand command2 = new SqlCommand("SP_ANSWER_VERIFICATION_IMAGES", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", sBillNumber[j].ToString());
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_VERIFICATION_ID", p_oBillVerification.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONANSWER");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();
                    }

                    FileName = p_oFile.Name;

                    Filepath = basePath + Filepath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "VERIFICATION-SENT-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();

                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToVerificationDenial(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";
            string pathFlag = "";

            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.denial.Denial> lstDenial = p_oBill.Denial;
                    model.bill.denial.Denial p_oBillDenial = lstDenial[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];

                    model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                    string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string specialty = p_oBill.Specialty.ID;

                    string[] sSpecialties = System.Text.RegularExpressions.Regex.Split(specialty, @"\,");
                    for (int j = 1; j < sSpecialties.Length; j++)
                    {
                        string arr1 = sSpecialties[j].ToString();
                        string arr2 = sSpecialties[j - 1].ToString();
                        if (arr1 != arr2)
                        {
                            pathFlag = "Common Folder" + "\\" + caseId + "\\" + ConfigurationManager.AppSettings["DEN"].ToString() + "\\";
                            break;
                        }
                    }


                    sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                    sqlCmd.Parameters.AddWithValue("@sz_process", "DEN");
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                    if (pathFlag != "" && pathFlag != null)
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                    }
                    SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                    SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    parameter2.Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add(parameter);
                    sqlCmd.Parameters.Add(parameter2);
                    sqlCmd.ExecuteNonQuery();
                    Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                    string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(p_oBill.Number, @"\,");
                    for (int j = 0; j < sBillNumber.Length; j++)
                    {
                        SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", sBillNumber[j].ToString());
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillDenial.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONSENT");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();
                    }

                    FileName = p_oFile.Name;

                    Filepath = basePath + Filepath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "DENIAL-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();
                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void ToVerificationPopup(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            string imageId = "";

            try
            {
                if (p_oBill.Process.ToLower() == "den")
                {
                    for (int i = 0; i < c_lstSourceFile.Count; i++)
                    {
                        string Filepath = "";
                        string FileName = "";
                        model.common.File p_oFile = new model.common.File();
                        List<model.bill.denial.Denial> lstDenial = p_oBill.Denial;
                        model.bill.denial.Denial p_oBillDenial = lstDenial[0];
                        p_oFile = (model.common.File)c_lstSourceFile[i];

                        model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                        string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        string caseId = Convert.ToString(c_oPatient.CaseID);
                        string specialty = p_oBill.Specialty.ID;
                        string pathFlag = "";

                        sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                        sqlCmd.Parameters.AddWithValue("@sz_process", p_oBill.Process);
                        sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                        sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                        sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                        sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                        if (pathFlag != "" && pathFlag != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                        }
                        SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                        SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                        parameter.Direction = ParameterDirection.Output;
                        parameter2.Direction = ParameterDirection.Output;
                        sqlCmd.Parameters.Add(parameter);
                        sqlCmd.Parameters.Add(parameter2);
                        sqlCmd.ExecuteNonQuery();
                        Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                        imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                        SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_oBill.Number);
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillDenial.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONPOPUP");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();

                        FileName = p_oFile.Name;

                        Filepath = basePath + Filepath;
                        Filepath = Filepath.Replace(@"\", "/");

                        string strMessage = "DENIAL-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                        sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                        sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

                        if (Filepath != "" && FileName != "")
                        {
                            if (!Directory.Exists(Filepath))
                            {
                                Directory.CreateDirectory(Filepath);
                            }

                            File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < c_lstSourceFile.Count; i++)
                    {
                        string Filepath = "";
                        string FileName = "";
                        model.common.File p_oFile = new model.common.File();
                        List<model.bill.verification.Verification> lstVerification = p_oBill.Verification;
                        model.bill.verification.Verification p_oBillVerification = lstVerification[0];
                        p_oFile = (model.common.File)c_lstSourceFile[i];

                        model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                        string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                        string caseId = Convert.ToString(c_oPatient.CaseID);
                        string specialty = p_oBill.Specialty.ID;
                        string pathFlag = "";

                        sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.Parameters.AddWithValue("@sz_case_id", caseId);
                        sqlCmd.Parameters.AddWithValue("@sz_process", p_oBill.Process);
                        sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@sz_file_name", p_oFile.Name);
                        sqlCmd.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                        sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                        sqlCmd.Parameters.AddWithValue("@sz_specialty_id", specialty);
                        if (pathFlag != "" && pathFlag != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@sz_path_flag", pathFlag);
                        }
                        SqlParameter parameter = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 0xff);
                        SqlParameter parameter2 = new SqlParameter("@i_image_id", SqlDbType.Int);
                        parameter.Direction = ParameterDirection.Output;
                        parameter2.Direction = ParameterDirection.Output;
                        sqlCmd.Parameters.Add(parameter);
                        sqlCmd.Parameters.Add(parameter2);
                        sqlCmd.ExecuteNonQuery();
                        Filepath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                        imageId = sqlCmd.Parameters["@i_image_id"].Value.ToString();

                        SqlCommand command2 = new SqlCommand("SP_INSERT_REQUEST_VERIFICATION_IMAGE", sqlCon);
                        command2.Transaction = sqlTran;
                        command2.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_oBill.Number);
                        command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        command2.Parameters.AddWithValue("@SZ_IMAGE_ID", imageId);
                        command2.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        command2.Parameters.AddWithValue("@I_TRANSACTION_ID", p_oBillVerification.Id);
                        command2.Parameters.AddWithValue("@FLAG", "VERIFICATIONPOPUP");
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.ExecuteNonQuery();

                        FileName = p_oFile.Name;

                        Filepath = basePath + Filepath;
                        Filepath = Filepath.Replace(@"\", "/");

                        string strMessage = "VERIFICATION-File " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + p_oBill.Number + ".";

                        sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                        sqlCmd.Transaction = sqlTran;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                        sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                        sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                        sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

                        if (Filepath != "" && FileName != "")
                        {
                            if (!Directory.Exists(Filepath))
                            {
                                Directory.CreateDirectory(Filepath);
                            }

                            File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                        }
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
        public void ToJFKPayment(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {

                sqlCmd = new SqlCommand("proc_get_invoce_cases", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqlTran;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@sz_company_id", c_oPatient.Account.ID);
                sqlCmd.Parameters.AddWithValue("@sz_invoice_no", p_oBill.Number);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sqlDa.Fill(ds);
                string BasePath = "";
                string BasePathId = "";
                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        BasePath = ds.Tables[1].Rows[0]["PhysicalBasePath"].ToString();
                        BasePathId = ds.Tables[1].Rows[0]["BasePathId"].ToString();


                    }
                }
                model.common.File p_oFile = new model.common.File();

                p_oFile = (model.common.File)c_lstSourceFile[0];
                string Path = c_oPatient.Account.Name + "/" + "Invoice/Payment/" + p_oBill.Number + "/";
                List<model.payment.Payment> lstPayment = p_oBill.Payment;
                model.payment.Payment p_oBillPayment = lstPayment[0];
                sqlCmd = new SqlCommand("PROC_SAVE_EMPLOYER_INVOICE_PAYMENT_IMAGES", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@I_PAYMENT_ID", p_oBillPayment.Id);
                sqlCmd.Parameters.AddWithValue("@SZ_COMMPANY_ID", c_oPatient.Account.ID);
                sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", p_oBill.Number);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", Path);
                sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", p_oFile.Name);
                sqlCmd.Parameters.AddWithValue("@BASE_PATH_ID", BasePathId);

                sqlCmd.CommandTimeout = 0;
                sqlCmd.Transaction = sqlTran;
                SqlDataReader dr = sqlCmd.ExecuteReader();
                string sImageId = "";
                while (dr.Read())
                {
                    sImageId = dr[0].ToString();
                }
                dr.Close();


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int iCount = 0; iCount < ds.Tables[0].Rows.Count; iCount++)
                        {
                            sqlCmd = new SqlCommand("PROC_SAVE_CASE_INVOICE_IMAGE", sqlCon);
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = sqlTran;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", sImageId);
                            sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", "NFPAYMENT");
                            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", ds.Tables[0].Rows[iCount][0].ToString());
                            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", p_oUser.UserName);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }



                if (Path != "" && p_oFile.Name != "")
                {
                    if (!Directory.Exists(BasePath + Path))
                    {
                        Directory.CreateDirectory(BasePath + Path);
                    }
                    File.Copy(p_oFile.Path + p_oFile.Name, BasePath + Path + p_oFile.Name, true);
                }

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
        public void ToGeneralDenial(model.bill.Bill p_oBill, model.user.User p_oUser)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            try
            {
                for (int i = 0; i < c_lstSourceFile.Count; i++)
                {
                    string Filepath = "";
                    string FileName = "";
                    model.common.File p_oFile = new model.common.File();
                    List<model.bill.denial.Denial> lstDenial = p_oBill.Denial;
                    model.bill.denial.Denial p_oBillDenial = lstDenial[0];
                    p_oFile = (model.common.File)c_lstSourceFile[i];

                    model.common.ApplicationSettings o_ApplicationSettings = new ApplicationSettings();
                    string basePath = o_ApplicationSettings.GetParameterValue("PhysicalBasePath");
                    string caseId = Convert.ToString(c_oPatient.CaseID);
                    string caseNo = Convert.ToString(c_oPatient.CaseNo);

                    string filePath = c_oPatient.Account.Name + @"\" + caseNo + @"\" + "General Denial" + @"\";

                    SqlCommand command2 = new SqlCommand("SP_SAVE_GENERAL_DENIALS_IMAGE", sqlCon);
                    command2.Transaction = sqlTran;
                    command2.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    command2.Parameters.AddWithValue("@NODE_ID", c_oNode.ID);
                    command2.Parameters.AddWithValue("@SZ_FILE_NAME", p_oFile.Name);
                    command2.Parameters.AddWithValue("@SZ_FILE_PATH", filePath);
                    command2.Parameters.AddWithValue("@I_DENIAL_ID", p_oBillDenial.Id);
                    command2.Parameters.AddWithValue("@USER_NAME", p_oUser.UserName);
                    command2.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.ExecuteNonQuery();

                    FileName = p_oFile.Name;

                    Filepath = basePath + filePath;
                    Filepath = Filepath.Replace(@"\", "/");

                    string strMessage = "GENERAL-DENIA " + FileName + " is add by user " + p_oUser.UserName + " On " + DateTime.Now.ToString("MM/dd/yyyy") + " For Case ID " + c_oPatient.CaseID + ".";

                    sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", c_oPatient.CaseID);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                    sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
                    sqlCmd.Parameters.AddWithValue("@IS_DENIED", "");
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", c_oPatient.Account.ID);
                    sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                    sqlCmd.ExecuteNonQuery();
                    if (Filepath != "" && FileName != "")
                    {
                        if (!Directory.Exists(Filepath))
                        {
                            Directory.CreateDirectory(Filepath);
                        }

                        File.Copy(p_oFile.Path + p_oFile.Name, Filepath + FileName, true);
                    }
                }
                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

    }
}