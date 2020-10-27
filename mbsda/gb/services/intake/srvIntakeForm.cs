using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using gb.mbs.da.service.util;
using gb.mbs.da.model.common;
using System.Configuration;
using System.IO;
using gbmodel = gb.mbs.da.model;
using gb.mbs.da.dbconstant;
using gb.mbs.da.common;
//using Newtonsoft.Json;
using gb.mbs.da.model.intake;

namespace gb.mbs.da.service.intake
{
    public class SrvIntakeForm
    {
        // the code here is commented because newtonsoft cannot be used in GB projects due to version conflict.
        // when the code is to be changed for webapi or doc module uncomment the below functions.
        // when the code is to be changed for gb mbs module comment all the functions and reference to newtonsoft


        //#region intake create
        //public void create(gbmodel.user.User oUser,gbmodel.intake.Intake oIntakeReq)            
        //{
        //    SqlConnection sqlConnection = null;
        //    SqlTransaction tran = null;
        //    string CaseStatusID = "";
        //    string sPatientID = "";
        //    string szPatientID = "";

        //    string sPolicyNo = "";
        //    string sWCBNO = "";

        //    gbmodel.intakeprovider.IntakeProvider oProvider = oIntakeReq.IntakeProvider;
        //    List<gbmodel.intake.IntakeForm> oList = oIntakeReq.FormField;
            
        //    gbmodel.patient.Patient oPat = new gbmodel.patient.Patient();
        //    gbmodel.intake.Intake oIntake = new gbmodel.intake.Intake();
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    sqlConnection.Open();
        //    tran = sqlConnection.BeginTransaction();

        //    #region new patient
        //    try
        //    {
        //        if (validationPatient(oList) == true)
        //        {
        //            SqlCommand sqlStatus = new SqlCommand();
        //            sqlStatus.CommandText = dbconstant.Procedures.PR_CASE_STATUS;
        //            sqlStatus.CommandType = CommandType.StoredProcedure;
        //            sqlStatus.Connection = sqlConnection;
        //            sqlStatus.Transaction = tran;
        //            sqlStatus.Parameters.Add(new SqlParameter("ID", oUser.Account.ID));
        //            sqlStatus.Parameters.Add(new SqlParameter("flag", "CASESTATUS_LIST"));
        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlStatus;
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);
        //            if (ds != null)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    DataRow[] dr = ds.Tables[0].Select("DESCRIPTION = 'OPEN'");
        //                    DataRow drRow = dr[0];
        //                    if (dr.Length > 0)
        //                    {
        //                        CaseStatusID = "" + drRow[0];
        //                    }
        //                    else
        //                    {
        //                        CaseStatusID = "" + drRow[0];
        //                    }
        //                }
        //            }

        //            //exec SP_MST_CASE_STATUS @flag='CASESTATUS_LIST' ,@ID='CO00023'


        //            // patient create logic                  
        //            gbmodel.employer.Employer oEmployer = new gbmodel.employer.Employer();
        //            gbmodel.casetype.CaseType oPatCaseType = new gbmodel.casetype.CaseType();
        //            Address oAddress = new Address();

        //            for (int i = 0; i < oList.Count; i++)
        //            {
        //                gbmodel.intake.IntakeForm oElement = oList[i];
        //                switch (oElement.Name)
        //                {
        //                    case "txtPatientFName":
        //                        oPat.FirstName = oElement.Value.ToString();
        //                        break;
        //                    case "txtPatientLName":
        //                        oPat.LastName = oElement.Value.ToString();
        //                        break;
        //                    case "dtDOB":
        //                        oPat.DOB = oElement.Value.ToString();
        //                        break;
        //                    case "dtDOA":
        //                        oPat.DOA = oElement.Value.ToString();
        //                        break;
        //                    case "txtHomePhone":
        //                        oPat.HomePhoneNumber = oElement.Value.ToString();
        //                        break;
        //                    case "txtSSN":
        //                        oPat.SSN = oElement.Value.ToString();
        //                        break;

