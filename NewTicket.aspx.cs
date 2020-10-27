using BlazeFast.client;
using BlazeFast.Constants;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using System.IO;
using System.Data;
using BlazeFast;
using System.Configuration;
using tickets;
using System.Net.Mail;
using System.Net;
public partial class NewTicket : PageBase
{
    private Bill_Sys_BillingCompanyObject objCompany = null;
    private Bill_Sys_UserObject objUser = null;
    private string sDirToken=null;
    protected string sFrequentEmailsText = "";

    

    const string UploadDirectory = "~/UploadedDocuments/";

    public void BindPriority()
    {
        DataSet dataSet = new DataSet();
        dataSet = (new SrvTickets()).GetDLLTicketPriority();
        this.ddlPriority.TextField = "Text";
        this.ddlPriority.ValueField = "ID";
        this.ddlPriority.DataSource = dataSet;
        this.ddlPriority.DataBind();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (this.validateFormInput())
        {
            SrvTickets srvTicket = new SrvTickets();
            TicketDAO ticketDAO = new TicketDAO();
            ticketDAO.RaisedBy=this.tRaisedBy.Text;
            ticketDAO.Subject=this.tSubject.Text;
            ticketDAO.Description=this.tDescription.Text;
            ticketDAO.EmailCC=this.tEmailCC.Text;
            ticketDAO.EmailDefault=this.tDefaultEmail.Text;
            ticketDAO.CompanyID=this.objCompany.SZ_COMPANY_ID;
            ticketDAO.CallbackPhone=this.tCallBack.Text;
            ticketDAO.Priority=this.ddlPriority.Value.ToString();
            string str = this.cIssueType.Value.ToString();
            string str1 = str;
            if (str != null)
            {
                if (str1 == "backend_support")
                {
                    ticketDAO.Type= TicketType.BACKEND_SUPPORT;
                }
                else if (str1 == "data_import")
                {
                    ticketDAO.Type= TicketType.DATA_IMPORT;
                }
                else if (str1 == "feature_request")
                {
                    ticketDAO.Type= TicketType.FEATURE_REQUEST;
                }
                else if (str1 == "website_problem")
                {
                    ticketDAO.Type= TicketType.WEBSITE_PROBLEM;
                }
            }
            if (this.cSubTypes.Value != null)
            {
                string str2 = this.cSubTypes.Value.ToString();
                string str3 = str2;
                if (str2 != null)
                {
                    switch (str3)
                    {
                        case "samd_patient":
                            {
                                ticketDAO.SubType="SAMD-P";
                                break;
                            }
                        case "samd_visit":
                            {
                                ticketDAO.SubType="SAMD-V";
                                break;
                            }
                        case "samd_procedure":
                            {
                                ticketDAO.SubType="SAMD-PR";
                                break;
                            }
                        case "samd_diagnosis":
                            {
                                ticketDAO.SubType="SAMD-D";
                                break;
                            }
                        case "samd_bill":
                            {
                                ticketDAO.SubType="SAMD-B";
                                break;
                            }
                        case "excel_import":
                            {
                                ticketDAO.SubType="EXI";
                                break;
                            }
                        case "manual_import":
                            {
                                ticketDAO.SubType="MNI";
                                break;
                            }
                        case "routine_import":
                            {
                                ticketDAO.SubType="RTI";
                                break;
                            }
                        case "application_error":
                            {
                                ticketDAO.SubType="AE";
                                break;
                            }
                        case "reporting_error":
                            {
                                ticketDAO.SubType="RE";
                                break;
                            }
                        case "thrown_out":
                            {
                                ticketDAO.SubType="TO";
                                break;
                            }
                    }
                }
            }
            ticketDAO = srvTicket.AddTicket(ticketDAO);
            if (ticketDAO != null)
            {
                if (ticketDAO.TicketNumber == null)
                {
                    this.lblErrorMessage.Text = "Oops! Some technical problem occurred while creating a ticket for you. Please contact adminisrator for assistance.";
                    this.lblErrorMessage.Visible = true;
                    return;
                }
                if (ticketDAO.TicketNumber == "")
                {
                    this.lblErrorMessage.Text = "Oops! Some technical problem occurred while creating a ticket for you. Please contact adminisrator for assistance.";
                    this.lblErrorMessage.Visible = true;
                    return;
                }
                ArrayList arrayLists = new ArrayList();
                string empty = string.Empty;
                string fileName = string.Empty;
                string empty1 = string.Empty;
                if (this.fuFirst.HasFile)
                {
                    empty = this.fuFirst.FileName;
                    SaveTicketImages saveTicketImage = new SaveTicketImages();
                    saveTicketImage.FileName=empty;
                    saveTicketImage.FilePath=string.Concat("Ticket/", ticketDAO.TicketNumber, "/");
                    saveTicketImage.TicketNumber=ticketDAO.TicketNumber;
                    arrayLists.Add(saveTicketImage);
                }
                if (this.fuSecond.HasFile)
                {
                    fileName = this.fuSecond.FileName;
                    SaveTicketImages saveTicketImage1 = new SaveTicketImages();
                    saveTicketImage1.FileName=fileName;
                    saveTicketImage1.FilePath=string.Concat("Ticket/", ticketDAO.TicketNumber, "/");
                    saveTicketImage1.TicketNumber=ticketDAO.TicketNumber;
                    arrayLists.Add(saveTicketImage1);
                }
                if (this.fuThird.HasFile)
                {
                    empty1 = this.fuThird.FileName;
                    SaveTicketImages saveTicketImage2 = new SaveTicketImages();
                    saveTicketImage2.FileName=empty1;
                    saveTicketImage2.FilePath=string.Concat("Ticket/", ticketDAO.TicketNumber, "/");
                    saveTicketImage2.TicketNumber=ticketDAO.TicketNumber;
                    arrayLists.Add(saveTicketImage2);
                }
                if (empty != string.Empty || fileName != string.Empty || empty1 != string.Empty)
                {
                    string str4 = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                    string str5 = string.Concat(str4, "Ticket/", ticketDAO.TicketNumber, "/");
                    if (!Directory.Exists(str5))
                    {
                        Directory.CreateDirectory(str5);
                    }
                    if (empty != string.Empty && !File.Exists(string.Concat(str5, empty)))
                    {
                        this.fuFirst.SaveAs(string.Concat(str5, empty));
                    }
                    if (fileName != string.Empty && !File.Exists(string.Concat(str5, fileName)))
                    {
                        this.fuFirst.SaveAs(string.Concat(str5, fileName));
                    }
                    if (empty1 != string.Empty && !File.Exists(string.Concat(str5, empty1)))
                    {
                        this.fuFirst.SaveAs(string.Concat(str5, empty1));
                    }
                }
                if (arrayLists.Count > 0)
                {
                    (new SaveTicketImages()).SaveImages(arrayLists);
                }
                this.lblMessage.Text = "Ticket submitted successfully. We will ensure that your request is attended at the earliest. We request you to be patient and visit the View Tickets page to see the updates on your ticket.";
                this.lblTicketNumber.Text = "Use your ticket reference number - " + ticketDAO.TicketNumber + " for all further communication related to this issue. Thank You.";
                this.SendEmail(ticketDAO.TicketNumber);
                this.btnCreate.Enabled = false;
                this.resetControls();
                return;
            }
            this.lblErrorMessage.Text = "Oops! Some technical problem occurred while creating a ticket for you. Please contact adminisrator for assistance.";
            this.lblErrorMessage.Visible = true;
        }
    }

