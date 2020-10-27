using System;                   
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SpecialityPDFBO
/// </summary>
public class SpecialityPDFBO 
{
    System.Drawing.Image Img;
	public SpecialityPDFBO()
	{
    }

    #region "To Centralise BarCodeGeneration Function"


    public string GetBarCodePath(string p_szCompanyID, string p_szCaseID, string p_szNodeId, String p_szBarcodeImagePath)
    {
        try
        {
            string barcodeValue = p_szCompanyID + "@" + p_szCaseID + "@" + p_szNodeId;
            GenerateBarcode(barcodeValue);
            string CaseBarcodePath = p_szBarcodeImagePath + "BarcodeImg.jpg";
            Img.Save(CaseBarcodePath);
            return CaseBarcodePath;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GenerateBarcode(string sz_BarcodeValue)
    {
        try
        {
            Vintasoft.Barcode.BarcodeWriter genbarcode = new Vintasoft.Barcode.BarcodeWriter();
            genbarcode.Settings.Barcode = BarcodeType.Code128;
            genbarcode.Settings.Value = sz_BarcodeValue;
            genbarcode.Settings.Height = 70;
            genbarcode.Settings.MinWidth = 3;
            genbarcode.Settings.Padding = 1;
            Bitmap bmp = new Bitmap(genbarcode.WriteBarcode());
            double size = 0.40;
            ResizeBarcodeImage((System.Drawing.Image)bmp, size);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ResizeBarcodeImage(System.Drawing.Image image, double Persize)
    {
        try
        {
            Img = null;
            int originalwidth = image.Width;
            int modifiedwidth = originalwidth - 350;
            int originalheight = image.Height;
            double resizedwidth = (int)(modifiedwidth * Persize);
            double resizedheight = (int)(originalheight * Persize);
            Bitmap bmp = new Bitmap((int)resizedwidth, (int)resizedheight);
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.DrawImage(image, 0, 0, (int)resizedwidth, (int)resizedheight);
            graphic.Dispose();
            Img = (System.Drawing.Image)bmp;
        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    #endregion
}


public class SpecialityPDFFill  
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;

    public SpecialityPDFFill()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
       
    private string _sz_EventID = "";

    public string sz_EventID
    {
        get { return _sz_EventID; }
        set { _sz_EventID = value; }
    }

    private string _sz_CompanyID = "";

    public string sz_CompanyID
    {
        get { return _sz_CompanyID; }
        set { _sz_CompanyID = value; }
    }


    private string _sz_CompanyName = "";

    public string sz_CompanyName
    {
        get { return _sz_CompanyName; }
        set { _sz_CompanyName = value; }
    }
      private string _sz_XMLPath = "";

    public string sz_XMLPath
    {
        get { return _sz_XMLPath; }
        set { _sz_XMLPath = value; }
    }

    private string _sz_PDFPath = "";
     public string sz_PDFPath
    {
        get { return _sz_PDFPath; }
        set { _sz_PDFPath = value; }
    }

    private string _sz_Session_Id = "";
    public string sz_Session_Id
    {
        get { return _sz_Session_Id; }
        set { _sz_Session_Id = value; }
    }
    private string _SZ_USER_NAME = "";
    public string SZ_USER_NAME
    {
        get { return _SZ_USER_NAME; }
        set { _SZ_USER_NAME = value; }
    }

    private string _SZ_PT_FILE_NAME = "";

    public string SZ_PT_FILE_NAME
    {
        get { return _SZ_PT_FILE_NAME; }
        set { _SZ_PT_FILE_NAME = value; }
    }
    private string _SZ_PT_FILE_PATH = "";

    public string SZ_PT_FILE_PATH
    {
        get { return _SZ_PT_FILE_PATH; }
        set { _SZ_PT_FILE_PATH = value; }
    }

    private string _SZ_SPECIALITY_CODE = "";

    public string SZ_SPECIALITY_CODE
    {
        get { return _SZ_SPECIALITY_CODE; }
        set { _SZ_SPECIALITY_CODE = value; }
    }

    private string _SZ_SPECIALITY_NAME = "";

    public string SZ_SPECIALITY_NAME
    {
        get { return _SZ_SPECIALITY_NAME; }
        set { _SZ_SPECIALITY_NAME = value; }
    }

    private string _IMG_ID_COLUMN_CODE = "";

    public string IMG_ID_COLUMN_CODE
    {
        get { return _IMG_ID_COLUMN_CODE; }
        set { _IMG_ID_COLUMN_CODE = value; }
    }





    //public ArrayList FillPDFValue(string p_EventID, string p_sz_CompanyID, string p_sz_CompanyName, string p_sz_XMLPath, string p_sz_PDFPath, string p_sz_Session_id, string p_sz_User_Name , string p_SZ_SPECIALITY_NAME)
    public ArrayList FillPDFValue(SpecialityPDFFill p_objSepcialityPDF)
    {
        ArrayList al = new ArrayList();
        string sz_CaseID = p_objSepcialityPDF.sz_Session_Id;
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        String szOpenFilePath = "";
        String szDestinationDir = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings[p_objSepcialityPDF.sz_XMLPath].ToString();
            string pdfpath = ConfigurationManager.AppSettings[p_objSepcialityPDF.sz_PDFPath].ToString();

            String szDefaultPath = p_objSepcialityPDF.sz_CompanyName  + "/" + sz_CaseID + "/Packet Document/";
            szDestinationDir = p_objSepcialityPDF.sz_CompanyName + "/" + sz_CaseID + "/No Fault File/Medicals/" + p_objSepcialityPDF.SZ_SPECIALITY_NAME + "/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, sz_EventID, p_objSepcialityPDF.sz_CompanyName , sz_CaseID);

            if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            }

            Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
            ArrayList objAL = new ArrayList();
            objAL.Add(sz_CaseID);
            objAL.Add(strGenFileName);
            objAL.Add(szDestinationDir);
            objAL.Add(p_objSepcialityPDF.SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(sz_EventID);


            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;

            al.Add(strGenFileName);
            al.Add(szOpenFilePath);
            al.Add(szDestinationDir);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return al;
    }


    public void savePDFForminDocMang(SpecialityPDFFill p_objSepcialityPDF1)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_SAVE_PDFFORMS_DOC_MANG";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objSepcialityPDF1.sz_Session_Id);
            comm.Parameters.AddWithValue("@SZ_PT_FILE_NAME", p_objSepcialityPDF1.SZ_PT_FILE_NAME);
            comm.Parameters.AddWithValue("@SZ_PT_FILE_PATH", p_objSepcialityPDF1.SZ_PT_FILE_PATH);
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objSepcialityPDF1.SZ_USER_NAME);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objSepcialityPDF1.sz_CompanyID);
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objSepcialityPDF1.sz_EventID);
            comm.Parameters.AddWithValue("@SZ_NODE_CODE", p_objSepcialityPDF1.SZ_SPECIALITY_CODE);
            comm.Parameters.AddWithValue("@SZ_NODE_NAME", p_objSepcialityPDF1.SZ_SPECIALITY_NAME);
            comm.Parameters.AddWithValue("@IMG_ID_COLUMN_CODE", p_objSepcialityPDF1.IMG_ID_COLUMN_CODE);
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally 
        {
            conn.Close();
        }
    }
}


public class SpecialityPDFDAO
{
    private string _szProcedureGroupID = "";

    public string ProcedureGroupID {
        get { return _szProcedureGroupID; }
        set { _szProcedureGroupID = value; }
    }

    
    private string _szProcedureGroup = "";
    public string ProcedureGroup
    {
        get { return _szProcedureGroup; }
        set { _szProcedureGroup = value; }
    }

    private string _szVisitType = "";
    public string VisitType
    {
        get { return _szVisitType; }
        set { _szVisitType = value; }
    }

    private string _szEventID = "";
    public string EventID
    {
        get { return _szEventID; }
        set { _szEventID = value; }
    }

    private string _szCaseID = "";
    public string CaseID
    {
        get { return _szCaseID; }
        set { _szCaseID = value; }
    }
}
