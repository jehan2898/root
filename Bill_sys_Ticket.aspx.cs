/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Ticket.aspx.cs
/*Purpose              :       To Sent Mail With Attachment 
/*Author               :       Ashutosh
/*Date of creation     :       18 May 2010
/*Modified By          :
/*Modified Date        :
/************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using Microsoft.ApplicationBlocks.Data;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using System.IO;
//using DAO;
//using EmailSender;
using System.Threading;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class _Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (!IsPostBack)
        {
            FileUploadControl.Visible = true;
            btnupload.Visible = true;
            lblUpload.Visible = true;
            lblMsg.Text = "";
            lblMsg.Visible = false;
           
           
            try
            {   //Get new Ticket Id
                Session["uName"] = "Username";
                Session["uID"] = "UserId";
                Session["uploadCount"] = 1;

                //  string connectionString = ConfigurationManager.ConnectionStrings["Connection_String"].ToString();
                //string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                // SqlConnection connection = new SqlConnection(connectionString);
                // connection.Open();
                // DataSet dsTicketNo = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "sp_Get_Next_Ticketno");
                // txtTicketNo.Text = dsTicketNo.Tables[0].Rows[0]["TicketNo"].ToString();
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
    }

    private string getFileName(string p_szFileName)
    {
        String szBillNumber = "";
        szBillNumber = p_szFileName;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szFileName + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    protected void btnupload_Click(object sender, EventArgs e)
     {
        // Image1.Visible = true;

          //Session["FilePath"] = FileUploadControl.FileName;
          string fPath = ConfigurationManager.AppSettings["EXCEL_SHEET"];
          EmailSender.DAO_Message objMessage = new EmailSender.DAO_Message();
        
          if (FileUploadControl.HasFile)
          {
              if (FileUploadControl.PostedFile.ContentLength < 10240000)
              {
                    string fname = "";
                    fname = FileUploadControl.PostedFile.FileName;
                    string file = "";
                    file = FileUploadControl.FileName;
                    string filename = "";
                    filename = getFileName("Support") + file.Substring(file.IndexOf("."),(file.Length - file.IndexOf(".")));
                    
                    txtupload.Text = fPath +"/"+ filename;
                    FileUploadControl.SaveAs(fPath +"/"+ filename);  
                   // FileUploadControl.SaveAs(fPath);  

                    lblFileName.Text = FileUploadControl.FileName;
                      
              }
        
    }
    else
    {
        lblMsg.Text = "Please upload file of size less than 10MB";
    }

}


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
    }
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //Saving Ticket Data
        //DateTime ticketDate = DateTime.Today;

        //  string connectionString = ConfigurationManager.ConnectionStrings["EConnection"].ToString();
        //string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        //SqlConnection connection = new SqlConnection(connectionString);

        //connection.Open();

        //SqlCommand cmd = connection.CreateCommand();

        //cmd.Connection = connection;
        //cmd.CommandType = CommandType.StoredProcedure;

        //// Add Parameters
        //cmd.CommandText = "sp_insert_ticket";
        //cmd.Parameters.Add(new SqlParameter("@sz_ticket_id", txtTicketNo.Text));
        //cmd.Parameters.Add(new SqlParameter("@sz_user_name", Session["uName"].ToString()));
        //cmd.Parameters.Add(new SqlParameter("@sz_personal_name", txtName.Text));
        //cmd.Parameters.Add(new SqlParameter("@sz_email", txtEmail.Text));
        //cmd.Parameters.Add(new SqlParameter("@i_support_type", ddlSupport.SelectedValue));
        //cmd.Parameters.Add(new SqlParameter("@sz_comments", txtAreaComments.Text));
        ////cmd.Parameters.Add(new SqlParameter("@sz_file_path", Session["FilePath"].ToString()));
        //cmd.ExecuteNonQuery();
        //Label1.Text = "Ticket created,Please upload files ";
        //FileUploadControl.Visible = true;
        //btnupload.Visible = true;
        //lblUpload.Visible = true;
        try
        {
            //...code for sending mail...//

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            Service_Email se = new Service_Email();
            EmailSender.DAO_XEmailEntity d = new EmailSender.DAO_XEmailEntity();
            
            d.ToEmail = txtEmail.Text;//atul.d.jadhav@gmail.com
            d.MessageKey = "UserRegistration";
            d.AccountPassword = txtName.Text;
          
            string flname = txtupload.Text;


            EmailSender.DAO_XEmailEntity p_objEmail = new EmailSender.DAO_XEmailEntity();
            p_objEmail.ToEmail = txtEmail.Text;
            p_objEmail.MessageKey = "frommail";
            p_objEmail.AccountPassword = txtName.Text;
            p_objEmail.Subject = ddlSupport.SelectedItem.Text;
            p_objEmail.Body= txtAreaComments.Text;


            EmailSender.DAO_Message objSupportMessage = new EmailSender.DAO_Message();
            EmailSender.DAO_Message objAcknowleMessage = new EmailSender.DAO_Message();
            if (txtupload.Text != "")
            {
                objSupportMessage = se.SendMail(p_objEmail, flname);
                objAcknowleMessage = se.SendMail(d);
            }
            else
            {
                objSupportMessage = se.SendMail(p_objEmail);
                objAcknowleMessage = se.SendMail(d);
            }
            if (objSupportMessage != null && objAcknowleMessage != null)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Your request submitted successfully";
                txtEmail.Text = "";
                txtName.Text = "";
                ddlSupport.Text = "<--- Select --->";
                txtAreaComments.Text = "";
                lblFileName.Text = "";
                txtupload.Text = "";
                
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "There is a problem while sending request, please try after some time.";
            }
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


        //...End Of Code...//

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string i;
        //string tId = Session["tId"].ToString();
        //string path = ConfigurationManager.AppSettings["TicketUploadPath"];
        //path = path + "/" + tId;

        //i = GridView1.DataKeys[e.RowIndex].Value.ToString();

        //File.Delete(path + "/" + i);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "browse button";
    }
    protected void lbtnattach_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtName.Text = "";
            txtEmail.Text = "";
            ddlSupport.Text = "<--- Select --->";
            txtAreaComments.Text = "";
            lblFileName.Text = "";
            txtupload.Text = "";
            lblMsg.Text = "";
            lblMsg.Visible = false;
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
}
    
