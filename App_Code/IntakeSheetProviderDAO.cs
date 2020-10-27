using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for IntakeSheetProviderDAO
/// </summary>
public class IntakeSheetProviderDAO
{
	public IntakeSheetProviderDAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Varaibles
    
    private int _i_id;
    public int i_id
    {
        get { return _i_id; }
        set { _i_id = value; }
    }

    private int _i_state_id;
    public int i_state_id
    {
        get { return _i_state_id; }
        set { _i_state_id = value; }
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
    private string _sz_address;
    public string sz_address
    {
        get { return _sz_address; }
        set { _sz_address = value; }
    }
    private string _sz_city;
    public string sz_city
    {
        get { return _sz_city; }
        set { _sz_city = value; }
    }

    private string _sz_zip;
    public string sz_zip
    {
        get { return _sz_zip; }
        set { _sz_zip = value; }
    }

    private string _sz_phone;
    public string sz_phone
    {
        get { return _sz_phone; }
        set { _sz_phone = value; }
    }

    private string _sz_email;
    public string sz_email
    {
        get { return _sz_email; }
        set { _sz_email = value; }
    }

    private string _sz_name;
    public string sz_name
    {
            get{ return _sz_name;}
            set{ _sz_name=value;}
    }

   
    #endregion


}