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


namespace gb.mbs.da.services.patient.form
{
    public class SrvHPJ1
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
        string font = "Arial";
        public DataSet Select(gbmodel.patient.Patient p_oPatient, gbmodel.account.Account p_oAccount)
        {

            DataSet ds = new DataSet();
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_CASE_DETAILS", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_oPatient.ID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_oAccount.ID);
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
        public string PatientId(gbmodel.patient.Patient p_oPatient,gbmodel.account.Account p_oAccount)
        {
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("select SZ_PATIENT_ID from MST_CASE_MASTER where SZ_CASE_ID='" + p_oPatient.CaseID + "' and SZ_COMPANY_ID='" + p_oAccount .ID+ "'", sqlCon);
                                             
                SqlDataReader dr;
                dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    p_oPatient.ID = dr["SZ_PATIENT_ID"].ToString();
                }

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

            return p_oPatient.ID;
        }
        public void Create(gbmodel.patient.form.HPJ1 omHPJ1)
        {
            SqlConnection connection = new SqlConnection(sSQLCon);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_save_hpj1_form", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sz_company_id",omHPJ1.Patient.Account.ID);
                cmd.Parameters.AddWithValue("@i_case_id",omHPJ1.Patient.CaseID);
                cmd.Parameters.AddWithValue("@sz_user_id",omHPJ1.User.ID);
                cmd.Parameters.AddWithValue("@sz_wcb_case_number",omHPJ1.Patient.WCBNumber);
                cmd.Parameters.AddWithValue("@sz_wcb_auth_number", omHPJ1.WCBAuthNumber);
                cmd.Parameters.AddWithValue("@sz_insurnce_case_number",omHPJ1.Carrier.CaseNumber);
                cmd.Parameters.AddWithValue("@sz_id_number",omHPJ1.InsuredIdNumber);
                cmd.Parameters.AddWithValue("@sz_injured_occured",omHPJ1.InjuredOccured);
                cmd.Parameters.AddWithValue("@sz_employer",omHPJ1.Employer.Name);
                cmd.Parameters.AddWithValue("@dt_doa",omHPJ1.Patient.DOA);
                cmd.Parameters.AddWithValue("@sz_insurnce_id",omHPJ1.Carrier.Id);
                cmd.Parameters.AddWithValue("@sz_insurnce",omHPJ1.Carrier.Name);
                cmd.Parameters.AddWithValue("@sz_insurnce_addrs",omHPJ1.Carrier.Address.AddressLines);
                cmd.Parameters.AddWithValue("@sz_insurnce_city", omHPJ1.Carrier.Address.City);
                cmd.Parameters.AddWithValue("@sz_insurance_state", omHPJ1.Carrier.Address.State);
                cmd.Parameters.AddWithValue("@sz_insurnce_zip", omHPJ1.Carrier.Address.Zip);
                cmd.Parameters.AddWithValue("@sz_provider_id",omHPJ1.Provider.Id);
                cmd.Parameters.AddWithValue("@sz_provider", omHPJ1.Provider.Name);
                cmd.Parameters.AddWithValue("@sz_provider_addrs", omHPJ1.ProviderAddress);
                cmd.Parameters.AddWithValue("@sz_provider_city", omHPJ1.ProviderCity);
                cmd.Parameters.AddWithValue("@sz_provider_state", omHPJ1.ProviderState);
                cmd.Parameters.AddWithValue("@sz_provider_zip", omHPJ1.ProviderZip);
                cmd.Parameters.AddWithValue("@dt_phy_comp_date",omHPJ1.PhyCompDate);
                cmd.Parameters.AddWithValue("@dt_all_oth_comp_date", omHPJ1.AllOthCompDate);
                cmd.Parameters.AddWithValue("@sz_provider_empty", omHPJ1.ProviderEmpty);
                cmd.Parameters.AddWithValue("@sz_insurance_empty", omHPJ1.CarrierEmpty);
                cmd.Parameters.AddWithValue("@WSignText", omHPJ1.WSignText);
                cmd.Parameters.AddWithValue("@WSignText2", omHPJ1.WSignText2);
                cmd.Parameters.AddWithValue("@StateText", omHPJ1.StateText);
                cmd.Parameters.AddWithValue("@SSText", omHPJ1.SSText);
                cmd.Parameters.AddWithValue("@CountryOf", omHPJ1.CountryOf);
                cmd.Parameters.AddWithValue("@BeingText", omHPJ1.BeingText);
                cmd.Parameters.AddWithValue("@HeistheText", omHPJ1.HeistheText);
                cmd.Parameters.AddWithValue("@DayText", omHPJ1.DayText);
                cmd.Parameters.AddWithValue("@DayOffText", omHPJ1.DayOffText);

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
        public string Print(string sFileName, gbmodel.account.Account p_oAccount, gbmodel.patient.Patient p_oPatient, gbmodel.specialty.Specialty p_ospecialty)
        {
            ArrayList list = new ArrayList();
            ApplicationSettings appSetting = new ApplicationSettings();
            string Outputfile = "";
            try
            {
                DataSet ds = new DataSet();
                ds = select_hpj1_data(p_oAccount, p_oPatient);

                ////  szDestinationDir = CmpName + "/" + szCaseID + "/No Fault File/Medicals/" + speciality + "/" + "Bills/";
                string sNodeStruPath = GetNodeStructure(p_oAccount, p_oPatient, p_ospecialty);
                string sFilePath = appSetting.GetParameterValue("PhysicalBasePath").ToString() + "/" + sNodeStruPath + "/" + sFileName;

                sNodeStruPath = sNodeStruPath.Replace("\"", "/");
                sNodeStruPath = sNodeStruPath.Replace("\\", "/");
                sFilePath = sFilePath.Replace("\"", "/");
                sFilePath = sFilePath.Replace("\\", "/");

                var normalFont = iTextSharp.text.FontFactory.GetFont(font, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                var UnderlineFont = iTextSharp.text.FontFactory.GetFont(font, 8, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK);

                if (ds != null)
                {

                    list.Add(sNodeStruPath);
                    MemoryStream stream = new MemoryStream();
                    //iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 40f, 40f, 1f, 40f);
                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20f, 20f, -50f, 20f);

                    float[] numArray = new float[] { 1f };
                    PdfPTable table = new PdfPTable(numArray);
                    table.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_BASELINE;
                    table.DefaultCell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                    //table.DefaultCell.Colspan = 1;

                    PdfWriter instance = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Print Header Information//

                    float[] numArray2 = new float[] { 2f, 1f, 12f, 1f, 3f };
                    PdfPTable tblHead = new PdfPTable(numArray2);
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHead.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblHead.DefaultCell.HorizontalAlignment = 1;
                    tblHead.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

                    tblHead.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHead.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblHead.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblHead.AddCell(new Phrase("STATE OF NEW YORK - WORKER'S COMPENSATION BOARD" + Environment.NewLine + "" + Environment.NewLine + "Bureau of Health Management" + "" + Environment.NewLine + "" + Environment.NewLine + "Office of Health Provider Administration" + Environment.NewLine + "" + Environment.NewLine + "1-800-781-2362", FontFactory.GetFont(font, 9f, Font.BOLD, Color.BLACK)));
                    tblHead.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblHead.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    table.AddCell(tblHead);

                    // Print Sub header Info- within Border Information

                    float[] numArray3 = new float[] { 1f, 6f, 1f };
                    PdfPTable tblSubHead = new PdfPTable(numArray3);
                    tblSubHead.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblSubHead.DefaultCell.HorizontalAlignment = 1;
                    tblSubHead.DefaultCell.VerticalAlignment = 5;
                    tblSubHead.DefaultCell.Border = 3;
                    tblSubHead.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblSubHead.AddCell(new Phrase("", FontFactory.GetFont("Arial", 8f, Font.ITALIC, Color.BLACK)));
                    tblSubHead.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblSubHead.AddCell(new Phrase("PROVIDER'S REQUEST FOR JUDGEMENT OF AWARD " + Environment.NewLine + "SECTION 54-b,Enforcement on Failure to Pay Award or Judgement", FontFactory.GetFont("Arial", 9f, Font.ITALIC, Color.BLACK)));
                    tblSubHead.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblSubHead.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.ITALIC, Color.BLACK)));
                    table.AddCell(tblSubHead);

