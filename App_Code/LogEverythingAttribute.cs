using System;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public class LogEverythingAttribute : JobFilterAttribute,IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
{
    SqlConnection conn;
    SqlCommand comm;
    public void OnCreating(CreatingContext filterContext)
    {

    }

    public void OnCreated(CreatedContext filterContext)
    {

    }

    public void OnPerforming(PerformingContext filterContext)
    {

    }

    public void OnPerformed(PerformedContext filterContext)
    {

    }

    public void OnStateElection(ElectStateContext context)
    {

    }


    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        if (!string.IsNullOrEmpty(context.NewState.Name))
        {
            conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            try
            {

                #region "Update Job Status"
                comm.CommandText = "[PROC_UpdateJobStatus]";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@Status", context.NewState.Name);
                comm.Parameters.AddWithValue("@JobID", Convert.ToInt32(context.JobId));
                comm.ExecuteNonQuery();
                #endregion
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }

    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {

    }
}