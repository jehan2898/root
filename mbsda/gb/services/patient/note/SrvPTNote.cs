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
    public class SrvPTNote : SrvNote
    {
        private List<gbmodel.bill.Bill> c_lstBill;

        public SrvPTNote(List<gbmodel.bill.Bill> p_lstBill)
        {
            this.c_lstBill = p_lstBill;
        }

        public override void PrintNote()
        {
            string c_basepath = System.Configuration.ConfigurationManager.AppSettings["BASEPATH"].ToString();
            string Path=System.Configuration.ConfigurationManager.AppSettings["BASEPATH"].ToString();
            SqlConnection selectConnection = new SqlConnection(DBUtil.ConnectionString);
            List<gbmodel.patient.note.Note> lstNote = Select(c_lstBill, gbmodel.patient.note.EnumNoteType.PT);

            string checkbox = System.Configuration.ConfigurationManager.AppSettings["CHECKBOXPATH"].ToString();
            string uncheckbox = System.Configuration.ConfigurationManager.AppSettings["UNCHECKBOXPATH"].ToString();
            string checkradio = System.Configuration.ConfigurationManager.AppSettings["CHECKRADIO"].ToString();
            string uncheckradio = System.Configuration.ConfigurationManager.AppSettings["UNCHECKRADIO"].ToString();

            int num = 0;
            string str = "";
            string SystemSettingValue = "0";

            for (int i = 0; i < lstNote.Count; i++)
            {

                gbmodel.patient.note.PTNote oPTNote = new gbmodel.patient.note.PTNote();
                oPTNote = (gbmodel.patient.note.PTNote)lstNote[i];

                string s_pdfname = string.Concat(new object[] { "FUReport_", this.c_lstBill[i].Number, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf" });
                c_basepath = c_basepath + "/" + s_pdfname;

                SqlCommand selectCommand = new SqlCommand("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + this.c_lstBill[i].Number + "', @FLAG='GET_EVENT_ID'", selectConnection);
                selectCommand.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                DataSet set2 = new DataSet();
                adapter.Fill(set2);


                MemoryStream stream = new MemoryStream();
                Document document = new Document(PageSize.A4, 36f, 36f, 20f, 36f);
                float[] numArray = new float[] { 4f };
                PdfPTable table = new PdfPTable(numArray);
                table.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                table.DefaultCell.HorizontalAlignment = 1;
                table.DefaultCell.VerticalAlignment = 4;
                table.DefaultCell.Colspan = 1;
                PdfWriter instance = PdfWriter.GetInstance(document, stream);
                document.Open();
                float[] numArray2 = new float[] { 4f };
                PdfPTable table2 = new PdfPTable(numArray2);
                int num3 = 60;
                int num4 = 0x16;
                int num5 = 0;
                for (int j = num; j < set2.Tables[0].Rows.Count; j++)
                {
                    DataSet set4 = new DataSet();
                    string str6 = set2.Tables[0].Rows[j][0].ToString();
                    int count = this.GET_COMPLIANTS_USING_EVENTID(str6).Tables[0].Rows.Count;
                    int num8 = (count / 2) + (count % 2);
                    int num9 = this.CheckCount(str6);
                    DataSet set5 = new DataSet();
                    int num10 = this.GET_PROCEDURECODE_USING_EVENTID(str6).Tables[0].Rows.Count;
                    int num11 = (num10 / 2) + (num10 % 2);
                    if (j == 0)
                    {
                        num5 = num3 - (((num8 + num9) + num11) + num4);
                        table2 = new PdfPTable(numArray2);
                        table2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                        table2.DefaultCell.Border = 0;
                        table2.DefaultCell.HorizontalAlignment = 1;
                        table2.DefaultCell.VerticalAlignment = 5;
                        table.DefaultCell.Border = 0;
                        table2.AddCell(new Phrase("PT NOTES", FontFactory.GetFont("Arial", 12f, 1, iTextSharp.text.Color.BLACK)));
                        table.AddCell(table2);
                    }
                    else
                    {
                        int num12 = ((num8 + num9) + num11) + num4;
                        if (num5 >= num12)
                        {
                            num5 -= num12;
                            table2 = new PdfPTable(numArray2);
                            table2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                            table2.DefaultCell.Border = 0;
                            table2.DefaultCell.HorizontalAlignment = 1;
                            table2.DefaultCell.VerticalAlignment = 5;
                            table.DefaultCell.Border = 0;
                            table2.AddCell(new Phrase("PT NOTES", FontFactory.GetFont("Arial", 12f, 1, iTextSharp.text.Color.BLACK)));
                            table.AddCell(table2);
                        }
                        else
                        {
                            num5 = num3 - (((num8 + num9) + num11) + num4);

                            table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);
                            table = new PdfPTable(numArray);
                            table.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                            table.DefaultCell.HorizontalAlignment = 1;
                            table.DefaultCell.VerticalAlignment = 4;
                            table.DefaultCell.Colspan = 1;
                            if (SystemSettingValue == "1")
                            {

                            }
                            else
                            {
                                document.NewPage();
                            }
                            //document.NewPage();
                            table2 = new PdfPTable(numArray2);
                            table2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                            table2.DefaultCell.Border = 0;
                            table2.DefaultCell.HorizontalAlignment = 1;
                            table2.DefaultCell.VerticalAlignment = 5;
                            table.DefaultCell.Border = 0;
                            table2.AddCell(new Phrase("PT NOTES", FontFactory.GetFont("Arial", 12f, 1, iTextSharp.text.Color.BLACK)));
                            table.AddCell(table2);
                        }
                    }
                    table.DefaultCell.Border = 0;
                   
                    float[] numArray3 = new float[] { 4f };
                    PdfPTable table3 = new PdfPTable(numArray3);
                    table3.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    float[] numArray4 = new float[] { 2f, 2f, 2f, 2f };
                    PdfPTable table4 = new PdfPTable(numArray4);
                    table4.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.Border = 0;
                    table4.DefaultCell.HorizontalAlignment = 0;
                    table4.DefaultCell.VerticalAlignment = 0;
                    table4.AddCell(new Phrase("Patient Name", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " + oPTNote.Patient.Name, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK))); //ptview.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString()
                    table4.AddCell(new Phrase("Case #", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " +oPTNote.Patient.CaseNo , FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//ptview.Tables[0].Rows[0]["SZ_CASE_NO"].ToString()
                    table4.DefaultCell.Colspan = 1;
                    string str7 = "";
                    if (oPTNote.Patient.DOA != "")//set3.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString()
                    {
                        str7 = Convert.ToDateTime(oPTNote.Patient.DOA).ToString("MM-dd-yyyy");
                    }

                    table4.AddCell(new Phrase("Date of Accident", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " + str7, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("Insurance Company", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " + oPTNote.Carrier.Name, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//set3.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString()
                    table4.DefaultCell.Colspan = 1;
                    table4.AddCell(new Phrase("Claim Number", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " +oPTNote.Patient.ClaimNumber , FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//set3.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString()
                    string str8 = "";
                    if (oPTNote.DtDate != "")//(ptview.Tables[0].Rows[0]["DT_DATE"].ToString()
                    {
                        str8 = Convert.ToDateTime(oPTNote.DtDate).ToString("MM-dd-yyyy");
                    }

                    table4.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " + str8, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.DefaultCell.Colspan = 1;
                    table4.AddCell(new Phrase("Doctor", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " +oPTNote.DoctorName , FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//ptview.Tables[0].Rows[0]["sz_doctor_name"].ToString()
                    table4.DefaultCell.Colspan = 1;
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table4.DefaultCell.Colspan = 1;
                    table4.AddCell(new Phrase("Patient Complaints", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));

                    PdfPCell cell = new PdfPCell(new Phrase("- " + oPTNote.Complaints, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//ptview.Tables[0].Rows[0]["SZ_COMPLAINTS"].ToString()
                    cell.Colspan = 3;
                    cell.Border = 0;

                    table4.AddCell(cell);
                    table4.DefaultCell.Colspan = 1;
                    table3.AddCell(table4);
                    set4 = this.GET_COMPLIANTS_USING_EVENTID(str6);

                    float[] numArray5 = new float[] { 1f, 3f, 1f, 3f };
                    PdfPTable table5 = new PdfPTable(numArray5);
                    table.DefaultCell.Border = 15;
                    table5.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table5.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table5.DefaultCell.Colspan = 4;
                    table5.AddCell(new Phrase("Complaints", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table5.DefaultCell.Colspan = 1;
                    for (int k = 0; k < set4.Tables[0].Rows.Count; k++)
                    {
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(checkbox);
                        image.ScaleAbsolute(10f, 10f);
                        PdfPCell cell2 = new PdfPCell(image);
                        cell2.Border = 0;
                        cell2.HorizontalAlignment = 2;
                        cell2.VerticalAlignment = 6;
                        cell2.FixedHeight = 12f;
                        table5.AddCell(cell2);
                        table5.DefaultCell.Border = 0;
                        table5.AddCell(new Phrase(set4.Tables[0].Rows[k]["SZ_COMPLAINT"].ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        table5.DefaultCell.HorizontalAlignment = 0;
                        table5.DefaultCell.VerticalAlignment = 6;
                    }
                    if ((set4.Tables[0].Rows.Count % 2) != 0)
                    {
                        table5.AddCell(new Phrase(""));
                        table5.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table5);
                    float[] numArray6 = new float[] { 2f, 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable table6 = new PdfPTable(numArray6);
                    table.DefaultCell.Border = 15;
                    table6.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table6.DefaultCell.Colspan = 1;
                    table6.DefaultCell.Border = 0;
                    table6.AddCell(new Phrase("Pain Grades", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Mild == "1")//ptview.Tables[0].Rows[0]["BT_MILD"].ToString()
                    {
                        iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(checkbox);
                        image2.ScaleAbsolute(10f, 10f);
                        PdfPCell cell3 = new PdfPCell(image2);
                        cell3.Border = 0;
                        cell3.HorizontalAlignment = 2;
                        cell3.VerticalAlignment = 6;
                        cell3.FixedHeight = 12f;
                        table6.AddCell(cell3);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image3 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image3.ScaleAbsolute(10f, 10f);
                        PdfPCell cell4 = new PdfPCell(image3);
                        cell4.Border = 0;
                        cell4.HorizontalAlignment = 2;
                        cell4.VerticalAlignment = 6;
                        cell4.FixedHeight = 12f;
                        table6.AddCell(cell4);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("MILD", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Moderate== "1")//ptview.Tables[0].Rows[0]["BT_MODERATE"].ToString()
                    {
                        iTextSharp.text.Image image4 = iTextSharp.text.Image.GetInstance(checkbox);
                        image4.ScaleAbsolute(10f, 10f);
                        PdfPCell cell5 = new PdfPCell(image4);
                        cell5.Border = 0;
                        cell5.HorizontalAlignment = 2;
                        cell5.VerticalAlignment = 6;
                        cell5.FixedHeight = 12f;
                        table6.AddCell(cell5);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image5 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image5.ScaleAbsolute(10f, 10f);
                        PdfPCell cell6 = new PdfPCell(image5);
                        cell6.Border = 0;
                        cell6.HorizontalAlignment = 2;
                        cell6.VerticalAlignment = 6;
                        cell6.FixedHeight = 12f;
                        table6.AddCell(cell6);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("MODERATE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Severe == "1")//ptview.Tables[0].Rows[0]["BT_SEVERE"].ToString()
                    {
                        iTextSharp.text.Image image6 = iTextSharp.text.Image.GetInstance(checkbox);
                        image6.ScaleAbsolute(10f, 10f);
                        PdfPCell cell7 = new PdfPCell(image6);
                        cell7.Border = 0;
                        cell7.HorizontalAlignment = 2;
                        cell7.VerticalAlignment = 6;
                        cell7.FixedHeight = 12f;
                        table6.AddCell(cell7);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image7 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image7.ScaleAbsolute(10f, 10f);
                        PdfPCell cell8 = new PdfPCell(image7);
                        cell8.Border = 0;
                        cell8.HorizontalAlignment = 2;
                        cell8.VerticalAlignment = 6;
                        cell8.FixedHeight = 12f;
                        table6.AddCell(cell8);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("SEVERE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Verysevere == "1")//ptview.Tables[0].Rows[0]["BT_VERY_SEVERE"].ToString()
                    {
                        iTextSharp.text.Image image8 = iTextSharp.text.Image.GetInstance(checkbox);
                        image8.ScaleAbsolute(10f, 10f);
                        PdfPCell cell9 = new PdfPCell(image8);
                        cell9.Border = 0;
                        cell9.HorizontalAlignment = 2;
                        cell9.VerticalAlignment = 6;
                        cell9.FixedHeight = 12f;
                        table6.AddCell(cell9);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image9 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image9.ScaleAbsolute(10f, 10f);
                        PdfPCell cell10 = new PdfPCell(image9);
                        cell10.Border = 0;
                        cell10.HorizontalAlignment = 2;
                        cell10.VerticalAlignment = 6;
                        cell10.FixedHeight = 12f;
                        table6.AddCell(cell10);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("VERY SEVERE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table3.AddCell(table6);
                    float[] numArray7 = new float[] { 2f, 2f };
                    PdfPTable table7 = new PdfPTable(numArray7);
                    table7.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 0;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table7.DefaultCell.Colspan = 1;
                    table7.DefaultCell.Border = 2;
                    table7.AddCell(new Phrase("Treatment", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table7.AddCell(new Phrase("Location", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table7.DefaultCell.Border = 0;
                    table7.DefaultCell.Colspan = 1;
                    table7.DefaultCell.HorizontalAlignment = 0;
                    table7.DefaultCell.VerticalAlignment = 4;
                    float[] numArray8 = new float[] { 3f, 1f, 1f, 1f };
                    PdfPTable table8 = new PdfPTable(numArray8);
                    table8.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase("Right", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase("Left", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase("Both", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (((oPTNote.TreatmentHeadacheRight == "1") || ( oPTNote.TreatmentHeadacheLeft== "1")) || (oPTNote.TreatmentHeadacheboth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_BOTH"].ToString()
                        table8.AddCell(new Phrase("HEADACHE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentHeadacheRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_RIGHT"].ToString()
                            iTextSharp.text.Image image10 = iTextSharp.text.Image.GetInstance(checkbox);
                            image10.ScaleAbsolute(10f, 10f);
                            PdfPCell cell11 = new PdfPCell(image10);
                            cell11.HorizontalAlignment = 1;
                            cell11.VerticalAlignment = 5;
                            cell11.FixedHeight = 12f;
                            table8.AddCell(cell11);
                        }
                        else
                        {
                            iTextSharp.text.Image image11 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image11.ScaleAbsolute(10f, 10f);
                            PdfPCell cell12 = new PdfPCell(image11);
                            cell12.HorizontalAlignment = 1;
                            cell12.VerticalAlignment = 5;
                            cell12.FixedHeight = 12f;
                            table8.AddCell(cell12);
                        }
                        if (oPTNote.TreatmentHeadacheLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_LEFT"].ToString()
                            iTextSharp.text.Image image12 = iTextSharp.text.Image.GetInstance(checkbox);
                            image12.ScaleAbsolute(10f, 10f);
                            PdfPCell cell13 = new PdfPCell(image12);
                            cell13.HorizontalAlignment = 1;
                            cell13.VerticalAlignment = 5;
                            cell13.FixedHeight = 12f;
                            table8.AddCell(cell13);
                        }
                        else
                        {
                            iTextSharp.text.Image image13 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image13.ScaleAbsolute(10f, 10f);
                            PdfPCell cell14 = new PdfPCell(image13);
                            cell14.HorizontalAlignment = 1;
                            cell14.VerticalAlignment = 5;
                            cell14.FixedHeight = 12f;
                            table8.AddCell(cell14);
                        }
                        if (oPTNote.TreatmentHeadacheboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_BOTH"].ToString()
                            iTextSharp.text.Image image14 = iTextSharp.text.Image.GetInstance(checkbox);
                            image14.ScaleAbsolute(10f, 10f);
                            PdfPCell cell15 = new PdfPCell(image14);
                            cell15.HorizontalAlignment = 1;
                            cell15.VerticalAlignment = 5;
                            cell15.FixedHeight = 12f;
                            table8.AddCell(cell15);
                        }
                        else
                        {
                            iTextSharp.text.Image image15 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image15.ScaleAbsolute(10f, 10f);
                            PdfPCell cell16 = new PdfPCell(image15);
                            cell16.HorizontalAlignment = 1;
                            cell16.VerticalAlignment = 5;
                            cell16.FixedHeight = 12f;
                            table8.AddCell(cell16);
                        }
                    }
                    if ((( oPTNote.TreatmentHandRight== "1") || (oPTNote.TreatmentHandLeft == "1")) || (oPTNote.TreatmentHandBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_BOTH"].ToString()
                        table8.AddCell(new Phrase("HAND", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.TreatmentHandRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_RIGHT"].ToString()
                            iTextSharp.text.Image image16 = iTextSharp.text.Image.GetInstance(checkbox);
                            image16.ScaleAbsolute(10f, 10f);
                            PdfPCell cell17 = new PdfPCell(image16);
                            cell17.HorizontalAlignment = 1;
                            cell17.VerticalAlignment = 5;
                            cell17.FixedHeight = 12f;
                            table8.AddCell(cell17);
                        }
                        else
                        {
                            iTextSharp.text.Image image17 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image17.ScaleAbsolute(10f, 10f);
                            PdfPCell cell18 = new PdfPCell(image17);
                            cell18.HorizontalAlignment = 1;
                            cell18.VerticalAlignment = 5;
                            cell18.FixedHeight = 12f;
                            table8.AddCell(cell18);
                        }
                        if ( oPTNote.TreatmentHandLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_LEFT"].ToString()
                            iTextSharp.text.Image image18 = iTextSharp.text.Image.GetInstance(checkbox);
                            image18.ScaleAbsolute(10f, 10f);
                            PdfPCell cell19 = new PdfPCell(image18);
                            cell19.HorizontalAlignment = 1;
                            cell19.VerticalAlignment = 5;
                            cell19.FixedHeight = 12f;
                            table8.AddCell(cell19);
                        }
                        else
                        {
                            iTextSharp.text.Image image19 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image19.ScaleAbsolute(10f, 10f);
                            PdfPCell cell20 = new PdfPCell(image19);
                            cell20.HorizontalAlignment = 1;
                            cell20.VerticalAlignment = 5;
                            cell20.FixedHeight = 12f;
                            table8.AddCell(cell20);
                        }
                        if ( oPTNote.TreatmentHandBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_BOTH"].ToString()
                            iTextSharp.text.Image image20 = iTextSharp.text.Image.GetInstance(checkbox);
                            image20.ScaleAbsolute(10f, 10f);
                            PdfPCell cell21 = new PdfPCell(image20);
                            cell21.HorizontalAlignment = 1;
                            cell21.VerticalAlignment = 5;
                            cell21.FixedHeight = 12f;
                            table8.AddCell(cell21);
                        }
                        else
                        {
                            iTextSharp.text.Image image21 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image21.ScaleAbsolute(10f, 10f);
                            PdfPCell cell22 = new PdfPCell(image21);
                            cell22.HorizontalAlignment = 1;
                            cell22.VerticalAlignment = 5;
                            cell22.FixedHeight = 12f;
                            table8.AddCell(cell22);
                        }
                    }
                    if ((( oPTNote.TreatmentNeckRight== "1") || (oPTNote.TreatmentNeckLeft == "1")) || ( oPTNote.TreatmentNeckBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_BOTH"].ToString()
                        table8.AddCell(new Phrase("NECK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentNeckRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_RIGHT"].ToString()
                            iTextSharp.text.Image image22 = iTextSharp.text.Image.GetInstance(checkbox);
                            image22.ScaleAbsolute(10f, 10f);
                            PdfPCell cell23 = new PdfPCell(image22);
                            cell23.HorizontalAlignment = 1;
                            cell23.VerticalAlignment = 5;
                            cell23.FixedHeight = 12f;
                            table8.AddCell(cell23);
                        }
                        else
                        {
                            iTextSharp.text.Image image23 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image23.ScaleAbsolute(10f, 10f);
                            PdfPCell cell24 = new PdfPCell(image23);
                            cell24.HorizontalAlignment = 1;
                            cell24.VerticalAlignment = 5;
                            cell24.FixedHeight = 12f;
                            table8.AddCell(cell24);
                        }
                        if ( oPTNote.TreatmentNeckLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_LEFT"].ToString()
                            iTextSharp.text.Image image24 = iTextSharp.text.Image.GetInstance(checkbox);
                            image24.ScaleAbsolute(10f, 10f);
                            PdfPCell cell25 = new PdfPCell(image24);
                            cell25.HorizontalAlignment = 1;
                            cell25.VerticalAlignment = 5;
                            cell25.FixedHeight = 12f;
                            table8.AddCell(cell25);
                        }
                        else
                        {
                            iTextSharp.text.Image image25 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image25.ScaleAbsolute(10f, 10f);
                            PdfPCell cell26 = new PdfPCell(image25);
                            cell26.HorizontalAlignment = 1;
                            cell26.VerticalAlignment = 5;
                            cell26.FixedHeight = 12f;
                            table8.AddCell(cell26);
                        }
                        if ( oPTNote.TreatmentNeckBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_BOTH"].ToString()
                            iTextSharp.text.Image image26 = iTextSharp.text.Image.GetInstance(checkbox);
                            image26.ScaleAbsolute(10f, 10f);
                            PdfPCell cell27 = new PdfPCell(image26);
                            cell27.HorizontalAlignment = 1;
                            cell27.VerticalAlignment = 5;
                            cell27.FixedHeight = 12f;
                            table8.AddCell(cell27);
                        }
                        else
                        {
                            iTextSharp.text.Image image27 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image27.ScaleAbsolute(10f, 10f);
                            PdfPCell cell28 = new PdfPCell(image27);
                            cell28.HorizontalAlignment = 1;
                            cell28.VerticalAlignment = 5;
                            cell28.FixedHeight = 12f;
                            table8.AddCell(cell28);
                        }
                    }
                    if ((( oPTNote.TreatmentFingersRight== "1") || (oPTNote.TreatmentFingersLeft == "1")) || (oPTNote.TreatmentFingersboth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_BOTH"].ToString()
                        table8.AddCell(new Phrase("FINGERS", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentFingersRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_RIGHT"].ToString()
                            iTextSharp.text.Image image28 = iTextSharp.text.Image.GetInstance(checkbox);
                            image28.ScaleAbsolute(10f, 10f);
                            PdfPCell cell29 = new PdfPCell(image28);
                            cell29.HorizontalAlignment = 1;
                            cell29.VerticalAlignment = 5;
                            cell29.FixedHeight = 12f;
                            table8.AddCell(cell29);
                        }
                        else
                        {
                            iTextSharp.text.Image image29 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image29.ScaleAbsolute(10f, 10f);
                            PdfPCell cell30 = new PdfPCell(image29);
                            cell30.HorizontalAlignment = 1;
                            cell30.VerticalAlignment = 5;
                            cell30.FixedHeight = 12f;
                            table8.AddCell(cell30);
                        }
                        if (oPTNote.TreatmentFingersLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_LEFT"].ToString()
                            iTextSharp.text.Image image30 = iTextSharp.text.Image.GetInstance(checkbox);
                            image30.ScaleAbsolute(10f, 10f);
                            PdfPCell cell31 = new PdfPCell(image30);
                            cell31.HorizontalAlignment = 1;
                            cell31.VerticalAlignment = 5;
                            cell31.FixedHeight = 12f;
                            table8.AddCell(cell31);
                        }
                        else
                        {
                            iTextSharp.text.Image image31 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image31.ScaleAbsolute(10f, 10f);
                            PdfPCell cell32 = new PdfPCell(image31);
                            cell32.HorizontalAlignment = 1;
                            cell32.VerticalAlignment = 5;
                            cell32.FixedHeight = 12f;
                            table8.AddCell(cell32);
                        }
                        if ( oPTNote.TreatmentFingersboth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_BOTH"].ToString()
                            iTextSharp.text.Image image32 = iTextSharp.text.Image.GetInstance(checkbox);
                            image32.ScaleAbsolute(10f, 10f);
                            PdfPCell cell33 = new PdfPCell(image32);
                            cell33.HorizontalAlignment = 1;
                            cell33.VerticalAlignment = 5;
                            cell33.FixedHeight = 12f;
                            table8.AddCell(cell33);
                        }
                        else
                        {
                            iTextSharp.text.Image image33 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image33.ScaleAbsolute(10f, 10f);
                            PdfPCell cell34 = new PdfPCell(image33);
                            cell34.HorizontalAlignment = 1;
                            cell34.VerticalAlignment = 5;
                            cell34.FixedHeight = 12f;
                            table8.AddCell(cell34);
                        }
                    }
                    if (((oPTNote.TreatmentMidbackRight == "1") || ( oPTNote.TreatmentMidbackLeft== "1")) || (oPTNote.TreatmentMidbackBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_BOTH"].ToString()
                        table8.AddCell(new Phrase("MID BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentMidbackRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image34 = iTextSharp.text.Image.GetInstance(checkbox);
                            image34.ScaleAbsolute(10f, 10f);
                            PdfPCell cell35 = new PdfPCell(image34);
                            cell35.HorizontalAlignment = 1;
                            cell35.VerticalAlignment = 5;
                            cell35.FixedHeight = 12f;
                            table8.AddCell(cell35);
                        }
                        else
                        {
                            iTextSharp.text.Image image35 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image35.ScaleAbsolute(10f, 10f);
                            PdfPCell cell36 = new PdfPCell(image35);
                            cell36.HorizontalAlignment = 1;
                            cell36.VerticalAlignment = 5;
                            cell36.FixedHeight = 12f;
                            table8.AddCell(cell36);
                        }
                        if (oPTNote.TreatmentMidbackLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_LEFT"].ToString()
                            iTextSharp.text.Image image36 = iTextSharp.text.Image.GetInstance(checkbox);
                            image36.ScaleAbsolute(10f, 10f);
                            PdfPCell cell37 = new PdfPCell(image36);
                            cell37.HorizontalAlignment = 1;
                            cell37.VerticalAlignment = 5;
                            cell37.FixedHeight = 12f;
                            table8.AddCell(cell37);
                        }
                        else
                        {
                            iTextSharp.text.Image image37 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image37.ScaleAbsolute(10f, 10f);
                            PdfPCell cell38 = new PdfPCell(image37);
                            cell38.HorizontalAlignment = 1;
                            cell38.VerticalAlignment = 5;
                            cell38.FixedHeight = 12f;
                            table8.AddCell(cell38);
                        }
                        if ( oPTNote.TreatmentMidbackBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image38 = iTextSharp.text.Image.GetInstance(checkbox);
                            image38.ScaleAbsolute(10f, 10f);
                            PdfPCell cell39 = new PdfPCell(image38);
                            cell39.HorizontalAlignment = 1;
                            cell39.VerticalAlignment = 5;
                            cell39.FixedHeight = 12f;
                            table8.AddCell(cell39);
                        }
                        else
                        {
                            iTextSharp.text.Image image39 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image39.ScaleAbsolute(10f, 10f);
                            PdfPCell cell40 = new PdfPCell(image39);
                            cell40.HorizontalAlignment = 1;
                            cell40.VerticalAlignment = 5;
                            cell40.FixedHeight = 12f;
                            table8.AddCell(cell40);
                        }
                    }
                    if (((oPTNote.TreatmentHipRight  == "1") || (oPTNote.TreatmentHipLeft == "1")) || ( oPTNote.TreatmentHipBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_BOTH"].ToString()
                        table8.AddCell(new Phrase("HIP", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentHipRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_RIGHT"].ToString() 
                            iTextSharp.text.Image image40 = iTextSharp.text.Image.GetInstance(checkbox);
                            image40.ScaleAbsolute(10f, 10f);
                            PdfPCell cell41 = new PdfPCell(image40);
                            cell41.HorizontalAlignment = 1;
                            cell41.VerticalAlignment = 5;
                            cell41.FixedHeight = 12f;
                            table8.AddCell(cell41);
                        }
                        else
                        {
                            iTextSharp.text.Image image41 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image41.ScaleAbsolute(10f, 10f);
                            PdfPCell cell42 = new PdfPCell(image41);
                            cell42.HorizontalAlignment = 1;
                            cell42.VerticalAlignment = 5;
                            cell42.FixedHeight = 12f;
                            table8.AddCell(cell42);
                        }
                        if ( oPTNote.TreatmentHipLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_LEFT"].ToString()
                            iTextSharp.text.Image image42 = iTextSharp.text.Image.GetInstance(checkbox);
                            image42.ScaleAbsolute(10f, 10f);
                            PdfPCell cell43 = new PdfPCell(image42);
                            cell43.HorizontalAlignment = 1;
                            cell43.VerticalAlignment = 5;
                            cell43.FixedHeight = 12f;
                            table8.AddCell(cell43);
                        }
                        else
                        {
                            iTextSharp.text.Image image43 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image43.ScaleAbsolute(10f, 10f);
                            PdfPCell cell44 = new PdfPCell(image43);
                            cell44.HorizontalAlignment = 1;
                            cell44.VerticalAlignment = 5;
                            cell44.FixedHeight = 12f;
                            table8.AddCell(cell44);
                        }
                        if ( oPTNote.TreatmentHipBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_BOTH"].ToString()
                            iTextSharp.text.Image image44 = iTextSharp.text.Image.GetInstance(checkbox);
                            image44.ScaleAbsolute(10f, 10f);
                            PdfPCell cell45 = new PdfPCell(image44);
                            cell45.HorizontalAlignment = 1;
                            cell45.VerticalAlignment = 5;
                            cell45.FixedHeight = 12f;
                            table8.AddCell(cell45);
                        }
                        else
                        {
                            iTextSharp.text.Image image45 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image45.ScaleAbsolute(10f, 10f);
                            PdfPCell cell46 = new PdfPCell(image45);
                            cell46.HorizontalAlignment = 1;
                            cell46.VerticalAlignment = 5;
                            cell46.FixedHeight = 12f;
                            table8.AddCell(cell46);
                        }
                    }
                    if (((oPTNote.TreatmentLowbackRight== "1") || (oPTNote.TreatmentLowbackLeft== "1")) || ( oPTNote.TreatmentLowbackBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_BOTH"].ToString()
                        table8.AddCell(new Phrase("LOW BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentLowbackRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image46 = iTextSharp.text.Image.GetInstance(checkbox);
                            image46.ScaleAbsolute(10f, 10f);
                            PdfPCell cell47 = new PdfPCell(image46);
                            cell47.HorizontalAlignment = 1;
                            cell47.VerticalAlignment = 5;
                            cell47.FixedHeight = 12f;
                            table8.AddCell(cell47);
                        }
                        else
                        {
                            iTextSharp.text.Image image47 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image47.ScaleAbsolute(10f, 10f);
                            PdfPCell cell48 = new PdfPCell(image47);
                            cell48.HorizontalAlignment = 1;
                            cell48.VerticalAlignment = 5;
                            cell48.FixedHeight = 12f;
                            table8.AddCell(cell48);
                        }
                        if (oPTNote.TreatmentLowbackLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_LEFT"].ToString()
                            iTextSharp.text.Image image48 = iTextSharp.text.Image.GetInstance(checkbox);
                            image48.ScaleAbsolute(10f, 10f);
                            PdfPCell cell49 = new PdfPCell(image48);
                            cell49.HorizontalAlignment = 1;
                            cell49.VerticalAlignment = 5;
                            cell49.FixedHeight = 12f;
                            table8.AddCell(cell49);
                        }
                        else
                        {
                            iTextSharp.text.Image image49 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image49.ScaleAbsolute(10f, 10f);
                            PdfPCell cell50 = new PdfPCell(image49);
                            cell50.HorizontalAlignment = 1;
                            cell50.VerticalAlignment = 5;
                            cell50.FixedHeight = 12f;
                            table8.AddCell(cell50);
                        }
                        if ( oPTNote.TreatmentLowbackBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image50 = iTextSharp.text.Image.GetInstance(checkbox);
                            image50.ScaleAbsolute(10f, 10f);
                            PdfPCell cell51 = new PdfPCell(image50);
                            cell51.HorizontalAlignment = 1;
                            cell51.VerticalAlignment = 5;
                            cell51.FixedHeight = 12f;
                            table8.AddCell(cell51);
                        }
                        else
                        {
                            iTextSharp.text.Image image51 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image51.ScaleAbsolute(10f, 10f);
                            PdfPCell cell52 = new PdfPCell(image51);
                            cell52.HorizontalAlignment = 1;
                            cell52.VerticalAlignment = 5;
                            cell52.FixedHeight = 12f;
                            table8.AddCell(cell52);
                        }
                    }
                    if (((oPTNote.TreatmenttHighLeft == "1") || ( oPTNote.TreatmenttHighLeft== "1")) || (oPTNote.TreatmenttHighLeft == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_LEFT"].ToString()
                        table8.AddCell(new Phrase("THIGH", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmenttHighRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_RIGHT"].ToString()
                            iTextSharp.text.Image image52 = iTextSharp.text.Image.GetInstance(checkbox);
                            image52.ScaleAbsolute(10f, 10f);
                            PdfPCell cell53 = new PdfPCell(image52);
                            cell53.HorizontalAlignment = 1;
                            cell53.VerticalAlignment = 5;
                            cell53.FixedHeight = 12f;
                            table8.AddCell(cell53);
                        }
                        else
                        {
                            iTextSharp.text.Image image53 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image53.ScaleAbsolute(10f, 10f);
                            PdfPCell cell54 = new PdfPCell(image53);
                            cell54.HorizontalAlignment = 1;
                            cell54.VerticalAlignment = 5;
                            cell54.FixedHeight = 12f;
                            table8.AddCell(cell54);
                        }
                        if (oPTNote.TreatmenttHighLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_LEFT"].ToString()
                            iTextSharp.text.Image image54 = iTextSharp.text.Image.GetInstance(checkbox);
                            image54.ScaleAbsolute(10f, 10f);
                            PdfPCell cell55 = new PdfPCell(image54);
                            cell55.HorizontalAlignment = 1;
                            cell55.VerticalAlignment = 5;
                            cell55.FixedHeight = 12f;
                            table8.AddCell(cell55);
                        }
                        else
                        {
                            iTextSharp.text.Image image55 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image55.ScaleAbsolute(10f, 10f);
                            PdfPCell cell56 = new PdfPCell(image55);
                            cell56.HorizontalAlignment = 1;
                            cell56.VerticalAlignment = 5;
                            cell56.FixedHeight = 12f;
                            table8.AddCell(cell56);
                        }
                        if (oPTNote.TreatmenttHighBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_BOTH"].ToString()
                            iTextSharp.text.Image image56 = iTextSharp.text.Image.GetInstance(checkbox);
                            image56.ScaleAbsolute(10f, 10f);
                            PdfPCell cell57 = new PdfPCell(image56);
                            cell57.HorizontalAlignment = 1;
                            cell57.VerticalAlignment = 5;
                            cell57.FixedHeight = 12f;
                            table8.AddCell(cell57);
                        }
                        else
                        {
                            iTextSharp.text.Image image57 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image57.ScaleAbsolute(10f, 10f);
                            PdfPCell cell58 = new PdfPCell(image57);
                            cell58.HorizontalAlignment = 1;
                            cell58.VerticalAlignment = 5;
                            cell58.FixedHeight = 12f;
                            table8.AddCell(cell58);
                        }
                    }
                    if ((( oPTNote.TreatmentJawRight== "1") || (oPTNote.TreatmentJawLeft == "1")) || (oPTNote.TreatmentJawBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_BOTH"].ToString()
                        table8.AddCell(new Phrase("JAW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentJawRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_RIGHT"].ToString()
                            iTextSharp.text.Image image58 = iTextSharp.text.Image.GetInstance(checkbox);
                            image58.ScaleAbsolute(10f, 10f);
                            PdfPCell cell59 = new PdfPCell(image58);
                            cell59.HorizontalAlignment = 1;
                            cell59.VerticalAlignment = 5;
                            cell59.FixedHeight = 12f;
                            table8.AddCell(cell59);
                        }
                        else
                        {
                            iTextSharp.text.Image image59 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image59.ScaleAbsolute(10f, 10f);
                            PdfPCell cell60 = new PdfPCell(image59);
                            cell60.HorizontalAlignment = 1;
                            cell60.VerticalAlignment = 5;
                            cell60.FixedHeight = 12f;
                            table8.AddCell(cell60);
                        }
                        if (oPTNote.TreatmentJawLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_LEFT"].ToString()
                            iTextSharp.text.Image image60 = iTextSharp.text.Image.GetInstance(checkbox);
                            image60.ScaleAbsolute(10f, 10f);
                            PdfPCell cell61 = new PdfPCell(image60);
                            cell61.HorizontalAlignment = 1;
                            cell61.VerticalAlignment = 5;
                            cell61.FixedHeight = 12f;
                            table8.AddCell(cell61);
                        }
                        else
                        {
                            iTextSharp.text.Image image61 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image61.ScaleAbsolute(10f, 10f);
                            PdfPCell cell62 = new PdfPCell(image61);
                            cell62.HorizontalAlignment = 1;
                            cell62.VerticalAlignment = 5;
                            cell62.FixedHeight = 12f;
                            table8.AddCell(cell62);
                        }
                        if (oPTNote.TreatmentJawBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_BOTH"].ToString()
                            iTextSharp.text.Image image62 = iTextSharp.text.Image.GetInstance(checkbox);
                            image62.ScaleAbsolute(10f, 10f);
                            PdfPCell cell63 = new PdfPCell(image62);
                            cell63.HorizontalAlignment = 1;
                            cell63.VerticalAlignment = 5;
                            cell63.FixedHeight = 12f;
                            table8.AddCell(cell63);
                        }
                        else
                        {
                            iTextSharp.text.Image image63 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image63.ScaleAbsolute(10f, 10f);
                            PdfPCell cell64 = new PdfPCell(image63);
                            cell64.HorizontalAlignment = 1;
                            cell64.VerticalAlignment = 5;
                            cell64.FixedHeight = 12f;
                            table8.AddCell(cell64);
                        }
                    }
                    if ((( oPTNote.TreatmentKneeRight== "1") || (oPTNote.TreatmentkneeLeft == "1")) || (oPTNote.TreatmentKneeBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_BOTH"].ToString()
                        table8.AddCell(new Phrase("KNEE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentKneeRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_RIGHT"].ToString()
                            iTextSharp.text.Image image64 = iTextSharp.text.Image.GetInstance(checkbox);
                            image64.ScaleAbsolute(10f, 10f);
                            PdfPCell cell65 = new PdfPCell(image64);
                            cell65.HorizontalAlignment = 1;
                            cell65.VerticalAlignment = 5;
                            cell65.FixedHeight = 12f;
                            table8.AddCell(cell65);
                        }
                        else
                        {
                            iTextSharp.text.Image image65 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image65.ScaleAbsolute(10f, 10f);
                            PdfPCell cell66 = new PdfPCell(image65);
                            cell66.HorizontalAlignment = 1;
                            cell66.VerticalAlignment = 5;
                            cell66.FixedHeight = 12f;
                            table8.AddCell(cell66);
                        }
                        if (oPTNote.TreatmentkneeLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_LEFT"].ToString()
                            iTextSharp.text.Image image66 = iTextSharp.text.Image.GetInstance(checkbox);
                            image66.ScaleAbsolute(10f, 10f);
                            PdfPCell cell67 = new PdfPCell(image66);
                            cell67.HorizontalAlignment = 1;
                            cell67.VerticalAlignment = 5;
                            cell67.FixedHeight = 12f;
                            table8.AddCell(cell67);
                        }
                        else
                        {
                            iTextSharp.text.Image image67 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image67.ScaleAbsolute(10f, 10f);
                            PdfPCell cell68 = new PdfPCell(image67);
                            cell68.HorizontalAlignment = 1;
                            cell68.VerticalAlignment = 5;
                            cell68.FixedHeight = 12f;
                            table8.AddCell(cell68);
                        }
                        if (oPTNote.TreatmentKneeBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_BOTH"].ToString()
                            iTextSharp.text.Image image68 = iTextSharp.text.Image.GetInstance(checkbox);
                            image68.ScaleAbsolute(10f, 10f);
                            PdfPCell cell69 = new PdfPCell(image68);
                            cell69.HorizontalAlignment = 1;
                            cell69.VerticalAlignment = 5;
                            cell69.FixedHeight = 12f;
                            table8.AddCell(cell69);
                        }
                        else
                        {
                            iTextSharp.text.Image image69 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image69.ScaleAbsolute(10f, 10f);
                            PdfPCell cell70 = new PdfPCell(image69);
                            cell70.HorizontalAlignment = 1;
                            cell70.VerticalAlignment = 5;
                            cell70.FixedHeight = 12f;
                            table8.AddCell(cell70);
                        }
                    }
                    if ((( oPTNote.TreatmentShoulderRight== "1") || ( oPTNote.TreatmentShoulderLeft== "1")) || ( oPTNote.TreatmentShoulderboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_BOTH"].ToString()
                        table8.AddCell(new Phrase("SHOULDER", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentShoulderRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_RIGHT"].ToString() 
                            iTextSharp.text.Image image70 = iTextSharp.text.Image.GetInstance(checkbox);
                            image70.ScaleAbsolute(10f, 10f);
                            PdfPCell cell71 = new PdfPCell(image70);
                            cell71.HorizontalAlignment = 1;
                            cell71.VerticalAlignment = 5;
                            cell71.FixedHeight = 12f;
                            table8.AddCell(cell71);
                        }
                        else
                        {
                            iTextSharp.text.Image image71 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image71.ScaleAbsolute(10f, 10f);
                            PdfPCell cell72 = new PdfPCell(image71);
                            cell72.HorizontalAlignment = 1;
                            cell72.VerticalAlignment = 5;
                            cell72.FixedHeight = 12f;
                            table8.AddCell(cell72);
                        }
                        if (oPTNote.TreatmentShoulderLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_LEFT"].ToString()
                            iTextSharp.text.Image image72 = iTextSharp.text.Image.GetInstance(checkbox);
                            image72.ScaleAbsolute(10f, 10f);
                            PdfPCell cell73 = new PdfPCell(image72);
                            cell73.HorizontalAlignment = 1;
                            cell73.VerticalAlignment = 5;
                            cell73.FixedHeight = 12f;
                            table8.AddCell(cell73);
                        }
                        else
                        {
                            iTextSharp.text.Image image73 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image73.ScaleAbsolute(10f, 10f);
                            PdfPCell cell74 = new PdfPCell(image73);
                            cell74.HorizontalAlignment = 1;
                            cell74.VerticalAlignment = 5;
                            cell74.FixedHeight = 12f;
                            table8.AddCell(cell74);
                        }
                        if (oPTNote.TreatmentShoulderboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_BOTH"].ToString()
                            iTextSharp.text.Image image74 = iTextSharp.text.Image.GetInstance(checkbox);
                            image74.ScaleAbsolute(10f, 10f);
                            PdfPCell cell75 = new PdfPCell(image74);
                            cell75.HorizontalAlignment = 1;
                            cell75.VerticalAlignment = 5;
                            cell75.FixedHeight = 12f;
                            table8.AddCell(cell75);
                        }
                        else
                        {
                            iTextSharp.text.Image image75 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image75.ScaleAbsolute(10f, 10f);
                            PdfPCell cell76 = new PdfPCell(image75);
                            cell76.HorizontalAlignment = 1;
                            cell76.VerticalAlignment = 5;
                            cell76.FixedHeight = 12f;
                            table8.AddCell(cell76);
                        }
                    }
                    if (((oPTNote.TreatmentLowerLegRight == "1") || (oPTNote.TreatmentLowerLegLeft == "1")) || ( oPTNote.TreatmentLowerLegboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_BOTH"].ToString()
                        table8.AddCell(new Phrase("LOWER LEG", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentLowerLegRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString()
                            iTextSharp.text.Image image76 = iTextSharp.text.Image.GetInstance(checkbox);
                            image76.ScaleAbsolute(10f, 10f);
                            PdfPCell cell77 = new PdfPCell(image76);
                            cell77.HorizontalAlignment = 1;
                            cell77.VerticalAlignment = 5;
                            cell77.FixedHeight = 12f;
                            table8.AddCell(cell77);
                        }
                        else
                        {
                            iTextSharp.text.Image image77 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image77.ScaleAbsolute(10f, 10f);
                            PdfPCell cell78 = new PdfPCell(image77);
                            cell78.HorizontalAlignment = 1;
                            cell78.VerticalAlignment = 5;
                            cell78.FixedHeight = 12f;
                            table8.AddCell(cell78);
                        }
                        if (oPTNote.TreatmentLowerLegLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_LEFT"].ToString()
                            iTextSharp.text.Image image78 = iTextSharp.text.Image.GetInstance(checkbox);
                            image78.ScaleAbsolute(10f, 10f);
                            PdfPCell cell79 = new PdfPCell(image78);
                            cell79.HorizontalAlignment = 1;
                            cell79.VerticalAlignment = 5;
                            cell79.FixedHeight = 12f;
                            table8.AddCell(cell79);
                        }
                        else
                        {
                            iTextSharp.text.Image image79 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image79.ScaleAbsolute(10f, 10f);
                            PdfPCell cell80 = new PdfPCell(image79);
                            cell80.HorizontalAlignment = 1;
                            cell80.VerticalAlignment = 5;
                            cell80.FixedHeight = 12f;
                            table8.AddCell(cell80);
                        }
                        if (oPTNote.TreatmentLowerLegboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_BOTH"].ToString()
                            iTextSharp.text.Image image80 = iTextSharp.text.Image.GetInstance(checkbox);
                            image80.ScaleAbsolute(10f, 10f);
                            PdfPCell cell81 = new PdfPCell(image80);
                            cell81.HorizontalAlignment = 1;
                            cell81.VerticalAlignment = 5;
                            cell81.FixedHeight = 12f;
                            table8.AddCell(cell81);
                        }
                        else
                        {
                            iTextSharp.text.Image image81 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image81.ScaleAbsolute(10f, 10f);
                            PdfPCell cell82 = new PdfPCell(image81);
                            cell82.HorizontalAlignment = 1;
                            cell82.VerticalAlignment = 5;
                            cell82.FixedHeight = 12f;
                            table8.AddCell(cell82);
                        }
                    }
                    if (((oPTNote.TreatmentElbowRight == "1") || (oPTNote.TreatmentElbowLeft == "1")) || (oPTNote.TreatmentElbowBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_BOTH"].ToString()
                        table8.AddCell(new Phrase("ELBOW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentElbowRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_RIGHT"].ToString()
                            iTextSharp.text.Image image82 = iTextSharp.text.Image.GetInstance(checkbox);
                            image82.ScaleAbsolute(10f, 10f);
                            PdfPCell cell83 = new PdfPCell(image82);
                            cell83.HorizontalAlignment = 1;
                            cell83.VerticalAlignment = 5;
                            cell83.FixedHeight = 12f;
                            table8.AddCell(cell83);
                        }
                        else
                        {
                            iTextSharp.text.Image image83 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image83.ScaleAbsolute(10f, 10f);
                            PdfPCell cell84 = new PdfPCell(image83);
                            cell84.HorizontalAlignment = 1;
                            cell84.VerticalAlignment = 5;
                            cell84.FixedHeight = 12f;
                            table8.AddCell(cell84);
                        }
                        if ( oPTNote.TreatmentElbowLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_LEFT"].ToString()
                            iTextSharp.text.Image image84 = iTextSharp.text.Image.GetInstance(checkbox);
                            image84.ScaleAbsolute(10f, 10f);
                            PdfPCell cell85 = new PdfPCell(image84);
                            cell85.HorizontalAlignment = 1;
                            cell85.VerticalAlignment = 5;
                            cell85.FixedHeight = 12f;
                            table8.AddCell(cell85);
                        }
                        else
                        {
                            iTextSharp.text.Image image85 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image85.ScaleAbsolute(10f, 10f);
                            PdfPCell cell86 = new PdfPCell(image85);
                            cell86.HorizontalAlignment = 1;
                            cell86.VerticalAlignment = 5;
                            cell86.FixedHeight = 12f;
                            table8.AddCell(cell86);
                        }
                        if (oPTNote.TreatmentElbowBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_BOTH"].ToString()
                            iTextSharp.text.Image image86 = iTextSharp.text.Image.GetInstance(checkbox);
                            image86.ScaleAbsolute(10f, 10f);
                            PdfPCell cell87 = new PdfPCell(image86);
                            cell87.HorizontalAlignment = 1;
                            cell87.VerticalAlignment = 5;
                            cell87.FixedHeight = 12f;
                            table8.AddCell(cell87);
                        }
                        else
                        {
                            iTextSharp.text.Image image87 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image87.ScaleAbsolute(10f, 10f);
                            PdfPCell cell88 = new PdfPCell(image87);
                            cell88.HorizontalAlignment = 1;
                            cell88.VerticalAlignment = 5;
                            cell88.FixedHeight = 12f;
                            table8.AddCell(cell88);
                        }
                    }
                    if (((oPTNote.TreatmentFootRight == "1") || (oPTNote.TreatmentFootLeft == "1")) || (oPTNote.TreatmentFootBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_RIGHT"].ToString()||ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_BOTH"].ToString()
                        table8.AddCell(new Phrase("FOOT", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentFootRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_RIGHT"].ToString()
                            iTextSharp.text.Image image88 = iTextSharp.text.Image.GetInstance(checkbox);
                            image88.ScaleAbsolute(10f, 10f);
                            PdfPCell cell89 = new PdfPCell(image88);
                            cell89.HorizontalAlignment = 1;
                            cell89.VerticalAlignment = 5;
                            cell89.FixedHeight = 12f;
                            table8.AddCell(cell89);
                        }
                        else
                        {
                            iTextSharp.text.Image image89 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image89.ScaleAbsolute(10f, 10f);
                            PdfPCell cell90 = new PdfPCell(image89);
                            cell90.HorizontalAlignment = 1;
                            cell90.VerticalAlignment = 5;
                            cell90.FixedHeight = 12f;
                            table8.AddCell(cell90);
                        }
                        if (oPTNote.TreatmentFootLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_LEFT"].ToString()
                            iTextSharp.text.Image image90 = iTextSharp.text.Image.GetInstance(checkbox);
                            image90.ScaleAbsolute(10f, 10f);
                            PdfPCell cell91 = new PdfPCell(image90);
                            cell91.HorizontalAlignment = 1;
                            cell91.VerticalAlignment = 5;
                            cell91.FixedHeight = 12f;
                            table8.AddCell(cell91);
                        }
                        else
                        {
                            iTextSharp.text.Image image91 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image91.ScaleAbsolute(10f, 10f);
                            PdfPCell cell92 = new PdfPCell(image91);
                            cell92.HorizontalAlignment = 1;
                            cell92.VerticalAlignment = 5;
                            cell92.FixedHeight = 12f;
                            table8.AddCell(cell92);
                        }
                        if (oPTNote.TreatmentFootBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_BOTH"].ToString()
                            iTextSharp.text.Image image92 = iTextSharp.text.Image.GetInstance(checkbox);
                            image92.ScaleAbsolute(10f, 10f);
                            PdfPCell cell93 = new PdfPCell(image92);
                            cell93.HorizontalAlignment = 1;
                            cell93.VerticalAlignment = 5;
                            cell93.FixedHeight = 12f;
                            table8.AddCell(cell93);
                        }
                        else
                        {
                            iTextSharp.text.Image image93 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image93.ScaleAbsolute(10f, 10f);
                            PdfPCell cell94 = new PdfPCell(image93);
                            cell94.HorizontalAlignment = 1;
                            cell94.VerticalAlignment = 5;
                            cell94.FixedHeight = 12f;
                            table8.AddCell(cell94);
                        }
                    }
                    if ((( oPTNote.TreatmentWristRight== "1") || (oPTNote.TreatmentWristLeft == "1")) || (oPTNote.TreatmentWristBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_BOTH"].ToString()
                        table8.AddCell(new Phrase("WRIST", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentWristRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_RIGHT"].ToString()
                            iTextSharp.text.Image image94 = iTextSharp.text.Image.GetInstance(checkbox);
                            image94.ScaleAbsolute(10f, 10f);
                            PdfPCell cell95 = new PdfPCell(image94);
                            cell95.HorizontalAlignment = 1;
                            cell95.VerticalAlignment = 5;
                            cell95.FixedHeight = 12f;
                            table8.AddCell(cell95);
                        }
                        else
                        {
                            iTextSharp.text.Image image95 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image95.ScaleAbsolute(10f, 10f);
                            PdfPCell cell96 = new PdfPCell(image95);
                            cell96.HorizontalAlignment = 1;
                            cell96.VerticalAlignment = 5;
                            cell96.FixedHeight = 12f;
                            table8.AddCell(cell96);
                        }
                        if (oPTNote.TreatmentWristLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_LEFT"].ToString()
                            iTextSharp.text.Image image96 = iTextSharp.text.Image.GetInstance(checkbox);
                            image96.ScaleAbsolute(10f, 10f);
                            PdfPCell cell97 = new PdfPCell(image96);
                            cell97.HorizontalAlignment = 1;
                            cell97.VerticalAlignment = 5;
                            cell97.FixedHeight = 12f;
                            table8.AddCell(cell97);
                        }
                        else
                        {
                            iTextSharp.text.Image image97 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image97.ScaleAbsolute(10f, 10f);
                            PdfPCell cell98 = new PdfPCell(image97);
                            cell98.HorizontalAlignment = 1;
                            cell98.VerticalAlignment = 5;
                            cell98.FixedHeight = 12f;
                            table8.AddCell(cell98);
                        }
                        if (oPTNote.TreatmentWristBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_BOTH"].ToString()
                            iTextSharp.text.Image image98 = iTextSharp.text.Image.GetInstance(checkbox);
                            image98.ScaleAbsolute(10f, 10f);
                            PdfPCell cell99 = new PdfPCell(image98);
                            cell99.HorizontalAlignment = 1;
                            cell99.VerticalAlignment = 5;
                            cell99.FixedHeight = 12f;
                            table8.AddCell(cell99);
                        }
                        else
                        {
                            iTextSharp.text.Image image99 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image99.ScaleAbsolute(10f, 10f);
                            PdfPCell cell100 = new PdfPCell(image99);
                            cell100.HorizontalAlignment = 1;
                            cell100.VerticalAlignment = 5;
                            cell100.FixedHeight = 12f;
                            table8.AddCell(cell100);
                        }
                    }
                    if (((oPTNote.TreatmentToesRight == "1") || (oPTNote.TreatmentToesLeft == "1")) || (oPTNote.TreatmentToesBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_BOTH"].ToString()
                        table8.AddCell(new Phrase("TOES", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentToesRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_RIGHT"].ToString()
                            iTextSharp.text.Image image100 = iTextSharp.text.Image.GetInstance(checkbox);
                            image100.ScaleAbsolute(10f, 10f);
                            PdfPCell cell101 = new PdfPCell(image100);
                            cell101.HorizontalAlignment = 1;
                            cell101.VerticalAlignment = 5;
                            cell101.FixedHeight = 12f;
                            table8.AddCell(cell101);
                        }
                        else
                        {
                            iTextSharp.text.Image image101 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image101.ScaleAbsolute(10f, 10f);
                            PdfPCell cell102 = new PdfPCell(image101);
                            cell102.HorizontalAlignment = 1;
                            cell102.VerticalAlignment = 5;
                            cell102.FixedHeight = 12f;
                            table8.AddCell(cell102);
                        }
                        if (oPTNote.TreatmentToesLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_LEFT"].ToString()
                            iTextSharp.text.Image image102 = iTextSharp.text.Image.GetInstance(checkbox);
                            image102.ScaleAbsolute(10f, 10f);
                            PdfPCell cell103 = new PdfPCell(image102);
                            cell103.HorizontalAlignment = 1;
                            cell103.VerticalAlignment = 5;
                            cell103.FixedHeight = 12f;
                            table8.AddCell(cell103);
                        }
                        else
                        {
                            iTextSharp.text.Image image103 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image103.ScaleAbsolute(10f, 10f);
                            PdfPCell cell104 = new PdfPCell(image103);
                            cell104.HorizontalAlignment = 1;
                            cell104.VerticalAlignment = 5;
                            cell104.FixedHeight = 12f;
                            table8.AddCell(cell104);
                        }
                        if (oPTNote.TreatmentToesBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_BOTH"].ToString()
                            iTextSharp.text.Image image104 = iTextSharp.text.Image.GetInstance(checkbox);
                            image104.ScaleAbsolute(10f, 10f);
                            PdfPCell cell105 = new PdfPCell(image104);
                            cell105.HorizontalAlignment = 1;
                            cell105.VerticalAlignment = 5;
                            cell105.FixedHeight = 12f;
                            table8.AddCell(cell105);
                        }
                        else
                        {
                            iTextSharp.text.Image image105 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image105.ScaleAbsolute(10f, 10f);
                            PdfPCell cell106 = new PdfPCell(image105);
                            cell106.HorizontalAlignment = 1;
                            cell106.VerticalAlignment = 5;
                            cell106.FixedHeight = 12f;
                            table8.AddCell(cell106);
                        }
                    }
                    if (((oPTNote.TreatmentArmRight == "1") || (oPTNote.TreatmentArmLeft == "1")) || (oPTNote.TreatmentArmBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_BOTH"].ToString()
                        table8.AddCell(new Phrase("ARM", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.TreatmentArmRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_RIGHT"].ToString()
                            iTextSharp.text.Image image106 = iTextSharp.text.Image.GetInstance(checkbox);
                            image106.ScaleAbsolute(10f, 10f);
                            PdfPCell cell107 = new PdfPCell(image106);
                            cell107.HorizontalAlignment = 1;
                            cell107.VerticalAlignment = 5;
                            cell107.FixedHeight = 12f;
                            table8.AddCell(cell107);
                        }
                        else
                        {
                            iTextSharp.text.Image image107 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image107.ScaleAbsolute(10f, 10f);
                            PdfPCell cell108 = new PdfPCell(image107);
                            cell108.HorizontalAlignment = 1;
                            cell108.VerticalAlignment = 5;
                            cell108.FixedHeight = 12f;
                            table8.AddCell(cell108);
                        }
                        if ( oPTNote.TreatmentArmLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_LEFT"].ToString()
                            iTextSharp.text.Image image108 = iTextSharp.text.Image.GetInstance(checkbox);
                            image108.ScaleAbsolute(10f, 10f);
                            PdfPCell cell109 = new PdfPCell(image108);
                            cell109.HorizontalAlignment = 1;
                            cell109.VerticalAlignment = 5;
                            cell109.FixedHeight = 12f;
                            table8.AddCell(cell109);
                        }
                        else
                        {
                            iTextSharp.text.Image image109 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image109.ScaleAbsolute(10f, 10f);
                            PdfPCell cell110 = new PdfPCell(image109);
                            cell110.HorizontalAlignment = 1;
                            cell110.VerticalAlignment = 5;
                            cell110.FixedHeight = 12f;
                            table8.AddCell(cell110);
                        }
                        if (oPTNote.TreatmentArmBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ARM_BOTH"].ToString()
                            iTextSharp.text.Image image110 = iTextSharp.text.Image.GetInstance(checkbox);
                            image110.ScaleAbsolute(10f, 10f);
                            PdfPCell cell111 = new PdfPCell(image110);
                            cell111.HorizontalAlignment = 1;
                            cell111.VerticalAlignment = 5;
                            cell111.FixedHeight = 12f;
                            table8.AddCell(cell111);
                        }
                        else
                        {
                            iTextSharp.text.Image image111 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image111.ScaleAbsolute(10f, 10f);
                            PdfPCell cell112 = new PdfPCell(image111);
                            cell112.HorizontalAlignment = 1;
                            cell112.VerticalAlignment = 5;
                            cell112.FixedHeight = 12f;
                            table8.AddCell(cell112);
                        }
                    }
                    if (((oPTNote.TreatmentForeArmRight == "1") || ( oPTNote.TreatmentForeArmLeft== "1")) || ( oPTNote.TreatmentForeArmBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_BOTH"].ToString()
                        table8.AddCell(new Phrase("FORE ARM", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.TreatmentForeArmRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_RIGHT"].ToString()
                            iTextSharp.text.Image image112 = iTextSharp.text.Image.GetInstance(checkbox);
                            image112.ScaleAbsolute(10f, 10f);
                            PdfPCell cell113 = new PdfPCell(image112);
                            cell113.HorizontalAlignment = 1;
                            cell113.VerticalAlignment = 5;
                            cell113.FixedHeight = 12f;
                            table8.AddCell(cell113);
                        }
                        else
                        {
                            iTextSharp.text.Image image113 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image113.ScaleAbsolute(10f, 10f);
                            PdfPCell cell114 = new PdfPCell(image113);
                            cell114.HorizontalAlignment = 1;
                            cell114.VerticalAlignment = 5;
                            cell114.FixedHeight = 12f;
                            table8.AddCell(cell114);
                        }
                        if (oPTNote.TreatmentForeArmLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_LEFT"].ToString()
                            iTextSharp.text.Image image114 = iTextSharp.text.Image.GetInstance(checkbox);
                            image114.ScaleAbsolute(10f, 10f);
                            PdfPCell cell115 = new PdfPCell(image114);
                            cell115.HorizontalAlignment = 1;
                            cell115.VerticalAlignment = 5;
                            cell115.FixedHeight = 12f;
                            table8.AddCell(cell115);
                        }
                        else
                        {
                            iTextSharp.text.Image image115 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image115.ScaleAbsolute(10f, 10f);
                            PdfPCell cell116 = new PdfPCell(image115);
                            cell116.HorizontalAlignment = 1;
                            cell116.VerticalAlignment = 5;
                            cell116.FixedHeight = 12f;
                            table8.AddCell(cell116);
                        }
                        if ( oPTNote.TreatmentForeArmBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_FORE_ARM_BOTH"].ToString()
                            iTextSharp.text.Image image116 = iTextSharp.text.Image.GetInstance(checkbox);
                            image116.ScaleAbsolute(10f, 10f);
                            PdfPCell cell117 = new PdfPCell(image116);
                            cell117.HorizontalAlignment = 1;
                            cell117.VerticalAlignment = 5;
                            cell117.FixedHeight = 12f;
                            table8.AddCell(cell117);
                        }
                        else
                        {
                            iTextSharp.text.Image image117 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image117.ScaleAbsolute(10f, 10f);
                            PdfPCell cell118 = new PdfPCell(image117);
                            cell118.HorizontalAlignment = 1;
                            cell118.VerticalAlignment = 5;
                            cell118.FixedHeight = 12f;
                            table8.AddCell(cell118);
                        }
                    }
                    if (((oPTNote.TreatmentAnkleRight == "1") || (oPTNote.TreatmentAnkleLeft == "1")) || (oPTNote.TreatmentAnkleBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_BOTH"].ToString()
                        table8.AddCell(new Phrase("ANKLE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.TreatmentAnkleRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_RIGHT"].ToString()
                            iTextSharp.text.Image image118 = iTextSharp.text.Image.GetInstance(checkbox);
                            image118.ScaleAbsolute(10f, 10f);
                            PdfPCell cell119 = new PdfPCell(image118);
                            cell119.HorizontalAlignment = 1;
                            cell119.VerticalAlignment = 5;
                            cell119.FixedHeight = 12f;
                            table8.AddCell(cell119);
                        }
                        else
                        {
                            iTextSharp.text.Image image119 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image119.ScaleAbsolute(10f, 10f);
                            PdfPCell cell120 = new PdfPCell(image119);
                            cell120.HorizontalAlignment = 1;
                            cell120.VerticalAlignment = 5;
                            cell120.FixedHeight = 12f;
                            table8.AddCell(cell120);
                        }
                        if (oPTNote.TreatmentAnkleLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_LEFT"].ToString()
                            iTextSharp.text.Image image120 = iTextSharp.text.Image.GetInstance(checkbox);
                            image120.ScaleAbsolute(10f, 10f);
                            PdfPCell cell121 = new PdfPCell(image120);
                            cell121.HorizontalAlignment = 1;
                            cell121.VerticalAlignment = 5;
                            cell121.FixedHeight = 12f;
                            table8.AddCell(cell121);
                        }
                        else
                        {
                            iTextSharp.text.Image image121 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image121.ScaleAbsolute(10f, 10f);
                            PdfPCell cell122 = new PdfPCell(image121);
                            cell122.HorizontalAlignment = 1;
                            cell122.VerticalAlignment = 5;
                            cell122.FixedHeight = 12f;
                            table8.AddCell(cell122);
                        }
                        if (oPTNote.TreatmentAnkleBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TREATMENT_ANKLE_BOTH"].ToString()
                            iTextSharp.text.Image image122 = iTextSharp.text.Image.GetInstance(checkbox);
                            image122.ScaleAbsolute(10f, 10f);
                            PdfPCell cell123 = new PdfPCell(image122);
                            cell123.HorizontalAlignment = 1;
                            cell123.VerticalAlignment = 5;
                            cell123.FixedHeight = 12f;
                            table8.AddCell(cell123);
                        }
                        else
                        {
                            iTextSharp.text.Image image123 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image123.ScaleAbsolute(10f, 10f);
                            PdfPCell cell124 = new PdfPCell(image123);
                            cell124.HorizontalAlignment = 1;
                            cell124.VerticalAlignment = 5;
                            cell124.FixedHeight = 12f;
                            table8.AddCell(cell124);
                        }
                    }
                    table8.DefaultCell.Border = 0;
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table7.AddCell(table8);
                    float[] numArray9 = new float[] { 2f, 1f, 1f, 1f, 1f };
                    PdfPTable table9 = new PdfPTable(numArray9);
                    table9.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table9.AddCell(new Phrase(""));
                    table9.AddCell(new Phrase("Right", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Left", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Both", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Pain Level", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (((oPTNote.Headacheright == "1") || ( oPTNote.Headacheleft== "1")) || ( oPTNote.Headacheboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString()
                        table9.AddCell(new Phrase("HEADACHE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.Headacheright == "1")
                        {//ptview.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString()
                            iTextSharp.text.Image image124 = iTextSharp.text.Image.GetInstance(checkbox);
                            image124.ScaleAbsolute(10f, 10f);
                            PdfPCell cell125 = new PdfPCell(image124);
                            cell125.HorizontalAlignment = 1;
                            cell125.VerticalAlignment = 5;
                            cell125.FixedHeight = 12f;
                            table9.AddCell(cell125);
                        }
                        else
                        {
                            iTextSharp.text.Image image125 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image125.ScaleAbsolute(10f, 10f);
                            PdfPCell cell126 = new PdfPCell(image125);
                            cell126.HorizontalAlignment = 1;
                            cell126.VerticalAlignment = 5;
                            cell126.FixedHeight = 12f;
                            table9.AddCell(cell126);
                        }
                        if ( oPTNote.Headacheleft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString()
                            iTextSharp.text.Image image126 = iTextSharp.text.Image.GetInstance(checkbox);
                            image126.ScaleAbsolute(10f, 10f);
                            PdfPCell cell127 = new PdfPCell(image126);
                            cell127.HorizontalAlignment = 1;
                            cell127.VerticalAlignment = 5;
                            cell127.FixedHeight = 12f;
                            table9.AddCell(cell127);
                        }
                        else
                        {
                            iTextSharp.text.Image image127 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image127.ScaleAbsolute(10f, 10f);
                            PdfPCell cell128 = new PdfPCell(image127);
                            cell128.HorizontalAlignment = 1;
                            cell128.VerticalAlignment = 5;
                            cell128.FixedHeight = 12f;
                            table9.AddCell(cell128);
                        }
                        if (oPTNote.Headacheboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString()
                            iTextSharp.text.Image image128 = iTextSharp.text.Image.GetInstance(checkbox);
                            image128.ScaleAbsolute(10f, 10f);
                            PdfPCell cell129 = new PdfPCell(image128);
                            cell129.HorizontalAlignment = 1;
                            cell129.VerticalAlignment = 5;
                            cell129.FixedHeight = 12f;
                            table9.AddCell(cell129);
                        }
                        else
                        {
                            iTextSharp.text.Image image129 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image129.ScaleAbsolute(10f, 10f);
                            PdfPCell cell130 = new PdfPCell(image129);
                            cell130.HorizontalAlignment = 1;
                            cell130.VerticalAlignment = 5;
                            cell130.FixedHeight = 12f;
                            table9.AddCell(cell130);
                        }
                        if (( oPTNote.PainlevelHeadache!= null) || ( oPTNote.PainlevelHeadache!= ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"]|ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelHeadache, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.HandRight== "1") || ( oPTNote.HandLeft== "1")) || ( oPTNote.HandBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString()
                        table9.AddCell(new Phrase("HAND", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.HandRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString()
                            iTextSharp.text.Image image130 = iTextSharp.text.Image.GetInstance(checkbox);
                            image130.ScaleAbsolute(10f, 10f);
                            PdfPCell cell131 = new PdfPCell(image130);
                            cell131.HorizontalAlignment = 1;
                            cell131.VerticalAlignment = 5;
                            cell131.FixedHeight = 12f;
                            table9.AddCell(cell131);
                        }
                        else
                        {
                            iTextSharp.text.Image image131 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image131.ScaleAbsolute(10f, 10f);
                            PdfPCell cell132 = new PdfPCell(image131);
                            cell132.HorizontalAlignment = 1;
                            cell132.VerticalAlignment = 5;
                            cell132.FixedHeight = 12f;
                            table9.AddCell(cell132);
                        }
                        if (oPTNote.HandLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString()
                            iTextSharp.text.Image image132 = iTextSharp.text.Image.GetInstance(checkbox);
                            image132.ScaleAbsolute(10f, 10f);
                            PdfPCell cell133 = new PdfPCell(image132);
                            cell133.HorizontalAlignment = 1;
                            cell133.VerticalAlignment = 5;
                            cell133.FixedHeight = 12f;
                            table9.AddCell(cell133);
                        }
                        else
                        {
                            iTextSharp.text.Image image133 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image133.ScaleAbsolute(10f, 10f);
                            PdfPCell cell134 = new PdfPCell(image133);
                            cell134.HorizontalAlignment = 1;
                            cell134.VerticalAlignment = 5;
                            cell134.FixedHeight = 12f;
                            table9.AddCell(cell134);
                        }
                        if ( oPTNote.HandBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString()
                            iTextSharp.text.Image image134 = iTextSharp.text.Image.GetInstance(checkbox);
                            image134.ScaleAbsolute(10f, 10f);
                            PdfPCell cell135 = new PdfPCell(image134);
                            cell135.HorizontalAlignment = 1;
                            cell135.VerticalAlignment = 5;
                            cell135.FixedHeight = 12f;
                            table9.AddCell(cell135);
                        }
                        else
                        {
                            iTextSharp.text.Image image135 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image135.ScaleAbsolute(10f, 10f);
                            PdfPCell cell136 = new PdfPCell(image135);
                            cell136.HorizontalAlignment = 1;
                            cell136.VerticalAlignment = 5;
                            cell136.FixedHeight = 12f;
                            table9.AddCell(cell136);
                        }
                        if ((oPTNote.PainlevelHand != null) || (oPTNote.PainlevelHand != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelHand, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.Neckright == "1") || ( oPTNote.Neckleft== "1")) || ( oPTNote.Neckboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString()
                        table9.AddCell(new Phrase("NECK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.Neckright == "1")
                        {//ptview.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString()
                            iTextSharp.text.Image image136 = iTextSharp.text.Image.GetInstance(checkbox);
                            image136.ScaleAbsolute(10f, 10f);
                            PdfPCell cell137 = new PdfPCell(image136);
                            cell137.HorizontalAlignment = 1;
                            cell137.VerticalAlignment = 5;
                            cell137.FixedHeight = 12f;
                            table9.AddCell(cell137);
                        }
                        else
                        {
                            iTextSharp.text.Image image137 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image137.ScaleAbsolute(10f, 10f);
                            PdfPCell cell138 = new PdfPCell(image137);
                            cell138.HorizontalAlignment = 1;
                            cell138.VerticalAlignment = 5;
                            cell138.FixedHeight = 12f;
                            table9.AddCell(cell138);
                        }
                        if (oPTNote.Neckleft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString()
                            iTextSharp.text.Image image138 = iTextSharp.text.Image.GetInstance(checkbox);
                            image138.ScaleAbsolute(10f, 10f);
                            PdfPCell cell139 = new PdfPCell(image138);
                            cell139.HorizontalAlignment = 1;
                            cell139.VerticalAlignment = 5;
                            cell139.FixedHeight = 12f;
                            table9.AddCell(cell139);
                        }
                        else
                        {
                            iTextSharp.text.Image image139 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image139.ScaleAbsolute(10f, 10f);
                            PdfPCell cell140 = new PdfPCell(image139);
                            cell140.HorizontalAlignment = 1;
                            cell140.VerticalAlignment = 5;
                            cell140.FixedHeight = 12f;
                            table9.AddCell(cell140);
                        }
                        if ( oPTNote.Neckboth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString()
                            iTextSharp.text.Image image140 = iTextSharp.text.Image.GetInstance(checkbox);
                            image140.ScaleAbsolute(10f, 10f);
                            PdfPCell cell141 = new PdfPCell(image140);
                            cell141.HorizontalAlignment = 1;
                            cell141.VerticalAlignment = 5;
                            cell141.FixedHeight = 12f;
                            table9.AddCell(cell141);
                        }
                        else
                        {
                            iTextSharp.text.Image image141 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image141.ScaleAbsolute(10f, 10f);
                            PdfPCell cell142 = new PdfPCell(image141);
                            cell142.HorizontalAlignment = 1;
                            cell142.VerticalAlignment = 5;
                            cell142.FixedHeight = 12f;
                            table9.AddCell(cell142);
                        }
                        if ((oPTNote.PainlevelNeck != null) || (oPTNote.PainlevelNeck != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelNeck, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.FingersRight== "1") || (oPTNote.FingersLeft == "1")) || (oPTNote.FingersBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString()
                        table9.AddCell(new Phrase("FINGERS", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.FingersRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString()
                            iTextSharp.text.Image image142 = iTextSharp.text.Image.GetInstance(checkbox);
                            image142.ScaleAbsolute(10f, 10f);
                            PdfPCell cell143 = new PdfPCell(image142);
                            cell143.HorizontalAlignment = 1;
                            cell143.VerticalAlignment = 5;
                            cell143.FixedHeight = 12f;
                            table9.AddCell(cell143);
                        }
                        else
                        {
                            iTextSharp.text.Image image143 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image143.ScaleAbsolute(10f, 10f);
                            PdfPCell cell144 = new PdfPCell(image143);
                            cell144.HorizontalAlignment = 1;
                            cell144.VerticalAlignment = 5;
                            cell144.FixedHeight = 12f;
                            table9.AddCell(cell144);
                        }
                        if ( oPTNote.FingersLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString()
                            iTextSharp.text.Image image144 = iTextSharp.text.Image.GetInstance(checkbox);
                            image144.ScaleAbsolute(10f, 10f);
                            PdfPCell cell145 = new PdfPCell(image144);
                            cell145.HorizontalAlignment = 1;
                            cell145.VerticalAlignment = 5;
                            cell145.FixedHeight = 12f;
                            table9.AddCell(cell145);
                        }
                        else
                        {
                            iTextSharp.text.Image image145 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image145.ScaleAbsolute(10f, 10f);
                            PdfPCell cell146 = new PdfPCell(image145);
                            cell146.HorizontalAlignment = 1;
                            cell146.VerticalAlignment = 5;
                            cell146.FixedHeight = 12f;
                            table9.AddCell(cell146);
                        }
                        if (oPTNote.FingersBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString()
                            iTextSharp.text.Image image146 = iTextSharp.text.Image.GetInstance(checkbox);
                            image146.ScaleAbsolute(10f, 10f);
                            PdfPCell cell147 = new PdfPCell(image146);
                            cell147.HorizontalAlignment = 1;
                            cell147.VerticalAlignment = 5;
                            cell147.FixedHeight = 12f;
                            table9.AddCell(cell147);
                        }
                        else
                        {
                            iTextSharp.text.Image image147 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image147.ScaleAbsolute(10f, 10f);
                            PdfPCell cell148 = new PdfPCell(image147);
                            cell148.HorizontalAlignment = 1;
                            cell148.VerticalAlignment = 5;
                            cell148.FixedHeight = 12f;
                            table9.AddCell(cell148);
                        }
                        if ((oPTNote.PainlevelFingers != null) || (oPTNote.PainlevelFingers != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelFingers, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.Midbackright == "1") || ( oPTNote.Midbackleft== "1")) || ( oPTNote.Midbackboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString()
                        table9.AddCell(new Phrase("MID BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.Midbackright == "1")
                        {//ptview.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image148 = iTextSharp.text.Image.GetInstance(checkbox);
                            image148.ScaleAbsolute(10f, 10f);
                            PdfPCell cell149 = new PdfPCell(image148);
                            cell149.HorizontalAlignment = 1;
                            cell149.VerticalAlignment = 5;
                            cell149.FixedHeight = 12f;
                            table9.AddCell(cell149);
                        }
                        else
                        {
                            iTextSharp.text.Image image149 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image149.ScaleAbsolute(10f, 10f);
                            PdfPCell cell150 = new PdfPCell(image149);
                            cell150.HorizontalAlignment = 1;
                            cell150.VerticalAlignment = 5;
                            cell150.FixedHeight = 12f;
                            table9.AddCell(cell150);
                        }
                        if ( oPTNote.Midbackleft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString()
                            iTextSharp.text.Image image150 = iTextSharp.text.Image.GetInstance(checkbox);
                            image150.ScaleAbsolute(10f, 10f);
                            PdfPCell cell151 = new PdfPCell(image150);
                            cell151.HorizontalAlignment = 1;
                            cell151.VerticalAlignment = 5;
                            cell151.FixedHeight = 12f;
                            table9.AddCell(cell151);
                        }
                        else
                        {
                            iTextSharp.text.Image image151 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image151.ScaleAbsolute(10f, 10f);
                            PdfPCell cell152 = new PdfPCell(image151);
                            cell152.HorizontalAlignment = 1;
                            cell152.VerticalAlignment = 5;
                            cell152.FixedHeight = 12f;
                            table9.AddCell(cell152);
                        }
                        if ( oPTNote.Midbackboth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image152 = iTextSharp.text.Image.GetInstance(checkbox);
                            image152.ScaleAbsolute(10f, 10f);
                            PdfPCell cell153 = new PdfPCell(image152);
                            cell153.HorizontalAlignment = 1;
                            cell153.VerticalAlignment = 5;
                            cell153.FixedHeight = 12f;
                            table9.AddCell(cell153);
                        }
                        else
                        {
                            iTextSharp.text.Image image153 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image153.ScaleAbsolute(10f, 10f);
                            PdfPCell cell154 = new PdfPCell(image153);
                            cell154.HorizontalAlignment = 1;
                            cell154.VerticalAlignment = 5;
                            cell154.FixedHeight = 12f;
                            table9.AddCell(cell154);
                        }
                        if ((oPTNote.PainlevelMidback != null) || (oPTNote.PainlevelMidback != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelMidback, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.HipRight== "1") || (oPTNote.HipLeft == "1")) || (oPTNote.HipBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString()
                        table9.AddCell(new Phrase("HIP", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.HipRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString()
                            iTextSharp.text.Image image154 = iTextSharp.text.Image.GetInstance(checkbox);
                            image154.ScaleAbsolute(10f, 10f);
                            PdfPCell cell155 = new PdfPCell(image154);
                            cell155.HorizontalAlignment = 1;
                            cell155.VerticalAlignment = 5;
                            cell155.FixedHeight = 12f;
                            table9.AddCell(cell155);
                        }
                        else
                        {
                            iTextSharp.text.Image image155 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image155.ScaleAbsolute(10f, 10f);
                            PdfPCell cell156 = new PdfPCell(image155);
                            cell156.HorizontalAlignment = 1;
                            cell156.VerticalAlignment = 5;
                            cell156.FixedHeight = 12f;
                            table9.AddCell(cell156);
                        }
                        if ( oPTNote.HipLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString()
                            iTextSharp.text.Image image156 = iTextSharp.text.Image.GetInstance(checkbox);
                            image156.ScaleAbsolute(10f, 10f);
                            PdfPCell cell157 = new PdfPCell(image156);
                            cell157.HorizontalAlignment = 1;
                            cell157.VerticalAlignment = 5;
                            cell157.FixedHeight = 12f;
                            table9.AddCell(cell157);
                        }
                        else
                        {
                            iTextSharp.text.Image image157 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image157.ScaleAbsolute(10f, 10f);
                            PdfPCell cell158 = new PdfPCell(image157);
                            cell158.HorizontalAlignment = 1;
                            cell158.VerticalAlignment = 5;
                            cell158.FixedHeight = 12f;
                            table9.AddCell(cell158);
                        }
                        if (oPTNote.HipBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString()
                            iTextSharp.text.Image image158 = iTextSharp.text.Image.GetInstance(checkbox);
                            image158.ScaleAbsolute(10f, 10f);
                            PdfPCell cell159 = new PdfPCell(image158);
                            cell159.HorizontalAlignment = 1;
                            cell159.VerticalAlignment = 5;
                            cell159.FixedHeight = 12f;
                            table9.AddCell(cell159);
                        }
                        else
                        {
                            iTextSharp.text.Image image159 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image159.ScaleAbsolute(10f, 10f);
                            PdfPCell cell160 = new PdfPCell(image159);
                            cell160.HorizontalAlignment = 1;
                            cell160.VerticalAlignment = 5;
                            cell160.FixedHeight = 12f;
                            table9.AddCell(cell160);
                        }
                        if ((oPTNote.PainlevelHip != null) || (oPTNote.PainlevelHip != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelHip, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.Lowbackright== "1") || ( oPTNote.Lowbackleft== "1")) || (oPTNote.Lowbackboth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString()
                        table9.AddCell(new Phrase("LOW BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.Lowbackright== "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image160 = iTextSharp.text.Image.GetInstance(checkbox);
                            image160.ScaleAbsolute(10f, 10f);
                            PdfPCell cell161 = new PdfPCell(image160);
                            cell161.HorizontalAlignment = 1;
                            cell161.VerticalAlignment = 5;
                            cell161.FixedHeight = 12f;
                            table9.AddCell(cell161);
                        }
                        else
                        {
                            iTextSharp.text.Image image161 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image161.ScaleAbsolute(10f, 10f);
                            PdfPCell cell162 = new PdfPCell(image161);
                            cell162.HorizontalAlignment = 1;
                            cell162.VerticalAlignment = 5;
                            cell162.FixedHeight = 12f;
                            table9.AddCell(cell162);
                        }
                        if (oPTNote.Lowbackleft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString()
                            iTextSharp.text.Image image162 = iTextSharp.text.Image.GetInstance(checkbox);
                            image162.ScaleAbsolute(10f, 10f);
                            PdfPCell cell163 = new PdfPCell(image162);
                            cell163.HorizontalAlignment = 1;
                            cell163.VerticalAlignment = 5;
                            cell163.FixedHeight = 12f;
                            table9.AddCell(cell163);
                        }
                        else
                        {
                            iTextSharp.text.Image image163 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image163.ScaleAbsolute(10f, 10f);
                            PdfPCell cell164 = new PdfPCell(image163);
                            cell164.HorizontalAlignment = 1;
                            cell164.VerticalAlignment = 5;
                            cell164.FixedHeight = 12f;
                            table9.AddCell(cell164);
                        }
                        if (oPTNote.Lowbackboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image164 = iTextSharp.text.Image.GetInstance(checkbox);
                            image164.ScaleAbsolute(10f, 10f);
                            PdfPCell cell165 = new PdfPCell(image164);
                            cell165.HorizontalAlignment = 1;
                            cell165.VerticalAlignment = 5;
                            cell165.FixedHeight = 12f;
                            table9.AddCell(cell165);
                        }
                        else
                        {
                            iTextSharp.text.Image image165 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image165.ScaleAbsolute(10f, 10f);
                            PdfPCell cell166 = new PdfPCell(image165);
                            cell166.HorizontalAlignment = 1;
                            cell166.VerticalAlignment = 5;
                            cell166.FixedHeight = 12f;
                            table9.AddCell(cell166);
                        }
                        if ((oPTNote.PainlevelLowback != null) || (oPTNote.PainlevelLowback != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelLowback, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.ThighRight== "1") || ( oPTNote.ThighLeft== "1")) || (oPTNote.ThighBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString()
                        table9.AddCell(new Phrase("THIGH", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.ThighRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString()
                            iTextSharp.text.Image image166 = iTextSharp.text.Image.GetInstance(checkbox);
                            image166.ScaleAbsolute(10f, 10f);
                            PdfPCell cell167 = new PdfPCell(image166);
                            cell167.HorizontalAlignment = 1;
                            cell167.VerticalAlignment = 5;
                            cell167.FixedHeight = 12f;
                            table9.AddCell(cell167);
                        }
                        else
                        {
                            iTextSharp.text.Image image167 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image167.ScaleAbsolute(10f, 10f);
                            PdfPCell cell168 = new PdfPCell(image167);
                            cell168.HorizontalAlignment = 1;
                            cell168.VerticalAlignment = 5;
                            cell168.FixedHeight = 12f;
                            table9.AddCell(cell168);
                        }
                        if ( oPTNote.ThighLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString()
                            iTextSharp.text.Image image168 = iTextSharp.text.Image.GetInstance(checkbox);
                            image168.ScaleAbsolute(10f, 10f);
                            PdfPCell cell169 = new PdfPCell(image168);
                            cell169.HorizontalAlignment = 1;
                            cell169.VerticalAlignment = 5;
                            cell169.FixedHeight = 12f;
                            table9.AddCell(cell169);
                        }
                        else
                        {
                            iTextSharp.text.Image image169 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image169.ScaleAbsolute(10f, 10f);
                            PdfPCell cell170 = new PdfPCell(image169);
                            cell170.HorizontalAlignment = 1;
                            cell170.VerticalAlignment = 5;
                            cell170.FixedHeight = 12f;
                            table9.AddCell(cell170);
                        }
                        if (oPTNote.ThighBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString()
                            iTextSharp.text.Image image170 = iTextSharp.text.Image.GetInstance(checkbox);
                            image170.ScaleAbsolute(10f, 10f);
                            PdfPCell cell171 = new PdfPCell(image170);
                            cell171.HorizontalAlignment = 1;
                            cell171.VerticalAlignment = 5;
                            cell171.FixedHeight = 12f;
                            table9.AddCell(cell171);
                        }
                        else
                        {
                            iTextSharp.text.Image image171 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image171.ScaleAbsolute(10f, 10f);
                            PdfPCell cell172 = new PdfPCell(image171);
                            cell172.HorizontalAlignment = 1;
                            cell172.VerticalAlignment = 5;
                            cell172.FixedHeight = 12f;
                            table9.AddCell(cell172);
                        }
                        if ((oPTNote.PainlevelThigh != null) || (oPTNote.PainlevelThigh != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelThigh, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.Jawright == "1") || ( oPTNote.Jawleft== "1")) || ( oPTNote.Jawboth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString()
                        table9.AddCell(new Phrase("JAW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.Jawright == "1")
                        {//ptview.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString()
                            iTextSharp.text.Image image172 = iTextSharp.text.Image.GetInstance(checkbox);
                            image172.ScaleAbsolute(10f, 10f);
                            PdfPCell cell173 = new PdfPCell(image172);
                            cell173.HorizontalAlignment = 1;
                            cell173.VerticalAlignment = 5;
                            cell173.FixedHeight = 12f;
                            table9.AddCell(cell173);
                        }
                        else
                        {
                            iTextSharp.text.Image image173 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image173.ScaleAbsolute(10f, 10f);
                            PdfPCell cell174 = new PdfPCell(image173);
                            cell174.HorizontalAlignment = 1;
                            cell174.VerticalAlignment = 5;
                            cell174.FixedHeight = 12f;
                            table9.AddCell(cell174);
                        }
                        if (oPTNote.Jawleft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString()
                            iTextSharp.text.Image image174 = iTextSharp.text.Image.GetInstance(checkbox);
                            image174.ScaleAbsolute(10f, 10f);
                            PdfPCell cell175 = new PdfPCell(image174);
                            cell175.HorizontalAlignment = 1;
                            cell175.VerticalAlignment = 5;
                            cell175.FixedHeight = 12f;
                            table9.AddCell(cell175);
                        }
                        else
                        {
                            iTextSharp.text.Image image175 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image175.ScaleAbsolute(10f, 10f);
                            PdfPCell cell176 = new PdfPCell(image175);
                            cell176.HorizontalAlignment = 1;
                            cell176.VerticalAlignment = 5;
                            cell176.FixedHeight = 12f;
                            table9.AddCell(cell176);
                        }
                        if ( oPTNote.Jawboth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString()
                            iTextSharp.text.Image image176 = iTextSharp.text.Image.GetInstance(checkbox);
                            image176.ScaleAbsolute(10f, 10f);
                            PdfPCell cell177 = new PdfPCell(image176);
                            cell177.HorizontalAlignment = 1;
                            cell177.VerticalAlignment = 5;
                            cell177.FixedHeight = 12f;
                            table9.AddCell(cell177);
                        }
                        else
                        {
                            iTextSharp.text.Image image177 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image177.ScaleAbsolute(10f, 10f);
                            PdfPCell cell178 = new PdfPCell(image177);
                            cell178.HorizontalAlignment = 1;
                            cell178.VerticalAlignment = 5;
                            cell178.FixedHeight = 12f;
                            table9.AddCell(cell178);
                        }
                        if ((oPTNote.PainlevelJaw != null) || (oPTNote.PainlevelJaw != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelJaw, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.KneeRight == "1") || ( oPTNote.KneeLeft== "1")) || ( oPTNote.KneeBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString()
                        table9.AddCell(new Phrase("KNEE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.KneeRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString()
                            iTextSharp.text.Image image178 = iTextSharp.text.Image.GetInstance(checkbox);
                            image178.ScaleAbsolute(10f, 10f);
                            PdfPCell cell179 = new PdfPCell(image178);
                            cell179.HorizontalAlignment = 1;
                            cell179.VerticalAlignment = 5;
                            cell179.FixedHeight = 12f;
                            table9.AddCell(cell179);
                        }
                        else
                        {
                            iTextSharp.text.Image image179 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image179.ScaleAbsolute(10f, 10f);
                            PdfPCell cell180 = new PdfPCell(image179);
                            cell180.HorizontalAlignment = 1;
                            cell180.VerticalAlignment = 5;
                            cell180.FixedHeight = 12f;
                            table9.AddCell(cell180);
                        }
                        if (oPTNote.KneeLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString()
                            iTextSharp.text.Image image180 = iTextSharp.text.Image.GetInstance(checkbox);
                            image180.ScaleAbsolute(10f, 10f);
                            PdfPCell cell181 = new PdfPCell(image180);
                            cell181.HorizontalAlignment = 1;
                            cell181.VerticalAlignment = 5;
                            cell181.FixedHeight = 12f;
                            table9.AddCell(cell181);
                        }
                        else
                        {
                            iTextSharp.text.Image image181 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image181.ScaleAbsolute(10f, 10f);
                            PdfPCell cell182 = new PdfPCell(image181);
                            cell182.HorizontalAlignment = 1;
                            cell182.VerticalAlignment = 5;
                            cell182.FixedHeight = 12f;
                            table9.AddCell(cell182);
                        }
                        if ( oPTNote.KneeBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString()
                            iTextSharp.text.Image image182 = iTextSharp.text.Image.GetInstance(checkbox);
                            image182.ScaleAbsolute(10f, 10f);
                            PdfPCell cell183 = new PdfPCell(image182);
                            cell183.HorizontalAlignment = 1;
                            cell183.VerticalAlignment = 5;
                            cell183.FixedHeight = 12f;
                            table9.AddCell(cell183);
                        }
                        else
                        {
                            iTextSharp.text.Image image183 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image183.ScaleAbsolute(10f, 10f);
                            PdfPCell cell184 = new PdfPCell(image183);
                            cell184.HorizontalAlignment = 1;
                            cell184.VerticalAlignment = 5;
                            cell184.FixedHeight = 12f;
                            table9.AddCell(cell184);
                        }
                        if ((oPTNote.PainlevelKnee != null) || (oPTNote.PainlevelKnee != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelKnee, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.ShoulderRight == "1") || (oPTNote.ShoulderLeft == "1")) || ( oPTNote.ShoulderBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString()\ptview.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString()
                        table9.AddCell(new Phrase("SHOULDER", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.ShoulderRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString()
                            iTextSharp.text.Image image184 = iTextSharp.text.Image.GetInstance(checkbox);
                            image184.ScaleAbsolute(10f, 10f);
                            PdfPCell cell185 = new PdfPCell(image184);
                            cell185.HorizontalAlignment = 1;
                            cell185.VerticalAlignment = 5;
                            cell185.FixedHeight = 12f;
                            table9.AddCell(cell185);
                        }
                        else
                        {
                            iTextSharp.text.Image image185 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image185.ScaleAbsolute(10f, 10f);
                            PdfPCell cell186 = new PdfPCell(image185);
                            cell186.HorizontalAlignment = 1;
                            cell186.VerticalAlignment = 5;
                            cell186.FixedHeight = 12f;
                            table9.AddCell(cell186);
                        }
                        if (oPTNote.ShoulderLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString()
                            iTextSharp.text.Image image186 = iTextSharp.text.Image.GetInstance(checkbox);
                            image186.ScaleAbsolute(10f, 10f);
                            PdfPCell cell187 = new PdfPCell(image186);
                            cell187.HorizontalAlignment = 1;
                            cell187.VerticalAlignment = 5;
                            cell187.FixedHeight = 12f;
                            table9.AddCell(cell187);
                        }
                        else
                        {
                            iTextSharp.text.Image image187 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image187.ScaleAbsolute(10f, 10f);
                            PdfPCell cell188 = new PdfPCell(image187);
                            cell188.HorizontalAlignment = 1;
                            cell188.VerticalAlignment = 5;
                            cell188.FixedHeight = 12f;
                            table9.AddCell(cell188);
                        }
                        if (oPTNote.ShoulderBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString()
                            iTextSharp.text.Image image188 = iTextSharp.text.Image.GetInstance(checkbox);
                            image188.ScaleAbsolute(10f, 10f);
                            PdfPCell cell189 = new PdfPCell(image188);
                            cell189.HorizontalAlignment = 1;
                            cell189.VerticalAlignment = 5;
                            cell189.FixedHeight = 12f;
                            table9.AddCell(cell189);
                        }
                        else
                        {
                            iTextSharp.text.Image image189 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image189.ScaleAbsolute(10f, 10f);
                            PdfPCell cell190 = new PdfPCell(image189);
                            cell190.HorizontalAlignment = 1;
                            cell190.VerticalAlignment = 5;
                            cell190.FixedHeight = 12f;
                            table9.AddCell(cell190);
                        }
                        if ((oPTNote.PainlevelShoulder != null) || (oPTNote.PainlevelShoulder == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelShoulder, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.LowerlegRight == "1") || (oPTNote.LowerlegLeft == "1")) || (oPTNote.LowerlegBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString()
                        table9.AddCell(new Phrase("LOWER LEG", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.LowerlegRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString()
                            iTextSharp.text.Image image190 = iTextSharp.text.Image.GetInstance(checkbox);
                            image190.ScaleAbsolute(10f, 10f);
                            PdfPCell cell191 = new PdfPCell(image190);
                            cell191.HorizontalAlignment = 1;
                            cell191.VerticalAlignment = 5;
                            cell191.FixedHeight = 12f;
                            table9.AddCell(cell191);
                        }
                        else
                        {
                            iTextSharp.text.Image image191 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image191.ScaleAbsolute(10f, 10f);
                            PdfPCell cell192 = new PdfPCell(image191);
                            cell192.HorizontalAlignment = 1;
                            cell192.VerticalAlignment = 5;
                            cell192.FixedHeight = 12f;
                            table9.AddCell(cell192);
                        }
                        if (oPTNote.LowerlegLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString()
                            iTextSharp.text.Image image192 = iTextSharp.text.Image.GetInstance(checkbox);
                            image192.ScaleAbsolute(10f, 10f);
                            PdfPCell cell193 = new PdfPCell(image192);
                            cell193.HorizontalAlignment = 1;
                            cell193.VerticalAlignment = 5;
                            cell193.FixedHeight = 12f;
                            table9.AddCell(cell193);
                        }
                        else
                        {
                            iTextSharp.text.Image image193 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image193.ScaleAbsolute(10f, 10f);
                            PdfPCell cell194 = new PdfPCell(image193);
                            cell194.HorizontalAlignment = 1;
                            cell194.VerticalAlignment = 5;
                            cell194.FixedHeight = 12f;
                            table9.AddCell(cell194);
                        }
                        if (oPTNote.LowerlegBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString()
                            iTextSharp.text.Image image194 = iTextSharp.text.Image.GetInstance(checkbox);
                            image194.ScaleAbsolute(10f, 10f);
                            PdfPCell cell195 = new PdfPCell(image194);
                            cell195.HorizontalAlignment = 1;
                            cell195.VerticalAlignment = 5;
                            cell195.FixedHeight = 12f;
                            table9.AddCell(cell195);
                        }
                        else
                        {
                            iTextSharp.text.Image image195 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image195.ScaleAbsolute(10f, 10f);
                            PdfPCell cell196 = new PdfPCell(image195);
                            cell196.HorizontalAlignment = 1;
                            cell196.VerticalAlignment = 5;
                            cell196.FixedHeight = 12f;
                            table9.AddCell(cell196);
                        }
                        if ((oPTNote.PainlevelLowerleg != null) || (oPTNote.PainlevelLowerleg == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelLowerleg, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.ElbowRight== "1") || (oPTNote.ElbowLeft == "1")) || (oPTNote.Elbowboth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString()
                        table9.AddCell(new Phrase("ELBOW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.ElbowRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString()
                            iTextSharp.text.Image image196 = iTextSharp.text.Image.GetInstance(checkbox);
                            image196.ScaleAbsolute(10f, 10f);
                            PdfPCell cell197 = new PdfPCell(image196);
                            cell197.HorizontalAlignment = 1;
                            cell197.VerticalAlignment = 5;
                            cell197.FixedHeight = 12f;
                            table9.AddCell(cell197);
                        }
                        else
                        {
                            iTextSharp.text.Image image197 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image197.ScaleAbsolute(10f, 10f);
                            PdfPCell cell198 = new PdfPCell(image197);
                            cell198.HorizontalAlignment = 1;
                            cell198.VerticalAlignment = 5;
                            cell198.FixedHeight = 12f;
                            table9.AddCell(cell198);
                        }
                        if (oPTNote.ElbowLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString()
                            iTextSharp.text.Image image198 = iTextSharp.text.Image.GetInstance(checkbox);
                            image198.ScaleAbsolute(10f, 10f);
                            PdfPCell cell199 = new PdfPCell(image198);
                            cell199.HorizontalAlignment = 1;
                            cell199.VerticalAlignment = 5;
                            cell199.FixedHeight = 12f;
                            table9.AddCell(cell199);
                        }
                        else
                        {
                            iTextSharp.text.Image image199 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image199.ScaleAbsolute(10f, 10f);
                            PdfPCell cell200 = new PdfPCell(image199);
                            cell200.HorizontalAlignment = 1;
                            cell200.VerticalAlignment = 5;
                            cell200.FixedHeight = 12f;
                            table9.AddCell(cell200);
                        }
                        if (oPTNote.Elbowboth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString()
                            iTextSharp.text.Image image200 = iTextSharp.text.Image.GetInstance(checkbox);
                            image200.ScaleAbsolute(10f, 10f);
                            PdfPCell cell201 = new PdfPCell(image200);
                            cell201.HorizontalAlignment = 1;
                            cell201.VerticalAlignment = 5;
                            cell201.FixedHeight = 12f;
                            table9.AddCell(cell201);
                        }
                        else
                        {
                            iTextSharp.text.Image image201 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image201.ScaleAbsolute(10f, 10f);
                            PdfPCell cell202 = new PdfPCell(image201);
                            cell202.HorizontalAlignment = 1;
                            cell202.VerticalAlignment = 5;
                            cell202.FixedHeight = 12f;
                            table9.AddCell(cell202);
                        }
                        if ((oPTNote.PainlevelElbow != null) || (oPTNote.PainlevelElbow == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelElbow, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.FootRight== "1") || (oPTNote.FootLeft == "1")) || (oPTNote.FootBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString()
                        table9.AddCell(new Phrase("FOOT", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.FootRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString()
                            iTextSharp.text.Image image202 = iTextSharp.text.Image.GetInstance(checkbox);
                            image202.ScaleAbsolute(10f, 10f);
                            PdfPCell cell203 = new PdfPCell(image202);
                            cell203.HorizontalAlignment = 1;
                            cell203.VerticalAlignment = 5;
                            cell203.FixedHeight = 12f;
                            table9.AddCell(cell203);
                        }
                        else
                        {
                            iTextSharp.text.Image image203 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image203.ScaleAbsolute(10f, 10f);
                            PdfPCell cell204 = new PdfPCell(image203);
                            cell204.HorizontalAlignment = 1;
                            cell204.VerticalAlignment = 5;
                            cell204.FixedHeight = 12f;
                            table9.AddCell(cell204);
                        }
                        if (oPTNote.FootLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString()
                            iTextSharp.text.Image image204 = iTextSharp.text.Image.GetInstance(checkbox);
                            image204.ScaleAbsolute(10f, 10f);
                            PdfPCell cell205 = new PdfPCell(image204);
                            cell205.HorizontalAlignment = 1;
                            cell205.VerticalAlignment = 5;
                            cell205.FixedHeight = 12f;
                            table9.AddCell(cell205);
                        }
                        else
                        {
                            iTextSharp.text.Image image205 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image205.ScaleAbsolute(10f, 10f);
                            PdfPCell cell206 = new PdfPCell(image205);
                            cell206.HorizontalAlignment = 1;
                            cell206.VerticalAlignment = 5;
                            cell206.FixedHeight = 12f;
                            table9.AddCell(cell206);
                        }
                        if (oPTNote.FootBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString()
                            iTextSharp.text.Image image206 = iTextSharp.text.Image.GetInstance(checkbox);
                            image206.ScaleAbsolute(10f, 10f);
                            PdfPCell cell207 = new PdfPCell(image206);
                            cell207.HorizontalAlignment = 1;
                            cell207.VerticalAlignment = 5;
                            cell207.FixedHeight = 12f;
                            table9.AddCell(cell207);
                        }
                        else
                        {
                            iTextSharp.text.Image image207 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image207.ScaleAbsolute(10f, 10f);
                            PdfPCell cell208 = new PdfPCell(image207);
                            cell208.HorizontalAlignment = 1;
                            cell208.VerticalAlignment = 5;
                            cell208.FixedHeight = 12f;
                            table9.AddCell(cell208);
                        }
                        if ((oPTNote.PainlevelFoot != null) || (oPTNote.PainlevelFoot == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelFoot, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.WristRight== "1") || (oPTNote.WristLeft == "1")) || (oPTNote.WristBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() |ptview.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString()
                        table9.AddCell(new Phrase("WRIST", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.WristRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString()
                            iTextSharp.text.Image image208 = iTextSharp.text.Image.GetInstance(checkbox);
                            image208.ScaleAbsolute(10f, 10f);
                            PdfPCell cell209 = new PdfPCell(image208);
                            cell209.HorizontalAlignment = 1;
                            cell209.VerticalAlignment = 5;
                            cell209.FixedHeight = 12f;
                            table9.AddCell(cell209);
                        }
                        else
                        {
                            iTextSharp.text.Image image209 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image209.ScaleAbsolute(10f, 10f);
                            PdfPCell cell210 = new PdfPCell(image209);
                            cell210.HorizontalAlignment = 1;
                            cell210.VerticalAlignment = 5;
                            cell210.FixedHeight = 12f;
                            table9.AddCell(cell210);
                        }
                        if (oPTNote.WristLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString()
                            iTextSharp.text.Image image210 = iTextSharp.text.Image.GetInstance(checkbox);
                            image210.ScaleAbsolute(10f, 10f);
                            PdfPCell cell211 = new PdfPCell(image210);
                            cell211.HorizontalAlignment = 1;
                            cell211.VerticalAlignment = 5;
                            cell211.FixedHeight = 12f;
                            table9.AddCell(cell211);
                        }
                        else
                        {
                            iTextSharp.text.Image image211 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image211.ScaleAbsolute(10f, 10f);
                            PdfPCell cell212 = new PdfPCell(image211);
                            cell212.HorizontalAlignment = 1;
                            cell212.VerticalAlignment = 5;
                            cell212.FixedHeight = 12f;
                            table9.AddCell(cell212);
                        }
                        if (oPTNote.WristBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString()
                            iTextSharp.text.Image image212 = iTextSharp.text.Image.GetInstance(checkbox);
                            image212.ScaleAbsolute(10f, 10f);
                            PdfPCell cell213 = new PdfPCell(image212);
                            cell213.HorizontalAlignment = 1;
                            cell213.VerticalAlignment = 5;
                            cell213.FixedHeight = 12f;
                            table9.AddCell(cell213);
                        }
                        else
                        {
                            iTextSharp.text.Image image213 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image213.ScaleAbsolute(10f, 10f);
                            PdfPCell cell214 = new PdfPCell(image213);
                            cell214.HorizontalAlignment = 1;
                            cell214.VerticalAlignment = 5;
                            cell214.FixedHeight = 12f;
                            table9.AddCell(cell214);
                        }
                        if ((oPTNote.PainlevelWrist != null) || (oPTNote.PainlevelWrist != ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"] 
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelWrist, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.ToesRight == "1") || (oPTNote.ToesLeft == "1")) || (oPTNote.ToesBoth == "1"))
                    {//ptview.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString()
                        table9.AddCell(new Phrase("TOES", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.ToesRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString()
                            iTextSharp.text.Image image214 = iTextSharp.text.Image.GetInstance(checkbox);
                            image214.ScaleAbsolute(10f, 10f);
                            PdfPCell cell215 = new PdfPCell(image214);
                            cell215.HorizontalAlignment = 1;
                            cell215.VerticalAlignment = 5;
                            cell215.FixedHeight = 12f;
                            table9.AddCell(cell215);
                        }
                        else
                        {
                            iTextSharp.text.Image image215 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image215.ScaleAbsolute(10f, 10f);
                            PdfPCell cell216 = new PdfPCell(image215);
                            cell216.HorizontalAlignment = 1;
                            cell216.VerticalAlignment = 5;
                            cell216.FixedHeight = 12f;
                            table9.AddCell(cell216);
                        }
                        if (oPTNote.ToesLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString()
                            iTextSharp.text.Image image216 = iTextSharp.text.Image.GetInstance(checkbox);
                            image216.ScaleAbsolute(10f, 10f);
                            PdfPCell cell217 = new PdfPCell(image216);
                            cell217.HorizontalAlignment = 1;
                            cell217.VerticalAlignment = 5;
                            cell217.FixedHeight = 12f;
                            table9.AddCell(cell217);
                        }
                        else
                        {
                            iTextSharp.text.Image image217 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image217.ScaleAbsolute(10f, 10f);
                            PdfPCell cell218 = new PdfPCell(image217);
                            cell218.HorizontalAlignment = 1;
                            cell218.VerticalAlignment = 5;
                            cell218.FixedHeight = 12f;
                            table9.AddCell(cell218);
                        }
                        if (oPTNote.ToesBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString()
                            iTextSharp.text.Image image218 = iTextSharp.text.Image.GetInstance(checkbox);
                            image218.ScaleAbsolute(10f, 10f);
                            PdfPCell cell219 = new PdfPCell(image218);
                            cell219.HorizontalAlignment = 1;
                            cell219.VerticalAlignment = 5;
                            cell219.FixedHeight = 12f;
                            table9.AddCell(cell219);
                        }
                        else
                        {
                            iTextSharp.text.Image image219 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image219.ScaleAbsolute(10f, 10f);
                            PdfPCell cell220 = new PdfPCell(image219);
                            cell220.HorizontalAlignment = 1;
                            cell220.VerticalAlignment = 5;
                            cell220.FixedHeight = 12f;
                            table9.AddCell(cell220);
                        }
                        if ((oPTNote.PainlevelToes != null) || (oPTNote.PainlevelToes == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainlevelToes.ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.ArmRight == "1") || ( oPTNote.ArmLeft== "1")) || ( oPTNote.ArmBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_ARM_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_ARM_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_ARM_BOTH"].ToString()
                        table9.AddCell(new Phrase("ARM", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oPTNote.ArmRight== "1")
                        {//ptview.Tables[0].Rows[0]["BT_ARM_RIGHT"].ToString()
                            iTextSharp.text.Image image220 = iTextSharp.text.Image.GetInstance(checkbox);
                            image220.ScaleAbsolute(10f, 10f);
                            PdfPCell cell221 = new PdfPCell(image220);
                            cell221.HorizontalAlignment = 1;
                            cell221.VerticalAlignment = 5;
                            cell221.FixedHeight = 12f;
                            table9.AddCell(cell221);
                        }
                        else
                        {
                            iTextSharp.text.Image image221 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image221.ScaleAbsolute(10f, 10f);
                            PdfPCell cell222 = new PdfPCell(image221);
                            cell222.HorizontalAlignment = 1;
                            cell222.VerticalAlignment = 5;
                            cell222.FixedHeight = 12f;
                            table9.AddCell(cell222);
                        }
                        if (oPTNote.ArmLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ARM_LEFT"].ToString()
                            iTextSharp.text.Image image222 = iTextSharp.text.Image.GetInstance(checkbox);
                            image222.ScaleAbsolute(10f, 10f);
                            PdfPCell cell223 = new PdfPCell(image222);
                            cell223.HorizontalAlignment = 1;
                            cell223.VerticalAlignment = 5;
                            cell223.FixedHeight = 12f;
                            table9.AddCell(cell223);
                        }
                        else
                        {
                            iTextSharp.text.Image image223 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image223.ScaleAbsolute(10f, 10f);
                            PdfPCell cell224 = new PdfPCell(image223);
                            cell224.HorizontalAlignment = 1;
                            cell224.VerticalAlignment = 5;
                            cell224.FixedHeight = 12f;
                            table9.AddCell(cell224);
                        }
                        if ( oPTNote.ArmBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_ARM_BOTH"].ToString()
                            iTextSharp.text.Image image224 = iTextSharp.text.Image.GetInstance(checkbox);
                            image224.ScaleAbsolute(10f, 10f);
                            PdfPCell cell225 = new PdfPCell(image224);
                            cell225.HorizontalAlignment = 1;
                            cell225.VerticalAlignment = 5;
                            cell225.FixedHeight = 12f;
                            table9.AddCell(cell225);
                        }
                        else
                        {
                            iTextSharp.text.Image image225 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image225.ScaleAbsolute(10f, 10f);
                            PdfPCell cell226 = new PdfPCell(image225);
                            cell226.HorizontalAlignment = 1;
                            cell226.VerticalAlignment = 5;
                            cell226.FixedHeight = 12f;
                            table9.AddCell(cell226);
                        }
                        if ((oPTNote.PainLevelArm != null) || (oPTNote.PainLevelArm == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ARM"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainLevelArm, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oPTNote.ForeArmRight == "1") || ( oPTNote.ForeArmLeft== "1")) || ( oPTNote.ForeArmBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_FORE_ARM_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_FORE_ARM_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_FORE_ARM_BOTH"].ToString()
                        table9.AddCell(new Phrase("FORE ARM", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.ForeArmRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FORE_ARM_RIGHT"].ToString()
                            iTextSharp.text.Image image226 = iTextSharp.text.Image.GetInstance(checkbox);
                            image226.ScaleAbsolute(10f, 10f);
                            PdfPCell cell227 = new PdfPCell(image226);
                            cell227.HorizontalAlignment = 1;
                            cell227.VerticalAlignment = 5;
                            cell227.FixedHeight = 12f;
                            table9.AddCell(cell227);
                        }
                        else
                        {
                            iTextSharp.text.Image image227 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image227.ScaleAbsolute(10f, 10f);
                            PdfPCell cell228 = new PdfPCell(image227);
                            cell228.HorizontalAlignment = 1;
                            cell228.VerticalAlignment = 5;
                            cell228.FixedHeight = 12f;
                            table9.AddCell(cell228);
                        }
                        if (oPTNote.ForeArmLeft == "1")
                        {//ptview.Tables[0].Rows[0]["BT_FORE_ARM_LEFT"].ToString()
                            iTextSharp.text.Image image228 = iTextSharp.text.Image.GetInstance(checkbox);
                            image228.ScaleAbsolute(10f, 10f);
                            PdfPCell cell229 = new PdfPCell(image228);
                            cell229.HorizontalAlignment = 1;
                            cell229.VerticalAlignment = 5;
                            cell229.FixedHeight = 12f;
                            table9.AddCell(cell229);
                        }
                        else
                        {
                            iTextSharp.text.Image image229 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image229.ScaleAbsolute(10f, 10f);
                            PdfPCell cell230 = new PdfPCell(image229);
                            cell230.HorizontalAlignment = 1;
                            cell230.VerticalAlignment = 5;
                            cell230.FixedHeight = 12f;
                            table9.AddCell(cell230);
                        }
                        if (oPTNote.ForeArmBoth== "1")
                        {//ptview.Tables[0].Rows[0]["BT_FORE_ARM_BOTH"].ToString()
                            iTextSharp.text.Image image230 = iTextSharp.text.Image.GetInstance(checkbox);
                            image230.ScaleAbsolute(10f, 10f);
                            PdfPCell cell231 = new PdfPCell(image230);
                            cell231.HorizontalAlignment = 1;
                            cell231.VerticalAlignment = 5;
                            cell231.FixedHeight = 12f;
                            table9.AddCell(cell231);
                        }
                        else
                        {
                            iTextSharp.text.Image image231 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image231.ScaleAbsolute(10f, 10f);
                            PdfPCell cell232 = new PdfPCell(image231);
                            cell232.HorizontalAlignment = 1;
                            cell232.VerticalAlignment = 5;
                            cell232.FixedHeight = 12f;
                            table9.AddCell(cell232);
                        }
                        if ((oPTNote.PainLevelForeArm != null) || (oPTNote.PainLevelForeArm == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FORE_ARM"] 
                            table9.AddCell(new Phrase(" " + oPTNote.PainLevelForeArm, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oPTNote.AnkleRight== "1") || ( oPTNote.AnkleLeft== "1")) || ( oPTNote.AnkleBoth== "1"))
                    {//ptview.Tables[0].Rows[0]["BT_ANKLE_RIGHT"].ToString()|ptview.Tables[0].Rows[0]["BT_ANKLE_LEFT"].ToString()|ptview.Tables[0].Rows[0]["BT_ANKLE_BOTH"].ToString()
                        table9.AddCell(new Phrase("ANKLE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oPTNote.AnkleRight == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ANKLE_RIGHT"].ToString()
                            iTextSharp.text.Image image232 = iTextSharp.text.Image.GetInstance(checkbox);
                            image232.ScaleAbsolute(10f, 10f);
                            PdfPCell cell233 = new PdfPCell(image232);
                            cell233.HorizontalAlignment = 1;
                            cell233.VerticalAlignment = 5;
                            cell233.FixedHeight = 12f;
                            table9.AddCell(cell233);
                        }
                        else
                        {
                            iTextSharp.text.Image image233 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image233.ScaleAbsolute(10f, 10f);
                            PdfPCell cell234 = new PdfPCell(image233);
                            cell234.HorizontalAlignment = 1;
                            cell234.VerticalAlignment = 5;
                            cell234.FixedHeight = 12f;
                            table9.AddCell(cell234);
                        }
                        if ( oPTNote.AnkleLeft== "1")
                        {//ptview.Tables[0].Rows[0]["BT_ANKLE_LEFT"].ToString()
                            iTextSharp.text.Image image234 = iTextSharp.text.Image.GetInstance(checkbox);
                            image234.ScaleAbsolute(10f, 10f);
                            PdfPCell cell235 = new PdfPCell(image234);
                            cell235.HorizontalAlignment = 1;
                            cell235.VerticalAlignment = 5;
                            cell235.FixedHeight = 12f;
                            table9.AddCell(cell235);
                        }
                        else
                        {
                            iTextSharp.text.Image image235 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image235.ScaleAbsolute(10f, 10f);
                            PdfPCell cell236 = new PdfPCell(image235);
                            cell236.HorizontalAlignment = 1;
                            cell236.VerticalAlignment = 5;
                            cell236.FixedHeight = 12f;
                            table9.AddCell(cell236);
                        }
                        if (oPTNote.AnkleBoth == "1")
                        {//ptview.Tables[0].Rows[0]["BT_ANKLE_BOTH"].ToString()
                            iTextSharp.text.Image image236 = iTextSharp.text.Image.GetInstance(checkbox);
                            image236.ScaleAbsolute(10f, 10f);
                            PdfPCell cell237 = new PdfPCell(image236);
                            cell237.HorizontalAlignment = 1;
                            cell237.VerticalAlignment = 5;
                            cell237.FixedHeight = 12f;
                            table9.AddCell(cell237);
                        }
                        else
                        {
                            iTextSharp.text.Image image237 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image237.ScaleAbsolute(10f, 10f);
                            PdfPCell cell238 = new PdfPCell(image237);
                            cell238.HorizontalAlignment = 1;
                            cell238.VerticalAlignment = 5;
                            cell238.FixedHeight = 12f;
                            table9.AddCell(cell238);
                        }
                        if ((oPTNote.PainLevelAnkle != null) || (oPTNote.PainLevelAnkle == ""))
                        {//ptview.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ANKLE"]
                            table9.AddCell(new Phrase(" " + oPTNote.PainLevelAnkle, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    table9.DefaultCell.Border = 0;
                    table9.AddCell(new Phrase(""));
                    table9.AddCell(new Phrase(""));
                    table9.AddCell(new Phrase(""));
                    table9.AddCell(new Phrase(""));
                    table7.AddCell(table9);
                    table3.AddCell(table7);
                    float[] numArray10 = new float[] { 4f, 4f, 4f };
                    PdfPTable table10 = new PdfPTable(numArray10);
                    table10.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table10.DefaultCell.Colspan = 3;
                    table10.AddCell(new Phrase("Treatment", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table10.DefaultCell.Colspan = 1;
                    table10.DefaultCell.HorizontalAlignment = 0;
                    table10.DefaultCell.VerticalAlignment = 4;
                    float[] numArray11 = new float[] { 1f, 4f };
                    PdfPTable table11 = new PdfPTable(numArray11);
                    table11.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table11.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table11.DefaultCell.Colspan = 2;
                    table11.AddCell(new Phrase("Objective", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table11.DefaultCell.Border = 0;
                    table11.AddCell(new Phrase(""));
                    table11.DefaultCell.Colspan = 1;
                    table11.DefaultCell.Border = 0;
                    if ( oPTNote.Objective1== "True")
                    {//ptview.Tables[0].Rows[0]["BT_OBJECTIVE1"].ToString()
                        iTextSharp.text.Image image238 = iTextSharp.text.Image.GetInstance(checkbox);
                        image238.ScaleAbsolute(10f, 10f);
                        PdfPCell cell239 = new PdfPCell(image238);
                        cell239.Border = 0;
                        cell239.HorizontalAlignment = 2;
                        cell239.VerticalAlignment = 4;
                        cell239.FixedHeight = 12f;
                        table11.AddCell(cell239);
                        table11.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image239 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image239.ScaleAbsolute(10f, 10f);
                        PdfPCell cell240 = new PdfPCell(image239);
                        cell240.Border = 0;
                        cell240.HorizontalAlignment = 2;
                        cell240.VerticalAlignment = 4;
                        cell240.FixedHeight = 12f;
                        table11.AddCell(cell240);
                        table11.DefaultCell.Border = 0;
                    }
                    table11.AddCell(new Phrase("Patient states condition is the same", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Objective2 == "True")
                    {//ptview.Tables[0].Rows[0]["BT_OBJECTIVE2"].ToString()
                        iTextSharp.text.Image image240 = iTextSharp.text.Image.GetInstance(checkbox);
                        image240.ScaleAbsolute(10f, 10f);
                        PdfPCell cell241 = new PdfPCell(image240);
                        cell241.Border = 0;
                        cell241.HorizontalAlignment = 2;
                        cell241.VerticalAlignment = 4;
                        cell241.FixedHeight = 12f;
                        table11.AddCell(cell241);
                        table11.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image241 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image241.ScaleAbsolute(10f, 10f);
                        PdfPCell cell242 = new PdfPCell(image241);
                        cell242.Border = 0;
                        cell242.HorizontalAlignment = 2;
                        cell242.VerticalAlignment = 4;
                        cell242.FixedHeight = 12f;
                        table11.AddCell(cell242);
                        table11.DefaultCell.Border = 0;
                    }
                    table11.AddCell(new Phrase("Patient states little improvement in condition", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.Objective3 == "True")
                    {//ptview.Tables[0].Rows[0]["BT_OBJECTIVE3"].ToString()
                        iTextSharp.text.Image image242 = iTextSharp.text.Image.GetInstance(checkbox);
                        image242.ScaleAbsolute(10f, 10f);
                        PdfPCell cell243 = new PdfPCell(image242);
                        cell243.Border = 0;
                        cell243.HorizontalAlignment = 2;
                        cell243.VerticalAlignment = 4;
                        cell243.FixedHeight = 12f;
                        table11.AddCell(cell243);
                        table11.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image243 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image243.ScaleAbsolute(10f, 10f);
                        PdfPCell cell244 = new PdfPCell(image243);
                        cell244.Border = 0;
                        cell244.HorizontalAlignment = 2;
                        cell244.VerticalAlignment = 4;
                        cell244.FixedHeight = 12f;
                        table11.AddCell(cell244);
                        table11.DefaultCell.Border = 0;
                    }
                    table11.AddCell(new Phrase("Patient states much improvement in condition", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table11.DefaultCell.HorizontalAlignment = 0;
                    table11.DefaultCell.VerticalAlignment = 6;
                    table10.AddCell(table11);
                    float[] numArray12 = new float[] { 1f, 4f };
                    PdfPTable table12 = new PdfPTable(numArray12);
                    table12.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table12.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table12.DefaultCell.Colspan = 2;
                    table12.AddCell(new Phrase("Assessment", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table12.DefaultCell.Border = 0;
                    table12.AddCell(new Phrase(""));
                    table12.DefaultCell.Colspan = 1;
                    table12.DefaultCell.Border = 0;
                    if (oPTNote.PatientTolerated == "True")
                    {//ptview.Tables[0].Rows[0]["BT_PATIENT_TOLERATED"].ToString()
                        iTextSharp.text.Image image244 = iTextSharp.text.Image.GetInstance(checkbox);
                        image244.ScaleAbsolute(10f, 10f);
                        PdfPCell cell245 = new PdfPCell(image244);
                        cell245.Border = 0;
                        cell245.HorizontalAlignment = 2;
                        cell245.VerticalAlignment = 4;
                        cell245.FixedHeight = 12f;
                        table12.AddCell(cell245);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image245 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image245.ScaleAbsolute(10f, 10f);
                        PdfPCell cell246 = new PdfPCell(image245);
                        cell246.Border = 0;
                        cell246.HorizontalAlignment = 2;
                        cell246.VerticalAlignment = 4;
                        cell246.FixedHeight = 12f;
                        table12.AddCell(cell246);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("Patient tolerated maximum level", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oPTNote.AssotherComments == "True")
                    {//ptview.Tables[0].Rows[0]["BT_ASS_OTHER_COMMENTS"].ToString()
                        iTextSharp.text.Image image246 = iTextSharp.text.Image.GetInstance(checkbox);
                        image246.ScaleAbsolute(10f, 10f);
                        PdfPCell cell247 = new PdfPCell(image246);
                        cell247.Border = 0;
                        cell247.HorizontalAlignment = 2;
                        cell247.VerticalAlignment = 4;
                        cell247.FixedHeight = 12f;
                        table12.AddCell(cell247);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image247 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image247.ScaleAbsolute(10f, 10f);
                        PdfPCell cell248 = new PdfPCell(image247);
                        cell248.Border = 0;
                        cell248.HorizontalAlignment = 2;
                        cell248.VerticalAlignment = 4;
                        cell248.FixedHeight = 12f;
                        table12.AddCell(cell248);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("Other Comments", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table12.AddCell(new Phrase(""));
                    table12.AddCell(new Phrase(oPTNote.OtherComments, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));///ptview.Tables[0].Rows[0]["SZ_OTHER_COMMENTS"].ToString()
                    table12.DefaultCell.HorizontalAlignment = 0;
                    table12.DefaultCell.VerticalAlignment = 6;
                    table10.AddCell(table12);
                    float[] numArray13 = new float[] { 1f, 4f };
                    PdfPTable table13 = new PdfPTable(numArray13);
                    table13.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table13.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table13.DefaultCell.Colspan = 2;
                    table13.AddCell(new Phrase("Plan", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table13.DefaultCell.Border = 0;
                    table13.AddCell(new Phrase(""));
                    table13.DefaultCell.Colspan = 1;
                    table13.DefaultCell.Border = 0;
                    if (oPTNote.CotinueTherapy == "True")
                    {//ptview.Tables[0].Rows[0]["BT_COTINUE_THERAPY"].ToString()
                        iTextSharp.text.Image image248 = iTextSharp.text.Image.GetInstance(checkbox);
                        image248.ScaleAbsolute(10f, 10f);
                        PdfPCell cell249 = new PdfPCell(image248);
                        cell249.Border = 0;
                        cell249.HorizontalAlignment = 2;
                        cell249.VerticalAlignment = 4;
                        cell249.FixedHeight = 12f;
                        table13.AddCell(cell249);
                        table13.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image249 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image249.ScaleAbsolute(10f, 10f);
                        PdfPCell cell250 = new PdfPCell(image249);
                        cell250.Border = 0;
                        cell250.HorizontalAlignment = 2;
                        cell250.VerticalAlignment = 4;
                        cell250.FixedHeight = 12f;
                        table13.AddCell(cell250);
                        table13.DefaultCell.Border = 0;
                    }
                    table13.AddCell(new Phrase("Continue Physical Therapy as prescribed", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if ( oPTNote.PlanOtherComments== "True")
                    {//ptview.Tables[0].Rows[0]["BT_PLAN_OTHER_COMMENTS"].ToString()
                        iTextSharp.text.Image image250 = iTextSharp.text.Image.GetInstance(checkbox);
                        image250.ScaleAbsolute(10f, 10f);
                        PdfPCell cell251 = new PdfPCell(image250);
                        cell251.Border = 0;
                        cell251.HorizontalAlignment = 2;
                        cell251.VerticalAlignment = 4;
                        cell251.FixedHeight = 12f;
                        table13.AddCell(cell251);
                        table13.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image251 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image251.ScaleAbsolute(10f, 10f);
                        PdfPCell cell252 = new PdfPCell(image251);
                        cell252.Border = 0;
                        cell252.HorizontalAlignment = 2;
                        cell252.VerticalAlignment = 6;
                        cell252.FixedHeight = 12f;
                        table13.AddCell(cell252);
                        table13.DefaultCell.Border = 0;
                    }
                    table13.AddCell(new Phrase("Other Comments", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table13.AddCell(new Phrase(""));
                    table13.AddCell(new Phrase(oPTNote.PlanOherComments2, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));//ptview.Tables[0].Rows[0]["SZ_BT_PLAN_OHER_COMMENTS"].ToString()
                    table13.DefaultCell.HorizontalAlignment = 0;
                    table13.DefaultCell.VerticalAlignment = 6;
                    table10.AddCell(table13);
                    table3.AddCell(table10);
                    set5 = this.GET_PROCEDURECODE_USING_EVENTID(str6);
                    float[] numArray14 = new float[] { 1f, 3f, 1f, 3f };
                    PdfPTable table14 = new PdfPTable(numArray14);
                    table.DefaultCell.Border = 15;
                    table14.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table14.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table14.DefaultCell.Colspan = 4;
                    table14.AddCell(new Phrase("Code", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table14.DefaultCell.Colspan = 1;
                    for (int m = 0; m < set5.Tables[0].Rows.Count; m++)
                    {
                        iTextSharp.text.Image image252 = iTextSharp.text.Image.GetInstance(checkbox);
                        image252.ScaleAbsolute(10f, 10f);
                        PdfPCell cell253 = new PdfPCell(image252);
                        cell253.Border = 0;
                        cell253.HorizontalAlignment = 2;
                        cell253.VerticalAlignment = 6;
                        cell253.FixedHeight = 12f;
                        table14.AddCell(cell253);
                        table14.DefaultCell.Border = 0;
                        table14.AddCell(new Phrase(set5.Tables[0].Rows[m]["Column1"].ToString(), FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                        table14.DefaultCell.HorizontalAlignment = 0;
                        table14.DefaultCell.VerticalAlignment = 6;
                    }
                    if ((set5.Tables[0].Rows.Count % 2) != 0)
                    {
                        table14.AddCell(new Phrase(""));
                        table14.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table14);
                    float[] numArray15 = new float[] { 1f, 2f, 1f, 2f };
                    PdfPTable table15 = new PdfPTable(numArray15);
                    table.DefaultCell.Border = 0;
                    table15.DefaultCell.Border = 0;
                    table15.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table15.AddCell(new Phrase("Patient Sign", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image image253 = iTextSharp.text.Image.GetInstance(oPTNote.PatientSignPath.Replace("/", @"\"));//ptview.Tables[0].Rows[0]["SZ_PATIENT_SIGN_PATH"].ToString()
                        image253.ScaleAbsoluteHeight(30f);
                        image253.ScaleAbsoluteWidth(50f);
                        PdfPCell cell254 = new PdfPCell(image253);
                        cell254.HorizontalAlignment = 0;
                        cell254.Border = 2;
                        cell254.PaddingBottom = 1f;
                        table15.AddCell(cell254);
                    }
                    catch (Exception)
                    {
                        table15.AddCell(new Phrase(""));
                    }
                    table15.AddCell(new Phrase("Doctor Sign", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image image254 = iTextSharp.text.Image.GetInstance(oPTNote.DoctorSignPath.Replace("/", @"\"));//ptview.Tables[0].Rows[0]["SZ_DOCTOR_SIGN_PATH"].ToString()
                        image254.ScaleAbsoluteHeight(30f);
                        image254.ScaleAbsoluteWidth(50f);
                        PdfPCell cell255 = new PdfPCell(image254);
                        cell255.HorizontalAlignment = 0;
                        cell255.Border = 2;
                        cell255.PaddingBottom = 1f;
                        table15.AddCell(cell255);
                        table15.AddCell(new Phrase(""));
                    }
                    catch (Exception)
                    {
                        table15.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table15);
                    table.AddCell(table3);
                    float[] numArray16 = new float[] { 4f };
                    PdfPTable table16 = new PdfPTable(numArray16);
                    table16.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table16.DefaultCell.Border = 0;
                    table16.DefaultCell.HorizontalAlignment = 1;
                    table16.DefaultCell.VerticalAlignment = 4;
                    table16.DefaultCell.Colspan = 1;
                    table16.AddCell(new Phrase(""));
                    table16.AddCell(new Phrase(""));
                    table.AddCell(table16);
                    if (SystemSettingValue == "1")
                    {
                        table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);
                        if (table.Rows.Count > 0)
                            document.NewPage();
                        table.FlushContent();
                    }
                }
                if (SystemSettingValue != "1")
                {
                    table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);
                    //table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);

                }
                else
                {

                }
                
                document.Close();

                
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                File.WriteAllBytes(Path + s_pdfname, stream.GetBuffer());
                   
            }
        }

        private DataSet GET_COMPLIANTS_USING_EVENTID(string EventId)
        {

            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SP_GET_PT_COMPLAINTS", connection);
                selectCommand.Parameters.AddWithValue("@I_EVENT_ID", EventId);
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

        private DataSet GET_PROCEDURECODE_USING_EVENTID(string EventId)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("GET_PROCEDURE_CODE_USING_EVENT_ID", connection);
                selectCommand.Parameters.AddWithValue("@I_EVENT_ID", EventId);
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

        public int CheckCount(string EventId)
        {
            int num = 0;
            int num2 = 0;
            DataSet ptview = new DataSet();
            ptview = this.GetPTView(EventId);
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_NECK_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_JAW_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_WRIST_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HAND_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_HIP_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_THIGH_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_KNEE_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_FOOT_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TREATMENT_TOES_BOTH"].ToString() == "1"))
            {
                num++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (((ptview.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString() == "1") || (ptview.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString() == "1")) || (ptview.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() == "1"))
            {
                num2++;
            }
            if (num > num2)
            {
                return num;
            }
            return num2;
        }

        public DataSet GetPTView(string EventId)
        {

            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SP_PT_NOTES", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@I_EVENT_ID ", EventId);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
                selectCommand.CommandTimeout = 0;

            }
            catch (SqlException exception)
            {
                exception.Message.ToString();
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

        public DataSet GetSystemSettingsforSinglePage(string AccountId)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SP_GET_PT_SYSTEM_SETTING_VALUE", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", AccountId);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (SqlException exception)
            {
                exception.Message.ToString();
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
    }
}
