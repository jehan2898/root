using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for UpdateEventStatus
/// </summary>
public class UpdateEventStatus
{
    private int  _I_EVENT_ID ;
    public int  I_EVENT_ID{
        get { return _I_EVENT_ID;}
        set { _I_EVENT_ID = value; }
     }


    private int _BT_STATUS;
    public int BT_STATUS
    {
        get { return _BT_STATUS; }
        set { _BT_STATUS = value; }
    }

    private int _I_STATUS;
    public int I_STATUS
    {
        get { return _I_STATUS; }
        set { _I_STATUS = value; }
    }

    private string _SZ_BILL_NUMBER;
    public string SZ_BILL_NUMBER
    {
        get { return _SZ_BILL_NUMBER; }
        set { _SZ_BILL_NUMBER = value; }
    }


    private string _sz_procode_id;
    public string sz_procode_id
    {
        get { return _sz_procode_id; }
        set { _sz_procode_id = value; }
    }


    private DateTime _DT_BILL_DATE;
    public DateTime DT_BILL_DATE
    {
        get { return _DT_BILL_DATE; }
        set { _DT_BILL_DATE = value; }
    }



 
    
	public UpdateEventStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}