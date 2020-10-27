using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.dataaccess.exception
{
    public class NoDataFoundException : System.Exception
    {
        public NoDataFoundException()
            : base()
        {
        }

        public NoDataFoundException(string message)
            : base(message)
        {
        }

        public NoDataFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}