        //                    case "txtMailingAddress":
        //                        oPat.Email = oElement.Value.ToString();
        //                        break;
        //                    case "txtCity":
        //                        oAddress.City = oElement.Value.ToString();
        //                        break;
        //                    case "txtState":
        //                        oAddress.State = oElement.Value.ToString();
        //                        break;
        //                    case "txtZip":
        //                        oAddress.Zip = oElement.Value.ToString();
        //                        break;
        //                    case "txtAge":
        //                        oPat.Age = Convert.ToInt32(oElement.Value.ToString());
        //                        break;
        //                    case "rdbSex":
        //                        oPat.Gender = oElement.Value.ToString();
        //                        break;
        //                    case "txtWorkPhone":
        //                        oPat.WorkPhone = oElement.Value.ToString();
        //                        break;
        //                    case "txtOccupation":
        //                        oPat.Occupation = oElement.Value.ToString();
        //                        break;

        //                    case "rdbCaseType":
        //                        oPatCaseType.ID = oElement.Value.ToString();
        //                        break;

        //                    //Carrier txtInsuranceCompany
        //                    case "txtPolicyNo":
        //                        sPolicyNo = oElement.Value.ToString();
        //                        break;
        //                    case "txtWCBNo":
        //                       sWCBNO = oElement.Value.ToString();
        //                        break;                           

        //                    //employer
        //                    case "txtEmployerName":
        //                        oEmployer.Name = oElement.Value.ToString();
        //                        break;
        //                    case "txtEmployerPhone":
        //                        oEmployer.PhoneNumber = oElement.Value.ToString();
        //                        break; 
        //                }
        //            }

        //            oPat.CaseType = oPatCaseType;
        //            oPat.Attorny = oIntakeReq.Attorney;
        //            oPat.Adjuster = oIntakeReq.Adjuster;
        //            oPat.Carrier = oIntakeReq.Carrier;                    
        //            oPat.Address = oAddress;
        //            oPat.Employer = oEmployer;                  

