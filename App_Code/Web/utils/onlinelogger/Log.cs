using System;
using System.Collections.Generic;
using System.Web;

namespace web.utils.onlinelogger
{
    [Serializable()]
    public class Log
    {
        private string sLogText;
        private OnlineLogType eLogType;
	    
        public string Text
        {
            get
            {
                return sLogText;
            }
        }

        public OnlineLogType Type
        {
            get
            {
                return eLogType;
            }
        }

        public Log(string p_sLogText,OnlineLogType p_Enum)
	    {
            this.sLogText = p_sLogText;
            this.eLogType = p_Enum;
	    }
    }
}

