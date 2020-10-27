using System;
using System.Collections.Generic;

using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for EmployerBO
/// </summary>
public class EmployerBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    SqlDataReader dr;
    public EmployerBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GetEmployeProcedure(string EmployerId, string CompanyID)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_EMPLOYER_PROCEDURE_CODE", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ID", EmployerId);

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

    public string SaveVisit(ArrayList arrCases)
    {
        string returnSave = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {

            for (int i = 0; i < arrCases.Count; i++)
            {
                EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                objEmployerVisitDO = (EmployerVisitDO)arrCases[i];
                sqlCmd = new SqlCommand("SP_SAVE_EMPLOYER_VISIT", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = tr;
                sqlCmd.Parameters.AddWithValue("@sz_case_id", objEmployerVisitDO.CaseID);
                sqlCmd.Parameters.AddWithValue("@dt_visit_date", objEmployerVisitDO.VisitDate);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", objEmployerVisitDO.CompanyID);
                sqlCmd.Parameters.AddWithValue("@sz_user_id", objEmployerVisitDO.UserID);
                sqlCmd.Parameters.AddWithValue("@sz_employer_id", objEmployerVisitDO.EmploerID);

                SqlDataReader dr = sqlCmd.ExecuteReader();
                string visitID = "";
                while (dr.Read())
                {
                    visitID = dr[0].ToString();

                }
                dr.Close();



                for (int j = 0; j < objEmployerVisitDO.EmployerVisitProcedure.Count; j++)
                {
                    EmployerVisitProcedure objEmployerVisitProcedure = objEmployerVisitDO.EmployerVisitProcedure[j];
                    sqlCmd = new SqlCommand("SP_SAVE_VISIT_PROCEDURE_CODE", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = tr;
                    sqlCmd.Parameters.AddWithValue("@sz_case_id", objEmployerVisitDO.CaseID);
                    sqlCmd.Parameters.AddWithValue("@i_visit_id", visitID);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", objEmployerVisitDO.CompanyID);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", objEmployerVisitDO.UserID);
                    sqlCmd.Parameters.AddWithValue("@sz_employer_id", objEmployerVisitDO.EmploerID);
                    sqlCmd.Parameters.AddWithValue("@i_procedure_id", objEmployerVisitProcedure.ProcedureCode);
                    sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", objEmployerVisitProcedure.ProcedureGroupId);
                    sqlCmd.ExecuteNonQuery();
                }


            }

            tr.Commit();
            returnSave = "Save";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnSave = "";
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnSave;
    }
    public DataSet GetVisit(EmployerVisitDO EmployerVisitDO)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_EMPLOYER_VISIT", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER", EmployerVisitDO.EmploerID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", EmployerVisitDO.CompanyID);
            if (EmployerVisitDO.CaseNO != "")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", EmployerVisitDO.CaseNO);
            }
            if (EmployerVisitDO.From_Visit_Date != "" && EmployerVisitDO.To_Visit_Date != "")
            {
                sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", EmployerVisitDO.From_Visit_Date);
                sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", EmployerVisitDO.To_Visit_Date);
            }

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
    public string GenerateInvoice(string visitId, string employerId, string companyID, string UserID)
    {
        string returnInvoiceID = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {



            sqlCmd = new SqlCommand("sp_generate_invoice", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = tr;
            sqlCmd.Parameters.AddWithValue("@sz_visit_id", visitId);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", companyID);
            sqlCmd.Parameters.AddWithValue("@sz_employer_id", employerId);
            sqlCmd.Parameters.AddWithValue("@sz_users_id", UserID);


            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                returnInvoiceID = dr[0].ToString();

            }
            dr.Close();





            tr.Commit();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnInvoiceID = "";
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnInvoiceID;
    }
    public DataSet GetInvoiceInfo(string invoiceId, string companyId)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICE_INFO", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", invoiceId);


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
    public int SaveInvoiceLink(string companyId, string invoiceId, string path, string fileName)
    {
        int iReturn = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {

            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_INVOICE_LINK", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", invoiceId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATH", path);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", fileName);
            iReturn = sqlCmd.ExecuteNonQuery();
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
        return iReturn;
    }

    public DataSet GetCaseEmployer(string caseID, string companyId)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_CASE_EMPLOYER", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);


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

    public DataSet GetAllInvoice(string caseNo, string companyId, string fromVisitDate, string toVisitDate, string fromInvoicetDate, string ToInvoicetDate, string employerId, string type)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_EMPLOYER_VISIT_ALL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", fromVisitDate);
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", toVisitDate);
            sqlCmd.Parameters.AddWithValue("@DT_INVOICE_FROM_DATE", fromInvoicetDate);
            sqlCmd.Parameters.AddWithValue("@DT_INVOICE_TO_DATE", ToInvoicetDate);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", caseNo);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER", employerId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", type);


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

    public string DeleteInvoice(ArrayList arrCases)
    {
        string returnSave = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {

            for (int i = 0; i < arrCases.Count; i++)
            {
                EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                objEmployerVisitDO = (EmployerVisitDO)arrCases[i];

                sqlCmd = new SqlCommand("sp_delete_employer_invoice", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = tr;
                sqlCmd.Parameters.AddWithValue("@sz_invoice_id", objEmployerVisitDO.InvoiceNo);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", objEmployerVisitDO.CompanyID);
                sqlCmd.ExecuteNonQuery();




            }

            tr.Commit();
            returnSave = "Save";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnSave = "";
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnSave;
    }
    public string DeleteVisit(ArrayList arrCases)
    {
        string returnSave = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {

            for (int i = 0; i < arrCases.Count; i++)
            {
                EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                objEmployerVisitDO = (EmployerVisitDO)arrCases[i];

                sqlCmd = new SqlCommand("sp_delete_employer_visit", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = tr;
                sqlCmd.Parameters.AddWithValue("@bi_visit_id", objEmployerVisitDO.VisitId);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", objEmployerVisitDO.CompanyID);
                sqlCmd.ExecuteNonQuery();




            }

            tr.Commit();
            returnSave = "Save";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnSave = "";
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnSave;
    }

    public string SaveVisitDocument(EmployerVisitDO objEmployerVisitDO)
    {
        string visitID = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {


            sqlCmd = new SqlCommand("proc_save_jfk_visit_documents", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = tr;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objEmployerVisitDO.CaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objEmployerVisitDO.CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_NAME", objEmployerVisitDO.CompanyName);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objEmployerVisitDO.UserID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objEmployerVisitDO.UserName);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objEmployerVisitDO.FileName);
            sqlCmd.Parameters.AddWithValue("@I_visit_id", objEmployerVisitDO.VisitId);

            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                visitID = dr[0].ToString();

            }
            dr.Close();








            tr.Commit();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            visitID = "";
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return visitID;
    }

    public DataSet GetDocumnets(EmployerVisitDO EmployerVisitDO)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("proc_view_jfk_visit_document", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_visit_id", EmployerVisitDO.VisitId);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", EmployerVisitDO.CompanyID);
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

    public string DeleteDocs(ArrayList arrCases)
    {
        string returnSave = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {

            for (int i = 0; i < arrCases.Count; i++)
            {
                EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                objEmployerVisitDO = (EmployerVisitDO)arrCases[i];
                sqlCmd = new SqlCommand("proc_delete_jfk_visit_document", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = tr;
                sqlCmd.Parameters.AddWithValue("@sz_company_id", objEmployerVisitDO.CompanyID);
                sqlCmd.Parameters.AddWithValue("@i_visit_id", objEmployerVisitDO.VisitId);
                sqlCmd.Parameters.AddWithValue("@i_image_id", objEmployerVisitDO.ImageId);
                sqlCmd.ExecuteNonQuery();





            }

            tr.Commit();
            returnSave = "deleted";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnSave = ex.ToString(); ;
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnSave;
    }

    public DataSet SearchInvoice(string invoiceno, string companyId, string fromInvoicetDate, string ToInvoicetDate, string employerId)
    {

        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("proc_employer_invoice", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@dt_invoice_from_date", fromInvoicetDate);
            sqlCmd.Parameters.AddWithValue("@dt_invoice_to_date", ToInvoicetDate);
            sqlCmd.Parameters.AddWithValue("@sz_invoice_id", invoiceno);
            sqlCmd.Parameters.AddWithValue("@sz_employer_id", employerId);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", companyId);



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

    public string DeletePaymetDocs(ArrayList arrCases,string paymentid,string compnayid,  string invoiceid)
    {
        string returnSave = "";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {

            for (int i = 0; i < arrCases.Count; i++)
            {
               
                sqlCmd = new SqlCommand("proc_delete_invoice_images", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = tr;
                sqlCmd.Parameters.AddWithValue("@i_imageid", arrCases[i].ToString());
                sqlCmd.Parameters.AddWithValue("@sz_invoice_id", invoiceid);
                sqlCmd.Parameters.AddWithValue("@i_paymnet_id", paymentid);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", compnayid);
                sqlCmd.ExecuteNonQuery();
            }

            tr.Commit();
            returnSave = "deleted";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            returnSave = ex.ToString(); ;
            tr.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnSave;
    }

}