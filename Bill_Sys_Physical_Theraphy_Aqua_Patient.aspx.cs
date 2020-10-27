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

public partial class Bill_Sys_Physical_Theraphy_Aqua_Patient : PageBase
{
    System.Drawing.Image Img;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Physical_Theraphy_Aqua_Patient.aspx");
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
        string sz_EventID = Session["IMEventID"].ToString();
        //String sz_EventID = "7";
      //  Session["PhysicalCaseID"] = "11900";
        int flag = 0;
        string DoctorImagePath = "";
        //string PatientImagePath = Session["PatientImagePath"].ToString();
        string CaseBarcodePath = "";

        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["PhysicalCaseID"].ToString() + "/Signs/";
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
                        DoctorImagePath = szDefaultPath + "_PatientSign.jpg";
                        Img.Save(DoctorImagePath);
                        //System.IO.File.WriteAllBytes("C://LawAllies//MBSUpload//docsign.jpg", imageContent2);

                        //string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) + Session["PhysicalCaseID"].ToString();
                        //DataSet dset = new DataSet();
                        //dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);

                        //string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                        //string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
                        //string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
                        //GenerateBarcode(barcodeValue);
                        //String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["PhysicalCaseID"].ToString() + "/Signs/";
                        //CaseBarcodePath = sz_BarcodeImagePath + "BarcodeImg.jpg";
                        //Img.Save(CaseBarcodePath);
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                    }
                }
            }

            _objcheckOut.UpdatePatientPhyscalTherapySign(sz_EventID, DoctorImagePath);
            Response.Redirect("Bill_Sys_Physical_Theraphy_Aqua_Doctor.aspx", false);


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
