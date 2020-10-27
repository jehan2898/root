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
using log4net;

/// <summary>
/// Summary description for Bill_Sys_UploadFile
/// </summary>
[Serializable]
public class Bill_Sys_UploadFile
{
  

    public Bill_Sys_UploadFile()
    {
        //strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    private ArrayList _sz_bill_no;
    public ArrayList sz_bill_no
    {
        set
        {
            _sz_bill_no = value;
        }
        get
        {
            return _sz_bill_no;
        }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }

    private string _sz_flag;
    public string sz_flag
    {
        set
        {
            _sz_flag = value;
        }
        get
        {
            return _sz_flag;
        }
    }

    private ArrayList _sz_case_id;
    public ArrayList sz_case_id
    {
        get
        {
            return _sz_case_id;
        }
        set
        {
            _sz_case_id = value;
        }
    }

    private ArrayList _sz_speciality_id;
    public ArrayList sz_speciality_id
    {
        get
        {
            return _sz_speciality_id;
        }
        set
        {
            _sz_speciality_id = value;
        }
    }

    private string _sz_FileName;
    public string sz_FileName
    {
        get
        {
            return _sz_FileName;
        }
        set
        {
            _sz_FileName = value;
        }
    }

    private byte[] _sz_File;
    public byte[] sz_File
    {
        get
        {
            return _sz_File;
        }
        set
        {
            _sz_File = value;
        }
    }

    private string _sz_UserName;
    public string sz_UserName
    {
        get
        {
            return _sz_UserName;
        }
        set
        {
            _sz_UserName = value;
        }
    }

    private string _sz_UserId;
    public string sz_UserId
    {
        get
        {
            return _sz_UserId;
        }
        set
        {
            _sz_UserId = value;
        }
    }

    private string _sz_StatusCode;
    public string sz_StatusCode
    {
        get
        {
            return _sz_StatusCode;
        }
        set
        {
            _sz_StatusCode = value;
        }
    }

    private string _veri_flag;
    public string sz_veri_flag
    {
        get
        {
            return _veri_flag;
        }
        set
        {
            _veri_flag = value;
        }
    }

    private string _sz_payment_id;
    public string sz_payment_id
    {
        get
        {
            return _sz_payment_id;
        }
        set
        {
            _sz_payment_id = value;
        }
    }

    private string _sz_PathFlag;
    public string sz_PathFlag
    {
        get
        {
            return _sz_PathFlag;
        }
        set
        {
            _sz_PathFlag = value;
        }
    }
    private string _sz_File_PhysicalPath;
    public string sz_File_PhysicalPath
    {
        get
        {
            return _sz_File_PhysicalPath;
        }
        set
        {
            _sz_File_PhysicalPath = value;
        }
    }
}

public class Bill_Sys_SoftwareInVoice_UploadFile
{


