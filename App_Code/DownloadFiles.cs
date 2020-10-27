using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace gb.web.utils
{
    public class DownloadFilesUtils
    {
        private const string Exports_Patients = "patient_list";
        private const string Exports_Visits = "appointment_list";
        private const string Exports_Bills = "bill_list";
        private const string Exports_Reconciliation = "reconciliation_list";
        private const string Exports_UnprocessBill = "unprocess_bill";
        
		public static string GetExportPhysicalPath(string sUserName, DownloadFilesExportTypes type, out string sFileName)
        {
            string sRoot = getUploadDocumentPhysicalPath();
            string sFolder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
            sFileName = getFileName(sUserName, type);
            return sRoot + sFolder + sFileName;
        }

        private static string getUploadDocumentPhysicalPath()
        {
            string str = "";
            SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
            try
            {
                connection.Open();
                SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
                while (reader.Read())
                {
                    str = reader["parametervalue"].ToString();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return str;
        }

        private static string getFileName(string sUserName, DownloadFilesExportTypes type)
        {
            string sName = "";
            if (type == DownloadFilesExportTypes.PATIENT_LIST)
                sName = Exports_Patients;
            if (type == DownloadFilesExportTypes.VISIT_LIST)
                sName = Exports_Visits;
            if (type == DownloadFilesExportTypes.BILL_LIST)
                sName = Exports_Bills;
            if (type == DownloadFilesExportTypes.RECONCILIATION_LIST)
                sName = Exports_Reconciliation;
            if (type == DownloadFilesExportTypes.UNPROCESS_BILLS)
                sName = Exports_UnprocessBill;
            return sUserName + "_" + sName + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".xlsx";
        }

        public static string GetExportRelativePath()
        {
            return ConfigurationSettings.AppSettings["FETCHEXCEL_SHEET"].ToString();
        }

        public static string GetExportRelativeURL(string sFileName)
        {
            return ConfigurationSettings.AppSettings["FETCHEXCEL_SHEET"].ToString() + sFileName;
        }
    }
    public enum DownloadFilesExportTypes
    {
        PATIENT_LIST, VISIT_LIST, BILL_LIST,RECONCILIATION_LIST,UNPROCESS_BILLS
    };
}