                    // Print Text Inforamtion

                    string sText = "Upon issance of an administrative award and/or arbitration decision you must wait at least 30 days before requesting consent for ";
                    sText += "judgement. To Avoid the complications of filling unnecessary requests, waiting 60 days is recommended. The 60 day time period will allow for ";
                    sText += "carriers billing/payment cycles. This form may be used by an authorized workers compensation provider whenever a carrier or self- insured employer";
                    sText += "has not paid for an award or decision(for awards/decision made on after march 13,2007).Section 54-b of workers' Compensation Law provides that in ";
                    sText += "the event an insurance carrier or self - insured employer defaults in the payment of an award made by the board, any party to an award may,";
                    sText += "with the Chair's consent (or the consent of the Chair's designee),file with the County Clerk for the country in which the injured occured or the county in which the carrier or self-insured employer has its principal place of business,";
                    sText += "a certified copy of the decision that awareded compensation.";

                    float[] numText = new float[] { 8f };
                    PdfPTable tblTextInfo = new PdfPTable(numText);
                    tblTextInfo.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblTextInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblTextInfo.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblTextInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextInfo.AddCell(new Phrase(sText, FontFactory.GetFont(font, 9f, Font.NORMAL, Color.BLACK)));
                    table.AddCell(tblTextInfo);
                    // Print Undline Text

