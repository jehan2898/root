using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.IO;
using System.Data.SqlClient;
//using testXFAItextSharp.DAL;
/// <summary>
/// Summary description for Bill_Sys_C4DataBind
/// </summary>
namespace testXFAItextSharp.DAO
{
    public class Bill_Sys_C4DataBind
    {
        public Bill_Sys_C4DataBind()
        {
            //
            // TODO: Add constructor logic here
            //
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
        }
        
        public DataSet GetData(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            conn.Open();
            ds = new DataSet();
            try
            {
                try
                {
                    if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                    {
                        comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_SEC", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                    else
                    {
                        comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                }
                catch
                {
                    comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW", conn);
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                }
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "ALL");
                da = new SqlDataAdapter(comm);
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
            //Method End

        }
        public DataSet GetDataForTest(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            conn.Open();
            ds = new DataSet();
            try
            {
                try
                {
                    if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                    {
                        comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_TEST_SEC", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                    else
                    {
                        comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_TEST", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                }
                catch { comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_TEST", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                }
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "ALL");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }

        //Mohan
        public DataSet Get_Service_Table(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW", conn);

                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "SERVICE_TABLE");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }
        public DataSet Get_Patient_Complaints(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "PATIENT_COMPLAINTS");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }
        public DataSet Get_Patient_TypeInjury(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "PATIENT_TYPE_INJURY");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }
        //Nirmal
        public DataSet Get_PATIENT_PHYSICAL_EXAM(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "PATIENT_PHYSICAL_EXAM");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }

        public DataSet Get_DIAGNOSTIC_TESTS(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "DIAGNOSTIC_TESTS");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }

        public DataSet Get_REFERRALS(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "REFERRALS");
                da = new SqlDataAdapter(comm);
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
            //Method End

        }

        public DataSet Get_ASSISTIVE_DEVICES(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "ASSISTIVE_DEVICES");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }

        public DataSet Get_WORK_STATUS(string sz_BillNumber)
        {
            DataSet ds;
            SqlCommand comm;
            SqlDataAdapter da;

            string strConn = "";
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

            SqlConnection conn;
            conn = new SqlConnection(strConn);
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);

            conn.Open();
            ds = new DataSet();
            try
            {
                comm = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
                comm.Parameters.AddWithValue("@FLAG", "WORK_STATUS");
                da = new SqlDataAdapter(comm);
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
            //Method End


        }

        //public Bill_Sys_C4DAO GetPDFData()
        //{
        //    C4Data objDL = new C4Data();
        //    string szBillNo = "mo1316";
        //    DataSet ds = objDL.GetData(szBillNo);

