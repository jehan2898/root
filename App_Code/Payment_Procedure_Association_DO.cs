using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Payment_Procedure_Association_DO
/// </summary>
public class Payment_Procedure_Association_DO
{
    private string _SZ_COMPANY_ID;
    private string _SZ_BILL_ID;
    private string _SZ_PAYMENT_ID;
    private string _SZ_PROC_CODE;
    private string _SZ_AMOUNT_PAID;

    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    public string SZ_BILL_ID
    {
        get { return _SZ_BILL_ID; }
        set { _SZ_BILL_ID = value; }
    }

    public string SZ_PAYMENT_ID
    {
        get { return _SZ_PAYMENT_ID; }
        set { _SZ_PAYMENT_ID = value; }
    }

    public string SZ_PROC_CODE
    {
        get { return _SZ_PROC_CODE; }
        set { _SZ_PROC_CODE = value; }
    }
    public string SZ_AMOUNT_PAID
    {
        get { return _SZ_AMOUNT_PAID; }
        set { _SZ_AMOUNT_PAID = value; }
    }
}