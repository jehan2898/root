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

public partial class Bill_Sys_IM_Followup_DoctorSign : PageBase
{
    System.Drawing.Image Img;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["IM_FW_EVENT_ID"].ToString());
        Session["IM_FOLLOWUP_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_IM_Followup_DoctorSign.aspx");
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
        string sz_EventID = Session["IM_FW_EVENT_ID"].ToString();

         
        int flag = 0;
        string DoctorImagePath = "";
        //string PatientImagePath = Session["PatientImagePath"].ToString();
        string CaseBarcodePath = "";

        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";
            //end
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            //Added By-Sunil
            SaveSignPath = SaveSignPath + sz_EventID;
            //end
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
                        DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
                        Img.Save(DoctorImagePath);
                        DataSet dset = new DataSet();
                        dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);

                        string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                        string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
                       
                        String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";
                        SpecialityPDFBO pdfbo = new SpecialityPDFBO();
                        CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);

                         
                    }
                    else
                    {
                        flag = 2;
                    }
                }
            }
            string DoctorImagePathlogical = SaveSignPath + "_DoctorSign.jpg";
            //Added By-Sunil
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_Followup_CaseID"].ToString() + "/Signs/";
            //end
            //Added By-Sunil
            string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
            //end
           // _objcheckOut.SaveIMFollowupSignPath(sz_EventID, DoctorImagePath, CaseBarcodePath);
            _objcheckOut.SaveIMFollowupSignPath(sz_EventID, DoctorImagePathlogical, CaseBarcodePathlogical);
            
            SpecialityPDFFill spf = new SpecialityPDFFill();
            ArrayList obj = new ArrayList();
            spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

            spf.sz_XMLPath = "IM_FOLLOWUP_XML_Path";
            spf.sz_PDFPath = "IM_FOLLOWUP_PDF_Path";

            spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.SZ_SPECIALITY_NAME = "IM";
            obj = spf.FillPDFValue(spf);

           // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
           Response.Redirect(obj[1].ToString(),false);

            spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString(); ;
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
            spf.SZ_PT_FILE_NAME = obj[0].ToString();
            spf.SZ_PT_FILE_PATH = obj[2].ToString();
            spf.SZ_SPECIALITY_CODE = "NFMIM";
            spf.SZ_SPECIALITY_NAME = "IM";
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.savePDFForminDocMang(spf);
            //Code To generate pdf according to check box value and Visit Status 
            Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
            if (ChkBo.ChekVisitStaus(Convert.ToInt32(Session["IM_FW_EVENT_ID"].ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
            {
                ArrayList ObjalGetVal = new ArrayList();
                ArrayList objal1 = new ArrayList();
                SpecialityPDFFill SPDFF = new SpecialityPDFFill();
                ObjalGetVal = ChkBo.ChekCheckBoxStaus(Convert.ToInt32(Session["IM_FW_EVENT_ID"].ToString()));
                //CHK_REFERRALS_CHIROPRACTOR
                if (ObjalGetVal[0].ToString().Equals("True"))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.IMG_ID_COLUMN_CODE = "CH_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }
                //CHK_REFERRALS_PHYSICAL_THERAPIST
                if (ObjalGetVal[1].ToString().Equals("True"))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.IMG_ID_COLUMN_CODE = "PT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_REFERRALS_OCCUPATIONAL_THERAPIST
                if (ObjalGetVal[14].ToString().Equals("True"))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.IMG_ID_COLUMN_CODE = "OT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_TEST_EMG_NCV
                if (ObjalGetVal[15].ToString().Equals("True"))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.sz_XMLPath = "MST_ECG_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "MST_ECG_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.IMG_ID_COLUMN_CODE = "VSNCT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }


                //CHK_SUPPLIES_EMS_UNIT TO CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS
                if (ObjalGetVal[2].ToString().Equals("True") || ObjalGetVal[3].ToString().Equals("True") || ObjalGetVal[4].ToString().Equals("True") || ObjalGetVal[5].ToString().Equals("True") || ObjalGetVal[6].ToString().Equals("True") || ObjalGetVal[7].ToString().Equals("True") || ObjalGetVal[8].ToString().Equals("True") || ObjalGetVal[9].ToString().Equals("True") || ObjalGetVal[10].ToString().Equals("True") || ObjalGetVal[11].ToString().Equals("True") || ObjalGetVal[12].ToString().Equals("True") || ObjalGetVal[13].ToString().Equals("True"))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.sz_XMLPath = "Supplies_XML_Path";
                    SPDFF.sz_PDFPath = "Supplies_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.IMG_ID_COLUMN_CODE = "SUPPLIES_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }


            }
            //End Code



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

    //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName) 'written common funtion
    //{
    //    string sz_CaseID = Session["IM_FOLLOWUP_CaseID"].ToString();
    //    PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
    //    string pdffilepath;
    //    string strGenFileName = "";
    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //        string xmlpath = ConfigurationManager.AppSettings["IM_FOLLOWUP_XML_Path"].ToString();
    //        string pdfpath = ConfigurationManager.AppSettings["IM_FOLLOWUP_PDF_Path"].ToString();

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
    //        Response.Redirect(szOpenFilePath, false);
    //        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
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
