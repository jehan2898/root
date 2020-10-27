using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using gb.mbs.da.model.ping;

namespace gb.mbs.da.service.ping
{
    public class SrvPing
    {
        public Domain getDomain()
        {
            Domain objDomain = new Domain();
            objDomain.Name = ConfigurationManager.AppSettings["DOMIAN_NAME"].ToString();
            return objDomain;
        }
    }
}
