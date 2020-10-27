using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.ComponentModel;
using System.Globalization;

/// <summary>
/// Summary description for ZwangerManualImport
/// </summary>
public class ZwangerManualImport
{
    string Messege = "";
    SqlConnection conn = null;
    string path = "";
    //string savepath = ConfigurationSettings.AppSettings["SAVEFILEPATH"].ToString();
    string fileNameExt = "";
    DataTable tableInward = new DataTable();
    DataTable dtGrd = new DataTable();
    DataTable dtEvents = new DataTable();
    string SelectedTable = "";
	public ZwangerManualImport()
	{
        dtEvents.Columns.Add("CaseID");
        dtEvents.Columns.Add("EventId");
        dtEvents.Columns.Add("EventProcId");
        dtEvents.Columns.Add("ProcedureCode");
        dtEvents.Columns.Add("ProcedureDesc");
        dtEvents.Columns.Add("Error");
	}

    public DataTable makeTable(Zwanger_Visit_ImportDAO objZPDAO)
    {
        DataTable dtImport = new DataTable();
        dtImport.Columns.Add("FirstName");
        dtImport.Columns.Add("LastName");
        dtImport.Columns.Add("MI");
        dtImport.Columns.Add("PatientID");
        dtImport.Columns.Add("DateOfBirth");
        dtImport.Columns.Add("Gender");
        dtImport.Columns.Add("Address");
        dtImport.Columns.Add("Address2");
        dtImport.Columns.Add("City");
        dtImport.Columns.Add("State");
        dtImport.Columns.Add("Zip");
        dtImport.Columns.Add("CaseType");
        dtImport.Columns.Add("SSNO");
        dtImport.Columns.Add("Patient Phone Number");
        dtImport.Columns.Add("GFirstName");
        dtImport.Columns.Add("GLastName");
        dtImport.Columns.Add("GAddress");
        dtImport.Columns.Add("GCity");
        dtImport.Columns.Add("GState");
        dtImport.Columns.Add("GZip");
        dtImport.Columns.Add("GSSNO");
        dtImport.Columns.Add("CaseID");
        dtImport.Columns.Add("DateOfAppointment");
        dtImport.Columns.Add("VisitTime");
        dtImport.Columns.Add("VisitStatus");

        dtImport.Columns.Add("VisitFlag");
        dtImport.Columns.Add("ProcedureCode");
        dtImport.Columns.Add("ProcedureDesc");
        dtImport.Columns.Add("Department");
        dtImport.Columns.Add("USERFLAG");
        dtImport.Columns.Add("ReadingDoctorCode");
        dtImport.Columns.Add("ReportDisplayName");
        dtImport.Columns.Add("FolderName");
        dtImport.Columns.Add("UID");
        dtImport.Columns.Add("FileName");
        dtImport.Columns.Add("PolicyNumber");
        dtImport.Columns.Add("InsuranceID");

        dtImport.Columns.Add("DateOfAccident");
        dtImport.Columns.Add("ReferralDoctor");
        dtImport.Columns.Add("ReferralSpecialty");
        dtImport.Columns.Add("AttorneyName");
        dtImport.Columns.Add("AttorneyAddr1");
        dtImport.Columns.Add("AttorneyAddr2");
        dtImport.Columns.Add("AttorneyCity");
        dtImport.Columns.Add("AttorneyState");
        dtImport.Columns.Add("AttorneyZIP");
        dtImport.Columns.Add("InsuranceName");
        dtImport.Columns.Add("InsuranceAddress1");
        dtImport.Columns.Add("InsuranceAddress2");

        dtImport.Columns.Add("InsuranceCity");
        dtImport.Columns.Add("InsuranceState");
        dtImport.Columns.Add("InsuranceZip");
        dtImport.Columns.Add("ReadingDoctorOffice");
        dtImport.Columns.Add("ReadingDoctorName");
        dtImport.Columns.Add("ReadingDoctorLicense");
        dtImport.Columns.Add("ReadingDoctorAddress");
        dtImport.Columns.Add("ReadingDoctorCity");
        dtImport.Columns.Add("ReadingDoctorState");
        dtImport.Columns.Add("ReadingDoctorZip");
        dtImport.Columns.Add("BookName");
        dtImport.Columns.Add("BookFacility");

        dtImport.Columns.Add("BookAddress");
        dtImport.Columns.Add("BookCity");
        dtImport.Columns.Add("BookState");
        dtImport.Columns.Add("BookZip");
        dtImport.Columns.Add("ClaimNumber");
        dtImport.Columns.Add("Modality");
        dtImport.Columns.Add("CreatedDate");


        DataRow dr = dtImport.NewRow();

        dr["FirstName"] = objZPDAO.SZ_First_Name;
        dr["LastName"] = objZPDAO.SZ_Last_Name;
        dr["MI"] = objZPDAO.MiddleName;
        dr["PatientID"] = objZPDAO.SZ_Patient_ID ;
        dr["DateOfBirth"] = objZPDAO.SZ_Date_Of_Birth ;
        string szGender = "";
        if (objZPDAO.SZ_Gender.ToLower().Trim()  == "m")
        {
            szGender = "M";
        }
        else if (objZPDAO.SZ_Gender.ToLower().Trim() == "f")
        {
            szGender = "F";
        }
        dr["Gender"] = szGender.Trim();
        string address = objZPDAO.SZ_Address + " " + objZPDAO.SZ_Address2;
        dr["Address"] = address.Trim();
        dr["Address2"] = "";
        dr["City"] = objZPDAO.SZ_City;
        dr["State"] = objZPDAO.SZ_State;
        dr["Zip"] = objZPDAO.SZ_Zip;
        string casetype = "";
        if (objZPDAO.SZ_Case_Type == "NF" || objZPDAO.SZ_Case_Type == "WC")
        {
            if (objZPDAO.SZ_Case_Type == "NF")
            {
                casetype = "NF";
            }
            if (objZPDAO.SZ_Case_Type == "WC")
            {
                casetype = "WC";
            }
        }
        dr["CaseType"] = casetype.Trim();
        dr["SSNO"] = objZPDAO.SZ_SSNO;
        dr["Patient Phone Number"] = objZPDAO.PatinetPhone;
        dr["GFirstName"] = objZPDAO.PolicyHolder;
        dr["GLastName"] = "";
        dr["GAddress"] = "";
        dr["GCity"] = "";
        dr["GState"] = "";
        dr["GZip"] = "";
        dr["GSSNO"] = "";
        dr["CaseID"] = objZPDAO.SZ_Case_ID;
        dr["DateOfAppointment"] = objZPDAO.SZ_Date_Of_Appointment;
        dr["VisitTime"] = objZPDAO.SZ_Visit_Time;//"9:00:00 AM";
        dr["VisitStatus"] = "F";
        dr["VisitFlag"] = "U";
        dr["ProcedureCode"] = objZPDAO.SZ_Procedure_Code;
        dr["ProcedureDesc"] = objZPDAO.SZ_Procedure_Desc;
        dr["Department"] = "";
        dr["USERFLAG"] = "";
        dr["ReadingDoctorCode"] = "";
        dr["ReportDisplayName"] = "";
        dr["FolderName"] = "";
        dr["UID"] = "";
        dr["FileName"] = "";
        dr["PolicyNumber"] = objZPDAO.InsurancePolicyNumber;
        dr["InsuranceID"] = "";
        dr["DateOfAccident"] = objZPDAO.SZ_Date_Of_Accident;
        dr["ReferralDoctor"] = objZPDAO.ReferringDoctor;
        dr["ReferralSpecialty"] = "";
        dr["AttorneyName"] = "";
        dr["AttorneyAddr1"] = "";
        dr["AttorneyAddr2"] = "";
        dr["AttorneyCity"] = "";
        dr["AttorneyState"] = "";
        dr["AttorneyZIP"] = "";
        dr["InsuranceName"] = objZPDAO.InsuranceName;
        dr["InsuranceAddress1"] = objZPDAO.InsuranceAddress;
        dr["InsuranceAddress2"] = "";
        dr["InsuranceCity"] = objZPDAO.InsuranceCity;
        dr["InsuranceState"] = objZPDAO.InsuranceState;
        dr["InsuranceZip"] = objZPDAO.InsuranceZip;
        dr["ReadingDoctorOffice"] = "";
        dr["ReadingDoctorName"] = objZPDAO.ReadingDoctor;
        dr["ReadingDoctorLicense"] = "";
        dr["ReadingDoctorAddress"] = "";
        dr["ReadingDoctorCity"] = "";
        dr["ReadingDoctorState"] = "";
        dr["ReadingDoctorZip"] = "";
        dr["BookName"] = "";
        dr["BookFacility"] = objZPDAO.SZ_Book_Facility;
        dr["BookAddress"] = "";
        dr["BookCity"] = "";
        dr["BookState"] = "";
        dr["BookZip"] = "";
        dr["ClaimNumber"] = objZPDAO.ClaimNumber;
        dr["Modality"] = objZPDAO.SZ_Modality;
        dr["CreatedDate"] = DateTime.Now.ToString("MM/dd/yyyy");
        dtImport.Rows.Add(dr);


        return dtImport;

    }

