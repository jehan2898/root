using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for Bill_Sys_Verification_Desc
/// </summary>
public class Bill_Sys_Verification_Desc
{
    private string _sz_verification_reason;
    public string _sz_verification_reasons
    {
        get
        {
            return _sz_verification_reason;
        }
        set
        {
            _sz_verification_reason = value;
        }
    }
    private string _sz_bill_no;
    public string sz_bill_no
    {
        set
        {
            _sz_bill_no = value;
        }
        get
        {
            return _sz_bill_no;
        }
    }

    private string _sz_description;
    public string sz_description
    {
        get
        {
            return _sz_description;
        }
        set
        {
            _sz_description = value;
        }
    }

    private string _sz_verification_date;

    public string  sz_verification_date
    {
        set
        {
            _sz_verification_date = value;
        }
        get
        {
            return _sz_verification_date;
        }
    }

    private int _i_varification_type;
    public int i_verification
    {
        get
        {
            return _i_varification_type;
        }
        set
        {
            _i_varification_type = value;
        }

    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }
    private string _sz_user_id;
    public string sz_user_id
    {
        set
        {
            _sz_user_id = value;
        }
        get
        {
            return _sz_user_id;
        }
    }

    private string _sz_flag;
    public string sz_flag
    {
        set
        {
            _sz_flag = value;
        }
        get
        {
            return _sz_flag;
        }
    }

    private string _sz_verification_id;
    public string sz_verification_id
    {
        get
        {
            return _sz_verification_id;
        }
        set
        {
            _sz_verification_id = value;
        }
    }

    private string _sz_case_id;
    public string sz_case_id
    {
        get
        {
            return _sz_case_id;
        }
        set
        {
            _sz_case_id = value;
        }
    }

    private string _sz_answer;
    public string sz_answer
    {
        get
        {
            return _sz_answer;
        }
        set
        {
            _sz_answer = value;
        }
    }

    private string _sz_answer_id;
    public string sz_answer_id
    {
        get
        {
            return _sz_answer_id;
        }
        set
        {
            _sz_answer_id = value;
        }
    }

}
public class Bill_sys_Verification_Pop
{
    private string _sz_company_id;
    public string sz_company_id
    {
        set
        {
            _sz_company_id = value;
        }
        get
        {
            return _sz_company_id;
        }
    }

    private string _sz_bill_no;
    public string sz_bill_no
    {
        set
        {
            _sz_bill_no = value;
        }
        get
        {
            return _sz_bill_no;
        }
    }
    private string _i_verification_id;
    public string i_verification_id
    {
        set
        {
            _i_verification_id = value;
        }
        get
        {
            return _i_verification_id;
        }
    }
    private string _sz_bill_status;
    public string sz_bill_Status
    {
        set
        {
            _sz_bill_status = value;
        }
        get
        {
            return _sz_bill_status;
        }
    }
    private string _sz_flag;
    public string sz_flag
    {
        set
        {
            _sz_flag = value;
        }
        get
        {
            return _sz_flag;
        }
    }
   
}
public class Bill_sys_litigantion
{
   private string _sz_bill_no;
    public string sz_bill_no
    {
        get
        {
            return _sz_bill_no;
        }
        set
        {
            _sz_bill_no = value;
        }
    }
}
