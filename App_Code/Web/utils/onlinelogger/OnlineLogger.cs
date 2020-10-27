using System;
using System.Collections.Generic;
using System.Web;

namespace web.utils.onlinelogger
{
    [Serializable()]
    public class OnlineLogger
    {
        private static List<web.utils.onlinelogger.Log> c_lstLog;
        
        public static void Log(string p_sText,OnlineLogType p_eType)
        {
            if (c_lstLog == null)
                c_lstLog = new List<web.utils.onlinelogger.Log>();
            c_lstLog.Add(new onlinelogger.Log(p_sText, p_eType));
        }

        public static void Clear()
        {
            try
            {
                System.Web.HttpContext.Current.Session["OnlineLog"] = null;
                System.Web.HttpContext.Current.Session.Remove("OnlineLog");
            }
            catch (Exception x) { }
        }
        
        public static void Save()
        {
            System.Web.HttpContext.Current.Session["OnlineLog"] = c_lstLog;
        }

        public static List<web.utils.onlinelogger.Log> Select()
        {
            List<web.utils.onlinelogger.Log> lstLog = (List<web.utils.onlinelogger.Log>)System.Web.HttpContext.Current.Session["OnlineLog"];

            //return empty log if log does not exists in session
            if (lstLog == null)
                lstLog = new List<web.utils.onlinelogger.Log>();

            return lstLog;
        }
    }
}
