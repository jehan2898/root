using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Bill_Sys_UserContactDetails : PageBase
{

    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    protected string token = "", veriLink = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            btnSubmitDetail.Attributes.Add("onclick", "return Validate();");
            txtUserName.Text = Session["UserName"].ToString();
            txtEmail.Text= Session["EmailId"].ToString();
            lblErrorMsg.Text = "This is a one time verification of your email.";
           // lblDomainError.Text = "An activation email has been sent. Please check your email";
            lblDomainError.Text = "";
        }
    }
    protected void btnSubmitDetail_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string emailAddres = ConfigurationManager.AppSettings["FromMailID"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        int port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
        string EmailServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();

        if (updateDetails())
        {
            token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();
            veriLink = ConfigurationManager.AppSettings["EmailVerfication"].ToString() + "?token=" + token;
            if (AddRenewAccountVeriToken(token))
            {
               
                    try
                    {
                        string sz_message = "Dear User," + "<br />" + "Congratulations! Your Personal details have been updated successfully, but before you can" + "<br />" + "login you need to complete a brief account verification process. <br />";
                        sz_message = sz_message + "<a href='" + veriLink + "'>Click Here</a> to verify your email ID." + "<br />" + "<br />" + "<br />";
                        sz_message = sz_message + "Thanks" + "<br />" + "Green Bills LLC";

                        System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(emailAddres, txtEmail.Text,
                       "Email Verification Link", sz_message);
                        MyMailMessage.IsBodyHtml = true;

                        MyMailMessage.Body = sz_message;
                        System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(emailAddres, Password);

                        System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(EmailServer, port);
                        mailClient.EnableSsl = true;

                        mailClient.UseDefaultCredentials = false;

                        mailClient.Credentials = mailAuthentication;

                        mailClient.Send(MyMailMessage);

                        lblDomainError.Text = "An activation email has been sent. Please check your email";

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
                    lblDomainError.Text = "Error sending Verification Link.";
                    }
                 

              
            }
            else
            { lblDomainError.Text = "Error generating Verification Link."; }
        }
        else
        { lblDomainError.Text = "Error updating records."; }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }            
    private bool updateDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool status = false;
        try
        {
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            sqlCon = new SqlConnection(strsqlCon);

            sqlCon.Open();


            sqlCmd = new SqlCommand("UPDATE MST_USERS SET  SZ_EMAIL='" + txtEmail.Text + "',sz_primary_phone='" + txtPhone.Text + "'WHERE SZ_USER_NAME = '" + txtUserName.Text + "'", sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            
            sqlCmd.ExecuteNonQuery();
            status = true;
        }
        catch (SqlException ex)
        {
            status = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        finally
        {
            sqlCon.Close();
        }
        return status;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    public bool AddRenewAccountVeriToken(string token)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool status = true;
        try
        {
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            sqlCon = new SqlConnection(strsqlCon);

            sqlCon.Open();


            sqlCmd = new SqlCommand("UPDATE MST_USERS SET  sz_token='" + token + "',dt_token_expiry='" + DateTime.Now.AddYears(1)+ "'WHERE SZ_USER_NAME = '" + txtUserName.Text + "'", sqlCon);
            sqlCmd.CommandType = CommandType.Text;
           
            sqlCmd.ExecuteNonQuery();
            status = true;
        }
        catch (SqlException ex)
        {
            status = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        finally
        {
            sqlCon.Close();
        }
        return status;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
   
      
    }
