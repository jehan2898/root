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
using System.IO;

public partial class Bill_Sys_CO_Signs : PageBase
{
    System.Drawing.Image Img;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CO_Signs.aspx");
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
        string sz_EventID = Session["EventID"].ToString();
        int flag = 0;
        string DoctorImagePath = "";
        string PatientImagePath = "";

        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() +"/Signs/";
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            if (WebSignature1.ExportToStreamOnly())
            {

                System.IO.MemoryStream imgstream1 = (System.IO.MemoryStream)(WebSignature1.ImageMemoryStream);
                if (imgstream1 != null)
                {
                    byte[] imageContent = new Byte[imgstream1.Length];
                    imgstream1.Position = 0;
                    imgstream1.Read(imageContent, 0, (int)imgstream1.Length);
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(imageContent);
                    Bitmap bmp = new Bitmap(imgstream1);
                    double size = 0.15;
                    ResizeImage((System.Drawing.Image)bmp, size);
                    PatientImagePath = szDefaultPath + "_PatientSign.jpg";
                    Img.Save(PatientImagePath);
                    //System.IO.File.WriteAllBytes("C://LawAllies//MBSUpload//patient.jpg", imageContent);
                    flag = 0;
                }
                else
                {
                    flag = 1;
                }
            }
            Session["PatientImagePath"] = PatientImagePath;
            Response.Redirect("Bill_Sys_CO_DoctorSign.aspx", false);
            //if (WebSignature2.ExportToStreamOnly())
            //{
            //    System.IO.MemoryStream imgstream2 = (System.IO.MemoryStream)(WebSignature2.imageMemoryStream);
            //    {
            //        if (imgstream2 != null)
            //        {
            //            byte[] imageContent2 = new Byte[imgstream2.Length];
            //            imgstream2.Position = 0;
            //            imgstream2.Read(imageContent2, 0, (int)imgstream2.Length);
            //            Response.ContentType = "image/jpeg";
            //            Response.BinaryWrite(imageContent2);
            //            Bitmap bmp = new Bitmap(imgstream2);
            //            double size = 0.15;
            //            ResizeImage((System.Drawing.Image)bmp, size);
            //            DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
            //            Img.Save(DoctorImagePath);
            //            //System.IO.File.WriteAllBytes("C://LawAllies//MBSUpload//docsign.jpg", imageContent2);
            //            flag = 0;
            //        }
            //        else
            //        {
            //            flag = 2;
            //        }
            //    }
            //}

            //_objcheckOut.UpdateSignPath(sz_EventID, PatientImagePath, DoctorImagePath);
            
            //FillPDFValue(sz_EventID, sz_CompanyID, sz_CompanyName);

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

    //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    //{
    //    string sz_CaseID = Session["ChkOutCaseID"].ToString();
    //    PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
    //    string pdffilepath;
    //    string strGenFileName = "";
    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //        string xmlpath = ConfigurationManager.AppSettings["PT_XML_Path"].ToString();
    //        string pdfpath = ConfigurationManager.AppSettings["PT_PDF_Path"].ToString();

    //        String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
    //        String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/PT/";

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
    //        objAL.Add(Session["EventID"].ToString());
    //        objCheckoutBO.save_PT_DocMang(objAL);
    //        // Open file
    //        String szOpenFilePath = "";
    //        szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
    //        Response.Redirect(szOpenFilePath, false);
    //        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
    //    }
    //   catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return strGenFileName;
    //}

    

    //protected void btnGetDoctorSign_Click(object sender, EventArgs e)
    //{
    //    Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
    //    Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //    string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //    string sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
    //    string sz_EventID = Session["EventID"].ToString();
    //    string PatientImagePath = "";

    //    try
    //    {
    //        String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
    //        if (!Directory.Exists(szDefaultPath))
    //        {
    //            Directory.CreateDirectory(szDefaultPath);
    //        }
    //        szDefaultPath = szDefaultPath + sz_EventID;
    //        if (WebSignature1.ExportToStreamOnly())
    //        {

    //            System.IO.MemoryStream imgstream1 = (System.IO.MemoryStream)(WebSignature1.imageMemoryStream);
    //            if (imgstream1 != null)
    //            {
    //                byte[] imageContent = new Byte[imgstream1.Length];
    //                imgstream1.Position = 0;
    //                imgstream1.Read(imageContent, 0, (int)imgstream1.Length);
    //                Response.ContentType = "image/jpeg";
    //                Response.BinaryWrite(imageContent);
    //                Bitmap bmp = new Bitmap(imgstream1);
    //                double size = 0.15;
    //                ResizeImage((System.Drawing.Image)bmp, size);
    //                PatientImagePath = szDefaultPath + "_PatientSign.jpg";
    //                Img.Save(PatientImagePath);
    //                //System.IO.File.WriteAllBytes("C://LawAllies//MBSUpload//patient.jpg", imageContent);
                    
    //            }
    //            else
    //            {
                    
    //            }
    //        }
    //        Session["PatientImagePath"] = PatientImagePath;
    //        pnlDoctorSign.Visible = true;
    //        pnlPatientSign.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

}