                    string sUndText = "Request for Consent and Certified Copy of Unpaid Award or Decision for Medical Care";
                    float[] numUndText = new float[] { 8f };
                    PdfPTable tblUndTextInfo = new PdfPTable(numUndText);
                    tblUndTextInfo.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblUndTextInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblUndTextInfo.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblUndTextInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblUndTextInfo.AddCell(new Phrase(sUndText, FontFactory.GetFont(font, 9f, Font.UNDERLINE, Color.BLACK)));
                    table.AddCell(tblUndTextInfo);

                    //Print Text after Underline
                    string sText2 = "I request consent for judgement and a certified copy of the unpaid award or decision for WCB dispute nymber(s):";
                    float[] numText2 = new float[] { 8f };
                    PdfPTable tblTextInfo2 = new PdfPTable(numText2);
                    tblTextInfo2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblTextInfo2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblTextInfo2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblTextInfo2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextInfo2.AddCell(new Phrase(sText2, FontFactory.GetFont(font, 9f, Font.NORMAL, Color.BLACK)));
                    table.AddCell(tblTextInfo2);


                    //Print Header 
                    string sHeader2 = "ATTACH A COPY OF THE ORIGINAL AWARD(S):";
                    float[] numHead2 = new float[] { 8f };
                    PdfPTable tblHeaderText2 = new PdfPTable(numHead2);
                    tblHeaderText2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblHeaderText2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblHeaderText2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblHeaderText2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderText2.AddCell(new Phrase(sHeader2, FontFactory.GetFont(font, 9f, Font.BOLD, Color.BLACK)));
                    table.AddCell(tblHeaderText2);

                    // Print Empty Information //

