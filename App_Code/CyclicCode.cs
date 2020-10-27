using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for CyclicCode
/// </summary>
[Serializable]
public class CyclicCode
{
	
    private string _sz_configuraton;
    public string sz_configuraton
    {
        get { return _sz_configuraton; }
        set { _sz_configuraton = value; }
    }
    private int _i_Count;
    public int i_Count
    {
        get { return _i_Count; }
        set { _i_Count = value; }
    }

    private int _i_Flag;
    public int i_Flag
    {
        get { return _i_Flag; }
        set { _i_Flag = value; }
    }

    private DataTable _ProcValue;
    public DataTable ProcValue
    {
        get { return _ProcValue; }
        set { _ProcValue = value; }
    }

    private DataTable _AllProc;
    public DataTable AllProc
    {
        get { return _AllProc; }
        set { _AllProc = value; }
    }
}