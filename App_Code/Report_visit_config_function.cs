using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Configuration;


public class Report_visit_config_function
{
    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
    ReportsCommon rptc = null;

	public Report_visit_config_function()
	{
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}

    private static void ResetVariable(ref int filterCode, ref string selectedValues, ref string wherecls, ref ArrayList arrParam)
    {
        filterCode = 0;
        arrParam = new ArrayList();
        selectedValues = string.Empty;
        wherecls = string.Empty;
    }

    public void save_visit_report(ArrayList arraylist)
    {
        Report_visit_configurationDAO rdobj = new Report_visit_configurationDAO();
        rdobj = (Report_visit_configurationDAO)arraylist[0];
        rptc = new ReportsCommon();
        int filterCode = 0, reportid = 0;
        string selectedValues = string.Empty, wherecls = string.Empty;
        ArrayList arrParam = new ArrayList();

        try
        {

            //Comment : Save Data To txn_reportqueries i.e reportid,filterid and wherecls column
            #region SaveData
            reportid = rptc.fetchReportID("Visit Report");

            if (rdobj.Sz_name != null)
            {
                filterCode = rptc.fetchFilterCode("sz_patient_name");
                selectedValues = Convert.ToString(rdobj.Sz_name);


                if (rdobj.Sz_name == "")
                    wherecls = " ";
                else if (rdobj.Sz_name != "Select All")
                    wherecls = " AND MST_PATIENT.SZ_PATIENT_ID IN (" + (rdobj.Sz_name) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_case_type != null)
            {
                filterCode = rptc.fetchFilterCode("sz_case_types");
                selectedValues = Convert.ToString(rdobj.Sz_case_type);

                if (rdobj.Sz_case_type == "")
                    wherecls = " ";
                else if (rdobj.Sz_case_type != "Select All")
                    wherecls = " AND MST_CASE_TYPE.SZ_CASE_TYPE_ID IN (" + (rdobj.Sz_case_type) + ") ";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_case_status != null)
            {
                filterCode = rptc.fetchFilterCode("sz_case_status");
                selectedValues = Convert.ToString(rdobj.Sz_case_status);

                if (rdobj.Sz_case_status == "")
                    wherecls = " ";
                else if (rdobj.Sz_case_status != "Select All")
                    wherecls = " AND MST_CASE_STATUS.SZ_CASE_STATUS_ID IN (" + (rdobj.Sz_case_status) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_carrier != null)
            {
                filterCode = rptc.fetchFilterCode("sz_carrier");
                selectedValues = Convert.ToString(rdobj.Sz_carrier);

                if (rdobj.Sz_carrier == "")
                    wherecls = " ";
                else if (rdobj.Sz_carrier != "Select All")
                    wherecls = " AND MST_INSURANCE_COMPANY.SZ_INSURANCE_ID IN (" + (rdobj.Sz_carrier) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_provider != null)
            {
                filterCode = rptc.fetchFilterCode("sz_provider");
                selectedValues = Convert.ToString(rdobj.Sz_provider);

                if (rdobj.Sz_provider == "")
                    wherecls = " ";
                else if (rdobj.Sz_provider != "Select All")
                    wherecls = " AND MST_OFFICE.SZ_OFFICE_ID IN (" + (rdobj.Sz_provider) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_attorney != null)
            {
                filterCode = rptc.fetchFilterCode("sz_attorney");
                selectedValues = Convert.ToString(rdobj.Sz_attorney);

                if (rdobj.Sz_attorney == "")
                    wherecls = " ";
                else if (rdobj.Sz_attorney != "Select All")
                    wherecls = " AND MST_ATTORNEY.SZ_ATTORNEY_ID IN (" + (rdobj.Sz_attorney) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_doctor != null)
            {
                filterCode = rptc.fetchFilterCode("sz_doctor");
                selectedValues = Convert.ToString(rdobj.Sz_doctor);

                if (rdobj.Sz_doctor == "")
                    wherecls = " ";
                else if (rdobj.Sz_doctor != "Select All")
                    wherecls = " AND MST_DOCTOR.SZ_DOCTOR_ID IN (" + (rdobj.Sz_doctor) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_specialty != null)
            {
                filterCode = rptc.fetchFilterCode("sz_specialty");
                selectedValues = Convert.ToString(rdobj.Sz_specialty);

                if (rdobj.Sz_specialty == "")
                    wherecls = " ";
                else if (rdobj.Sz_specialty != "Select All")
                    wherecls = " AND MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP_ID IN (" + (rdobj.Sz_specialty) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_location != null)
            {
                filterCode = rptc.fetchFilterCode("sz_location");
                selectedValues = Convert.ToString(rdobj.Sz_location);

                if (rdobj.Sz_location == "")
                    wherecls = " ";
                else if (rdobj.Sz_location != "Select All")
                    wherecls = " AND MST_LOCATION.SZ_LOCATION_ID IN (" + (rdobj.Sz_location) + " )";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }

            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_case_number != null)
            {
                filterCode = rptc.fetchFilterCode("sz_patientcase_numbers");
                selectedValues = Convert.ToString(rdobj.Sz_case_number);

                if (rdobj.Sz_case_number == "")
                    wherecls = " ";
                else if (rdobj.Sz_case_number != "Select All")
                    wherecls = " AND MST_ATTORNEY.SZ_ATTORNEY_ID IN (" + (rdobj.Sz_case_number) + ")";
                else
                    wherecls = " ALL ";

                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }


            ResetVariable(ref filterCode, ref selectedValues, ref wherecls, ref arrParam);

            if (rdobj.Sz_accident_date != null)
            {
                filterCode = rptc.fetchFilterCode("sz_accident_date");
                selectedValues = Convert.ToString(rdobj.Sz_accident_date);

                if (rdobj.Sz_accident_date != "")
                    wherecls = " AND  MST_PATIENT.DT_INJURY='" + rdobj.Sz_accident_date + "' ";
                else
                    wherecls = "";
                arrParam.Add(rdobj.CompanyID);
                arrParam.Add(reportid);
                arrParam.Add(filterCode);
                arrParam.Add(selectedValues);
                arrParam.Add(wherecls);

                rptc.saveReportData(arrParam);
            }
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            
        }
      }

   

}