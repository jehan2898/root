using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;


/// <summary>
/// Summary description for PatientService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class PatientService : System.Web.Services.WebService
{
    SqlConnection sqlCon;
    DataSet ds;
    String strsqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    public PatientService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    // [WebMethod]
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetPatient(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {

            string[] strfirst = prefixText.Split(' ');
            string strFirstName = strfirst[0];
            string strLastName = null;
            if (strfirst != null)
            {
                if (strfirst.Length > 1)
                {
                    strLastName = strfirst[1];
                }
            }
            DataTable dtIns = PatientRecord(strFirstName, strLastName, contextKey);


            for (int i = 0; i < dtIns.Rows.Count; i++)
            {
                string strName = dtIns.Rows[i][0].ToString();
                Ins.Add(strName);
            }
            if (Ins.Count == 0)
            {
                Ins.Insert(0, "No suggestions found for your search");
            }
            return Ins.ToArray();


        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }
        // return lst.ToArray();
        // return items.ToArray();

        //string[] r = new string[2];
        //r.SetValue("John Smith", 0);
        //r.SetValue("Smith John", 1);
        //return r;
    }
    public DataTable PatientRecord(string strFName, string strLName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FNAME", strFName);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LNAME", strLName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "PATIENT");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State != ConnectionState.Closed)
            {
                sqlCon.Close();
            }
        }
        return ds.Tables[0];

    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetInsurance(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = InsuranceRecord(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtIns.Rows[i][0].ToString(),
                       dtIns.Rows[i][1].ToString());
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }

    //for insurance company search on Bill_Sys_Patient page.
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetInsurance_With_Addr(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = InsuranceWith_AddrRecord(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtIns.Rows[i][0].ToString(),
                       dtIns.Rows[i][1].ToString());
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetInsuranceLHR(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = InsuranceRecord_LHR(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtIns.Rows[i][0].ToString(),
                       dtIns.Rows[i][1].ToString());
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] GetAttorney(string prefixText, int count, string contextKey)
    {

        List<String> Attorney = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtAttorney = AttorneyRecord(prefixText, contextKey);
            if (dtAttorney.Rows.Count > 0)
            {
                for (int i = 0; i < dtAttorney.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtAttorney.Rows[i][0].ToString(),
                       dtAttorney.Rows[i][1].ToString());

                    Attorney.Add(strName);
                }
                if (Attorney.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Attorney.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Attorney.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Attorney.Add(strnotfound);
                return Attorney.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Attorney.ToArray();
        }

    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetEmployer(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = EmployerRecord(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtIns.Rows[i][0].ToString(),
                       dtIns.Rows[i][1].ToString());
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }


    public DataTable InsuranceRecord(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", strName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "INS");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }

    public DataTable InsuranceRecord_LHR(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", strName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "INS_LHR");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }

    public DataTable AttorneyRecord(string szattorneyfirstname, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", szattorneyfirstname);
            //sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_LAST_NAME", szattorneylastname);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ATTORNEY");
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }



    public DataTable AdjusterRecord(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", strName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADJUSTER");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }


    public DataTable InsuranceWith_AddrRecord(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", strName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "INS_WITH_ADDR");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetPatientDoctorWise(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            string[] ContextKey = contextKey.Split(',');
            string[] strfirst = prefixText.Split(' ');
            string strFirstName = strfirst[0];
            string strLastName = null;
            if (strfirst != null)
            {
                if (strfirst.Length > 1)
                {
                    strLastName = strfirst[1];
                }
            }
            DataTable dtIns = PatientRecordDoctorWise(strFirstName, strLastName, ContextKey[0], ContextKey[1]);


            for (int i = 0; i < dtIns.Rows.Count; i++)
            {
                string strName = dtIns.Rows[i][0].ToString();
                Ins.Add(strName);
            }
            if (Ins.Count == 0)
            {
                Ins.Insert(0, "No suggestions found for your search");
            }
            return Ins.ToArray();


        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }
        // return lst.ToArray();
        // return items.ToArray();

        //string[] r = new string[2];
        //r.SetValue("John Smith", 0);
        //r.SetValue("Smith John", 1);
        //return r;
    }
    public DataTable PatientRecordDoctorWise(string strFName, string strLName, string CompanyId, string DoctorId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();


        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE_DOCTOR_WISE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FNAME", strFName);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LNAME", strLName);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", DoctorId);


            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State != ConnectionState.Closed)
            {
                sqlCon.Close();
            }
        }
        return ds.Tables[0];

    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProvider(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = ProviderRecord(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = dtIns.Rows[i][0].ToString();
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }
    public DataTable ProviderRecord(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_OFFICE_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@OFFICE_NAME", strName);


            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }


    // get adjuster
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetAdjuster(string prefixText, int count, string contextKey)
    {

        List<String> Ins = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtIns = AdjusterRecord(prefixText, contextKey);
            if (dtIns.Rows.Count > 0)
            {
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    //string strName = dtIns.Rows[i][0].ToString();
                    string strName = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (dtIns.Rows[i][0].ToString(),
                       dtIns.Rows[i][1].ToString());
                    Ins.Add(strName);
                }
                if (Ins.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Ins.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Ins.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Ins.Add(strnotfound);
                return Ins.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Ins.ToArray();
        }

    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProcCodeDescription(string prefixText, int count, string contextKey)
    {

        List<String> Desc = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            DataTable dtCodeDesc = GetCodeDescriptionRecord(prefixText, contextKey);
            if (dtCodeDesc.Rows.Count > 0)
            {
                for (int i = 0; i < dtCodeDesc.Rows.Count; i++)
                {
                    string sz_code_description = dtCodeDesc.Rows[i][0].ToString();
                    Desc.Add(sz_code_description);
                }
                if (Desc.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Desc.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Desc.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Desc.Add(strnotfound);
                return Desc.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Desc.ToArray();
        }

    }
    public DataTable GetCodeDescriptionRecord(string sz_code_description, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_CODE_DESCRIPTION_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", sz_code_description);


            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetDignosisCodes(string prefixText, int count, string contextKey)
    {

       List<String> Desc = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            string[] ContextKey = contextKey.Split(',');
            DataTable dtCodeDesc = GetDignosisCodesDesc(prefixText, ContextKey[0], ContextKey[1]);
            if (dtCodeDesc.Rows.Count > 0)
            {
                for (int i = 0; i < dtCodeDesc.Rows.Count; i++)
                {
                    string Code = dtCodeDesc.Rows[i][0].ToString() + "~" + dtCodeDesc.Rows[i][1].ToString() + "-" + dtCodeDesc.Rows[i][2].ToString() + "~" + dtCodeDesc.Rows[i][3].ToString();
                    Desc.Add(Code);
                }
                if (Desc.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Desc.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Desc.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Desc.Add(strnotfound);
                return Desc.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Desc.ToArray();
        }

    }
    public DataTable GetDignosisCodesDesc(string sz_search, string CompanyId, string sz_type_id)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCmd = new SqlCommand("sp_lookup_diagnosis", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyId);
            sqlCmd.Parameters.AddWithValue("@sz_search", sz_search);
            sqlCmd.Parameters.AddWithValue("@sz_type_id", sz_type_id);
            sqlCmd.Connection = sqlCon;
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GePopuptDignosisCodes(string prefixText, int count, string contextKey)
    {

        List<String> Desc = new List<String>();
        DataSet returnState = new DataSet();
        try
        {
            string[] ContextKey = contextKey.Split(',');
            DataTable dtCodeDesc = GetPopupDignosisCodesDesc(prefixText, ContextKey[0], ContextKey[1]);
            if (dtCodeDesc.Rows.Count > 0)
            {
                for (int i = 0; i < dtCodeDesc.Rows.Count; i++)
                {
                    string Code = dtCodeDesc.Rows[i][0].ToString() + "~" + dtCodeDesc.Rows[i][1].ToString() + "~" + dtCodeDesc.Rows[i][2].ToString() + "~" + dtCodeDesc.Rows[i][3].ToString();
                    Desc.Add(Code);
                }
                if (Desc.Count == 0)
                {
                    string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       ("No suggestions found for your search",
                       "");
                    Desc.Add(strnotfound);
                    //Ins.Insert(0, "No suggestions found for your search");
                }
                return Desc.ToArray();
            }

            else
            {
                string strnotfound = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                      ("No suggestions found for your search",
                      "0");
                Desc.Add(strnotfound);
                return Desc.ToArray();
            }

        }
        catch (Exception ex)
        {
            //log.Debug("Page Unloaded : " + ex.Message);
            //log.Debug("Page Unloaded : " + ex.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnState = null;
            return Desc.ToArray();
        }

    }
    public DataTable GetPopupDignosisCodesDesc(string sz_search, string CompanyId, string sz_type_id)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCmd = new SqlCommand("sp_lookup_diagnosis", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyId);
            sqlCmd.Parameters.AddWithValue("@sz_search", sz_search);
            sqlCmd.Parameters.AddWithValue("@sz_type_id", sz_type_id);
            sqlCmd.Connection = sqlCon;
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }

    public DataTable EmployerRecord(string strName, string CompanyId)
    {

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {

            sqlCon = new SqlConnection(strsqlCon);

            sqlCmd = new SqlCommand("SP_GET_PATIENT_SERVICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", strName);
            sqlCmd.Parameters.AddWithValue("@FLAG", "EMP");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
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
        return ds.Tables[0];
    }

}

