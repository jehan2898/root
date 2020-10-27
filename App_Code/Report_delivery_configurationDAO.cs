using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;

public class Report_delivery_configurationDAO
{
	public Report_delivery_configurationDAO()
	{
	}


    private string sReportDeliveryFormat;
    public string ReportDeliveryFormatText
    {
        get
        {
            if (sReportDeliveryFormat != null)
            {
                if (sReportDeliveryFormat.EndsWith(","))
                {
                    sReportDeliveryFormat = sReportDeliveryFormat.Substring(0, sReportDeliveryFormat.LastIndexOf(','));
                }
            }

            return sReportDeliveryFormat;
        }
    }
    
    private Reporting.ReportingDeliveryFormat eReportDeliveryFormat;
    public Reporting.ReportingDeliveryFormat ReportDeliveryFormat
    {
        get
        {
            return eReportDeliveryFormat;
        }
        set
        {
            eReportDeliveryFormat = value;
            switch (eReportDeliveryFormat)
            {
                case Reporting.ReportingDeliveryFormat.HTML:
                    this.sReportDeliveryFormat += Reporting.ReportingDeliveryFormatText.HTML + ",";
                    break;
                case Reporting.ReportingDeliveryFormat.MSEXCEL:
                    this.sReportDeliveryFormat += Reporting.ReportingDeliveryFormatText.MSEXCEL + ",";
                    break;
                case Reporting.ReportingDeliveryFormat.PDF:
                    this.sReportDeliveryFormat += Reporting.ReportingDeliveryFormatText.PDF + ",";
                    break;
                //case Reporting.ReportingDeliveryFormat.EMAIL:
                //    this.sReportDeliveryFormat += Reporting.ReportingDeliveryFormatText.EMAIL + ",";
                //    break;
                //case Reporting.ReportingDeliveryFormat.ONLINE:
                //    this.sReportDeliveryFormat += Reporting.ReportingDeliveryFormatText.ONLINE + ",";
                //    break;
            }
        }
    }


    //deliverytype


    private string sReportDeliveryType;
    public string ReportDeliveryTypeText
    {
        get
        {
            if (sReportDeliveryType != null)
            {
                if (sReportDeliveryType.EndsWith(","))
                {
                    sReportDeliveryType = sReportDeliveryType.Substring(0, sReportDeliveryType.LastIndexOf(','));
                }
            }

            return sReportDeliveryType;
        }
    }



    private Deliveryformat.ReportingDeliveryType eReportDeliveryType;
    public Deliveryformat.ReportingDeliveryType ReportDeliveryType
    {
        get
        {
            return eReportDeliveryType;
        }
        set
        {
            eReportDeliveryType = value;

            switch (eReportDeliveryType)
            {
                case Deliveryformat.ReportingDeliveryType.EMAIL:
                    this.sReportDeliveryType += Deliveryformat.DeliveryformatText.EMAIL + ",";
                    break;
                case Deliveryformat.ReportingDeliveryType.ONLINE:
                    this.sReportDeliveryType += Deliveryformat.DeliveryformatText.ONLINE + ",";
                    break;

            }
        }
    }



    private string szUserID;
    public string UserID
    {
        get
        {
            return szUserID;
        }

        set
        {
            szUserID = value;
        }
    }

    private string sz_company_id;
    public string CompanyID
    {
        get 
        {
            return sz_company_id;
        }
        set
        {
            sz_company_id = value;
        }        
    }

        private int i_report_id;
        public int ireportid
        {
            get
            {
                return i_report_id;
            }
            set
            {
                i_report_id = value;
            }
        
        }

        private string sz_type;
        public string sztype
        {
            get
            {
                return sz_type;
            
            }
            set            
            {
                sz_type = value;
            }
        }

        private string sz_months;
            public string szmonths
            {
                get
                {
                    return sz_months;
                }

                set
                {
                    sz_months=value;
                }
            }

            private string sz_days;
            public string szdays
            {
                get 
                {
                    return sz_days;
                }
                set
                {
                    sz_days = value;
                }
            
            }

            private string sz_week;
            public string szweek
            {
                get
                {
                    return sz_week;
                }
                set
                {
                    sz_week = value;
                }

            }
            private string sz_time;
            public string sztime
            {
                get 
                {
                    return sz_time;
                }
                set 
                {
                    sz_time = value;
                }
            
            }

        private string sDelivery;
        public string szdelivery
        {
            get
            {
                return sDelivery;
            }
            set
            {
                sDelivery = value;
            }
        }
        
    private string sz_created_by;
    public string szcreatedby
    {
        get 
        {
            return sz_created_by;
        }
        
        set
        {
            sz_created_by=value;
        }
    }

    private ArrayList sz_arrayList_param;
    public ArrayList SZ_arrayList_param
    {
        get
        {
            return sz_arrayList_param;
        }

        set
        {
            sz_arrayList_param = value;
        }
    }

    public void resetAll()
    {
        sReportDeliveryFormat = string.Empty;
        szUserID = string.Empty;
        sz_company_id = string.Empty;
        i_report_id = 0;
        sz_type = string.Empty;
        sz_months = string.Empty;
        sz_days = string.Empty;
        sz_week = string.Empty;
        sz_time = string.Empty;
        sDelivery = string.Empty;
        sz_created_by = string.Empty;
        sz_arrayList_param = new ArrayList();
    }
}