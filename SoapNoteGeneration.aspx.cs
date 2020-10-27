using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DevExpress.CodeParser;
using PDFgenerator;
using System.Linq;
using System.Collections;
using DevExpress.Web;

public partial class SoapNoteGeneration : System.Web.UI.Page
{
    clsPDFGenerator pdfGenerator;
    string FilePath = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnUpload.Attributes.Add("onclick", "javascript: validate();");
            pdfGenerator = new clsPDFGenerator();
            if (!IsPostBack)
            {
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                bind_grid();


            }


        }
        catch (Exception ex)
        {

        }
    }

    //protected void btnGeneratePDF_Click(object sender, EventArgs e)
    //{        
    //    pdfGenerator.GeneratePDF(@"D:\c4.2.pdf", @"D:\c4.2.pdf", extddlDoctor.Selected_Text, DateTime.Now.ToShortDateString(), rdlhederfooter.SelectedValue == "0" ? Enums.HeaderFooter.Header : (rdlhederfooter.SelectedValue == "1" ? Enums.HeaderFooter.Footer : Enums.HeaderFooter.Both),
    //        rdlPosition.SelectedValue == "0" ? Enums.HeaderLocation.Right : (rdlPosition.SelectedValue == "1" ? Enums.HeaderLocation.Center : Enums.HeaderLocation.Left),
    //        rdlPosition.SelectedValue == "0" ? Enums.FooterLocation.Right : (rdlPosition.SelectedValue == "1" ? Enums.FooterLocation.Center : Enums.FooterLocation.Left));

    //}
    


    public void bind_grid()
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {

            SqlCommand comm = new SqlCommand("Get_Template_Name", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);


            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
            grdSoapNote.DataSource = ds;
            grdSoapNote.DataBind();
            //Session["Dataset"] = ds;
            // }

        }
        catch (Exception ex)
        {

        }



    }
    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 10000).ToString();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        //Get path from web.config file to upload
        string FilePath = ApplicationSettings.GetParameterValue("PhysicalBasePath") + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/SoapNote/";
        bool blSucces = false;
        string filename = string.Empty;
        //To check whether file is selected or not to uplaod
        if (FileUploadtoserver.HasFile)
        {
            try
            {
                string randomName = getRandomNumber();
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string[] allowdFile = { ".pdf" };
                //Here we are allowing only pdf file so verifying selected file pdf or not
                string FileExt = System.IO.Path.GetExtension(FileUploadtoserver.PostedFile.FileName);
                bool isValidFile = allowdFile.Contains(FileExt);
                if (!isValidFile)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Please upload only pdf ";
                }
                else
                {
                    // Get size of uploaded file, here restricting size of file
                    int FileSize = FileUploadtoserver.PostedFile.ContentLength;


                    //Get file name of selected file
                    filename = Path.GetFileName(FileUploadtoserver.FileName);
                    //Save selected file into specified location
                    FileUploadtoserver.SaveAs(FilePath + filename);

                    pdfGenerator.GeneratePDF(FilePath + filename, FilePath + randomName + filename, extddlDoctor.Selected_Text, extddlDoctor.Selected_Text, rdlhederfooter.SelectedValue == "0" ? Enums.HeaderFooter.Header : (rdlhederfooter.SelectedValue == "1" ? Enums.HeaderFooter.Footer : Enums.HeaderFooter.Both), rdlPosition.SelectedValue == "0" ? Enums.HeaderLocation.Right : (rdlPosition.SelectedValue == "1" ? Enums.HeaderLocation.Center : Enums.HeaderLocation.Left),

            rdlFooterPosition.SelectedValue == "0" ? Enums.FooterLocation.Right : (rdlFooterPosition.SelectedValue == "1" ? Enums.FooterLocation.Center : Enums.FooterLocation.Left));
                    lblMsg.Text = "File upload successfully!";
                    blSucces = true;



                }
                string openUrl = ApplicationSettings.GetParameterValue("DocumentManagerUrl") + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/SoapNote/" + randomName + filename;
                if (blSucces)
                {
                    Updatefileinfo(FilePath, randomName + filename, openUrl);
                }
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + openUrl + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
                bind_grid();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error occurred while uploading a file: " + ex.Message;
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to upload.";
        }
        //Store file details into database

    }
    private void Updatefileinfo(string file_path, string filename, string linkUrl)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {

            SqlCommand comm = new SqlCommand("Insert_template_by_doctorID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
            comm.Parameters.AddWithValue("@sz_doctor_id", extddlDoctor.Text);
            comm.Parameters.AddWithValue("@file_path", file_path);
            comm.Parameters.AddWithValue("@Link_Url", linkUrl);
            comm.Parameters.AddWithValue("@template_name", filename);

            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);



        }
        catch (Exception)
        {

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ArrayList arrFile = new ArrayList();
        
        //string SzUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        foreach (DataGridItem dgiItem in grdSoapNote.Items)
        {
            CheckBox chk = (CheckBox)dgiItem.Cells[5].FindControl("ChkDelete");
            if (chk.Checked == true)

        //        for (int i = 0; i < grdSoapNote.VisibleRowCount; i++)
        //{
        //    GridViewDataColumn c = (GridViewDataColumn)grdSoapNote.Columns[7];
        //    CheckBox chk = (CheckBox)grdSoapNote.FindRowCellTemplateControl(i, c, "ChkDelete");
        //    if (chk.Checked)
            {
                //string id = grdSoapNote.GetRowValues(i, "I_ID").ToString();
                arrFile.Add(dgiItem.Cells[0].Text.ToString());
                //arrFile.Add(id);
               

            }
           
        }
        DeleteFile(arrFile);
        bind_grid();
    }
    public int DeleteFile(ArrayList objAL)
    {
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        int num = 0;
        try
        {
            try
            {
                conn.Open();
                for (int i = 0; i < objAL.Count; i++)
                {
                    SqlCommand comm = new SqlCommand("SP_DELETE_SOAP_NOTE", conn);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
                    comm.Parameters.Add("@File_Path", SqlDbType.VarChar, 300);
                    comm.Parameters["@File_Path"].Direction = ParameterDirection.Output;
                    comm.Parameters.AddWithValue("@I_ID", objAL[i].ToString());
                    comm.ExecuteNonQuery();

                    string filePath = comm.Parameters["@File_Path"].Value.ToString();
                    if (filePath.Trim() != string.Empty)
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        else {


                        }


                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            conn.Close();
        }
        return num;
    }
}