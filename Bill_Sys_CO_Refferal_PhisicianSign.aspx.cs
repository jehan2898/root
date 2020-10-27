using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RealSignature;
using System.Drawing;
using System.Drawing.Drawing2D;
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.IO;

public partial class Bill_Sys_CO_Refferal_PhisicianSign : PageBase
{
    System.Drawing.Image Img;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["CO_Refferal_Event_Id"].ToString());
        Session["CO_Refferal_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CO_Refferal_PhisicianSign.aspx");
        }
        #endregion
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        string sz_EventID = Session["CO_Refferal_Event_Id"].ToString();
        string XML_Path;
        string PDF_Path;
         
        int flag = 0;
        string DoctorImagePath = "";
        //string PatientImagePath = Session["PatientImagePath"].ToString();
        string CaseBarcodePath = "";

        try
        {
                string sz_NodeId;
                string sz_CaseId;
                String sz_BarcodeImagePath;
                string sz_Session_Id;
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["CO_Refferal_CaseID"].ToString() + "/Signs/";
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            if (WebSignature2.ExportToStreamOnly())
            {
                System.IO.MemoryStream imgstream2 = (System.IO.MemoryStream)(WebSignature2.ImageMemoryStream);
                {
                    if (imgstream2 != null)
                    {
                        byte[] imageContent2 = new Byte[imgstream2.Length];
                        imgstream2.Position = 0;
                        imgstream2.Read(imageContent2, 0, (int)imgstream2.Length);
                        Response.ContentType = "image/jpeg";
                        Response.BinaryWrite(imageContent2);
                        Bitmap bmp = new Bitmap(imgstream2);
                        double size = 0.15;
                        ResizeImage((System.Drawing.Image)bmp, size);
                        DoctorImagePath = szDefaultPath + "_PhysicianSign.jpg";
                        Img.Save(DoctorImagePath);
                        DataSet dset = new DataSet();
                        dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);

                          sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                            sz_CaseId = dset.Tables[0].Rows[0][0].ToString();

                      
                          sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["CO_Refferal_CaseID"].ToString() + "/Signs/";

                       

                           //code by tushar to centralise barcode generation process
                          SpecialityPDFBO pdfbo = new SpecialityPDFBO();
                          CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
                        //end of code
                    }
                    else
                    {
                        flag = 2;
                    }
                }
            }

           
            _objcheckOut.SaveCORefferalSignPath(sz_EventID, DoctorImagePath, CaseBarcodePath);
             
            
        // Code by tushar to centralise pdf generation process
            SpecialityPDFFill spf = new SpecialityPDFFill();
            ArrayList obj = new ArrayList();
            spf.sz_EventID = sz_EventID;
            spf.sz_CompanyID = sz_CompanyID;
            spf.sz_CompanyName = sz_CompanyName;
            XML_Path = "CO_Refferal_XML_Path";
            PDF_Path = "CO_Refferal_PDF_Path";
            spf.sz_XMLPath = XML_Path.ToString();
            spf.sz_PDFPath = PDF_Path.ToString();
            sz_Session_Id = Session["CO_Refferal_Event_Id"].ToString();
            spf.sz_Session_Id = sz_Session_Id.ToString();
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.SZ_SPECIALITY_NAME = "CH";
             obj = spf.FillPDFValue(spf);

            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
            Response.Write("<script>window.open('" + obj[1] + "')</script>");
       //end of code 


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

    //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    //{
    //    string sz_CaseID = Session["CO_Refferal_CaseID"].ToString();
    //    PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
    //    string pdffilepath;
    //    string strGenFileName = "";
    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //        string xmlpath = ConfigurationManager.AppSettings["CO_Refferal_XML_Path"].ToString();
    //        string pdfpath = ConfigurationManager.AppSettings["CO_Refferal_PDF_Path"].ToString();

    //        String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
    //        String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/IM/";

    //        strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

    //        if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
    //        {
    //            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
    //            {
    //                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
    //            }
    //            File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
    //        }

    //        Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
    //        ArrayList objAL = new ArrayList();
    //        objAL.Add(sz_CaseID);
    //        objAL.Add(strGenFileName);
    //        objAL.Add(szDestinationDir);
    //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //        objAL.Add(sz_CompanyID);
    //        objAL.Add(EventID);
    //        objCheckoutBO.save_IM_DocMang(objAL);
    //        // Open file
    //        String szOpenFilePath = "";
    //        szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
    //        //Response.Redirect(szOpenFilePath, false);
    //        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
    //    }
    //   catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return strGenFileName;
    //}

    public void ResizeImage(System.Drawing.Image image, double Persize)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Img = null;
            int originalwidth = image.Width;
            int originalheight = image.Height;

            double resizedwidth = (int)(originalwidth * Persize);
            double resizedheight = (int)(originalheight * Persize);

            Bitmap bmp = new Bitmap((int)resizedwidth, (int)resizedheight);
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.DrawImage(image, 0, 0, (int)resizedwidth, (int)resizedheight);
            graphic.Dispose();
            Img = (System.Drawing.Image)bmp;
            //bmp.Save(imagePath + "finalcopy.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

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


    //public void GenerateBarcode(string sz_BarcodeValue)
    //{
    //    try
    //    {
    //        Vintasoft.Barcode.BarcodeWriter genbarcode = new Vintasoft.Barcode.BarcodeWriter();
    //        genbarcode.Settings.Barcode = BarcodeType.Code128;
    //        genbarcode.Settings.Value = sz_BarcodeValue;
    //        genbarcode.Settings.Height = 70;
    //        genbarcode.Settings.MinWidth = 3;
    //        genbarcode.Settings.Padding = 1;

    //        Bitmap bmp = new Bitmap(genbarcode.WriteBarcode());
    //        //picBarcode.Image = (Bitmap)genbarcode.WriteBarcode();
    //        //   picBarcode.Image.Save(imagePath + "testimg.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

    //        //  Bitmap bmp = new Bitmap(imagePath + "testimg.jpg");

    //        double size = 0.40;
    //        ResizeBarcodeImage((System.Drawing.Image)bmp, size);
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //public void ResizeBarcodeImage(System.Drawing.Image image, double Persize)
    //{
    //    try
    //    {
    //        Img = null;
    //        int originalwidth = image.Width;
    //        int modifiedwidth = originalwidth - 350;
    //        int originalheight = image.Height;

    //        double resizedwidth = (int)(modifiedwidth * Persize);
    //        double resizedheight = (int)(originalheight * Persize);

    //        Bitmap bmp = new Bitmap((int)resizedwidth, (int)resizedheight);
    //        Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
    //        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
    //        graphic.DrawImage(image, 0, 0, (int)resizedwidth, (int)resizedheight);
    //        graphic.Dispose();
    //        Img = (System.Drawing.Image)bmp;
    //        //bmp.Save(imagePath + "finalcopy.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

    //    }
    //   catch (Exception ex)
    //    {

    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }

    //}



}