    public Bill_Sys_SoftwareInVoice_UploadFile()
    {
        //strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    private string _sz_bill_no;
    public string sz_bill_no
    {
        set
        {
            _sz_bill_no = value;
        }
        get
        {
            return _sz_bill_no;
        }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }

    private string _sz_flag;
    public string sz_flag
    {
        set
        {
            _sz_flag = value;
        }
        get
        {
            return _sz_flag;
        }
    }

    private string _sz_case_id;
    public string sz_case_id
    {
        get
        {
            return _sz_case_id;
        }
        set
        {
            _sz_case_id = value;
        }
    }

    private string _sz_speciality_id;
    public string sz_speciality_id
    {
        get
        {
            return _sz_speciality_id;
        }
        set
        {
            _sz_speciality_id = value;
        }
    }

    private string _sz_FileName;
    public string sz_FileName
    {
        get
        {
            return _sz_FileName;
        }
        set
        {
            _sz_FileName = value;
        }
    }

    private byte[] _sz_File;
    public byte[] sz_File
    {
        get
        {
            return _sz_File;
        }
        set
        {
            _sz_File = value;
        }
    }

    private string _sz_UserName;
    public string sz_UserName
    {
        get
        {
            return _sz_UserName;
        }
        set
        {
            _sz_UserName = value;
        }
    }

    private string _sz_UserId;
    public string sz_UserId
    {
        get
        {
            return _sz_UserId;
        }
        set
        {
            _sz_UserId = value;
        }
    }

    private string _sz_StatusCode;
    public string sz_StatusCode
    {
        get
        {
            return _sz_StatusCode;
        }
        set
        {
            _sz_StatusCode = value;
        }
    }

    private string _veri_flag;
    public string sz_veri_flag
    {
        get
        {
            return _veri_flag;
        }
        set
        {
            _veri_flag = value;
        }
    }

    private string _sz_payment_id;
    public string sz_payment_id
    {
        get
        {
            return _sz_payment_id;
        }
        set
        {
            _sz_payment_id = value;
        }
    }

    private string _sz_PathFlag;
    public string sz_PathFlag
    {
        get
        {
            return _sz_PathFlag;
        }
        set
        {
            _sz_PathFlag = value;
        }
    }
    private string _sz_File_PhysicalPath;
    public string sz_File_PhysicalPath
    {
        get
        {
            return _sz_File_PhysicalPath;
        }
        set
        {
            _sz_File_PhysicalPath = value;
        }
    }
    private string _i_ImageId;
    public string i_ImageId
    {
        get
        {
            return _i_ImageId;
        }
        set
        {
            _i_ImageId = value;
        }
    }
    private string _sz_Logical_Path;
    public string sz_Logical_Path
    {
        get
        {
            return _sz_Logical_Path;
        }
        set
        {
            _sz_Logical_Path = value;
        }
    }
}

public class FileUpload
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    private static ILog log = LogManager.GetLogger("Bill_Sys_UploadFile");
    public FileUpload()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public ArrayList UploadFile(Bill_Sys_UploadFile arr)
    {
        log.Debug("In upload file ");
        ArrayList arrNodeType = new ArrayList();
        ArrayList arrNodePath = new ArrayList();
        ArrayList arrNodeId = new ArrayList();
        ArrayList arrCaseId = new ArrayList();
        ArrayList arrSpec = new ArrayList();
        Hashtable htData = new Hashtable();

        Bill_Sys_UploadFile obj = new Bill_Sys_UploadFile();
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        string szPath = "";
        string szPathFlag = "";
        int flag = 0;
        string szBasePath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
        log.Debug("szBasePath" + szBasePath);
        arrCaseId = arr.sz_case_id;
        arrSpec = arr.sz_speciality_id;
        log.Debug("arrCaseId" + arrCaseId);
        log.Debug("arrCaseId" + arrSpec);
        if (arrCaseId.Count > 1)
        {
            for (int i = 1; i < arrCaseId.Count; i++)
            {
                string arr1 = arrCaseId[i].ToString();
                string arr2 = arrCaseId[i - 1].ToString();
                if (arr1 != arr2)
                {
                    flag = 1;
                    szPathFlag = "Common Folder" + "\\" + "POM" + "\\";
                    break;
                }
            }
        }


        if (flag != 1)
        {
            for (int i = 1; i < arrSpec.Count; i++)
            {
                string arr1 = arrSpec[i].ToString();
                string arr2 = arrSpec[i - 1].ToString();
                if (arr1 != arr2)
                {
                    flag = 1;
                    szPathFlag = "Common Folder" + "\\" + arrCaseId[0] + "\\" + ConfigurationManager.AppSettings[arr.sz_flag].ToString() + "\\";
                    break;
                }
            }
        }

        
        arr.sz_PathFlag = szPathFlag;
        log.Debug("szPathFlag" + szPathFlag);
        htData = GetData(arr);
        string path = "";
        string Imgid = "";
        ArrayList arrImgId = new ArrayList();
        foreach (DictionaryEntry var in htData)
        {
            path = var.Value.ToString();
            path = szBasePath + path;
            Imgid = var.Key.ToString();
            arrImgId.Add(Imgid);
        }
        log.Debug("path" + path);
        //string path = arrNodePath[0].ToString();
        path = path.Replace("\\", "/");
        log.Debug("path" + path);
        //szPath = szBasePath + path;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllBytes(path + "\\" + arr.sz_FileName, arr.sz_File);


        return arrImgId;

    }

