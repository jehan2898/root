using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for Bill_sys_updateunBilledDoctor
/// </summary>
public class Bill_sys_updateunBilledDoctor
{
    SqlConnection sqlcon ;
    SqlCommand sqlcmd ;
    string constring;
    public Bill_sys_updateunBilledDoctor()
	{
        constring = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    private string _i_event_id;
    public string EventID
    {
        get
        {
            return _i_event_id;
        }
        set
        {
            _i_event_id = value;
        }
    }
    private string _sz_company_id;
    public string CompanyId
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }
    private string _sz_Patient_id;
    public string PatientID
    {
        get
        {
            return _sz_Patient_id;
        }
        set
        {
            _sz_Patient_id = value;
        }
    }

    private string _sz_Doctor_id;
    public string DoctorID
    {
        get
        {
            return _sz_Doctor_id;
        }
        set
        {
            _sz_Doctor_id = value;
        }
    }

    private string _sz_Event_Date;
    public string EventDate
    {
        get
        {
            return _sz_Event_Date;
        }
        set
        {
            _sz_Event_Date = value;
        }
    }

    public string UpdateUnbilledDoctor(ArrayList _objdoctor,string sz_flag)
    {
        
        
        sqlcon = new SqlConnection(constring);
        sqlcmd = new SqlCommand("sp_update_unbilled_doctor",sqlcon);
        sqlcmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction tran;
        sqlcon.Open();
        tran = sqlcon.BeginTransaction();
        sqlcmd.CommandType = CommandType.StoredProcedure;
      

        try
        {
            foreach (Bill_sys_updateunBilledDoctor _obj in _objdoctor)
            {
                sqlcmd.Transaction = tran;
                sqlcmd.Parameters.Add("@sz_doctor_id",_obj.DoctorID);
                sqlcmd.Parameters.Add("@i_event_id" , _obj.EventID);
                sqlcmd.Parameters.Add("@dt_event_date",_obj.EventDate);
                sqlcmd.Parameters.Add("@sz_company_id",_obj.CompanyId);
                sqlcmd.Parameters.Add("@sz_patient_id",_obj.PatientID);
                sqlcmd.Parameters.Add("@flag", sz_flag);
                sqlcmd.ExecuteNonQuery();
                sqlcmd.Parameters.Clear();
            }
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }

        }
        return "Done";
    }
}


