using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using MG2PDF.DataAccessObject;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

/// <summary>
/// Summary description for MBS_CASEWISE_MG2
/// </summary>
public class MBS_CASEWISE_MG2
{
    public MBS_CASEWISE_MG2()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    String strConn;
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    public string SaveMG2(AddMG2Casewise objMG2)
    {
        string i_ID = "0";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "SP_txn_mg2_case_wise_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@sz_company_id", objMG2.sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", objMG2.sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_user_id", objMG2.sz_UserID);
            cmd.Parameters.AddWithValue("@sz_modified_by", objMG2.sz_UserID);

            cmd.Parameters.AddWithValue("@sz_guidelines_reference", objMG2.attendingDoctorNameAddress);//DDLGiidline .SelectedItem .Text
            cmd.Parameters.AddWithValue("@sz_approval_request", objMG2.approvalRequest);
            cmd.Parameters.AddWithValue("@sz_wcb_case_file", objMG2.dateOfService);
            cmd.Parameters.AddWithValue("@sz_applicable", objMG2.datesOfDeniedRequest);

            cmd.Parameters.AddWithValue("@bt_did", objMG2.chkDid);

            cmd.Parameters.AddWithValue("@bt_not_did", objMG2.chkDidNot);
            cmd.Parameters.AddWithValue("@sz_spoke", objMG2.contactDate);
            cmd.Parameters.AddWithValue("@sz_spoke_anyone", objMG2.personContacted);

            cmd.Parameters.AddWithValue("@bt_a_copy", objMG2.chkCopySent);
            cmd.Parameters.AddWithValue("@sz_fund_by", objMG2.faxEmail);

            cmd.Parameters.AddWithValue("@bt_equipped", objMG2.chkCopyNotSent);
            cmd.Parameters.AddWithValue("@sz_indicated", objMG2.indicatedFaxEmail);
            //cmd.Parameters.AddWithValue("@sz_provider_signature",TxtProviderSig .Text);            
            cmd.Parameters.AddWithValue("@dt_provider_signature_date", objMG2.providerSignDate);