    public Hashtable GetData(Bill_Sys_UploadFile _obj)
    {
        log.Debug(" In GetData");
        sqlCon = new SqlConnection(strsqlCon);
        Bill_Sys_UploadFile obj = new Bill_Sys_UploadFile();
        ArrayList arrBillNo = new ArrayList();
        ArrayList arrCaseId = new ArrayList();
        ArrayList arrSpe = new ArrayList();

        string szRetImg = "";
        string szRetPath = "";
        string szOldCase = "";
        Hashtable htData = new Hashtable();

        arrCaseId = _obj.sz_case_id;

        arrBillNo = _obj.sz_bill_no;
        arrSpe = _obj.sz_speciality_id;
        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrCaseId.Count; i++)
            {

                sqlCmd = new SqlCommand("sp_get_upload_document_node_list", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@sz_case_id", arrCaseId[i].ToString());
                log.Debug("  @sz_case_id " + arrCaseId[i].ToString());
                sqlCmd.Parameters.AddWithValue("@sz_process", _obj.sz_flag);
                log.Debug("  @sz_process " + _obj.sz_flag);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", _obj.sz_company_id);
                log.Debug("  @sz_company_id " + _obj.sz_company_id);
                sqlCmd.Parameters.AddWithValue("@sz_file_name", _obj.sz_FileName);
                log.Debug("  @sz_file_name " + _obj.sz_FileName);
                sqlCmd.Parameters.AddWithValue("@sz_user_name", _obj.sz_UserName);
                log.Debug("  @sz_user_name " + _obj.sz_UserName);
                sqlCmd.Parameters.AddWithValue("@sz_bill_no", arrBillNo[i].ToString());
                log.Debug("  @sz_bill_no " + arrBillNo[i].ToString());
                sqlCmd.Parameters.AddWithValue("@i_status_code", _obj.sz_StatusCode);
                log.Debug("  @i_status_code " + _obj.sz_StatusCode);
                //sqlCmd.Parameters.AddWithValue("@sz_veri_flag", _obj.sz_veri_flag);
                sqlCmd.Parameters.AddWithValue("@sz_user_id", _obj.sz_UserId);
                log.Debug("  @sz_user_id " + _obj.sz_UserId);
                sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", _obj.sz_payment_id);
                log.Debug("  @SZ_PAYMENT_ID " + _obj.sz_payment_id);
                sqlCmd.Parameters.AddWithValue("@sz_specialty_id", arrSpe[i].ToString());
                log.Debug("  @sz_specialty_id " + arrSpe[i].ToString());
                if (_obj.sz_PathFlag != null && _obj.sz_PathFlag != "")
                {
                    sqlCmd.Parameters.AddWithValue("@sz_path_flag", _obj.sz_PathFlag);
                }

                SqlParameter paramPath = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 255);
                SqlParameter paramImg = new SqlParameter("@i_image_id", SqlDbType.Int);

                paramPath.Direction = ParameterDirection.Output;
                paramImg.Direction = ParameterDirection.Output;

                sqlCmd.Parameters.Add(paramPath);
                sqlCmd.Parameters.Add(paramImg);
                sqlCmd.CommandTimeout = 0;
                log.Debug("ExecuteNonQuery ");
                sqlCmd.ExecuteNonQuery();
                log.Debug("ExecuteNonQuery  done");
                szRetPath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                szRetImg = sqlCmd.Parameters["@i_image_id"].Value.ToString();
                log.Debug("szRetPath  " + szRetPath);
                log.Debug("szRetImg  " + szRetImg);
                //arrPath.Add(szRetPath);
                try
                {
                    if (szRetImg != "" && szRetImg != null)
                    {
                        htData.Add(szRetImg, szRetPath);
                    }

                }
                catch(Exception ex1)
                {
                    log.Debug("error" + ex1.Message.ToString());

                }
                //arrImgId.Add(szRetImg);
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
        log.Debug("return htData");
        return htData;
       
    }

    public string GetPath(int I_IMAGE_ID)
    {
        SqlDataReader dr;
        sqlCon = new SqlConnection(strsqlCon);
        string iReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATH_FROM_IMAGEID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", I_IMAGE_ID);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                iReturn = dr["FilePath"].ToString();
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
        return iReturn;
    }


    public void InvoiceUploadFile(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);

        Bill_Sys_SoftwareInVoice_UploadFile obj = new Bill_Sys_SoftwareInVoice_UploadFile();

        SqlTransaction transaction;

        string szRetImg = "";
        string szRetPath = "";
        string szOldCase = "";

        sqlCon.Open();