    public string ImportVisit_ZP(DataTable dt)
    {
        try
        {
            DataTable dtImport = new DataTable();
            dtImport.Columns.Add("FirstName");
            dtImport.Columns.Add("LastName");
            dtImport.Columns.Add("MI");
            dtImport.Columns.Add("PatientID");
            dtImport.Columns.Add("DateOfBirth");
            dtImport.Columns.Add("Gender");
            dtImport.Columns.Add("Address");
            dtImport.Columns.Add("Address2");
            dtImport.Columns.Add("City");
            dtImport.Columns.Add("State");
            dtImport.Columns.Add("Zip");
            dtImport.Columns.Add("CaseType");
            dtImport.Columns.Add("SSNO");
            dtImport.Columns.Add("Patient Phone Number");
            dtImport.Columns.Add("GFirstName");
            dtImport.Columns.Add("GLastName");
            dtImport.Columns.Add("GAddress");
            dtImport.Columns.Add("GCity");
            dtImport.Columns.Add("GState");
            dtImport.Columns.Add("GZip");
            dtImport.Columns.Add("GSSNO");
            dtImport.Columns.Add("CaseID");
            dtImport.Columns.Add("DateOfAppointment");
            dtImport.Columns.Add("VisitTime");
            dtImport.Columns.Add("VisitStatus");

            dtImport.Columns.Add("VisitFlag");
            dtImport.Columns.Add("ProcedureCode");
            dtImport.Columns.Add("ProcedureDesc");
            dtImport.Columns.Add("Department");
            dtImport.Columns.Add("USERFLAG");
            dtImport.Columns.Add("ReadingDoctorCode");
            dtImport.Columns.Add("ReportDisplayName");
            dtImport.Columns.Add("FolderName");
            dtImport.Columns.Add("UID");
            dtImport.Columns.Add("FileName");
            dtImport.Columns.Add("PolicyNumber");
            dtImport.Columns.Add("InsuranceID");

            dtImport.Columns.Add("DateOfAccident");
            dtImport.Columns.Add("ReferralDoctor");
            dtImport.Columns.Add("ReferralSpecialty");
            dtImport.Columns.Add("AttorneyName");
            dtImport.Columns.Add("AttorneyAddr1");
            dtImport.Columns.Add("AttorneyAddr2");
            dtImport.Columns.Add("AttorneyCity");
            dtImport.Columns.Add("AttorneyState");
            dtImport.Columns.Add("AttorneyZIP");
            dtImport.Columns.Add("InsuranceName");
            dtImport.Columns.Add("InsuranceAddress1");
            dtImport.Columns.Add("InsuranceAddress2");

            dtImport.Columns.Add("InsuranceCity");
            dtImport.Columns.Add("InsuranceState");
            dtImport.Columns.Add("InsuranceZip");
            dtImport.Columns.Add("ReadingDoctorOffice");
            dtImport.Columns.Add("ReadingDoctorName");
            dtImport.Columns.Add("ReadingDoctorLicense");
            dtImport.Columns.Add("ReadingDoctorAddress");
            dtImport.Columns.Add("ReadingDoctorCity");
            dtImport.Columns.Add("ReadingDoctorState");
            dtImport.Columns.Add("ReadingDoctorZip");
            dtImport.Columns.Add("BookName");
            dtImport.Columns.Add("BookFacility");

            dtImport.Columns.Add("BookAddress");
            dtImport.Columns.Add("BookCity");
            dtImport.Columns.Add("BookState");
            dtImport.Columns.Add("BookZip");
            dtImport.Columns.Add("ClaimNumber");
            dtImport.Columns.Add("Modality");
            dtImport.Columns.Add("CreatedDate");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtImport.NewRow();
                if (dt.Rows[i]["FirstName"].ToString() != "")
                {
                    // string[] patientAR = dt.Rows[i]["Patient Name"].ToString().Split(',');
                    dr["FirstName"] = dt.Rows[i]["FirstName"].ToString().Trim();
                    dr["LastName"] = dt.Rows[i]["LastName"].ToString().Trim();
                    dr["MI"] = dt.Rows[i]["MI"].ToString().Trim();
                    dr["PatientID"] = dt.Rows[i]["PatientID"].ToString().Trim();
                    dr["DateOfBirth"] = dt.Rows[i]["DateOfBirth"].ToString();
                    string szGender = "";
                    if (dt.Rows[i]["Gender"].ToString().ToLower().Trim() == "m")
                    {
                        szGender = "M";
                    }
                    else if (dt.Rows[i]["Gender"].ToString().ToLower().Trim() == "f")
                    {
                        szGender = "F";
                    }
                    dr["Gender"] = szGender.Trim();
                    string address = dt.Rows[i]["Address"].ToString() + " " + dt.Rows[i]["Address2"].ToString();
                    dr["Address"] = address.Trim();
                    dr["Address2"] = "";
                    dr["City"] = dt.Rows[i]["City"].ToString().Trim();
                    dr["State"] = dt.Rows[i]["State"].ToString().Trim();
                    dr["Zip"] = dt.Rows[i]["Zip"].ToString().Trim();
                    string casetype = "";
                    if (dt.Rows[i]["CaseType"].ToString().Trim() == "NF" || dt.Rows[i]["CaseType"].ToString().Trim() == "WC")
                    {
                        if (dt.Rows[i]["CaseType"].ToString().Trim() == "NF")
                        {
                            casetype = "NO-FAULT";
                        }
                        if (dt.Rows[i]["CaseType"].ToString().Trim() == "WC")
                        {
                            casetype = "WORKERS CO";
                        }

                    }
                    dr["CaseType"] = casetype.Trim();
                    dr["SSNO"] = dt.Rows[i]["SSNO"].ToString().Trim();
                    dr["Patient Phone Number"] = dt.Rows[i]["Patient Phone Number"].ToString().Trim();
                    dr["GFirstName"] = dt.Rows[i]["GFirstName"].ToString();
                    dr["GLastName"] = "";
                    dr["GAddress"] = "";
                    dr["GCity"] = "";
                    dr["GState"] = "";
                    dr["GZip"] = "";
                    dr["GSSNO"] = "";
                    dr["CaseID"] = dt.Rows[i]["CaseID"].ToString().Trim().Trim();
                    dr["DateOfAppointment"] = dt.Rows[i]["DateOfAppointment"].ToString().Trim();
                    dr["VisitTime"] = dt.Rows[i]["VisitTime"].ToString();//"9:00:00 AM";
                    dr["VisitStatus"] = "F";
                    dr["VisitFlag"] = "U";
                    dr["ProcedureCode"] = dt.Rows[i]["ProcedureCode"].ToString().Trim();
                    dr["ProcedureDesc"] = dt.Rows[i]["ProcedureDesc"].ToString().Trim();
                    dr["Department"] = "";
                    dr["USERFLAG"] = "";
                    dr["ReadingDoctorCode"] = "";
                    dr["ReportDisplayName"] = "";
                    dr["FolderName"] = "";
                    dr["UID"] = "";
                    dr["FileName"] = "";
                    dr["PolicyNumber"] = dt.Rows[i]["PolicyNumber"].ToString().Trim();
                    dr["InsuranceID"] = "";
                    //dr["DateOfAccident"] = dt.Rows[i]["DateOfAccident"].ToString();
                    dr["DateOfAccident"] = "";
                    //string[] refDocAr = dt.Rows[i]["Referring Doctor / UPIN"].ToString().Split('/');
                    //string refdoc = refDocAr[0].ToString();
                    dr["ReferralDoctor"] = dt.Rows[i]["ReferralDoctor"].ToString().Trim();
                    dr["ReferralSpecialty"] = "";
                    dr["AttorneyName"] = "";
                    dr["AttorneyAddr1"] = "";
                    dr["AttorneyAddr2"] = "";
                    dr["AttorneyCity"] = "";
                    dr["AttorneyState"] = "";
                    dr["AttorneyZIP"] = "";
                    dr["InsuranceName"] = dt.Rows[i]["InsuranceName"].ToString().Trim();
                    dr["InsuranceAddress1"] = dt.Rows[i]["InsuranceAddress1"].ToString().Trim();
                    dr["InsuranceAddress2"] = "";
                    dr["InsuranceCity"] = dt.Rows[i]["InsuranceCity"].ToString().Trim();
                    dr["InsuranceState"] = dt.Rows[i]["InsuranceState"].ToString().Trim();
                    dr["InsuranceZip"] = dt.Rows[i]["InsuranceZip"].ToString().Trim();
                    dr["ReadingDoctorOffice"] = "";
                    dr["ReadingDoctorName"] = dt.Rows[i]["ReadingDoctorName"].ToString().Trim();
                    dr["ReadingDoctorLicense"] = "";
                    dr["ReadingDoctorAddress"] = "";
                    dr["ReadingDoctorCity"] = "";
                    dr["ReadingDoctorState"] = "";
                    dr["ReadingDoctorZip"] = "";
                    dr["BookName"] = "";
                    //if (dt.Rows[i]["Location"].ToString() != "")
                    //{
                    //    string[] bkf = dt.Rows[i]["Location"].ToString().Split('-');
                    //    if (bkf.Length == 2)
                    //    {
                    //        dr["BookFacility"] = bkf[1].ToString().Trim();
                    //    }
                    //    else
                    //    {
                    //        dr["BookFacility"] = "";
                    //    }

                    //}
                    //else
                    //{
                    //    dr["BookFacility"] = "";
                    //}
                    dr["BookFacility"] = dt.Rows[i]["BookFacility"].ToString().Trim();
                    dr["BookAddress"] = "";
                    dr["BookCity"] = "";
                    dr["BookState"] = "";
                    dr["BookZip"] = "";
                    dr["ClaimNumber"] = dt.Rows[i]["ClaimNumber"].ToString().Trim();
                    dr["Modality"] = dt.Rows[i]["Modality"].ToString().Trim();
                    dr["CreatedDate"] = DateTime.Now.ToString("MM/dd/yyyy");
                    dtImport.Rows.Add(dr);
                }
            }
            //string Basefilepath = System.Configuration.ConfigurationSettings.AppSettings["FilePath"].ToString();
            //string filepath = Basefilepath + "\\ReportFile" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtImport.Copy());
            ScanAndAddPatient(ds);
            Messege = "inserted";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Messege = ex.ToString();
        }
        finally
        {

        }
        return Messege;
    }

    private void ScanAndAddPatient(DataSet ds)
    {
        try
        {
            DateTime dt1 = new DateTime();
            DataTable dt = null;
            string sz_location_not_found = "";
            string sz_test = "";
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int iCount = 0; iCount < ds.Tables[0].Rows.Count; iCount++)
                    {
                        if (ds.Tables[0].Rows[iCount]["CaseID"].ToString().Trim() != "")
                        {
                            try
                            {
                                string szPolicyHolder = ds.Tables[0].Rows[iCount]["GFirstName"].ToString() + " " + ds.Tables[0].Rows[iCount]["GLastName"].ToString();
                                szPolicyHolder = szPolicyHolder.Trim();
                                if (ds.Tables[0].Rows[iCount]["CaseType"].ToString() == "NO-FAULT" || ds.Tables[0].Rows[iCount]["CaseType"].ToString() == "WORKERS CO" || ds.Tables[0].Rows[iCount]["CaseType"].ToString() == "LIEN")
                                {
                                    //ThisLogger.Debug(ds.Tables[0].Rows[iCount]["CaseType"].ToString() + "-- Case Type is a match --");
                                    DateTime dtA = new DateTime();
                                    string iPatientReturn = "";
                                    if (ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString() != "" && ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString() != null)
                                    {
                                        dtA = Convert.ToDateTime(ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString());
                                        iPatientReturn = savePatient(ds.Tables[0].Rows[iCount]["FirstName"].ToString(), ds.Tables[0].Rows[iCount]["LastName"].ToString(), ds.Tables[0].Rows[iCount]["DateOfBirth"].ToString(), ds.Tables[0].Rows[iCount]["Address"].ToString(), ds.Tables[0].Rows[iCount]["City"].ToString(), ds.Tables[0].Rows[iCount]["State"].ToString(), ds.Tables[0].Rows[iCount]["Zip"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString(), dtA.ToString("MM/dd/yyyy"), ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString(), ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["SSNO"].ToString(), szPolicyHolder, ds.Tables[0].Rows[iCount]["Gender"].ToString(), ds.Tables[0].Rows[iCount]["Patient Phone Number"].ToString(), Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]), Convert.ToString(ds.Tables[0].Rows[iCount]["MI"]));
                                    }
                                    else
                                    {
                                        iPatientReturn = savePatient(ds.Tables[0].Rows[iCount]["FirstName"].ToString(), ds.Tables[0].Rows[iCount]["LastName"].ToString(), ds.Tables[0].Rows[iCount]["DateOfBirth"].ToString(), ds.Tables[0].Rows[iCount]["Address"].ToString(), ds.Tables[0].Rows[iCount]["City"].ToString(), ds.Tables[0].Rows[iCount]["State"].ToString(), ds.Tables[0].Rows[iCount]["Zip"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString(), "", ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString(), ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["SSNO"].ToString(), szPolicyHolder, ds.Tables[0].Rows[iCount]["Gender"].ToString(), ds.Tables[0].Rows[iCount]["Patient Phone Number"].ToString(), Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]), Convert.ToString(ds.Tables[0].Rows[iCount]["MI"]));
                                    }
                                    if (iPatientReturn != "")
                                    {
                                        string[] ArrId = iPatientReturn.Split(',');
                                        string sz_Patient_Id = ArrId[1].ToString().Trim();
                                        string sz_Case_Id = ArrId[0].ToString().Trim();


                                        if (ds.Tables[0].Rows[iCount]["VisitStatus"].ToString().ToLower() == "f" && ds.Tables[0].Rows[iCount]["VisitFlag"].ToString().ToLower() == "u")
                                        {
                                            //ThisLogger.Debug(" --  Patient added-- ");

                                            string sz_Rading_Office = "";
                                            string szproctype = GetProcType(ds.Tables[0].Rows[iCount]["Modality"].ToString());
                                            string sz_visit_ins = "";
                                            string sz_even_id_ins = "";
                                            string sz_event_proc_id_ins = "";
                                            string sz_Start_Timetype_ins = "";
                                            string sz_Start_Time_ins = "";
                                            string sz_End_Time_ins = "";
                                            string sz_End_Timetype_ins = "";
                                            if (ds.Tables[0].Rows[iCount]["Modality"].ToString() != "" && szproctype != "" && ds.Tables[0].Rows[iCount]["Modality"].ToString() != "OT")
                                            {
                                                if (ds.Tables[0].Rows[iCount]["ReadingDoctorOffice"].ToString() != "")
                                                {
                                                    sz_Rading_Office = GetRadingOffice(ds.Tables[0].Rows[iCount]["ReadingDoctorOffice"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorAddress"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorState"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorCity"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorZip"].ToString());
                                                }
                                                else
                                                {
                                                    sz_Rading_Office = "OI000000000000000508";
                                                }
                                                string Reading_doc = "";
                                                
                                                if (ds.Tables[0].Rows[iCount]["ReadingDoctorName"].ToString().Trim() != "")
                                                    Reading_doc = GetRadingDoctor(ds.Tables[0].Rows[iCount]["ReadingDoctorName"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorLicense"].ToString(), sz_Rading_Office, ds.Tables[0].Rows[iCount]["Modality"].ToString());

                                                string sz_doc_name = ds.Tables[0].Rows[iCount]["ReferralDoctor"].ToString();
                                                sz_doc_name = sz_doc_name.Replace(',', ' ');
                                                string Ref_off_id = "";
                                                Ref_off_id = "OI000000000000000508";
                                                string sz_dco_id = "";
                                                string sz_off_id = "";
                                                string sz_dco_id1 = "";
                                                if (sz_doc_name.Trim() == "")
                                                    sz_doc_name = "Unknown";
                                                sz_dco_id1 = GetDocId(sz_doc_name, "", ds.Tables[0].Rows[iCount]["Modality"].ToString(), Ref_off_id);
                                                string[] arrdocs = sz_dco_id1.Split(',');
                                                sz_dco_id = arrdocs[0].ToString();
                                                sz_off_id = arrdocs[1].ToString();
                                                if (sz_dco_id1 == "")
                                                {
                                                    //ThisLogger.Debug("Doctor Id NotFound");
                                                }
                                                else
                                                {
                                                    ArrayList objAdd = new ArrayList();
                                                    objAdd.Add(sz_Patient_Id);
                                                    //objAdd.Add(ds.Tables[0].Rows[iCount][12].ToString());
                                                    objAdd.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                    string sz_Start_Timetype = "";
                                                    string sz_Start_Time = "";
                                                    string sz_End_Time = "";
                                                    string sz_End_Timetype = "";
                                                    //string szTime = ds.Tables[0].Rows[iCount][13].ToString();
                                                    string szTime = ds.Tables[0].Rows[iCount]["VisitTime"].ToString();
                                                    DateTime check_time = new DateTime();
                                                    bool check_time1 = DateTime.TryParse(szTime, out check_time);
                                                    if (check_time1 == true && szTime != "" && szTime != null)
                                                    {
                                                        DateTime Date_Time = DateTime.Parse(szTime);
                                                        sz_Start_Time = Date_Time.ToString("hh.mm");
                                                        sz_Start_Timetype = Date_Time.ToString("tt");
                                                        sz_End_Time = Date_Time.AddMinutes(30.00).ToString("hh.mm");
                                                        sz_End_Timetype = Date_Time.AddMinutes(30.00).ToString("tt");

                                                        sz_Start_Time_ins = sz_Start_Time;
                                                        sz_Start_Timetype_ins = sz_Start_Timetype;
                                                        sz_End_Time_ins = sz_End_Time;
                                                        sz_End_Timetype_ins = sz_End_Timetype;
                                                    }

                                                    //string proctype = GetProcType(ds.Tables[0].Rows[iCount][41].ToString());
                                                    string szVisit = "";
                                                    szVisit = CheckLHRrVisit(ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["FileName"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString(), sz_Start_Time, sz_Start_Timetype, ds.Tables[0].Rows[iCount]["Modality"].ToString(), Reading_doc);
                                                    sz_visit_ins = szVisit;
                                                    if (szVisit == "0")
                                                    {


                                                        //ThisLogger.Debug(" --New Visit Found-- ");
                                                        string proctype = GetProcType(ds.Tables[0].Rows[iCount]["Modality"].ToString());
                                                        if (proctype != "")
                                                        {
                                                            objAdd.Add(sz_Start_Time);
                                                            objAdd.Add("");
                                                            objAdd.Add(sz_dco_id);
                                                            objAdd.Add(proctype);
                                                            objAdd.Add("CO000000000000000107");
                                                            objAdd.Add(sz_Start_Timetype);
                                                            objAdd.Add(sz_End_Time);
                                                            objAdd.Add(sz_End_Timetype);
                                                            objAdd.Add("CO000000000000000107");
                                                            objAdd.Add("False");
                                                            objAdd.Add(2);
                                                            objAdd.Add(null);
                                                            objAdd.Add(sz_off_id);
                                                            int eventID;
                                                            eventID = Check_Event(objAdd);
                                                            //eventID=0 - Add New Visit
                                                            //eventID<>0 - Visit Already present, eventID contains the i_event_id of the visit
                                                            if (eventID == 0)
                                                            {
                                                                //Add New Visit
                                                                eventID = Save_Event(objAdd, "US000000000000000572");
                                                                if (eventID != 0)
                                                                    Update_Event(eventID, sz_Case_Id, "CO000000000000000107");
                                                                //ThisLogger.Debug(" --ADD Event ID -- ");
                                                            }

                                                            objAdd = new ArrayList();
                                                            objAdd.Add(proctype);
                                                            objAdd.Add(eventID);
                                                            objAdd.Add(2);
                                                            Save_Event_RefferPrcedure(objAdd, Reading_doc);
                                                            //ThisLogger.Debug(" --ADD Proc ID -- ");
                                                            string szBillProctype = "";
                                                            szBillProctype = GetEventProcID(eventID.ToString());
                                                            sz_even_id_ins = eventID.ToString();
                                                            sz_event_proc_id_ins = szBillProctype.ToString();
                                                            try
                                                            {
                                                                UpadteImportOn(sz_event_proc_id_ins, ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), sz_even_id_ins, sz_Case_Id);
                                                            }
                                                            catch (Exception ex)
                                                            {


                                                            }
                                                            AddLHRrVisit(ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["FileName"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString(), sz_Start_Time, sz_Start_Timetype, ds.Tables[0].Rows[iCount]["Modality"].ToString(), eventID.ToString(), szBillProctype, ds.Tables[0].Rows[iCount]["CreatedDate"].ToString());
                                                            #region "Save appointment Notes."
                                                            DAO_NOTES_BO _DAO_NOTES_BO;
                                                            DAO_NOTES_EO _DAO_NOTES_EO;
                                                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                                                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                                                            // _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + ds.Tables[0].Rows[iCount][12].ToString();
                                                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString();

                                                            _DAO_NOTES_BO = new DAO_NOTES_BO();
                                                            _DAO_NOTES_EO.SZ_USER_ID = "US000000000000000572";
                                                            _DAO_NOTES_EO.SZ_CASE_ID = sz_Case_Id;
                                                            _DAO_NOTES_EO.SZ_COMPANY_ID = "CO000000000000000107";
                                                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                                                            //ThisLogger.Debug(" --Notes Save -- ");

                                                            #endregion
                                                            CheckTrans(eventID.ToString(), szBillProctype, ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), sz_Case_Id);
                                                            //string insid = GetIns(ds.Tables[0].Rows[iCount][25].ToString(), ds.Tables[0].Rows[iCount][26].ToString(), "", ds.Tables[0].Rows[iCount][28].ToString(), ds.Tables[0].Rows[iCount][29].ToString(), ds.Tables[0].Rows[iCount][30].ToString());

                                                            string sz_location_id = "";

                                                            if (ds.Tables[0].Rows[iCount]["BookFacility"].ToString() != "")
                                                            {
                                                                sz_location_id = CheckLoaction(ds.Tables[0].Rows[iCount]["BookFacility"].ToString());
                                                                if (sz_location_id == "0")
                                                                {
                                                                    sz_location_not_found = sz_location_id + "Location " + ds.Tables[0].Rows[iCount]["BookFacility"].ToString() + "Note Found For Case:" + sz_Case_Id + "\n";
                                                                    //ThisLogger.Debug(" --Laoction Not Found -- ");

                                                                }
                                                                else
                                                                {
                                                                    UpadteLocationID(sz_location_id, sz_Case_Id);
                                                                    SaveLocationForEvent(sz_location_id, sz_even_id_ins);
                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            //ThisLogger.Debug(" -- failed to Find Proc_type_code-- ");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //ThisLogger.Debug(" --Same Visit Found-- ");
                                                        ArrayList al = new ArrayList();
                                                        al.Add(ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                        al.Add(ds.Tables[0].Rows[iCount]["CaseID"].ToString());
                                                        al.Add("CO000000000000000107");
                                                        al.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                        al.Add(sz_Start_Time);
                                                        al.Add(sz_Start_Timetype);
                                                        al.Add(sz_End_Time);
                                                        al.Add(sz_End_Timetype);
                                                        updateOldVisits(al);
                                                        string sz_location_id = "";
                                                        if (ds.Tables[0].Rows[iCount]["BookFacility"].ToString() != "")
                                                        {
                                                            sz_location_id = CheckLoaction(ds.Tables[0].Rows[iCount]["BookFacility"].ToString());
                                                            if (sz_location_id == "0")
                                                            {
                                                                sz_location_not_found = sz_location_id + "Location " + ds.Tables[0].Rows[iCount]["BookFacility"].ToString() + "Note Found For Case:" + sz_Case_Id + "\n";
                                                                //ThisLogger.Debug(" --Laoction Not Found -- ");
                                                            }
                                                            else
                                                            {
                                                                ArrayList alforevent = new ArrayList();
                                                                alforevent.Add(ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                                alforevent.Add(ds.Tables[0].Rows[iCount]["CaseID"].ToString());
                                                                alforevent.Add("CO000000000000000107");
                                                                alforevent.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                                alforevent.Add(sz_Start_Time);
                                                                alforevent.Add(sz_Start_Timetype);
                                                                alforevent.Add(sz_End_Time);
                                                                alforevent.Add(sz_End_Timetype);
                                                                alforevent.Add(sz_location_id);
                                                                updateOldVisitsForEvent(alforevent);

                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (ds.Tables[0].Rows[iCount]["ReadingDoctorOffice"].ToString() != "")
                                                {
                                                    //sz_Rading_Office = GetRadingOffice(ds.Tables[0].Rows[iCount][31].ToString(), ds.Tables[0].Rows[iCount][34].ToString(), ds.Tables[0].Rows[iCount][36].ToString(), ds.Tables[0].Rows[iCount][35].ToString(), ds.Tables[0].Rows[iCount][37].ToString());
                                                    sz_Rading_Office = GetRadingOffice(ds.Tables[0].Rows[iCount]["ReadingDoctorOffice"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorAddress"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorState"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorCity"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorZip"].ToString());
                                                }
                                                else
                                                {
                                                    sz_Rading_Office = "OI000000000000000508";
                                                }


                                                string Reading_doc = "";
                                                string ot_proc_type = getLhrOtProcCode();
                                                if (ds.Tables[0].Rows[iCount]["ReadingDoctorName"].ToString().Trim() != "")
                                                    Reading_doc = GetRadingDoctor(ds.Tables[0].Rows[iCount]["ReadingDoctorName"].ToString(), ds.Tables[0].Rows[iCount]["ReadingDoctorLicense"].ToString(), sz_Rading_Office, "OT");

                                                string sz_doc_name = ds.Tables[0].Rows[iCount]["ReferralDoctor"].ToString();
                                                sz_doc_name = sz_doc_name.Replace(',', ' ');
                                                string Ref_off_id = "";
                                                //Ref_off_id=GetOffice(ds.Tables[0].Rows[iCount][39].ToString());
                                                //Ref_off_id = GetOffice(ds.Tables[0].Rows[iCount]["ReadingDoctorOffice"].ToString());
                                                Ref_off_id = "OI000000000000000508";
                                                string sz_dco_id = "";
                                                string sz_off_id = "";
                                                //string sz_dco_id1 = GetDocId(sz_doc_name, "", ds.Tables[0].Rows[iCount][41].ToString(), Ref_off_id);
                                                string sz_dco_id1 = "";
                                                if (sz_doc_name.Trim() == "")
                                                    sz_doc_name = "Unknown";
                                                sz_dco_id1 = GetDocId(sz_doc_name, "", "OT", Ref_off_id);
                                                string[] arrdocs = sz_dco_id1.Split(',');
                                                sz_dco_id = arrdocs[0].ToString();
                                                sz_off_id = arrdocs[1].ToString();
                                                ArrayList objAdd = new ArrayList();
                                                objAdd.Add(sz_Patient_Id);
                                                //objAdd.Add(ds.Tables[0].Rows[iCount][12].ToString());
                                                objAdd.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                string sz_Start_Timetype = "";
                                                string sz_Start_Time = "";
                                                string sz_End_Time = "";
                                                string sz_End_Timetype = "";
                                                //string szTime = ds.Tables[0].Rows[iCount][13].ToString();
                                                string szTime = ds.Tables[0].Rows[iCount]["VisitTime"].ToString();
                                                DateTime check_time = new DateTime();
                                                bool check_time1 = DateTime.TryParse(szTime, out check_time);
                                                if (check_time1 == true && szTime != "" && szTime != null)
                                                {
                                                    DateTime Date_Time = DateTime.Parse(szTime);
                                                    sz_Start_Time = Date_Time.ToString("hh.mm");
                                                    sz_Start_Timetype = Date_Time.ToString("tt");
                                                    sz_End_Time = Date_Time.AddMinutes(30.00).ToString("hh.mm");
                                                    sz_End_Timetype = Date_Time.AddMinutes(30.00).ToString("tt");
                                                }
                                                //string proctype = GetProcType(ds.Tables[0].Rows[iCount][41].ToString());
                                                string szVisit = "";
                                                szVisit = CheckLHRrVisit(ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["FileName"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString(), sz_Start_Time, sz_Start_Timetype, "NA", Reading_doc);
                                                sz_visit_ins = szVisit;
                                                if (szVisit == "0")
                                                {
                                                    objAdd.Add(sz_Start_Time);
                                                    objAdd.Add("");
                                                    objAdd.Add(sz_dco_id);
                                                    objAdd.Add(ot_proc_type);//proctype
                                                    objAdd.Add("CO000000000000000107");
                                                    objAdd.Add(sz_Start_Timetype);
                                                    objAdd.Add(sz_End_Time);
                                                    objAdd.Add(sz_End_Timetype);
                                                    objAdd.Add("CO000000000000000107");
                                                    objAdd.Add("False");
                                                    objAdd.Add(2);
                                                    objAdd.Add(null);
                                                    objAdd.Add(sz_off_id);
                                                    int eventID;
                                                    eventID = Check_Event(objAdd);
                                                    //eventID=0 - Add New Visit
                                                    //eventID<>0 - Visit Already present, eventID contains the i_event_id of the visit
                                                    if (eventID == 0)
                                                    {
                                                        //Add New Visit
                                                        eventID = Save_Event(objAdd, "US000000000000000572");
                                                        if (eventID != 0)
                                                            Update_Event(eventID, sz_Case_Id, "CO000000000000000107");
                                                        //ThisLogger.Debug(" --ADD Event ID -- ");
                                                    }
                                                    objAdd = new ArrayList();
                                                    objAdd.Add(ot_proc_type);//proctype
                                                    objAdd.Add(eventID);
                                                    objAdd.Add(2);
                                                    Save_Event_RefferPrcedure(objAdd, Reading_doc);
                                                    //ThisLogger.Debug(" --ADD Proc ID -- ");
                                                    string szBillProctype = "";
                                                    szBillProctype = GetEventProcID(eventID.ToString());
                                                    sz_even_id_ins = eventID.ToString();
                                                    sz_event_proc_id_ins = szBillProctype.ToString();
                                                    try
                                                    {
                                                        UpadteImportOn(sz_event_proc_id_ins, ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), sz_even_id_ins, sz_Case_Id);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                                                    }
                                                    AddLHRrVisit(ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["FileName"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString(), sz_Start_Time, sz_Start_Timetype, "NA", eventID.ToString(), szBillProctype, ds.Tables[0].Rows[iCount]["CreatedDate"].ToString());
                                                    CheckTrans(eventID.ToString(), szBillProctype, ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), sz_Case_Id);
                                                    #region "Save appointment Notes."
                                                    DAO_NOTES_BO _DAO_NOTES_BO;
                                                    DAO_NOTES_EO _DAO_NOTES_EO;
                                                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                                                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                                                    // _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + ds.Tables[0].Rows[iCount][12].ToString();
                                                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString();

                                                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                                                    _DAO_NOTES_EO.SZ_USER_ID = "US000000000000000572";
                                                    _DAO_NOTES_EO.SZ_CASE_ID = sz_Case_Id;
                                                    _DAO_NOTES_EO.SZ_COMPANY_ID = "CO000000000000000107";
                                                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                                                    //ThisLogger.Debug(" --Notes Save -- ");
                                                    #endregion

                                                    string sz_location_id = "";
                                                    if (ds.Tables[0].Rows[iCount]["BookFacility"].ToString() != "")
                                                    {
                                                        sz_location_id = CheckLoaction(ds.Tables[0].Rows[iCount]["BookFacility"].ToString());
                                                        if (sz_location_id == "0")
                                                        {
                                                            sz_location_not_found = sz_location_id + "Location " + ds.Tables[0].Rows[iCount]["BookFacility"].ToString() + "Note Found For Case:" + sz_Case_Id + "\n";
                                                            //ThisLogger.Debug(" --Laoction Not Found -- ");
                                                        }
                                                        else
                                                        {
                                                            UpadteLocationID(sz_location_id, sz_Case_Id);
                                                            SaveLocationForEvent(sz_location_id, sz_even_id_ins);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string sz_location_id = "";
                                                    //ThisLogger.Debug(" --Same Visit Found-- ");
                                                    ArrayList al = new ArrayList();
                                                    al.Add(ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                    al.Add(ds.Tables[0].Rows[iCount]["CaseID"].ToString());
                                                    al.Add("CO000000000000000107");
                                                    al.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                    al.Add(sz_Start_Time);
                                                    al.Add(sz_Start_Timetype);
                                                    al.Add(sz_End_Time);
                                                    al.Add(sz_End_Timetype);
                                                    updateOldVisits(al);
                                                    if (ds.Tables[0].Rows[iCount]["BookFacility"].ToString() != "")
                                                    {
                                                        sz_location_id = CheckLoaction(ds.Tables[0].Rows[iCount]["BookFacility"].ToString());
                                                        if (sz_location_id == "0")
                                                        {
                                                            sz_location_not_found = sz_location_id + "Location " + ds.Tables[0].Rows[iCount]["BookFacility"].ToString() + "Note Found For Case:" + sz_Case_Id + "\n";
                                                            //ThisLogger.Debug(" --Laoction Not Found -- ");
                                                        }
                                                        else
                                                        {
                                                            ArrayList alforevent = new ArrayList();
                                                            alforevent.Add(ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                            alforevent.Add(ds.Tables[0].Rows[iCount]["CaseID"].ToString());
                                                            alforevent.Add("CO000000000000000107");
                                                            alforevent.Add(ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString());
                                                            alforevent.Add(sz_Start_Time);
                                                            alforevent.Add(sz_Start_Timetype);
                                                            alforevent.Add(sz_End_Time);
                                                            alforevent.Add(sz_End_Timetype);
                                                            alforevent.Add(sz_location_id);
                                                            updateOldVisitsForEvent(alforevent);

                                                        }

                                                    }
                                                }
                                            }
                                            //a

                                            if (ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim() != "" && ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim().ToLower() != "no insurance available" && ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim().ToLower() != " ")
                                            {
                                                string insid = GetIns(ds.Tables[0].Rows[iCount]["InsuranceName"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceAddress1"].ToString(), "", ds.Tables[0].Rows[iCount]["InsuranceCity"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceState"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceZip"].ToString());
                                                string szCheckIns = CheckPatientIns(sz_Case_Id);
                                                string[] arrIns = insid.Split(',');
                                                string sz_ins_id = arrIns[0].ToString();
                                                string sz_ins_add_id = arrIns[1].ToString();

                                                if (sz_visit_ins == "0")
                                                {

                                                    string sz_policy_no = "";
                                                    string sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();

                                                    string sz_dao = ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString().Trim();
                                                    if (ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_policy_no = ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim();
                                                    }
                                                    if (ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();
                                                    }
                                                    //AddVisitInsInfo(sz_even_id_ins, sz_event_proc_id_ins, sz_ins_id, sz_ins_add_id, sz_Claim_no, sz_policy_no, "", sz_dao);
                                                    SaveInsForEvent(sz_ins_id, sz_ins_add_id, sz_event_proc_id_ins, sz_even_id_ins, sz_Case_Id, sz_Claim_no, sz_policy_no, ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["GFirstName"].ToString() + " " + ds.Tables[0].Rows[iCount]["GLastName"].ToString(), sz_dao, Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]));

                                                }
                                                else
                                                {
                                                    string sz_policy_no = "";
                                                    string sz_Claim_no = "";
                                                    string sz_dao = ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString().Trim();
                                                    if (ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_policy_no = ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim();
                                                    }
                                                    if (ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();
                                                    }
                                                    //// UpdateVisitInsuranceInformation(sz_ins_id, sz_ins_add_id, sz_Claim_no, sz_policy_no, "", sz_dao, ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                    UpdateInsForEvent(sz_ins_id, sz_ins_add_id, ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), sz_Case_Id, sz_Claim_no, sz_policy_no, ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["GFirstName"].ToString() + " " + ds.Tables[0].Rows[iCount]["GLastName"].ToString(), sz_dao, ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]));
                                                }

                                                if (szCheckIns.Trim() == "0,0")
                                                {
                                                    UpadteID(sz_ins_id, sz_ins_add_id, sz_Case_Id);
                                                    //ThisLogger.Debug(" --Update InsId,InsAddId ToCase -- ");
                                                }
                                                string szCheck2Ins = CheckSecondaryIns(sz_Case_Id, sz_ins_id, sz_ins_add_id);
                                                if (szCheck2Ins.Trim() == "0,0")
                                                {
                                                    AddSecondaryIns(sz_Patient_Id, sz_Case_Id, sz_ins_id, sz_ins_add_id, ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                    //ThisLogger.Debug(" --Added Secondary INS -- ");
                                                }
                                                try
                                                {
                                                    DataSet dsSwapIns = new DataSet();
                                                    dsSwapIns = GetSwapInsurance();
                                                    int iSwapFlag = 0;
                                                    for (int cntSwap = 0; cntSwap < dsSwapIns.Tables[0].Rows.Count; cntSwap++)
                                                    {
                                                        if (sz_ins_id == dsSwapIns.Tables[0].Rows[cntSwap]["SZ_INSURANCE_ID"].ToString())
                                                        {
                                                            iSwapFlag = 1;
                                                            break;
                                                        }
                                                    }

                                                    if (iSwapFlag == 0)
                                                    {
                                                        int iSwapFlag1 = 0;
                                                        DataSet dsGetIns = new DataSet();
                                                        dsGetIns = GetInsuranceCompanyID(sz_Case_Id);
                                                        string szOrignalID = dsGetIns.Tables[0].Rows[0][0].ToString();
                                                        string szSecondryInsId = "";
                                                        string szSecondryInsAddId = "";

                                                        for (int cntCheckInsID = 0; cntCheckInsID < dsSwapIns.Tables[0].Rows.Count; cntCheckInsID++)
                                                        {
                                                            if (szOrignalID == dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ID"].ToString())
                                                            {
                                                                iSwapFlag1 = 1;
                                                                szSecondryInsId = dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ID"].ToString();
                                                                szSecondryInsAddId = dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ADDRESS_ID"].ToString();
                                                                break;
                                                            }
                                                        }
                                                        if (iSwapFlag1 == 1)
                                                        {
                                                            UpadteID(sz_ins_id, sz_ins_add_id, sz_Case_Id);
                                                            string szCheck2Ins1 = CheckSecondaryIns(sz_Case_Id, szSecondryInsId, szSecondryInsAddId);
                                                            if (szCheck2Ins1.Trim() == "0,0")
                                                            {
                                                                AddSecondaryIns(sz_Patient_Id, sz_Case_Id, szSecondryInsId, szSecondryInsAddId, ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                                //ThisLogger.Debug(" --Added Secondary INS -- ");
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

                                                }
                                                //ThisLogger.Debug(" --InsId,InsAddId -- " + insid);
                                            }
                                            else
                                            {
                                                if (sz_visit_ins == "0")
                                                {
                                                    string sz_policy_no = "";
                                                    string sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();

                                                    string sz_dao = ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString().Trim();
                                                    if (ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_policy_no = ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim();
                                                    }
                                                    if (ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();
                                                    }
                                                    //AddVisitInsInfo(sz_even_id_ins, sz_event_proc_id_ins, sz_ins_id, sz_ins_add_id, sz_Claim_no, sz_policy_no, "", sz_dao);
                                                    SaveInsForEvent(null, null, sz_event_proc_id_ins, sz_even_id_ins, sz_Case_Id, sz_Claim_no, sz_policy_no, ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["GFirstName"].ToString() + " " + ds.Tables[0].Rows[iCount]["GLastName"].ToString(), sz_dao, Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]));

                                                }
                                                else
                                                {
                                                    string sz_policy_no = "";
                                                    string sz_Claim_no = "";
                                                    string sz_dao = ds.Tables[0].Rows[iCount]["DateOfAccident"].ToString().Trim();
                                                    if (ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_policy_no = ds.Tables[0].Rows[iCount]["PolicyNumber"].ToString().Trim();
                                                    }
                                                    if (ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != "NULL" && ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim() != " ")
                                                    {
                                                        sz_Claim_no = ds.Tables[0].Rows[iCount]["ClaimNumber"].ToString().Trim();
                                                    }
                                                    //// UpdateVisitInsuranceInformation(sz_ins_id, sz_ins_add_id, sz_Claim_no, sz_policy_no, "", sz_dao, ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                    UpdateInsForEvent(null, null, ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), sz_Case_Id, sz_Claim_no, sz_policy_no, ds.Tables[0].Rows[iCount]["CaseType"].ToString(), ds.Tables[0].Rows[iCount]["GFirstName"].ToString() + " " + ds.Tables[0].Rows[iCount]["GLastName"].ToString(), sz_dao, ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), Convert.ToString(ds.Tables[0].Rows[iCount]["GAddress"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GCity"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GState"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GZip"]), Convert.ToString(ds.Tables[0].Rows[iCount]["GSSNO"]));

                                                }
                                                //ThisLogger.Debug("Insurance name not available");
                                            }

                                            if (ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim() != "" && ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim().ToLower() != " ")
                                            {
                                                string sz_AttorneyAddress = ds.Tables[0].Rows[iCount]["AttorneyAddr1"].ToString().Trim() + "," + ds.Tables[0].Rows[iCount]["AttorneyAddr2"].ToString().Trim();
                                                int iReturn = CheckAttorney(ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim(), "", sz_AttorneyAddress, ds.Tables[0].Rows[iCount]["AttorneyCity"].ToString().Trim(), ds.Tables[0].Rows[iCount]["AttorneyState"].ToString().Trim(), ds.Tables[0].Rows[iCount]["AttorneyZIP"].ToString().Trim(), sz_Case_Id);

                                                if (iReturn > 0)
                                                {
                                                    //ThisLogger.Debug(" -- Attorney added -- ");
                                                }
                                            }
                                            else
                                            {
                                                //ThisLogger.Debug(" -- failed to add attorney- ");
                                            }
                                        }
                                        else
                                        {
                                            string sz_location_id = "";
                                            AddLHRrVisit(ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), ds.Tables[0].Rows[iCount]["CaseID"].ToString(), ds.Tables[0].Rows[iCount]["FileName"].ToString(), ds.Tables[0].Rows[iCount]["PatientID"].ToString(), ds.Tables[0].Rows[iCount]["DateOfAppointment"].ToString(), "", "", ds.Tables[0].Rows[iCount]["Modality"].ToString(), "1", "1", ds.Tables[0].Rows[iCount]["CreatedDate"].ToString());
                                            // CheckTrans(eventID.ToString(), szBillProctype, ds.Tables[0].Rows[iCount]["ProcedureCode"].ToString(), ds.Tables[0].Rows[iCount]["ProcedureDesc"].ToString(), sz_Case_Id);
                                            if (ds.Tables[0].Rows[iCount]["BookFacility"].ToString() != "")
                                            {
                                                sz_location_id = CheckLoaction(ds.Tables[0].Rows[iCount]["BookFacility"].ToString());
                                                if (sz_location_id == "0")
                                                {
                                                    sz_location_not_found = sz_location_id + "Location " + ds.Tables[0].Rows[iCount]["BookFacility"].ToString() + "Note Found For Case:" + sz_Case_Id + "\n";
                                                    //ThisLogger.Debug(" --Laoction Not Found -- ");
                                                }
                                                else
                                                {
                                                    UpadteLocationID(sz_location_id, sz_Case_Id);
                                                }
                                            }
                                            if (ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim() != "" && ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim().ToLower() != "no insurance available" && ds.Tables[0].Rows[iCount]["InsuranceName"].ToString().Trim().ToLower() != " ")
                                            {
                                                string insid = GetIns(ds.Tables[0].Rows[iCount]["InsuranceName"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceAddress1"].ToString(), "", ds.Tables[0].Rows[iCount]["InsuranceCity"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceState"].ToString(), ds.Tables[0].Rows[iCount]["InsuranceZip"].ToString());
                                                string szCheckIns = CheckPatientIns(sz_Case_Id);
                                                string[] arrIns = insid.Split(',');
                                                string sz_ins_id = arrIns[0].ToString();
                                                string sz_ins_add_id = arrIns[1].ToString();
                                                if (szCheckIns.Trim() == "0,0")
                                                {
                                                    UpadteID(sz_ins_id, sz_ins_add_id, sz_Case_Id);
                                                    //ThisLogger.Debug(" --Update InsId,InsAddId ToCase -- ");
                                                }
                                                string szCheck2Ins = CheckSecondaryIns(sz_Case_Id, sz_ins_id, sz_ins_add_id);
                                                if (szCheck2Ins.Trim() == "0,0")
                                                {
                                                    AddSecondaryIns(sz_Patient_Id, sz_Case_Id, sz_ins_id, sz_ins_add_id, ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                    //ThisLogger.Debug(" --Added Secondary INS -- ");
                                                }
                                                //ThisLogger.Debug(" --InsId,InsAddId -- " + insid);

                                                try
                                                {
                                                    DataSet dsSwapIns = new DataSet();
                                                    dsSwapIns = GetSwapInsurance();
                                                    int iSwapFlag = 0;
                                                    for (int cntSwap = 0; cntSwap < dsSwapIns.Tables[0].Rows.Count; cntSwap++)
                                                    {
                                                        if (sz_ins_id == dsSwapIns.Tables[0].Rows[cntSwap]["SZ_INSURANCE_ID"].ToString())
                                                        {
                                                            iSwapFlag = 1;
                                                            break;
                                                        }
                                                    }

                                                    if (iSwapFlag == 0)
                                                    {
                                                        int iSwapFlag1 = 0;
                                                        DataSet dsGetIns = new DataSet();
                                                        dsGetIns = GetInsuranceCompanyID(sz_Case_Id);
                                                        string szOrignalID = dsGetIns.Tables[0].Rows[0][0].ToString();
                                                        string szSecondryInsId = "";
                                                        string szSecondryInsAddId = "";

                                                        for (int cntCheckInsID = 0; cntCheckInsID < dsSwapIns.Tables[0].Rows.Count; cntCheckInsID++)
                                                        {
                                                            if (szOrignalID == dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ID"].ToString())
                                                            {
                                                                iSwapFlag1 = 1;
                                                                szSecondryInsId = dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ID"].ToString();
                                                                szSecondryInsAddId = dsSwapIns.Tables[0].Rows[cntCheckInsID]["SZ_INSURANCE_ADDRESS_ID"].ToString();
                                                                break;
                                                            }
                                                        }
                                                        if (iSwapFlag1 == 1)
                                                        {
                                                            UpadteID(sz_ins_id, sz_ins_add_id, sz_Case_Id);
                                                            string szCheck2Ins1 = CheckSecondaryIns(sz_Case_Id, szSecondryInsId, szSecondryInsAddId);
                                                            if (szCheck2Ins1.Trim() == "0,0")
                                                            {
                                                                AddSecondaryIns(sz_Patient_Id, sz_Case_Id, szSecondryInsId, szSecondryInsAddId, ds.Tables[0].Rows[iCount]["PatientID"].ToString());
                                                                //ThisLogger.Debug(" --Added Secondary INS -- ");
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

                                                }


                                            }
                                            else
                                            {
                                                //ThisLogger.Debug("Ins Name Is not available");
                                            }

                                            if (ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim() != "" && ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim().ToLower() != " ")
                                            {
                                                string sz_AttorneyAddress = ds.Tables[0].Rows[iCount]["AttorneyAddr1"].ToString().Trim() + "," + ds.Tables[0].Rows[iCount]["AttorneyAddr2"].ToString().Trim();
                                                int iReturn = CheckAttorney(ds.Tables[0].Rows[iCount]["AttorneyName"].ToString().Trim(), "", sz_AttorneyAddress, ds.Tables[0].Rows[iCount]["AttorneyCity"].ToString().Trim(), ds.Tables[0].Rows[iCount]["AttorneyState"].ToString().Trim(), ds.Tables[0].Rows[iCount]["AttorneyZIP"].ToString().Trim(), sz_Case_Id);
                                                if (iReturn > 0)
                                                {

                                                    // ThisLogger.Debug(" -- attoreny added -- ");
                                                }
                                            }
                                            else
                                            {
                                                //ThisLogger.Debug(" -- fail to  add  attorney- ");

                                            }
                                        }
                                    }
                                    else
                                    {
                                        Messege = "failed to add Patient";
                                        //ThisLogger.Debug(" -- failed to add Patient-- ");
                                    }
                                }
                                else
                                {
                                    // ThisLogger.Debug( ds.Tables[0].Rows[iCount][0].ToString() +" Not Added because Case Type is not Match");
                                    //ThisLogger.Debug(ds.Tables[0].Rows[iCount]["FirstName"].ToString() + " Not Added because Case Type is not Match");
                                }
                            }
                            catch(Exception ex)
                            {
                                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            }
                        }//end of order number
                    }//for

                    if (sz_location_not_found != "")
                    {
                        //MessageBox.Show(sz_location_not_found);
                    }
                    Messege = "inserted";
                }
                else
                {
                    Messege = "No records found for the mentioned date";
                }


                dt = ds.Tables[0];
               // createcsv(dt, filepath);
                //if (chkAudit.Checked == false)
                //{
                //    MessageBox.Show("The output has been saved in: " + filepath);
                //}
            }
            else
            {
               
                Messege = "Invalid Parameter";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Messege = ex.Message.ToString();
        }
    }

    private string savePatient(string szFname, string szLname, string szDOB, string szAddress, string szCity, string szSate, string szZip, string szCaseID, string szPoliyNumber, string szDOA, string ClaimNumber, string CaseType, string SSNO, string szPolicyHolder, string szGender, string szPatientPhn, string sPolicyAddress, string sPolicyCity, string sPolicyState, string sPolicyZip, string sPolicySSN, string szMI)
    {
        //ThisLogger.Debug(" -- Call savePatient  -- ");
        string szSateID = GetSateId(szSate);
        //ThisLogger.Debug(" -- Patient Satae ID -- " + szSateID);
        string szReturn = "";



        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY_LHR", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", szFname);
                command.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", szLname);
                command.Parameters.AddWithValue("@MI", szMI);

                command.Parameters.AddWithValue("@DT_DOB", szDOB);

                command.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", szAddress);
                command.Parameters.AddWithValue("@SZ_PATIENT_CITY", szCity);
                command.Parameters.AddWithValue("@SZ_PATIENT_ZIP", szZip);

                command.Parameters.AddWithValue("@SZ_PATIENT_STATE_ID", szSateID);
                command.Parameters.AddWithValue("@sz_remote_case_id", szCaseID);
                command.Parameters.AddWithValue("@SZ_POLICY_NUMBER", szPoliyNumber);
                command.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", "CS000000000000000279");
                command.Parameters.AddWithValue("@SZ_PATIENT_PHONE", szPatientPhn);
                //  if (szDOA != "")
                if (szDOA != "" && szDOA != "NULL" && szDOA != null)
                {

                    try
                    {

                        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

                        dtfi.LongDatePattern = "mm/dd/yyyy";

                        DateTime dt = DateTime.ParseExact(szDOA, "mm/dd/yyyy", dtfi);

                        //DateTime dt = new DateTime();
                        dt = Convert.ToDateTime(szDOA);
                        szDOA = dt.ToString();
                    }

                    catch (Exception ex)
                    {
                        try
                        {
                            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

                            dtfi.LongDatePattern = "m/dd/yyyy";

                            DateTime dt = DateTime.ParseExact(szDOA, "m/dd/yyyy", dtfi);
                            dt = Convert.ToDateTime(szDOA);
                            szDOA = dt.ToString();

                        }
                        catch
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            szDOA = "";
                        }

                    }

                    command.Parameters.AddWithValue("@DT_ACCIDENT_DATE", szDOA);
                }
                command.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", ClaimNumber);
                if (CaseType == "WORKERS CO")
                {
                    command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000209");
                }
                else if (CaseType == "NO-FAULT")
                {
                    command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000208");
                }
                else if (CaseType == "LIEN")
                {
                    command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000211");
                }
                command.Parameters.AddWithValue("@SZ_COMPANY_ID ", "CO000000000000000107");

                command.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", SSNO);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER", szPolicyHolder);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", sPolicyAddress);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", sPolicyCity);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", sPolicyState);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", sPolicyZip);
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_SSN", sPolicySSN);

                switch (szGender.ToLower().Trim())
                {
                    case "m":
                        command.Parameters.AddWithValue("@SZ_GENDER", "Male");
                        break;
                    case "f":
                        command.Parameters.AddWithValue("@SZ_GENDER", "Female");
                        break;
                }
                command.Parameters.AddWithValue("@FLAG", "ADD");
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                szReturn = ds.Tables[0].Rows[0][0].ToString();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed to add Patient-- ");
            //ThisLogger.Debug(ex.ToString());
        }

        return szReturn;
    }

    private string GetProcType(string szModality)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szSateId = "";
        try
        {
            //ThisLogger.Debug(" -- SP_GET_STATE_ID  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_PROC_TYPE_CODE", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_MODALITY", szModality);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");


            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szSateId = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szSateId = "";
            //ThisLogger.Debug(" -- failed get State id -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szSateId;
    }

    private string GetSateId(string StateCode)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szSateId = "";
        try
        {
            //ThisLogger.Debug(" -- SP_GET_STATE_ID  SZ_STATE_CODE   -- " + StateCode);
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_STATE_ID", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_STATE_CODE", StateCode);

            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szSateId = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szSateId = "";
            //ThisLogger.Debug(" -- failed get State id -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szSateId;
    }

    protected void createcsv(DataTable dt, string strFilePath)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);
        int iColCount = dt.Columns.Count;
        for (int i = 0; i < iColCount; i++)
        {
            sw.Write(dt.Columns[i]);
            if (i < iColCount - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    if (dr[i].ToString().Trim() == "")
                        sw.Write("NULL");
                    else
                        sw.Write(dr[i].ToString().Replace(',', ' '));
                }
                else
                {
                    sw.Write("NULL");
                }
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }

    private string getLhrReadingDoctor_(string szDocName)
    {
        string returnValue = string.Empty;
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SP_GET_ZP_READING_DOCTOR", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SZ_DOCTOR_NAME", szDocName);
                command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
                command.Connection = conn;
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    returnValue = dr[0].ToString();
                }
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug("Error in getting OT Proc Type " + ex.Message);
        }
        return returnValue;
    }

    private string GetRadingDoctor(string sz_Doc_Name, string sz_license_no, string sz_office_id, string sz_Modality)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szRadingDoctor = "";

        try
        {
            //ThisLogger.Debug(" -- SP_GET_STATE_ID  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_READING_DOCTOR_FOR_ZWANGER", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doc_Name);
            command.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_license_no);
            command.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_office_id);
            command.Parameters.AddWithValue("@SZ_MODALITY", sz_Modality);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");


            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szRadingDoctor = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szRadingDoctor = "";
            //ThisLogger.Debug(" -- failed get RadingDoctor -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szRadingDoctor;
    }

    private string GetDocId(string sz_patient_name, string sz_license_no, string sz_modality, string sz_office_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szSateId = "";
        try
        {
            //ThisLogger.Debug(" -- SP_GET_LHR_DOCTOR_ID  SZ_STATE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_LHR_DOCTOR_ID", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_patient_name);
            command.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_license_no);
            command.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_office_id);
            command.Parameters.AddWithValue("@SZ_MODALITY", sz_modality);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szSateId = dr[0].ToString() + "," + dr[1].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szSateId = "";
            //ThisLogger.Debug(" -- failed get State id -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szSateId;
    }

    private string CheckLHRrVisit(string sz_proc_code, string sz_remote_proc_desc, string sz_remote_appointment_id, string sz_doc, string sz_remote_case_id, string sz_visit_date, string sz_visit_time, string sz_visit_time_type, string sz_Modality, string readingDoc)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szRadingDoctor = "";

        try
        {
            //ThisLogger.Debug(" -- SP_CHECK_LHR_VISIT   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_CHECK_LHR_VISIT", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_CODE", sz_proc_code);

            command.Parameters.AddWithValue("@SZ_REMOTE_DOCUMENT", sz_doc);
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_DESC", sz_remote_proc_desc);
            command.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", sz_remote_appointment_id);
            command.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID", sz_remote_case_id);
            command.Parameters.AddWithValue("@DT_VISIT_DATE", sz_visit_date);
            command.Parameters.AddWithValue("@SZ_VISIT_TIME", sz_visit_time);
            command.Parameters.AddWithValue("@SZ_VISIT_TIME_TYPE", sz_visit_time_type);
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_GROUP", sz_Modality);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            if (readingDoc.Trim() != "")
            {
                command.Parameters.AddWithValue("@sz_reading_doctor_id", readingDoc);
            }

            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szRadingDoctor = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szRadingDoctor = "";
            //ThisLogger.Debug(" -- failed SP_CHECK_LHR_VISIT -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szRadingDoctor;
    }

    public int Save_Event(ArrayList onjAdd, string userid)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            conn.Open();
            ArrayList _return = new ArrayList();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandText = "SP_TXN_CALENDAR_EVENT";
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = conn;
            command.Parameters.AddWithValue("@SZ_CASE_ID ", onjAdd[0].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            command.Parameters.AddWithValue("@SZ_EVENT_NOTES", onjAdd[3].ToString());
            command.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            command.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            command.Parameters.AddWithValue("@SZ_REFERENCE_ID", onjAdd[10].ToString());
            command.Parameters.AddWithValue("@BT_STATUS", onjAdd[11].ToString());

            // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin       

            command.Parameters.AddWithValue("@BT_TRANSPORTATION", onjAdd[12]);
            //========================================================================================================
            // 15 April 2010 add coloumn in txn_calender_event table and save value in that table -- sachin
            command.Parameters.AddWithValue("@I_TRANSPORTATION_COMPANY", onjAdd[13]);
            //========================================================================================================
            if (onjAdd.Count > 14)
            {
                command.Parameters.AddWithValue("@SZ_OFFICE_ID", onjAdd[14].ToString());
            }

            //if (onjAdd.Count >= 13) { command.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[13].ToString()); }
            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
            parmReturnValue.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parmReturnValue);
            command.Parameters.AddWithValue("@FLAG", "ADD2");
            command.Parameters.AddWithValue("@SZ_USER_ID", userid);

            command.ExecuteNonQuery();
            returnvalue = (int)command.Parameters["@RETURN"].Value;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" --Error To Add Evenet Id-- ");

        }
        finally { conn.Close(); }
        return returnvalue;
    }

    protected void Update_Event(int EventID, string CaseID, string CompanyID)
    {
        //ThisLogger.Debug(" -- Update Event Status to complete  -- ");
        //ThisLogger.Debug(" -- Event ID -- " + EventID);

        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SP_CHANGE_LHR_EVENT_STATUS", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@I_EVENT_ID", EventID);
                command.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
                command.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed to update event status-- ");
            //ThisLogger.Debug(ex.ToString());
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public void Save_Event_RefferPrcedure(ArrayList onjAdd, string readingDoc)
    {
        try
        {
            conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            conn.Open();
            ArrayList _return = new ArrayList();

            SqlCommand comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE ", onjAdd[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", onjAdd[1].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", onjAdd[2].ToString());
            if (onjAdd.Count >= 4 && onjAdd[3].ToString() != "") { comm.Parameters.AddWithValue("@DT_RESCHEDULE_DATE", onjAdd[3].ToString()); }
            if (onjAdd.Count >= 5) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME", onjAdd[4].ToString()); }
            if (onjAdd.Count >= 6) { comm.Parameters.AddWithValue("@DT_RESCHEDULE_TIME_TYPE", onjAdd[5].ToString()); }
            if (onjAdd.Count >= 7) { comm.Parameters.AddWithValue("@I_RESCHEDULE_ID", onjAdd[6].ToString()); }
            if (onjAdd.Count >= 8) { comm.Parameters.AddWithValue("@SZ_STUDY_NUMBER", onjAdd[7].ToString()); }
            if (onjAdd.Count >= 9) { comm.Parameters.AddWithValue("@SZ_NOTES", onjAdd[8].ToString()); }
            if (readingDoc != null)
            {
                if (readingDoc.Trim() != "")
                    comm.Parameters.AddWithValue("@sz_reading_Doc_id", readingDoc);

            }

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" --Erro to add Proc ID -- ");

        }
        finally { conn.Close(); }
    }

    private string GetEventProcID(string sz_Event_ID)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szRadingDoctor = "";

        try
        {
            //ThisLogger.Debug(" -- SP_GET_EVENT_PROC_ID  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_EVENT_PROC_ID", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EVENT_ID", sz_Event_ID);
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szRadingDoctor = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szRadingDoctor = "";
            //ThisLogger.Debug(" -- failed SP_ADD_LHR_VISIT -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szRadingDoctor;
    }

    private void UpadteImportOn(string sz_event_proc_id, string sz_lhr_code, string sz_event_id, string sz_case_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szRadingDoctor = "";

        try
        {
            //ThisLogger.Debug(" -- AddVisitInsInfo   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_INT_UPDATE_IMPORT_ON", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_EVENT_PROC_ID", sz_event_proc_id);
            command.Parameters.AddWithValue("@SZ_EVENT_ID", sz_event_id);
            command.Parameters.AddWithValue("@SZ_LHR_CODE", sz_lhr_code);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            command.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szRadingDoctor = "";
            //ThisLogger.Debug(" -- failed AddVisitInsInfo -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    private void AddLHRrVisit(string sz_proc_code, string sz_proc_desc, string sz_appointment_id, string sz_doc, string sz_remote_case_id, string sz_visit_date, string sz_visit_time, string sz_visit_time_type, string sz_Modality, string sz_Event_ID, string sz_Bill_ProcType, string szCreatedDate)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            //ThisLogger.Debug(" -- SP_ADD_LHR_VISIT  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_ADD_LHR_VISIT", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_CODE", sz_proc_code);
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_DESC", sz_proc_desc);
            command.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", sz_appointment_id);
            command.Parameters.AddWithValue("@SZ_REMOTE_DOCUMENT", sz_doc);
            command.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID", sz_remote_case_id);
            command.Parameters.AddWithValue("@DT_VISIT_DATE", sz_visit_date);
            command.Parameters.AddWithValue("@SZ_VISIT_TIME", sz_visit_time);
            command.Parameters.AddWithValue("@SZ_VISIT_TIME_TYPE", sz_visit_time_type);
            command.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_GROUP", sz_Modality);
            command.Parameters.AddWithValue("@EVENT_ID", sz_Event_ID);
            command.Parameters.AddWithValue("@EVENT_PROC_ID", sz_Bill_ProcType);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            command.Parameters.AddWithValue("@CREATED_DATE", szCreatedDate);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed SP_ADD_LHR_VISIT -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        finally
        {
            conn.Close();
        }

    }

    public void CheckTrans(string sz_EventID, string sz_EventProcId, string sz_ProcedureCode, string sz_ProcedureDesc, string sz_CaseID)
    {
        try
        {
            //ThisLogger.Debug(" -- CheckTrans   -- ");
            string sz_Result = CheckEventTransaction(sz_EventID, sz_EventProcId);
            if (sz_Result == "")
            {
                //ThisLogger.Debug(" -- failed CheckEventTransaction get result empty -- ");
            }
            else if (sz_Result == "YES")
            {
                //ThisLogger.Debug(" -- evenet added successfully-- ");
            }
            else
            {
                DataRow dr = dtEvents.NewRow();
                dr["CaseID"] = sz_CaseID;
                dr["EventId"] = sz_EventID;
                dr["EventProcId"] = sz_EventProcId;
                dr["ProcedureCode"] = sz_ProcedureCode;
                dr["ProcedureDesc"] = sz_ProcedureDesc;
                dr["Error"] = sz_CaseID;
                dtEvents.Rows.Add(dr);


                //ThisLogger.Debug(" -- evenet added with error-- ");
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed CheckTrans -- ");
            //ThisLogger.Debug(ex.ToString());
        }
    }

    private string CheckEventTransaction(string sz_EventID, string sz_EventProcId)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szReturn = "";
        try
        {
            //ThisLogger.Debug(" -- CheckEventTransaction   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_INT_CHECK_EVENT_TRANSCATION", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@I_EVENT_ID", sz_EventID);
            command.Parameters.AddWithValue("@I_EVENT_PROC_ID", sz_EventProcId);
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
            dr.Close();

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed CheckEventTransaction -- ");
            //ThisLogger.Debug(ex.ToString());
        }
        return szReturn;
    }

    private string CheckLoaction(string sz_Location_Name)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szRadingDoctor = "";

        try
        {
            //ThisLogger.Debug(" -- SP_CHECK_LOCATION  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_CHECK_LOCATION", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_LOCATION_NAME", sz_Location_Name);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szRadingDoctor = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szRadingDoctor = "";
            //ThisLogger.Debug(" -- failed SP_CHECK_LOCATION -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szRadingDoctor;
    }

    private void UpadteLocationID(string sz_location_id, string sz_case_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            //ThisLogger.Debug(" -- SP_UPDATE_CASE_MASTER_LOCATION  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_UPDATE_CASE_MASTER_LOCATION", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_LOCATION_ID", sz_location_id);
            command.Parameters.AddWithValue("@SZ_CASEID", sz_case_id);
            command.Parameters.AddWithValue("@SZ_COPMANY_ID", "CO000000000000000107");
            command.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed SP_UPDATE_CASE_MASTER_LOCATION -- ");
            //ThisLogger.Debug(ex.ToString());

        }

    }

    private void SaveLocationForEvent(string sz_location_id, string sz_event_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            //ThisLogger.Debug(" -- sp_save_location_for_event  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("sp_save_location_for_event", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_LOCATIONID", sz_location_id);
            command.Parameters.AddWithValue("@I_EVENT_ID", sz_event_id);
            command.Parameters.AddWithValue("@SZ_COPMANY_ID", "CO000000000000000107");
            command.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed SP_UPDATE_CASE_MASTER_LOCATION -- ");
            //ThisLogger.Debug(ex.ToString());

        }

    }

    protected void updateOldVisits(ArrayList al)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("sp_txn_update_lhr_visits", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@sz_remote_case_id", al[0].ToString());
                command.Parameters.AddWithValue("@sz_remote_appointment_id", al[1].ToString());
                command.Parameters.AddWithValue("@sz_company_id", al[2].ToString());
                command.Parameters.AddWithValue("@dt_visit_date", al[3].ToString());
                command.Parameters.AddWithValue("@dt_event_time", al[4].ToString());
                command.Parameters.AddWithValue("@dt_event_time_type", al[5].ToString());
                command.Parameters.AddWithValue("@dt_Event_end_time", al[6].ToString());
                command.Parameters.AddWithValue("@dt_Event_end_time_type", al[7].ToString());
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug("Error in Updating old visits for appointment id " + al[1].ToString() + " " + ex.Message);
        }
    }

    protected void updateOldVisitsForEvent(ArrayList al)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("sp_update_location_for_event", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@sz_remote_case_id", al[0].ToString());
                command.Parameters.AddWithValue("@sz_remote_appointment_id", al[1].ToString());
                command.Parameters.AddWithValue("@sz_company_id", al[2].ToString());
                command.Parameters.AddWithValue("@dt_visit_date", al[3].ToString());
                command.Parameters.AddWithValue("@dt_event_time", al[4].ToString());
                command.Parameters.AddWithValue("@dt_event_time_type", al[5].ToString());
                command.Parameters.AddWithValue("@dt_Event_end_time", al[6].ToString());
                command.Parameters.AddWithValue("@dt_Event_end_time_type", al[7].ToString());
                command.Parameters.AddWithValue("@sz_location_id", al[8].ToString());
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug("Error in Updating old visits for appointment id " + al[1].ToString() + " " + ex.Message);
        }
    }

    private string GetRadingOffice(string sz_office, string sz_addres, string sz_state, string sz_city, string sz_zip)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string szOfficId = "";
        string szSateID = GetSateId(sz_state);
        //ThisLogger.Debug(" -- Patient Satae ID -- " + szSateID);
        try
        {
            //ThisLogger.Debug(" -- SP_GET_STATE_ID  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_CHECK_OFFICE_READING_DOCTOR", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_OFFICE_NAME", sz_office);
            command.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS", sz_addres);
            command.Parameters.AddWithValue("@SZ_OFFICE_CITY", sz_city);
            command.Parameters.AddWithValue("@SZ_OFFICE_ZIP", sz_zip);
            command.Parameters.AddWithValue("@SZ_OFFICE_STATE_ID", szSateID);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");


            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szOfficId = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szOfficId = "";
            //ThisLogger.Debug(" -- failed get szOfficId -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return szOfficId;
    }

    private string getLhrOtProcCode()
    {
        string returnValue = string.Empty;
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SP_GET_LHR_OT_PROC_CODE", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                returnValue = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug("Error in getting OT Proc Type " + ex.Message);
        }
        return returnValue;
    }

    private int Check_Event(ArrayList onjAdd)
    {
        int returnvalue = 0;
        try
        {
            conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            conn.Open();
            ArrayList _return = new ArrayList();
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandText = "SP_TXN_CALENDAR_EVENT_CHECK";
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = conn;
            command.Parameters.AddWithValue("@SZ_PATIENT_ID ", onjAdd[0].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_DATE", onjAdd[1].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_TIME", onjAdd[2].ToString());
            command.Parameters.AddWithValue("@SZ_DOCTOR_ID", onjAdd[4].ToString());
            command.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", onjAdd[5].ToString());
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", onjAdd[6].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", onjAdd[7].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_END_TIME", onjAdd[8].ToString());
            command.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", onjAdd[9].ToString());
            command.Parameters.AddWithValue("@SZ_REFERENCE_ID", onjAdd[10].ToString());
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
                returnvalue = Int32.Parse(dr[0].ToString());
            else
                returnvalue = 0;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" --Error To Check Evenet Id-- ");

        }
        finally { conn.Close(); }
        return returnvalue;
    }

    private string GetIns(string sz_Ins_Name, string sz_ins_add, string ins_street, string ins_city, string ins_state, string ins_zip)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        //ThisLogger.Debug(" -- Call savePatient  -- ");
        string szSateID = GetSateId(ins_state);
        //ThisLogger.Debug(" -- Patient Satae ID -- " + szSateID);
        string insid = "";
        try
        {
            //ThisLogger.Debug(" -- SP_CHECK_INSURANCE  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_CHECK_INSURANCE_ZWANGER", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_INSURANCE_NAME", sz_Ins_Name);
            command.Parameters.AddWithValue("@SZ_INSURANCE_ADDRESS", sz_ins_add);
            command.Parameters.AddWithValue("@SZ_INSURANCE_STREET", ins_street);

            command.Parameters.AddWithValue("@SZ_INSURANCE_CITY", ins_city);
            command.Parameters.AddWithValue("@SZ_INSURANCE_STATE", ins_state);
            command.Parameters.AddWithValue("@SZ_INSURANCE_ZIP", ins_zip);
            command.Parameters.AddWithValue("@SZ_INSURANCE_STATE_ID", szSateID);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");


            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                insid = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            insid = "";
            //ThisLogger.Debug(" -- failed get GetIns -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return insid;
    }

    private string CheckPatientIns(string sz_case_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string sz_ins_id = "";

        try
        {
            //ThisLogger.Debug(" -- SP_GET_LHR_INS_INFO  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_LHR_INS_INFO", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                sz_ins_id = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sz_ins_id = "";
            //ThisLogger.Debug(" -- failed SP_GET_LHR_INS_INFO -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return sz_ins_id;
    }

    private void SaveInsForEvent(string sz_ins_id, string sz_ins_add_id, string sz_evenet_proc_id, string sz_evene_id, string sz_caseid, string sz_claim_no, string sz_policy_no, string sz_case_type, string sz_policy_holder, string dao, string sPolicyAddress, string sPolicyCity, string sPolicyState, string sPolicyZip, string sPolicySSN)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            //ThisLogger.Debug(" -- sp_int_save_event_ins   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("sp_int_save_event_ins", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_ins_id);
            command.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_ins_add_id);
            command.Parameters.AddWithValue("@SZ_CAS_ID", sz_caseid);
            command.Parameters.AddWithValue("@SZ_EVENT_PROC_ID", sz_evenet_proc_id);
            command.Parameters.AddWithValue("@SZ_EVENT_ID", sz_evene_id);
            command.Parameters.AddWithValue("@SZ_CLAIM_NO ", sz_claim_no);
            command.Parameters.AddWithValue("@SZ_POLICY_NO ", sz_policy_no);
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", sPolicyAddress);
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", sPolicyCity);
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", sPolicyState);
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", sPolicyZip);
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_SSN", sPolicySSN);

            if (sz_case_type == "WORKERS CO")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000209");
            }
            else if (sz_case_type == "NO-FAULT")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000208");
            }
            else if (sz_case_type == "LIEN")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000211");
            }
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER", sz_policy_holder);
            command.Parameters.AddWithValue("@DAO", dao);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed sp_int_check_and_save_visit_ins_info -- ");
            //ThisLogger.Debug(ex.ToString());
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    private void UpdateInsForEvent(string sz_ins_id, string sz_ins_add_id, string sz_remote_case_id, string sz_remote_appo_id, string sz_caseid, string sz_claim_no, string sz_policy_no, string sz_case_type, string sz_policy_holder, string dao, string sz_lhr_code, string sPolicyAddress, string sPolicyCity, string sPolicyState, string sPolicyZip, string sPolicySSN)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            //ThisLogger.Debug(" -- sp_int_update_event_ins   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("sp_int_update_event_ins", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID", sz_remote_case_id);
            command.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", sz_remote_appo_id);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            command.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_ins_id);
            command.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_ins_add_id);
            command.Parameters.AddWithValue("@SZ_CAS_ID", sz_caseid);
            command.Parameters.AddWithValue("@SZ_CLAIM_NO ", sz_claim_no);
            command.Parameters.AddWithValue("@SZ_POLICY_NO ", sz_policy_no);
            command.Parameters.AddWithValue("@SZ_NEW_POLICY_HOLDER_ADDRESS", sPolicyAddress);
            command.Parameters.AddWithValue("@SZ_NEW_POLICY_HOLDER_CITY", sPolicyCity);
            command.Parameters.AddWithValue("@SZ_NEW_POLICY_HOLDER_STATE", sPolicyState);
            command.Parameters.AddWithValue("@SZ_NEW_POLICY_HOLDER_ZIP", sPolicyZip);
            command.Parameters.AddWithValue("@SZ_NEW_POLICY_HOLDER_SSN", sPolicySSN);

            if (sz_case_type == "WORKERS CO")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000209");
            }
            else if (sz_case_type == "NO-FAULT")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000208");
            }
            else if (sz_case_type == "LIEN")
            {
                command.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", "CT000000000000000211");
            }
            command.Parameters.AddWithValue("@SZ_POLICY_HOLDER", sz_policy_holder);
            command.Parameters.AddWithValue("@DAO", dao);
            command.Parameters.AddWithValue("@DT_IMPORT_FOR ", DateTime.Now.ToString("MM/dd/yyyy"));
            command.Parameters.AddWithValue("@SZ_LHR_CODE", sz_lhr_code);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed sp_int_check_and_save_visit_ins_info -- ");
            //ThisLogger.Debug(ex.ToString());
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    private void UpadteID(string sz_ins_id, string sz_ins_add_id, string sz_case_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            //ThisLogger.Debug(" -- SP_GET_STATE_ID  SP_GET_PROC_TYPE_CODE   -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_UPDATE_CASE_MASTER", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_ins_add_id);
            command.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_ins_id);
            command.Parameters.AddWithValue("@SZ_CASEID", sz_case_id);

            command.Parameters.AddWithValue("@SZ_COPMANY_ID", "CO000000000000000107");
            command.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed UpadteID -- ");
            //ThisLogger.Debug(ex.ToString());

        }

    }

    private string CheckSecondaryIns(string sz_case_id, string sz_ins_id, string sz_ins_add_id)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string sz_ins2_id = "";

        try
        {
            //ThisLogger.Debug(" -- SP_GET_LHR_SECOND_INS_INFO  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_GET_LHR_SECOND_INS_INFO", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            command.Parameters.AddWithValue("@SZ_INS_ID", sz_ins_id);
            command.Parameters.AddWithValue("@SZ_INS_ADD_ID", sz_ins_add_id);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                sz_ins2_id = dr[0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sz_ins_id = "";
            //ThisLogger.Debug(" -- failed SP_GET_LHR_SECOND_INS_INFO -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        return sz_ins2_id;
    }

    private void AddSecondaryIns(string szPatientId, string szCaseID, string szInsId, string szInsAddId, string szRemoteCasID)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());


        try
        {
            //ThisLogger.Debug(" -- SP_ADD_LHR_VISIT  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("SP_ADD_SECONDARY_INS", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientId);
            command.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            command.Parameters.AddWithValue("@SZ_INS_ID", szInsId);
            command.Parameters.AddWithValue("@SZ_INS_ADD_ID", szInsAddId);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            command.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID", szRemoteCasID);
            command.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //ThisLogger.Debug(" -- failed SP_ADD_LHR_VISIT -- ");
            //ThisLogger.Debug(ex.ToString());

        }
        finally
        {
            conn.Close();
        }

    }

    private DataSet GetSwapInsurance()
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            //ThisLogger.Debug(" -- GetSwapIns  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("sp_int_get_swap_insurance_id", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //procgroup = "";
            //ThisLogger.Debug(" -- failed SP_GET_OT_ID -- ");
            //ThisLogger.Debug(ex.ToString());
        }
        return ds;
    }

    private DataSet GetInsuranceCompanyID(string szCaseID)
    {
        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            //ThisLogger.Debug(" -- GetIns  -- ");
            conn.Open();
            SqlCommand command = new SqlCommand("sp_int_get_insurance_company", conn);
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //procgroup = "";
            //ThisLogger.Debug(" -- failed SP_GET_OT_ID -- ");
            //ThisLogger.Debug(ex.ToString());
        }
        return ds;
    }

    private int CheckAttorney(string szFname, string szLname, string szAddress, string szCity, string szSate, string szZip, string szCaseID)
    {

        //ThisLogger.Debug(" -- Call SP_CHECK_ATTORNEY  -- ");
        string szSateID = GetSateId(szSate);
        //ThisLogger.Debug(" -- Patient Satae ID -- " + szSateID);
        int szReturn = 0;


        conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {



            if (conn != null)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SP_CHECK_ATTORNEY", conn);
                command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", szFname);
                command.Parameters.AddWithValue("@SZ_ATTORNEY_LAST_NAME", szLname);
                command.Parameters.AddWithValue("@SZ_ATTORNEY_CITY", szCity);

                command.Parameters.AddWithValue("@SZ_ATTORNEY_STATE", szSate);
                command.Parameters.AddWithValue("@SZ_ATTORNEY_STATE_ID", szSateID);
                command.Parameters.AddWithValue("@SZ_ATTORNEY_ZIP", szZip);
                command.Parameters.AddWithValue("@SZ_ATTORNEY_ADDRESS", szAddress);
                command.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
                command.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO000000000000000107");



                szReturn = command.ExecuteNonQuery();
                conn.Close();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = 0;
            //ThisLogger.Debug(" -- failed to add attorney-- ");
            //ThisLogger.Debug(ex.ToString());
        }

        return szReturn;
    }

}