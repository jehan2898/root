using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.user.exception
{
    public class AuthenticationException : System.Exception
    {
        public AuthenticationException()
            : base()
        {
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}