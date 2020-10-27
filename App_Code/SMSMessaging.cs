using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

/// <summary>
/// Summary description for SMSMessaging
/// </summary>
public class SMSMessaging
{
    public SMSMessaging()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string SendReminder(int i_schedule_id)
    {

        DataSet ds = new DataSet();
        Patient_TVBO _patient_TVBO = new Patient_TVBO();
        ds = _patient_TVBO.GetAppointPatientDetails_For_Reminder(i_schedule_id);
        String PatientPhone = string.Empty;
        String PatientName = string.Empty;
        String Office = string.Empty;
        String Specialty = string.Empty;
        String EventDate = string.Empty;
        string Time = string.Empty;

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            PatientPhone = ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
            PatientName = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
            Office = ds.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString();
            Specialty = ds.Tables[0].Rows[0]["SPECIALTY"].ToString();
            EventDate = ds.Tables[0].Rows[0]["EVENT_DATE"].ToString();
            Time = ds.Tables[0].Rows[0]["START_TIME"].ToString() + " " + ds.Tables[0].Rows[0]["AM_PM"].ToString();
        }
        try
        {
            string accountSid = ConfigurationManager.AppSettings.Get("TWILIO_ACCOUNT_ID");      //-- Account SID from twilio.com/console : AC48ba9355b0bae1234caa9e29dc73b407                            
            string authToken = ConfigurationManager.AppSettings.Get("TWILIO_AUTH_TOKEN");       //-- bAuth Token from twilio.com/console : 74b9f9f1c60c200d28b8c5b22968e65f
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(PatientPhone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""));
            var message = MessageResource.Create(
             to,
             from: new PhoneNumber(ConfigurationManager.AppSettings.Get("TWILIO_FROM_PHN")), //-- +14252150865
             body: "You have an appointment for " + Specialty + " at : " + EventDate + " " + Time + " " + Office + " .");
           
            return message.Sid;
        }
        catch (Exception ex)
        {

            return "Error";

        }
       


    }
    public static string SendSMS(string szpatientPnumber, string sz_sms)
    {



        try
        {
            string accountSid = ConfigurationManager.AppSettings.Get("TWILIO_ACCOUNT_ID");      //-- Account SID from twilio.com/console : AC48ba9355b0bae1234caa9e29dc73b407                            
            string authToken = ConfigurationManager.AppSettings.Get("TWILIO_AUTH_TOKEN");       //-- bAuth Token from twilio.com/console : 74b9f9f1c60c200d28b8c5b22968e65f
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(szpatientPnumber);
            var message = MessageResource.Create(
             to,
             from: new PhoneNumber(ConfigurationManager.AppSettings.Get("TWILIO_FROM_PHN")), //-- +14252150865
             body: sz_sms);
            return message.Sid;
           
        }
        catch (Exception ex)
        {

            return "Error";

        }
       


    }

}