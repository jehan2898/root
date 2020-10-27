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
using System.Threading;

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
            FileUploadControl.Visible = false;
            btnupload.Visible = false;
            lblUpload.Visible = false;
            
            try
            {   //Get new Ticket Id
                Session["uName"] = "Username";
                Session["uID"] = "UserId";
                Session["uploadCount"] = 1;

              //  string connectionString = ConfigurationManager.ConnectionStrings["Connection_String"].ToString();
               string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                DataSet dsTicketNo = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "sp_Get_Next_Ticketno");
                txtTicketNo.Text = dsTicketNo.Tables[0].Rows[0]["TicketNo"].ToString();
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
    
    protected void btnupload_Click(object sender, EventArgs e)
    {
       // Image1.Visible = true;
        
        Session["FilePath"] = FileUploadControl.FileName;
        string fPath = ConfigurationManager.AppSettings["TicketUploadPath"];

        

            if (FileUploadControl.HasFile)
            {
               if (FileUploadControl.PostedFile.ContentLength < 10240000)
               {
               
                       int uploadCount = Convert.ToInt32(Session["uploadCount"].ToString());

                             if (uploadCount < 6)
                            {
                                string tId = txtTicketNo.Text;
                                Session["tId"] = tId;
                                Directory.CreateDirectory(fPath + "/" + tId);

                                FileUploadControl.SaveAs(fPath + "/" + tId + "/" + FileUploadControl.FileName);
                                Label1.Text = "File Uploaded: " + FileUploadControl.FileName;
                                //Image1.Visible = false;
                                //saving data Tran table

                             //   string connectionString = ConfigurationManager.ConnectionStrings["EConnection"].ToString();
                                string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();
                                //DataSet DS = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "sp_Insert_Ticket_Trn", new SqlParameter("@sz_ticket_id", txtTicketNo.Text), new SqlParameter("@sz_file_path", Session["FilePath"].ToString()));

                                GridView1.Visible = true;

                               
                                GridView1.DataSourceID = SqlDataSource1.ID;
                                GridView1.DataBind();
                               
                                uploadCount = uploadCount + 1;
                                Session["uploadCount"] = uploadCount;
                            }
                            else
                            {
                                Label1.Text = "File upload limit is 5";
                            }
                        }
                        else
                        {
                            Label1.Text = "File upload limit is 10MB";
                        }

            }
                   
            else
            {
                Label1.Text = "No File Uploaded.";
            }
        
        
    

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
    }
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {        
        //Saving Ticket Data
        DateTime ticketDate = DateTime.Today;

      //  string connectionString = ConfigurationManager.ConnectionStrings["EConnection"].ToString();
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();

        SqlCommand cmd = connection.CreateCommand();

        cmd.Connection = connection;
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters
        cmd.CommandText = "sp_insert_ticket";
        cmd.Parameters.Add(new SqlParameter("@sz_ticket_id", txtTicketNo.Text));
        cmd.Parameters.Add(new SqlParameter("@sz_user_name", Session["uName"].ToString()));
        cmd.Parameters.Add(new SqlParameter("@sz_personal_name", txtName.Text));
        cmd.Parameters.Add(new SqlParameter("@sz_email", txtEmail.Text));
        cmd.Parameters.Add(new SqlParameter("@i_support_type", ddlSupport.SelectedValue));
        cmd.Parameters.Add(new SqlParameter("@sz_comments", txtAreaComments.Text));
        //cmd.Parameters.Add(new SqlParameter("@sz_file_path", Session["FilePath"].ToString()));
        cmd.ExecuteNonQuery();
        Label1.Text = "Ticket created,Please upload files ";
        FileUploadControl.Visible = true;
        btnupload.Visible = true;
        lblUpload.Visible = true;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
     

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string i;
        string tId = Session["tId"].ToString();
        string path = ConfigurationManager.AppSettings["TicketUploadPath"];
        path = path + "/" + tId;

        i = GridView1.DataKeys[e.RowIndex].Value.ToString();
        
        File.Delete(path + "/" + i);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      
    }
}

    
