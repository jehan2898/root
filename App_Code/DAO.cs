using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EmailHelper;


    public class DAO_Lead
    {
        private string szLeadID;            // auto generated unique system ID of the lead
        private string szManualID;          // the lead manual ID
        private string szLeadSource;        // the lead source - Internet, Mail, Personal etc.
        private string szLeadFirstName;     // the lead first name
        private string szLeadLastName;      // the lead last name
        private string szLeadState;         // the state location of the lead
        private string szLeadPhone;         // the contact number of the lead
        private string szLeadAssignedTo;    // the person to whom the lead is assigned by the administrator
        private string szCreatedOn;         // date on which the lead was created
        private string szCreatedBy;         // the person who created the lead
        private string szFirstLastName;     // the first and last name (combined) for the lead

        public string LeadSource
        {
            get
            {
                return szLeadSource;
            }
            set
            {
                szLeadSource = value;
            }
        }
            
        public string FirstLastName
        {
            get
            {
                return szFirstLastName;
            }
            set
            {
                szFirstLastName = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return szCreatedBy;
            }
            set
            {
                szCreatedBy = value;
            }
        }

        public string CreatedOn
        {
            get
            {
                return szCreatedOn;
            }
            set
            {
                szCreatedOn = value;
            }
        }
        
        public string LeadAssignedTo
        {
            get
            {
                return szLeadAssignedTo;
            }
            set
            {
                szLeadAssignedTo = value;
            }
        }
        
        public string LeadPhone
        {
            get
            {
                return szLeadPhone;
            }
            set
            {
                szLeadPhone = value;
            }
        }
        
        public string LeadState
        {
            get
            {
                return szLeadState;
            }
            set
            {
                szLeadState = value;
            }
        }
        
        public string LeadLastName
        {
            get
            {
                return szLeadLastName;
            }
            set
            {
                szLeadLastName = value;
            }
        }

        public string LeadFirstName
        {
            get
            {
                return szLeadFirstName;
            }
            set
            {
                szLeadFirstName = value;
            }
        }

        public string ManualID
        {
            get
            {
                return szManualID;
            }
            set
            {
                szManualID = value;
            }
        }

        public string LeadID
        {
            get
            {
                return szLeadID;
            }
            set
            {
                szLeadID = value;
            }
        }
    }
    public class DAO_XEmailEntity : EmailHelper.EmailEntity
    {
        
        private String szMessageKey;

        public string MessageKey
        {
            get
            {
                return szMessageKey;
            }
            set
            {
                szMessageKey = value;
            }
        }
    }
    public class DAO_OptionMenu
    {
        private string szMenuID;
        private Boolean blnIsParent;
        private string szURL;
        private string szDescription;
        private string szMenuName;

        public string MenuID
        {
            get
            {
                return szMenuID;
            }
            set
            {
                szMenuID = value;
            }
        }

        public string URL
        {
            get
            {
                return szURL;
            }
            set
            {
                szURL = value;
            }
        }

        public Boolean isParent
        {
            get
            {
                return blnIsParent;
            }
            set
            {
                blnIsParent = value;
            }
        }

        public string Description
        {
            get
            {
                return szDescription;
            }
            set
            {
                szDescription = value;
            }
        }
        
        
        public string MenuName
        {
            get
            {
                return szMenuName;
            }
            set
            {
                szMenuName = value;
            }
        }

        public DAO_OptionMenu()
        {

        }
    }
    public class DAO_User
    {
        private string szUserName;
        private string szUserID;
        private string szUserRoleID;
        
        public DAO_User()
        {
        }

        public string UserName
        {
            get
            {
                return szUserName;
            }
            set
            {
                szUserName = value;
            }
        }

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

        public string UserRoleID
        {
            get
            {
                return szUserRoleID;
            }
            set
            {
                szUserRoleID = value;
            }
        }
    }

public class DAO_BillingCompanyRegistration
{
    // registration details
    private string strUserName;
    private string strPassword;
    private string strConfirmPassword;
    private string strCompanyName;
    private string strStreet;
    private string strCity;
    private string strState;
    private string strZip;
    private string strPhone;

    private string strEmailID;
    private string strFirstName;
    private string strLastName;
    private string strAdminEmail;
    private string strOfficePhone;
    private string strOfficeExt;
    //private string strContactEmail;
    private string strAccountType;
    private string strScheme;
    private string strNewGUID;
    private string strSchemeID;

    // case prefix
    private string strCasePrefix;

    public string AccountType
    {
        get
        {
            return strAccountType;
        }
        set
        {
            strAccountType = value;
        }
    }

    public string SchemeID
    {
        get
        {
            return strSchemeID;
        }
        set
        {
            strSchemeID = value;
        }
    }

    public string UserName
    {
        get
        {
            return strUserName;
        }
        set
        {
            strUserName = value;
        }
    }

    public string Password
    {
        get
        {
            return strPassword;
        }
        set
        {
            strPassword = value;
        }
    }

    public string ConfirmPassword
    {
        get
        {
            return strConfirmPassword;
        }
        set
        {
            strConfirmPassword = value;
        }
    }

    public string Street
    {
        get
        {
            return strStreet;
        }
        set
        {
            strStreet = value;
        }
    }

    public string CompanyName
    {
        get
        {
            return strCompanyName;
        }
        set
        {
            strCompanyName = value;
        }
    }

    public string City
    {
        get
        {
            return strCity;
        }
        set
        {
            strCity = value;
        }
    }

    public string Phone
    {
        get
        {
            return strPhone;
        }
        set
        {
            strPhone = value;
        }
    }

    public string State
    {
        get
        {
            return strState;
        }
        set
        {
            strState = value;
        }
    }

    public string Zip
    {
        get
        {
            return strZip;
        }
        set
        {
            strZip = value;
        }
    }

    public string EmailID
    {
        get
        {
            return strEmailID;
        }
        set
        {
            strEmailID = value;
        }
    }

    public string FirstName
    {
        get
        {
            return strFirstName;
        }
        set
        {
            strFirstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return strLastName;
        }
        set
        {
            strLastName = value;
        }
    }

    public string AdminEmail
    {
        get
        {
            return strAdminEmail;
        }
        set
        {
            strAdminEmail = value;
        }
    }

    public string OfficePhone
    {
        get
        {
            return strOfficePhone;
        }
        set
        {
            strOfficePhone = value;
        }
    }

    public string OfficeExt
    {
        get
        {
            return strOfficeExt;
        }
        set
        {
            strOfficeExt = value;
        }
    }

    public string Scheme
    {
        get
        {
            return strScheme;
        }
        set
        {
            strScheme = value;
        }
    }

    //public string ContactEmail
    //{
    //    get
    //    {
    //        return strContactEmail;
    //    }
    //    set
    //    {
    //        strContactEmail = value;
    //    }
    //}

    public string NewGUID
    {
        get
        {
            return strNewGUID;
        }
        set
        {
            strNewGUID = value;
        }
    }

    // case prefix
    public string CasePrefix
    {
        get
        {
            return strCasePrefix;
        }
        set
        {
            strCasePrefix = value;
        }
    }

    public DAO_BillingCompanyRegistration()
    {
    }
}