    protected void changeSubType(object sender, CallbackEventArgsBase e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.cIssueType.Value.ToString().Equals("feature_request") || this.cIssueType.Value.ToString().Equals("backend_support"))
        {
            this.cSubTypes.Enabled = false;
            return;
        }
        try
        {
            this.cSubTypes.Enabled = true;
            this.cSubTypes.DataSource = StaticSubTypes.LoadSubTypes(this.cIssueType.Value.ToString());
            this.cSubTypes.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.validateOnPageLoad();
        this.lblErrorMessage.Text = "";
        this.lblMessage.Text = "";
        if (!this.Page.IsPostBack)
        {
            this.BindPriority();
            this.setFormDefaults();
            this.hdDirToken.Value = this.tRaisedBy.Text;
        }
        try
        {
            Client_Select clientSelect = new Client_Select();
            DataSet dataSet = clientSelect.Select_Tickets_FrequentEmails(SrvCacheKeyConstants.KEY_TICKETS_FREQUENT_EMAILS, this.tRaisedBy.Text);
            if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
            {
                this.tEmailCC.Text = dataSet.Tables[0].Rows[0]["sz_mail_cc"].ToString();
            }
            this.sFrequentEmailsText = "Frequently used email addresses are displayed by default";
        }
        catch (Exception ex)
        {
            this.sFrequentEmailsText = "Frequently used email addresses could not be loaded this time";

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void resetControls()
    {
        this.cIssueType.SelectedIndex = -1;
        this.ddlPriority.SelectedIndex = -1;
        this.tDefaultEmail.Text = "";
        this.tDescription.Text = "";
        this.tSubject.Text = "";
        this.tEmailCC.Text = "";
    }

    private int SendEmail(string ticketNumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num;
        string str = ConfigurationManager.AppSettings["TicketOpenTemplete"].ToString();
        string MailFrom = ConfigurationManager.AppSettings["FromMailID"].ToString();
        string MailPassword = ConfigurationManager.AppSettings["Password"].ToString();
        string MailCC = ConfigurationManager.AppSettings["CCEmail"].ToString();
        string Port = ConfigurationManager.AppSettings["SMTPPort"].ToString();
        string Server = ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string str1 = "";
        StreamReader streamReader = new StreamReader(str);
        do
        {
            str1 = string.Concat(str1, streamReader.ReadLine(), "\r\n");
        }
        while (streamReader.Peek() != -1);
        ArrayList arrayLists = new ArrayList();
        try
        {
            string[] strArrays = this.tEmailCC.Text.ToString().Split(new char[] { ',' });
            if (this.tEmailCC.Text.Contains(","))
            {
                strArrays = this.tEmailCC.Text.ToString().Split(new char[] { ',' });
            }
            if ((int)strArrays.Length <= 0)
            {
                num = 0;
            }
            else
            {
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    if (!strArrays[i].ToString().Contains(";"))
                    {
                        arrayLists.Add(strArrays[i].ToString());
                    }
                    else
                    {
                        string[] strArrays1 = this.tEmailCC.Text.ToString().Split(new char[] { ';' });
                        for (int j = 0; j < (int)strArrays1.Length; j++)
                        {
                            arrayLists.Add(strArrays1[j].ToString());
                        }
                    }
                }
                string str2 = ticketNumber;
                string text = this.tDescription.Text;
                MailMessage mailMessage = new MailMessage(MailFrom, this.tDefaultEmail.Text.ToString(), string.Concat("[Ticket Opened - ", str2, " ] ", this.tSubject.Text), text);
                if (this.tEmailCC.Text.Trim().Length != 0)
                {
                    mailMessage.CC.Add(this.tEmailCC.Text);
                }
                mailMessage.CC.Add(MailCC);
                mailMessage.IsBodyHtml = true;
                str1 = str1.Replace("@@TicketNumber@@", ticketNumber);
                str1 = str1.Replace("@@CompanyName@@", this.objCompany.SZ_COMPANY_NAME);
                str1 = str1.Replace("@@DomainName@@", this.objUser.DomainName);
                str1 = str1.Replace("@@UserName@@", this.tRaisedBy.Text);
                str1 = str1.Replace("@@Status@@", "Open");
                str1 = str1.Replace("@@Prioity@@", this.ddlPriority.Text);
                str1 = str1.Replace("@@Subject@@", this.tSubject.Text);
                str1 = str1.Replace("@@Description@@", this.tDescription.Text);
                mailMessage.Body = str1;
                NetworkCredential networkCredential = new NetworkCredential(MailFrom, MailPassword);
                SmtpClient smtpClient = new SmtpClient(Server, Convert.ToInt32(Port))
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = networkCredential
                };
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception exception)
                {
                    this.lblErrorMessage.Text = "Oops! Your ticket was generated but an email could not be sent. Please contact the administrator for assistance";
                    this.lblErrorMessage.Visible = true;
                    num = 0;
                    return num;
                }
                num = 1;
            }
        }
        catch (Exception ex)
        {
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }return num;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        
    }

    private void setFormDefaults()
    {
        this.tDefaultEmail.Text = this.objUser.SZ_USER_EMAIL;
        this.tRaisedBy.Text = this.objUser.SZ_USER_NAME;
    }

    private bool validateFormInput()
    {
        if (this.cIssueType.Value.ToString().ToLower().Equals("not_selected"))
        {
            this.lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        if (this.ddlPriority.Value.ToString() == "")
        {
            this.lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        if (this.tDefaultEmail.Text.Trim().Length == 0)
        {
            this.lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        if (this.tSubject.Text.Trim().Length == 0)
        {
            this.lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        if (this.tDescription.Text.Trim().Length == 0)
        {
            this.lblErrorMessage.Text = "Fields marked with asterix (*) are mandatory. Enter the missing data and re-submit the ticket.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        this.objCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        if (this.objCompany != null)
        {
            return true;
        }
        this.lblErrorMessage.Text = "Your session has expired. You must re-login to access this page.";
        this.lblErrorMessage.Visible = true;
        return false;
    }

    private bool validateOnPageLoad()
    {
        this.objCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        if (this.objCompany == null)
        {
            this.lblErrorMessage.Text = "Your session has expired. You must re-login to access this page.";
            this.lblErrorMessage.Visible = true;
            return false;
        }
        this.objUser = (Bill_Sys_UserObject)this.Session["USER_OBJECT"];
        if (this.objUser != null)
        {
            return true;
        }
        this.lblErrorMessage.Text = "Your session has expired. You must re-login to access this page.";
        this.lblErrorMessage.Visible = true;
        return false;
    }


}