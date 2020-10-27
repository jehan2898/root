using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace gb.mbs.da.dataaccess
{
    public class ConnectionManager
    {
        public static string GetConnectionString(string p_sKey)
        {
            if (p_sKey == null)
                return ConfigurationManager.AppSettings["Connection_String"].ToString();

            //TODO: Not implemented
            return null;
        }
    }
}