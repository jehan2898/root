using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Intake_Documents_DAO
/// </summary>
public class Intake_Documents_DAO
{
    #region Varaibles

    public Intake_Documents_DAO()
	{
        
		//
		// TODO: Add constructor logic here
		//
	}

    private int _i_id;
    public int i_id
    {
        get { return _i_id; }
        set { _i_id = value; }
    }
    private int _i_provider_id;
    public int i_provider_id
    {
        get { return _i_provider_id; }
        set { _i_provider_id = value; }
    }
    private int _i_documnet_id;
    public int i_documnet_id
    {
        get { return _i_documnet_id; }
        set { _i_documnet_id = value; }
    }

    private string _i_user_id;
    public string i_user_id
    {
        get { return _i_user_id; }
        set { _i_user_id = value; }
    }
    private string _sz_case_type_id;
    public string sz_case_type_id
    {
        get { return _sz_case_type_id; }
        set { _sz_case_type_id = value; }
    }
    private string _sz_case_id;
    public string sz_case_id
    {
        get { return _sz_case_id; }
        set { _sz_case_id = value; }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get { return _sz_company_id; }
        set { _sz_company_id = value; }
    }
    private string _CaseType;
    public string CaseType
    {
        get { return _CaseType; }
        set { _CaseType = value; }
    }

    private string _sz_name;
    public string sz_name
    {
            get{ return _sz_name;}
            set{ _sz_name=value;}
    }



    private string _Document;
    public string Document
    {
        get { return _Document; }
        set { _Document = value; }
    }
    #endregion
}