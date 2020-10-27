using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
/// <summary>
/// Summary description for SaveTicketImages
/// </summary>
public class SaveTicketImages
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlCommand comm1;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public SaveTicketImages()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    private string _FileName;

    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }
    private string _FilePath;

    public string FilePath
    {
        get { return _FilePath; }
        set { _FilePath = value; }
    }
    private string _TicketNumber;

    public string TicketNumber
    {
        get { return _TicketNumber; }
        set { _TicketNumber = value; }
    }
    public void SaveImages(ArrayList arrImages)
              
    {
     
         conn = new SqlConnection(strConn);
        conn.Open();
        SqlTransaction tr;
        tr = conn.BeginTransaction();
        try
        {
            for (int i = 0; i < arrImages.Count; i++)
            {
                SaveTicketImages objImages = new SaveTicketImages();
                objImages = (SaveTicketImages)arrImages[i];
                comm = new SqlCommand("sp_save_ticket_documents", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandTimeout = 0;
                comm.Transaction = tr;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@sz_ticket_number", objImages.TicketNumber);
                comm.Parameters.AddWithValue("@sz_file_name", objImages.FileName);
                comm.Parameters.AddWithValue("@sz_file_path", objImages.FilePath);
                comm.ExecuteNonQuery();              

            }
            tr.Commit();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            tr.Rollback();
        }
          finally
       {
            if(conn.State==ConnectionState.Open)
            {
                conn.Close();
            }
        }

       
   
    
    }

}


