using System;

public class AddedEvetDetail
{
    private int eventID;

    private DateTime eventDate;

    private decimal eventTime;

    public DateTime EventDate
    {
        get
        {
            return this.eventDate;
        }
        set
        {
            this.eventDate = value;
        }
    }

    public int EventID
    {
        get
        {
            return this.eventID;
        }
        set
        {
            this.eventID = value;
        }
    }

    public decimal EventTime
    {
        get
        {
            return this.eventTime;
        }
        set
        {
            this.eventTime = value;
        }
    }

    public AddedEvetDetail()
    {
    }
}