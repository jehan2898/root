using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using gbmodel = gb.mbs.da.model;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;

namespace gb.mbs.da.services.patient.note
{
    public class SrvACNote : SrvNote
    {
        private List<gbmodel.bill.Bill> c_lstBill;
        
        public SrvACNote(List<gbmodel.bill.Bill> p_lstBill)
        {
            this.c_lstBill = p_lstBill;
        }

        public override void PrintNote()
        {
            string c_basepath = System.Configuration.ConfigurationManager.AppSettings["BASEPATH"].ToString(); 
            ArrayList arrayLists = new ArrayList();

            List<gbmodel.patient.note.Note> lstNote = Select(c_lstBill, gbmodel.patient.note.EnumNoteType.AC);

            for (int i = 0; i < lstNote.Count; i++)
            {
                gbmodel.patient.note.ACNote oAcNote = new gbmodel.patient.note.ACNote();
                oAcNote = (gbmodel.patient.note.ACNote)lstNote[i];

                string s_pdfname = string.Concat(new object[] { "FUReport_", this.c_lstBill[i].Number, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf" });
                c_basepath = c_basepath + "/" + s_pdfname;

                DataSet dsNote = new DataSet();
                dsNote = GetAcNotesInfo(this.c_lstBill[i].Number, this.c_lstBill[i].Patient.Account.ID);

                MemoryStream memoryStream = new MemoryStream();
                FileStream fileStream = new FileStream(c_basepath, FileMode.OpenOrCreate);
                Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
                PdfWriter.GetInstance(document, fileStream);
                document.Open();
                float[] singleArray = new float[] { 6f };
                PdfPTable pdfPTable = new PdfPTable(singleArray);
                pdfPTable.DefaultCell.Border = 0;
                pdfPTable.WidthPercentage = 100f;
                singleArray = new float[] { 4f };
                PdfPTable pdfPTable1 = new PdfPTable(singleArray);
                pdfPTable1.DefaultCell.Border = 0;
                pdfPTable1.WidthPercentage = 100f;

                pdfPTable1 = this.generateACNotes(oAcNote, dsNote, oAcNote.EventId);
                pdfPTable.AddCell(pdfPTable1);

                document.Add(pdfPTable);
                document.Close();
            }

        }

        private PdfPTable generateACNotes(gbmodel.patient.note.ACNote objDAO, DataSet Dset, string EID)
        {
            SqlDataAdapter sqlDataAdapter;
            SqlConnection sqlConnection;
            SqlCommand sqlCommand;
            DataSet dataSet;
            SqlCommand sqlCommand1;
            SqlDataAdapter sqlDataAdapter1;
            DataSet dataSet1;
            string str;
            string str1;
            int j;
            string str2;
            int k;
            string[] strArrays;
            string str3 = "Arial";
            int num = 9;
            int num1 = 10;
            int num2 = 7;

            string str4 = System.Configuration.ConfigurationManager.AppSettings["CHECKBOXPATH"].ToString();
            string str5 = System.Configuration.ConfigurationManager.AppSettings["UNCHECKBOXPATH"].ToString();
            string str6 = System.Configuration.ConfigurationManager.AppSettings["CHECKRADIO"].ToString();
            string str7 = System.Configuration.ConfigurationManager.AppSettings["UNCHECKRADIO"].ToString();

            string str8 = objDAO.DoctorSign.Replace("\\", "/");
            string str9 = objDAO.PatientSign.Replace("\\", "/");
            Image instance = Image.GetInstance(str8);
            instance.ScaleAbsolute(70f, 15f);
            PdfPCell pdfPCell = new PdfPCell(instance);
            pdfPCell.Border = 0;
            pdfPCell.HorizontalAlignment = 1;
            pdfPCell.FixedHeight = 15f;
            Image image = Image.GetInstance(str9);
            image.ScaleAbsolute(70f, 15f);
            PdfPCell pdfPCell1 = new PdfPCell(image);
            pdfPCell1.Border = 0;
            pdfPCell1.HorizontalAlignment = 1;
            pdfPCell1.FixedHeight = 15f;
            Image instance1 = Image.GetInstance(str4);
            instance1.ScaleAbsolute(10f, 10f);
            PdfPCell pdfPCell2 = new PdfPCell(instance1);
            pdfPCell2.Border = 0;
            pdfPCell2.HorizontalAlignment = 1;
            pdfPCell2.FixedHeight = 10f;
            pdfPCell2.PaddingTop = 0.55f;
            Image image1 = Image.GetInstance(str5);
            image1.ScaleAbsolute(10f, 10f);
            PdfPCell pdfPCell3 = new PdfPCell(image1);
            pdfPCell3.Border = 0;
            pdfPCell3.HorizontalAlignment = 1;
            pdfPCell3.FixedHeight = 10f;
            pdfPCell3.PaddingTop = 0.55f;
            Image instance2 = Image.GetInstance(str6);
            instance2.ScaleAbsolute(10f, 10f);
            PdfPCell pdfPCell4 = new PdfPCell(instance2);
            pdfPCell4.Border = 0;
            pdfPCell4.HorizontalAlignment = 1;
            pdfPCell4.FixedHeight = 10f;
            Image image2 = Image.GetInstance(str7);
            image2.ScaleAbsolute(10f, 10f);
            PdfPCell pdfPCell5 = new PdfPCell(image2);
            pdfPCell5.Border = 0;
            pdfPCell5.HorizontalAlignment = 1;
            pdfPCell5.FixedHeight = 10f;
            float[] singleArray = new float[] { 4f };
            PdfPTable pdfPTable = new PdfPTable(singleArray);
            pdfPTable.DefaultCell.Border = 0;
            pdfPTable.WidthPercentage = 100f;
            PdfPTable pdfPTable1 = new PdfPTable(new float[] { 1.8f, 10f, 1.5f });
            pdfPTable1.DefaultCell.Border = 0;
            pdfPTable1.WidthPercentage = 100f;
            pdfPTable1.DefaultCell.HorizontalAlignment = 1;
            pdfPTable1.AddCell("");
            pdfPTable1.AddCell("");
            pdfPTable1.AddCell("");
            pdfPTable.AddCell(pdfPTable1);
            pdfPTable.DefaultCell.HorizontalAlignment = 1;
            pdfPTable.AddCell(new Phrase("AC NOTES", FontFactory.GetFont(str3, (float)num1, 1, Color.BLACK)));
            pdfPTable.DefaultCell.Border = 0;
            pdfPTable.DefaultCell.HorizontalAlignment = 0;
            singleArray = new float[] { 1f };
            PdfPTable pdfPTable2 = new PdfPTable(singleArray);
            pdfPTable2.DefaultCell.Border = 15;
            pdfPTable2.WidthPercentage = 100f;
            pdfPTable2.DefaultCell.HorizontalAlignment = 1;
            PdfPTable pdfPTable3 = new PdfPTable(new float[] { 0.5f, 0.25f, 1.3f, 0.6f, 0.25f, 1.3f });
            pdfPTable3.DefaultCell.Border = 15;
            pdfPTable3.WidthPercentage = 100f;
            pdfPTable3.DefaultCell.HorizontalAlignment = 0;
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Patient Name", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.Patient.Name, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Case #", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(Convert.ToString(objDAO.Patient.CaseNo), FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Date of Accident", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.Patient.DOA, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Insurance Company", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.Carrier.Name, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Claim Number", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.Patient.ClaimNumber, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("Date", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.Date, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));

            pdfPTable3.AddCell(new Phrase("Doctor Name", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("-", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase(objDAO.DoctorName, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));

            pdfPTable3.AddCell(new Phrase("", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable3.DefaultCell.Border = 0;
            pdfPTable3.AddCell(new Phrase("", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));

            pdfPTable2.AddCell(pdfPTable3);
            singleArray = new float[] { 0.7f, 1.3f };
            PdfPTable pdfPTable4 = new PdfPTable(singleArray);
            pdfPTable4.DefaultCell.Border = 0;
            pdfPTable4.WidthPercentage = 100f;
            pdfPTable4.DefaultCell.HorizontalAlignment = 0;
            pdfPTable4.AddCell(new Phrase("Patient reported the following information:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable4.AddCell("");
            pdfPTable2.DefaultCell.Border = 13;
            pdfPTable2.AddCell(pdfPTable4);
            PdfPTable pdfPTable5 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f });
            pdfPTable5.DefaultCell.Border = 15;
            pdfPTable5.WidthPercentage = 100f;
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable6 = new PdfPTable(singleArray);
            pdfPTable6.DefaultCell.Border = 0;
            pdfPTable6.WidthPercentage = 100f;
            pdfPTable6.DefaultCell.Border = 15;
            if (!(objDAO.PatientReported.ToString() == "0"))
            {
                pdfPTable6.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable6.AddCell(pdfPCell4);
            }
            pdfPTable6.DefaultCell.Border = 0;
            pdfPTable6.AddCell(new Phrase("No Pain", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable6.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable6);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable7 = new PdfPTable(singleArray);
            pdfPTable7.DefaultCell.Border = 0;
            pdfPTable7.WidthPercentage = 100f;
            pdfPTable7.DefaultCell.Border = 15;
            if (!(objDAO.PatientReported.ToString() == "1"))
            {
                pdfPTable7.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable7.AddCell(pdfPCell4);
            }
            pdfPTable7.DefaultCell.Border = 0;
            pdfPTable7.AddCell(new Phrase("Pain", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable7.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable7);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable8 = new PdfPTable(singleArray);
            pdfPTable8.DefaultCell.Border = 0;
            pdfPTable8.WidthPercentage = 100f;
            pdfPTable8.DefaultCell.Border = 15;
            if (!(objDAO.PatientReported.ToString() == "2"))
            {
                pdfPTable8.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable8.AddCell(pdfPCell4);
            }
            pdfPTable8.DefaultCell.Border = 0;
            pdfPTable8.AddCell(new Phrase("Fincreased", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable8.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable8);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable9 = new PdfPTable(singleArray);
            pdfPTable9.DefaultCell.Border = 0;
            pdfPTable9.WidthPercentage = 100f;
            pdfPTable9.DefaultCell.Border = 15;
            if (!(objDAO.PatientReported.ToString() == "3"))
            {
                pdfPTable9.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable9.AddCell(pdfPCell4);
            }
            pdfPTable9.DefaultCell.Border = 0;
            pdfPTable9.AddCell(new Phrase("Decreased", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable9.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable9);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable10 = new PdfPTable(singleArray);
            pdfPTable10.DefaultCell.Border = 0;
            pdfPTable10.WidthPercentage = 100f;
            pdfPTable10.DefaultCell.Border = 15;
            if (!(objDAO.PatientReported.ToString() == "4"))
            {
                pdfPTable10.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable10.AddCell(pdfPCell4);
            }
            pdfPTable10.DefaultCell.Border = 0;
            pdfPTable10.AddCell(new Phrase("No Changes", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable10.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable10);
            pdfPTable5.DefaultCell.HorizontalAlignment = 0;
            pdfPTable5.AddCell(new Phrase("Patient was treated:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable11 = new PdfPTable(singleArray);
            pdfPTable11.DefaultCell.Border = 0;
            pdfPTable11.WidthPercentage = 100f;
            pdfPTable11.DefaultCell.Border = 15;
            if (!(objDAO.PatientTreatedAcupuncture.ToString() == "1"))
            {
                pdfPTable11.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable11.AddCell(pdfPCell2);
            }
            pdfPTable11.DefaultCell.Border = 0;
            pdfPTable11.AddCell(new Phrase("Acupuncture", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable11.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable11);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable12 = new PdfPTable(singleArray);
            pdfPTable12.DefaultCell.Border = 0;
            pdfPTable12.WidthPercentage = 100f;
            pdfPTable12.DefaultCell.Border = 15;
            if (!(objDAO.PatientTreatedElectro.ToString() == "1"))
            {
                pdfPTable12.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable12.AddCell(pdfPCell2);
            }
            pdfPTable12.DefaultCell.Border = 0;
            pdfPTable12.AddCell(new Phrase("Electro-Acupuncture", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable12.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable12);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable13 = new PdfPTable(singleArray);
            pdfPTable13.DefaultCell.Border = 0;
            pdfPTable13.WidthPercentage = 100f;
            pdfPTable13.DefaultCell.Border = 15;
            if (!(objDAO.PatientTreatedMoxa.ToString() == "1"))
            {
                pdfPTable13.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable13.AddCell(pdfPCell2);
            }
            pdfPTable13.DefaultCell.Border = 0;
            pdfPTable13.AddCell(new Phrase("Moxa", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable13.DefaultCell.Border = 0;
            pdfPTable5.AddCell(pdfPTable13);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable14 = new PdfPTable(singleArray);
            pdfPTable14.DefaultCell.Border = 0;
            pdfPTable14.WidthPercentage = 100f;
            pdfPTable14.DefaultCell.Border = 15;
            if (!(objDAO.PatientTreatedCupping.ToString() == "1"))
            {
                pdfPTable14.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable14.AddCell(pdfPCell2);
            }
            pdfPTable14.DefaultCell.Border = 0;
            pdfPTable14.AddCell(new Phrase("Cupping", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable14.DefaultCell.Border = 0;
            pdfPTable2.DefaultCell.Border = 12;
            pdfPTable5.AddCell(pdfPTable14);
            pdfPTable2.AddCell(pdfPTable5);
            singleArray = new float[] { 0.38f, 0.63f };
            PdfPTable pdfPTable15 = new PdfPTable(singleArray);
            pdfPTable15.DefaultCell.Border = 15;
            pdfPTable15.WidthPercentage = 100f;
            PdfPTable pdfPTable16 = new PdfPTable(new float[] { 0.32f, 0.15f, 0.53f });
            pdfPTable16.DefaultCell.Border = 0;
            pdfPTable16.WidthPercentage = 100f;
            pdfPTable16.AddCell(new Phrase("Pain Grades:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable16.DefaultCell.Border = 8;
            pdfPTable16.AddCell("");
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable17 = new PdfPTable(singleArray);
            pdfPTable17.DefaultCell.Border = 0;
            pdfPTable17.WidthPercentage = 100f;
            pdfPTable17.DefaultCell.Border = 15;
            if (!(objDAO.PainGrades.ToString() == "0"))
            {
                pdfPTable17.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable17.AddCell(pdfPCell4);
            }
            pdfPTable17.DefaultCell.Border = 0;
            pdfPTable17.AddCell(new Phrase("Mild", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable17.DefaultCell.Border = 0;
            pdfPTable16.DefaultCell.Border = 0;
            pdfPTable16.AddCell(pdfPTable17);
            pdfPTable15.AddCell(pdfPTable16);
            PdfPTable pdfPTable18 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f });
            pdfPTable18.DefaultCell.Border = 0;
            pdfPTable18.WidthPercentage = 100f;
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable19 = new PdfPTable(singleArray);
            pdfPTable19.DefaultCell.Border = 0;
            pdfPTable19.WidthPercentage = 100f;
            pdfPTable19.DefaultCell.Border = 15;
            if (!(objDAO.PainGrades.ToString() == "1"))
            {
                pdfPTable19.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable19.AddCell(pdfPCell4);
            }
            pdfPTable19.DefaultCell.Border = 0;
            pdfPTable19.AddCell(new Phrase("Moderate", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable18.DefaultCell.Border = 8;
            pdfPTable18.AddCell(pdfPTable19);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable20 = new PdfPTable(singleArray);
            pdfPTable20.DefaultCell.Border = 0;
            pdfPTable20.WidthPercentage = 100f;
            pdfPTable20.DefaultCell.Border = 15;
            if (!(objDAO.PainGrades.ToString() == "2"))
            {
                pdfPTable20.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable20.AddCell(pdfPCell4);
            }
            pdfPTable20.DefaultCell.Border = 0;
            pdfPTable20.AddCell(new Phrase("Sharp", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable20.DefaultCell.Border = 0;
            pdfPTable18.DefaultCell.Border = 8;
            pdfPTable18.AddCell(pdfPTable20);
            singleArray = new float[] { 0.25f, 1.75f };
            float[] singleArray1 = singleArray;
            PdfPTable pdfPTable21 = new PdfPTable(singleArray1);
            pdfPTable21.DefaultCell.Border = 0;
            pdfPTable21.WidthPercentage = 100f;
            pdfPTable21.DefaultCell.Border = 15;
            if (!(objDAO.PainGrades.ToString() == "3"))
            {
                pdfPTable21.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable21.AddCell(pdfPCell4);
            }
            pdfPTable21.DefaultCell.Border = 0;
            pdfPTable21.AddCell(new Phrase("Dull", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable18.DefaultCell.Border = 8;
            pdfPTable18.AddCell(pdfPTable21);
            singleArray = new float[] { 0.25f, 1.75f };
            PdfPTable pdfPTable22 = new PdfPTable(singleArray1);
            pdfPTable22.DefaultCell.Border = 0;
            pdfPTable22.WidthPercentage = 100f;
            pdfPTable22.DefaultCell.Border = 15;
            if (!(objDAO.PainGrades.ToString() == "4"))
            {
                pdfPTable22.AddCell(pdfPCell5);
            }
            else
            {
                pdfPTable22.AddCell(pdfPCell4);
            }
            pdfPTable22.DefaultCell.Border = 0;
            pdfPTable22.AddCell(new Phrase("Severe Constant", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable18.DefaultCell.Border = 0;
            pdfPTable18.AddCell(pdfPTable22);
            pdfPTable15.AddCell(pdfPTable18);
            singleArray = new float[] { 1.35f, 0.65f };
            PdfPTable pdfPTable23 = new PdfPTable(singleArray);
            pdfPTable23.DefaultCell.Border = 0;
            pdfPTable23.WidthPercentage = 100f;
            pdfPTable23.AddCell(new Phrase("Intermittent/occasional pain:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable23.AddCell("");
            pdfPTable15.DefaultCell.Border = 15;
            pdfPTable15.AddCell(pdfPTable23);
            PdfPTable pdfPTable24 = new PdfPTable(new float[] { 0.15f, 0.3f, 0.15f, 0.3f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.3f, 0.15f, 0.4f, 0.15f, 0.6f });
            pdfPTable24.DefaultCell.Border = 0;
            pdfPTable24.WidthPercentage = 100f;
            if (!(objDAO.Head.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("Head", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.Neck.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("Neck", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.Thoracic.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("Thoracic", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.Lumbar.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("Lumbar", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.RLShoulder.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("R/L Sh", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.RLWrist.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("R/L Wrist", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.RLElbow.ToString() == "1"))
            {
                pdfPTable24.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable24.AddCell(pdfPCell2);
            }
            pdfPTable24.AddCell(new Phrase("R/L Elbow", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable15.DefaultCell.Border = 13;
            pdfPTable15.AddCell(pdfPTable24);
            pdfPTable15.DefaultCell.Border = 15;
            pdfPTable15.AddCell(new Phrase(" ", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            PdfPTable pdfPTable25 = new PdfPTable(new float[] { 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.2f, 0.15f, 0.3f, 0.15f, 0.4f, 0.15f, 0.6f });
            pdfPTable25.DefaultCell.Border = 0;
            pdfPTable25.WidthPercentage = 100f;
            if (!(objDAO.RLHip.ToString() == "1"))
            {
                pdfPTable25.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable25.AddCell(pdfPCell2);
            }
            pdfPTable25.AddCell(new Phrase("R/L Hip", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.RLRLKnee.ToString() == "1"))
            {
                pdfPTable25.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable25.AddCell(pdfPCell2);
            }
            pdfPTable25.AddCell(new Phrase("R/L Knee", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.RLRLAnkle.ToString() == "1"))
            {
                pdfPTable25.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable25.AddCell(pdfPCell2);
            }
            pdfPTable25.AddCell(new Phrase("R/L Ankle", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable25.AddCell("");
            pdfPTable15.DefaultCell.Border = 14;
            pdfPTable15.AddCell(pdfPTable25);
            pdfPTable2.AddCell(pdfPTable15);
            PdfPTable pdfPTable26 = new PdfPTable(new float[] { 1f, 1f, 1f });
            pdfPTable26.DefaultCell.Border = 0;
            pdfPTable26.WidthPercentage = 100f;
            singleArray = new float[] { 0.7f, 1.3f };
            PdfPTable pdfPTable27 = new PdfPTable(singleArray);
            pdfPTable27.DefaultCell.Border = 0;
            pdfPTable27.WidthPercentage = 100f;
            pdfPTable27.AddCell(new Phrase("Objective:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable27.AddCell("");
            pdfPTable26.DefaultCell.Border = 8;
            pdfPTable26.AddCell(pdfPTable27);
            singleArray = new float[] { 0.8f, 1.2f };
            PdfPTable pdfPTable28 = new PdfPTable(singleArray);
            pdfPTable28.DefaultCell.Border = 0;
            pdfPTable28.WidthPercentage = 100f;
            pdfPTable28.AddCell(new Phrase("Assessment:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable28.AddCell("");
            pdfPTable26.DefaultCell.Border = 8;
            pdfPTable26.AddCell(pdfPTable28);
            singleArray = new float[] { 0.4f, 1.6f };
            PdfPTable pdfPTable29 = new PdfPTable(singleArray);
            pdfPTable29.DefaultCell.Border = 0;
            pdfPTable29.WidthPercentage = 100f;
            pdfPTable29.DefaultCell.Border = 0;
            pdfPTable29.AddCell(new Phrase("Notes:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable29.DefaultCell.Border = 0;
            pdfPTable29.AddCell("");
            pdfPTable26.DefaultCell.Border = 0;
            pdfPTable26.AddCell(pdfPTable29);
            pdfPTable2.DefaultCell.Border = 15;
            pdfPTable2.AddCell(pdfPTable26);
            PdfPTable pdfPTable30 = new PdfPTable(new float[] { 1f, 1f, 1f });
            pdfPTable30.DefaultCell.Border = 0;
            pdfPTable30.WidthPercentage = 100f;
            singleArray = new float[] { 0.1f, 1.1f };
            PdfPTable pdfPTable31 = new PdfPTable(singleArray);
            pdfPTable31.DefaultCell.Border = 0;
            pdfPTable31.WidthPercentage = 100f;
            if (!(objDAO.PatientStates.ToString() == "1"))
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell2);
            }
            pdfPTable31.AddCell(new Phrase("Patient states condition is the same", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.PatientStates1.ToString() == "1"))
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell2);
            }
            pdfPTable31.AddCell(new Phrase("Patient states little improvement in condition", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            if (!(objDAO.PatientStates2.ToString() == "1"))
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell3);
            }
            else
            {
                pdfPTable31.HorizontalAlignment = 0;
                pdfPTable31.AddCell(pdfPCell2);
            }
            pdfPTable31.AddCell(new Phrase("Patient states much improvement in condition", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable30.DefaultCell.Border = 8;
            pdfPTable30.AddCell(pdfPTable31);
            singleArray = new float[] { 0.2f, 1f };
            PdfPTable pdfPTable32 = new PdfPTable(singleArray);
            pdfPTable32.DefaultCell.Border = 15;
            pdfPTable32.WidthPercentage = 100f;
            if (!(objDAO.ChkPatientTolerated.ToString() == "1"))
            {
                pdfPCell3.Border = 15;
                pdfPTable32.HorizontalAlignment = 0;
                pdfPTable32.AddCell(pdfPCell3);
                pdfPCell3.Border = 0;
            }
            else
            {
                pdfPCell2.Border = 15;
                pdfPTable32.HorizontalAlignment = 0;
                pdfPTable32.AddCell(pdfPCell2);
                pdfPCell2.Border = 0;
            }
            pdfPTable32.AddCell(new Phrase("Patient tolerated treatment well", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable32.AddCell(new Phrase("Plan:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable32.AddCell("");
            if (!(objDAO.Continue.ToString() == "1"))
            {
                pdfPCell3.Border = 15;
                pdfPTable32.HorizontalAlignment = 0;
                pdfPTable32.AddCell(pdfPCell3);
                pdfPCell3.Border = 0;
            }
            else
            {
                pdfPCell2.Border = 15;
                pdfPTable32.HorizontalAlignment = 0;
                pdfPTable32.AddCell(pdfPCell2);
                pdfPCell2.Border = 0;
            }
            pdfPTable32.AddCell(new Phrase("Continue with present therapy", FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable30.AddCell(pdfPTable32);
            pdfPTable30.DefaultCell.Border = 0;
            pdfPTable30.AddCell(new Phrase(objDAO.Notes, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable2.AddCell(pdfPTable30);
            singleArray = new float[] { 1f };
            PdfPTable pdfPTable33 = new PdfPTable(singleArray);
            pdfPTable33.DefaultCell.Border = 0;
            pdfPTable33.WidthPercentage = 100f;
            pdfPTable33.AddCell(new Phrase("Acupuncture  Points Used:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable33.AddCell(new Phrase(objDAO.Acupuncture, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
            pdfPTable2.AddCell(pdfPTable33);
            singleArray = new float[] { 1f };
            PdfPTable pdfPTable34 = new PdfPTable(singleArray);
            pdfPTable34.DefaultCell.Border = 0;
            pdfPTable34.WidthPercentage = 100f;
            pdfPTable34.HorizontalAlignment = 0;
            pdfPTable34.AddCell(pdfPCell2);
            pdfPTable34.AddCell(new Phrase("Code:", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable2.AddCell(pdfPTable34);
            string str10 = "";
            PdfPTable pdfPTable35 = new PdfPTable(new float[] { 1.6f, 0.15f, 0.3f, 0.15f, 0.3f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f, 0.15f, 0.4f });
            for (int i = 0; i < Dset.Tables[0].Rows.Count; i++)
            {
                if ((EID == "" ? true : EID == Dset.Tables[0].Rows[i][1].ToString()))
                {
                    if (!(str10 == ""))
                    {
                        //sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
                        sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                        strArrays = new string[] { "SELECT i_body_part FROM txn_procedure_code_with_bodypart WHERE i_event_id='", Dset.Tables[0].Rows[i][1].ToString(), "' AND sz_procedure_code_id='", Dset.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString(), "'" };
                        sqlCommand = new SqlCommand(string.Concat(strArrays), sqlConnection)
                        {
                            CommandTimeout = 0
                        };
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        dataSet = new DataSet();
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter.Fill(dataSet);
                        sqlCommand1 = new SqlCommand("SELECT i_id,sz_body_part FROM mst_accu_bodypart where i_id!=11", sqlConnection);
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
                        dataSet1 = new DataSet();
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter1.Fill(dataSet1);
                        str = "";
                        str1 = "";
                        str10 = string.Concat(Dset.Tables[0].Rows[i]["CODE"].ToString(), "-", Dset.Tables[0].Rows[i]["DESCRIPTION"].ToString());
                        pdfPTable35.DefaultCell.Border = 15;
                        pdfPTable35.WidthPercentage = 100f;
                        pdfPTable35.HorizontalAlignment = 0;
                        pdfPTable35.AddCell(new Phrase(str10, FontFactory.GetFont(str3, (float)num2, 1, Color.BLACK)));
                        for (j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                        {
                            str = dataSet1.Tables[0].Rows[j]["sz_body_part"].ToString();
                            str1 = dataSet1.Tables[0].Rows[j]["i_id"].ToString();
                            str2 = "0";
                            for (k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                            {
                                if (str1 == dataSet.Tables[0].Rows[k]["i_body_part"].ToString())
                                {
                                    str2 = "1";
                                    pdfPCell2.Border = 15;
                                    pdfPTable35.AddCell(pdfPCell2);
                                }
                            }
                            if (str2 == "0")
                            {
                                pdfPCell3.Border = 15;
                                pdfPTable35.AddCell(pdfPCell3);
                            }
                            pdfPTable35.AddCell(new Phrase(str, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
                        }
                    }
                    else
                    {
                        // sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
                        sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                        strArrays = new string[] { "SELECT i_body_part FROM txn_procedure_code_with_bodypart WHERE i_event_id='", Dset.Tables[0].Rows[i][1].ToString(), "' AND sz_procedure_code_id='", Dset.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString(), "'" };
                        sqlCommand = new SqlCommand(string.Concat(strArrays), sqlConnection)
                        {
                            CommandTimeout = 0
                        };
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        dataSet = new DataSet();
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter.Fill(dataSet);
                        sqlCommand1 = new SqlCommand("SELECT i_id,sz_body_part FROM mst_accu_bodypart where i_id!=11", sqlConnection);
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
                        dataSet1 = new DataSet();
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter1.Fill(dataSet1);
                        str = "";
                        str1 = "";
                        str10 = string.Concat(Dset.Tables[0].Rows[i]["CODE"].ToString(), "-", Dset.Tables[0].Rows[i]["DESCRIPTION"].ToString());
                        pdfPTable35.DefaultCell.Border = 15;
                        pdfPTable35.WidthPercentage = 100f;
                        pdfPTable35.HorizontalAlignment = 0;
                        pdfPTable35.AddCell(new Phrase(str10, FontFactory.GetFont(str3, (float)num2, 1, Color.BLACK)));
                        for (j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                        {
                            str = dataSet1.Tables[0].Rows[j]["sz_body_part"].ToString();
                            str1 = dataSet1.Tables[0].Rows[j]["i_id"].ToString();
                            str2 = "0";
                            for (k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                            {
                                if (str1 == dataSet.Tables[0].Rows[k]["i_body_part"].ToString())
                                {
                                    str2 = "1";
                                    pdfPCell2.Border = 15;
                                    pdfPTable35.AddCell(pdfPCell2);
                                }
                            }
                            if (str2 == "0")
                            {
                                pdfPCell3.Border = 15;
                                pdfPTable35.AddCell(pdfPCell3);
                            }
                            pdfPTable35.AddCell(new Phrase(str, FontFactory.GetFont(str3, (float)num2, Color.BLACK)));
                        }
                        pdfPTable2.AddCell(pdfPTable35);
                    }
                }
            }
            singleArray = new float[] { 1f, 1f };
            PdfPTable pdfPTable36 = new PdfPTable(singleArray);
            pdfPTable36.DefaultCell.Border = 0;
            pdfPTable36.WidthPercentage = 100f;
            pdfPTable36.HorizontalAlignment = 0;
            pdfPTable36.AddCell(new Phrase("Patient Sign", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable36.HorizontalAlignment = 0;
            pdfPTable36.AddCell(new Phrase("Doctor Sign", FontFactory.GetFont(str3, (float)num, 1, Color.BLACK)));
            pdfPTable36.AddCell(pdfPCell1);
            pdfPTable36.AddCell(pdfPCell);
            pdfPTable36.AddCell("");
            pdfPTable36.AddCell("");
            pdfPTable2.AddCell(pdfPTable36);
            pdfPTable.AddCell(pdfPTable2);
            pdfPTable.AddCell("");
            pdfPTable.AddCell("");
            pdfPTable.AddCell("");
            pdfPTable.AddCell("");
            pdfPTable.AddCell("");
            pdfPTable.AddCell("");
            return pdfPTable;
        }

        private DataSet GetAcNotesInfo(string BillNumber,string AccountId)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            connection.Open();
            try
            {
                SqlCommand selectCommand = new SqlCommand("SP_PDFBILLS_MASTERBILLING_New", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;

                selectCommand.Parameters.Add(new SqlParameter("@SZ_BILL_NUMBER", BillNumber));
                selectCommand.Parameters.Add(new SqlParameter("@SZ_COMPANY_ID", AccountId));
                selectCommand.Parameters.Add(new SqlParameter("@FLAG", "PDF"));
                
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (Exception ex)
            {
               

            }
            return dataSet;
        }
    }
}