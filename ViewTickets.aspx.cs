using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class ViewTickets : PageBase
{
    private Bill_Sys_BillingCompanyObject objCompany = null;
    private Bill_Sys_UserObject objUser = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        lblMessage.Text = "";

        if(!this.pUpdateTime.IsCallback){
            if (validateOnPageLoad())
            {
                BindTicketList();
            }
        }
    }

    private bool validateOnPageLoad()
    {
        objCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        if (objCompany == null)
        {
            lblErrorMessage.Text = "Your session has expired. You must re-login to access this page";
            lblErrorMessage.Visible = true;
            return false;
        }

        objUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];
        if (objUser == null)
        {
            lblErrorMessage.Text = "Your session has expired. You must re-login to access this page";
            lblErrorMessage.Visible = true;
            return false;
        }
        return true;
    }

    private void ResetControls()
    {
        this.htid.Set("tid", "");
        this.tDescription.Text = "";
    }
    
    private void BindTicketList()
    {
        tickets.TicketDAO dao = new tickets.TicketDAO();
        dao.CompanyID = objCompany.SZ_COMPANY_ID;
        dao.RaisedBy = objUser.SZ_USER_NAME;
        tickets.SrvTickets service = new tickets.SrvTickets();
        this.grdTickets.DataSource = service.GetTickets(dao);
        this.grdTickets.DataBind();
    }

    protected void onUpdateTime_CallBack(object source, DevExpress.Web.CallbackEventArgsBase b)
    {
        objCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        if (objCompany == null)
        {
            lblErrorMessage.Text = "Your session has expired. You must re-login to access this page";
            lblErrorMessage.Visible = true;
            return;
        }

        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        tickets.TicketDAO dao = new tickets.TicketDAO();
        dao.TicketID = Convert.ToInt64(this.htid.Get("tid").ToString());
        dao.CompanyID = objCompany.SZ_COMPANY_ID;
        BindThreads(dao);
        pUpdateTime.Visible = true;
        btnReply.Enabled = true;
    }

    private bool validateFormInput()
    {
        if (this.tDescription.Text.Trim().Length == 0)
        {
            lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            lblErrorMessage.Visible = true;
            return false;
        }

        try
        {
            if (this.htid.Get("tid").ToString().Trim().Length == 0)
            {
                lblErrorMessage.Text = "Selet a ticket from the list of tickets below before replying.";
                lblErrorMessage.Visible = true;
                return false;
            }
        }
        catch (System.Collections.Generic.KeyNotFoundException o)
        {
            lblErrorMessage.Text = "Selet a ticket from the list of tickets below before replying.";
            lblErrorMessage.Visible = true;
            return false;
        }
        return true;
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        if (!validateFormInput())
            return;

        tickets.SrvTickets service = new tickets.SrvTickets();
        tickets.TicketDAO dao = new tickets.TicketDAO();
        dao.Description = tDescription.Text;
        dao.Status = tickets.TicketStatus.USER_REPLY;
        dao.CompanyID = objCompany.SZ_COMPANY_ID;
        dao.TicketID = Convert.ToInt64(this.htid.Get("tid").ToString());

        tickets.TicketDAO retDao = service.AddTicketThread(dao);
        if (retDao != null)
        {
            try
            {
                int i = SendEmail(dao.TicketID);
                lblMessage.Text = "Your reply was successfully posted.";
                lblMessage.Visible = true;
                BindThreads(dao);
                BindTicketList();
                ResetControls();
                btnReply.Enabled = false;
            }
            catch (Exception o)
            {
                lblErrorMessage.Text = "ops! There was some technical problem closing the ticket. Please contact administrator for assistance";
                lblErrorMessage.Visible = true;
            }
        }
        else
        {
            lblErrorMessage.Text = "Oops! Some technical problem occurred while replying to the ticket. Please contact adminisrator for assistance.";
            lblErrorMessage.Visible = true;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (!validateFormInput())
            return;

        tickets.SrvTickets service = new tickets.SrvTickets();
        tickets.TicketDAO dao = new tickets.TicketDAO();
        dao.Description = tDescription.Text;
        dao.Status = tickets.TicketStatus.CLOSED;
        dao.CompanyID = objCompany.SZ_COMPANY_ID;
        dao.TicketID = Convert.ToInt64(this.htid.Get("tid").ToString());

        tickets.TicketDAO retDao = service.AddTicketThread(dao);
        
        if (retDao != null)
        {
            try
            {
                int i = SendEmail(dao.TicketID);
                lblMessage.Text = "The ticket was successfully closed";
                lblMessage.Visible = true;
                BindThreads(dao);
                BindTicketList();
                ResetControls();
                btnReply.Enabled = false;
            }
            catch (Exception o)
            {
                lblErrorMessage.Text = "Oops! Your ticket updates were successfully posted but there was some technical problem sending out emails. Please contact administrator for assistance";
                lblErrorMessage.Visible = true;
            }
        }
        else
        {
            lblErrorMessage.Text = "Oops! Some technical problem occurred while replying to the ticket. Please contact adminisrator for assistance.";
            lblErrorMessage.Visible = true;
        }
    }

    private void BindThreads(tickets.TicketDAO p_dao)
    {
        tickets.SrvTickets service = new tickets.SrvTickets();
        rThread.DataSource = service.GetThread(p_dao);
        rThread.DataBind();
    }

    private int SendEmail(long p_TicketID)
    {
        // fetch the ticket details before sending the email. this function receives the ticket id, loads the ticket data and uses it to send email

        string MailFrom = ConfigurationManager.AppSettings["FromMailID"].ToString();
        string MailPassword = ConfigurationManager.AppSettings["Password"].ToString();
        string MailCC = ConfigurationManager.AppSettings["CCEmail"].ToString();
        string Port = ConfigurationManager.AppSettings["SMTPPort"].ToString();
        string Server = ConfigurationManager.AppSettings["SMTPServer"].ToString();

        tickets.TicketDAO dao = new tickets.TicketDAO();
        dao.TicketID = p_TicketID;
        dao.CompanyID = objCompany.SZ_COMPANY_ID;

        tickets.SrvTickets service = new tickets.SrvTickets();
        dao = service.GetTicketForID(dao);

        ArrayList toMail = new ArrayList();
        try
        {
            string[] emailTo = dao.EmailCC.Split(',');
            if (dao.EmailCC.Contains(","))
            {
                emailTo = dao.EmailCC.ToString().Split(',');
            }
            if (emailTo.Length > 0)
            {
                for (int i = 0; i < emailTo.Length; i++)
                {
                    if (emailTo[i].ToString().Contains(";"))
                    {
                        string[] emailTo1 = dao.EmailCC.ToString().Split(';');
                        for (int j = 0; j < emailTo1.Length; j++)
                        {
                            toMail.Add(emailTo1[j].ToString());
                        }
                    }
                    else
                    {
                        toMail.Add(emailTo[i].ToString());
                    }
                }
            }
            else
            {
                return 0;
            }

            string num = dao.TicketNumber;
            string sz_message = tDescription.Text;

            string sSubjectStatus = "";
            if (dao.StatusCode == tickets.TicketStatusConstants.CLOSED)
            {
                sSubjectStatus = "[Ticket Closed ";
            }
            else
            {
                sSubjectStatus = "[Ticket Updated ";
            }

            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(MailFrom, dao.EmailDefault,sSubjectStatus + " - " + num + " ] " + dao.Subject, sz_message);

            if (dao.EmailCC.Trim().Length != 0)
            {
                MyMailMessage.CC.Add(dao.EmailCC);
            }

            MyMailMessage.CC.Add(MailCC);

            MyMailMessage.IsBodyHtml = false;
            string msgBody = "Dear " + dao.RaisedBy + ", \n\nTicket number - " + dao.TicketNumber + " has been updated. The summary of the update is mentioned below.\n\n";

            msgBody = msgBody + "\nTicket Reference Number: " + dao.TicketNumber + "\n";
            msgBody = msgBody + "Account Name: " + dao.AccountName + "\n";
            msgBody = msgBody + "Domain: " + dao.DomainName + "\n";
            msgBody = msgBody + "Type: " + dao.TypeText + "\n";
            msgBody = msgBody + "Status: " + dao.StatusText + "\n";
            msgBody = msgBody + "Issue: " + dao.Subject + "\n";
            msgBody = msgBody + "Issue Description:\n";
            msgBody = msgBody + tDescription.Text + "\n";

            msgBody = msgBody + "\nYou can view the updates made to this ticket in the View Tickets section of the website. \n";

            msgBody = msgBody + "\nThank You,";
            msgBody = msgBody + "\nGreenbills Support.\n";

            MyMailMessage.Body = msgBody;
            System.Net.NetworkCredential mailAuthentication = new
            System.Net.NetworkCredential(MailFrom, MailPassword);
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(Server, Convert.ToInt32(Port));

            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = mailAuthentication;

            try
            {
                mailClient.Send(MyMailMessage);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Oops! Your ticket was generated but an email could not be sent. Please contact the administrator for assistance";
                lblErrorMessage.Visible = true;
                return 0;
            }
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}