        //    Bill_Sys_C4DAO PdfDAO = new Bill_Sys_C4DAO();
        //    PdfDAO = bindObject(ds);
        //    return PdfDAO;
        //}
        public Bill_Sys_C4DAO bindObject1(string outPutFilePath, string szBillNo, DataSet ds)
        {
            Bill_Sys_C4DAO PdfDAO = new Bill_Sys_C4DAO();
            PdfDAO.patientHomePhone1 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString();//SZ_PATIENT_FIRST_NAME,SZ_PATIENT_LAST_NAME,SZ_MI,SZ_SOCIAL_SECURITY_NO
            PdfDAO.patientHomePhone2 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString();//SZ_PATIENT_STREET,SZ_PATIENT_CITY,SZ_PATIENT_STATE,SZ_PATIENT_ZIP,SZ_WCB_NO
            PdfDAO.FirstName = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
            PdfDAO.LastName = ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
            PdfDAO.middleInitial = ds.Tables[0].Rows[0]["SZ_MI"].ToString();
            PdfDAO.socialSecNumber = ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
            PdfDAO.patientAddress = ds.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString();
            PdfDAO.patientCity = ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            PdfDAO.patientState = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
            PdfDAO.patientZip = ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
            PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
            PdfDAO.SZ_CARRIER_CODE = ds.Tables[0].Rows[0]["SZ_CARRIER_CODE"].ToString();
            //PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
            PdfDAO.carrierCaseNo = ds.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
            PdfDAO.patientAddress = ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
            PdfDAO.patientCity = ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            PdfDAO.patientState = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
            PdfDAO.patientZip = ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
            PdfDAO.accidentDate = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString();
            PdfDAO.accidentMonth = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString();
            PdfDAO.accidentYear = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString();

            if (ds.Tables[0].Rows[0]["DOB_MM"].ToString() == "0")
            {
                PdfDAO.DOBDate = "";
            }
            else
            {
                PdfDAO.DOBDate = ds.Tables[0].Rows[0]["DOB_MM"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DOB_DD"].ToString() == "0")
            {
                PdfDAO.DOBMonth = "";
            }
            else
            {
                PdfDAO.DOBMonth = ds.Tables[0].Rows[0]["DOB_DD"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DOB_YY"].ToString() == "0")
            {
                PdfDAO.DOBYear = "";
            }
            else
            {
                PdfDAO.DOBYear = ds.Tables[0].Rows[0]["DOB_YY"].ToString();
            }

            //PdfDAO.DOBMonth = ds.Tables[0].Rows[0]["DOB_MM"].ToString();
            //PdfDAO.DOBYear = ds.Tables[0].Rows[0]["DOB_YY"].ToString();
            PdfDAO.jobTitle = ds.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
            PdfDAO.workActivity = ds.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES1"].ToString();
            PdfDAO.workActivity1 = ds.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES2"].ToString();
            PdfDAO.patientAccountNo = ds.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString();
            PdfDAO.employerName = ds.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
            PdfDAO.employerPhone1 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString();
            PdfDAO.employerPhone2 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString();
            PdfDAO.SZ_EMPLOYER_ADDRESS = ds.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
            PdfDAO.SZ_EMPLOYER_CITY = ds.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
            PdfDAO.SZ_EMPLOYER_STATE = ds.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
            PdfDAO.SZ_EMPLOYER_ZIP = ds.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
            PdfDAO.employerPhone1 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString();
            PdfDAO.employerPhone2 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString();

            PdfDAO.SZ_DOCTOR_NAME = ds.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString();
            PdfDAO.SZ_WCB_AUTHORIZATION = ds.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
            PdfDAO.SZ_WCB_RATING_CODE = ds.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString();
            PdfDAO.SZ_FEDERAL_TAX_ID = ds.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString();
            PdfDAO.SZ_OFFICE_ADDRESS = ds.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString();
            PdfDAO.SZ_OFFICE_CITY = ds.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString();
            PdfDAO.SZ_OFFICE_STATE = ds.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString();
            PdfDAO.SZ_OFFICE_ZIP = ds.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString();


            PdfDAO.SZ_BILLING_GROUP_NAME = ds.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString();
            PdfDAO.SZ_BILLING_ADDRESS = ds.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString();
            PdfDAO.SZ_BILLING_CITY = ds.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString();
            PdfDAO.SZ_BILLING_STATE = ds.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString();
            PdfDAO.SZ_BILLING_ZIP = ds.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString();
            PdfDAO.SZ_OFFICE_PHONE1 = ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString();
            PdfDAO.SZ_OFFICE_PHONE2 = ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString();
            PdfDAO.SZ_BILLING_PHONE1 = ds.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString();
            PdfDAO.SZ_BILLING_PHONE2 = ds.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString();
            PdfDAO.SZ_NPI = ds.Tables[0].Rows[0]["SZ_NPI"].ToString();
            PdfDAO.SZ_INSURANCE_NAME = ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
            PdfDAO.SZ_INSURANCE_STREET = ds.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();
            PdfDAO.SZ_INSURANCE_CITY = ds.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
            PdfDAO.SZ_INSURANCE_STATE = ds.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
            PdfDAO.SZ_INSURANCE_ZIP = ds.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
            PdfDAO.SZ_DIAGNOSIS_CODE = ds.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString();
            //PdfDAO.SZ_BILL_ID = szBillNo;


            if (ds.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                PdfDAO.chkMale = "1";
                PdfDAO.chkFemale = "0";
            }
            else if (ds.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                PdfDAO.chkMale = "0";
                PdfDAO.chkFemale = "1";
            }
            else
            {
                PdfDAO.chkMale = "0";
                PdfDAO.chkFemale = "0";
            }

            if (ds.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                PdfDAO.chk_EIN = "0";
                PdfDAO.chk_SSN = "1";
            }
            if (ds.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                PdfDAO.chk_EIN = "1";
                PdfDAO.chk_SSN = "0";
            }
            else
            {
                PdfDAO.chk_EIN = "0";
                PdfDAO.chk_SSN = "0";
            }
            if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                PdfDAO.chkPhysician = "1";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "0";
            }
            else if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "1";
                PdfDAO.chkChiropractor = "0";
            }
            else if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "1";
            }
            else
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "0";
            }
            return PdfDAO;
        }
        public Bill_Sys_C4DAO bindObject(string outPutFilePath,string szBillNo, DataSet ds, DataSet ds_Service_Table, DataSet ds_Patient_Complaints, DataSet ds_Patient_TypeInjury, DataSet ds_Patient_Exam, DataSet ds_Diagnosis_Test, DataSet ds_Referral, DataSet ds_Assistive_Device, DataSet ds_Work_Status)
        {
            Bill_Sys_C4DAO PdfDAO = new Bill_Sys_C4DAO();
            PdfDAO.patientHomePhone1 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString();//SZ_PATIENT_FIRST_NAME,SZ_PATIENT_LAST_NAME,SZ_MI,SZ_SOCIAL_SECURITY_NO
            PdfDAO.patientHomePhone2 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString();//SZ_PATIENT_STREET,SZ_PATIENT_CITY,SZ_PATIENT_STATE,SZ_PATIENT_ZIP,SZ_WCB_NO
            PdfDAO.FirstName = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
            PdfDAO.LastName = ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
            PdfDAO.middleInitial = ds.Tables[0].Rows[0]["SZ_MI"].ToString();
            PdfDAO.socialSecNumber = ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
            PdfDAO.patientAddress = ds.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString();
            PdfDAO.patientCity = ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            PdfDAO.patientState = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
            PdfDAO.patientZip = ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
            PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
            PdfDAO.SZ_CARRIER_CODE = ds.Tables[0].Rows[0]["SZ_CARRIER_CODE"].ToString();
            //PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
            PdfDAO.carrierCaseNo = ds.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
            PdfDAO.patientAddress = ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
            PdfDAO.patientCity = ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            PdfDAO.patientState = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
            PdfDAO.patientZip = ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
            PdfDAO.accidentDate = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString();
            PdfDAO.accidentMonth = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString();
            PdfDAO.accidentYear = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString();
            PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();

            if (ds.Tables[0].Rows[0]["DOB_MM"].ToString() == "0")
            {
                PdfDAO.DOBDate = "";
            }
            else
            {
                PdfDAO.DOBDate = ds.Tables[0].Rows[0]["DOB_MM"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DOB_DD"].ToString() == "0")
            {
                PdfDAO.DOBMonth = "";
            }
            else
            {
                PdfDAO.DOBMonth = ds.Tables[0].Rows[0]["DOB_DD"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DOB_YY"].ToString() == "0")
            {
                PdfDAO.DOBYear = "";
            }
            else
            {
                PdfDAO.DOBYear = ds.Tables[0].Rows[0]["DOB_YY"].ToString();
            }
            //PdfDAO.DOBDate = ds.Tables[0].Rows[0]["DOB_DD"].ToString();
            //PdfDAO.DOBMonth = ds.Tables[0].Rows[0]["DOB_MM"].ToString();
            //PdfDAO.DOBYear = ds.Tables[0].Rows[0]["DOB_YY"].ToString();
            PdfDAO.jobTitle = ds.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
            PdfDAO.workActivity = ds.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES1"].ToString();
            PdfDAO.workActivity1 = ds.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES2"].ToString();
            PdfDAO.patientAccountNo = ds.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString();
            PdfDAO.employerName = ds.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
            PdfDAO.employerPhone1 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString();
            PdfDAO.employerPhone2 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString();
            PdfDAO.SZ_EMPLOYER_ADDRESS = ds.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
            PdfDAO.SZ_EMPLOYER_CITY = ds.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
            PdfDAO.SZ_EMPLOYER_STATE = ds.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
            PdfDAO.SZ_EMPLOYER_ZIP = ds.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
            PdfDAO.employerPhone1 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString();
            PdfDAO.employerPhone2 = ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString();

            PdfDAO.SZ_DOCTOR_NAME = ds.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString();
            PdfDAO.SZ_WCB_AUTHORIZATION = ds.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
            PdfDAO.SZ_WCB_RATING_CODE = ds.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString();
            PdfDAO.SZ_FEDERAL_TAX_ID = ds.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString();
            PdfDAO.SZ_OFFICE_ADDRESS = ds.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString();
            PdfDAO.SZ_OFFICE_CITY = ds.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString();
            PdfDAO.SZ_OFFICE_STATE = ds.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString();
            PdfDAO.SZ_OFFICE_ZIP = ds.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString();


            PdfDAO.SZ_BILLING_GROUP_NAME = ds.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString();
            PdfDAO.SZ_BILLING_ADDRESS = ds.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString();
            PdfDAO.SZ_BILLING_CITY = ds.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString();
            PdfDAO.SZ_BILLING_STATE = ds.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString();
            PdfDAO.SZ_BILLING_ZIP = ds.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString();
            PdfDAO.SZ_OFFICE_PHONE1 = ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString();
            PdfDAO.SZ_OFFICE_PHONE2 = ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString();
            PdfDAO.SZ_BILLING_PHONE1 = ds.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString();
            PdfDAO.SZ_BILLING_PHONE2 = ds.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString();
            PdfDAO.SZ_NPI = ds.Tables[0].Rows[0]["SZ_NPI"].ToString();
            PdfDAO.SZ_INSURANCE_NAME = ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
            PdfDAO.SZ_INSURANCE_STREET = ds.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();
            PdfDAO.SZ_INSURANCE_CITY = ds.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
            PdfDAO.SZ_INSURANCE_STATE = ds.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
            PdfDAO.SZ_INSURANCE_ZIP = ds.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
            PdfDAO.SZ_DIAGNOSIS_CODE = ds.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString();
            PdfDAO.SZ_BILL_ID = szBillNo;

            PdfDAO.SZ_INJURY_ILLNESS_DETAIL = ds.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL"].ToString();
            PdfDAO.SZ_INJURY_ILLNESS_DETAIL_1 = ds.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_1"].ToString();
            PdfDAO.SZ_INJURY_ILLNESS_DETAIL_2 = ds.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_2"].ToString();
            PdfDAO.SZ_INJURY_LEARN_SOURCE_DESCRIPTION = ds.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_DESCRIPTION"].ToString();
            PdfDAO.SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION = ds.Tables[0].Rows[0]["SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION"].ToString();
            PdfDAO.DT_PREVIOUSLY_TREATED_DATE = ds.Tables[0].Rows[0]["DT_PREVIOUSLY_TREATED_DATE"].ToString();
            PdfDAO.DT_DATE_OF_EXAMINATION = ds.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString();
            PdfDAO.SZ_DIAGNOSTIC_TEST = ds.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST"].ToString();
            PdfDAO.SZ_DIAGNOSTIC_TEST_1 = ds.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST_1"].ToString();
            PdfDAO.SZ_TREATMENT = ds.Tables[0].Rows[0]["SZ_TREATMENT"].ToString();
            PdfDAO.SZ_TREATMENT_1 = ds.Tables[0].Rows[0]["SZ_TREATMENT_1"].ToString();
            PdfDAO.SZ_PROGNOSIS_RECOVERY = ds.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY"].ToString();
            PdfDAO.SZ_PROGNOSIS_RECOVERY_1 = ds.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_1"].ToString();
            PdfDAO.SZ_PROGNOSIS_RECOVERY_2 = ds.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_2"].ToString();
            PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION = ds.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION"].ToString();
            PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1 = ds.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1"].ToString();
            PdfDAO.FT_TEMPORARY_IMPAIRMENT_PERCENTAGE = ds.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString();
            PdfDAO.SZ_TEST_RESULTS = ds.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString();
            PdfDAO.SZ_TEST_RESULTS_1 = ds.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString();
            PdfDAO.SZ_PROPOSED_TREATEMENT = ds.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT"].ToString();
            PdfDAO.SZ_PROPOSED_TREATEMENT_1 = ds.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_1"].ToString();
            PdfDAO.SZ_PROPOSED_TREATEMENT_3 = ds.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_3"].ToString();
            PdfDAO.SZ_MEDICATIONS_PRESCRIBED = ds.Tables[0].Rows[0]["SZ_MEDICATIONS_PRESCRIBED"].ToString();
            PdfDAO.SZ_MEDICATIONS_ADVISED = ds.Tables[0].Rows[0]["SZ_MEDICATIONS_ADVISED"].ToString();
            PdfDAO.SZ_MEDICATION_RESTRICTIONS_DESCRIPTION = ds.Tables[0].Rows[0]["SZ_MEDICATION_RESTRICTIONS_DESCRIPTION"].ToString();
            PdfDAO.Text226 = ds.Tables[0].Rows[0]["Text226"].ToString();

            if (ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString()=="0")
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_DD = "";
            }
            else
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_DD = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString() == "0")
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_MM = "";
            }
            else
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_MM = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString();
            }
            if (ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString() == "0")
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_YY = "";
            }
            else
            {
                PdfDAO.DT_PATIENT_MISSED_WORK_DATE_YY = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString();
            }
            //PdfDAO.DT_PATIENT_MISSED_WORK_DATE_DD = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString();
            //PdfDAO.DT_PATIENT_MISSED_WORK_DATE_MM = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString();
            //PdfDAO.DT_PATIENT_MISSED_WORK_DATE_YY = ds.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString();
            PdfDAO.SZ_PATIENT_RETURN_WORK_DESCRIPTION = "";
            PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD = "";
            PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM = "";
            PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY = "";
            PdfDAO.Text249_DD = "";
            PdfDAO.Text249_MM = "";
            PdfDAO.Text249_YY = "";
            if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                PdfDAO.SZ_PATIENT_RETURN_WORK_DESCRIPTION = ds.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString();
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString() == "0")
                {
                    PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD = "";
                    PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM = "";
                    PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY = "";
                }
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD = ds.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString();
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM = ds.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString();
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY = ds.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString();
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                PdfDAO.Text249_DD = ds.Tables[0].Rows[0]["Text249_MM"].ToString();
                PdfDAO.Text249_MM = ds.Tables[0].Rows[0]["Text249_DD"].ToString();
                PdfDAO.Text249_YY = ds.Tables[0].Rows[0]["Text249_YY"].ToString();
            }
            else
            {
                PdfDAO.SZ_PATIENT_RETURN_WORK_DESCRIPTION ="";
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD = "";
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM = "";
                PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY = "";
                PdfDAO.Text249_DD = "";
                PdfDAO.Text249_MM = "";
                PdfDAO.Text249_YY = "";

            }
            
            PdfDAO.SZ_LIMITATION_DURATION = ds.Tables[0].Rows[0]["SZ_LIMITATION_DURATION"].ToString();
            PdfDAO.SZ_QUANIFY_LIMITATION = ds.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION"].ToString();
            PdfDAO.SZ_QUANIFY_LIMITATION_1 = ds.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_1"].ToString();
            PdfDAO.SZ_QUANIFY_LIMITATION_2 = ds.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_2"].ToString();
            PdfDAO.SZ_OFFICE_NAME = ds.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString();
            PdfDAO.SZ_SPECIALITY = ds.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString();
            PdfDAO.SZ_READINGDOCTOR = ds.Tables[0].Rows[0]["SZ_READINGDOCTOR"].ToString();
            PdfDAO.SZ_PHYSICIAN_SIGN = ds.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString();
            PdfDAO.txtTodayDate_DD = ds.Tables[0].Rows[0]["txtTodayDate_MM"].ToString();
            PdfDAO.txtTodayDate_MM = ds.Tables[0].Rows[0]["txtTodayDate_DD"].ToString();
            PdfDAO.txtTodayDate_YY = ds.Tables[0].Rows[0]["txtTodayDate_YY"].ToString();


            if (ds.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                PdfDAO.chkMale = "1";
                PdfDAO.chkFemale = "0";
            }
            else if (ds.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                PdfDAO.chkMale = "0";
                PdfDAO.chkFemale = "1";
            }
            else
            {
                PdfDAO.chkMale = "0";
                PdfDAO.chkFemale = "0";
            }

            if (ds.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                PdfDAO.chk_EIN = "0";
                PdfDAO.chk_SSN = "1";
            }
            if (ds.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                PdfDAO.chk_EIN = "1";
                PdfDAO.chk_SSN = "0";
            }
            else
            {
                PdfDAO.chk_EIN = "0";
                PdfDAO.chk_SSN = "0";
            }

            if (ds.Tables[0].Rows[0]["BT_PPO"].ToString() == "1")
            {
                PdfDAO.chkService = "1";
            }
            else
            {
                PdfDAO.chkService = "0";
            }
            if (ds.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "0")
            {
                PdfDAO.chk_H_Patient = "1";
                PdfDAO.chk_H_MedicalRecords = "0";
                PdfDAO.chk_H_Other = "0";
            }
            else if (ds.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "1")
            {
                PdfDAO.chk_H_Patient = "0";
                PdfDAO.chk_H_MedicalRecords = "1";
                PdfDAO.chk_H_Other = "0";
            }
            else if (ds.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "2")
            {
                PdfDAO.chk_H_Patient = "0";
                PdfDAO.chk_H_MedicalRecords = "0";
                PdfDAO.chk_H_Other = "1";
            }
            else
            {
                PdfDAO.chk_H_Patient = "0";
                PdfDAO.chk_H_MedicalRecords = "0";
                PdfDAO.chk_H_Other = "0";
            }

            if (ds.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "0")
            {
                PdfDAO.chk_H_Yes = "1";
                PdfDAO.chk_H_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "1")
            {
                PdfDAO.chk_H_Yes = "0";
                PdfDAO.chk_H_No = "1";
            }
            else
            {
                PdfDAO.chk_H_Yes = "0";
                PdfDAO.chk_H_No = "0";
            }

            if (ds.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Yes = "1";
                PdfDAO.chk_H_4_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Yes = "0";
                PdfDAO.chk_H_4_No = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Yes = "0";
                PdfDAO.chk_H_4_No = "0";
            }

            if (ds.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "0")
            {
                PdfDAO.chk_F_8_Yes = "1";
                PdfDAO.chk_F_8_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "1")
            {
                PdfDAO.chk_F_8_Yes = "0";
                PdfDAO.chk_F_8_No = "1";
            }
            else
            {
                PdfDAO.chk_F_8_Yes = "0";
                PdfDAO.chk_F_8_No = "0";
            }

            if (ds.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                PdfDAO.chk_G_1_Yes = "1";
                PdfDAO.chk_G_1_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                PdfDAO.chk_G_1_Yes = "0";
                PdfDAO.chk_G_1_No = "1";
            }
            else
            {
                PdfDAO.chk_G_1_Yes = "0";
                PdfDAO.chk_G_1_No = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                PdfDAO.chk_G_2_Yes = "1";
                PdfDAO.chk_G_2_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                PdfDAO.chk_G_2_Yes = "0";
                PdfDAO.chk_G_2_No = "1";
            }
            else
            {
                PdfDAO.chk_G_2_Yes = "0";
                PdfDAO.chk_G_2_No = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                PdfDAO.chk_G_3_Yes = "1";
                PdfDAO.chk_G_3_No = "0";
                PdfDAO.chk_G_3_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                PdfDAO.chk_G_3_Yes = "0";
                PdfDAO.chk_G_3_No = "1";
                PdfDAO.chk_G_3_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                PdfDAO.chk_G_3_Yes = "0";
                PdfDAO.chk_G_3_No = "0";
                PdfDAO.chk_G_3_NA = "1";
            }
            else
            {
                PdfDAO.chk_G_3_Yes = "0";
                PdfDAO.chk_G_3_No = "0";
                PdfDAO.chk_G_3_NA = "0";
            }
            if ((ds.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString()).ToUpper() == "FALSE")
            {
                PdfDAO.chk_H_2_Other = "0";
                PdfDAO.chk_H_2_None = "1";
            }
            else if( (ds.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString()).ToUpper() == "TRUE")
            {
                PdfDAO.chk_H_2_Other = "1";
                PdfDAO.chk_H_2_None = "0";
            }
            else
            {
                PdfDAO.chk_H_2_Other = "0";
                PdfDAO.chk_H_2_None = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Yes = "1";
                PdfDAO.chk_H_3_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Yes = "0";
                PdfDAO.chk_H_3_No = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Yes = "0";
                PdfDAO.chk_H_3_No = "0";
            }

            if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "0")
            {
                PdfDAO.chk_H_5_WithinWeek = "1";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "1")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "1";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "2")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "1";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "3")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "1";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "4")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "1";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "5")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "1";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = ds.Tables[0].Rows[0]["SZ_APPOINTMENT"].ToString();

            }
            else if (ds.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "6")
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "1";
                PdfDAO.txt_H_5_Month = "";
            }
            else
            {
                PdfDAO.chk_H_5_WithinWeek = "0";
                PdfDAO.chk_H_5_1_2_Week = "0";
                PdfDAO.chk_H_5_3_4_Week = "0";
                PdfDAO.chk_H_5_5_6_Week = "0";
                PdfDAO.chk_H_5_7_8_Week = "0";
                PdfDAO.chk_H_5_Month = "0";
                PdfDAO.chk_H_5_Return = "0";
                PdfDAO.txt_H_5_Month = "";
            }

            //if (ds.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "0")
            //{
            //    PdfDAO.chk_H_8_Yes = "1";
            //    PdfDAO.chk_H_8_No = "0";
            //}
            //else if (ds.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "1")
            //{
            //    PdfDAO.chk_H_8_Yes = "0";
            //    PdfDAO.chk_H_8_No = "1";
            //}

            if (ds.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "0")
            {
                PdfDAO.chk_I_1_Yes = "1";
                PdfDAO.chk_I_1_No = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "1")
            {
                PdfDAO.chk_I_1_Yes = "0";
                PdfDAO.chk_I_1_No = "1";
            }
            else
            {
                PdfDAO.chk_I_1_Yes = "0";
                PdfDAO.chk_I_1_No = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "0")
            {
                PdfDAO.chk_I_1_PatientWorkingYes = "1";
                PdfDAO.chk_I_1_PatientWorkingNo = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "1")
            {
                PdfDAO.chk_I_1_PatientWorkingYes = "0";
                PdfDAO.chk_I_1_PatientWorkingNo = "1";
            }
            else
            {
                PdfDAO.chk_I_1_PatientWorkingYes = "0";
                PdfDAO.chk_I_1_PatientWorkingNo = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "0")
            {
                PdfDAO.chk_I_1_PatientReturnYes = "1";
                PdfDAO.chk_I_1_PatientReturnNo = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "1")
            {
                PdfDAO.chk_I_1_PatientReturnYes = "0";
                PdfDAO.chk_I_1_PatientReturnNo = "1";
            }
            else
            {
                PdfDAO.chk_I_1_PatientReturnYes = "0";
                PdfDAO.chk_I_1_PatientReturnNo = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                PdfDAO.chk_H_Provider_1 = "1";
                PdfDAO.chk_H_Provider_2 = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                PdfDAO.chk_H_Provider_1 = "0";
                PdfDAO.chk_H_Provider_2 = "1";
            }
            else
            {
                PdfDAO.chk_H_Provider_1 = "0";
                PdfDAO.chk_H_Provider_2 = "0";
            }
            if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                PdfDAO.chk_I_2_a = "1";
                PdfDAO.chk_I_2_b = "0";
                PdfDAO.chk_I_2_c = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                PdfDAO.chk_I_2_a = "0";
                PdfDAO.chk_I_2_b = "1";
                PdfDAO.chk_I_2_c = "0";
            }
            else if (ds.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                PdfDAO.chk_I_2_a = "0";
                PdfDAO.chk_I_2_b = "0";
                PdfDAO.chk_I_2_c = "1";
            }
            else
            {
                PdfDAO.chk_I_2_a = "0";
                PdfDAO.chk_I_2_b = "0";
                PdfDAO.chk_I_2_c = "0";
            }
            if (ds.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Patient = "1";
                PdfDAO.chk_H_3_PatientsEmployer = "0";
                PdfDAO.chk_H_4_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Patient = "0";
                PdfDAO.chk_H_3_PatientsEmployer = "1";
                PdfDAO.chk_H_4_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                PdfDAO.chk_H_3_Patient = "0";
                PdfDAO.chk_H_3_PatientsEmployer = "0";
                PdfDAO.chk_H_4_NA = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Patient = "0";
                PdfDAO.chk_H_3_PatientsEmployer = "0";
                PdfDAO.chk_H_4_NA = "0";
            }


            if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                PdfDAO.chkPhysician = "1";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "0";
            }
            else if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "1";
                PdfDAO.chkChiropractor = "0";
            }
            else if (ds.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "1";
            }
            else
            {
                PdfDAO.chkPhysician = "0";
                PdfDAO.chkPodiatrist = "0";
                PdfDAO.chkChiropractor = "0";
            }

            if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                PdfDAO.chk_H_2_1_2_Days = "1";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "1";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "1";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "1";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "1";
                PdfDAO.chk_H_2_NA = "0";
            }
            else if (ds.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "1";
            }
            else
            {
                PdfDAO.chk_H_2_1_2_Days = "0";
                PdfDAO.chk_H_2_3_7_Days = "0";
                PdfDAO.chk_H_2_8_14_Days = "0";
                PdfDAO.chk_H_2_15P_Days = "0";
                PdfDAO.chk_H_2_Unknown = "0";
                PdfDAO.chk_H_2_NA = "0";
            }

            PdfDAO.DC_1 = "";
            PdfDAO.SZ_MODIFIER_B1 = "";
            PdfDAO.COB_1 = "";

            PdfDAO.FROM_MM_1 = "";
            PdfDAO.FROM_DD_1 = "";
            PdfDAO.FROM_YY_1 = "";
            PdfDAO.TO_MM_1 = "";
            PdfDAO.TO_DD_1 = "";
            PdfDAO.TO_YY_1 = "";
            PdfDAO.CPT_1 = "";
            PdfDAO.CHARGE_1 = "";
            PdfDAO.UNIT_1 = "";
            PdfDAO.ZIP_1 = "";
            PdfDAO.POS_1 = "";
            PdfDAO.MODIFIER_1 = "";
            PdfDAO.DC_2 = "";
            PdfDAO.SZ_MODIFIER_B2 = "";
            PdfDAO.COB_2 = "";

            PdfDAO.FROM_MM_2 = "";
            PdfDAO.FROM_DD_2 = "";
            PdfDAO.FROM_YY_2 = "";
            PdfDAO.TO_MM_2 = "";
            PdfDAO.TO_DD_2 = "";
            PdfDAO.TO_YY_2 = "";
            PdfDAO.CPT_2 = "";
            PdfDAO.CHARGE_2 = "";
            PdfDAO.UNIT_2 = "";
            PdfDAO.ZIP_2 = "";
            PdfDAO.POS_2 = "";
            PdfDAO.MODIFIER_2 = "";

            PdfDAO.DC_3 = "";
            PdfDAO.SZ_MODIFIER_B3 = "";
            PdfDAO.COB_3 = "";

            PdfDAO.FROM_MM_3 = "";
            PdfDAO.FROM_DD_3 = "";
            PdfDAO.FROM_YY_3 = "";
            PdfDAO.TO_MM_3 = "";
            PdfDAO.TO_DD_3 = "";
            PdfDAO.TO_YY_3 = "";
            PdfDAO.CPT_3 = "";
            PdfDAO.CHARGE_3 = "";
            PdfDAO.UNIT_3 = "";
            PdfDAO.ZIP_3 = "";
            PdfDAO.POS_3 = "";
            PdfDAO.MODIFIER_3 = "";

            PdfDAO.DC_4 = "";
            PdfDAO.SZ_MODIFIER_B4 = "";
            PdfDAO.COB_4 = "";

            PdfDAO.FROM_MM_4 = "";
            PdfDAO.FROM_DD_4 = "";
            PdfDAO.FROM_YY_4 = "";
            PdfDAO.TO_MM_4 = "";
            PdfDAO.TO_DD_4 = "";
            PdfDAO.TO_YY_4 = "";
            PdfDAO.CPT_4 = "";
            PdfDAO.CHARGE_4 = "";
            PdfDAO.UNIT_4 = "";
            PdfDAO.ZIP_4 = "";
            PdfDAO.POS_4 = "";
            PdfDAO.MODIFIER_4 = "";

            PdfDAO.DC_5 = "";
            PdfDAO.SZ_MODIFIER_B5 = "";
            PdfDAO.COB_5 = "";

            PdfDAO.FROM_MM_5 = "";
            PdfDAO.FROM_DD_5 = "";
            PdfDAO.FROM_YY_5 = "";
            PdfDAO.TO_MM_5 = "";
            PdfDAO.TO_DD_5 = "";
            PdfDAO.TO_YY_5 = "";
            PdfDAO.CPT_5 = "";
            PdfDAO.CHARGE_5 = "";
            PdfDAO.UNIT_5 = "";
            PdfDAO.ZIP_5 = "";
            PdfDAO.POS_5 = "";
            PdfDAO.MODIFIER_5 = "";

            PdfDAO.DC_6 = "";
            PdfDAO.SZ_MODIFIER_B6 = "";
            PdfDAO.COB_6 = "";

            PdfDAO.FROM_MM_6 = "";
            PdfDAO.FROM_DD_6 = "";
            PdfDAO.FROM_YY_6 = "";
            PdfDAO.TO_MM_6 = "";
            PdfDAO.TO_DD_6 = "";
            PdfDAO.TO_YY_6 = "";
            PdfDAO.CPT_6 = "";
            PdfDAO.CHARGE_6 = "";
            PdfDAO.UNIT_6 = "";
            PdfDAO.ZIP_6 = "";
            PdfDAO.POS_6 = "";
            PdfDAO.MODIFIER_6 = "";

            if (ds_Service_Table != null)
            {
                if (ds_Service_Table.Tables.Count > 0)
                {
                    if (ds_Service_Table.Tables[0].Rows.Count > 0)
                    {
                        PdfDAO.TOTAL_BILL_AMOUNT = ds_Service_Table.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
                        PdfDAO.TOTAL_PAID_AMOUNT = ds_Service_Table.Tables[0].Rows[0]["PAID_AMOUNT"].ToString();
                        PdfDAO.TOTAL_BAL_AMOUNT = ds_Service_Table.Tables[0].Rows[0]["BALANCE"].ToString();

                        PdfDAO.DC_1 = ds.Tables[0].Rows[0]["DC_1"].ToString();
                        PdfDAO.SZ_MODIFIER_B1 = "";
                        PdfDAO.COB_1 = "";

                        PdfDAO.FROM_MM_1 = ds_Service_Table.Tables[0].Rows[0]["MONTH"].ToString();
                        PdfDAO.FROM_DD_1 = ds_Service_Table.Tables[0].Rows[0]["DAY"].ToString();
                        PdfDAO.FROM_YY_1 = ds_Service_Table.Tables[0].Rows[0]["YEAR"].ToString();
                        PdfDAO.TO_MM_1 = ds_Service_Table.Tables[0].Rows[0]["TO_MONTH"].ToString();
                        PdfDAO.TO_DD_1 = ds_Service_Table.Tables[0].Rows[0]["TO_DAY"].ToString();
                        PdfDAO.TO_YY_1 = ds_Service_Table.Tables[0].Rows[0]["TO_YEAR"].ToString();
                        PdfDAO.CPT_1 = ds_Service_Table.Tables[0].Rows[0]["SZ_PROCEDURE_CODE"].ToString();
                        PdfDAO.CHARGE_1 = ds_Service_Table.Tables[0].Rows[0]["FL_AMOUNT"].ToString();
                        PdfDAO.UNIT_1 = ds_Service_Table.Tables[0].Rows[0]["I_UNIT"].ToString();
                        PdfDAO.ZIP_1 = ds_Service_Table.Tables[0].Rows[0]["ZIP_CODE"].ToString();
                        PdfDAO.POS_1 = ds_Service_Table.Tables[0].Rows[0]["PLACE_OF_SERVICE"].ToString();
                        PdfDAO.MODIFIER_1 = ds_Service_Table.Tables[0].Rows[0]["SZ_MODIFIER"].ToString();
                        if (ds_Service_Table.Tables[0].Rows.Count > 1)
                        {
                            PdfDAO.DC_2 = ds.Tables[0].Rows[0]["DC_2"].ToString();
                            PdfDAO.SZ_MODIFIER_B2 = "";
                            PdfDAO.COB_2 = "";

                            PdfDAO.FROM_MM_2 = ds_Service_Table.Tables[0].Rows[1]["MONTH"].ToString();
                            PdfDAO.FROM_DD_2 = ds_Service_Table.Tables[0].Rows[1]["DAY"].ToString();
                            PdfDAO.FROM_YY_2 = ds_Service_Table.Tables[0].Rows[1]["YEAR"].ToString();
                            PdfDAO.TO_MM_2 = ds_Service_Table.Tables[0].Rows[1]["TO_MONTH"].ToString();
                            PdfDAO.TO_DD_2 = ds_Service_Table.Tables[0].Rows[1]["TO_DAY"].ToString();
                            PdfDAO.TO_YY_2 = ds_Service_Table.Tables[0].Rows[1]["TO_YEAR"].ToString();
                            PdfDAO.CPT_2 = ds_Service_Table.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString();
                            PdfDAO.CHARGE_2 = ds_Service_Table.Tables[0].Rows[1]["FL_AMOUNT"].ToString();
                            PdfDAO.UNIT_2 = ds_Service_Table.Tables[0].Rows[1]["I_UNIT"].ToString();
                            PdfDAO.ZIP_2 = ds_Service_Table.Tables[0].Rows[1]["ZIP_CODE"].ToString();
                            PdfDAO.POS_2 = ds_Service_Table.Tables[0].Rows[1]["PLACE_OF_SERVICE"].ToString();
                            PdfDAO.MODIFIER_2 = ds_Service_Table.Tables[0].Rows[1]["SZ_MODIFIER"].ToString();

                            if (ds_Service_Table.Tables[0].Rows.Count > 2)
                            {
                                PdfDAO.DC_3 = ds.Tables[0].Rows[0]["DC_3"].ToString();
                                PdfDAO.SZ_MODIFIER_B3 = "";
                                PdfDAO.COB_3 = "";

                                PdfDAO.FROM_MM_3 = ds_Service_Table.Tables[0].Rows[2]["MONTH"].ToString();
                                PdfDAO.FROM_DD_3 = ds_Service_Table.Tables[0].Rows[2]["DAY"].ToString();
                                PdfDAO.FROM_YY_3 = ds_Service_Table.Tables[0].Rows[2]["YEAR"].ToString();
                                PdfDAO.TO_MM_3 = ds_Service_Table.Tables[0].Rows[2]["TO_MONTH"].ToString();
                                PdfDAO.TO_DD_3 = ds_Service_Table.Tables[0].Rows[2]["TO_DAY"].ToString();
                                PdfDAO.TO_YY_3 = ds_Service_Table.Tables[0].Rows[2]["TO_YEAR"].ToString();
                                PdfDAO.CPT_3 = ds_Service_Table.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString();
                                PdfDAO.CHARGE_3 = ds_Service_Table.Tables[0].Rows[2]["FL_AMOUNT"].ToString();
                                PdfDAO.UNIT_3 = ds_Service_Table.Tables[0].Rows[2]["I_UNIT"].ToString();
                                PdfDAO.ZIP_3 = ds_Service_Table.Tables[0].Rows[2]["ZIP_CODE"].ToString();
                                PdfDAO.POS_3 = ds_Service_Table.Tables[0].Rows[2]["PLACE_OF_SERVICE"].ToString();
                                PdfDAO.MODIFIER_3 = ds_Service_Table.Tables[0].Rows[2]["SZ_MODIFIER"].ToString();
                                if (ds_Service_Table.Tables[0].Rows.Count > 3)
                                {
                                    PdfDAO.DC_4 = ds.Tables[0].Rows[0]["DC_4"].ToString();
                                    PdfDAO.SZ_MODIFIER_B4 = "";
                                    PdfDAO.COB_4 = "";

                                    PdfDAO.FROM_MM_4 = ds_Service_Table.Tables[0].Rows[3]["MONTH"].ToString();
                                    PdfDAO.FROM_DD_4 = ds_Service_Table.Tables[0].Rows[3]["DAY"].ToString();
                                    PdfDAO.FROM_YY_4 = ds_Service_Table.Tables[0].Rows[3]["YEAR"].ToString();
                                    PdfDAO.TO_MM_4 = ds_Service_Table.Tables[0].Rows[3]["TO_MONTH"].ToString();
                                    PdfDAO.TO_DD_4 = ds_Service_Table.Tables[0].Rows[3]["TO_DAY"].ToString();
                                    PdfDAO.TO_YY_4 = ds_Service_Table.Tables[0].Rows[3]["TO_YEAR"].ToString();
                                    PdfDAO.CPT_4 = ds_Service_Table.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString();
                                    PdfDAO.CHARGE_4 = ds_Service_Table.Tables[0].Rows[3]["FL_AMOUNT"].ToString();
                                    PdfDAO.UNIT_4 = ds_Service_Table.Tables[0].Rows[3]["I_UNIT"].ToString();
                                    PdfDAO.ZIP_4 = ds_Service_Table.Tables[0].Rows[3]["ZIP_CODE"].ToString();
                                    PdfDAO.POS_4 = ds_Service_Table.Tables[0].Rows[3]["PLACE_OF_SERVICE"].ToString();
                                    PdfDAO.MODIFIER_4 = ds_Service_Table.Tables[0].Rows[3]["SZ_MODIFIER"].ToString();
                                    if (ds_Service_Table.Tables[0].Rows.Count > 4)
                                    {
                                        PdfDAO.DC_5 = ds.Tables[0].Rows[0]["DC_5"].ToString();
                                        PdfDAO.SZ_MODIFIER_B5 = "";
                                        PdfDAO.COB_5 = "";

                                        PdfDAO.FROM_MM_5 = ds_Service_Table.Tables[0].Rows[4]["MONTH"].ToString();
                                        PdfDAO.FROM_DD_5 = ds_Service_Table.Tables[0].Rows[4]["DAY"].ToString();
                                        PdfDAO.FROM_YY_5 = ds_Service_Table.Tables[0].Rows[4]["YEAR"].ToString();
                                        PdfDAO.TO_MM_5 = ds_Service_Table.Tables[0].Rows[4]["TO_MONTH"].ToString();
                                        PdfDAO.TO_DD_5 = ds_Service_Table.Tables[0].Rows[4]["TO_DAY"].ToString();
                                        PdfDAO.TO_YY_5 = ds_Service_Table.Tables[0].Rows[4]["TO_YEAR"].ToString();
                                        PdfDAO.CPT_5 = ds_Service_Table.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString();
                                        PdfDAO.CHARGE_5 = ds_Service_Table.Tables[0].Rows[4]["FL_AMOUNT"].ToString();
                                        PdfDAO.UNIT_5 = ds_Service_Table.Tables[0].Rows[4]["I_UNIT"].ToString();
                                        PdfDAO.ZIP_5 = ds_Service_Table.Tables[0].Rows[4]["ZIP_CODE"].ToString();
                                        PdfDAO.POS_5 = ds_Service_Table.Tables[0].Rows[4]["PLACE_OF_SERVICE"].ToString();
                                        PdfDAO.MODIFIER_5 = ds_Service_Table.Tables[0].Rows[4]["SZ_MODIFIER"].ToString();
                                        if (ds_Service_Table.Tables[0].Rows.Count > 5)
                                        {
                                            PdfDAO.DC_6 = ds.Tables[0].Rows[0]["DC_6"].ToString();
                                            PdfDAO.SZ_MODIFIER_B6 = "";
                                            PdfDAO.COB_6 = "";

                                            PdfDAO.FROM_MM_6 = ds_Service_Table.Tables[0].Rows[5]["MONTH"].ToString();
                                            PdfDAO.FROM_DD_6 = ds_Service_Table.Tables[0].Rows[5]["DAY"].ToString();
                                            PdfDAO.FROM_YY_6 = ds_Service_Table.Tables[0].Rows[5]["YEAR"].ToString();
                                            PdfDAO.TO_MM_6 = ds_Service_Table.Tables[0].Rows[5]["TO_MONTH"].ToString();
                                            PdfDAO.TO_DD_6 = ds_Service_Table.Tables[0].Rows[5]["TO_DAY"].ToString();
                                            PdfDAO.TO_YY_6 = ds_Service_Table.Tables[0].Rows[5]["TO_YEAR"].ToString();
                                            PdfDAO.CPT_6 = ds_Service_Table.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString();
                                            PdfDAO.CHARGE_6 = ds_Service_Table.Tables[0].Rows[5]["FL_AMOUNT"].ToString();
                                            PdfDAO.UNIT_6 = ds_Service_Table.Tables[0].Rows[5]["I_UNIT"].ToString();
                                            PdfDAO.ZIP_6 = ds_Service_Table.Tables[0].Rows[5]["ZIP_CODE"].ToString();
                                            PdfDAO.POS_6 = ds_Service_Table.Tables[0].Rows[5]["PLACE_OF_SERVICE"].ToString();
                                            PdfDAO.MODIFIER_6 = ds_Service_Table.Tables[0].Rows[5]["SZ_MODIFIER"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //DIAGNOSIC TEST

            if (ds_Diagnosis_Test.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_CTScan = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_CTScan = "1";
            }
            else
            {
                PdfDAO.chk_H_3_CTScan = "0";
            }
            //PdfDAO.txt_F_Numbness = ds_Diagnosis_Test.Tables[0].Rows[0]["SZ_DESCRIPTION"].ToString();

            if (ds_Diagnosis_Test.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_EMGNCS = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_EMGNCS = "1";
            }
            else
            {
                PdfDAO.chk_H_3_EMGNCS = "0";
            }
            //PdfDAO.txt_F_Swelling = ds_Diagnosis_Test.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            if (ds_Diagnosis_Test.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_MRI = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_MRI = "1";
            }
            else
            {
                PdfDAO.chk_H_3_MRI = "0";
            }
            PdfDAO.txt_H_3_MRI = ds_Diagnosis_Test.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            if (ds_Diagnosis_Test.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Labs = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Labs = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Labs = "0";
            }
            PdfDAO.txt_H_3_Labs = ds_Diagnosis_Test.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            if (ds_Diagnosis_Test.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_XRay = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_XRay = "1";
            }
            else
            {
                PdfDAO.chk_H_3_XRay = "0";
            }
            PdfDAO.txt_H_3_XRay = ds_Diagnosis_Test.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            if (ds_Diagnosis_Test.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Left_Other = "0";
            }
            else if (ds_Diagnosis_Test.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Left_Other = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Left_Other = "0";
            }
            PdfDAO.txt_H_3_Left_Other = ds_Diagnosis_Test.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();


            //END OF DIAGNOSTIC TEST

            //REFERRALS

            if (ds_Referral.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Chiropractor = "0";
            }
            else if (ds_Referral.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Chiropractor = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Chiropractor = "0";
            }
            //PdfDAO.txt_H_3_Left_Other = ds_Referral.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            if (ds_Referral.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Interist = "0";
            }
            else if (ds_Referral.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Interist = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Interist = "0";
            }

            if (ds_Referral.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Occupational = "0";
            }
            else if (ds_Referral.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Occupational = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Occupational = "0";
            }
            if (ds_Referral.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Physical = "0";
            }
            else if (ds_Referral.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Physical = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Physical = "0";
            }

            if (ds_Referral.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Specialist = "0";
            }
            else if (ds_Referral.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Specialist = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Specialist = "0";
            }
            PdfDAO.txt_H_3_Specialist = ds_Referral.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();

            if (ds_Referral.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_3_Right_Other = "0";
            }
            else if (ds_Referral.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_3_Right_Other = "1";
            }
            else
            {
                PdfDAO.chk_H_3_Right_Other = "0";
            }
            PdfDAO.txt_H_3_Right_Other = ds_Referral.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();

            //END OF REFERRALS

            //Assestive device

            if (ds_Assistive_Device.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Cane = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Cane = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Cane = "0";
            }
            //PdfDAO.txt_F_Numbness = ds_Diagnosis_Test.Tables[0].Rows[0]["SZ_DESCRIPTION"].ToString();

            if (ds_Assistive_Device.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Crutches = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Crutches = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Crutches = "0";
            }
            //PdfDAO.txt_F_Swelling = ds_Assistive_Device.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            if (ds_Assistive_Device.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Orthotics = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Orthotics = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Orthotics = "0";
            }
            //PdfDAO.txt_H_3_MRI = ds_Assistive_Device.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            if (ds_Assistive_Device.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Walker = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Walker = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Walker = "0";
            }
            //PdfDAO.txt_H_3_Labs = ds_Assistive_Device.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            if (ds_Assistive_Device.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_WheelChair = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_WheelChair = "1";
            }
            else
            {
                PdfDAO.chk_H_4_WheelChair = "0";
            }
            //PdfDAO.txt_H_3_x = ds_Assistive_Device.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            if (ds_Assistive_Device.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_H_4_Other = "0";
            }
            else if (ds_Assistive_Device.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_H_4_Other = "1";
            }
            else
            {
                PdfDAO.chk_H_4_Other = "0";
            }
            PdfDAO.txt_H_4_Other = ds_Assistive_Device.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            //END OF Assestive device

            //Work Status

            if (ds_Work_Status.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Bending = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Bending = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Bending = "0";
            }
            //PdfDAO.txt_F_Numbness = ds_Work_Status.Tables[0].Rows[0]["SZ_DESCRIPTION"].ToString();

            if (ds_Work_Status.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Lifting = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Lifting = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Lifting = "0";
            }
            //PdfDAO.txt_F_Swelling = ds_Work_Status.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            if (ds_Work_Status.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Sitting = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Sitting = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Sitting = "0";
            }
            //PdfDAO.txt_H_3_MRI = ds_Assistive_Device.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            if (ds_Work_Status.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Climbing = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Climbing = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Climbing = "0";
            }
            //PdfDAO.txt_H_3_Labs = ds_Work_Status.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            if (ds_Work_Status.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_OperatingHeavy = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_OperatingHeavy = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_OperatingHeavy = "0";
            }
            //PdfDAO.txt_H_3_x = ds_Work_Status.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            if (ds_Work_Status.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Standing = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Standing = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Standing = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_EnvironmentalCond = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_EnvironmentalCond = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_EnvironmentalCond = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_OpMotorVehicle = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_OpMotorVehicle = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_OpMotorVehicle = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_UsePublicTrans = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_UsePublicTrans = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_UsePublicTrans = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Kneeling = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Kneeling = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Kneeling = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_PersonalProtectiveEq = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_PersonalProtectiveEq = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_PersonalProtectiveEq = "0";
            }


            if (ds_Work_Status.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_UseOfUpperExtremeities = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_UseOfUpperExtremeities = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_UseOfUpperExtremeities = "0";
            }

            if (ds_Work_Status.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_I_2_C_Other = "0";
            }
            else if (ds_Work_Status.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_I_2_C_Other = "1";
            }
            else
            {
                PdfDAO.chk_I_2_C_Other = "0";
            }
            //PdfDAO.txt_H_4_Other = ds_Work_Status.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            //End of Work Status

            //Mohan


            if (ds_Patient_Complaints.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Numbness = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Numbness = "1";
            }
            else
            {
                PdfDAO.chk_F_Numbness = "0";
            }

            PdfDAO.txt_F_Numbness = ds_Patient_Complaints.Tables[0].Rows[0]["SZ_DESCRIPTION"].ToString();

            if (ds_Patient_Complaints.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Swelling = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Swelling = "1";
            }
            else
            {
                PdfDAO.chk_F_Swelling = "0";
            }
            PdfDAO.txt_F_Swelling = ds_Patient_Complaints.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            if (ds_Patient_Complaints.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Pain = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Pain = "1";
            }
            else
            {
                PdfDAO.chk_F_Pain = "0";
            }
            PdfDAO.txt_F_Pain = ds_Patient_Complaints.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            if (ds_Patient_Complaints.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Weakness = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Weakness = "1";
            }
            else
            {
                PdfDAO.chk_F_Weakness = "0";
            }
            PdfDAO.txt_F_Weakness = ds_Patient_Complaints.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            if (ds_Patient_Complaints.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Stiffness = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Stiffness = "1";
            }
            else
            {
                PdfDAO.chk_F_Stiffness = "0";
            }
            PdfDAO.txt_F_Stiffness = ds_Patient_Complaints.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            if (ds_Patient_Complaints.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_Other = "0";
            }
            else if (ds_Patient_Complaints.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_Other = "1";
            }
            else
            {
                PdfDAO.chk_F_Other = "0";
            }
            PdfDAO.txt_F_Other = ds_Patient_Complaints.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            //End PatientComplaint

            //PATIENT_TYPE_INJURY
            //1
            if (ds_Patient_TypeInjury.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Abrasion = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Abrasion = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Abrasion = "0";
            }
            PdfDAO.txt_F_3_Abrasion = ds_Patient_TypeInjury.Tables[0].Rows[0]["SZ_DESCRIPTION"].ToString();
            //2
            if (ds_Patient_TypeInjury.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_InfectiousDisease = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_InfectiousDisease = "1";
            }
            else
            {
                PdfDAO.chk_F_3_InfectiousDisease = "0";
            }
            PdfDAO.txt_F_3_InfectiousDisease = ds_Patient_TypeInjury.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            //3
            if (ds_Patient_TypeInjury.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Amputation = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Amputation = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Amputation = "0";
            }
            PdfDAO.txt_F_3_Amputation = ds_Patient_TypeInjury.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            //4
            if (ds_Patient_TypeInjury.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Inhalation_Exposure = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Inhalation_Exposure = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Inhalation_Exposure = "0";
            }
            PdfDAO.txt_F_3_Inhalation_Exposure = ds_Patient_TypeInjury.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            //5
            if (ds_Patient_TypeInjury.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Avulsion = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Avulsion = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Avulsion = "0";
            }
            PdfDAO.txt_F_3_Avulsion = ds_Patient_TypeInjury.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            //6
            if (ds_Patient_TypeInjury.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Laceration = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Laceration = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Laceration = "0";
            }
            PdfDAO.txt_F_3_Laceration = ds_Patient_TypeInjury.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            //7
            if (ds_Patient_TypeInjury.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Bite = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Bite = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Bite = "0";
            }
            PdfDAO.txt_F_3_Bite = ds_Patient_TypeInjury.Tables[0].Rows[6]["SZ_DESCRIPTION"].ToString();
            //8
            if (ds_Patient_TypeInjury.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_NeedleStick = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_NeedleStick = "1";
            }
            else
            {
                PdfDAO.chk_F_3_NeedleStick = "0";
            }
            PdfDAO.txt_F_3_NeedleStick = ds_Patient_TypeInjury.Tables[0].Rows[7]["SZ_DESCRIPTION"].ToString();
            //9
            if (ds_Patient_TypeInjury.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Burn = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Burn = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Burn = "0";
            }
            PdfDAO.txt_F_3_Burn = ds_Patient_TypeInjury.Tables[0].Rows[8]["SZ_DESCRIPTION"].ToString();
            //10
            if (ds_Patient_TypeInjury.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Poisoning = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Poisoning = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Poisoning = "0";
            }
            PdfDAO.txt_F_3_Poisoning = ds_Patient_TypeInjury.Tables[0].Rows[9]["SZ_DESCRIPTION"].ToString();
            //11
            if (ds_Patient_TypeInjury.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Contusion = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Contusion = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Contusion = "0";
            }
            PdfDAO.txt_F_3_Contusion = ds_Patient_TypeInjury.Tables[0].Rows[10]["SZ_DESCRIPTION"].ToString();
            //12
            if (ds_Patient_TypeInjury.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Psychological = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Psychological = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Psychological = "0";
            }
            PdfDAO.txt_F_3_Psychological = ds_Patient_TypeInjury.Tables[0].Rows[11]["SZ_DESCRIPTION"].ToString();
            //13
            if (ds_Patient_TypeInjury.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_CurshInjury = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_CurshInjury = "1";
            }
            else
            {
                PdfDAO.chk_F_3_CurshInjury = "0";
            }
            PdfDAO.txt_F_3_CurshInjury = ds_Patient_TypeInjury.Tables[0].Rows[12]["SZ_DESCRIPTION"].ToString();
            //14
            if (ds_Patient_TypeInjury.Tables[0].Rows[13]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_PuntureWound = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[13]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_PuntureWound = "1";
            }
            else
            {
                PdfDAO.chk_F_3_PuntureWound = "0";
            }
            PdfDAO.txt_F_3_PuntureWound = ds_Patient_TypeInjury.Tables[0].Rows[13]["SZ_DESCRIPTION"].ToString();
            //15
            if (ds_Patient_TypeInjury.Tables[0].Rows[14]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Dermatitis = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[14]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Dermatitis = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Dermatitis = "0";
            }
            PdfDAO.txt_F_3_Dermatitis = ds_Patient_TypeInjury.Tables[0].Rows[14]["SZ_DESCRIPTION"].ToString();
            //16
            if (ds_Patient_TypeInjury.Tables[0].Rows[15]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_RepetitiveStrainInjury = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[15]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_RepetitiveStrainInjury = "1";
            }
            else
            {
                PdfDAO.chk_F_3_RepetitiveStrainInjury = "0";
            }
            PdfDAO.txt_F_3_RepetitiveStrainInjury = ds_Patient_TypeInjury.Tables[0].Rows[15]["SZ_DESCRIPTION"].ToString();
            //17
            if (ds_Patient_TypeInjury.Tables[0].Rows[16]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Dislocation = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[16]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Dislocation = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Dislocation = "0";
            }
            PdfDAO.txt_F_3_Dislocation = ds_Patient_TypeInjury.Tables[0].Rows[16]["SZ_DESCRIPTION"].ToString();
            //18
            if (ds_Patient_TypeInjury.Tables[0].Rows[17]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_SpinalCordInjury = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[17]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_SpinalCordInjury = "1";
            }
            else
            {
                PdfDAO.chk_F_3_SpinalCordInjury = "0";
            }
            PdfDAO.txt_F_3_SpinalCordInjury = ds_Patient_TypeInjury.Tables[0].Rows[17]["SZ_DESCRIPTION"].ToString();
            //19
            if (ds_Patient_TypeInjury.Tables[0].Rows[18]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Fracture = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[18]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Fracture = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Fracture = "0";
            }
            PdfDAO.txt_F_3_Fracture = ds_Patient_TypeInjury.Tables[0].Rows[18]["SZ_DESCRIPTION"].ToString();
            //20
            if (ds_Patient_TypeInjury.Tables[0].Rows[19]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Sprain = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[19]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Sprain = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Sprain = "0";
            }
            PdfDAO.txt_F_3_Sprain = ds_Patient_TypeInjury.Tables[0].Rows[19]["SZ_DESCRIPTION"].ToString();
            //21
            if (ds_Patient_TypeInjury.Tables[0].Rows[20]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_HearingLoss = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[20]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_HearingLoss = "1";
            }
            else
            {
                PdfDAO.chk_F_3_HearingLoss = "0";
            }
            PdfDAO.txt_F_3_HearingLoss = ds_Patient_TypeInjury.Tables[0].Rows[20]["SZ_DESCRIPTION"].ToString();
            //22
            if (ds_Patient_TypeInjury.Tables[0].Rows[21]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Torn = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[21]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Torn = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Torn = "0";
            }
            PdfDAO.txt_F_3_Torn = ds_Patient_TypeInjury.Tables[0].Rows[21]["SZ_DESCRIPTION"].ToString();

            //23
            if (ds_Patient_TypeInjury.Tables[0].Rows[22]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Hernia = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[22]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Hernia = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Hernia = "0";
            }
            PdfDAO.txt_F_3_Hernia = ds_Patient_TypeInjury.Tables[0].Rows[22]["SZ_DESCRIPTION"].ToString();
            //24
            if (ds_Patient_TypeInjury.Tables[0].Rows[23]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_VisionLoss = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[23]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_VisionLoss = "1";
            }
            else
            {
                PdfDAO.chk_F_3_VisionLoss = "0";
            }
            PdfDAO.txt_F_3_VisionLoss = ds_Patient_TypeInjury.Tables[0].Rows[23]["SZ_DESCRIPTION"].ToString();
            //25
            if (ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_3_Other = "0";
            }
            else if (ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_3_Other = "1";
            }
            else
            {
                PdfDAO.chk_F_3_Other = "0";
            }
            if (ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString().Length > 120)
            {
                //string temp = ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString().Substring(0,120);
                PdfDAO.txt_F_3_Other = ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString().Substring(0, 120);
                string temp = ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString();
                PdfDAO.txt_F_3_Other_1 = ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString().Substring(120, ((ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString().Length)-120));
            }
            else
            {
                PdfDAO.txt_F_3_Other = ds_Patient_TypeInjury.Tables[0].Rows[24]["SZ_DESCRIPTION"].ToString();
                PdfDAO.txt_F_3_Other_1 = "";
            }

            //exam
            //1
            if (ds_Patient_Exam.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_7_None = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[0]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_7_None = "1";
            }
            else
            {
                PdfDAO.chk_F_7_None = "0";
            }
            //2
            if (ds_Patient_Exam.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Bruising = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[1]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Bruising = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Bruising = "0";
            }
            PdfDAO.txt_F_4_Bruising = ds_Patient_Exam.Tables[0].Rows[1]["SZ_DESCRIPTION"].ToString();
            //3
            if (ds_Patient_Exam.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Burns = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[2]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Burns = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Burns = "0";
            }
            PdfDAO.txt_F_4_Burns = ds_Patient_Exam.Tables[0].Rows[2]["SZ_DESCRIPTION"].ToString();
            //4
            if (ds_Patient_Exam.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Crepitation = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[3]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Crepitation = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Crepitation = "0";
            }
            PdfDAO.txt_F_4_Crepitation = ds_Patient_Exam.Tables[0].Rows[3]["SZ_DESCRIPTION"].ToString();
            //5
            if (ds_Patient_Exam.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Deformity = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[4]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Deformity = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Deformity = "0";
            }
            PdfDAO.txt_F_4_Deformity = ds_Patient_Exam.Tables[0].Rows[4]["SZ_DESCRIPTION"].ToString();
            //6
            if (ds_Patient_Exam.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Edema = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[5]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Edema = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Edema = "0";
            }
            PdfDAO.txt_F_4_Edema = ds_Patient_Exam.Tables[0].Rows[5]["SZ_DESCRIPTION"].ToString();
            //7
            if (ds_Patient_Exam.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Hematoma = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[6]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Hematoma = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Hematoma = "0";
            }
            PdfDAO.txt_F_4_Hematoma = ds_Patient_Exam.Tables[0].Rows[6]["SZ_DESCRIPTION"].ToString();
            //8
            if (ds_Patient_Exam.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_JointEffusion = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[7]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_JointEffusion = "1";
            }
            else
            {
                PdfDAO.chk_F_4_JointEffusion = "0";
            }
            PdfDAO.txt_F_4_JointEffusion = ds_Patient_Exam.Tables[0].Rows[7]["SZ_DESCRIPTION"].ToString();
            //9
            if (ds_Patient_Exam.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Laceration = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[8]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Laceration = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Laceration = "0";
            }
            PdfDAO.txt_F_4_Laceration = ds_Patient_Exam.Tables[0].Rows[8]["SZ_DESCRIPTION"].ToString();
            //10
            if (ds_Patient_Exam.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Pain = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[9]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Pain = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Pain = "0";
            }
            PdfDAO.txt_F_4_Pain = ds_Patient_Exam.Tables[0].Rows[9]["SZ_DESCRIPTION"].ToString();
            //11
            if (ds_Patient_Exam.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Scar = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[10]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Scar = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Scar = "0";
            }
            PdfDAO.txt_F_4_Scar = ds_Patient_Exam.Tables[0].Rows[10]["SZ_DESCRIPTION"].ToString();

            //12
            if (ds_Patient_Exam.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_OtherFindings = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[11]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_OtherFindings = "1";
            }
            else
            {
                PdfDAO.chk_F_4_OtherFindings = "0";
            }
            PdfDAO.txt_F_4_OtherFindings = ds_Patient_Exam.Tables[0].Rows[11]["SZ_DESCRIPTION"].ToString();
            //13 
            if (ds_Patient_Exam.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Neuromuscular = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[12]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Neuromuscular = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Neuromuscular = "0";
            }
            //14 
            if (ds_Patient_Exam.Tables[0].Rows[13]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Abnormal = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[13]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Abnormal = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Abnormal = "0";
            }
            //15
            if (ds_Patient_Exam.Tables[0].Rows[14]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_ActiveROM = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[14]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_ActiveROM = "1";
            }
            else
            {
                PdfDAO.chk_F_4_ActiveROM = "0";
            }
            PdfDAO.txt_F_4_ActiveROM = ds_Patient_Exam.Tables[0].Rows[14]["SZ_DESCRIPTION"].ToString();
            //16
            if (ds_Patient_Exam.Tables[0].Rows[15]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_PassiveROM = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[15]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_PassiveROM = "1";
            }
            else
            {
                PdfDAO.chk_F_4_PassiveROM = "0";
            }
            PdfDAO.txt_F_4_PassiveROM = ds_Patient_Exam.Tables[0].Rows[15]["SZ_DESCRIPTION"].ToString();
            //17
            if (ds_Patient_Exam.Tables[0].Rows[16]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Gait = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[16]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Gait = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Gait = "0";
            }
            PdfDAO.txt_F_4_Gait = ds_Patient_Exam.Tables[0].Rows[16]["SZ_DESCRIPTION"].ToString();
            //18
            if (ds_Patient_Exam.Tables[0].Rows[17]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Palpable = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[17]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Palpable = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Palpable = "0";
            }
            PdfDAO.txt_F_4_Palpable = ds_Patient_Exam.Tables[0].Rows[17]["SZ_DESCRIPTION"].ToString();
            //19
            if (ds_Patient_Exam.Tables[0].Rows[18]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Reflexes = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[18]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Reflexes = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Reflexes = "0";
            }
            PdfDAO.txt_F_4_Reflexes = ds_Patient_Exam.Tables[0].Rows[18]["SZ_DESCRIPTION"].ToString();
            //20
            if (ds_Patient_Exam.Tables[0].Rows[19]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Sensation = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[19]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Sensation = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Sensation = "0";
            }
            PdfDAO.txt_F_4_Sensation = ds_Patient_Exam.Tables[0].Rows[19]["SZ_DESCRIPTION"].ToString();
            //21
            if (ds_Patient_Exam.Tables[0].Rows[20]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Strength = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[20]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Strength = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Strength = "0";
            }
            PdfDAO.txt_F_4_Strength = ds_Patient_Exam.Tables[0].Rows[20]["SZ_DESCRIPTION"].ToString();
            //22
            if (ds_Patient_Exam.Tables[0].Rows[21]["SZ_VALUE"].ToString() == "0")
            {
                PdfDAO.chk_F_4_Wasting = "0";
            }
            else if (ds_Patient_Exam.Tables[0].Rows[21]["SZ_VALUE"].ToString() == "1")
            {
                PdfDAO.chk_F_4_Wasting = "1";
            }
            else
            {
                PdfDAO.chk_F_4_Wasting = "0";
            }
            PdfDAO.txt_F_4_Wasting = ds_Patient_Exam.Tables[0].Rows[21]["SZ_DESCRIPTION"].ToString();
            //end exam


            return PdfDAO;
        }
    }
}