            cmd.Parameters.AddWithValue("@bt_self_insurrer", objMG2.chkNoticeGiven);
            cmd.Parameters.AddWithValue("@sz_print_name_D", objMG2.printCarrierEmployerNoticeName);
            cmd.Parameters.AddWithValue("@sz_title_D", objMG2.noticeTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_D",TxtSignatureD .Text);
            cmd.Parameters.AddWithValue("@dt_date_D", objMG2.noticeCarrierSignDate);
            cmd.Parameters.AddWithValue("@sz_section_E", objMG2.carrierDenial);

            cmd.Parameters.AddWithValue("@bt_granted", objMG2.chkGranted);

            cmd.Parameters.AddWithValue("@bt_granted_in_part", objMG2.chkGrantedInParts);

            cmd.Parameters.AddWithValue("@bt_without_prejudice", objMG2.chkWithoutPrejudice);

            cmd.Parameters.AddWithValue("@bt_denied", objMG2.chkDenied);

            cmd.Parameters.AddWithValue("@bt_burden", objMG2.chkBurden);

            cmd.Parameters.AddWithValue("@bt_substantialy", objMG2.chkSubstantiallySimilar);
            cmd.Parameters.AddWithValue("@sz_if_applicable", objMG2.medicalProfessional);

            cmd.Parameters.AddWithValue("@bt_made_E", objMG2.chkMedicalArbitrator);

            cmd.Parameters.AddWithValue("@bt_chair_E", objMG2.chkWCBHearing);
            cmd.Parameters.AddWithValue("@sz_print_name_E", objMG2.printCarrierEmployerResponseName);
            cmd.Parameters.AddWithValue("@sz_title_E", objMG2.responseTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_E",TxtSignatureE .Text );
            cmd.Parameters.AddWithValue("@dt_date_E", objMG2.responseCarrierSignDate);

            cmd.Parameters.AddWithValue("@sz_print_name_F", objMG2.printDenialCarrierName);
            cmd.Parameters.AddWithValue("@sz_title_F", objMG2.denialTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_F", TxtSignatureF.Text);
            cmd.Parameters.AddWithValue("@dt_date_F", objMG2.denialCarrierSignDate);


            cmd.Parameters.AddWithValue("@bt_i_request", objMG2.chkRequestWC);

            cmd.Parameters.AddWithValue("@bt_made_G", objMG2.chkMedicalArbitratorByWC);

            cmd.Parameters.AddWithValue("@bt_chair_G", objMG2.chkWCBHearingByWC);
            //cmd.Parameters.AddWithValue("@sz_claimant_signature",TxtClairmantSignature .Text );
            cmd.Parameters.AddWithValue("@dt_claimant_date", objMG2.claimantSignDate);

            cmd.Parameters.AddWithValue("@sz_wcb_case_no", objMG2.WCBCaseNumber);
            cmd.Parameters.AddWithValue("@sz_carrier_case_no", objMG2.carrierCaseNumber);
            cmd.Parameters.AddWithValue("@sz_date_of_injury", objMG2.dateOfInjury);
            cmd.Parameters.AddWithValue("@sz_patient_firstname", objMG2.firstName);
            cmd.Parameters.AddWithValue("@sz_patient_middlename", objMG2.middleName);
            cmd.Parameters.AddWithValue("@sz_patient_lastname", objMG2.lastName);
            cmd.Parameters.AddWithValue("@sz_patient_address", objMG2.patientAddress);
            cmd.Parameters.AddWithValue("@sz_employee_name_address", objMG2.employerNameAddress);
            cmd.Parameters.AddWithValue("@sz_insurance_name_address", objMG2.insuranceNameAddress);
            cmd.Parameters.AddWithValue("@sz_doctor_name_address", objMG2.sz_DoctorID);//DDLAttendingDoctors.Text
            cmd.Parameters.AddWithValue("@sz_individual_provider", objMG2.providerWCBNumber);
            cmd.Parameters.AddWithValue("@sz_teltphone_no", objMG2.doctorPhone);
            cmd.Parameters.AddWithValue("@sz_fax_no", objMG2.doctorFax);
            cmd.Parameters.AddWithValue("@sz_Guidline_Char", objMG2.bodyInitial);


            cmd.Parameters.AddWithValue("@sz_Guidline", objMG2.guidelineSection);
            cmd.Parameters.AddWithValue("@sz_security_no", objMG2.socialSecurityNumber);
            //cmd.Parameters.AddWithValue("@sz_bill_no", objMG2.sz_BillNo);


            //cmd.Parameters.AddWithValue("@sz_patient_id", objMG2.PatientID);

            cmd.Parameters.AddWithValue("@I_ID", objMG2.I_ID);
            cmd.Parameters.AddWithValue("@sz_procedure_group_id", objMG2.sz_procedure_group_id);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i_ID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return i_ID;
    }

    public DataTable GetMG2Record(String sz_CompanyID, String i_case_id, String i_Id)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_txn_mg2_case_wise_details", conn);
            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", i_case_id);
            cmd.Parameters.AddWithValue("@i_Id", i_Id);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataTable GetMG2Patient(String Sz_CaseID)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_Get_Patient_MG2_case_wise", conn);
            cmd.Parameters.AddWithValue("@sz_case_id", Sz_CaseID);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public string GetPDFPath(String sz_CompanyID, String sz_CaseID, String sz_speciality)
    {
        string result;
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_Find_MG2_Path", conn);
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@sz_CompanyID", sz_CompanyID);
            cmd.Parameters.AddWithValue("@sz_CaseID", sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_speciality", sz_speciality);

            cmd.CommandType = CommandType.StoredProcedure;
            result = Convert.ToString(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return result;
    }

    public DataTable FindNode(String sz_CompanyID, String sz_speciality)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Sp_find_node", conn);
            cmd.Parameters.AddWithValue("@sz_CompanyID", sz_CompanyID);
            cmd.Parameters.AddWithValue("@sz_speciality", sz_speciality);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public void SaveLogicalPath(String sz_BillNo, String BillPath)
    {

        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_Add_MG2_FilePath", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_BillNo);
            cmd.Parameters.AddWithValue("@sz_pdf_url", BillPath);

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }

    public DataTable GetDoctorInfo(String SZ_DOCTOR_ID)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_Get_Doctor_info", conn);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", SZ_DOCTOR_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public string MG2CheckExist(String sz_CompanyID, String sz_CaseID, String sz_BillNo)
    {
        string result;
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("ChechExistMG2", conn);
            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_BillNo", sz_BillNo);

            cmd.CommandType = CommandType.StoredProcedure;
            result = Convert.ToString(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return result;
    }

    public void SaveUploadDocumentMG2(String sz_CaseID, String sz_CompanyID, String sz_FileName, String sz_FilePath, String sz_NodeName, String SZ_USER_NAME, String SZ_USER_ID, String sz_Node_ID)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_WS_UPLOAD_DOCUMENT_MG2", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            cmd.Parameters.AddWithValue("@SZ_FILE_NAME", sz_FileName);
            cmd.Parameters.AddWithValue("@SZ_FILE_PATH", sz_FilePath);
            cmd.Parameters.AddWithValue("@sz_NodeName", sz_NodeName);
            cmd.Parameters.AddWithValue("@SZ_USER_NAME", SZ_USER_NAME);
            cmd.Parameters.AddWithValue("@SZ_USER_ID", SZ_USER_ID);
            cmd.Parameters.AddWithValue("@I_TAG_ID", sz_Node_ID);

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }


    public string generateMG2New(String sz_CompanyID, String sz_CaseID, String i_Id, String CmpName, String LogicalPath)
    {
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        string cmpid = sz_CompanyID;
        string caseid = sz_CaseID;
        string id = i_Id;
        string lgpath = LogicalPath;

        //string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

        //string strGenFileName = billno + "_" + caseid + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");
        string szURLDocumentManager_OCT = objNF3Template.getPhysicalPath();
        string pdfPath = "";

        if (Directory.Exists(szURLDocumentManager_OCT + "/" + CmpName))
        {
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + lgpath;
        }
        else
        {
            Directory.CreateDirectory(szURLDocumentManager_OCT + "/" + CmpName);
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + lgpath;
        }


        ////if (Directory.Exists(szURLDocumentManager_OCT + "/" + CmpName + "/" + caseid + "/Packet Document/"))
        ////{
        ////    pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + caseid + "/Packet Document/" + strGenFileName;
        ////}
        ////else
        ////{
        ////    Directory.CreateDirectory(szURLDocumentManager_OCT + "/" + CmpName + "/" + caseid + "/Packet Document/");
        ////    pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + caseid + "/Packet Document/" + strGenFileName;
        ////}




        //GenerateMG2 objPDF = new GenerateMG2();
        //string filePath = ConfigurationSettings.AppSettings["BASEPATH"];

        string finalPath = GenerateMG2PDFNEW(pdfPath, cmpid, caseid, id);
        //MessageBox.Show("PDF Generated");
        return finalPath;
    }

    public string GenerateMG2PDFNEW(string szFilePath, String sz_CompanyID, String sz_CaseID, String i_Id)
    {
        string FontStyle = "Arial";
        int TopHeadSize = 12;
        int MidHeadtSize = 9;
        int BottomHeadtSize = 10;
        int RightTopHeadSize = 20;
        int NormalFontSize = 7;
        int NormalHeadSize = 8;
        int SubTitle = 6;
        string cmpid = sz_CompanyID;
        string caseid = sz_CaseID;
        string Id = i_Id;

        AddMG2Casewise objDAO = new AddMG2Casewise();
        objDAO = BindDataMG2PDF(cmpid, caseid, i_Id);

        string checkPath = ConfigurationSettings.AppSettings["CHECKBOXPATH"].ToString()+ "checkbox.JPG";
        string unCheckPath = ConfigurationSettings.AppSettings["UNCHECKBOXPATH"].ToString()+ "uncheckbox.JPG";
        string WCLOGO = ConfigurationSettings.AppSettings["WCLOGOPATH"].ToString();
        //string wcbIcon = ConfigurationSettings.AppSettings["WCBIcon"].ToString();
        //string wcbIcon = "";

        szFilePath = szFilePath + ".pdf";
        MemoryStream m = new MemoryStream();
        FileStream fs = new FileStream(szFilePath, System.IO.FileMode.OpenOrCreate);
        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
        PdfWriter writer = PdfWriter.GetInstance(document, fs);
        document.Open();

        iTextSharp.text.Image ImgIcon = iTextSharp.text.Image.GetInstance(WCLOGO);
        //ImgIcon.ScaleAbsolute(10f, 10f);
        PdfPCell cellObjIcon = new PdfPCell(ImgIcon);
        cellObjIcon.Border = Rectangle.NO_BORDER;
        cellObjIcon.HorizontalAlignment = Element.ALIGN_CENTER;
        cellObjIcon.FixedHeight = 10f;

        iTextSharp.text.Image ImgObjChk = iTextSharp.text.Image.GetInstance(checkPath);
        ImgObjChk.ScaleAbsolute(12f, 12f);
        PdfPCell cellObjChk = new PdfPCell(ImgObjChk);
        cellObjChk.Border = Rectangle.NO_BORDER;
        cellObjChk.HorizontalAlignment = Element.ALIGN_CENTER;
        cellObjChk.FixedHeight = 10f;

        iTextSharp.text.Image ImgObjUnchk = iTextSharp.text.Image.GetInstance(unCheckPath);
        ImgObjUnchk.ScaleAbsolute(12f, 12f);
        PdfPCell cellObjUnchk = new PdfPCell(ImgObjUnchk);
        cellObjUnchk.Border = Rectangle.NO_BORDER;
        cellObjUnchk.HorizontalAlignment = Element.ALIGN_CENTER;
        cellObjUnchk.FixedHeight = 10f;

        float[] wMain = { 6f };
        PdfPTable tblMain = new PdfPTable(wMain);
        tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblMain.WidthPercentage = 100;

        float[] wBase = { 4f };
        PdfPTable tblBase = new PdfPTable(wBase);
        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblBase.WidthPercentage = 100;

        float[] wBaseFraction = { 1f, 22f };
        PdfPTable tblBaseFraction = new PdfPTable(wBaseFraction);
        tblBaseFraction.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblBaseFraction.WidthPercentage = 100;

        float[] fTop = { 1f, 4f, 1f };
        PdfPTable Ttop = new PdfPTable(fTop);
        Ttop.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        Ttop.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
        Ttop.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        Ttop.WidthPercentage = 100;

        Ttop.AddCell(ImgIcon);
        // Ttop.AddCell("");

        float[] fTopMiddle = { 4f };
        PdfPTable TtopMiddle = new PdfPTable(fTopMiddle);
        TtopMiddle.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TtopMiddle.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        TtopMiddle.WidthPercentage = 100;

        TtopMiddle.AddCell(new Phrase("ATTENDING DOCTOR'S REQUEST FOR APPROVAL OF VARIANCE AND CARRIER'S RESPONSE", iTextSharp.text.FontFactory.GetFont(FontStyle, TopHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        //TtopMiddle.AddCell(new Phrase("VARIANCE AND CARRIER'S RESPONSE", iTextSharp.text.FontFactory.GetFont(FontStyle, TopHeadSize, iTextSharp.text.Font.BOLD,iTextSharp.text.Color.BLACK)));
        TtopMiddle.AddCell(new Phrase("State of New York - Workers' Compensation Board", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
        TtopMiddle.AddCell(new Phrase("For additional variance requests in this case, attach Form MG-2.1. Answer all questions where information is known.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        //TtopMiddle.AddCell(new Phrase("Answer all questions where information is known.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        Ttop.AddCell(TtopMiddle);
        Ttop.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
        Ttop.AddCell(new Phrase("MG-2", iTextSharp.text.FontFactory.GetFont(FontStyle, RightTopHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        tblBaseFraction.AddCell("");
        tblBaseFraction.AddCell(Ttop);


        float[] fTopBox = { 0.6f, 0.4f, 0.6f, 0.4f, 0.6f, 0.4f };
        PdfPTable TtopBox = new PdfPTable(fTopBox);
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
        TtopBox.DefaultCell.FixedHeight = 14f;
        TtopBox.WidthPercentage = 100;

        TtopBox.AddCell(new Phrase("WCB Case Number:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.AddCell(new Phrase(objDAO.WCBCaseNumber, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.AddCell(new Phrase("Carrier Case Number:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.AddCell(new Phrase(objDAO.carrierCaseNumber, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.AddCell(new Phrase("Date of Injury:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBox.AddCell(new Phrase(objDAO.dateOfInjury, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        tblBaseFraction.AddCell("");
        tblBaseFraction.AddCell(TtopBox);

        tblBaseFraction.AddCell("");
        tblBaseFraction.AddCell("");
        //tblBaseFraction.AddCell("");
        //tblBaseFraction.AddCell("");

        tblBaseFraction.AddCell(new Phrase("A.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fPatientInfo = { 1f, 1f, 1f, 1f, 1.2f, 0.8f };
        PdfPTable TPatientInfo = new PdfPTable(fPatientInfo);
        TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TPatientInfo.DefaultCell.FixedHeight = 14f;
        TPatientInfo.WidthPercentage = 100;

        TPatientInfo.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TPatientInfo.AddCell(new Phrase(objDAO.firstName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.AddCell(new Phrase(objDAO.middleName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.AddCell(new Phrase(objDAO.lastName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TPatientInfo.AddCell(new Phrase("Social Security No.:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TPatientInfo.AddCell(new Phrase(objDAO.socialSecurityNumber, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        //TPatientInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        TPatientInfo.DefaultCell.FixedHeight = 10f;
        TPatientInfo.AddCell("");
        TPatientInfo.AddCell(new Phrase("First", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));
        TPatientInfo.AddCell(new Phrase("MI", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));
        TPatientInfo.AddCell(new Phrase("Last", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));
        TPatientInfo.AddCell("");
        TPatientInfo.AddCell("");
        TPatientInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
        TPatientInfo.DefaultCell.FixedHeight = 14f;

        //TPatientInfo.AddCell("");
        //TPatientInfo.CompleteRow();
        TPatientInfo.AddCell(new Phrase("Patient's Address:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TPatientInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        PdfPCell patientAddress = new PdfPCell(new Phrase(objDAO.patientAddress, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        patientAddress.Colspan = 5;
        patientAddress.FixedHeight = 14f;
        //patientAddress.VerticalAlignment = Element.ALIGN_CENTER;
        patientAddress.Border = Rectangle.BOTTOM_BORDER;
        patientAddress.PaddingBottom = 1f;
        TPatientInfo.AddCell(patientAddress);

        //TPatientInfo.AddCell("");
        //TPatientInfo.CompleteRow();
        PdfPCell employerInfo = new PdfPCell(new Phrase("Employer's Name & Address:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        employerInfo.Colspan = 2;
        employerInfo.Border = Rectangle.NO_BORDER;
        employerInfo.FixedHeight = 14f;
        employerInfo.PaddingBottom = 1f;
        TPatientInfo.AddCell(employerInfo);
        PdfPCell employerAddress = new PdfPCell(new Phrase(objDAO.employerNameAddress, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        employerAddress.Colspan = 4;
        employerAddress.Border = Rectangle.BOTTOM_BORDER;
        employerAddress.PaddingBottom = 1f;

        TPatientInfo.AddCell(employerAddress);

        //TPatientInfo.AddCell("");
        //TPatientInfo.CompleteRow();
        PdfPCell insuranceInfo = new PdfPCell(new Phrase("Insurance Carrier's Name & Address: ", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        insuranceInfo.Colspan = 2;
        insuranceInfo.Border = Rectangle.NO_BORDER;
        insuranceInfo.FixedHeight = 14f;
        insuranceInfo.PaddingBottom = 1f;
        TPatientInfo.AddCell(insuranceInfo);
        PdfPCell insuranceAddress = new PdfPCell(new Phrase(objDAO.insuranceNameAddress, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        insuranceAddress.Colspan = 4;
        insuranceAddress.Border = Rectangle.BOTTOM_BORDER;
        insuranceAddress.FixedHeight = 14f;
        insuranceAddress.PaddingBottom = 1f;
        TPatientInfo.AddCell(insuranceAddress);

        tblBaseFraction.AddCell(TPatientInfo);

        tblBaseFraction.AddCell(new Phrase("B.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecB = { 4f };
        PdfPTable TSecB = new PdfPTable(fSecB);
        TSecB.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecB.WidthPercentage = 100;

        float[] fDoctorInfo = { 2f, 4f };
        PdfPTable TDoctorInfo = new PdfPTable(fDoctorInfo);
        TDoctorInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TDoctorInfo.DefaultCell.FixedHeight = 14f;
        TDoctorInfo.WidthPercentage = 100;

        TDoctorInfo.AddCell(new Phrase("Attending Doctor's Name & Address:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TDoctorInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TDoctorInfo.AddCell(new Phrase(objDAO.attendingDoctorNameAddress, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        TSecB.AddCell(TDoctorInfo);
        tblBaseFraction.AddCell(TSecB);

        float[] fProviderInfo = { 3f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.1f, 0.17f, 0.05f, 0.17f, 1.1f, 0.9f, 0.7f, 1.1f };
        PdfPTable TProviderInfo = new PdfPTable(fProviderInfo);
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.DefaultCell.FixedHeight = 14f;
        TProviderInfo.WidthPercentage = 100;

        string temp1 = objDAO.providerWCBNumber;

        string[] WCB = new string[temp1.Length];
        for (int i = 0; i < temp1.Length; i++)
        {
            WCB[i] = temp1[i].ToString();
        }

        TProviderInfo.AddCell(new Phrase("Individual Provider's WCB Authorization No.:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 0)
        {
            TProviderInfo.AddCell(new Phrase(WCB[0], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 1)
        {
            TProviderInfo.AddCell(new Phrase(WCB[1], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 2)
        { TProviderInfo.AddCell(new Phrase(WCB[2], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 3)
        { TProviderInfo.AddCell(new Phrase(WCB[3], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 4)
        { TProviderInfo.AddCell(new Phrase(WCB[4], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 5)
        { TProviderInfo.AddCell(new Phrase(WCB[5], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 6)
        { TProviderInfo.AddCell(new Phrase(WCB[6], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (temp1.Length > 7)
        { TProviderInfo.AddCell(new Phrase(WCB[7], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        else { TProviderInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK))); }
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("Telephone No.:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TProviderInfo.AddCell(new Phrase(objDAO.doctorPhone, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderInfo.AddCell(new Phrase("Fax No.:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TProviderInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TProviderInfo.AddCell(new Phrase(objDAO.doctorFax, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        //TSecB.AddCell(TProviderInfo);
        //tblBaseFraction.AddCell(TSecB);
        tblBaseFraction.AddCell("");
        tblBaseFraction.AddCell(TProviderInfo);

        tblBaseFraction.AddCell(new Phrase("C.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecC = { 4f };
        PdfPTable TSecC = new PdfPTable(fSecC);
        TSecC.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecC.WidthPercentage = 100;

        TSecC.AddCell(new Phrase("The undersigned requests approval to VARY from the WCB Medical Treatment Guidelines as indicated below:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));

        float[] fGuidlineRefference = { 2.3f, 4.2f };
        PdfPTable TGuidlineRefference = new PdfPTable(fGuidlineRefference);
        TGuidlineRefference.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGuidlineRefference.WidthPercentage = 100;

        float[] fGuidlineRefferenceBox = { 2.8f, 0.3f, 0.2f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f };
        //float[] fGuidlineRefferenceBox = { 2.8f, 1.8f };// 0.3f, 0.2f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f };
        PdfPTable TGuidlineRefferenceBox = new PdfPTable(fGuidlineRefferenceBox);
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //TGuidlineRefferenceBox.DefaultCell.FixedHeight = 14f;
        TGuidlineRefferenceBox.WidthPercentage = 100;

        string temp = objDAO.guidelineSection;
        if (temp == "")
            temp = "NONE";

        string[] reff = new string[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            reff[i] = temp[i].ToString();
        }

        TGuidlineRefferenceBox.AddCell(new Phrase("Guideline Reference:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;

        TGuidlineRefferenceBox.AddCell(new Phrase(objDAO.bodyInitial, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        TGuidlineRefferenceBox.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        if (reff.Length > 0)
        {
            TGuidlineRefferenceBox.AddCell(new Phrase(reff[0], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;

        //  TGuidlineRefferenceBox.AddCell(new Phrase(reff[1], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (reff.Length > 1)
        {
            TGuidlineRefferenceBox.AddCell(new Phrase(reff[1], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }

        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        // TGuidlineRefferenceBox.AddCell(new Phrase(reff[2], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (reff.Length > 2)
        {
            TGuidlineRefferenceBox.AddCell(new Phrase(reff[2], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }

        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        ///TGuidlineRefferenceBox.AddCell(new Phrase(reff[3], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (reff.Length > 3)
        {
            TGuidlineRefferenceBox.AddCell(new Phrase(reff[3], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        //TGuidlineRefferenceBox.AddCell(new Phrase(reff[4], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (reff.Length > 4)
        {
            TGuidlineRefferenceBox.AddCell(new Phrase(reff[4], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        }
        //TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //TGuidlineRefferenceBox.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        //TGuidlineRefferenceBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        //TGuidlineRefferenceBox.AddCell(new Phrase(reff[4], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        //TGuidlineRefferenceBox.AddCell(new Phrase("Guideline Reference:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        //TGuidlineRefferenceBox.AddCell(new Phrase(temp, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        TGuidlineRefference.AddCell(TGuidlineRefferenceBox);

        //float[] fGuidlineInstruction = { 4f };
        //PdfPTable TGuidlineInstruction = new PdfPTable(fGuidlineInstruction);
        //TGuidlineInstruction.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //TGuidlineInstruction.WidthPercentage = 100;

        float[] fGuidlineInstruction1 = { 3.4f, 0.12f, 0.9f, 0.12f, 2.7f, 0.12f, 0.9f, 0.12f, 1f };
        PdfPTable TGuidlineInstruction1 = new PdfPTable(fGuidlineInstruction1);
        TGuidlineInstruction1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGuidlineInstruction1.WidthPercentage = 100;

        TGuidlineInstruction1.AddCell(new Phrase("(In first box, indicate body part: K=", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("K", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("nee, S=", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("S", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("houlder, B= Mid and Low ", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("B", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("ack, N=", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("N", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction1.AddCell(new Phrase("eck,", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));

        TGuidlineRefference.AddCell(TGuidlineInstruction1);


        float[] fGuidlineInstruction2 = { 1.1f, 0.3f, 23f };
        PdfPTable TGuidlineInstruction2 = new PdfPTable(fGuidlineInstruction2);
        TGuidlineInstruction2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGuidlineInstruction2.WidthPercentage = 100;

        TGuidlineInstruction2.AddCell(new Phrase("C=", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction2.AddCell(new Phrase("C", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TGuidlineInstruction2.AddCell(new Phrase("arpal Tunnel. In remaining boxes, indicate corresponding section of WCB Medical Treatment", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));

        TGuidlineRefference.AddCell("");
        TGuidlineRefference.AddCell(TGuidlineInstruction2);

        TGuidlineRefference.AddCell("");
        TGuidlineRefference.AddCell(new Phrase("Guidelines. If the treatment requested is not addressed by the Guidelines, in the remaining boxes use NONE.)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));


        //TGuidlineRefference.AddCell(TGuidlineInstruction);

        TSecC.AddCell(TGuidlineRefference);

        float[] fApprovalReqFor = { 1.35f, 0.25f, 4.4f };
        PdfPTable TApprovalReqFor = new PdfPTable(fApprovalReqFor);
        TApprovalReqFor.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TApprovalReqFor.WidthPercentage = 100;

        TApprovalReqFor.AddCell(new Phrase("Approval Requested for: (", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TApprovalReqFor.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TApprovalReqFor.AddCell(new Phrase("one", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TApprovalReqFor.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TApprovalReqFor.AddCell(new Phrase("request type per form)", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        TSecC.AddCell(TApprovalReqFor);
        //TSecC.AddCell(new Phrase("Approval Requested for: (one request type per form)", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        int line = 1, j = 0, k = 0;
        string[] appreq;
        string appRequest = objDAO.approvalRequest.ToString();
        if (appRequest.Length > 120)
        {
            int div = appRequest.Length / 120;
            int rem = appRequest.Length % 120;
            if (rem > 0)
            {
                appreq = new string[div + 1];
                line = div + 1;
            }
            else
            {
                appreq = new string[div];
                line = div;
            }
            for (; k < (div); k++)
            {
                appreq[k] = appRequest.Substring(j, 120);
                j = j + 120;
            }
            appreq[k] = appRequest.Substring(j);
        }
        else
        {
            appreq = new string[] { appRequest };
        }
        PdfPCell[] approvalRequest = new PdfPCell[line];
        for (int i = 0; i < line && i < 5; i++)
        {
            approvalRequest[i] = new PdfPCell(new Phrase(appreq[i], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            approvalRequest[i].Border = Rectangle.BOTTOM_BORDER;
            approvalRequest[i].FixedHeight = 14f;
            approvalRequest[i].PaddingBottom = 1f;
            TSecC.AddCell(approvalRequest[i]);
        }



        //PdfPCell approvalRequest = new PdfPCell(new Phrase(objDAO.approvalRequest, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        //approvalRequest.Border = Rectangle.BOTTOM_BORDER;
        //approvalRequest.FixedHeight = 14f;
        //approvalRequest.PaddingBottom = 1f;
        //TSecC.AddCell(approvalRequest);

        //TSecC.AddCell(new Phrase("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));


        PdfPCell blankLine = new PdfPCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        blankLine.Border = Rectangle.BOTTOM_BORDER;
        blankLine.FixedHeight = 14f;
        //approvalRequest1.PaddingBottom = 8f;

        for (int remline = 0; remline < 4 - line; remline++)
        {
            TSecC.AddCell(blankLine);
        }
        //TSecC.AddCell(approvalRequest1);
        //TSecC.AddCell(approvalRequest1);

        TSecC.AddCell("");
        //TSecC.AddCell("");

        TSecC.AddCell(new Phrase("STATEMENT OF MEDICAL NECESSITY - See item 4 on instruction page.", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        //TSecC.AddCell("");

        TSecC.AddCell(new Phrase("Your explanation must provide the following information:", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("    - the basis for your opinion that the medical care you propose is appropriate for the claimant and is medically necessary at this time; and", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("    - an explanation why alternatives set forth in the Medical Treatment Guidelines are not appropriate or sufficient.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("If applicable, your explanation must also provide:", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("    - the symptoms, signs, or lack of improvement that compel you to seek the proposed treatment, or", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("    - a description of the functional outcomes that, as of the date of the variance request, have continued to demonstrate objective improvement from that treatment and are reasonably expected to further improve with additional treatment.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("      are reasonably expected to further improve with additional treatment.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("    - the specific duration or frequency of treatment for which a variance is requested.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(new Phrase("You have the option to submit citations or copies of relevant literature published in recognized, peer-reviewed medical journals as part of the basis in support of this variance request.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));

        float[] fDateOfService = { 3.2f, 2.5f };
        PdfPTable TDateOfService = new PdfPTable(fDateOfService);
        TDateOfService.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TDateOfService.WidthPercentage = 100;

        TDateOfService.AddCell(new Phrase("Date of service of supporting medical in WCB case file, if not attached:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.dateOfService == "")
        {
            TDateOfService.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TDateOfService.AddCell(new Phrase(objDAO.dateOfService, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }

        TSecC.AddCell(TDateOfService);

        float[] fDateOfDeniedRequest = { 4.5f, 1.5f };
        PdfPTable TDateOfDeniedRequest = new PdfPTable(fDateOfDeniedRequest);
        TDateOfDeniedRequest.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TDateOfDeniedRequest.DefaultCell.FixedHeight = 14f;
        TDateOfDeniedRequest.WidthPercentage = 100;

        string denieddates = objDAO.datesOfDeniedRequest.ToString();
        string denieddates1 = "";
        if (denieddates.Length > 27)
        {
            denieddates1 = denieddates.Substring(0, 27);
        }
        else
        {
            denieddates1 = denieddates;//.Substring(0)
        }
        TDateOfDeniedRequest.AddCell(new Phrase("Date(s) of previously denied variance request for substantially similar treatment, if applicable:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TDateOfDeniedRequest.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TDateOfDeniedRequest.AddCell(new Phrase(denieddates1, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(TDateOfDeniedRequest);

        line = 1;
        j = 27;
        k = 0;
        int firstRemovedchar = 27;
        int remChar = denieddates.Length - firstRemovedchar;
        string[] deniedDates;
        //string appRequest = objDAO.approvalRequest.ToString();
        if (remChar > 120)
        {
            int div = remChar / 120;
            int rem = remChar % 120;
            if (rem > 0)
            {
                deniedDates = new string[div + 1];
                line = div + 1;
            }
            else
            {
                deniedDates = new string[div];
                line = div;
            }
            for (; k < (div); k++)
            {
                deniedDates[k] = denieddates.Substring(j, 120);
                j = j + 120;
            }
            deniedDates[k] = denieddates.Substring(j);
        }
        else if (remChar > 0 && remChar < 120)
        {
            deniedDates = new string[] { denieddates.Substring(27, (denieddates.Length - 27)) };
        }
        else
        {
            deniedDates = new string[] { "" };
        }
        PdfPCell[] deniedVarianceRequest = new PdfPCell[line];
        for (int i = 0; i < line && i < 5; i++)
        {
            deniedVarianceRequest[i] = new PdfPCell(new Phrase(deniedDates[i], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            deniedVarianceRequest[i].Border = Rectangle.BOTTOM_BORDER;
            deniedVarianceRequest[i].FixedHeight = 14f;
            deniedVarianceRequest[i].PaddingBottom = 1f;
            TSecC.AddCell(deniedVarianceRequest[i]);
        }

        for (int remline = 0; remline < (4 - line); remline++)
        {
            TSecC.AddCell(blankLine);
        }






        //TSecC.AddCell(approvalRequest1);
        //TSecC.AddCell(approvalRequest1);
        //TSecC.AddCell(approvalRequest1);
        //TSecC.AddCell(approvalRequest1);
        //PdfPCell deniedRequest = new PdfPCell(new Phrase(objDAO., iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        //deniedRequest.Border = Rectangle.BOTTOM_BORDER;
        //deniedRequest.PaddingBottom = 1f;
        //TSecC.AddCell(deniedRequest);

        float[] fAfterDateOfDenied = { 4f };
        PdfPTable TAfterDateOfDenied = new PdfPTable(fAfterDateOfDenied);
        TAfterDateOfDenied.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TAfterDateOfDenied.WidthPercentage = 100;

        TAfterDateOfDenied.AddCell(new Phrase("I certify that I am making the above request for approval of a variance and my affirmative statements are true and correct. I certify that I have", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TAfterDateOfDenied.AddCell(new Phrase("read and applied the Medical Treatment Guidelines to the treatment and care in this case and that I am requesting this variance before", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TAfterDateOfDenied.AddCell(new Phrase("rendering any medical care that varies from the Guidelines. I certify that the claimant understands and agrees to undergo the proposed medical", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));

        TSecC.AddCell(TAfterDateOfDenied);

        float[] fCarrierContacted1 = { 0.9f, 0.3f, 0.7f, 0.3f, 14.5f };
        PdfPTable TCarrierContacted1 = new PdfPTable(fCarrierContacted1);
        TCarrierContacted1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCarrierContacted1.WidthPercentage = 100;

        TCarrierContacted1.AddCell(new Phrase("care. I", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkDid == "1")
        {
            TCarrierContacted1.AddCell(cellObjChk);
        }
        else
        {
            TCarrierContacted1.AddCell(cellObjUnchk);
        }
        TCarrierContacted1.AddCell(new Phrase(" did /", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkDidNot == "1")
        {
            TCarrierContacted1.AddCell(cellObjChk);
        }
        else
        {
            TCarrierContacted1.AddCell(cellObjUnchk);
        }
        TCarrierContacted1.AddCell(new Phrase(" did not contact the carrier by telephone to discuss this variance request before making the request. I contacted the carrier by", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecC.AddCell(TCarrierContacted1);

        float[] fCarrierContacted2 = { 0.6f, 0.4f, 1.8f, 1f };
        PdfPTable TCarrierContacted2 = new PdfPTable(fCarrierContacted2);
        TCarrierContacted2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCarrierContacted2.DefaultCell.FixedHeight = 14f;
        TCarrierContacted2.WidthPercentage = 100;

        TCarrierContacted2.AddCell(new Phrase("telephone on (date)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.contactDate == "")
        {
            TCarrierContacted2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            TCarrierContacted2.AddCell("");
        }
        else
        {
            TCarrierContacted2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            TCarrierContacted2.AddCell(new Phrase(objDAO.contactDate, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TCarrierContacted2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCarrierContacted2.AddCell(new Phrase("and spoke to (person spoke to or was not able to speak to anyone)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TCarrierContacted2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TCarrierContacted2.AddCell(new Phrase(objDAO.personContacted, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));

        TSecC.AddCell(TCarrierContacted2);

        //float[] fCarrierContacted3 = { 1.2f,8f };
        //PdfPTable TCarrierContacted3 = new PdfPTable(fCarrierContacted3);
        //TCarrierContacted3.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //TCarrierContacted3.WidthPercentage = 100;

        //TCarrierContacted3.AddCell(new Phrase("speak to anyone)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        //TCarrierContacted3.AddCell(new Phrase(objDAO.personContacted, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize,iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        //TSecC.AddCell(TCarrierContacted3);

        //TSecC.AddCell("");
        float[] fCopySentToCarrier = { 4f };
        PdfPTable TCopySentToCarrier = new PdfPTable(fCopySentToCarrier);
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //TCopySentToCarrier.DefaultCell.FixedHeight = 14f;
        TCopySentToCarrier.WidthPercentage = 100;

        float[] fCopySentToCarrier1 = { 0.1f, 4.5f };
        PdfPTable TCopySentToCarrier1 = new PdfPTable(fCopySentToCarrier1);
        TCopySentToCarrier1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier1.WidthPercentage = 100;

        if (objDAO.chkCopySent == "1")
        {
            TCopySentToCarrier1.AddCell(cellObjChk);
        }
        else
        {
            TCopySentToCarrier1.AddCell(cellObjUnchk);
        }
        TCopySentToCarrier1.AddCell(new Phrase("A copy of this form was sent to the carrier/employer/self-insured employer/Special Fund by (fax number or email address required)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        // TCopySentToCarrier1.AddCell(new Phrase(objDAO.faxEmail, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));

        TCopySentToCarrier.AddCell(TCopySentToCarrier1);
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TCopySentToCarrier.AddCell(new Phrase(objDAO.faxEmail, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell(new Phrase("A copy was sent to the Workers' Compensation Board, and copies were provided to the claimant’s legal counsel, if any, to the claimant if not", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell(new Phrase("represented, and to any other parties of interest within two (2) business days of the date below.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell("");

        float[] fCopySentToCarrier2 = { 0.1f, 4.5f };
        PdfPTable TCopySentToCarrier2 = new PdfPTable(fCopySentToCarrier2);
        TCopySentToCarrier2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier2.WidthPercentage = 100;

        if (objDAO.chkCopyNotSent == "1")
        {
            TCopySentToCarrier2.AddCell(cellObjChk);
        }
        else
        {
            TCopySentToCarrier2.AddCell(cellObjUnchk);
        }
        TCopySentToCarrier2.AddCell(new Phrase("I am not equipped to send or receive forms by fax or email. This form was mailed to the parties indicated above on", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        //TCopySentToCarrier2.AddCell(new Phrase(objDAO.indicatedFaxEmail, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));

        TCopySentToCarrier.AddCell(TCopySentToCarrier2);
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
        TCopySentToCarrier.AddCell(new Phrase(objDAO.indicatedFaxEmail, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell("");
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell(new Phrase("In addition, I certify that I do not have a substantially similar request pending and that this request contains additional supporting", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell(new Phrase("medical evidence if it is substantially similar to a prior denied request.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TCopySentToCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCopySentToCarrier.AddCell("");
        //TCopySentToCarrier.AddCell("");



        float[] fProviderSign = { 1.5f, 4f, 0.5f, 1.5f };
        PdfPTable TProviderSign = new PdfPTable(fProviderSign);
        TProviderSign.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TProviderSign.DefaultCell.FixedHeight = 14f;
        TProviderSign.WidthPercentage = 100;

        TProviderSign.AddCell(new Phrase("Provider's Signature:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.providerSign == "")
        {
            TProviderSign.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TProviderSign.AddCell(new Phrase(objDAO.providerSign, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TProviderSign.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.providerSignDate == "")
        {
            TProviderSign.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TProviderSign.AddCell(new Phrase(objDAO.providerSignDate, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TCopySentToCarrier.AddCell(TProviderSign);

        TSecC.AddCell(TCopySentToCarrier);

        tblBaseFraction.AddCell(TSecC);
        tblBase.AddCell(tblBaseFraction);


        float[] fFooterPage1 = { 1.3f, 4.2f, 0.7f };
        PdfPTable TFooterPage1 = new PdfPTable(fFooterPage1);
        TFooterPage1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TFooterPage1.WidthPercentage = 100;

        TFooterPage1.AddCell(new Phrase("MG-2.0 (2-13) Page 1 of 2", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TFooterPage1.AddCell(new Phrase("THE WORKERS' COMPENSATION BOARD EMPLOYS AND SERVES PEOPLE WITH DISABILITIES WITHOUT DISCRIMINATION.", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TFooterPage1.AddCell(new Phrase("www.wcb.ny.gov", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));



        tblBase.AddCell("");
        tblBase.AddCell("");


        tblBase.AddCell(TFooterPage1);

        float[] wBaseFraction1 = { 1f, 22f };
        PdfPTable tblBaseFraction1 = new PdfPTable(wBaseFraction1);
        tblBaseFraction1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblBaseFraction1.WidthPercentage = 100;

        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();


        float[] fTopBoxPage2 = { 0.4f, 0.6f, 0.5f, 0.5f, 0.4f, 0.6f };
        PdfPTable TtopBoxPage2 = new PdfPTable(fTopBoxPage2);
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.DefaultCell.FixedHeight = 14f;
        //TtopBox.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        TtopBoxPage2.WidthPercentage = 100;

        TtopBoxPage2.AddCell(new Phrase("Patient Name:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.AddCell(new Phrase(objDAO.patientName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.AddCell(new Phrase("WCB Case Number:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.AddCell(new Phrase(objDAO.WCBCaseNumber, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.AddCell(new Phrase("Date of Injury:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TtopBoxPage2.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
        TtopBoxPage2.AddCell(new Phrase(objDAO.dateOfInjury, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        tblBaseFraction1.AddCell("");
        tblBaseFraction1.AddCell(TtopBoxPage2);

        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.CompleteRow();


        tblBaseFraction1.AddCell(new Phrase("D.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecD = { 4f };
        PdfPTable TSecD = new PdfPTable(fSecD);
        TSecD.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecD.DefaultCell.BorderWidth = 1;
        TSecD.WidthPercentage = 100;

        TSecD.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        TSecD.AddCell(new Phrase("CARRIER'S / EMPLOYER'S NOTICE OF INDEPENDENT MEDICAL EXAMINATION (IME) OR MEDICAL RECORDS REVIEW", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TSecD.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        float[] fCarrierNotice = { 0.1f, 4.5f };
        PdfPTable TCarrierNotice = new PdfPTable(fCarrierNotice);
        TCarrierNotice.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TCarrierNotice.WidthPercentage = 100;

        if (objDAO.chkNoticeGiven == "1")
        {
            TCarrierNotice.AddCell(cellObjChk);
        }
        else
        {
            TCarrierNotice.AddCell(cellObjUnchk);
        }
        TCarrierNotice.AddCell(new Phrase("The self-insurer/carrier hereby gives notice that it will have the claimant examined by an Independent Medical Examiner or the claimant's medical records reviewed by a Records Reviewer and submit Form IME-4 within 30 calendar days of the variance request.", iTextSharp.text.FontFactory.GetFont(FontStyle, 9, iTextSharp.text.Color.BLACK)));
        //TCarrierNotice.AddCell("");
        //TCarrierNotice.AddCell(new Phrase("medical records reviewed by a Records Reviewer and submit Form IME-4 within 30 calendar days of the variance request.", iTextSharp.text.FontFactory.GetFont(FontStyle, 9, iTextSharp.text.Color.BLACK)));
        TSecD.AddCell(TCarrierNotice);
        TSecD.AddCell("");

        float[] fDsignPart = { 0.6f, 2f, 0.3f, 1.5f };
        PdfPTable TDsignPart = new PdfPTable(fDsignPart);
        TDsignPart.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TDsignPart.DefaultCell.FixedHeight = 14f;
        TDsignPart.WidthPercentage = 100;

        TDsignPart.AddCell(new Phrase("By: (print name)", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.printCarrierEmployerNoticeName == "")
        {
            TDsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TDsignPart.AddCell(new Phrase(objDAO.printCarrierEmployerNoticeName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TDsignPart.AddCell(new Phrase("Title:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.noticeTitle == "")
        {
            TDsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TDsignPart.AddCell(new Phrase(objDAO.noticeTitle, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TDsignPart.AddCell("");
        TDsignPart.CompleteRow();
        //TDsignPart.AddCell("");
        //TDsignPart.CompleteRow();

        TDsignPart.AddCell(new Phrase("Signature:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.noticeCarrierSign == "")
        {
            TDsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TDsignPart.AddCell(new Phrase(objDAO.noticeCarrierSign, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TDsignPart.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.noticeCarrierSignDate == "")
        { TDsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))); }
        else
        {
            TDsignPart.AddCell(new Phrase(objDAO.noticeCarrierSignDate, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TSecD.AddCell(TDsignPart);
        TSecD.AddCell("");


        tblBaseFraction1.AddCell(TSecD);
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.AddCell("");
        tblBaseFraction1.AddCell(new Phrase("E.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecE = { 4f };
        PdfPTable TSecE = new PdfPTable(fSecE);
        TSecE.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecE.DefaultCell.BorderWidth = 1;
        TSecE.WidthPercentage = 100;

        TSecE.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        TSecE.AddCell(new Phrase("CARRIER'S / EMPLOYER'S RESPONSE TO VARIANCE REQUEST", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TSecE.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        float[] fEServiseExplaination = { 2.5f, 1.5f };
        PdfPTable TEServiseExplaination = new PdfPTable(fEServiseExplaination);
        TEServiseExplaination.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplaination.WidthPercentage = 100;

        float[] fEServiseExplainationSpace = { 4f };
        PdfPTable TEServiseExplainationSpace = new PdfPTable(fEServiseExplainationSpace);
        TEServiseExplainationSpace.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplainationSpace.WidthPercentage = 100;

        TEServiseExplainationSpace.AddCell(new Phrase("Carrier's response to the variance request is indicated in the checkboxes on the right. Carrier denial, when appropriate, should be reviewed by a health professional. (Attach written report of medical professional.) If request is approved or denied, sign and date the form in Section E.", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TEServiseExplainationSpace.AddCell("");
        TEServiseExplainationSpace.AddCell("");

        PdfPCell carrierResponce = new PdfPCell(new Phrase(objDAO.carrierDenial, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        carrierResponce.Border = Rectangle.BOTTOM_BORDER;
        carrierResponce.FixedHeight = 14f;
        //carrierResponce.PaddingBottom = 8f;

        TEServiseExplainationSpace.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

        line = 1;
        j = 0;
        k = 0;
        string[] carrierResp;
        string carrierResponse = objDAO.carrierDenial.ToString();
        if (carrierResponse.Length > 75)
        {
            int div = carrierResponse.Length / 75;
            int rem = carrierResponse.Length % 75;
            if (rem > 0)
            {
                carrierResp = new string[div + 1];
                line = div + 1;
            }
            else
            {
                carrierResp = new string[div];
                line = div;
            }
            for (; k < (div); k++)
            {
                carrierResp[k] = carrierResponse.Substring(j, 120);
                j = j + 75;
            }
            carrierResp[k] = carrierResponse.Substring(j);
        }
        else
        {
            carrierResp = new string[] { carrierResponse };
        }
        PdfPCell[] carResponse = new PdfPCell[line];
        for (int i = 0; i < line && i < 4; i++)
        {
            carResponse[i] = new PdfPCell(new Phrase(carrierResp[i], iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            carResponse[i].Border = Rectangle.BOTTOM_BORDER;
            carResponse[i].FixedHeight = 14f;
            carResponse[i].PaddingBottom = 1f;
            TEServiseExplainationSpace.AddCell(carResponse[i]);
        }

        for (int remline = 0; remline < 5 - line; remline++)
        {
            TEServiseExplainationSpace.AddCell(blankLine);
        }

        float[] fEServiseExplainationChk = { 4f };
        PdfPTable TEServiseExplainationChk = new PdfPTable(fEServiseExplainationChk);
        TEServiseExplainationChk.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        TEServiseExplainationChk.WidthPercentage = 100;

        float[] fEServiseExplainationChkUpper = { 4f };
        PdfPTable TEServiseExplainationChkUpper = new PdfPTable(fEServiseExplainationChkUpper);
        TEServiseExplainationChkUpper.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplainationChkUpper.WidthPercentage = 100;

        float[] fEServiseExplainationChkUpperHeader = { 4f };
        PdfPTable TEServiseExplainationChkUpperHeader = new PdfPTable(fEServiseExplainationChkUpperHeader);
        TEServiseExplainationChkUpperHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplainationChkUpperHeader.WidthPercentage = 100;

        TEServiseExplainationChkUpperHeader.AddCell(new Phrase("CARRIER'S / EMPLOYER'S RESPONSE", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TEServiseExplainationChkUpperHeader.AddCell(new Phrase("If service is denied or granted in part, explain in space provided.", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));

        float[] fEServiseExplainationChkUpperChk = { 1f, 7f, 1f, 7f };
        PdfPTable TEServiseExplainationChkUpperChk = new PdfPTable(fEServiseExplainationChkUpperChk);
        TEServiseExplainationChkUpperChk.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplainationChkUpperChk.WidthPercentage = 100;

        if (objDAO.chkGranted == "1")
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkUpperChk.AddCell(new Phrase("Granted", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkWithoutPrejudice == "1")
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkUpperChk.AddCell(new Phrase("Without Prejudice", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkGrantedInParts == "1")
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkUpperChk.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkUpperChk.AddCell(new Phrase("Granted in Part", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TEServiseExplainationChkUpperChk.CompleteRow();

        TEServiseExplainationChkUpper.AddCell(TEServiseExplainationChkUpperHeader);
        TEServiseExplainationChkUpper.AddCell(TEServiseExplainationChkUpperChk);

        float[] fEServiseExplainationChkLower = { 0.1f, 1.5f };
        PdfPTable TEServiseExplainationChkLower = new PdfPTable(fEServiseExplainationChkLower);
        TEServiseExplainationChkLower.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEServiseExplainationChkLower.WidthPercentage = 100;

        if (objDAO.chkDenied == "1")
        {
            TEServiseExplainationChkLower.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkLower.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkLower.AddCell(new Phrase("Denied", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkBurden == "1")
        {
            TEServiseExplainationChkLower.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkLower.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkLower.AddCell(new Phrase("Burden of Proof Not Met", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkSubstantiallySimilar == "1")
        {
            TEServiseExplainationChkLower.AddCell(cellObjChk);
        }
        else
        {
            TEServiseExplainationChkLower.AddCell(cellObjUnchk);
        }
        TEServiseExplainationChkLower.AddCell(new Phrase("Substantially Similar Request Pending or Denied", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

        TEServiseExplainationChk.AddCell(TEServiseExplainationChkUpper);
        TEServiseExplainationChk.AddCell(TEServiseExplainationChkLower);


        TEServiseExplaination.AddCell(TEServiseExplainationSpace);
        TEServiseExplaination.AddCell(TEServiseExplainationChk);
        TSecE.AddCell(TEServiseExplaination);

        float[] fMedicalProfessional = { 1.4f, 1f };
        PdfPTable TMedicalProfessional = new PdfPTable(fMedicalProfessional);
        TMedicalProfessional.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TMedicalProfessional.DefaultCell.FixedHeight = 14f;
        TMedicalProfessional.WidthPercentage = 100;

        TMedicalProfessional.AddCell(new Phrase("Name of the Medical Professional who reviewed the denial, if applicable:", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TMedicalProfessional.AddCell(new Phrase(objDAO.medicalProfessional, iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(TMedicalProfessional);

        TSecE.AddCell(new Phrase(" I certify that copies of this form were sent to the Treating Medical Provider requesting the variance, the Workers' Compensation Board, the", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(new Phrase(" claimant's legal counsel, if any, and any other parties of interest, with the written report of the medical professional in the office of the carrier/", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(new Phrase(" employer/self-insured employer/Special Fund attached, within two (2) business days of the date below.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));

        float[] fReqDenied = { 1.05f, 1.85f, 0.07f, 0.5f };
        PdfPTable TReqDenied = new PdfPTable(fReqDenied);
        TReqDenied.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TReqDenied.WidthPercentage = 100;

        TReqDenied.AddCell(new Phrase("(Please complete if request is denied.)", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TReqDenied.AddCell(new Phrase("If the issue cannot be resolved informally, I opt for the decision to be made", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkMedicalArbitrator == "1")
        {
            TReqDenied.AddCell(cellObjChk);
        }
        else
        {
            TReqDenied.AddCell(cellObjUnchk);
        }
        TReqDenied.AddCell(new Phrase("by the Medical", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(TReqDenied);


        float[] fReqDenied1 = { 1.6f, 0.1f, 4.4f };
        PdfPTable TReqDenied1 = new PdfPTable(fReqDenied1);
        TReqDenied1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TReqDenied1.WidthPercentage = 100;

        TReqDenied1.AddCell(new Phrase("Arbitrator designated by the Chair or", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkWCBHearing == "1")
        {
            TReqDenied1.AddCell(cellObjChk);
        }
        else
        {
            TReqDenied1.AddCell(cellObjUnchk);
        }
        TReqDenied1.AddCell(new Phrase("at a WCB Hearing. I understand that if either party, the carrier or the claimant, opts in writing for", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(TReqDenied1);
        //resolution at a WCB

        TSecE.AddCell(new Phrase(" resolution at a WCB hearing; the decision will be made at a WCB hearing. I understand that if neither party opts for resolution at a hearing,", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell(new Phrase(" the variance issue will be decided by a medical arbitrator and the resolution is binding and not appealable under WCL § 23.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        //TSecE.AddCell(new Phrase("at a WCB Hearing. I understand that if either party, the carrier or the claimant, opts in writing for", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TSecE.AddCell("");

        float[] fEsignPart = { 0.6f, 2f, 0.3f, 1.5f };
        PdfPTable TEsignPart = new PdfPTable(fEsignPart);
        TEsignPart.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TEsignPart.DefaultCell.FixedHeight = 14f;
        TEsignPart.WidthPercentage = 100;

        TEsignPart.AddCell(new Phrase("By: (print name)", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.printCarrierEmployerResponseName == "")
        {
            TEsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TEsignPart.AddCell(new Phrase(objDAO.printCarrierEmployerResponseName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TEsignPart.AddCell(new Phrase("Title:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.responseTitle == "")
        {
            TEsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TEsignPart.AddCell(new Phrase(objDAO.responseTitle, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TEsignPart.AddCell("");
        TEsignPart.CompleteRow();
        //TEsignPart.AddCell("");
        //TEsignPart.CompleteRow();

        TEsignPart.AddCell(new Phrase("Signature:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.responseCarrierSign == "")
        {
            TEsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TEsignPart.AddCell(new Phrase(objDAO.responseCarrierSign, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TEsignPart.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.responseCarrierSignDate == "")
        { TEsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))); }
        else
        {
            TEsignPart.AddCell(new Phrase(objDAO.responseCarrierSignDate, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TSecE.AddCell(TEsignPart);
        TSecE.AddCell("");

        tblBaseFraction1.AddCell(TSecE);

        tblBaseFraction1.AddCell(new Phrase("F.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecF = { 4f };
        PdfPTable TSecF = new PdfPTable(fSecF);
        TSecF.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecF.DefaultCell.BorderWidth = 1;
        TSecF.WidthPercentage = 100;

        TSecF.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        TSecF.AddCell(new Phrase("DENIAL INFORMALLY DISCUSSED AND RESOLVED BETWEEN PROVIDER AND CARRIER", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TSecF.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecF.AddCell(new Phrase(" I certify that the provider's variance request initially denied above is now granted or partially granted.", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        TSecF.AddCell("");

        float[] fFsignPart = { 0.6f, 2f, 0.3f, 1.5f };
        PdfPTable TFsignPart = new PdfPTable(fFsignPart);
        TFsignPart.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TFsignPart.DefaultCell.FixedHeight = 14f;
        TFsignPart.WidthPercentage = 100;

        TFsignPart.AddCell(new Phrase("By: (print name)", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.printDenialCarrierName == "")
        {
            TFsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TFsignPart.AddCell(new Phrase(objDAO.printDenialCarrierName, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TFsignPart.AddCell(new Phrase("Title:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.denialTitle == "")
        {
            TFsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TFsignPart.AddCell(new Phrase(objDAO.denialTitle, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TFsignPart.AddCell("");
        TFsignPart.CompleteRow();
        //TFsignPart.AddCell("");
        //TFsignPart.CompleteRow();

        TFsignPart.AddCell(new Phrase("Signature:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.denialCarrierSign == "")
        {
            TFsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TFsignPart.AddCell(new Phrase(objDAO.denialCarrierSign, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TFsignPart.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.denialCarrierSignDate == "")
        { TFsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))); }
        else
        {
            TFsignPart.AddCell(new Phrase(objDAO.denialCarrierSignDate, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TSecF.AddCell(TFsignPart);

        tblBaseFraction1.AddCell(TSecF);
        tblBaseFraction1.AddCell(new Phrase("G.", iTextSharp.text.FontFactory.GetFont(FontStyle, BottomHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fSecG = { 4f };
        PdfPTable TSecG = new PdfPTable(fSecG);
        TSecG.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecG.DefaultCell.BorderWidth = 1;
        TSecG.WidthPercentage = 100;

        TSecG.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        TSecG.AddCell(new Phrase("CLAIMANT'S / CLAIMANT REPRESENTATIVE'S REQUEST FOR REVIEW OF SELF-INSURED EMPLOYER'S / CARRIER'S DENIAL", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TSecG.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        float[] fSecGNote = { 1f, 1f };
        PdfPTable TSecGNote = new PdfPTable(fSecGNote);
        TSecGNote.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TSecGNote.WidthPercentage = 100;

        TSecGNote.AddCell(new Phrase("NOTE to Claimant's / Claimant Licensed Representative's:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TSecGNote.AddCell(new Phrase("The claimant should only sign this section after the request is", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        //TSecGNote.AddCell(new Phrase("This section should not be completed at the time of initial request.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        //TSecGNote.CompleteRow();
        TSecG.AddCell(TSecGNote);

        TSecG.AddCell(new Phrase(" fully or partially denied. This section should not be completed at the time of initial request.", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        //TSecG.CompleteRow();
        TSecG.AddCell(new Phrase(" YOU MUST COMPLETE THIS SECTION IF YOU WANT THE BOARD TO REVIEW THE CARRIER'S DENIAL OF THE PROVIDER'S   VARIANCE REQUEST.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

        float[] fVarianceRequest = { 0.1f, 4.5f };
        PdfPTable TVarianceRequest = new PdfPTable(fVarianceRequest);
        TVarianceRequest.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TVarianceRequest.WidthPercentage = 100;


        if (objDAO.chkRequestWC == "1")
        {
            TVarianceRequest.AddCell(cellObjChk);
        }
        else
        {
            TVarianceRequest.AddCell(cellObjUnchk);
        }
        TVarianceRequest.AddCell(new Phrase(" I request that the Workers' Compensation Board review the carrier's denial of my doctor's request for approval to vary from the Medical", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));


        float[] fVarianceRequest2 = { 1.8f, 0.1f, 1.7f, 0.1f, 0.8f };
        PdfPTable TVarianceRequest2 = new PdfPTable(fVarianceRequest2);
        TVarianceRequest2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TVarianceRequest2.WidthPercentage = 100;

        TVarianceRequest2.AddCell(new Phrase("Treatment Guidelines. I opt for the decision to be made", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkMedicalArbitratorByWC == "1")
        {
            TVarianceRequest2.AddCell(cellObjChk);
        }
        else
        {
            TVarianceRequest2.AddCell(cellObjUnchk);
        }
        TVarianceRequest2.AddCell(new Phrase("by the Medical Arbitrator designated by the Chair or", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.chkWCBHearingByWC == "1")
        {
            TVarianceRequest2.AddCell(cellObjChk);
        }
        else
        {
            TVarianceRequest2.AddCell(cellObjUnchk);
        }
        TVarianceRequest2.AddCell(new Phrase("at a WCB Hearing. I", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TVarianceRequest.AddCell("");
        TVarianceRequest.AddCell(TVarianceRequest2);


        TVarianceRequest.AddCell("");
        TVarianceRequest.AddCell(new Phrase(" understand that if either party, the carrier or the claimant, opts in writing for resolution at a WCB hearing; the decision will be made at a WCB", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TVarianceRequest.AddCell("");
        TVarianceRequest.AddCell(new Phrase(" hearing. I understand that if neither party opts for resolution at a hearing, the variance issue will be decided by a medical arbitrator and the", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));
        TVarianceRequest.AddCell("");
        TVarianceRequest.AddCell(new Phrase(" resolution is binding and not appealable under WCL § 23.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Color.BLACK)));


        TSecG.AddCell(TVarianceRequest);
        TSecG.AddCell("");

        float[] fGsignPart = { 1.8f, 1.4f, 0.3f, 0.8f };
        PdfPTable TGsignPart = new PdfPTable(fGsignPart);
        TGsignPart.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TGsignPart.DefaultCell.FixedHeight = 14f;
        TGsignPart.WidthPercentage = 100;

        TGsignPart.AddCell(new Phrase("Claimant's / Claimant Representative's Signature:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.claimantSign == "")
        { TGsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))); }
        else
        {
            TGsignPart.AddCell(new Phrase(objDAO.claimantSign, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TGsignPart.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
        if (objDAO.claimantSignDate == "")
        {
            TGsignPart.AddCell(new Phrase("_______________", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
        }
        else
        {
            TGsignPart.AddCell(new Phrase(objDAO.claimantSignDate, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
        }
        TSecG.AddCell(TGsignPart);

        tblBaseFraction1.AddCell(TSecG);
        tblBase.AddCell(tblBaseFraction1);
        tblBase.AddCell("");
        tblBase.AddCell("");



        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        tblBase.AddCell(new Phrase("ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED, OR PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER, OR SELF-INSURER, ANY INFORMATION CONTAINING ANY FALSE MATERIAL STATEMENT OR CONCEALS ANY MATERIAL FACT SHALL BE GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND IMPRISONMENT.", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalHeadSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        tblBase.AddCell("");
        tblBase.AddCell("");
        tblBase.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        tblBase.AddCell(new Phrase("NYS Workers' Compensation Board, Centralized Mailing, PO Box 5205, Binghamton, NY 13902-5205", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        tblBase.AddCell("");
        tblBase.AddCell(new Phrase("Customer Service Toll-Free Number: 877-632-4996", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        tblBase.AddCell("");
        tblBase.AddCell("");




        float[] fFooterPage2 = { 1.4f, 1f, 1.7f, 0.7f };
        PdfPTable TFooterPage2 = new PdfPTable(fFooterPage2);
        TFooterPage2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        TFooterPage2.WidthPercentage = 100;

        TFooterPage2.AddCell(new Phrase("MG-2.0 (2-13) Page 2 of 2", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
        TFooterPage2.AddCell(new Phrase("FAX NUMBER: 877-533-0337", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TFooterPage2.AddCell(new Phrase("E-MAIL TO: wcbclaimsfiling@wcb.ny.gov", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Color.BLACK)));
        TFooterPage2.AddCell(new Phrase("www.wcb.ny.gov", iTextSharp.text.FontFactory.GetFont(FontStyle, NormalFontSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
        tblBase.AddCell(TFooterPage2);


        tblMain.AddCell(tblBase);
        document.Add(tblMain);





        document.Close();
        return szFilePath;
    }

    public AddMG2Casewise BindDataMG2PDF(String sz_CompanyID, String sz_CaseID, String i_Id)
    {
        AddMG2Casewise objDAO = new AddMG2Casewise();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("SP_GET_txn_mg2_case_wise_details", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", sz_CaseID);
            cmd.Parameters.AddWithValue("@i_Id", i_Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                objDAO.WCBCaseNumber = dt.Rows[0]["sz_wcb_case_no"].ToString();
                objDAO.carrierCaseNumber = dt.Rows[0]["sz_carrier_case_no"].ToString();
                objDAO.dateOfInjury = dt.Rows[0]["sz_date_of_injury"].ToString();
                objDAO.firstName = dt.Rows[0]["sz_patient_firstname"].ToString();
                objDAO.middleName = dt.Rows[0]["sz_patient_middlename"].ToString();
                objDAO.lastName = dt.Rows[0]["sz_patient_lastname"].ToString();
                objDAO.socialSecurityNumber = dt.Rows[0]["sz_security_no"].ToString();
                objDAO.patientAddress = dt.Rows[0]["sz_patient_address"].ToString();
                objDAO.employerNameAddress = dt.Rows[0]["sz_employee_name_address"].ToString();
                objDAO.insuranceNameAddress = dt.Rows[0]["sz_insurance_name_address"].ToString();


                objDAO.attendingDoctorNameAddress = dt.Rows[0]["sz_guidelines_reference"].ToString();//sz_doctor_name_address

                objDAO.providerWCBNumber = dt.Rows[0]["sz_individual_provider"].ToString();
                objDAO.doctorPhone = dt.Rows[0]["sz_teltphone_no"].ToString();
                objDAO.doctorFax = dt.Rows[0]["sz_fax_no"].ToString();
                objDAO.bodyInitial = dt.Rows[0]["sz_Guidline_Char"].ToString();
                objDAO.guidelineSection = dt.Rows[0]["sz_Guidline"].ToString();
                //objDAO.guidelineSection = dt.Rows[0]["sz_guidelines_reference"].ToString();
                objDAO.approvalRequest = dt.Rows[0]["sz_approval_request"].ToString();
                objDAO.dateOfService = dt.Rows[0]["sz_wcb_case_file"].ToString();
                objDAO.datesOfDeniedRequest = dt.Rows[0]["sz_applicable"].ToString();
                if (dt.Rows[0]["bt_did"].ToString() == "1")
                    objDAO.chkDid = "1";
                else
                    objDAO.chkDid = "0";
                if (dt.Rows[0]["bt_not_did"].ToString() == "1")
                    objDAO.chkDidNot = "1";
                else
                    objDAO.chkDidNot = "0";
                objDAO.contactDate = dt.Rows[0]["sz_spoke"].ToString();
                objDAO.personContacted = dt.Rows[0]["sz_spoke_anyone"].ToString();
                if (dt.Rows[0]["bt_a_copy"].ToString() == "1")
                    objDAO.chkCopySent = "1";
                else
                    objDAO.chkCopySent = "0";

                objDAO.faxEmail = dt.Rows[0]["sz_fund_by"].ToString();
                if (dt.Rows[0]["bt_equipped"].ToString() == "1")
                    objDAO.chkCopyNotSent = "1";
                else
                    objDAO.chkCopyNotSent = "0";

                objDAO.indicatedFaxEmail = dt.Rows[0]["sz_indicated"].ToString();
                objDAO.providerSign = "";// dt.Rows[0]["sz_provider_signature"].ToString();

                if (dt.Rows[0]["dt_provider_signature_date"].ToString() != "" && dt.Rows[0]["dt_provider_signature_date"].ToString() != "01/01/1900")
                    objDAO.providerSignDate = dt.Rows[0]["dt_provider_signature_date"].ToString();
                else
                    objDAO.providerSignDate = "";

                objDAO.patientName = dt.Rows[0]["sz_patient_firstname"].ToString() + " " + dt.Rows[0]["sz_patient_middlename"].ToString() + " " + dt.Rows[0]["sz_patient_lastname"].ToString();
                if (dt.Rows[0]["bt_self_insurrer"].ToString() == "1")
                    objDAO.chkNoticeGiven = "1";
                else
                    objDAO.chkNoticeGiven = "0";

                objDAO.printCarrierEmployerNoticeName = dt.Rows[0]["sz_print_name_D"].ToString();
                objDAO.noticeTitle = dt.Rows[0]["sz_title_D"].ToString();
                objDAO.noticeCarrierSign = "";// dt.Rows[0]["sz_signature_D"].ToString();

                if (dt.Rows[0]["dt_date_D"].ToString() != "" && dt.Rows[0]["dt_date_D"].ToString() != "01/01/1900")
                    objDAO.noticeCarrierSignDate = dt.Rows[0]["dt_date_D"].ToString();
                else
                    objDAO.noticeCarrierSignDate = "";

                if (dt.Rows[0]["bt_granted"].ToString() == "1")
                    objDAO.chkGranted = "1";
                else
                    objDAO.chkGranted = "0";
                if (dt.Rows[0]["bt_granted_in_part"].ToString() == "1")
                    objDAO.chkGrantedInParts = "1";
                else
                    objDAO.chkGrantedInParts = "0";
                if (dt.Rows[0]["bt_without_prejudice"].ToString() == "1")
                    objDAO.chkWithoutPrejudice = "1";
                else
                    objDAO.chkWithoutPrejudice = "0";
                if (dt.Rows[0]["bt_denied"].ToString() == "1")
                    objDAO.chkDenied = "1";
                else
                    objDAO.chkDenied = "0";
                if (dt.Rows[0]["bt_burden"].ToString() == "1")
                    objDAO.chkBurden = "1";
                else
                    objDAO.chkBurden = "0";
                if (dt.Rows[0]["bt_substantialy"].ToString() == "1")
                    objDAO.chkSubstantiallySimilar = "1";
                else
                    objDAO.chkSubstantiallySimilar = "0";

                objDAO.carrierDenial = dt.Rows[0]["sz_section_E"].ToString();
                objDAO.medicalProfessional = dt.Rows[0]["sz_if_applicable"].ToString();
                if (dt.Rows[0]["bt_made_E"].ToString() == "1")
                    objDAO.chkMedicalArbitrator = "1";
                else
                    objDAO.chkMedicalArbitrator = "0";
                if (dt.Rows[0]["bt_chair_E"].ToString() == "1")
                    objDAO.chkWCBHearing = "1";
                else
                    objDAO.chkWCBHearing = "0";

                objDAO.printCarrierEmployerResponseName = dt.Rows[0]["sz_print_name_E"].ToString();
                objDAO.responseTitle = dt.Rows[0]["sz_title_E"].ToString();
                objDAO.responseCarrierSign = "";// dt.Rows[0]["sz_signature_E"].ToString();

                if (dt.Rows[0]["dt_date_E"].ToString() != "" && dt.Rows[0]["dt_date_E"].ToString() != "01/01/1900")
                    objDAO.responseCarrierSignDate = dt.Rows[0]["dt_date_E"].ToString();
                else
                    objDAO.responseCarrierSignDate = "";

                objDAO.printDenialCarrierName = dt.Rows[0]["sz_print_name_F"].ToString();
                objDAO.denialTitle = dt.Rows[0]["sz_title_F"].ToString();
                objDAO.denialCarrierSign = "";// dt.Rows[0]["sz_signature_F"].ToString();

                if (dt.Rows[0]["dt_date_F"].ToString() != "" && dt.Rows[0]["dt_date_F"].ToString() != "01/01/1900")
                    objDAO.denialCarrierSignDate = dt.Rows[0]["dt_date_F"].ToString();
                else
                    objDAO.denialCarrierSignDate = "";

                if (dt.Rows[0]["bt_i_request"].ToString() == "1")
                    objDAO.chkRequestWC = "1";
                else
                    objDAO.chkRequestWC = "0";
                if (dt.Rows[0]["bt_made_G"].ToString() == "1")
                    objDAO.chkMedicalArbitratorByWC = "1";
                else
                    objDAO.chkMedicalArbitratorByWC = "0";
                if (dt.Rows[0]["bt_chair_G"].ToString() == "1")
                    objDAO.chkWCBHearingByWC = "1";
                else
                    objDAO.chkWCBHearingByWC = "0";

                objDAO.claimantSign = "";// dt.Rows[0]["sz_claimant_signature"].ToString();

                if (dt.Rows[0]["dt_claimant_date"].ToString() != "" && dt.Rows[0]["dt_claimant_date"].ToString() != "01/01/1900")
                    objDAO.claimantSignDate = dt.Rows[0]["dt_claimant_date"].ToString();
                else
                    objDAO.claimantSignDate = "";
            }
            else
            {
                objDAO.WCBCaseNumber = "";
                objDAO.carrierCaseNumber = "";
                objDAO.dateOfInjury = "";
                objDAO.firstName = "";
                objDAO.middleName = "";
                objDAO.lastName = "";
                objDAO.socialSecurityNumber = "";
                objDAO.patientAddress = "";
                objDAO.employerNameAddress = "";
                objDAO.insuranceNameAddress = "";
                objDAO.attendingDoctorNameAddress = "";
                objDAO.providerWCBNumber = "";
                objDAO.doctorPhone = "";
                objDAO.doctorFax = "";
                objDAO.bodyInitial = "";
                objDAO.guidelineSection = "";
                objDAO.approvalRequest = "";
                objDAO.dateOfService = "";
                objDAO.datesOfDeniedRequest = "";
                objDAO.chkDid = "0";
                objDAO.chkDidNot = "0";
                objDAO.contactDate = "";
                objDAO.personContacted = "";
                objDAO.chkCopySent = "0";
                objDAO.faxEmail = "";
                objDAO.chkCopyNotSent = "0";
                objDAO.indicatedFaxEmail = "";
                objDAO.providerSign = "";
                objDAO.providerSignDate = "";
                objDAO.patientName = "";
                objDAO.chkNoticeGiven = "0";
                objDAO.printCarrierEmployerNoticeName = "";
                objDAO.noticeTitle = "";
                objDAO.noticeCarrierSign = "";
                objDAO.noticeCarrierSignDate = "";
                objDAO.chkGranted = "0";
                objDAO.chkGrantedInParts = "0";
                objDAO.chkWithoutPrejudice = "0";
                objDAO.chkDenied = "0";
                objDAO.chkBurden = "0";
                objDAO.chkSubstantiallySimilar = "0";
                objDAO.carrierDenial = "";
                objDAO.medicalProfessional = "";
                objDAO.chkMedicalArbitrator = "0";
                objDAO.chkWCBHearing = "0";
                objDAO.printCarrierEmployerResponseName = "";
                objDAO.responseTitle = "";
                objDAO.responseCarrierSign = "";
                objDAO.responseCarrierSignDate = "";
                objDAO.printDenialCarrierName = "";
                objDAO.denialTitle = "";
                objDAO.denialCarrierSign = "";
                objDAO.denialCarrierSignDate = "";
                objDAO.chkRequestWC = "0";
                objDAO.chkMedicalArbitratorByWC = "0";
                objDAO.chkWCBHearingByWC = "0";
                objDAO.claimantSign = "";
                objDAO.claimantSignDate = "";

            }
            return objDAO;
        }



        catch (SqlException ex)
        {
            return objDAO;
        }
        finally
        {

            con.Close();
        }
    }
}