                    float[] numArrText = new float[] { 0.5f, 0.1f, 0.5f, 0.1f, 0.5f, 0.1f, 0.5f, 0.1f, 0.5f, 0.1f, 0.5f };
                    PdfPTable tblText = new PdfPTable(numArrText);
                    tblText.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblText.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblText.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblText.DefaultCell.MinimumHeight = 15;
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("A", FontFactory.GetFont(font, 0.4f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("B", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("C", FontFactory.GetFont(font, 0.4f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("D", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("E", FontFactory.GetFont(font, 0.4f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("F", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("G", FontFactory.GetFont(font, 0.4f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("H", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("I", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("J", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblText.AddCell(new Phrase("K", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    tblText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblText.AddCell(new Phrase("L", FontFactory.GetFont(font, 0.1f, 1, Color.BLACK)));
                    table.AddCell(tblText);

                    //Print Provider Information of the table.  

                    float[] numHeadProv = new float[] { 8f, 3f, 3f };
                    PdfPTable tblHeaderProv = new PdfPTable(numHeadProv);
                    tblHeaderProv.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblHeaderProv.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblHeaderProv.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblHeaderProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderProv.AddCell(new Phrase("Name and Address of Health Care Provider", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblHeaderProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderProv.AddCell(new Phrase("WCB Case Number", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblHeaderProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderProv.AddCell(new Phrase("WCB Authorization Number", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    table.AddCell(tblHeaderProv);

                    //Print Provider table

                    float[] numtblProv = new float[] { 1f, 6f, 1f, 3f, 0.5f, 3f };
                    PdfPTable tblTextProv = new PdfPTable(numtblProv);
                    tblTextProv.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblTextProv.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblTextProv.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase("Name 1", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_wcb_case_number"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_wcb_auth_number"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase("2", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider_empty"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("Date of Accident Or Injury", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("Carrier Case Number", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase("Address", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider_addrs"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["dt_doa"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurnce_case_number"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase("City", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    // add state zip

                    float[] numReg1 = new float[] { 1f, 1f, 1f, 1f, 1f };
                    PdfPTable tblRegText1 = new PdfPTable(numReg1);
                    tblRegText1.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblRegText1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblRegText1.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblRegText1.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText1.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider_city"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText1.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText1.AddCell(new Phrase("State", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText1.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText1.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider_state"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText1.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText1.AddCell(new Phrase("Zip Code", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblRegText1.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_provider_zip"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextProv.AddCell(tblRegText1);
                    //--end--//

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("Carrier/Self-Insured Employer I.D Number ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("County in Which Injury Occured", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    // for only Carrier and County Textbox

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));


                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_id_number"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextProv.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextProv.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextProv.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_injured_occured"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    table.AddCell(tblTextProv);

                    //Print Carrier Information of the table.  

                    float[] numHeadCarrier = new float[] { 8f, 6f };
                    PdfPTable tblHeaderCarrier = new PdfPTable(numHeadCarrier);
                    tblHeaderCarrier.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblHeaderCarrier.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblHeaderCarrier.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblHeaderCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderCarrier.AddCell(new Phrase("Name and Address of Carrier/Self-Insured Employer", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblHeaderCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblHeaderCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    table.AddCell(tblHeaderCarrier);

                    //Print carrier table

                    float[] numtblCarrier = new float[] { 1f, 6f, 1f, 6.5f };
                    PdfPTable tblTextCarrier = new PdfPTable(numtblCarrier);
                    tblTextCarrier.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblTextCarrier.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblTextCarrier.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase("Name 1", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurnce"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    //tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase("2", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase("Employer", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    //tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase("Address", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurnce_addrs"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_employer"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    //tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    //tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblTextCarrier.AddCell(new Phrase("City", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    //add state zip columns 

                    float[] numReg = new float[] { 1f, 1f, 1f, 1f, 1f };
                    PdfPTable tblRegText = new PdfPTable(numReg);
                    tblRegText.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblRegText.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblRegText.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblRegText.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurnce_city"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText.AddCell(new Phrase("State", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurance_state"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblRegText.AddCell(new Phrase("Zip Code", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblRegText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblRegText.AddCell(new Phrase(ds.Tables[0].Rows[0]["sz_insurnce_zip"].ToString(), FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.AddCell(tblRegText);

                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;


                    //Print Middle Header i.e -Affirmation of Non-Payment 

                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;



                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblTextCarrier.AddCell(new Phrase(" ", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblTextCarrier.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;


                    table.AddCell(tblTextCarrier);

                    //Print Middle Header i.e -Affirmation of Non-Payment 
                    string sMiHeader = "Affirmation of Non-Payment";
                    float[] numMiHeader = new float[] { 8f };
                    PdfPTable tblMiHeaderText = new PdfPTable(numMiHeader);
                    tblMiHeaderText.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblMiHeaderText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblMiHeaderText.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblMiHeaderText.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblMiHeaderText.AddCell(new Phrase(sMiHeader, FontFactory.GetFont(font, 9f, Font.BOLD, Color.BLACK)));
                    table.AddCell(tblMiHeaderText);

                    // Print Physician Information

                    string pcfinfo = "I state that I am a physician, authorized by law to practice in the State of New York,";
                    pcfinfo += "am not a party to this proceeding, am the physician not remunerated for the above award(s) or decision(s), ";
                    pcfinfo += "have read and know the contents thereof; that the same is true to my knowledge, except as to the matters stated to be on information and belief,";
                    pcfinfo += "and as to those matters I believe it to be true. Affirmed  as true under the penalty of perjury.";

                 
                    string sWText = ds.Tables[0].Rows[0]["WSignText"].ToString();
                    var WSignPhrase = new Phrase();
                    WSignPhrase.Add(new Chunk("Written Signature(Facsimile not Accepted)  ", normalFont));
                    WSignPhrase.Add(new Chunk(sWText, UnderlineFont));
                    WSignPhrase.Add(new Chunk(" Date ", normalFont));
                    WSignPhrase.Add(new Chunk(ds.Tables[0].Rows[0]["dt_phy_comp_date"].ToString(), UnderlineFont));


                    float[] numpcf = new float[] { 8f };
                    PdfPTable tblpcf = new PdfPTable(numpcf);
                    tblpcf.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblpcf.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblpcf.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblpcf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.ITALIC, Color.BLACK)));
                    tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                    tblpcf.AddCell(new Phrase("PHYSICIANS COMPLETE THE FOLLOWING: ", FontFactory.GetFont(font, 9f, Font.UNDERLINE, Color.BLACK)));
                    tblpcf.DefaultCell.Border = tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblpcf.AddCell(new Phrase(pcfinfo, FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblpcf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    //tblpcf.AddCell(new Phrase("Written Signature(Facsimile not Accepted)" + new Phrase(sWText, FontFactory.GetFont(font, 9f, Font.UNDERLINE, Color.BLACK)) + "Date " + ds.Tables[0].Rows[0]["dt_phy_comp_date"].ToString() + "", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblpcf.AddCell(new Phrase(WSignPhrase));
                    tblpcf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblpcf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    table.AddCell(tblpcf);


                    //Print Complete The Following Info

                    string pctfinfo = "I state that I am a chiropractor, authorized hospital representative, physical or ";
                    pctfinfo += "occupational therapist, podiatrist or psychologist, authorized by law to practice in the  ";
                    pctfinfo += "State of New York and/or authorized to represent a hospital, am not a party to this ";
                    pctfinfo += "proceeding, am the provider or representative of a hospital not remunerated for the above";
                    pctfinfo += "award(s) or decision(s), have read and know the contents thereof; that the same is true to my knowledge, except as to";
                    pctfinfo += "the matters stated to be on information and belief, and as to those matters I believe it to be true. Affirmed as true under the penalty of perjury.";

                    string sWText2 = ds.Tables[0].Rows[0]["WSignText2"].ToString();
                    var WSignPhrase2 = new Phrase();
                    WSignPhrase2.Add(new Chunk("Written Signature(Facsimile not Accepted)  ", normalFont));
                    WSignPhrase2.Add(new Chunk(sWText2, UnderlineFont));
                    WSignPhrase2.Add(new Chunk(" Date ", normalFont));
                    WSignPhrase2.Add(new Chunk(ds.Tables[0].Rows[0]["dt_all_oth_comp_date"].ToString(), UnderlineFont));


                    string sWCountryOf = ds.Tables[0].Rows[0]["CountryOf"].ToString();
                    string sWBeingText = ds.Tables[0].Rows[0]["BeingText"].ToString();

                    var WCountryPhrase = new Phrase();
                    WCountryPhrase.Add(new Chunk("County of   ", normalFont));
                    WCountryPhrase.Add(new Chunk(sWCountryOf, UnderlineFont));
                    WCountryPhrase.Add(new Chunk(" ) ", normalFont));
                    WCountryPhrase.Add(new Chunk(sWBeingText, UnderlineFont));
                    WCountryPhrase.Add(new Chunk(" being duly sworn, deposes and says:", normalFont));


                    string pcdotline = ".....................................................................................................................................";
                    pcdotline += "................................................................................................................";
                    float[] numctf = new float[] { 8f };
                    PdfPTable tblctf = new PdfPTable(numctf);
                    tblctf.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblctf.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblctf.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblctf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.ITALIC, Color.BLACK)));
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                    tblctf.AddCell(new Phrase("ALL OTHERS COMPLETE THE FOLLOWING:", FontFactory.GetFont(font, 9f, Font.UNDERLINE, Color.BLACK)));
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblctf.AddCell(new Phrase("IMPORTANT:  BY LAW THOSE COMPLETING THIS SECTION MUST BE SWORN TO BEFORE A NOTARY PUBLIC.", FontFactory.GetFont(font, 9f, Font.BOLD, Color.BLACK)));
                    tblctf.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblctf.DefaultCell.Border = tblpcf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.AddCell(new Phrase(pctfinfo, FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                   // tblctf.AddCell(new Phrase("Written Signature(Facsimile not Accepted) "+ ds.Tables[0].Rows[0]["WSignText2"].ToString() +" Date " + ds.Tables[0].Rows[0]["dt_all_oth_comp_date"].ToString() + "", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase(WSignPhrase2));

                    tblctf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase(pcdotline, FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.AddCell(new Phrase("State of New York" + ds.Tables[0].Rows[0]["StateText"].ToString() + ") ss:" + ds.Tables[0].Rows[0]["SSText"].ToString() + ",", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    //tblctf.AddCell(new Phrase("County of " + ds.Tables[0].Rows[0]["CountryOf"].ToString() + ")" + ds.Tables[0].Rows[0]["BeingText"].ToString() + "being duly sworn, deposes and says:", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase(WCountryPhrase));

                    string sWHeText = ds.Tables[0].Rows[0]["HeistheText"].ToString();
                    var WThePhrase = new Phrase();
                    WThePhrase.Add(new Chunk("That (s) he is the   ", normalFont));
                    WThePhrase.Add(new Chunk(sWHeText, UnderlineFont));
                    WThePhrase.Add(new Chunk("  duly licensed in the State of New York and/or authorized to represent a hospital, who has not been remunerated for  the above award(s) or decision(s), and that (s)he has read the same and knows the contents thereof; that the same is true to the knowledge of the deponent, except as to the matters stated to be on information and belief, and as to those matters (s)he believes it to be true. ", normalFont));
                  


                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    //tblctf.AddCell(new Phrase("That (s)he is the " + ds.Tables[0].Rows[0]["HeistheText"].ToString() + " duly licensed in the State of New York and/or authorized to represent a hospital, who has not been remunerated for  the above award(s) or decision(s), and that (s)he has read the same and knows the contents thereof; that the same is true to the knowledge of the deponent, except as to the matters stated to be on information and belief, and as to those matters (s)he believes it to be true.", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase(WThePhrase));

                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.AddCell(new Phrase("Subscribed and sworn before me this", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    string sWDay = ds.Tables[0].Rows[0]["DayText"].ToString();
                    string sWDayText = ds.Tables[0].Rows[0]["DayOffText"].ToString();
                    var WDayPhrase = new Phrase();
                    WDayPhrase.Add(new Chunk(sWDay, UnderlineFont));
                    WDayPhrase.Add(new Chunk(" day of  ", normalFont));
                    WDayPhrase.Add(new Chunk(sWDayText, UnderlineFont));
                  

                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    //tblctf.AddCell(new Phrase("" + ds.Tables[0].Rows[0]["DayText"].ToString() + " day of " + ds.Tables[0].Rows[0]["DayOffText"].ToString() + ",", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.AddCell(new Phrase(WDayPhrase));

                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    tblctf.AddCell(new Phrase("                                                                                                                                                                     (Signature of  Notary Public)", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));
                    tblctf.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    tblctf.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, Font.NORMAL, Color.BLACK)));

                    table.AddCell(tblctf);

                    // Print Bottom Info- 

                    float[] numArrBottom = new float[] { 2f, 1f, 12f, 1f, 3f };
                    PdfPTable tblBottom = new PdfPTable(numArrBottom);
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblBottom.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    tblBottom.DefaultCell.HorizontalAlignment = 1;
                    tblBottom.DefaultCell.VerticalAlignment = 2;
                    tblBottom.DefaultCell.Border = 2;
                    tblBottom.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblBottom.AddCell(new Phrase("HP - J1 (7-08)", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblBottom.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblBottom.AddCell(new Phrase("Mail Completed form to : Workers' Compensation Board" + Environment.NewLine + "" + Environment.NewLine + "Office of Health Provider Administartion" + "" + Environment.NewLine + "" + Environment.NewLine + "100 Broadway - Menands" + Environment.NewLine + "" + Environment.NewLine + "Albany,NY 12241", FontFactory.GetFont(font, 8f, Font.BOLD, Color.BLACK)));
                    tblBottom.AddCell(new Phrase("", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    tblBottom.AddCell(new Phrase("www.wcb.state.ny.us", FontFactory.GetFont(font, 8f, 1, Color.BLACK)));
                    table.AddCell(tblBottom);

                    table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - tblHead.TotalHeight) - 1f, instance.DirectContent);
                    document.Close();


                    if (!Directory.Exists(appSetting.GetParameterValue("PhysicalBasePath").ToString() + sNodeStruPath))
                    {
                        Directory.CreateDirectory(appSetting.GetParameterValue("PhysicalBasePath").ToString() + sNodeStruPath);
                    }

                    System.IO.File.WriteAllBytes(sFilePath, stream.GetBuffer());
                }

                Outputfile = appSetting.GetParameterValue("DocumentManagerURL").ToString() + sNodeStruPath +"/"+ sFileName;
                return Outputfile;
                
            }
            catch (Exception exception)
            {
                throw exception;
               
            }
        }

        public DataSet select_hpj1_data(gbmodel.account.Account p_oAccount, gbmodel.patient.Patient p_oPatient)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(sSQLCon);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_hpj1pdf", connection);
                selectCommand.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                selectCommand.Parameters.AddWithValue("@i_case_id", p_oPatient.CaseID);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                new SqlDataAdapter(selectCommand).Fill(dataSet);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dataSet;
        }

        public string GetNodeStructure(gbmodel.account.Account p_oAccount, gbmodel.patient.Patient p_oPatient, gbmodel.specialty.Specialty oSpecialty)
        {
            string str;
            SqlConnection connection = new SqlConnection(sSQLCon);
            try
            {
                try
                {

                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SP_Find_HP1_Path", connection);
                    sqlCommand.Parameters.AddWithValue("@sz_CompanyID", p_oAccount.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_CaseID", p_oPatient.CaseID);
                    sqlCommand.Parameters.AddWithValue("@sz_speciality", oSpecialty.Name);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    str = Convert.ToString(sqlCommand.ExecuteScalar());
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return str;
        }

        private static iTextSharp.text.Font CreateFont(int size, int style = iTextSharp.text.Font.UNDERLINE)
        {
            return new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, size, style);
        }
    }
}
