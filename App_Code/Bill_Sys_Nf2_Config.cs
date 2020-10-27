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
using System.IO; 

public class Bill_Sys_Nf2_Config
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_Nf2_Config()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public void UpdateNf2INfo(NF2_CONFIG_DAO  _NF2_CONFIG_DAO)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_Update_Nf2_Config_Setting", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _NF2_CONFIG_DAO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", _NF2_CONFIG_DAO.SZ_INSURANCE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADD_ID", _NF2_CONFIG_DAO.SZ_INSURANCE_ADD_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _NF2_CONFIG_DAO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", _NF2_CONFIG_DAO.SZ_CLAIM_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", _NF2_CONFIG_DAO.SZ_POLICY_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICYHOLDER", _NF2_CONFIG_DAO.SZ_POLICYHOLDER);
            sqlCmd.Parameters.AddWithValue("@DT_DATEOF_ACCIDENT", _NF2_CONFIG_DAO.DT_DATEOF_ACCIDENT);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE_NO", _NF2_CONFIG_DAO.SZ_PATIENT_PHONE_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", _NF2_CONFIG_DAO.SZ_SOCIAL_SECURITY_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", _NF2_CONFIG_DAO.SZ_PATIENT_FIRST_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", _NF2_CONFIG_DAO.SZ_PATIENT_LAST_NAME);
            sqlCmd.Parameters.AddWithValue("@MI", _NF2_CONFIG_DAO.MI);
            sqlCmd.Parameters.AddWithValue("@SZ_GENDER", _NF2_CONFIG_DAO.SZ_GENDER);
            sqlCmd.Parameters.AddWithValue("@DT_DOB", _NF2_CONFIG_DAO.DT_DOB);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", _NF2_CONFIG_DAO.SZ_PATIENT_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_CITY", _NF2_CONFIG_DAO.SZ_PATIENT_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STATE", _NF2_CONFIG_DAO.SZ_PATIENT_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ZIP", _NF2_CONFIG_DAO.SZ_PATIENT_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", _NF2_CONFIG_DAO.SZ_ACCIDENT_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", _NF2_CONFIG_DAO.SZ_ACCIDENT_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE", _NF2_CONFIG_DAO.SZ_ACCIDENT_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_HOME_PHONE", _NF2_CONFIG_DAO.SZ_HOME_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_PHONE", _NF2_CONFIG_DAO.SZ_WORK_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_MAKE", _NF2_CONFIG_DAO.SZ_MAKE);
            sqlCmd.Parameters.AddWithValue("@SZ_YEAR", _NF2_CONFIG_DAO.SZ_YEAR);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_POLICY_NAME", _NF2_CONFIG_DAO.SZ_PATIENT_POLICY_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_NAME", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_ADDRESS", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_CITY", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_STATE", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_STATE_ID", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_STATE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_ZIP", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER_CLAIM_PHONE_NO", _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_PHONE_NO);
            sqlCmd.Parameters.AddWithValue("@SZ_BRIEF_DESCRIPTION_OF_ACCIDENT", _NF2_CONFIG_DAO.SZ_BRIEF_DESCRIPTION_OF_ACCIDENT);
            sqlCmd.Parameters.AddWithValue("@BT_BUS", _NF2_CONFIG_DAO.BT_BUS);
            sqlCmd.Parameters.AddWithValue("@BT_MOTORCYCLE", _NF2_CONFIG_DAO.BT_MOTORCYCLE);
            sqlCmd.Parameters.AddWithValue("@BT_TRUCK", _NF2_CONFIG_DAO.BT_TRUCK);
            sqlCmd.Parameters.AddWithValue("@BT_AUTOMOBILE", _NF2_CONFIG_DAO.BT_AUTOMOBILE);
            sqlCmd.Parameters.AddWithValue("@BT_YES_DRIVERMOTORVEHICLE", _NF2_CONFIG_DAO.BT_YES_DRIVERMOTORVEHICLE);
            sqlCmd.Parameters.AddWithValue("@BT_NO_DRIVERMOTORVEHICLE", _NF2_CONFIG_DAO.BT_NO_DRIVERMOTORVEHICLE);
            sqlCmd.Parameters.AddWithValue("@BT_YES_PASSENGERMOTORVEHICLE", _NF2_CONFIG_DAO.BT_YES_PASSENGERMOTORVEHICLE);
            sqlCmd.Parameters.AddWithValue("@BT_NO_PASSENGERMOTORVEHICLE", _NF2_CONFIG_DAO.BT_NO_PASSENGERMOTORVEHICLE);
            sqlCmd.Parameters.AddWithValue("@BT_YES_PEDESTRIAN", _NF2_CONFIG_DAO.BT_YES_PEDESTRIAN);
            sqlCmd.Parameters.AddWithValue("@BT_NO_PEDESTRIAN", _NF2_CONFIG_DAO.BT_NO_PEDESTRIAN);
            sqlCmd.Parameters.AddWithValue("@BT_YES_POLICYHOLDERHOUSEHOLD", _NF2_CONFIG_DAO.BT_YES_POLICYHOLDERHOUSEHOLD);
            sqlCmd.Parameters.AddWithValue("@BT_NO_POLICYHOLDERHOUSEHOLD", _NF2_CONFIG_DAO.BT_NO_POLICYHOLDERHOUSEHOLD);
            sqlCmd.Parameters.AddWithValue("@BT_YES_RELATIVERESIDEOWN", _NF2_CONFIG_DAO.BT_YES_RELATIVERESIDEOWN);
            sqlCmd.Parameters.AddWithValue("@BT_NO_RELATIVERESIDEOWN", _NF2_CONFIG_DAO.BT_NO_RELATIVERESIDEOWN);
            sqlCmd.Parameters.AddWithValue("@BT_YES_FURNISHINGHEALTHSERVICE", _NF2_CONFIG_DAO.BT_YES_FURNISHINGHEALTHSERVICE);
            sqlCmd.Parameters.AddWithValue("@BT_NO_FURNISHINGHEALTHSERVICE", _NF2_CONFIG_DAO.BT_NO_FURNISHINGHEALTHSERVICE);
            sqlCmd.Parameters.AddWithValue("@BT_OUT_PATIENT", _NF2_CONFIG_DAO.BT_OUT_PATIENT);
            sqlCmd.Parameters.AddWithValue("@BT_IN_PATIENT", _NF2_CONFIG_DAO.BT_IN_PATIENT);
            sqlCmd.Parameters.AddWithValue("@SZ_AMOUNT_HEALTHBILL", _NF2_CONFIG_DAO.SZ_AMOUNT_HEALTHBILL);
            sqlCmd.Parameters.AddWithValue("@BT_YES_MOREHEALTHTREATMENT", _NF2_CONFIG_DAO.BT_YES_MOREHEALTHTREATMENT);
            sqlCmd.Parameters.AddWithValue("@BT_NO_MOREHEALTHTREATMENT", _NF2_CONFIG_DAO.BT_NO_MOREHEALTHTREATMENT);
            sqlCmd.Parameters.AddWithValue("@BT_YES_COURSEOFEMPLOYMENT", _NF2_CONFIG_DAO.BT_YES_COURSEOFEMPLOYMENT);
            sqlCmd.Parameters.AddWithValue("@BT_NO_COURSEOFEMPLOYMENT", _NF2_CONFIG_DAO.BT_NO_COURSEOFEMPLOYMENT);
            sqlCmd.Parameters.AddWithValue("@SZ_DID_YOU_LOOSETIME", _NF2_CONFIG_DAO.SZ_DID_YOU_LOOSETIME);
            sqlCmd.Parameters.AddWithValue("@DT_ABSENCEFROM_WORK_BEGIN", _NF2_CONFIG_DAO.DT_ABSENCEFROM_WORK_BEGIN);
            sqlCmd.Parameters.AddWithValue("@SZ_RETURNED_TO_WORK", _NF2_CONFIG_DAO.SZ_RETURNED_TO_WORK);
            sqlCmd.Parameters.AddWithValue("@DT_RETURN_TO_WORK", _NF2_CONFIG_DAO.DT_RETURN_TO_WORK);
            sqlCmd.Parameters.AddWithValue("@SZ_AMOUNT_OF_TIME", _NF2_CONFIG_DAO.SZ_AMOUNT_OF_TIME);
            sqlCmd.Parameters.AddWithValue("@FLT_GROSS_WEEKLY_EARNINGS", _NF2_CONFIG_DAO.FLT_GROSS_WEEKLY_EARNINGS);
            sqlCmd.Parameters.AddWithValue("@I_NUMBEROFDAYS_WORK_WEEKLY", _NF2_CONFIG_DAO.I_NUMBEROFDAYS_WORK_WEEKLY);
            sqlCmd.Parameters.AddWithValue("@I_NUMBEROFHOURS_WORK_WEEKLY", _NF2_CONFIG_DAO.I_NUMBEROFHOURS_WORK_WEEKLY);
            sqlCmd.Parameters.AddWithValue("@BT_YES_RECIEVING_UNEMPLOYMENT", _NF2_CONFIG_DAO.BT_YES_RECIEVING_UNEMPLOYMENT);
            sqlCmd.Parameters.AddWithValue("@BT_NO_RECIEVING_UNEMPLOYMENT", _NF2_CONFIG_DAO.BT_NO_RECIEVING_UNEMPLOYMENT);
            sqlCmd.Parameters.AddWithValue("@BT_YES_OTHER_EXP", _NF2_CONFIG_DAO.BT_YES_OTHER_EXP);
            sqlCmd.Parameters.AddWithValue("@BT_NO_OTHER_EXP", _NF2_CONFIG_DAO.BT_NO_OTHER_EXP);
            sqlCmd.Parameters.AddWithValue("@BT_YES_STATE_DISABILITY", _NF2_CONFIG_DAO.BT_YES_STATE_DISABILITY);
            sqlCmd.Parameters.AddWithValue("@BT_NO_STATE_DISABILITY", _NF2_CONFIG_DAO.BT_NO_STATE_DISABILITY);
            sqlCmd.Parameters.AddWithValue("@BT_YES_WORKERS_COMP", _NF2_CONFIG_DAO.BT_YES_WORKERS_COMP);
            sqlCmd.Parameters.AddWithValue("@BT_NO_WORKERS_COMP", _NF2_CONFIG_DAO.BT_NO_WORKERS_COMP);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_NAME", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_ADDRESS", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_CITY", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_STATE", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_STATE_ID", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERONE_OCCUPATION", _NF2_CONFIG_DAO.SZ_EMPLOYERONE_OCCUPATION);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERONE_FROM", _NF2_CONFIG_DAO.DT_EMPLOYERONE_FROM);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERONE_TO", _NF2_CONFIG_DAO.DT_EMPLOYERONE_TO);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_NAME", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_ADDRESS", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_CITY", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_STATE", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_STATE_ID", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTWO_OCCUPATION", _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_OCCUPATION);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERTWO_FROM", _NF2_CONFIG_DAO.DT_EMPLOYERTWO_FROM);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERTWO_TO", _NF2_CONFIG_DAO.DT_EMPLOYERTWO_TO);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_NAME", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_ADDRESS", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_CITY", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_STATE", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_STATE_ID", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYERTHREE_OCCUPATION", _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_OCCUPATION);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERTHREE_FROM", _NF2_CONFIG_DAO.DT_EMPLOYERTHREE_FROM);
            sqlCmd.Parameters.AddWithValue("@DT_EMPLOYERTHREE_TO", _NF2_CONFIG_DAO.DT_EMPLOYERTHREE_TO);
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_NAME", _NF2_CONFIG_DAO.SZ_HOSPITAL_NAME);
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_ADDRESS", _NF2_CONFIG_DAO.SZ_HOSPITAL_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@DT_ADMISSION_DATE", _NF2_CONFIG_DAO.DT_ADMISSION_DATE);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();

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

    public DataSet GetNf2Info(string szCaseId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_NF2_CONFIG", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ALL");
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
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
        return ds;
    }

    public class NF2_CONFIG_DAO
    {
        private string _SZ_COMPANY_ID = string.Empty;
        public string SZ_COMPANY_ID 
        {
            get
            {
                return _SZ_COMPANY_ID;
            }
            set
            {
                _SZ_COMPANY_ID= value;
            }
        }

        private string _SZ_INSURANCE_ID = string.Empty;
        public string SZ_INSURANCE_ID
        {
            get
            {
                return _SZ_INSURANCE_ID;
            }
            set
            {
                _SZ_INSURANCE_ID = value;
            }
        }

        private string _SZ_INSURANCE_ADD_ID = string.Empty;
        public string SZ_INSURANCE_ADD_ID
        {
            get
            {
                return _SZ_INSURANCE_ADD_ID;
            }
            set
            {
                _SZ_INSURANCE_ADD_ID = value;
            }
        }
        private string _SZ_CASE_ID = string.Empty;
        public string SZ_CASE_ID
        {
            get
            {
                return _SZ_CASE_ID;
            }
            set
            {
                _SZ_CASE_ID = value;
            }
        }

        private string _SZ_CLAIM_NO = string.Empty;
        public string SZ_CLAIM_NO
        {
            get
            {
                return _SZ_CLAIM_NO;
            }
            set
            {
                _SZ_CLAIM_NO = value;
            }
        }



        private string _SZ_POLICY_NO = string.Empty;
        public string SZ_POLICY_NO 
        {
            get
            {
                return _SZ_POLICY_NO;
            }
            set
            {
                _SZ_POLICY_NO = value;
            }
        }
        private string _SZ_POLICYHOLDER = string.Empty;
        public string SZ_POLICYHOLDER
        {
            get
            {
                return _SZ_POLICYHOLDER;
            }
            set
            {
                _SZ_POLICYHOLDER = value;
            }
        }
        private string _DT_DATEOF_ACCIDENT = string.Empty;
        public string DT_DATEOF_ACCIDENT
        {
            get
            {
                return _DT_DATEOF_ACCIDENT;
            }
            set
            {
                _DT_DATEOF_ACCIDENT = value;
            }
        }

        private string _SZ_PATIENT_PHONE_NO = string.Empty;
        public string SZ_PATIENT_PHONE_NO
        {
            get
            {
                return _SZ_PATIENT_PHONE_NO;
            }
            set
            {
                _SZ_PATIENT_PHONE_NO = value;
            }
        }
        private string _SZ_SOCIAL_SECURITY_NO = string.Empty;
        public string SZ_SOCIAL_SECURITY_NO
        {
            get
            {
                return _SZ_SOCIAL_SECURITY_NO;
            }
            set
            {
                _SZ_SOCIAL_SECURITY_NO = value;
            }
        }
        private string _SZ_PATIENT_FIRST_NAME = string.Empty;
        public string SZ_PATIENT_FIRST_NAME
        {
            get
            {
                return _SZ_PATIENT_FIRST_NAME;
            }
            set
            {
                _SZ_PATIENT_FIRST_NAME = value;
            }
        }
        private string _SZ_PATIENT_LAST_NAME = string.Empty;
        public string SZ_PATIENT_LAST_NAME
        {
            get
            {
                return _SZ_PATIENT_LAST_NAME;
            }
            set
            {
                _SZ_PATIENT_LAST_NAME = value;
            }
        }

        private string _MI = string.Empty;
        public string MI
        {
            get
            {
                return _MI;
            }
            set
            {
                _MI = value;
            }
        }

        private string _SZ_GENDER = string.Empty;
        public string SZ_GENDER
        {
            get
            {
                return _SZ_GENDER;
            }
            set
            {
                _SZ_GENDER = value;
            }
        }

        private string _DT_DOB = string.Empty;
        public string DT_DOB
        {
            get
            {
                return _DT_DOB;
            }
            set
            {
                _DT_DOB = value;
            }
        }

        private string _SZ_PATIENT_ADDRESS = string.Empty;
        public string SZ_PATIENT_ADDRESS
        {
            get
            {
                return _SZ_PATIENT_ADDRESS;
            }
            set
            {
                _SZ_PATIENT_ADDRESS = value;
            }
        }
        private string _SZ_PATIENT_CITY = string.Empty;
        public string SZ_PATIENT_CITY 
        {
            get
            {
                return _SZ_PATIENT_CITY;
            }
            set
            {
                _SZ_PATIENT_CITY = value;
            }
        }
        private string _SZ_PATIENT_STATE = string.Empty;
        public string SZ_PATIENT_STATE
        {
            get
            {
                return _SZ_PATIENT_STATE;
            }
            set
            {
                _SZ_PATIENT_STATE = value;
            }
        }
        private string _SZ_PATIENT_ZIP = string.Empty;
        public string SZ_PATIENT_ZIP
        {
            get
            {
                return _SZ_PATIENT_ZIP;
            }
            set
            {
                _SZ_PATIENT_ZIP = value;
            }
        }
        private string _SZ_ACCIDENT_ADDRESS = string.Empty;
        public string SZ_ACCIDENT_ADDRESS
        {
            get
            {
                return _SZ_ACCIDENT_ADDRESS;
            }
            set
            {
                _SZ_ACCIDENT_ADDRESS = value;
            }
        }
        private string _SZ_ACCIDENT_CITY = string.Empty;
        public string SZ_ACCIDENT_CITY 
        {
            get
            {
                return _SZ_ACCIDENT_CITY;
            }
            set
            {
                _SZ_ACCIDENT_CITY = value;
            }
        }
        private string _SZ_ACCIDENT_STATE = string.Empty;
        public string SZ_ACCIDENT_STATE
        {
            get
            {
                return _SZ_ACCIDENT_STATE;
            }
            set
            {
                _SZ_ACCIDENT_STATE = value;
            }
        }

        private string _SZ_HOME_PHONE = string.Empty;
        public string SZ_HOME_PHONE
        {
            get
            {
                return _SZ_HOME_PHONE;
            }
            set
            {
                _SZ_HOME_PHONE = value;
            }
        }
        private string _SZ_WORK_PHONE = string.Empty;
        public string SZ_WORK_PHONE
        {
            get
            {
                return _SZ_WORK_PHONE;
            }
            set
            {
                _SZ_WORK_PHONE = value;
            }
        }
        private string _SZ_MAKE = string.Empty;
        public string SZ_MAKE
        {
            get
            {
                return _SZ_MAKE;
            }
            set
            {
                _SZ_MAKE = value;
            }
        }
        private string _SZ_YEAR = string.Empty;
        public string SZ_YEAR
        {
            get
            {
                return _SZ_YEAR;
            }
            set
            {
                _SZ_YEAR = value;
            }
        }
        private string _SZ_PATIENT_POLICY_NAME = string.Empty;
        public string SZ_PATIENT_POLICY_NAME
        {
            get
            {
                return _SZ_PATIENT_POLICY_NAME;
            }
            set
            {
                _SZ_PATIENT_POLICY_NAME = value;
            }
        }
        private string _SZ_INSURER_CLAIM_NAME = string.Empty;
        public string SZ_INSURER_CLAIM_NAME
        {
            get
            {
                return _SZ_INSURER_CLAIM_NAME;
            }
            set
            {
                _SZ_INSURER_CLAIM_NAME = value;
            }
        }
        private string _SZ_INSURER_CLAIM_ADDRESS = string.Empty;
        public string SZ_INSURER_CLAIM_ADDRESS
        {
            get
            {
                return _SZ_INSURER_CLAIM_ADDRESS;
            }
            set
            {
                _SZ_INSURER_CLAIM_ADDRESS = value;
            }
        }
        private string _SZ_INSURER_CLAIM_CITY = string.Empty;
        public string SZ_INSURER_CLAIM_CITY
        {
            get
            {
                return _SZ_INSURER_CLAIM_CITY;
            }
            set
            {
                _SZ_INSURER_CLAIM_CITY = value;
            }
        }
        private string _SZ_INSURER_CLAIM_STATE = string.Empty;
        public string SZ_INSURER_CLAIM_STATE
        {
            get
            {
                return _SZ_INSURER_CLAIM_STATE;
            }
            set
            {
                _SZ_INSURER_CLAIM_STATE = value;
            }
        }
        private string _SZ_INSURER_CLAIM_STATE_ID = string.Empty;
        public string SZ_INSURER_CLAIM_STATE_ID
        {
            get
            {
                return _SZ_INSURER_CLAIM_STATE_ID;
            }
            set
            {
                _SZ_INSURER_CLAIM_STATE_ID = value;
            }
        }

        private string _SZ_INSURER_CLAIM_ZIP = string.Empty;
        public string SZ_INSURER_CLAIM_ZIP
        {
            get
            {
                return _SZ_INSURER_CLAIM_ZIP;
            }
            set
            {
                _SZ_INSURER_CLAIM_ZIP = value;
            }
        }
        private string _SZ_INSURER_CLAIM_PHONE_NO = string.Empty;
        public string SZ_INSURER_CLAIM_PHONE_NO
        {
            get
            {
                return _SZ_INSURER_CLAIM_PHONE_NO;
            }
            set
            {
                _SZ_INSURER_CLAIM_PHONE_NO = value;
            }
        }
        private string _SZ_BRIEF_DESCRIPTION_OF_ACCIDENT = string.Empty;
        public string SZ_BRIEF_DESCRIPTION_OF_ACCIDENT
        {
            get
            {
                return _SZ_BRIEF_DESCRIPTION_OF_ACCIDENT;
            }
            set
            {
                _SZ_BRIEF_DESCRIPTION_OF_ACCIDENT = value;
            }
        }
        private string _BT_BUS = string.Empty;
        public string BT_BUS
        {
            get
            {
                return _BT_BUS;
            }
            set
            {
                _BT_BUS = value;
            }
        }
        private string _BT_MOTORCYCLE = string.Empty;
        public string BT_MOTORCYCLE
        {
            get
            {
                return _BT_MOTORCYCLE;
            }
            set
            {
                _BT_MOTORCYCLE = value;
            }
        }
        private string _BT_TRUCK = string.Empty;
        public string BT_TRUCK
        {
            get
            {
                return _BT_TRUCK;
            }
            set
            {
                _BT_TRUCK = value;
            }
        }
        private string _BT_AUTOMOBILE = string.Empty;
        public string BT_AUTOMOBILE
        {
            get
            {
                return _BT_AUTOMOBILE;
            }
            set
            {
                _BT_AUTOMOBILE = value;
            }
        }
        private string _BT_YES_DRIVERMOTORVEHICLE = string.Empty;
        public string BT_YES_DRIVERMOTORVEHICLE
        {
            get
            {
                return _BT_YES_DRIVERMOTORVEHICLE;
            }
            set
            {
                _BT_YES_DRIVERMOTORVEHICLE = value;
            }
        }
        private string _BT_NO_DRIVERMOTORVEHICLE = string.Empty;
        public string BT_NO_DRIVERMOTORVEHICLE
        {
            get
            {
                return _BT_NO_DRIVERMOTORVEHICLE;
            }
            set
            {
                _BT_NO_DRIVERMOTORVEHICLE = value;
            }
        }
        private string _BT_YES_PASSENGERMOTORVEHICLE = string.Empty;
        public string BT_YES_PASSENGERMOTORVEHICLE
        {
            get
            {
                return _BT_YES_PASSENGERMOTORVEHICLE;
            }
            set
            {
                _BT_YES_PASSENGERMOTORVEHICLE = value;
            }
        }
        private string _BT_NO_PASSENGERMOTORVEHICLE = string.Empty;
        public string BT_NO_PASSENGERMOTORVEHICLE
        {
            get
            {
                return _BT_NO_PASSENGERMOTORVEHICLE;
            }
            set
            {
                _BT_NO_PASSENGERMOTORVEHICLE = value;
            }
        }
        private string _BT_YES_PEDESTRIAN = string.Empty;
        public string BT_YES_PEDESTRIAN
        {
            get
            {
                return _BT_YES_PEDESTRIAN;
            }
            set
            {
                _BT_YES_PEDESTRIAN = value;
            }
        }
        private string _BT_NO_PEDESTRIAN = string.Empty;
        public string BT_NO_PEDESTRIAN 
        {
            get
            {
                return _BT_NO_PEDESTRIAN;
            }
            set
            {
                _BT_NO_PEDESTRIAN = value;
            }
        }
        private string _BT_YES_POLICYHOLDERHOUSEHOLD = string.Empty;
        public string BT_YES_POLICYHOLDERHOUSEHOLD
        {
            get
            {
                return _BT_YES_POLICYHOLDERHOUSEHOLD;
            }
            set
            {
                _BT_YES_POLICYHOLDERHOUSEHOLD = value;
            }
        }
        private string _BT_NO_POLICYHOLDERHOUSEHOLD = string.Empty;
        public string BT_NO_POLICYHOLDERHOUSEHOLD
        {
            get
            {
                return _BT_NO_POLICYHOLDERHOUSEHOLD;
            }
            set
            {
                _BT_NO_POLICYHOLDERHOUSEHOLD = value;
            }
        }
        private string _BT_YES_RELATIVERESIDEOWN = string.Empty;
        public string BT_YES_RELATIVERESIDEOWN
        {
            get
            {
                return _BT_YES_RELATIVERESIDEOWN;
            }
            set
            {
                _BT_YES_RELATIVERESIDEOWN = value;
            }
        }
        private string _BT_NO_RELATIVERESIDEOWN = string.Empty;
        public string BT_NO_RELATIVERESIDEOWN
        {
            get
            {
                return _BT_NO_RELATIVERESIDEOWN;
            }
            set
            {
                _BT_NO_RELATIVERESIDEOWN = value;
            }
        }
        private string _BT_YES_FURNISHINGHEALTHSERVICE = string.Empty;
        public string BT_YES_FURNISHINGHEALTHSERVICE
        {
            get
            {
                return _BT_YES_FURNISHINGHEALTHSERVICE;
            }
            set
            {
                _BT_YES_FURNISHINGHEALTHSERVICE = value;
            }
        }
        private string _BT_NO_FURNISHINGHEALTHSERVICE = string.Empty;
        public string BT_NO_FURNISHINGHEALTHSERVICE
        {
            get
            {
                return _BT_NO_FURNISHINGHEALTHSERVICE;
            }
            set
            {
                _BT_NO_FURNISHINGHEALTHSERVICE = value;
            }
        }
        private string _BT_OUT_PATIENT = string.Empty;
        public string BT_OUT_PATIENT
        {
            get
            {
                return _BT_OUT_PATIENT;
            }
            set
            {
                _BT_OUT_PATIENT = value;
            }
        }
        private string _BT_IN_PATIENT = string.Empty;
        public string BT_IN_PATIENT
        {
            get
            {
                return _BT_IN_PATIENT;
            }
            set
            {
                _BT_IN_PATIENT = value;
            }
        }
        private string _SZ_AMOUNT_HEALTHBILL= string.Empty;
        public string SZ_AMOUNT_HEALTHBILL
        {
            get
            {
                return _SZ_AMOUNT_HEALTHBILL;
            }
            set
            {
                _SZ_AMOUNT_HEALTHBILL = value;
            }
        }
        private string _BT_YES_MOREHEALTHTREATMENT = string.Empty;
        public string BT_YES_MOREHEALTHTREATMENT
        {
            get
            {
                return _BT_YES_MOREHEALTHTREATMENT;
            }
            set
            {
                _BT_YES_MOREHEALTHTREATMENT = value;
            }
        }
        private string _BT_NO_MOREHEALTHTREATMENT = string.Empty;
        public string BT_NO_MOREHEALTHTREATMENT
        {
            get
            {
                return _BT_NO_MOREHEALTHTREATMENT;
            }
            set
            {
                _BT_NO_MOREHEALTHTREATMENT = value;
            }
        }
        private string _BT_YES_COURSEOFEMPLOYMENT = string.Empty;
        public string BT_YES_COURSEOFEMPLOYMENT
        {
            get
            {
                return _BT_YES_COURSEOFEMPLOYMENT;
            }
            set
            {
                _BT_YES_COURSEOFEMPLOYMENT = value;
            }
        }
        private string _BT_NO_COURSEOFEMPLOYMENT = string.Empty;
        public string BT_NO_COURSEOFEMPLOYMENT
        {
            get
            {
                return _BT_NO_COURSEOFEMPLOYMENT;
            }
            set
            {
                _BT_NO_COURSEOFEMPLOYMENT = value;
            }
        }
        private string _SZ_DID_YOU_LOOSETIME = string.Empty;
        public string SZ_DID_YOU_LOOSETIME
        {
            get
            {
                return _SZ_DID_YOU_LOOSETIME;
            }
            set
            {
                _SZ_DID_YOU_LOOSETIME = value;
            }
        }
        private string _DT_ABSENCEFROM_WORK_BEGIN= string.Empty;
        public string DT_ABSENCEFROM_WORK_BEGIN
        {
            get
            {
                return _DT_ABSENCEFROM_WORK_BEGIN;
            }
            set
            {
                _DT_ABSENCEFROM_WORK_BEGIN = value;
            }
        }
        private string _SZ_RETURNED_TO_WORK= string.Empty;
        public string SZ_RETURNED_TO_WORK
        {
            get
            {
                return _SZ_RETURNED_TO_WORK;
            }
            set
            {
                _SZ_RETURNED_TO_WORK = value;
            }
        }
        private string _DT_RETURN_TO_WORK  = string.Empty;
        public string DT_RETURN_TO_WORK 
        {
            get
            {
                return _DT_RETURN_TO_WORK;
            }
            set
            {
                _DT_RETURN_TO_WORK = value;
            }
        }
        private string _SZ_AMOUNT_OF_TIME  = string.Empty;
        public string SZ_AMOUNT_OF_TIME 
        {
            get
            {
                return _SZ_AMOUNT_OF_TIME;
            }
            set
            {
                _SZ_AMOUNT_OF_TIME = value;
            }
        }
        private string _FLT_GROSS_WEEKLY_EARNINGS  = string.Empty;
        public string FLT_GROSS_WEEKLY_EARNINGS 
        {
            get
            {
                return _FLT_GROSS_WEEKLY_EARNINGS;
            }
            set
            {
                _FLT_GROSS_WEEKLY_EARNINGS = value;
            }
        }
        private string _I_NUMBEROFDAYS_WORK_WEEKLY  = string.Empty;
        public string I_NUMBEROFDAYS_WORK_WEEKLY 
        {
            get
            {
                return _I_NUMBEROFDAYS_WORK_WEEKLY;
            }
            set
            {
                _I_NUMBEROFDAYS_WORK_WEEKLY = value;
            }

        }
        private string _I_NUMBEROFHOURS_WORK_WEEKLY  = string.Empty;
        public string I_NUMBEROFHOURS_WORK_WEEKLY 
        {
            get
            {
                return _I_NUMBEROFHOURS_WORK_WEEKLY;
            }
            set
            {
                _I_NUMBEROFHOURS_WORK_WEEKLY = value;
            }
        }
        private string _BT_YES_RECIEVING_UNEMPLOYMENT  = string.Empty;
        public string BT_YES_RECIEVING_UNEMPLOYMENT 
        {
            get
            {
                return _BT_YES_RECIEVING_UNEMPLOYMENT;
            }
            set
            {
                _BT_YES_RECIEVING_UNEMPLOYMENT = value;
            }
        }
        private string _BT_NO_RECIEVING_UNEMPLOYMENT  = string.Empty;
        public string BT_NO_RECIEVING_UNEMPLOYMENT 
        {
            get
            {
                return _BT_NO_RECIEVING_UNEMPLOYMENT;
            }
            set
            {
                _BT_NO_RECIEVING_UNEMPLOYMENT = value;
            }
        }
        private string _BT_YES_OTHER_EXP = string.Empty;
        public string BT_YES_OTHER_EXP 
        {
            get
            {
                return _BT_YES_OTHER_EXP;
            }
            set
            {
                _BT_YES_OTHER_EXP = value;
            }
        }

        private string _BT_NO_OTHER_EXP  = string.Empty;
        public string BT_NO_OTHER_EXP 
        {
            get
            {
                return _BT_NO_OTHER_EXP;
            }
            set
            {
                _BT_NO_OTHER_EXP = value;
            }
        }
        private string _BT_YES_STATE_DISABILITY = string.Empty;
        public string BT_YES_STATE_DISABILITY 
        {
            get
            {
                return _BT_YES_STATE_DISABILITY;
            }
            set
            {
                _BT_YES_STATE_DISABILITY = value;
            }
        }
        private string _BT_NO_STATE_DISABILITY  = string.Empty;
        public string BT_NO_STATE_DISABILITY 
        {
            get
            {
                return _BT_NO_STATE_DISABILITY;
            }
            set
            {
                _BT_NO_STATE_DISABILITY = value;
            }
        }
        private string _BT_YES_WORKERS_COMP  = string.Empty;
        public string BT_YES_WORKERS_COMP 
        {
            get
            {
                return _BT_YES_WORKERS_COMP;
            }
            set
            {
                _BT_YES_WORKERS_COMP = value;
            }
        }
        private string _BT_NO_WORKERS_COMP  = string.Empty;
        public string BT_NO_WORKERS_COMP 
        {
            get
            {
                return _BT_NO_WORKERS_COMP;
            }
            set
            {
                _BT_NO_WORKERS_COMP = value;
            }
        }
        private string _SZ_EMPLOYERONE_NAME  = string.Empty;
        public string SZ_EMPLOYERONE_NAME 
        {
            get
            {
                return _SZ_EMPLOYERONE_NAME;
            }
            set
            {
                _SZ_EMPLOYERONE_NAME = value;
            }
        }
        private string _SZ_EMPLOYERONE_ADDRESS = string.Empty;
        public string SZ_EMPLOYERONE_ADDRESS 
        {
            get
            {
                return _SZ_EMPLOYERONE_ADDRESS;
            }
            set
            {
                _SZ_EMPLOYERONE_ADDRESS = value;
            }
        }
        private string _SZ_EMPLOYERONE_CITY  = string.Empty;
        public string SZ_EMPLOYERONE_CITY 
        {
            get
            {
                return _SZ_EMPLOYERONE_CITY;
            }
            set
            {
                _SZ_EMPLOYERONE_CITY = value;
            }
        }
        private string _SZ_EMPLOYERONE_STATE  = string.Empty;
        public string SZ_EMPLOYERONE_STATE 
        {
            get
            {
                return _SZ_EMPLOYERONE_STATE;
            }
            set
            {
                _SZ_EMPLOYERONE_STATE = value;
            }
        }
        private string _SZ_EMPLOYERONE_STATE_ID  = string.Empty;
        public string SZ_EMPLOYERONE_STATE_ID 
        {
            get
            {
                return _SZ_EMPLOYERONE_STATE_ID;
            }
            set
            {
                _SZ_EMPLOYERONE_STATE_ID = value;
            }
        }
       
        private string _SZ_EMPLOYERONE_OCCUPATION  = string.Empty;
        public string SZ_EMPLOYERONE_OCCUPATION 
        {
            get
            {
                return _SZ_EMPLOYERONE_OCCUPATION;
            }
            set
            {
                _SZ_EMPLOYERONE_OCCUPATION = value;
            }
        }
        private string _DT_EMPLOYERONE_FROM  = string.Empty;
        public string DT_EMPLOYERONE_FROM 
        {
            get
            {
                return _DT_EMPLOYERONE_FROM;
            }
            set
            {
                _DT_EMPLOYERONE_FROM = value;
            }
        }
        private string _DT_EMPLOYERONE_TO = string.Empty;
        public string DT_EMPLOYERONE_TO 
        {
            get
            {
                return _DT_EMPLOYERONE_TO;
            }
            set
            {
                _DT_EMPLOYERONE_TO = value;
            }
        }
        private string _SZ_EMPLOYERTWO_NAME  = string.Empty;
        public string SZ_EMPLOYERTWO_NAME 
        {
            get
            {
                return _SZ_EMPLOYERTWO_NAME;
            }
            set
            {
                _SZ_EMPLOYERTWO_NAME = value;
            }
        }
        private string _SZ_EMPLOYERTWO_ADDRESS  = string.Empty;
        public string SZ_EMPLOYERTWO_ADDRESS 
        {
            get
            {
                return _SZ_EMPLOYERTWO_ADDRESS;
            }
            set
            {
                _SZ_EMPLOYERTWO_ADDRESS = value;
            }
        }
        private string _SZ_EMPLOYERTWO_CITY = string.Empty;
        public string SZ_EMPLOYERTWO_CITY 
        {
            get
            {
                return _SZ_EMPLOYERTWO_CITY;
            }
            set
            {
                _SZ_EMPLOYERTWO_CITY = value;
            }
        }
        private string _SZ_EMPLOYERTWO_STATE  = string.Empty;
        public string SZ_EMPLOYERTWO_STATE 
        {
            get
            {
                return _SZ_EMPLOYERTWO_STATE;
            }
            set
            {
                _SZ_EMPLOYERTWO_STATE = value;
            }
        }
        private string _SZ_EMPLOYERTWO_STATE_ID = string.Empty;
        public string SZ_EMPLOYERTWO_STATE_ID 
        {
            get
            {
                return _SZ_EMPLOYERTWO_STATE_ID;
            }
            set
            {
                _SZ_EMPLOYERTWO_STATE_ID = value;
            }
        }
      
        private string _SZ_EMPLOYERTWO_OCCUPATION  = string.Empty;
        public string SZ_EMPLOYERTWO_OCCUPATION 
        {
            get
            {
                return _SZ_EMPLOYERTWO_OCCUPATION;
            }
            set
            {
                _SZ_EMPLOYERTWO_OCCUPATION = value;
            }
        }
        private string _DT_EMPLOYERTWO_FROM  = string.Empty;
        public string DT_EMPLOYERTWO_FROM 
        {
            get
            {
                return _DT_EMPLOYERTWO_FROM;
            }
            set
            {
                _DT_EMPLOYERTWO_FROM = value;
            }
        }
        private string _DT_EMPLOYERTWO_TO  = string.Empty;
        public string DT_EMPLOYERTWO_TO 
        {
            get
            {
                return _DT_EMPLOYERTWO_TO;
            }
            set
            {
                _DT_EMPLOYERTWO_TO = value;
            }
        }
        private string _SZ_EMPLOYERTHREE_NAME  = string.Empty;
        public string SZ_EMPLOYERTHREE_NAME 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_NAME;
            }
            set
            {
                _SZ_EMPLOYERTHREE_NAME = value;
            }
        }
        private string _SZ_EMPLOYERTHREE_ADDRESS  = string.Empty;
        public string SZ_EMPLOYERTHREE_ADDRESS 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_ADDRESS;
            }
            set
            {
                _SZ_EMPLOYERTHREE_ADDRESS = value;
            }
        }
         private string _SZ_EMPLOYERTHREE_CITY  = string.Empty;
        public string SZ_EMPLOYERTHREE_CITY 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_CITY;
            }
            set
            {
                _SZ_EMPLOYERTHREE_CITY = value;
            }
        }
         private string _SZ_EMPLOYERTHREE_STATE  = string.Empty;
        public string SZ_EMPLOYERTHREE_STATE 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_STATE;
            }
            set
            {
                _SZ_EMPLOYERTHREE_STATE = value;
            }
        }
         private string _SZ_EMPLOYERTHREE_STATE_ID  = string.Empty;
        public string SZ_EMPLOYERTHREE_STATE_ID 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_STATE_ID;
            }
            set
            {
                _SZ_EMPLOYERTHREE_STATE_ID = value;
            }
        }
      
          private string _SZ_EMPLOYERTHREE_OCCUPATION   = string.Empty;
        public string SZ_EMPLOYERTHREE_OCCUPATION 
        {
            get
            {
                return _SZ_EMPLOYERTHREE_OCCUPATION;
            }
            set
            {
                _SZ_EMPLOYERTHREE_OCCUPATION = value;
            }
        }
        private string _DT_EMPLOYERTHREE_FROM  = string.Empty;
        public string DT_EMPLOYERTHREE_FROM 
        {
            get
            {
                return _DT_EMPLOYERTHREE_FROM;
            }
            set
            {
                _DT_EMPLOYERTHREE_FROM = value;
            }
        }
        private string _DT_EMPLOYERTHREE_TO  = string.Empty;
        public string DT_EMPLOYERTHREE_TO 
        {
            get
            {
                return _DT_EMPLOYERTHREE_TO;
            }
            set
            {
                _DT_EMPLOYERTHREE_TO = value;
            }
        }

          private string _SZ_HOSPITAL_NAME  = string.Empty;
        public string SZ_HOSPITAL_NAME 
        {
            get
            {
                return _SZ_HOSPITAL_NAME;
            }
            set
            {
                _SZ_HOSPITAL_NAME = value;
            }
        }
          private string _SZ_HOSPITAL_ADDRESS = string.Empty;
        public string SZ_HOSPITAL_ADDRESS 
        {
            get
            {
                return _SZ_HOSPITAL_ADDRESS;
            }
            set
            {
                _SZ_HOSPITAL_ADDRESS = value;
            }
        }
          private string _DT_ADMISSION_DATE  = string.Empty;
        public string DT_ADMISSION_DATE 
        {
            get
            {
                return _DT_ADMISSION_DATE;
            }
            set
            {
                _DT_ADMISSION_DATE = value;
            }
        }



        

        }





    
}
