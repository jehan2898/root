using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
[Serializable]
public class DAO_Assign_Doc
{
    public DAO_Assign_Doc()
    {
    }
    private string szSelectedId;
    private string szSelectedText;
    private string szSelectedRole;
    private string szSelectedRoleID;
    private string szSelectedSpeciality;
    private string szSelectedSpecialityID;
    private bool BT_REQUIRED_MULTIPLE;
    private int I_ORDER;

    public int ORDER
    {
        get
        {
            return I_ORDER;
        }
        set
        {
            I_ORDER = value;
        }
    }

    public bool REQUIRED_MULTIPLE
    {
        get
        {
            return BT_REQUIRED_MULTIPLE;
        }
        set
        {
            BT_REQUIRED_MULTIPLE = value;
        }
    }

    public string SelectedSpecialityID
    {
        get
        {
            return szSelectedSpecialityID;
        }
        set
        {
            szSelectedSpecialityID = value;
        }
    }

    public string SelectedSpeciality
    {
        get
        {
            return szSelectedSpeciality;
        }
        set
        {
            szSelectedSpeciality = value;
        }
    }
    
    public string SelectedId
    {
        get
        {
            return szSelectedId;
        }
        set
        {
            szSelectedId = value;
        }
    }
    public string SelectedText
    {
        get
        {
            return szSelectedText;
        }
        set
        {
            szSelectedText = value;
        }
    }
    public string SelectedRole
    {
        get
        {
            return szSelectedRole;
        }
        set
        {
            szSelectedRole = value;
        }
    }
    public string SelectedRoleID
    {
        get
        {
            return szSelectedRoleID;
        }
        set
        {
            szSelectedRoleID = value;
        }
    }
}

