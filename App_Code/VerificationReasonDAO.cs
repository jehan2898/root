using System;

public class VerificationReasonDAO
{
    private int iReasonID;
    private string sCompanyID;
    private string sReason;
    private string sz_created;
    private string sz_modified;
    public VerificationReasonDAO()
	{
	}

    public int ReasonID
    {
        set { this.iReasonID = value;}
        get { return iReasonID; }
    }

    public string CompanyID
    {
        set { this.sCompanyID = value; }
        get { return sCompanyID; }
    }

    public string Reason
    {
        set { this.sReason = value; }
        get { return sReason; }
    }

    public string szcreated
    {
        set { this.sz_created = value; }
        get { return sz_created; }
    }

    public string szmodified
    {
        set { this.sz_modified = value; }
        get { return sz_modified; }
    }
}