        transaction = sqlCon.BeginTransaction();
        try
        {
            ArrayList arrFlg = new ArrayList();
            Bill_Sys_Verification_Desc objVerification_Desc;
            string sz_Type = "";


            for (int i = 0; i < arr.Count; i++)
            {
                int iFalg = 0;

                obj = (Bill_Sys_SoftwareInVoice_UploadFile)arr[i];

                for (int j = 0; j < arrFlg.Count; j++)
                {
                    Bill_Sys_SoftwareInVoice_UploadFile obj1 = new Bill_Sys_SoftwareInVoice_UploadFile();
                    obj1 = (Bill_Sys_SoftwareInVoice_UploadFile)arrFlg[j];


                    sqlCmd = new SqlCommand("SP_CHECK_NODE", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", obj.sz_bill_no);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@SZ_PROCESS", "BILL");

                    dr = sqlCmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sz_Type = Convert.ToString(dr.GetValue(0).ToString());
                    }

                    dr.Close();


                    if (sz_Type == "NFVER")
                    {
                        if (obj1.sz_case_id == obj.sz_case_id)
                        {
                            obj.i_ImageId = obj1.i_ImageId;
                            obj.sz_Logical_Path = obj1.sz_Logical_Path;
                            iFalg = 1;


                        }

                    }
                    else
                    {
                        if (obj1.sz_case_id == obj.sz_case_id && obj1.sz_speciality_id == obj.sz_speciality_id)
                        {
                            obj.i_ImageId = obj1.i_ImageId;
                            obj.sz_Logical_Path = obj1.sz_Logical_Path;
                            iFalg = 1;


                        }
                    }



                }

                if (iFalg == 0)
                {

                    sqlCmd = new SqlCommand("sp_get_upload_software_invoice_payment", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", obj.sz_case_id);
                    sqlCmd.Parameters.AddWithValue("@sz_process", obj.sz_flag);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", obj.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", obj.sz_FileName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", obj.sz_UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_bill_no", obj.sz_bill_no);
                    sqlCmd.Parameters.AddWithValue("@i_status_code", obj.sz_StatusCode);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", obj.sz_UserId);
                    sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", obj.sz_speciality_id);


                    if (obj.sz_PathFlag != null && obj.sz_PathFlag != "")
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", obj.sz_PathFlag);
                    }

                    SqlParameter paramPath = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 255);
                    SqlParameter paramImg = new SqlParameter("@i_image_id", SqlDbType.Int);

                    paramPath.Direction = ParameterDirection.Output;
                    paramImg.Direction = ParameterDirection.Output;

                    sqlCmd.Parameters.Add(paramPath);
                    sqlCmd.Parameters.Add(paramImg);
                    sqlCmd.Parameters.AddWithValue("@sz_PhysicalFilePath", obj.sz_File_PhysicalPath);
                    sqlCmd.ExecuteNonQuery();

                    szRetPath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    szRetImg = sqlCmd.Parameters["@i_image_id"].Value.ToString();
                    obj.sz_Logical_Path = szRetPath;
                    obj.i_ImageId = szRetImg;

                    arrFlg.Add(obj);   //add obj in new arraylist

                }


                sqlCmd = new SqlCommand("SP_TXN_BILL_SOFTWARE_INVOICE_PAYMENT_IMAGES", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", obj.sz_bill_no);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.sz_company_id);
                sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", obj.i_ImageId);
                sqlCmd.Parameters.AddWithValue("@SZ_CREATED_USER_ID", obj.sz_UserId);
                sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
                sqlCmd.Parameters.AddWithValue("@sz_logical_file_path", obj.sz_Logical_Path);
                sqlCmd.Parameters.AddWithValue("@sz_FileName", obj.sz_FileName);
                sqlCmd.Parameters.AddWithValue("@sz_filePhysical_path", obj.sz_File_PhysicalPath);

                sqlCmd.ExecuteNonQuery();



            }