//namespace testXFAItextSharp.DAO
//{
//    class C4DataBind
//    {
//        public C4DAO GetPDFData()
//        {
//            C4Data objDL = new C4Data();
//            string szBillNo = "mo1316";
//            DataSet ds = objDL.GetData(szBillNo);

//            C4DAO PdfDAO = new C4DAO();
//            PdfDAO= bindObject(ds);
//            return PdfDAO;
//        }

//        public C4DAO bindObject(DataSet ds)
//        {
//            C4DAO PdfDAO = new C4DAO();
//            PdfDAO.patientHomePhone1 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString();//SZ_PATIENT_FIRST_NAME,SZ_PATIENT_LAST_NAME,SZ_MI,SZ_SOCIAL_SECURITY_NO
//            PdfDAO.patientHomePhone2 = ds.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString();//SZ_PATIENT_STREET,SZ_PATIENT_CITY,SZ_PATIENT_STATE,SZ_PATIENT_ZIP,SZ_WCB_NO
//            PdfDAO.FirstName = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
//            PdfDAO.LastName= ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
//            PdfDAO.middleInitial= ds.Tables[0].Rows[0]["SZ_MI"].ToString();
//            PdfDAO.socialSecNumber= ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
//            PdfDAO.patientAddress= ds.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString();
//            PdfDAO.patientCity= ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
//            PdfDAO.patientState= ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
//            PdfDAO.patientZip= ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
//            PdfDAO.WCBNumber= ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
//            PdfDAO.WCBNumber = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
//            PdfDAO.carrierCaseNo= ds.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();

//            return PdfDAO;
//        }
//    }
//}
