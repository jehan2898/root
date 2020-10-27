using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    public void sendMail(string msg)
    {
        try
        {
            string from = ConfigurationManager.AppSettings["From"].ToString(); //Replace this with your own correct Gmail Address
            string to = ConfigurationManager.AppSettings["To"].ToString();//Replace this with the Email Address to whom you want to send the mail
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.CC.Add("prashant.z@procomsys.in");
            string subject = ConfigurationManager.AppSettings["Subject"].ToString();
            string logo = ConfigurationManager.AppSettings["Logo"].ToString();
            mail.From = new MailAddress(from, logo, System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = msg;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            //Add the Creddentials- use your own email id and password
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            client.Credentials = new System.Net.NetworkCredential(from, pwd);
            client.Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString());  // Gmail works on this port
            client.Host = ConfigurationManager.AppSettings["Host"].ToString(); ;
            client.EnableSsl = true; //Gmail works on Server Secured Layer
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                HttpContext.Current.Response.Write(errorMessage);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}
