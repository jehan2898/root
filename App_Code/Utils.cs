using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gurock.SmartInspect;
using System.Drawing;
using System.Reflection;
using System.Configuration;
/// <summary>
/// Summary description for Utils
/// </summary>
public  class Utils : IDisposable
{
    public Utils()
    {

    }
    #region Method Start
    public void MethodStart(string id,MethodBase method)
    {
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["Applog"]) == true)
        {
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            string Request = "Request " + id + "==>" + className + "." + methodName + "-----------" + DateTime.Now.ToString();
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(Request));
        }
    }
    #endregion

    #region Method End
    public void MethodEnd(string id,MethodBase method)
    {
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["Applog"]) == true)
        {
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            string Response_ = "Response " + id + "==>" + className + "." + methodName + "-----------" + DateTime.Now.ToString();
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(Response_));
        }
    }

    bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}