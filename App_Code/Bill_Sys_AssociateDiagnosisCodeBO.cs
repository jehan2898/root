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
/// <summary>
/// Summary description for Bill_Sys_AssociateDiagnosisCodeBO
/// </summary>
public class Bill_Sys_AssociateDiagnosisCodeBO
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_AssociateDiagnosisCodeBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetDoctorDiagnosisCode(string doctorId,string caseId,string flag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            if (doctorId != "") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId); }
            //comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            if (caseId != "") { comm.Parameters.AddWithValue("@SZ_CASE_ID", caseId); }
            comm.Parameters.AddWithValue("@FLAG", "GET_DOCTOR_DIAGNOSIS_CODE");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetDoctorDiagCodeforMasterBilling(string ProcedureGroupID,string caseId,string flag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_PDFBILLS_MASTERBILLING", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            if (ProcedureGroupID != "") { comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", ProcedureGroupID); }
            //comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId);
            if (caseId != "") { comm.Parameters.AddWithValue("@SZ_CASE_ID", caseId); }
            comm.Parameters.AddWithValue("@FLAG", "GET_DOCTOR_DIAGNOSIS_CODE");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetBillDiagnosisCode(string billNo)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_BILL_DIAGNOSISCODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", billNo); 
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }


    public DataSet GetCaseDiagnosisCode(string p_szCaseID,string p_szDoctorID,string p_szCompanyID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_CASE_DIAGNOSISCODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetDiagnosisCode(string caseId, string companyId, string doctorId, string flag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            if (doctorId != "") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorId); }
            comm.Parameters.AddWithValue("@FLAG", flag);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetProcedureCodeList(string id)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_MST_PROCEDURE_CODES", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", id);
            comm.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_CODE_LIST");
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
        finally { conn.Close(); }
    }

    public void SaveCaseAssociateDignosisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[1].ToString());
            if (_arrayList[2].ToString() !=""){ comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", _arrayList[2].ToString());}
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[3].ToString());
        
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", _arrayList[5].ToString());

            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteCaseAssociateDignosisCode(string setid , string dcodeid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_DELETE_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", setid);
           // comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", dcodeid);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");   

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteCaseAssociateDignosisCodeWithProcCode(string caseid, string companyid,string  doctorID)//, string dcodeid,string procid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_DELETE_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;

            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorID);
            comm.Parameters.AddWithValue("@FLAG", "DELETE_WITH_PROC_CODE");   

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }
    
    public void UpdateCaseAssociateDignosisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;

            comm.Parameters.AddWithValue("@SZ_CASE_ASSOCIATE_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", _arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[5].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[6].ToString());

            comm.Parameters.AddWithValue("@FLAG", "UPDATE");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetDiagnosisSetID()
    {
        conn = new SqlConnection(strConn);
        string strID="";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@FLAG", "GET_DIAGNOSIS_SET_ID");

            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            return strID;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
        finally { conn.Close(); }
    }

    public int GetDoctorType(string id)
    {
        conn = new SqlConnection(strConn);
        SqlParameter _sqlParam = new SqlParameter();
        int iDoctorType = 0;
      
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            _sqlParam = comm.Parameters.Add("@Return", SqlDbType.Int);
            _sqlParam.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", id);
            comm.Parameters.AddWithValue("@FLAG", "CHECK_DOCTOR_TYPE");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();

            iDoctorType= Convert.ToInt32(comm.Parameters["@Return"].Value);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
       
        finally { conn.Close(); }
        return iDoctorType;
    }

    public DataSet GetCaseAssociateDiagnosisDetails(string id)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", id);
            
            comm.Parameters.AddWithValue("@FLAG", "GET_DETAILS");


            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetAssociateCaseBillNumber(string id, string setID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", id);
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", setID);
            comm.Parameters.AddWithValue("@FLAG", "GET_BILL_NUMBER");


            sqlda = new SqlDataAdapter(comm);            
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public void SaveTransactionDignosisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_ADD_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[4].ToString());

            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteTransactionDignosisCodeWithProcCode(string billno)//, string dignosisid, string procid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_ADD_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billno);
           //-- comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", dignosisid);
          //  comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", procid);

            comm.Parameters.AddWithValue("@FLAG", "DELETE_WITH_PROC_CODE");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteTransactionDignosisCode(string billno, string dignosisid)
    {
        conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            comm = new SqlCommand("SP_ADD_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", billno);
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", dignosisid);

            comm.Parameters.AddWithValue("@FLAG", "DELETE");

            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public string GetCaseType(string caseid)
    {
        conn = new SqlConnection(strConn);
        string strID = "";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            comm.Parameters.AddWithValue("@FLAG", "GET_CASE_TYPE");

            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            return strID;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }

    public string GetCaseTypeName(string caseid)
    {
        conn = new SqlConnection(strConn);
        string strID = "";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            comm.Parameters.AddWithValue("@FLAG", "GET_CASE_TYPE_NAME");

            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            return strID;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
        finally { conn.Close(); }
        return null;
    }


    public Boolean DeleteAssociateDiagonisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);
        string strID = "";
        bool das = false;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", (_arrayList[0].ToString()).Trim());
            if (_arrayList[3].ToString() != "") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", (_arrayList[3].ToString()).Trim()); }
            comm.Parameters.AddWithValue("@SZ_CASE_ID", (_arrayList[1].ToString()).Trim());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", (_arrayList[2].ToString()).Trim());
            comm.Parameters.AddWithValue("@FLAG", "DELETE_ASSO_DIAG_CODE");
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", (_arrayList[4].ToString()).Trim());
            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            if (strID == "DELETED")
                return true;
            else
                return false;
           

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return das;
       
    }

    public Boolean DeleteAssociateDiagonisCodewithProcCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);
        string strID = "";
        bool bproccode = false;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", (_arrayList[0].ToString()).Trim());
            if (_arrayList[3].ToString() != "") { comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", (_arrayList[3].ToString()).Trim()); }
            comm.Parameters.AddWithValue("@SZ_CASE_ID", (_arrayList[1].ToString()).Trim());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", (_arrayList[2].ToString()).Trim());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", (_arrayList[4].ToString()).Trim());
            comm.Parameters.AddWithValue("@FLAG", "DELETE_WITH_PROC_CODE");

            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            if (strID == "DELETED")
                return true;
            else
                return false;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return bproccode;
    }
    // 21 April 2010 get specaility -- sachin
    public string  GetAssociateSpecaility(string typeCode, string companyID,string flag)
    {
        ds = new DataSet();
        string _specaility = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_SPECIALITY_RECEIVEDOCUMENT_POPUP", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_id", typeCode);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            comm.Parameters.AddWithValue("@FLAG", flag);


            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                _specaility = dr[0].ToString();
            }

            //sqlda = new SqlDataAdapter(comm);
            //sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return _specaility;
    }

    public DataSet Get_Associate_Speciality(string szSpecialityID, string szCompanyID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_ASSOCIATED_SPECIALIY", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_SPECIALITY", szSpecialityID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }


    public void SaveProcedureAssociateDignosisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", _arrayList[4].ToString());
            comm.Parameters.AddWithValue("@SZ_ASSOCIATED_DIAG_CODE_ID", _arrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[5].ToString());
            comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", _arrayList[6].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void DeleteProcedureAssociateDignosisCode(string caseid, string companyid, string szEventID)//, string dcodeid,string procid)
    {
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventID);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetProcedureDiagnosisCode(string caseId, string companyId, string szEventProcID, string flag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventProcID);
            comm.Parameters.AddWithValue("@FLAG", flag);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public Boolean DeleteAssociateProcedureDiagonisCode(ArrayList _arrayList)
    {
        conn = new SqlConnection(strConn);
        string strID = "";
        bool DeleteDiagnocode = false;
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_ASSOCIATED_DIAG_CODE_ID", _arrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[2].ToString());
            comm.Parameters.AddWithValue("@FLAG", "DELETE_ASSO_DIAG_CODE");
            comm.CommandType = CommandType.StoredProcedure;
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strID = dr[0].ToString();
            }

            if (strID == "DELETED")
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return DeleteDiagnocode;
    }

    public DataSet GetDoctorProcedureDiagnosisCode(string szCompanyID, string szEventID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            if (szCompanyID != "") { comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID); }
            if (szEventID != "") { comm.Parameters.AddWithValue("@eventid", szEventID); }
            comm.Parameters.AddWithValue("@FLAG", "GET_DIAGNOSIS_CODE_FROM_EVENT_ID");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetVists(string szCompanyID, string szCaseId, string szflag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_GET_VISITS_FOR_CASE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId); 
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", szflag);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetProcedureAssociatedDiagnosisCode(string szEventProcId, string szCompanyID, string szCaseId, string szProcedureGroupID, string szflag)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            comm = new SqlCommand("SP_TXN_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventProcId);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupID);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", szflag);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }
}

