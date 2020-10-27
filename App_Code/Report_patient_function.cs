using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public class Report_patient_function : PageBase
{

    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
    ReportsCommon rptc = null;
   
	public Report_patient_function()
	{
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	
	}

    public void save_visit_report(ArrayList arraylist)
    {
        Report_PatientDAO rptobj = new Report_PatientDAO();
        Report_delivery_configurationDAO objDelCongig = (Report_delivery_configurationDAO)Session["Report_Conifguration"];
        string aa =objDelCongig.UserID;
        ArrayList arrParam = new ArrayList();
        rptc = new ReportsCommon();
        rptobj = (Report_PatientDAO)arraylist[0];
        int filterCode = 0, reportid = 0,userReportID=0;
        string selectedValues = string.Empty, wherecls = string.Empty, userID = rptobj.sz_userID;

        reportid = rptc.fetchReportID("Patient Report");

        #region SaveData

        arrParam.Add(rptobj.CompanyID);
        arrParam.Add(userID);
        arrParam.Add(reportid);
        arrParam.Add(rptobj.sz_userReportName);
        int affectedRow=rptc.saveReportNameData(arrParam);

        if (affectedRow ==1 )
        {
            int menuAffectedRow = rptc.saveReportMenuNameData(arrParam);

            arrParam = new ArrayList();

            arrParam.Add(reportid);
            arrParam.Add(rptobj.sz_userReportName);
            arrParam.Add(userID);
            arrParam.Add(rptobj.CompanyID);
            userReportID = rptc.fetchUserReportID(arrParam);
            arrParam = new ArrayList();

            if (rptobj.Sz_name != null)
            {
                filterCode = rptc.fetchFilterCode("sz_patient_name");
                selectedValues = Convert.ToString(rptobj.Sz_name);


                if (rptobj.Sz_name == "")
                    wherecls = " ";
                else if (rptobj.Sz_name != "Select All")
                    wherecls = " AND MST_PATIENT.SZ_PATIENT_ID IN (" + (rptobj.Sz_name) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_case_type != null)
            {
                filterCode = rptc.fetchFilterCode("sz_case_types");
                selectedValues = Convert.ToString(rptobj.Sz_case_type);

                if (rptobj.Sz_case_type == "")
                    wherecls = " ";
                else if (rptobj.Sz_case_type != "Select All")
                    wherecls = " AND MST_CASE_TYPE.SZ_CASE_TYPE_ID IN (" + (rptobj.Sz_case_type) + ") ";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_case_status != null)
            {
                filterCode = rptc.fetchFilterCode("sz_case_status");
                selectedValues = Convert.ToString(rptobj.Sz_case_status);

                if (rptobj.Sz_case_status == "")
                    wherecls = " ";
                else if (rptobj.Sz_case_status != "Select All")
                    wherecls = " AND MST_CASE_STATUS.SZ_CASE_STATUS_ID IN (" + (rptobj.Sz_case_status) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_carrier != null)
            {
                filterCode = rptc.fetchFilterCode("sz_carrier");
                selectedValues = Convert.ToString(rptobj.Sz_carrier);

                if (rptobj.Sz_carrier == "")
                    wherecls = " ";
                else if (rptobj.Sz_carrier != "Select All")
                    wherecls = " AND MST_INSURANCE_COMPANY.SZ_INSURANCE_ID IN (" + (rptobj.Sz_carrier) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_provider != null)
            {
                filterCode = rptc.fetchFilterCode("sz_provider");
                selectedValues = Convert.ToString(rptobj.Sz_provider);

                if (rptobj.Sz_provider == "")
                    wherecls = " ";
                else if (rptobj.Sz_provider != "Select All")
                    wherecls = " AND MST_OFFICE.SZ_OFFICE_ID IN (" + (rptobj.Sz_provider) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_attorney != null)
            {
                filterCode = rptc.fetchFilterCode("sz_attorney");
                selectedValues = Convert.ToString(rptobj.Sz_attorney);

                if (rptobj.Sz_attorney == "")
                    wherecls = " ";
                else if (rptobj.Sz_attorney != "Select All")
                    wherecls = " AND MST_ATTORNEY.SZ_ATTORNEY_ID IN (" + (rptobj.Sz_attorney) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_case_number != null)
            {
                filterCode = rptc.fetchFilterCode("sz_patientcase_numbers");
                selectedValues = Convert.ToString(rptobj.Sz_case_number);

                if (rptobj.Sz_case_number == "")
                    wherecls = " ";
                else if (rptobj.Sz_case_number != "Select All")
                    wherecls = " AND MST_ATTORNEY.SZ_ATTORNEY_ID IN (" + (rptobj.Sz_case_number) + ")";
                else
                    wherecls = " ALL ";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rptobj.Sz_accident_date != null)
            {
                filterCode = rptc.fetchFilterCode("sz_accident_date");
                selectedValues = Convert.ToString(rptobj.Sz_accident_date);

                if (rptobj.Sz_accident_date != "")
                    wherecls = " AND  MST_PATIENT.DT_INJURY='" + rptobj.Sz_accident_date + "' ";
                else
                    wherecls = "";

                arrParam.Add(rptobj.CompanyID);
                arrParam.Add(userID);
                arrParam.Add(userReportID);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            #region SaveReportConfiguration
            Report_delivery_config_function fun = new Report_delivery_config_function();
            objDelCongig.ireportid = userReportID;
            fun.save_Delivery_report(objDelCongig.SZ_arrayList_param);
            #endregion
        }
        else
        {
        }
        #endregion

     

        //Comment : Fetch Data And Export To Excel
        #region FetchData
        //DataSet ds = fetchData(rptobj.CompanyID, reportid);
        //rptc.exportDataExcel(ds, "Patient Report");
        #endregion
    }

    private static void ResetVariable(ref int filterCode, ref string selectedValues, ref string wherecls, ref ArrayList arrParam)
    {
        filterCode = 0; 
        arrParam = new ArrayList();
        selectedValues = string.Empty; 
        wherecls = string.Empty;
    }

    public DataSet fetchData(string comanyid,int reportID)
    {
        DataSet dsReportData = new DataSet();
        SqlConnection conn = new SqlConnection();
        try
        {
            SqlCommand comm;
            conn = new SqlConnection(strsqlcon);
            SqlDataAdapter sda =null;

            conn.Open();
            comm = new SqlCommand("FETCH_REPORT_DATA");//fetch_report_data_patient
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_ID", comanyid);
            //comm.Parameters.AddWithValue("@i_report_id", reportID);
            sda = new SqlDataAdapter(comm);
            sda.Fill(dsReportData);
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReportData;
    }
}