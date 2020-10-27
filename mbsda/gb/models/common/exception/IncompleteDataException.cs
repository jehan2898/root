using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.common.exception
{
    public class IncompleteDataException : Exception
    {
        public IncompleteDataException()
            : base()
        {
        }

        public IncompleteDataException(string message)
            : base(message)
        {
        }

        public IncompleteDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
