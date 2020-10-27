using System;
using System.Collections.Generic;
using System.Web;

namespace Reporting
{
    public enum ReportingDeliveryFormat
    {
        MSEXCEL, HTML, PDF, ONLINE, EMAIL
    };

    
    public class ReportingDeliveryFormatText
    {
        public const string MSEXCEL = "MSEXCEL";
        public const string HTML = "HTML";
        public const string PDF = "PDF";
       
    }
}