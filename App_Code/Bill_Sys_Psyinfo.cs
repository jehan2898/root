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

/// <summary>
/// Summary description for Bill_Sys_Psyinfo
/// </summary>
public class Bill_Sys_Psyinfo
{
    public Bill_Sys_Psyinfo()
    {
        
    }
    private string _RBATTENDING;
    public string RBATTENDING
    {
        get
        {
            return _RBATTENDING;
        }
        set
        {
            _RBATTENDING = value;
        }
    }

    private string _SERVICEPROVIDER;
    public string SERVICEPROVIDER
    {
        get
        {
            return _SERVICEPROVIDER;
        }
        set
        {
            _SERVICEPROVIDER = value;
        }
    }


    private string _INCIDENT;
    public string INCIDENT
    {
        get
        {
            return _INCIDENT;
        }
        set
        {
            _INCIDENT = value;
        }
    }

    private string _HISTORY;
    public string HISTORY
    {
        get
        {
            return _HISTORY;
        }
        set
        {
            _HISTORY = value;
        }
    }

    private string _BREFERAL;
    public string BREFERAL
    {
        get
        {
            return _BREFERAL;
        }
        set
        {
            _BREFERAL = value;
        }
    }

    private string _EVALUATION;
    public string EVALUATION
    {
        get
        {
            return _EVALUATION;
        }
        set
        {
            _EVALUATION = value;
        }
    }


    private string _CONDITION;
    public string CONDITION
    {
        get
        {
            return _CONDITION;
        }
        set
        {
            _CONDITION = value;
        }
    }

    private string _CHK_TREATEMENT;
    public string CHK_TREATEMENT
    {
        get
        {
            return _CHK_TREATEMENT;
        }
        set
        {
            _CHK_TREATEMENT = value;
        }
    }
    private string _TREATEMENTTEXT;
    public string TREATEMENTTEXT
    {
        get
        {
            return _TREATEMENTTEXT;
        }
        set
        {
            _TREATEMENTTEXT = value;
        }
    }

    private string _VISITEDDATE;
    public string VISITEDDATE
    {
        get
        {
            return _VISITEDDATE;
        }
        set
        {
            _VISITEDDATE = value;
        }
    }


    private string _FIRSTVISITEDDATE;
    public string FIRSTVISITEDDATE
    {
        get
        {
            return _FIRSTVISITEDDATE;
        }
        set
        {
            _FIRSTVISITEDDATE = value;
        }
    }
    private string _PATIANTSEEN;
    public string PATIANTSEEN
    {
        get
        {
            return _PATIANTSEEN;
        }
        set
        {
            _PATIANTSEEN = value;
        }
    }
    private string _NOPATIENTSEEN;
    public string NOPATIENTSEEN
    {
        get
        {
            return _NOPATIENTSEEN;
        }
        set
        {
            _NOPATIENTSEEN = value;
        }
    }
   

    private string _PATIANTATTENDING_DOCTOR;
    public string PATIANTATTENDING_DOCTOR
    {
        get
        {
            return _PATIANTATTENDING_DOCTOR;
        }
        set
        {
            _PATIANTATTENDING_DOCTOR = value;
        }
    }



    private string _PATIANT_WORKING;
    public string PATIANT_WORKING
    {
        get
        {
            return _PATIANT_WORKING;
        }
        set
        {
            _PATIANT_WORKING = value;
        }
    }

    private string _LIMITEDWORK;
    public string LIMITEDWORK
    {
        get
        {
            return _LIMITEDWORK;
        }
        set
        {
            _LIMITEDWORK = value;
        }
    }

    private string _REGULARWORK;
    public string REGULARWORK
    {
        get
        {
            return _REGULARWORK;
        }
        set
        {
            _REGULARWORK = value;
        }
    }


    private string _SUSTAINED;
    public string SUSTAINED
    {
        get
        {
            return _SUSTAINED;
        }
        set
        {
            _SUSTAINED = value;
        }
    }

    private string _ADDITIONALINFO;
    public string ADDITIONALINFO
    {
        get
        {
            return _ADDITIONALINFO;
        }
        set
        {
            _ADDITIONALINFO = value;
        }
    }

    private string _VLBF;
    public string VLBF
    {
        get
        {
            return _VLBF;
        }
        set
        {
            _VLBF = value;
        }
 
   }