        //            SqlCommand sqlCommand = new SqlCommand();
        //            sqlCommand.CommandText = dbconstant.Procedures.PR_PATIENT_DATA_ENTRY;
        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.Connection = sqlConnection;
        //            sqlCommand.Transaction = tran;

        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_FIRST_NAME", oPat.FirstName));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_LAST_NAME", oPat.LastName));
        //            sqlCommand.Parameters.Add(new SqlParameter("I_PATIENT_AGE", oPat.Age));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_ADDRESS", oPat.Address.AddressLines + " " + oPat.Address.City + " " + oPat.Address.State + " " + oPat.Address.Zip));
        //            //sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_STREET", oUser.ID));

        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_CITY", oPat.Address.City));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_ZIP", oPat.Address.Zip));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_PHONE", oPat.HomePhoneNumber));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_PATIENT_EMAIL", oPat.Email));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_WORK_PHONE", oPat.WorkPhone));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_JOB_TITLE", oPat.Occupation));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_SOCIAL_SECURITY_NO", oPat.SSN));
        //            sqlCommand.Parameters.Add(new SqlParameter("DT_DOB", oPat.DOB));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_GENDER", oPat.Gender));
        //            sqlCommand.Parameters.Add(new SqlParameter("DT_INJURY", oPat.DOA));

        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_INSURANCE_ID", oPat.Carrier.Id));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_WCB_NO",sWCBNO));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_POLICY_NO", sPolicyNo));

        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_EMPLOYER_NAME", oPat.Employer.Name));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_EMPLOYER_PHONE", oPat.Employer.PhoneNumber));

        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_ATTORNEY_ID", oPat.Attorny.Id));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_ADJUSTER_ID", oPat.Adjuster.Id));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_POLICY_NUMBER", sPolicyNo));
        //            sqlCommand.Parameters.Add(new SqlParameter("DT_DATE_OF_ACCIDENT", oPat.DOA));
        //            //SZ_CASE_STATUS_ID
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_CASE_STATUS_ID", CaseStatusID)); // hardcoded

        //            sqlCommand.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //            sqlCommand.Parameters.Add(new SqlParameter("SZ_COMPANY_ID", oUser.Account.ID));
        //            sqlCommand.Parameters.Add(new SqlParameter("FLAG", "ADD"));

        //            SqlParameter outPutParameter = new SqlParameter();
        //            outPutParameter.ParameterName = "@out_patient_id";
        //            outPutParameter.SqlDbType = System.Data.SqlDbType.BigInt;
        //            outPutParameter.Direction = System.Data.ParameterDirection.Output;


        //            SqlParameter outPutParameter2 = new SqlParameter();
        //            outPutParameter2.ParameterName = "@out_sz_patient_id";
        //            outPutParameter2.Size = 25;
        //            outPutParameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        //            outPutParameter2.Direction = System.Data.ParameterDirection.Output;
                    

        //            sqlCommand.Parameters.Add(outPutParameter);
        //            sqlCommand.Parameters.Add(outPutParameter2);

        //            sqlCommand.ExecuteNonQuery();
        //            sPatientID = outPutParameter.Value.ToString();
        //            szPatientID = outPutParameter2.Value.ToString();
        //            //write logic to add patient                   
        //        }
        //        else
        //        {
        //            tran.Rollback();
        //            throw new ApplicationException(string.Format("some mandatory fields are not supplied"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        tran.Rollback();
        //    }

        //    #endregion new patient
        //    //Logic for intake
        //    #region new Code
        //    try
        //    {                
        //        string jsonStr = JsonConvert.SerializeObject(oList);              
        //        //need to be correct as per new DB
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_INTAKE_INSERT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", sPatientID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", szPatientID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oIntakeReq.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.Transaction = tran;
        //        sqlCommandInsert.ExecuteNonQuery();
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        tran.Rollback();
        //    }
           
        //    #endregion new Code

        //}

        //public bool validationPatient(List<gbmodel.intake.IntakeForm> oList)
        //{
        //    for (int i = 0; i < oList.Count; i++)
        //    {
        //        gbmodel.intake.IntakeForm oElement = oList[i];
        //        switch (oElement.Name)
        //        {
        //            case "txtPatientFName":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "txtPatientLName":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "dtDOB":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "dtDOA":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "txtHomePhone":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "txtSSN":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "txtInsuranceCompany":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;
        //                break;
        //            case "txtAttorneyName":
        //                if (oElement.Value.ToString() == "" || oElement.Value == null)
        //                    return false;

        //                break;
        //        }
        //    }

        //    return true;
        //}
        
        //#endregion intake create

        //#region sign
        //public string[] addsign(byte[] buffer, string sEventID, string sCompanyID, string sFlag)
        //{
        //    //TODO :need to develope new logic 

        //    SqlConnection sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    SqlDataAdapter da = null;
        //    DataSet ds = null;
        //    try
        //    {
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = new SqlCommand(dbconstant.Procedures.PR_INTAKE_DOCUMENT, sqlConnection); // TODO: Add procedure here
        //        sqlCommand.CommandType = CommandType.StoredProcedure;
        //        sqlCommand.Parameters.AddWithValue("@i_event_id", sEventID);
        //        sqlCommand.Parameters.AddWithValue("@sz_company_id", sCompanyID);
        //        da = new SqlDataAdapter(sqlCommand);
        //        ds = new DataSet();
        //        da.Fill(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //        {
        //            sqlConnection.Close();
        //        }
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //        {
        //            sqlConnection.Close();
        //        }
        //    }

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        string sFileName = null;
        //        string sFolderName = null;

        //        string sUploadRoot = System.Configuration.ConfigurationManager.AppSettings["SignPath"].ToString();
        //        sFolderName = ds.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString() + @"\" + ds.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() + @"\" + "Signs";
        //        if (sFlag == "dt")
        //            sFileName = sUploadRoot + sFolderName + @"\" + sEventID + "_DoctorSign.jpg";
        //        else
        //            sFileName = sUploadRoot + sFolderName + @"\" + sEventID + "_PatientSign.jpg";

        //        if (!System.IO.Directory.Exists((sUploadRoot + sFolderName)))
        //        {
        //            System.IO.Directory.CreateDirectory((sUploadRoot + sFolderName));
        //        }

        //        FileStream stream = new FileStream(sFileName, FileMode.Create, FileAccess.ReadWrite);
        //        BinaryWriter writer = new BinaryWriter(stream);
        //        writer.Write(buffer);
        //        writer.Close();
        //        stream.Close();

        //        //logic to add signature to the Db
        //        string sProcedureName = ""; //TODO: procedure Name....add procedure after DB changes  and confirmation frrom amod sir


        //        if (sFlag == "pat")
        //            sProcedureName = "sp_mst_patient";
        //        if (sFlag == "doc")
        //            sProcedureName = "sp_mst_doctor";


        //        if (SignValidation(sFileName))
        //        {
        //            try
        //            {
        //                sqlConnection.Open();
        //                SqlCommand sqlCommand = new SqlCommand(sProcedureName, sqlConnection);
        //                sqlCommand.CommandType = CommandType.StoredProcedure;
        //                sqlCommand.Parameters.AddWithValue("@i_event_id", sEventID);
        //                if (sFlag.ToLower() == "dt")
        //                {
        //                    sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sFileName);
        //                    sqlCommand.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHDOCTOR");
        //                }
        //                else
        //                {
        //                    sqlCommand.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sFileName);
        //                    sqlCommand.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHPATIENT");
        //                }

        //                sqlCommand.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (sqlConnection.State == ConnectionState.Open)
        //                {
        //                    sqlConnection.Close();
        //                }
        //                throw ex;
        //            }
        //            finally
        //            {
        //                if (sqlConnection.State == ConnectionState.Open)
        //                {
        //                    sqlConnection.Close();
        //                }
        //            }
        //        }
        //        string[] sFileProperties = new string[2];
        //        sFileProperties[0] = sFileName;
        //        sFileProperties[1] = @"MBSScans\" + sFolderName;
        //        return sFileProperties;
        //    }
        //    else
        //        throw new da.dataaccess.exception.NoDataFoundException("some values are missing for this event id");
        //}

        //public string parseFilePath(string p_PathName)
        //{
        //    string sFilename = null;
        //    if (p_PathName != null)
        //    {
        //        if (p_PathName.Contains("\\") || p_PathName.Contains("/"))
        //        {
        //            int i = p_PathName.LastIndexOf('\\');
        //            if (i != 0)
        //            {
        //                sFilename = p_PathName.Substring(i, p_PathName.Length - i);
        //            }
        //            else
        //            {
        //                i = p_PathName.LastIndexOf('/');
        //                if (i != 0)
        //                {
        //                    sFilename = p_PathName.Substring(i, p_PathName.Length - i);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            sFilename = p_PathName;
        //        }
        //    }

        //    if (sFilename != null)
        //    {
        //        sFilename = sFilename.Replace("\\", "");
        //        sFilename = sFilename.Replace("/", "");
        //    }
        //    return sFilename;
        //}

        //public bool SignValidation(string Path)
        //{
        //    if (System.IO.File.Exists(Path))
        //    {
        //        FileInfo file = new FileInfo(Path);
        //        long oFileLength = file.Length;
        //        long oSize = Convert.ToInt64(ConfigurationManager.AppSettings["signsize"].ToString());
        //        if (oFileLength > oSize)
        //        {
        //            return true;
        //        }
        //        else
        //            throw new da.dataaccess.exception.NoDataFoundException("File size must be greater than 1KB.");
        //    }
        //    else
        //        throw new da.dataaccess.exception.NoDataFoundException("Sign path file not exist.");

        //}

        //#endregion sign        


        //public List<gbmodel.intake.IntakeForm> Select(gbmodel.user.User oUser, gbmodel.intake.IntakeForm oForm)
        //{
        //    SqlConnection sqlConnection = null;
        //    List<gbmodel.intake.IntakeForm> oList = new List<gbmodel.intake.IntakeForm>();
        //    try
        //    {
        //        sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = null;
        //        sqlCommand = new SqlCommand("Procedure name", sqlConnection);//TODO : add procedure name  // Procedures.PR_INTAKE_SELECT
        //        sqlCommand.CommandType = CommandType.StoredProcedure;
        //        sqlCommand.Parameters.Add(new SqlParameter("@id", oUser.Account.ID));
        //        sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "LIST")); //TODO
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                DataRow dr = ds.Tables[0].Rows[i];
        //                gbmodel.intake.IntakeForm oElement = new gbmodel.intake.IntakeForm();
        //                oElement.Name = dr["name"].ToString(); //TODO
        //                oElement.Value = dr["value"].ToString(); //TODO:Addd db field inside []
        //                oList.Add(oElement);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //    }
        //    return oList;
        //}

        //public IsPregnant ResolvePregnantData(string pregnantModeString)
        //{
        //    if (pregnantModeString != null)
        //    {
        //        switch (pregnantModeString.ToLower())
        //        {
        //            case "1":
        //                return IsPregnant.Yes;
        //            case "2":
        //                return IsPregnant.Not;
        //            case "3":
        //                return IsPregnant.MayBe;
        //            default:
        //                throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", pregnantModeString));
        //        }
        //    }
        //    throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", pregnantModeString));
        //}        

        //public MaritalStatusType ResolveMaritialStatus(string MaritialStatusString)
        //{
        //    if (MaritialStatusString != null)
        //    {
        //        switch (MaritialStatusString.ToLower())
        //        {
        //            case "1":
        //                return MaritalStatusType.Married;
        //            case "2":
        //                return MaritalStatusType.UnMarried;
        //            case "3":
        //                return MaritalStatusType.Divocee;
        //            case "4":
        //                return MaritalStatusType.Widow;
        //            default:
        //                throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", MaritialStatusString));
        //        }
        //    }
        //    throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", MaritialStatusString));
        //}


        //public void CreateHipaa(gbmodel.user.User oUser, gbmodel.hippa.Hipaa oHipaa)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oHipaa.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oHipaa.FormField);
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_HIPPA_ALL, sqlConnection); 
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oHipaa.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oHipaa.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();                
 
        //    }            
 
        //}

        //public void CreateAob(gbmodel.user.User oUser, gbmodel.aob.AOB oAOB)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oAOB.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oAOB.FormField);
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_AOB_ALL, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oAOB.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oAOB.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();               
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }  


        //}

        //public void CreateLien(gbmodel.user.User oUser, gbmodel.Lien.Lien oLien)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oLien.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oLien.FormField);
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_LIEN_ALL, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oLien.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oLien.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }  
        //}        

        //public void CreateDeclaration(gbmodel.user.User oUser, gbmodel.declaration.Declaration oDeclaration)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oDeclaration.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oDeclaration.FormField);
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_DECLARATION, sqlConnection); 
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oDeclaration.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oDeclaration.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch(Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();               
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }  
        //}

        //public void CreateConset(gbmodel.user.User oUser, gbmodel.consetmirror.ConsetMirror oConsetMirror)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oConsetMirror.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oConsetMirror.FormField); // Add as per logic
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_CONSET_ALL, sqlConnection); 
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oConsetMirror.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oConsetMirror.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void CreateInformedConset(gbmodel.user.User oUser, gbmodel.informedconset.InformedConset oInformedConset)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oInformedConset.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oInformedConset.FormField); // Add as per logic
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_CONSET_INFORMED_ALL, sqlConnection); 
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oInformedConset.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oInformedConset.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void CreateOCA(gbmodel.user.User oUser, gbmodel.oca.OCA oOCA)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oOCA.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oOCA.FormField); // Add as per logic
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_OCA_CREATE, sqlConnection);                 
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oOCA.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oOCA.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void CreateNYS(gbmodel.user.User oUser, gbmodel.nys.NYS oNYS)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oNYS.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oNYS.FormField); // Add as per logic
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_NYS_CREATE, sqlConnection); 
        //        //SqlCommand sqlCommandInsert = new SqlCommand("", sqlConnection); 

        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oNYS.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oNYS.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void CreateEmployeeClaim(gbmodel.user.User oUser, gbmodel.employeeclaim.EmployeeCliam oEmpClaim)
        //{
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oEmpClaim.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oEmpClaim.FormField); // Add as per logic
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_EMPLOYEECLAIM_CREATE, sqlConnection); 
        //        //SqlCommand sqlCommandInsert = new SqlCommand("", sqlConnection); 

        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oEmpClaim.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oEmpClaim.IntakeProvider.Id));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("s_data", jsonStr));
        //        sqlCommandInsert.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}
        
        ////static lien demo
        //public SelectRequestData SelectLien(gbmodel.user.User oUser, gbmodel.Lien.Lien oLien)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oLien.Patient;
        //        string jsonStr = JsonConvert.SerializeObject(oPat);
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_LIEN_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oLien.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oLien.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
                      
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.MailingAddress = row["sz_patient_address"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no lien data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectHipaa(gbmodel.user.User oUser, gbmodel.hippa.Hipaa oHipaa)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oHipaa.Patient;              
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_HIPAA_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oHipaa.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oHipaa.IntakeProvider.Id));                               
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                   
                       
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no hipaa data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectAob(gbmodel.user.User oUser, gbmodel.aob.AOB oAOB)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oAOB.Patient;                
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_AOB_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oAOB.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oAOB.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.MailingAddress = row["sz_patient_address"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;

        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no aob data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectDeclaration(gbmodel.user.User oUser, gbmodel.declaration.Declaration oDeclaration)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oDeclaration.Patient;              
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_DECLARATION_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oDeclaration.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oDeclaration.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.FirstName = row["sz_patient_first_name"].ToString();
        //                oPatient.LastName = row["sz_patient_last_name"].ToString();
        //                oPatient.SSN = row["SSN"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no declaration data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectConset(gbmodel.user.User oUser, gbmodel.consetmirror.ConsetMirror oConsetMirror)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oConsetMirror.Patient;               
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_CONSET_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oConsetMirror.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oConsetMirror.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no conset data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectInformedConset(gbmodel.user.User oUser, gbmodel.informedconset.InformedConset oInformedConset)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oInformedConset.Patient;             
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_CONSET_INFORMED_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oInformedConset.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oInformedConset.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no conset informed data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectOCA(gbmodel.user.User oUser, gbmodel.oca.OCA oOCA)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oOCA.Patient;              
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_OCA_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oOCA.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oOCA.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                    
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.MailingAddress = row["sz_patient_address"].ToString();            
        //                oPatient.SSN = row["SSN"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no OCA data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectNYS(gbmodel.user.User oUser, gbmodel.nys.NYS oNYS)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oNYS.Patient;            
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_NYS_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oNYS.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oNYS.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
             
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();                        
        //                oPatient.FirstName = row["sz_patient_first_name"].ToString();
        //                oPatient.LastName = row["sz_patient_last_name"].ToString();
        //                oPatient.SSN = row["SSN"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                Address oAddres = new Address();
                        
        //                oAddres.City = row["sz_patient_city"].ToString();
        //                oAddres.State = row["sz_patient_state"].ToString();
        //                oAddres.Zip = row["sz_patient_zip"].ToString();
        //                oPatient.Address = oAddres;

        //                oPatient.DOB = row["dt_dob"].ToString();
        //                oPatient.Gender = row["sz_gender"].ToString();
        //                oPatient.HomePhoneNumber = row["sz_patient_phone"].ToString();
        //                oPatient.WorkPhone = row["sz_work_phone"].ToString();
        //                oPatient.PolicyNumber = row["sz_policy_no"].ToString();
        //                oData.Patient = oPatient;


        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no NYS data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectEmployeeClaim(gbmodel.user.User oUser, gbmodel.employeeclaim.EmployeeCliam oEmpClaim)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oEmpClaim.Patient;               
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_C3_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oEmpClaim.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oEmpClaim.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;
                 
        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();                       
        //                oPatient.WCBNumber = row["sz_wcb_no"].ToString();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oData.Patient = oPatient;


        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no c3 data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}

        //public SelectRequestData SelectIntake(gbmodel.user.User oUser, gbmodel.intakereq.IntakeReq oIntake)
        //{
        //    SelectRequestData oData = new SelectRequestData();
        //    List<IntakeForm> oList = new List<IntakeForm>();
        //    SqlConnection sqlConnection = null;
        //    sqlConnection = new SqlConnection(DBUtil.ConnectionString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        gbmodel.patient.Patient oPat = oIntake.Patient;
        //        SqlCommand sqlCommandInsert = new SqlCommand(dbconstant.Procedures.PR_INTAKE_SELECT, sqlConnection);
        //        sqlCommandInsert.CommandType = CommandType.StoredProcedure;
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_company_id", oUser.Account.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_user_id", oUser.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("bi_patient_id", oPat.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_patient_id", oPat.Patient_ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("sz_case_type_id", oIntake.CaseType.ID));
        //        sqlCommandInsert.Parameters.Add(new SqlParameter("i_intake_provider_id", oIntake.IntakeProvider.Id));
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommandInsert);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds.Tables[0].Rows[0];
        //                string sdata = row["s_data"].ToString();
        //                oList = JsonConvert.DeserializeObject<List<IntakeForm>>(sdata);
        //                oData.FormField = oList;

        //                gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
        //                oPatient.ID = oPat.ID;
        //                oPatient.Patient_ID = oPat.Patient_ID;
        //                oPatient.Name = row["sz_patient_name"].ToString();
        //                oPatient.WCBNumber = row["sz_wcb_no"].ToString();
        //                oPatient.FirstName = row["sz_patient_first_name"].ToString();
        //                oPatient.LastName = row["sz_patient_last_name"].ToString();
        //                oPatient.SSN = row["SSN"].ToString();
        //                oPatient.DOA = row["DOA"].ToString();
        //                Address oAddres = new Address();

        //                oAddres.City = row["sz_patient_city"].ToString();
        //                oAddres.State = row["sz_patient_state"].ToString();
        //                oAddres.Zip = row["sz_patient_zip"].ToString();
        //                oPatient.Address = oAddres;

        //                oPatient.DOB = row["dt_dob"].ToString();
        //                oPatient.Gender = row["sz_gender"].ToString();
        //                oPatient.HomePhoneNumber = row["sz_patient_phone"].ToString();
        //                oPatient.WorkPhone = row["sz_work_phone"].ToString();
        //                oPatient.PolicyNumber = row["sz_policy_no"].ToString();
        //                oData.Patient = oPatient;

        //                gbmodel.intakeprovider.IntakeProvider oProvider = new gbmodel.intakeprovider.IntakeProvider();
        //                oProvider.Name = row["sz_name"].ToString();
        //                oProvider.Address = row["sz_address"].ToString();
        //                oProvider.Phone = row["sz_phone"].ToString();
        //                oProvider.City = row["sz_city"].ToString();

        //                oData.IntakeProvider = oProvider;
        //            }
        //            else
        //                throw new Exception("please cheack inputs or patient has no intake data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == ConnectionState.Open)
        //            sqlConnection.Close();

        //    }
        //    return oData;
        //}
    }

}