            sqlCmd = new SqlCommand("SP_GET_INVOICE_PAYMENT_FILE_PATH_USING_PAYMNET_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
            sqlCmd.Parameters.AddWithValue("@SZ_FILENAME", obj.sz_FileName);
            sqlCmd.Parameters.AddWithValue("@SZ_FILEPATH", obj.sz_File_PhysicalPath);

            sqlCmd.ExecuteNonQuery();

            transaction.Commit();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }

    }

    public void StorageInvoiceUploadFile(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        Bill_Sys_SoftwareInVoice_UploadFile obj = new Bill_Sys_SoftwareInVoice_UploadFile();

        SqlTransaction transaction;

        string szRetImg = "";
        string szRetPath = "";
        string szOldCase = "";

        sqlCon.Open();

        transaction = sqlCon.BeginTransaction();
        try
        {
            ArrayList arrFlg = new ArrayList();
            string sz_Type = "";


            for (int i = 0; i < arr.Count; i++)
            {
                int iFalg = 0;

                obj = (Bill_Sys_SoftwareInVoice_UploadFile)arr[i];

                for (int j = 0; j < arrFlg.Count; j++)
                {
                    Bill_Sys_SoftwareInVoice_UploadFile obj1 = new Bill_Sys_SoftwareInVoice_UploadFile();
                    obj1 = (Bill_Sys_SoftwareInVoice_UploadFile)arrFlg[j];

                    sqlCmd = new SqlCommand("SP_CHECK_NODE", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", obj.sz_bill_no);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@SZ_PROCESS", "BILL");

                    dr = sqlCmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sz_Type = Convert.ToString(dr.GetValue(0).ToString());
                    }

                    dr.Close();


                    if (sz_Type == "NFVER")
                    {
                        if (obj1.sz_case_id == obj.sz_case_id)
                        {
                            obj.i_ImageId = obj1.i_ImageId;
                            obj.sz_Logical_Path = obj1.sz_Logical_Path;
                            iFalg = 1;


                        }

                    }
                    else
                    {
                        if (obj1.sz_case_id == obj.sz_case_id && obj1.sz_speciality_id == obj.sz_speciality_id)
                        {
                            obj.i_ImageId = obj1.i_ImageId;
                            obj.sz_Logical_Path = obj1.sz_Logical_Path;
                            iFalg = 1;


                        }
                    }




                }

                if (iFalg == 0)
                {

                    sqlCmd = new SqlCommand("sp_get_upload_storage_invoice_payment", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", obj.sz_case_id);
                    sqlCmd.Parameters.AddWithValue("@sz_process", obj.sz_flag);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", obj.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@sz_file_name", obj.sz_FileName);
                    sqlCmd.Parameters.AddWithValue("@sz_user_name", obj.sz_UserName);
                    sqlCmd.Parameters.AddWithValue("@sz_bill_no", obj.sz_bill_no);
                    sqlCmd.Parameters.AddWithValue("@i_status_code", obj.sz_StatusCode);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", obj.sz_UserId);
                    sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
                    sqlCmd.Parameters.AddWithValue("@sz_specialty_id", obj.sz_speciality_id);


                    if (obj.sz_PathFlag != null && obj.sz_PathFlag != "")
                    {
                        sqlCmd.Parameters.AddWithValue("@sz_path_flag", obj.sz_PathFlag);
                    }

                    SqlParameter paramPath = new SqlParameter("@sz_file_path", SqlDbType.NVarChar, 255);
                    SqlParameter paramImg = new SqlParameter("@i_image_id", SqlDbType.Int);

                    paramPath.Direction = ParameterDirection.Output;
                    paramImg.Direction = ParameterDirection.Output;

                    sqlCmd.Parameters.Add(paramPath);
                    sqlCmd.Parameters.Add(paramImg);
                    sqlCmd.Parameters.AddWithValue("@sz_PhysicalFilePath", obj.sz_File_PhysicalPath);
                    sqlCmd.ExecuteNonQuery();

                    szRetPath = sqlCmd.Parameters["@sz_file_path"].Value.ToString();
                    szRetImg = sqlCmd.Parameters["@i_image_id"].Value.ToString();
                    obj.sz_Logical_Path = szRetPath;
                    obj.i_ImageId = szRetImg;

                    arrFlg.Add(obj);   //add obj in new arraylist

                }


                sqlCmd = new SqlCommand("SP_TXN_BILL_STORAGE_INVOICE_PAYMENT_IMAGES", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", obj.sz_bill_no);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.sz_company_id);
                sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", obj.i_ImageId);
                sqlCmd.Parameters.AddWithValue("@SZ_CREATED_USER_ID", obj.sz_UserId);
                sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
                sqlCmd.Parameters.AddWithValue("@sz_logical_file_path", obj.sz_Logical_Path);
                sqlCmd.Parameters.AddWithValue("@sz_FileName", obj.sz_FileName);
                sqlCmd.Parameters.AddWithValue("@sz_filePhysical_path", obj.sz_File_PhysicalPath);

                sqlCmd.ExecuteNonQuery();



            }

            sqlCmd = new SqlCommand("SP_GET_STORAGE_INVOICE_PAYMENT_FILE_PATH_USING_PAYMNET_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", obj.sz_payment_id);
            sqlCmd.Parameters.AddWithValue("@SZ_FILENAME", obj.sz_FileName);
            sqlCmd.Parameters.AddWithValue("@SZ_FILEPATH", obj.sz_File_PhysicalPath);

            sqlCmd.ExecuteNonQuery();

            transaction.Commit();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();

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