    private string _DATEOFFORTH;
    public string DATEOFFORTH
    {
       get
       {
           return _DATEOFFORTH;
       }
       set
       {
           _DATEOFFORTH = value;
       }
   }

   private string _COMPANYID;
    public string COMPANYID
   {
       get
       {
           return _COMPANYID;
       }
       set
       {
           _COMPANYID = value;
       }
   }

   private string _CASEID;
    public string CASEID
   {
       get
       {
           return _CASEID;
       }
       set
       {
           _CASEID = value;
       }
   }

   private string _PATIANTACCNO;
    public string PATIANTACCNO
   {
       get
       {
           return _PATIANTACCNO;
       }
       set
       {
           _PATIANTACCNO = value;
       }
   }

}

public class Bill_save
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    public Bill_save()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();  
    }




    public void Save_Psy_Info(Bill_Sys_Psyinfo _Bill_Sys_Psyinfo)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_txn_update_psy_patient_info", conn);

            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = sqlCon;
            comm.Parameters.AddWithValue("@i_attending_psy", _Bill_Sys_Psyinfo.RBATTENDING);
            comm.Parameters.AddWithValue("@bt_service_provider", _Bill_Sys_Psyinfo.SERVICEPROVIDER);
            comm.Parameters.AddWithValue("@sz_incident_description", _Bill_Sys_Psyinfo.INCIDENT);
            comm.Parameters.AddWithValue("@sz_preexisting_psy", _Bill_Sys_Psyinfo.HISTORY);
            comm.Parameters.AddWithValue("@bt_referal_for", _Bill_Sys_Psyinfo.BREFERAL);
            comm.Parameters.AddWithValue("@sz_evalution", _Bill_Sys_Psyinfo.EVALUATION);
            comm.Parameters.AddWithValue("@sz_patient_condition", _Bill_Sys_Psyinfo.CONDITION);
            comm.Parameters.AddWithValue("@bt_authentication_req", _Bill_Sys_Psyinfo.CHK_TREATEMENT);
            comm.Parameters.AddWithValue("@sz_authentication_req", _Bill_Sys_Psyinfo.TREATEMENTTEXT);
            comm.Parameters.AddWithValue("@dt_dateof_visited", _Bill_Sys_Psyinfo.VISITEDDATE);
            comm.Parameters.AddWithValue("@dt_first_dateof_visit", _Bill_Sys_Psyinfo.FIRSTVISITEDDATE);
            comm.Parameters.AddWithValue("@bt_will_patient_see_again", _Bill_Sys_Psyinfo.PATIANTSEEN);
            comm.Parameters.AddWithValue("@dt_yes_seen", _Bill_Sys_Psyinfo.NOPATIENTSEEN);
            comm.Parameters.AddWithValue("@i_no_seen", _Bill_Sys_Psyinfo.PATIANTATTENDING_DOCTOR);
            comm.Parameters.AddWithValue("@i_is_patient_working", _Bill_Sys_Psyinfo.PATIANT_WORKING);
            comm.Parameters.AddWithValue("@sz_yes_patient_working", _Bill_Sys_Psyinfo.LIMITEDWORK);
            comm.Parameters.AddWithValue("@sz_patient_regular_work", _Bill_Sys_Psyinfo.REGULARWORK);
            comm.Parameters.AddWithValue("@i_sustained", _Bill_Sys_Psyinfo.SUSTAINED);
            comm.Parameters.AddWithValue("@sz_additional_info", _Bill_Sys_Psyinfo.ADDITIONALINFO);
            comm.Parameters.AddWithValue("@i_vfbl_or_vawbl", _Bill_Sys_Psyinfo.VLBF);
            comm.Parameters.AddWithValue("@sz_patient_acc_number", _Bill_Sys_Psyinfo.PATIANTACCNO);
            comm.Parameters.AddWithValue("@i_case_id", _Bill_Sys_Psyinfo.CASEID);
            comm.Parameters.AddWithValue("@sz_company_id", _Bill_Sys_Psyinfo.COMPANYID);
            comm.Parameters.AddWithValue("@dt_historyof_injury", _Bill_Sys_Psyinfo.DATEOFFORTH);

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public DataSet Get_Psy_Info(String szCaseID, String szCompanyId)
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_txn_select_psy_patient_info", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@i_case_id", szCaseID);
            comm.Parameters.AddWithValue("@sz_company_id", szCompanyId);

            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);

        }
        catch (Exception ex)
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