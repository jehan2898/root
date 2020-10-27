using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;
using gb.mbs.da.model.common;
using specialtyNote = gb.mbs.da.service.patient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace gb.mbs.da.services.patient.form
{
   public class SrvMG2
    {
       private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);

       public DataSet Select(gbmodel.patient.Patient p_oPatient, gbmodel.account.Account p_oAccount)
       {

           DataSet ds = new DataSet();
           SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
           try
           {
               sqlCon.Open();
               SqlCommand sqlCmd = new SqlCommand("sp_select_mg2_forms_pdf", sqlCon);
               sqlCmd.CommandType = CommandType.StoredProcedure;
               sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
               sqlCmd.Parameters.AddWithValue("@i_case_id", p_oPatient.CaseID);
               SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
               sqlda.Fill(ds);



           }
           catch (SqlException ex)
           {
               throw ex;
           }
           finally
           {
               if (sqlCon.State == ConnectionState.Open)
               {
                   sqlCon.Close();
               }
           }
           return ds;
       }
       public string Print(gbmodel.account.Account p_oAccount, gbmodel.patient.Patient p_oPatient)
        {
            ApplicationSettings appSetting = new ApplicationSettings();
            try
           {
               
               int i;
               string[] strArrays;
               int length;
               int j;
               string[] strArrays1;
               string[] strArrays2;
               char chr;
               string[] strArrays3;
               string str = "Arial";
               int num = 12;
               int num1 = 6;
               int num2 = 10;
               int num3 = 20;
               int num4 = 7;
               int num5 = 8;
               int num6 = 6;
               string sFilePath = "";
               string str1 = ConfigurationSettings.AppSettings["CHECKBOXPATH"].ToString();
               string str2 = ConfigurationSettings.AppSettings["UNCHECKBOXPATH"].ToString();
               string str3 = ConfigurationSettings.AppSettings["WCLOGOPATH"].ToString();
               string sFilename = "";

               DataSet dsMG2 = new DataSet();
               dsMG2 = Select(p_oPatient, p_oAccount);
               gbmodel.patient.form.MG2 oMG2Form =new gbmodel.patient.form.MG2();

               // Bind the objects 
               oMG2Form.Employer = new gbmodel.employer.Employer();
               oMG2Form.Carrier = new gbmodel.carrier.Carrier();
               oMG2Form.Provider = new gbmodel.provider.Provider();
               oMG2Form.Patient = new gbmodel.patient.Patient();
               oMG2Form.Patient.Address = new gbmodel.common.Address();
               oMG2Form.Doctor = new gbmodel.physician.Physician();

               if (dsMG2 != null && dsMG2.Tables[0].Rows.Count > 0)
               {
                   oMG2Form.Patient.WCBNumber = dsMG2.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                   oMG2Form.Carrier.CaseNumber = dsMG2.Tables[0].Rows[0]["sz_carrier_case_no"].ToString();
                   oMG2Form.Patient.DOA = dsMG2.Tables[0].Rows[0]["sz_date_of_injury"].ToString();
                   oMG2Form.Patient.FirstName = dsMG2.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                   oMG2Form.Patient.MiddleName = dsMG2.Tables[0].Rows[0]["MI"].ToString();
                   oMG2Form.Patient.LastName = dsMG2.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                   oMG2Form.Patient.SSN = dsMG2.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                   oMG2Form.Patient.Address.AddressLines = dsMG2.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                   oMG2Form.Employer.Name = dsMG2.Tables[0].Rows[0]["sz_employee_name_address"].ToString();
                   oMG2Form.Carrier.Name = dsMG2.Tables[0].Rows[0]["sz_insurance_name_address"].ToString();
                   oMG2Form.Doctor.Name = dsMG2.Tables[0].Rows[0]["sz_doctor_ID"].ToString();
                   oMG2Form.Provider.WCBNumber = dsMG2.Tables[0].Rows[0]["sz_individual_provider"].ToString();
                   oMG2Form.Doctor.PhoneNo = dsMG2.Tables[0].Rows[0]["sz_teltphone_no"].ToString();
                   oMG2Form.Doctor.FaxNo = dsMG2.Tables[0].Rows[0]["sz_fax_no"].ToString();
                   oMG2Form.BodyInitial = dsMG2.Tables[0].Rows[0]["sz_Guidline_Char"].ToString();
                   oMG2Form.GuidelineSection = dsMG2.Tables[0].Rows[0]["sz_Guidline"].ToString();
                   oMG2Form.ApprovalRequest = dsMG2.Tables[0].Rows[0]["sz_approval_request"].ToString();
                   oMG2Form.DateOfService = dsMG2.Tables[0].Rows[0]["sz_wcb_case_file"].ToString();
                   oMG2Form.DatesOfDeniedRequest = dsMG2.Tables[0].Rows[0]["sz_applicable"].ToString();
                   if (!(dsMG2.Tables[0].Rows[0]["bt_did"].ToString() == "1"))
                   {
                       oMG2Form.ChkDid = "0";
                   }
                   else
                   {
                       oMG2Form.ChkDid = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_not_did"].ToString() == "1"))
                   {
                       oMG2Form.ChkDidNot = "0";
                   }
                   else
                   {
                       oMG2Form.ChkDidNot = "1";
                   }
                   oMG2Form.ContactDate = dsMG2.Tables[0].Rows[0]["sz_spoke"].ToString();
                   oMG2Form.PersonContacted = dsMG2.Tables[0].Rows[0]["sz_spoke_anyone"].ToString();
                   if (!(dsMG2.Tables[0].Rows[0]["bt_a_copy"].ToString() == "1"))
                   {
                       oMG2Form.ChkCopySent = "0";
                   }
                   else
                   {
                       oMG2Form.ChkCopySent = "1";
                   }
                   oMG2Form.FaxEmail = dsMG2.Tables[0].Rows[0]["sz_fund_by"].ToString();
                   if (!(dsMG2.Tables[0].Rows[0]["bt_equipped"].ToString() == "1"))
                   {
                       oMG2Form.ChkCopyNotSent = "0";
                   }
                   else
                   {
                       oMG2Form.ChkCopyNotSent = "1";
                   }
                   oMG2Form.IndicatedFaxEmail = dsMG2.Tables[0].Rows[0]["sz_indicated"].ToString();
                   oMG2Form.Provider.Sign = "";
                   if ((dsMG2.Tables[0].Rows[0]["dt_provider_signature_date"].ToString() == "" ? true : !(dsMG2.Tables[0].Rows[0]["dt_provider_signature_date"].ToString() != "01/01/1900")))
                   {
                       oMG2Form.Provider.SignDate = "";
                   }
                   else
                   {
                       oMG2Form.Provider.SignDate = dsMG2.Tables[0].Rows[0]["dt_provider_signature_date"].ToString();
                   }
                   
                   oMG2Form.Patient.Name = dsMG2.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString()+" "+dsMG2.Tables[0].Rows[0]["MI"].ToString()+" "+dsMG2.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                   if (!(dsMG2.Tables[0].Rows[0]["bt_self_insurrer"].ToString() == "1"))
                   {
                       oMG2Form.ChkNoticeGiven = "0";
                   }
                   else
                   {
                       oMG2Form.ChkNoticeGiven = "1";
                   }
                   oMG2Form.PrintCarrierEmployerNoticeName = dsMG2.Tables[0].Rows[0]["sz_print_name_D"].ToString();
                   oMG2Form.NoticeTitle = dsMG2.Tables[0].Rows[0]["sz_title_D"].ToString();
                   oMG2Form.NoticeCarrierSign = "";
                   if ((dsMG2.Tables[0].Rows[0]["dt_date_D"].ToString() == "" ? true : !(dsMG2.Tables[0].Rows[0]["dt_date_D"].ToString() != "01/01/1900")))
                   {
                       oMG2Form.NoticeCarrierSignDate = "";
                   }
                   else
                   {
                       oMG2Form.NoticeCarrierSignDate = dsMG2.Tables[0].Rows[0]["dt_date_D"].ToString();
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_granted"].ToString() == "1"))
                   {
                       oMG2Form.ChkGranted = "0";
                   }
                   else
                   {
                       oMG2Form.ChkGranted = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_granted_in_part"].ToString() == "1"))
                   {
                       oMG2Form.ChkGrantedInParts = "0";
                   }
                   else
                   {
                       oMG2Form.ChkGrantedInParts = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_without_prejudice"].ToString() == "1"))
                   {
                       oMG2Form.ChkWithoutPrejudice = "0";
                   }
                   else
                   {
                       oMG2Form.ChkWithoutPrejudice = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_denied"].ToString() == "1"))
                   {
                       oMG2Form.ChkDenied = "0";
                   }
                   else
                   {
                       oMG2Form.ChkDenied = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_burden"].ToString() == "1"))
                   {
                       oMG2Form.ChkBurden = "0";
                   }
                   else
                   {
                       oMG2Form.ChkBurden = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_substantialy"].ToString() == "1"))
                   {
                       oMG2Form.ChkSubstantiallySimilar = "0";
                   }
                   else
                   {
                       oMG2Form.ChkSubstantiallySimilar = "1";
                   }
                   oMG2Form.CarrierDenial = dsMG2.Tables[0].Rows[0]["sz_section_E"].ToString();
                   oMG2Form.MedicalProfessional = dsMG2.Tables[0].Rows[0]["sz_if_applicable"].ToString();
                   if (!(dsMG2.Tables[0].Rows[0]["bt_made_E"].ToString() == "1"))
                   {
                       oMG2Form.ChkMedicalArbitrator = "0";
                   }
                   else
                   {
                       oMG2Form.ChkMedicalArbitrator = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_chair_E"].ToString() == "1"))
                   {
                       oMG2Form.ChkWCBHearing = "0";
                   }
                   else
                   {
                       oMG2Form.ChkWCBHearing = "1";
                   }
                   oMG2Form.PrintCarrierEmployerResponseName = dsMG2.Tables[0].Rows[0]["sz_print_name_E"].ToString();
                   oMG2Form.ResponseTitle = dsMG2.Tables[0].Rows[0]["sz_title_E"].ToString();
                   oMG2Form.ResponseCarrierSign = "";
                   if ((dsMG2.Tables[0].Rows[0]["dt_date_E"].ToString() == "" ? true : !(dsMG2.Tables[0].Rows[0]["dt_date_E"].ToString() != "01/01/1900")))
                   {
                       oMG2Form.ResponseCarrierSignDate = "";
                   }
                   else
                   {
                       oMG2Form.ResponseCarrierSignDate = dsMG2.Tables[0].Rows[0]["dt_date_E"].ToString();
                   }
                   oMG2Form.PrintDenialCarrierName = dsMG2.Tables[0].Rows[0]["sz_print_name_F"].ToString();
                   oMG2Form.DenialTitle = dsMG2.Tables[0].Rows[0]["sz_title_F"].ToString();
                   oMG2Form.DenialCarrierSign = "";
                   if ((dsMG2.Tables[0].Rows[0]["dt_date_F"].ToString() == "" ? true : !(dsMG2.Tables[0].Rows[0]["dt_date_F"].ToString() != "01/01/1900")))
                   {
                       oMG2Form.DenialCarrierSignDate = "";
                   }
                   else
                   {
                       oMG2Form.DenialCarrierSignDate = dsMG2.Tables[0].Rows[0]["dt_date_F"].ToString();
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_i_request"].ToString() == "1"))
                   {
                       oMG2Form.ChkRequestWC = "0";
                   }
                   else
                   {
                       oMG2Form.ChkRequestWC = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_made_G"].ToString() == "1"))
                   {
                       oMG2Form.ChkMedicalArbitratorByWC = "0";
                   }
                   else
                   {
                       oMG2Form.ChkMedicalArbitratorByWC = "1";
                   }
                   if (!(dsMG2.Tables[0].Rows[0]["bt_chair_G"].ToString() == "1"))
                   {
                       oMG2Form.ChkWCBHearingByWC = "0";
                   }
                   else
                   {
                       oMG2Form.ChkWCBHearingByWC = "1";
                   }
                   oMG2Form.ClaimantSign = "";
                   if ((dsMG2.Tables[0].Rows[0]["dt_claimant_date"].ToString() == "" ? true : !(dsMG2.Tables[0].Rows[0]["dt_claimant_date"].ToString() != "01/01/1900")))
                   {
                       oMG2Form.ClaimantSignDate = "";
                   }
                   else
                   {
                       oMG2Form.ClaimantSignDate = dsMG2.Tables[0].Rows[0]["dt_claimant_date"].ToString();
                   }
               }
               else
               {
                   oMG2Form.Patient.WCBNumber = "";
                   oMG2Form.Carrier.CaseNumber = "";
                   oMG2Form.Patient.DOA = "";
                   oMG2Form.Patient.FirstName = "";
                   oMG2Form.Patient.MiddleName = "";
                   oMG2Form.Patient.LastName = "";
                   oMG2Form.Patient.SSN = "";
                   oMG2Form.Patient.Address.AddressLines = "";
                   oMG2Form.Employer.Name = "";
                   oMG2Form.Carrier.Name = "";
                   oMG2Form.Doctor.Name = "";
                   oMG2Form.Provider.WCBNumber = "";
                   oMG2Form.Doctor.PhoneNo = "";
                   oMG2Form.Doctor.FaxNo = "";
                   oMG2Form.BodyInitial = "";
                   oMG2Form.GuidelineSection = "";
                   oMG2Form.ApprovalRequest = "";
                   oMG2Form.DateOfService = "";
                   oMG2Form.DatesOfDeniedRequest = "";
                   oMG2Form.ChkDid = "0";
                   oMG2Form.ChkDidNot = "0";
                   oMG2Form.ContactDate = "";
                   oMG2Form.PersonContacted = "";
                   oMG2Form.ChkCopySent = "0";
                   oMG2Form.FaxEmail = "";
                   oMG2Form.ChkCopyNotSent = "0";
                   oMG2Form.IndicatedFaxEmail = "";
                   oMG2Form.Provider.Sign = "";
                   oMG2Form.Provider.SignDate = "";
                   oMG2Form.Patient.Name = "";
                   oMG2Form.ChkNoticeGiven = "0";
                   oMG2Form.PrintCarrierEmployerNoticeName = "";
                   oMG2Form.NoticeTitle = "";
                   oMG2Form.NoticeCarrierSign = "";
                   oMG2Form.NoticeCarrierSignDate = "";
                   oMG2Form.ChkGranted = "0";
                   oMG2Form.ChkGrantedInParts = "0";
                   oMG2Form.ChkWithoutPrejudice = "0";
                   oMG2Form.ChkDenied = "0";
                   oMG2Form.ChkBurden = "0";
                   oMG2Form.ChkSubstantiallySimilar = "0";
                   oMG2Form.CarrierDenial = "";
                   oMG2Form.MedicalProfessional = "";
                   oMG2Form.ChkMedicalArbitrator = "0";
                   oMG2Form.ChkWCBHearing = "0";
                   oMG2Form.PrintCarrierEmployerResponseName = "";
                   oMG2Form.ResponseTitle = "";
                   oMG2Form.ResponseCarrierSign = "";
                   oMG2Form.ResponseCarrierSignDate = "";
                   oMG2Form.PrintDenialCarrierName = "";
                   oMG2Form.DenialTitle = "";
                   oMG2Form.DenialCarrierSign = "";
                   oMG2Form.DenialCarrierSignDate = "";
                   oMG2Form.ChkRequestWC = "0";
                   oMG2Form.ChkMedicalArbitratorByWC = "0";
                   oMG2Form.ChkWCBHearingByWC = "0";
                   oMG2Form.ClaimantSign = "";
                   oMG2Form.ClaimantSignDate = "";
               }

               // create folder structure with filename
               if (dsMG2 != null && dsMG2.Tables[0].Rows.Count > 0)
               {

                   if (oMG2Form.Patient.MiddleName != "")
                   {
                       oMG2Form.Patient.MiddleName = oMG2Form.Patient.MiddleName.Replace(".", string.Empty);
                       oMG2Form.Patient.MiddleName = oMG2Form.Patient.MiddleName.Replace(",", string.Empty);
                   }

                   if (oMG2Form.Employer.Name != null)
                   {
                       oMG2Form.Employer.Name = oMG2Form.Employer.Name.Replace(",,", string.Empty);
                   }

                   string OpenPath = "";
                   OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + p_oAccount.Name + "/" + p_oPatient.CaseID + "/Packet Document/";
                   string BasePath = appSetting.GetParameterValue("PhysicalBasePath").ToString();
                   BasePath = BasePath + p_oAccount.Name + "/" + p_oPatient.CaseID + "/Packet Document/";
                   OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + p_oAccount.Name + "/" + p_oPatient.CaseID + "/Packet Document/";
                   if (!Directory.Exists(BasePath))
                   {
                       Directory.CreateDirectory(BasePath);
                   }
                   
                   sFilename = "MG2_Forms_" + getFileName(p_oPatient);
                   sFilePath = BasePath + sFilename;

                   // logic start to create pdf file

                   MemoryStream memoryStream = new MemoryStream();
                   FileStream fileStream = new FileStream(sFilePath, FileMode.OpenOrCreate);
                   Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
                   PdfWriter.GetInstance(document, fileStream);
                   document.Open();
                   Image instance = Image.GetInstance(str3);
                   PdfPCell pdfPCell = new PdfPCell(instance);
                   pdfPCell.Border = 0;
                   pdfPCell.HorizontalAlignment = 1;
                   pdfPCell.FixedHeight = 10f;
                   Image image = Image.GetInstance(str1);
                   image.ScaleAbsolute(12f, 12f);
                   PdfPCell pdfPCell1 = new PdfPCell(image);
                   pdfPCell1.Border = 0;
                   pdfPCell1.HorizontalAlignment = 1;
                   pdfPCell1.FixedHeight = 10f;
                   Image instance1 = Image.GetInstance(str2);
                   instance1.ScaleAbsolute(12f, 12f);
                   PdfPCell pdfPCell2 = new PdfPCell(instance1);
                   pdfPCell2.Border = 0;
                   pdfPCell2.HorizontalAlignment = 1;
                   pdfPCell2.FixedHeight = 10f;
                   float[] singleArray = new float[] { 6f };
                   PdfPTable pdfPTable = new PdfPTable(singleArray);
                   pdfPTable.DefaultCell.Border = 0;
                   pdfPTable.WidthPercentage = 100f;
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable1 = new PdfPTable(singleArray);
                   pdfPTable1.DefaultCell.Border = 0;
                   pdfPTable1.WidthPercentage = 100f;
                   singleArray = new float[] { 1f, 22f };
                   PdfPTable pdfPTable2 = new PdfPTable(singleArray);
                   pdfPTable2.DefaultCell.Border = 0;
                   pdfPTable2.WidthPercentage = 100f;
                   PdfPTable pdfPTable3 = new PdfPTable(new float[] { 1f, 4f, 1f });
                   pdfPTable3.DefaultCell.Border = 0;
                   pdfPTable3.DefaultCell.VerticalAlignment = 1;
                   pdfPTable3.DefaultCell.HorizontalAlignment = 1;
                   pdfPTable3.WidthPercentage = 100f;
                   pdfPTable3.AddCell(instance);
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable4 = new PdfPTable(singleArray);
                   pdfPTable4.DefaultCell.Border = 0;
                   pdfPTable4.DefaultCell.HorizontalAlignment = 1;
                   pdfPTable4.WidthPercentage = 100f;
                   pdfPTable4.AddCell(new Phrase("ATTENDING DOCTOR'S REQUEST FOR APPROVAL OF VARIANCE AND CARRIER'S RESPONSE", FontFactory.GetFont(str, (float)num, 1, Color.BLACK)));
                   pdfPTable4.AddCell(new Phrase("State of New York - Workers' Compensation Board", FontFactory.GetFont(str, (float)num1, 3, Color.BLACK)));
                   pdfPTable4.AddCell(new Phrase("For additional variance requests in this case, attach Form MG-2.1. Answer all questions where information is known.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   pdfPTable3.AddCell(pdfPTable4);
                   pdfPTable3.DefaultCell.VerticalAlignment = 1;
                   pdfPTable3.AddCell(new Phrase("MG-2", FontFactory.GetFont(str, (float)num3, 1, Color.BLACK)));
                   pdfPTable2.AddCell("");
                   pdfPTable2.AddCell(pdfPTable3);
                   PdfPTable pdfPTable5 = new PdfPTable(new float[] { 0.6f, 0.4f, 0.6f, 0.4f, 0.6f, 0.4f });
                   pdfPTable5.DefaultCell.Border = 7;
                   pdfPTable5.DefaultCell.VerticalAlignment = 1;
                   pdfPTable5.DefaultCell.FixedHeight = 14f;
                   pdfPTable5.WidthPercentage = 100f;
                   pdfPTable5.AddCell(new Phrase("WCB Case Number:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable5.DefaultCell.Border = 3;
                   pdfPTable5.AddCell(new Phrase(oMG2Form.Patient.WCBNumber, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable5.DefaultCell.Border = 7;
                   pdfPTable5.AddCell(new Phrase("Carrier Case Number:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable5.DefaultCell.Border = 3;
                   pdfPTable5.AddCell(new Phrase(oMG2Form.Carrier.CaseNumber, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable5.DefaultCell.Border = 7;
                   pdfPTable5.AddCell(new Phrase("Date of Injury:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable5.DefaultCell.Border = 3;
                   pdfPTable5.AddCell(new Phrase(oMG2Form.Patient.DOA, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable2.AddCell("");
                   pdfPTable2.AddCell(pdfPTable5);
                   pdfPTable2.AddCell("");
                   pdfPTable2.AddCell("");
                   pdfPTable2.AddCell(new Phrase("A.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   PdfPTable pdfPTable6 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1.2f, 0.8f });
                   pdfPTable6.DefaultCell.Border = 0;
                   pdfPTable6.DefaultCell.FixedHeight = 14f;
                   pdfPTable6.WidthPercentage = 100f;
                   pdfPTable6.AddCell(new Phrase("Patient's Name:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.DefaultCell.Border = 2;
                   pdfPTable6.AddCell(new Phrase(oMG2Form.Patient.FirstName, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.AddCell(new Phrase(oMG2Form.Patient.MiddleName, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.AddCell(new Phrase(oMG2Form.Patient.LastName, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.DefaultCell.Border = 0;
                   pdfPTable6.AddCell(new Phrase("Social Security No.:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.DefaultCell.Border = 2;
                   pdfPTable6.AddCell(new Phrase(oMG2Form.Patient.SSN, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.DefaultCell.Border = 0;
                   pdfPTable6.DefaultCell.FixedHeight = 10f;
                   pdfPTable6.AddCell("");
                   pdfPTable6.AddCell(new Phrase("First", FontFactory.GetFont(str, (float)num6, Color.BLACK)));
                   pdfPTable6.AddCell(new Phrase("MI", FontFactory.GetFont(str, (float)num6, Color.BLACK)));
                   pdfPTable6.AddCell(new Phrase("Last", FontFactory.GetFont(str, (float)num6, Color.BLACK)));
                   pdfPTable6.AddCell("");
                   pdfPTable6.AddCell("");
                   pdfPTable6.DefaultCell.HorizontalAlignment = 3;
                   pdfPTable6.DefaultCell.FixedHeight = 14f;
                   pdfPTable6.AddCell(new Phrase("Patient's Address:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable6.DefaultCell.HorizontalAlignment = 1;
                   PdfPCell pdfPCell3 = new PdfPCell(new Phrase(oMG2Form.Patient.Address.AddressLines, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell3.Colspan = 5;
                   pdfPCell3.FixedHeight = 14f;
                   pdfPCell3.Border = 2;
                   pdfPCell3.PaddingBottom = 1f;
                   pdfPTable6.AddCell(pdfPCell3);
                   PdfPCell pdfPCell4 = new PdfPCell(new Phrase("Employer's Name & Address:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell4.Colspan = 2;
                   pdfPCell4.Border = 0;
                   pdfPCell4.FixedHeight = 14f;
                   pdfPCell4.PaddingBottom = 1f;
                   pdfPTable6.AddCell(pdfPCell4);
                   PdfPCell pdfPCell5 = new PdfPCell(new Phrase(oMG2Form.Employer.Name, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell5.Colspan = 4;
                   pdfPCell5.Border = 2;
                   pdfPCell5.PaddingBottom = 1f;
                   pdfPTable6.AddCell(pdfPCell5);
                   PdfPCell pdfPCell6 = new PdfPCell(new Phrase("Insurance Carrier's Name & Address: ", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell6.Colspan = 2;
                   pdfPCell6.Border = 0;
                   pdfPCell6.FixedHeight = 14f;
                   pdfPCell6.PaddingBottom = 1f;
                   pdfPTable6.AddCell(pdfPCell6);
                   PdfPCell pdfPCell7 = new PdfPCell(new Phrase(oMG2Form.Carrier.Name, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell7.Colspan = 4;
                   pdfPCell7.Border = 2;
                   pdfPCell7.FixedHeight = 14f;
                   pdfPCell7.PaddingBottom = 1f;
                   pdfPTable6.AddCell(pdfPCell7);
                   pdfPTable2.AddCell(pdfPTable6);
                   pdfPTable2.AddCell(new Phrase("B.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable7 = new PdfPTable(singleArray);
                   pdfPTable7.DefaultCell.Border = 0;
                   pdfPTable7.WidthPercentage = 100f;
                   singleArray = new float[] { 2f, 4f };
                   PdfPTable pdfPTable8 = new PdfPTable(singleArray);
                   pdfPTable8.DefaultCell.Border = 0;
                   pdfPTable8.DefaultCell.FixedHeight = 14f;
                   pdfPTable8.WidthPercentage = 100f;
                   pdfPTable8.AddCell(new Phrase("Attending Doctor's Name & Address:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable8.DefaultCell.Border = 2;
                   pdfPTable8.AddCell(new Phrase(oMG2Form.Doctor.Name, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable7.AddCell(pdfPTable8);
                   pdfPTable2.AddCell(pdfPTable7);
                   PdfPTable pdfPTable9 = new PdfPTable(new float[] { 3f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.05f, 0.17f, 0.1f, 0.17f, 0.05f, 0.17f, 1.1f, 1.2f, 0.7f, 1.1f });
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.DefaultCell.FixedHeight = 14f;
                   pdfPTable9.WidthPercentage = 100f;
                   string str4 = oMG2Form.Provider.WCBNumber;
                   string[] strArrays4 = new string[str4.Length];
                   for (i = 0; i < str4.Length; i++)
                   {
                       chr = str4[i];
                       strArrays4[i] = chr.ToString();
                   }
                   pdfPTable9.AddCell(new Phrase("Individual Provider's WCB Authorization No.:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 0)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[0], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 1)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[1], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 2)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[2], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 3)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[3], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 4)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[4], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 5)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[5], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("-", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 6)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[6], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 15;
                   if (str4.Length <= 7)
                   {
                       pdfPTable9.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   else
                   {
                       pdfPTable9.AddCell(new Phrase(strArrays4[7], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   }
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("Phone No.:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 2;
                   pdfPTable9.AddCell(new Phrase(oMG2Form.Doctor.PhoneNo, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 0;
                   pdfPTable9.AddCell(new Phrase("Fax No.:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable9.DefaultCell.Border = 2;
                   pdfPTable9.AddCell(new Phrase(oMG2Form.Doctor.FaxNo, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable2.AddCell("");
                   pdfPTable2.AddCell(pdfPTable9);
                   pdfPTable2.AddCell(new Phrase("C.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable10 = new PdfPTable(singleArray);
                   pdfPTable10.DefaultCell.Border = 0;
                   pdfPTable10.WidthPercentage = 100f;
                   pdfPTable10.AddCell(new Phrase("The undersigned requests approval to VARY from the WCB Medical Treatment Guidelines as indicated below:", FontFactory.GetFont(str, (float)num1, 2, Color.BLACK)));
                   singleArray = new float[] { 2.3f, 4.2f };
                   PdfPTable pdfPTable11 = new PdfPTable(singleArray);
                   pdfPTable11.DefaultCell.Border = 0;
                   pdfPTable11.WidthPercentage = 100f;
                   PdfPTable pdfPTable12 = new PdfPTable(new float[] { 2.8f, 0.3f, 0.2f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f, 0.1f, 0.3f });
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.WidthPercentage = 100f;
                   string str5 = oMG2Form.GuidelineSection;
                   if (str5 == "")
                   {
                       str5 = "NONE";
                   }
                   string[] strArrays5 = new string[str5.Length];
                   for (i = 0; i < str5.Length; i++)
                   {
                       chr = str5[i];
                       strArrays5[i] = chr.ToString();
                   }
                   pdfPTable12.AddCell(new Phrase("Guideline Reference:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(oMG2Form.BodyInitial, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.AddCell(new Phrase("-", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(strArrays5[0], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(strArrays5[1], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(strArrays5[2], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(strArrays5[3], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 0;
                   pdfPTable12.AddCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable12.DefaultCell.Border = 15;
                   pdfPTable12.AddCell(new Phrase(strArrays5[4], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable11.AddCell(pdfPTable12);
                   PdfPTable pdfPTable13 = new PdfPTable(new float[] { 3.4f, 0.12f, 0.9f, 0.12f, 2.7f, 0.12f, 0.9f, 0.12f, 1f });
                   pdfPTable13.DefaultCell.Border = 0;
                   pdfPTable13.WidthPercentage = 100f;
                   pdfPTable13.AddCell(new Phrase("(In first box, indicate body part: K=", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("K", FontFactory.GetFont(str, (float)num4, 1, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("nee, S=", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("S", FontFactory.GetFont(str, (float)num4, 1, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("houlder, B= Mid and Low ", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("B", FontFactory.GetFont(str, (float)num4, 1, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("ack, N=", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("N", FontFactory.GetFont(str, (float)num4, 1, Color.BLACK)));
                   pdfPTable13.AddCell(new Phrase("eck,", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable11.AddCell(pdfPTable13);
                   PdfPTable pdfPTable14 = new PdfPTable(new float[] { 1.1f, 0.3f, 23f });
                   pdfPTable14.DefaultCell.Border = 0;
                   pdfPTable14.WidthPercentage = 100f;
                   pdfPTable14.AddCell(new Phrase("C=", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable14.AddCell(new Phrase("C", FontFactory.GetFont(str, (float)num4, 1, Color.BLACK)));
                   pdfPTable14.AddCell(new Phrase("arpal Tunnel. In remaining boxes, indicate corresponding section of WCB Medical Treatment", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable11.AddCell("");
                   pdfPTable11.AddCell(pdfPTable14);
                   pdfPTable11.AddCell("");
                   pdfPTable11.AddCell(new Phrase("Guidelines. If the treatment requested is not addressed by the Guidelines, in the remaining boxes use NONE.)", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable11);
                   PdfPTable pdfPTable15 = new PdfPTable(new float[] { 1.35f, 0.25f, 4.4f });
                   pdfPTable15.DefaultCell.Border = 0;
                   pdfPTable15.WidthPercentage = 100f;
                   pdfPTable15.AddCell(new Phrase("Approval Requested for: (", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable15.DefaultCell.Border = 0;
                   pdfPTable15.AddCell(new Phrase("one", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable15.DefaultCell.Border = 0;
                   pdfPTable15.AddCell(new Phrase("request type per form)", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable15);
                   int num7 = 1;
                   int num8 = 0;
                   int num9 = 0;
                   string str6 = oMG2Form.ApprovalRequest.ToString();
                   if (str6.Length <= 120)
                   {
                       strArrays3 = new string[] { str6 };
                       strArrays = strArrays3;
                   }
                   else
                   {
                       length = str6.Length / 120;
                       if (str6.Length % 120 <= 0)
                       {
                           strArrays = new string[length];
                           num7 = length;
                       }
                       else
                       {
                           strArrays = new string[length + 1];
                           num7 = length + 1;
                       }
                       while (num9 < length)
                       {
                           strArrays[num9] = str6.Substring(num8, 120);
                           num8 = num8 + 120;
                           num9++;
                       }
                       strArrays[num9] = str6.Substring(num8);
                   }
                   PdfPCell[] pdfPCellArray = new PdfPCell[num7];
                   i = 0;
                   while (true)
                   {
                       if ((i >= num7 ? true : i >= 5))
                       {
                           break;
                       }
                       pdfPCellArray[i] = new PdfPCell(new Phrase(strArrays[i], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                       pdfPCellArray[i].Border = 2;
                       pdfPCellArray[i].FixedHeight = 14f;
                       pdfPCellArray[i].PaddingBottom = 1f;
                       pdfPTable10.AddCell(pdfPCellArray[i]);
                       i++;
                   }
                   PdfPCell pdfPCell8 = new PdfPCell(new Phrase("", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell8.Border = 2;
                   pdfPCell8.FixedHeight = 14f;
                   for (j = 0; j < 4 - num7; j++)
                   {
                       pdfPTable10.AddCell(pdfPCell8);
                   }
                   pdfPTable10.AddCell("");
                   pdfPTable10.AddCell(new Phrase("STATEMENT OF MEDICAL NECESSITY - See item 4 on instruction page.", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("Your explanation must provide the following information:", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("    - the basis for your opinion that the medical care you propose is appropriate for the claimant and is medically necessary at this time; and", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("    - an explanation why alternatives set forth in the Medical Treatment Guidelines are not appropriate or sufficient.", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("If applicable, your explanation must also provide:", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("    - the symptoms, signs, or lack of improvement that compel you to seek the proposed treatment, or", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("    - a description of the functional outcomes that, as of the date of the variance request, have continued to demonstrate objective improvement from that treatment and are reasonably expected to further improve with additional treatment.", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("      are reasonably expected to further improve with additional treatment.", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("    - the specific duration or frequency of treatment for which a variance is requested.", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(new Phrase("You have the option to submit citations or copies of relevant literature published in recognized, peer-reviewed medical journals as part of the basis in support of this variance request.", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   singleArray = new float[] { 3.2f, 2.5f };
                   PdfPTable pdfPTable16 = new PdfPTable(singleArray);
                   pdfPTable16.DefaultCell.Border = 0;
                   pdfPTable16.WidthPercentage = 100f;
                   pdfPTable16.AddCell(new Phrase("Date of service of supporting medical in WCB case file, if not attached:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable16.AddCell(new Phrase(oMG2Form.DateOfService, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable16);
                   singleArray = new float[] { 4.5f, 1.5f };
                   PdfPTable pdfPTable17 = new PdfPTable(singleArray);
                   pdfPTable17.DefaultCell.Border = 0;
                   pdfPTable17.DefaultCell.FixedHeight = 14f;
                   pdfPTable17.WidthPercentage = 100f;
                   string str7 = oMG2Form.DatesOfDeniedRequest.ToString();
                   string str8 = "";
                   str8 = (str7.Length <= 27 ? str7 : str7.Substring(0, 27));
                   pdfPTable17.AddCell(new Phrase("Date(s) of previously denied variance request for substantially similar treatment, if applicable:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable17.DefaultCell.Border = 2;
                   pdfPTable17.AddCell(new Phrase(str8, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable17);
                   num7 = 1;
                   num8 = 27;
                   num9 = 0;
                   int length1 = str7.Length - 27;
                   if (length1 > 120)
                   {
                       length = length1 / 120;
                       if (length1 % 120 <= 0)
                       {
                           strArrays1 = new string[length];
                           num7 = length;
                       }
                       else
                       {
                           strArrays1 = new string[length + 1];
                           num7 = length + 1;
                       }
                       while (num9 < length)
                       {
                           strArrays1[num9] = str7.Substring(num8, 120);
                           num8 = num8 + 120;
                           num9++;
                       }
                       strArrays1[num9] = str7.Substring(num8);
                   }
                   else if ((length1 <= 0 ? true : length1 >= 120))
                   {
                       strArrays3 = new string[] { "" };
                       strArrays1 = strArrays3;
                   }
                   else
                   {
                       strArrays3 = new string[] { str7.Substring(27, str7.Length - 27) };
                       strArrays1 = strArrays3;
                   }
                   PdfPCell[] pdfPCellArray1 = new PdfPCell[num7];
                   i = 0;
                   while (true)
                   {
                       if ((i >= num7 ? true : i >= 5))
                       {
                           break;
                       }
                       pdfPCellArray1[i] = new PdfPCell(new Phrase(strArrays1[i], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                       pdfPCellArray1[i].Border = 2;
                       pdfPCellArray1[i].FixedHeight = 14f;
                       pdfPCellArray1[i].PaddingBottom = 1f;
                       pdfPTable10.AddCell(pdfPCellArray1[i]);
                       i++;
                   }
                   for (j = 0; j < 4 - num7; j++)
                   {
                       pdfPTable10.AddCell(pdfPCell8);
                   }
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable18 = new PdfPTable(singleArray);
                   pdfPTable18.DefaultCell.Border = 0;
                   pdfPTable18.WidthPercentage = 100f;
                   pdfPTable18.AddCell(new Phrase("I certify that I am making the above request for approval of a variance and my affirmative statements are true and correct. I certify that I have", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable18.AddCell(new Phrase("read and applied the Medical Treatment Guidelines to the treatment and care in this case and that I am requesting this variance before", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable18.AddCell(new Phrase("rendering any medical care that varies from the Guidelines. I certify that the claimant understands and agrees to undergo the proposed medical", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable18);
                   PdfPTable pdfPTable19 = new PdfPTable(new float[] { 0.9f, 0.3f, 0.7f, 0.3f, 14.5f });
                   pdfPTable19.DefaultCell.Border = 0;
                   pdfPTable19.WidthPercentage = 100f;
                   pdfPTable19.AddCell(new Phrase("care. I", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkDid == "1"))
                   {
                       pdfPTable19.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable19.AddCell(pdfPCell1);
                   }
                   pdfPTable19.AddCell(new Phrase(" did /", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkDidNot == "1"))
                   {
                       pdfPTable19.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable19.AddCell(pdfPCell1);
                   }
                   pdfPTable19.AddCell(new Phrase(" did not contact the carrier by telephone to discuss this variance request before making the request. I contacted the carrier by", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable19);
                   PdfPTable pdfPTable20 = new PdfPTable(new float[] { 0.6f, 0.4f, 1.8f, 1f });
                   pdfPTable20.DefaultCell.Border = 0;
                   pdfPTable20.DefaultCell.FixedHeight = 14f;
                   pdfPTable20.WidthPercentage = 100f;
                   pdfPTable20.AddCell(new Phrase("telephone on (date)", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable20.AddCell(new Phrase(oMG2Form.ContactDate, FontFactory.GetFont(str, (float)num5, 4, Color.BLACK)));
                   pdfPTable20.AddCell(new Phrase("and spoke to (person spoke to or was not able to speak to anyone)", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable20.DefaultCell.Border = 2;
                   pdfPTable20.AddCell(new Phrase(oMG2Form.PersonContacted, FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable10.AddCell(pdfPTable20);
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable21 = new PdfPTable(singleArray);
                   pdfPTable21.DefaultCell.Border = 0;
                   pdfPTable21.WidthPercentage = 100f;
                   singleArray = new float[] { 0.1f, 4.5f };
                   PdfPTable pdfPTable22 = new PdfPTable(singleArray);
                   pdfPTable22.DefaultCell.Border = 0;
                   pdfPTable22.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkCopySent == "1"))
                   {
                       pdfPTable22.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable22.AddCell(pdfPCell1);
                   }
                   pdfPTable22.AddCell(new Phrase("A copy of this form was sent to the carrier/employer/self-insured employer/Special Fund by (fax number or email address required)", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable21.AddCell(pdfPTable22);
                   pdfPTable21.AddCell(new Phrase(oMG2Form.FaxEmail, FontFactory.GetFont(str, (float)num5, 4, Color.BLACK)));
                   pdfPTable21.AddCell(new Phrase("A copy was sent to the Workers' Compensation Board, and copies were provided to the claimant’s legal counsel, if any, to the claimant if not", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable21.AddCell(new Phrase("represented, and to any other parties of interest within two (2) business days of the date below.", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable21.AddCell("");
                   singleArray = new float[] { 0.1f, 4.5f };
                   PdfPTable pdfPTable23 = new PdfPTable(singleArray);
                   pdfPTable23.DefaultCell.Border = 0;
                   pdfPTable23.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkCopyNotSent == "1"))
                   {
                       pdfPTable23.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable23.AddCell(pdfPCell1);
                   }
                   pdfPTable23.AddCell(new Phrase("I am not equipped to send or receive forms by fax or email. This form was mailed to the parties indicated above on", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable21.AddCell(pdfPTable23);
                   pdfPTable21.AddCell(new Phrase(oMG2Form.IndicatedFaxEmail, FontFactory.GetFont(str, (float)num5, 4, Color.BLACK)));
                   pdfPTable21.AddCell("");
                   pdfPTable21.AddCell(new Phrase("In addition, I certify that I do not have a substantially similar request pending and that this request contains additional supporting", FontFactory.GetFont(str, (float)num5, 1, Color.BLACK)));
                   pdfPTable21.AddCell(new Phrase("medical evidence if it is substantially similar to a prior denied request.", FontFactory.GetFont(str, (float)num5, 1, Color.BLACK)));
                   pdfPTable21.AddCell("");
                   PdfPTable pdfPTable24 = new PdfPTable(new float[] { 1.5f, 4f, 0.5f, 1.5f });
                   pdfPTable24.DefaultCell.Border = 0;
                   pdfPTable24.DefaultCell.FixedHeight = 14f;
                   pdfPTable24.WidthPercentage = 100f;
                   pdfPTable24.AddCell(new Phrase("Provider's Signature:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable24.AddCell(new Phrase(oMG2Form.Provider.Sign, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable24.AddCell(new Phrase("Date:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable24.AddCell(new Phrase(oMG2Form.Provider.SignDate, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable21.AddCell(pdfPTable24);
                   pdfPTable10.AddCell(pdfPTable21);
                   pdfPTable2.AddCell(pdfPTable10);
                   pdfPTable1.AddCell(pdfPTable2);
                   PdfPTable pdfPTable25 = new PdfPTable(new float[] { 1.3f, 4.2f, 0.7f });
                   pdfPTable25.DefaultCell.Border = 0;
                   pdfPTable25.WidthPercentage = 100f;
                   pdfPTable25.AddCell(new Phrase("MG-2.0 (2-13) Page 1 of 2", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable25.AddCell(new Phrase("THE WORKERS' COMPENSATION BOARD EMPLOYS AND SERVES PEOPLE WITH DISABILITIES WITHOUT DISCRIMINATION.", FontFactory.GetFont(str, (float)num6, 1, Color.BLACK)));
                   pdfPTable25.AddCell(new Phrase("www.wcb.ny.gov", FontFactory.GetFont(str, (float)num4, 3, Color.BLACK)));
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell(pdfPTable25);
                   singleArray = new float[] { 1f, 22f };
                   PdfPTable pdfPTable26 = new PdfPTable(singleArray);
                   pdfPTable26.DefaultCell.Border = 0;
                   pdfPTable26.WidthPercentage = 100f;
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   PdfPTable pdfPTable27 = new PdfPTable(new float[] { 0.4f, 0.6f, 0.5f, 0.5f, 0.4f, 0.6f });
                   pdfPTable27.DefaultCell.Border = 7;
                   pdfPTable27.DefaultCell.FixedHeight = 14f;
                   pdfPTable27.WidthPercentage = 100f;
                   pdfPTable27.AddCell(new Phrase("Patient Name:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable27.DefaultCell.Border = 3;
                   pdfPTable27.AddCell(new Phrase(oMG2Form.Patient.Name, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable27.DefaultCell.Border = 7;
                   pdfPTable27.AddCell(new Phrase("WCB Case Number:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable27.DefaultCell.Border = 3;
                   pdfPTable27.AddCell(new Phrase(oMG2Form.Patient.WCBNumber, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable27.DefaultCell.Border = 7;
                   pdfPTable27.AddCell(new Phrase("Date of Injury:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable27.DefaultCell.Border = 3;
                   pdfPTable27.AddCell(new Phrase(oMG2Form.Patient.DOA, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable26.AddCell("");
                   pdfPTable26.AddCell(pdfPTable27);
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   pdfPTable26.AddCell("");
                   pdfPTable26.CompleteRow();
                   pdfPTable26.AddCell(new Phrase("D.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable28 = new PdfPTable(singleArray);
                   pdfPTable28.DefaultCell.Border = 0;
                   pdfPTable28.DefaultCell.BorderWidth = 1f;
                   pdfPTable28.WidthPercentage = 100f;
                   pdfPTable28.DefaultCell.Border = 15;
                   pdfPTable28.AddCell(new Phrase("CARRIER'S / EMPLOYER'S NOTICE OF INDEPENDENT MEDICAL EXAMINATION (IME) OR MEDICAL RECORDS REVIEW", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable28.DefaultCell.Border = 0;
                   singleArray = new float[] { 0.1f, 4.5f };
                   PdfPTable pdfPTable29 = new PdfPTable(singleArray);
                   pdfPTable29.DefaultCell.Border = 0;
                   pdfPTable29.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkNoticeGiven == "1"))
                   {
                       pdfPTable29.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable29.AddCell(pdfPCell1);
                   }
                   pdfPTable29.AddCell(new Phrase("The self-insurer/carrier hereby gives notice that it will have the claimant examined by an Independent Medical Examiner or the claimant's medical records reviewed by a Records Reviewer and submit Form IME-4 within 30 calendar days of the variance request.", FontFactory.GetFont(str, 9f, Color.BLACK)));
                   pdfPTable28.AddCell(pdfPTable29);
                   pdfPTable28.AddCell("");
                   PdfPTable pdfPTable30 = new PdfPTable(new float[] { 0.6f, 2f, 0.3f, 1.5f });
                   pdfPTable30.DefaultCell.Border = 0;
                   pdfPTable30.DefaultCell.FixedHeight = 14f;
                   pdfPTable30.WidthPercentage = 100f;
                   pdfPTable30.AddCell(new Phrase("By: (print name)", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase(oMG2Form.PrintCarrierEmployerNoticeName, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase("Title:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase(oMG2Form.NoticeTitle, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable30.AddCell("");
                   pdfPTable30.CompleteRow();
                   pdfPTable30.AddCell(new Phrase("Signature:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase(oMG2Form.NoticeCarrierSign, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase("Date:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable30.AddCell(new Phrase(oMG2Form.NoticeCarrierSignDate, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable28.AddCell(pdfPTable30);
                   pdfPTable28.AddCell("");
                   pdfPTable26.AddCell(pdfPTable28);
                   pdfPTable26.AddCell("");
                   pdfPTable26.AddCell("");
                   pdfPTable26.AddCell(new Phrase("E.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable31 = new PdfPTable(singleArray);
                   pdfPTable31.DefaultCell.Border = 0;
                   pdfPTable31.DefaultCell.BorderWidth = 1f;
                   pdfPTable31.WidthPercentage = 100f;
                   pdfPTable31.DefaultCell.Border = 15;
                   pdfPTable31.AddCell(new Phrase("CARRIER'S / EMPLOYER'S RESPONSE TO VARIANCE REQUEST", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable31.DefaultCell.Border = 0;
                   singleArray = new float[] { 2.5f, 1.5f };
                   PdfPTable pdfPTable32 = new PdfPTable(singleArray);
                   pdfPTable32.DefaultCell.Border = 0;
                   pdfPTable32.WidthPercentage = 100f;
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable33 = new PdfPTable(singleArray);
                   pdfPTable33.DefaultCell.Border = 0;
                   pdfPTable33.WidthPercentage = 100f;
                   pdfPTable33.AddCell(new Phrase("Carrier's response to the variance request is indicated in the checkboxes on the right. Carrier denial, when appropriate, should be reviewed by a health professional. (Attach written report of medical professional.) If request is approved or denied, sign and date the form in Section E.", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable33.AddCell("");
                   pdfPTable33.AddCell("");
                   PdfPCell pdfPCell9 = new PdfPCell(new Phrase(oMG2Form.CarrierDenial, FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPCell9.Border = 2;
                   pdfPCell9.FixedHeight = 14f;
                   pdfPTable33.DefaultCell.Border = 2;
                   num7 = 1;
                   num8 = 0;
                   num9 = 0;
                   string str9 = oMG2Form.CarrierDenial.ToString();
                   if (str9.Length <= 75)
                   {
                       strArrays3 = new string[] { str9 };
                       strArrays2 = strArrays3;
                   }
                   else
                   {
                       length = str9.Length / 75;
                       if (str9.Length % 75 <= 0)
                       {
                           strArrays2 = new string[length];
                           num7 = length;
                       }
                       else
                       {
                           strArrays2 = new string[length + 1];
                           num7 = length + 1;
                       }
                       while (num9 < length)
                       {
                           strArrays2[num9] = str9.Substring(num8, 120);
                           num8 = num8 + 75;
                           num9++;
                       }
                       strArrays2[num9] = str9.Substring(num8);
                   }
                   PdfPCell[] pdfPCellArray2 = new PdfPCell[num7];
                   i = 0;
                   while (true)
                   {
                       if ((i >= num7 ? true : i >= 4))
                       {
                           break;
                       }
                       pdfPCellArray2[i] = new PdfPCell(new Phrase(strArrays2[i], FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                       pdfPCellArray2[i].Border = 2;
                       pdfPCellArray2[i].FixedHeight = 14f;
                       pdfPCellArray2[i].PaddingBottom = 1f;
                       pdfPTable33.AddCell(pdfPCellArray2[i]);
                       i++;
                   }
                   for (j = 0; j < 5 - num7; j++)
                   {
                       pdfPTable33.AddCell(pdfPCell8);
                   }
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable34 = new PdfPTable(singleArray);
                   pdfPTable34.DefaultCell.Border = 15;
                   pdfPTable34.WidthPercentage = 100f;
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable35 = new PdfPTable(singleArray);
                   pdfPTable35.DefaultCell.Border = 0;
                   pdfPTable35.WidthPercentage = 100f;
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable36 = new PdfPTable(singleArray);
                   pdfPTable36.DefaultCell.Border = 0;
                   pdfPTable36.WidthPercentage = 100f;
                   pdfPTable36.AddCell(new Phrase("CARRIER'S / EMPLOYER'S RESPONSE", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable36.AddCell(new Phrase("If service is denied or granted in part, explain in space provided.", FontFactory.GetFont(str, (float)num6, Color.BLACK)));
                   PdfPTable pdfPTable37 = new PdfPTable(new float[] { 1f, 7f, 1f, 7f });
                   pdfPTable37.DefaultCell.Border = 0;
                   pdfPTable37.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkGranted == "1"))
                   {
                       pdfPTable37.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable37.AddCell(pdfPCell1);
                   }
                   pdfPTable37.AddCell(new Phrase("Granted", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   if (!(oMG2Form.ChkWithoutPrejudice == "1"))
                   {
                       pdfPTable37.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable37.AddCell(pdfPCell1);
                   }
                   pdfPTable37.AddCell(new Phrase("Without Prejudice", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   if (!(oMG2Form.ChkGrantedInParts == "1"))
                   {
                       pdfPTable37.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable37.AddCell(pdfPCell1);
                   }
                   pdfPTable37.AddCell(new Phrase("Granted in Part", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable37.CompleteRow();
                   pdfPTable35.AddCell(pdfPTable36);
                   pdfPTable35.AddCell(pdfPTable37);
                   singleArray = new float[] { 0.1f, 1.5f };
                   PdfPTable pdfPTable38 = new PdfPTable(singleArray);
                   pdfPTable38.DefaultCell.Border = 0;
                   pdfPTable38.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkDenied == "1"))
                   {
                       pdfPTable38.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable38.AddCell(pdfPCell1);
                   }
                   pdfPTable38.AddCell(new Phrase("Denied", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   if (!(oMG2Form.ChkBurden == "1"))
                   {
                       pdfPTable38.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable38.AddCell(pdfPCell1);
                   }
                   pdfPTable38.AddCell(new Phrase("Burden of Proof Not Met", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   if (!(oMG2Form.ChkSubstantiallySimilar == "1"))
                   {
                       pdfPTable38.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable38.AddCell(pdfPCell1);
                   }
                   pdfPTable38.AddCell(new Phrase("Substantially Similar Request Pending or Denied", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable34.AddCell(pdfPTable35);
                   pdfPTable34.AddCell(pdfPTable38);
                   pdfPTable32.AddCell(pdfPTable33);
                   pdfPTable32.AddCell(pdfPTable34);
                   pdfPTable31.AddCell(pdfPTable32);
                   singleArray = new float[] { 1.4f, 1f };
                   PdfPTable pdfPTable39 = new PdfPTable(singleArray);
                   pdfPTable39.DefaultCell.Border = 0;
                   pdfPTable39.DefaultCell.FixedHeight = 14f;
                   pdfPTable39.WidthPercentage = 100f;
                   pdfPTable39.AddCell(new Phrase("Name of the Medical Professional who reviewed the denial, if applicable:", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable39.AddCell(new Phrase(oMG2Form.MedicalProfessional, FontFactory.GetFont(str, (float)num5, 4, Color.BLACK)));
                   pdfPTable31.AddCell(pdfPTable39);
                   pdfPTable31.AddCell(new Phrase(" I certify that copies of this form were sent to the Treating Medical Provider requesting the variance, the Workers' Compensation Board, the", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell(new Phrase(" claimant's legal counsel, if any, and any other parties of interest, with the written report of the medical professional in the office of the carrier/", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell(new Phrase(" employer/self-insured employer/Special Fund attached, within two (2) business days of the date below.", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   PdfPTable pdfPTable40 = new PdfPTable(new float[] { 1.05f, 1.85f, 0.07f, 0.5f });
                   pdfPTable40.DefaultCell.Border = 0;
                   pdfPTable40.WidthPercentage = 100f;
                   pdfPTable40.AddCell(new Phrase("(Please complete if request is denied.)", FontFactory.GetFont(str, (float)num5, 1, Color.BLACK)));
                   pdfPTable40.AddCell(new Phrase("If the issue cannot be resolved informally, I opt for the decision to be made", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkMedicalArbitrator == "1"))
                   {
                       pdfPTable40.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable40.AddCell(pdfPCell1);
                   }
                   pdfPTable40.AddCell(new Phrase("by the Medical", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell(pdfPTable40);
                   PdfPTable pdfPTable41 = new PdfPTable(new float[] { 1.6f, 0.1f, 4.4f });
                   pdfPTable41.DefaultCell.Border = 0;
                   pdfPTable41.WidthPercentage = 100f;
                   pdfPTable41.AddCell(new Phrase("Arbitrator designated by the Chair or", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkWCBHearing == "1"))
                   {
                       pdfPTable41.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable41.AddCell(pdfPCell1);
                   }
                   pdfPTable41.AddCell(new Phrase("at a WCB Hearing. I understand that if either party, the carrier or the claimant, opts in writing for", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell(pdfPTable41);
                   pdfPTable31.AddCell(new Phrase(" resolution at a WCB hearing; the decision will be made at a WCB hearing. I understand that if neither party opts for resolution at a hearing,", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell(new Phrase(" the variance issue will be decided by a medical arbitrator and the resolution is binding and not appealable under WCL § 23.", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable31.AddCell("");
                   PdfPTable pdfPTable42 = new PdfPTable(new float[] { 0.6f, 2f, 0.3f, 1.5f });
                   pdfPTable42.DefaultCell.Border = 0;
                   pdfPTable42.DefaultCell.FixedHeight = 14f;
                   pdfPTable42.WidthPercentage = 100f;
                   pdfPTable42.AddCell(new Phrase("By: (print name)", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase(oMG2Form.PrintCarrierEmployerResponseName, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase("Title:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase(oMG2Form.ResponseTitle, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable42.AddCell("");
                   pdfPTable42.CompleteRow();
                   pdfPTable42.AddCell(new Phrase("Signature:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase(oMG2Form.ResponseCarrierSign, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase("Date:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable42.AddCell(new Phrase(oMG2Form.ResponseCarrierSignDate, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable31.AddCell(pdfPTable42);
                   pdfPTable31.AddCell("");
                   pdfPTable26.AddCell(pdfPTable31);
                   pdfPTable26.AddCell(new Phrase("F.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable43 = new PdfPTable(singleArray);
                   pdfPTable43.DefaultCell.Border = 0;
                   pdfPTable43.DefaultCell.BorderWidth = 1f;
                   pdfPTable43.WidthPercentage = 100f;
                   pdfPTable43.DefaultCell.Border = 15;
                   pdfPTable43.AddCell(new Phrase("DENIAL INFORMALLY DISCUSSED AND RESOLVED BETWEEN PROVIDER AND CARRIER", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable43.DefaultCell.Border = 0;
                   pdfPTable43.AddCell(new Phrase(" I certify that the provider's variance request initially denied above is now granted or partially granted.", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable43.AddCell("");
                   PdfPTable pdfPTable44 = new PdfPTable(new float[] { 0.6f, 2f, 0.3f, 1.5f });
                   pdfPTable44.DefaultCell.Border = 0;
                   pdfPTable44.DefaultCell.FixedHeight = 14f;
                   pdfPTable44.WidthPercentage = 100f;
                   pdfPTable44.AddCell(new Phrase("By: (print name)", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase(oMG2Form.PrintDenialCarrierName, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase("Title:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase(oMG2Form.DenialTitle, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable44.AddCell("");
                   pdfPTable44.CompleteRow();
                   pdfPTable44.AddCell(new Phrase("Signature:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase(oMG2Form.DenialCarrierSign, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase("Date:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable44.AddCell(new Phrase(oMG2Form.DenialCarrierSignDate, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable43.AddCell(pdfPTable44);
                   pdfPTable26.AddCell(pdfPTable43);
                   pdfPTable26.AddCell(new Phrase("G.", FontFactory.GetFont(str, (float)num2, 1, Color.BLACK)));
                   singleArray = new float[] { 4f };
                   PdfPTable pdfPTable45 = new PdfPTable(singleArray);
                   pdfPTable45.DefaultCell.Border = 0;
                   pdfPTable45.DefaultCell.BorderWidth = 1f;
                   pdfPTable45.WidthPercentage = 100f;
                   pdfPTable45.DefaultCell.Border = 15;
                   pdfPTable45.AddCell(new Phrase("CLAIMANT'S / CLAIMANT REPRESENTATIVE'S REQUEST FOR REVIEW OF SELF-INSURED EMPLOYER'S / CARRIER'S DENIAL", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable45.DefaultCell.Border = 0;
                   singleArray = new float[] { 1f, 1f };
                   PdfPTable pdfPTable46 = new PdfPTable(singleArray);
                   pdfPTable46.DefaultCell.Border = 0;
                   pdfPTable46.WidthPercentage = 100f;
                   pdfPTable46.AddCell(new Phrase("NOTE to Claimant's / Claimant Licensed Representative's:", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable46.AddCell(new Phrase("The claimant should only sign this section after the request is", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable45.AddCell(pdfPTable46);
                   pdfPTable45.AddCell(new Phrase(" fully or partially denied. This section should not be completed at the time of initial request.", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable45.AddCell(new Phrase(" YOU MUST COMPLETE THIS SECTION IF YOU WANT THE BOARD TO REVIEW THE CARRIER'S DENIAL OF THE PROVIDER'S   VARIANCE REQUEST.", FontFactory.GetFont(str, (float)num5, 1, Color.BLACK)));
                   singleArray = new float[] { 0.1f, 4.5f };
                   PdfPTable pdfPTable47 = new PdfPTable(singleArray);
                   pdfPTable47.DefaultCell.Border = 0;
                   pdfPTable47.WidthPercentage = 100f;
                   if (!(oMG2Form.ChkRequestWC == "1"))
                   {
                       pdfPTable47.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable47.AddCell(pdfPCell1);
                   }
                   pdfPTable47.AddCell(new Phrase(" I request that the Workers' Compensation Board review the carrier's denial of my doctor's request for approval to vary from the Medical", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   PdfPTable pdfPTable48 = new PdfPTable(new float[] { 1.8f, 0.1f, 1.7f, 0.1f, 0.8f });
                   pdfPTable48.DefaultCell.Border = 0;
                   pdfPTable48.WidthPercentage = 100f;
                   pdfPTable48.AddCell(new Phrase("Treatment Guidelines. I opt for the decision to be made", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkMedicalArbitratorByWC == "1"))
                   {
                       pdfPTable48.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable48.AddCell(pdfPCell1);
                   }
                   pdfPTable48.AddCell(new Phrase("by the Medical Arbitrator designated by the Chair or", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   if (!(oMG2Form.ChkWCBHearingByWC == "1"))
                   {
                       pdfPTable48.AddCell(pdfPCell2);
                   }
                   else
                   {
                       pdfPTable48.AddCell(pdfPCell1);
                   }
                   pdfPTable48.AddCell(new Phrase("at a WCB Hearing. I", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable47.AddCell("");
                   pdfPTable47.AddCell(pdfPTable48);
                   pdfPTable47.AddCell("");
                   pdfPTable47.AddCell(new Phrase(" understand that if either party, the carrier or the claimant, opts in writing for resolution at a WCB hearing; the decision will be made at a WCB", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable47.AddCell("");
                   pdfPTable47.AddCell(new Phrase(" hearing. I understand that if neither party opts for resolution at a hearing, the variance issue will be decided by a medical arbitrator and the", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable47.AddCell("");
                   pdfPTable47.AddCell(new Phrase(" resolution is binding and not appealable under WCL § 23.", FontFactory.GetFont(str, (float)num5, Color.BLACK)));
                   pdfPTable45.AddCell(pdfPTable47);
                   pdfPTable45.AddCell("");
                   PdfPTable pdfPTable49 = new PdfPTable(new float[] { 1.8f, 1.4f, 0.3f, 0.8f });
                   pdfPTable49.DefaultCell.Border = 0;
                   pdfPTable49.DefaultCell.FixedHeight = 14f;
                   pdfPTable49.WidthPercentage = 100f;
                   pdfPTable49.AddCell(new Phrase("Claimant's / Claimant Representative's Signature:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable49.AddCell(new Phrase(oMG2Form.ClaimantSign, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable49.AddCell(new Phrase("Date:", FontFactory.GetFont(str, (float)num1, Color.BLACK)));
                   pdfPTable49.AddCell(new Phrase(oMG2Form.ClaimantSignDate, FontFactory.GetFont(str, (float)num1, 4, Color.BLACK)));
                   pdfPTable45.AddCell(pdfPTable49);
                   pdfPTable26.AddCell(pdfPTable45);
                   pdfPTable1.AddCell(pdfPTable26);
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell("");
                   pdfPTable1.DefaultCell.Border = 15;
                   pdfPTable1.AddCell(new Phrase("ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED, OR PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER, OR SELF-INSURER, ANY INFORMATION CONTAINING ANY FALSE MATERIAL STATEMENT OR CONCEALS ANY MATERIAL FACT SHALL BE GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND IMPRISONMENT.", FontFactory.GetFont(str, (float)num5, 1, Color.BLACK)));
                   pdfPTable1.DefaultCell.Border = 0;
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell("");
                   pdfPTable1.DefaultCell.HorizontalAlignment = 1;
                   pdfPTable1.AddCell(new Phrase("NYS Workers' Compensation Board, Centralized Mailing, PO Box 5205, Binghamton, NY 13902-5205", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell(new Phrase("Customer Service Toll-Free Number: 877-632-4996", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable1.AddCell("");
                   pdfPTable1.AddCell("");
                   PdfPTable pdfPTable50 = new PdfPTable(new float[] { 1.4f, 1f, 1.7f, 0.7f });
                   pdfPTable50.DefaultCell.Border = 0;
                   pdfPTable50.WidthPercentage = 100f;
                   pdfPTable50.AddCell(new Phrase("MG-2.0 (2-13) Page 2 of 2", FontFactory.GetFont(str, (float)num1, 1, Color.BLACK)));
                   pdfPTable50.AddCell(new Phrase("FAX NUMBER: 877-533-0337", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable50.AddCell(new Phrase("E-MAIL TO: wcbclaimsfiling@wcb.ny.gov", FontFactory.GetFont(str, (float)num4, Color.BLACK)));
                   pdfPTable50.AddCell(new Phrase("www.wcb.ny.gov", FontFactory.GetFont(str, (float)num4, 3, Color.BLACK)));
                   pdfPTable1.AddCell(pdfPTable50);
                   pdfPTable.AddCell(pdfPTable1);
                   document.Add(pdfPTable);
                   document.Close();

               }
               return sFilename;
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
      
       public void Create(gbmodel.patient.form.MG2 objMG2)
       {
           SqlConnection connection = new SqlConnection(sSQLCon);
               try
               {
                  
                   connection.Open();
                   SqlCommand cmd = new SqlCommand("sp_save_mg2_form", connection);
                   
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@sz_company_id", objMG2.Account.ID);
                   cmd.Parameters.AddWithValue("@i_case_id", objMG2.Patient.CaseID);
                   cmd.Parameters.AddWithValue("@sz_user_id", objMG2.User.ID);
                   cmd.Parameters.AddWithValue("@sz_modified_by", objMG2.User.ID);
                   cmd.Parameters.AddWithValue("@sz_guidelines_reference", "");
                   cmd.Parameters.AddWithValue("@sz_approval_request", objMG2.ApprovalRequest);
                   cmd.Parameters.AddWithValue("@sz_wcb_case_file", objMG2.DateOfService);
                   cmd.Parameters.AddWithValue("@sz_applicable", objMG2.DatesOfDeniedRequest);
                   cmd.Parameters.AddWithValue("@bt_did", objMG2.ChkDid);
                   cmd.Parameters.AddWithValue("@bt_not_did", objMG2.ChkDidNot);
                   cmd.Parameters.AddWithValue("@sz_spoke", objMG2.ContactDate);
                   cmd.Parameters.AddWithValue("@sz_spoke_anyone", objMG2.PersonContacted);
                   cmd.Parameters.AddWithValue("@bt_a_copy", objMG2.ChkCopySent);
                   cmd.Parameters.AddWithValue("@sz_fund_by", objMG2.FaxEmail);
                   cmd.Parameters.AddWithValue("@bt_equipped", objMG2.ChkCopyNotSent);
                   cmd.Parameters.AddWithValue("@sz_indicated", objMG2.IndicatedFaxEmail);
                   cmd.Parameters.AddWithValue("@dt_provider_signature_date", objMG2.Provider.SignDate);
                   cmd.Parameters.AddWithValue("@bt_self_insurrer", objMG2.ChkNoticeGiven);
                   cmd.Parameters.AddWithValue("@sz_print_name_D", objMG2.PrintCarrierEmployerNoticeName);
                   cmd.Parameters.AddWithValue("@sz_title_D", objMG2.NoticeTitle);
                   cmd.Parameters.AddWithValue("@dt_date_D", objMG2.NoticeCarrierSignDate);
                   cmd.Parameters.AddWithValue("@sz_section_E", objMG2.CarrierDenial);
                   cmd.Parameters.AddWithValue("@bt_granted", objMG2.ChkGranted);
                   cmd.Parameters.AddWithValue("@bt_granted_in_part", objMG2.ChkGrantedInParts);
                   cmd.Parameters.AddWithValue("@bt_without_prejudice", objMG2.ChkWithoutPrejudice);
                   cmd.Parameters.AddWithValue("@bt_denied", objMG2.ChkDenied);
                   cmd.Parameters.AddWithValue("@bt_burden", objMG2.ChkBurden);
                   cmd.Parameters.AddWithValue("@bt_substantialy", objMG2.ChkSubstantiallySimilar);
                   cmd.Parameters.AddWithValue("@sz_if_applicable", objMG2.MedicalProfessional);
                   cmd.Parameters.AddWithValue("@bt_made_E", objMG2.ChkMedicalArbitrator);
                   cmd.Parameters.AddWithValue("@bt_chair_E", objMG2.ChkWCBHearing);
                   cmd.Parameters.AddWithValue("@sz_print_name_E", objMG2.PrintCarrierEmployerResponseName);
                   cmd.Parameters.AddWithValue("@sz_title_E", objMG2.ResponseTitle);
                   cmd.Parameters.AddWithValue("@dt_date_E", objMG2.ResponseCarrierSignDate);
                   cmd.Parameters.AddWithValue("@sz_print_name_F", objMG2.PrintDenialCarrierName);
                   cmd.Parameters.AddWithValue("@sz_title_F", objMG2.DenialTitle);
                   cmd.Parameters.AddWithValue("@dt_date_F", objMG2.DenialCarrierSignDate);
                   cmd.Parameters.AddWithValue("@bt_i_request", objMG2.ChkRequestWC);
                   cmd.Parameters.AddWithValue("@bt_made_G", objMG2.ChkMedicalArbitratorByWC);
                   cmd.Parameters.AddWithValue("@bt_chair_G", objMG2.ChkWCBHearingByWC);
                   cmd.Parameters.AddWithValue("@dt_claimant_date", objMG2.ClaimantSignDate);
                   cmd.Parameters.AddWithValue("@sz_wcb_case_no", objMG2.Patient.WCBNumber);
                   cmd.Parameters.AddWithValue("@sz_carrier_case_no", objMG2.Carrier.CaseNumber);
                   cmd.Parameters.AddWithValue("@sz_date_of_injury", objMG2.Patient.DOA);
                   cmd.Parameters.AddWithValue("@sz_patient_firstname", objMG2.Patient.FirstName);
                   cmd.Parameters.AddWithValue("@sz_patient_middlename", objMG2.Patient.MiddleName);
                   cmd.Parameters.AddWithValue("@sz_patient_lastname", objMG2.Patient.LastName);
                   cmd.Parameters.AddWithValue("@sz_patient_address", objMG2.Patient.Address.AddressLines);
                   cmd.Parameters.AddWithValue("@sz_employee_name_address", objMG2.Employer.Name);
                   cmd.Parameters.AddWithValue("@sz_insurance_name_address", objMG2.Carrier.Name);
                   cmd.Parameters.AddWithValue("@sz_doctor_name_address", objMG2.Doctor.Name);
                   cmd.Parameters.AddWithValue("@sz_individual_provider", objMG2.Provider.WCBNumber);
                   cmd.Parameters.AddWithValue("@sz_teltphone_no", objMG2.Doctor.PhoneNo);
                   cmd.Parameters.AddWithValue("@sz_fax_no", objMG2.Doctor.FaxNo);
                   cmd.Parameters.AddWithValue("@sz_Guidline_Char", objMG2.BodyInitial);
                   cmd.Parameters.AddWithValue("@sz_Guidline", objMG2.GuidelineSection);
                   cmd.Parameters.AddWithValue("@sz_security_no", objMG2.Patient.SSN);
                   cmd.Parameters.AddWithValue("@sz_patient_id", objMG2.Patient.Patient_ID);
                   cmd.ExecuteNonQuery();
               }
               catch (Exception exception)
               {
                   throw exception;
               }
               finally
               {
                   if (connection != null)
                   {
                       if (connection.State == ConnectionState.Open)
                       {
                           connection.Close();
                       }
                       connection = null;
                   }
               }
           
       }

       private string getFileName(gbmodel.patient.Patient p_oPatient)
       {
           String sFileName = "";
           DateTime currentDate;
           currentDate = DateTime.Now;
           sFileName = p_oPatient.CaseID + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
           return sFileName;
       }

    }
}
