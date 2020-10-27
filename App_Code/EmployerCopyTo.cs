using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for EmployerCopyTo
/// </summary>
public class EmployerCopyTo
{

    public EmployerCopyTo()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    private string _EmployerFrom;
    public string EmployerFrom
    {
        get { return _EmployerFrom; }
        set { _EmployerFrom = value; }
    }
    private string _EmployerTO;
    public string EmployerTO
    {
        get { return _EmployerTO; }
        set { _EmployerTO = value; }
    }
    private string _ProcedureCodeID;
    public string ProcedureCodeID
    {
        get { return _ProcedureCodeID; }
        set { _ProcedureCodeID = value; }
    }

     String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    private string _SucessMsg;
    public string SucessMsg
    {
        get { return _SucessMsg; }
        set { _SucessMsg = value; }
    }

    private string _DuplicateMsg;
    public string DuplicateMsg
    {
        get { return _DuplicateMsg; }
        set { _DuplicateMsg = value; }
    }

    private string _CompanyID;
    public string CompanyID
    {
        get { return _CompanyID; }
        set { _CompanyID = value; }
    }


    public EmployerCopyTo Copy_EmployerProcedureCode(ArrayList arrObj)
    {
        EmployerCopyTo objEmployerCopyTo = new EmployerCopyTo();
        conn = new SqlConnection(strConn);
       string dupMsg = "";
       conn.Open();
        SqlTransaction sqlTrn;
        sqlTrn = conn.BeginTransaction();
        try
        {

            for (int i = 0; i < arrObj.Count; i++)
            {
                EmployerCopyTo objEmployerCopy = (EmployerCopyTo)arrObj[i];

                comm = new SqlCommand("SP_GET_COPY_EMPLOYER_PROCEDURE_CODES", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.Transaction = sqlTrn;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_FROM_EMPLOYER_ID", objEmployerCopy.EmployerFrom);
                comm.Parameters.AddWithValue("@SZ_TO_EMPLOYER_ID", objEmployerCopy.EmployerTO);
                comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objEmployerCopy.ProcedureCodeID);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objEmployerCopy.CompanyID);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[0].ToString().Contains("DUP-"))
                    {
                        if (dupMsg == "")
                        {
                            dupMsg = "Duplicate Code(s): " + dr[0].ToString();
                        }
                        else
                        {
                            dupMsg = dupMsg +" ,"+ dr[0].ToString();
                        }

                    }
                }
                dr.Close();
            }
            sqlTrn.Commit();
            objEmployerCopyTo.DuplicateMsg = dupMsg;
            objEmployerCopyTo.SucessMsg = "Procedure codes copied successfully ...";

        }

        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sqlTrn.Rollback();
            objEmployerCopyTo.SucessMsg = "Error";
            
        }finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        return objEmployerCopyTo;
    }  
}