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
   public class SrvCHNote:SrvNote
    {
       private List<gbmodel.bill.Bill> c_lstBill;

       public SrvCHNote(List<gbmodel.bill.Bill> p_lstBill)
        {
            this.c_lstBill = p_lstBill;
        }

        public override void PrintNote()
        {
            string c_basepath = System.Configuration.ConfigurationManager.AppSettings["BASEPATH"].ToString();
            string Path=System.Configuration.ConfigurationManager.AppSettings["BASEPATH"].ToString();
            SqlConnection selectConnection = new SqlConnection(DBUtil.ConnectionString);
            List<gbmodel.patient.note.Note> lstNote = Select(c_lstBill, gbmodel.patient.note.EnumNoteType.CH);

            string checkbox = System.Configuration.ConfigurationManager.AppSettings["CHECKBOXPATH"].ToString();
            string uncheckbox = System.Configuration.ConfigurationManager.AppSettings["UNCHECKBOXPATH"].ToString();
            string checkradio = System.Configuration.ConfigurationManager.AppSettings["CHECKRADIO"].ToString();
            string uncheckradio = System.Configuration.ConfigurationManager.AppSettings["UNCHECKRADIO"].ToString();
                               


            int num = 0;
            string str = "";

            for (int i = 0; i < lstNote.Count; i++)
            {
                gbmodel.patient.note.CHNote oCHNote = new gbmodel.patient.note.CHNote();
                oCHNote = (gbmodel.patient.note.CHNote)lstNote[i];

                string s_pdfname = string.Concat(new object[] { "FUReport_", this.c_lstBill[i].Number, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf" });
                c_basepath = c_basepath + "/" + s_pdfname;

                SqlDataAdapter adapter = new SqlDataAdapter("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + this.c_lstBill[i].Number + "', @FLAG='GET_EVENT_ID'", selectConnection);
                adapter.SelectCommand.CommandTimeout = 0;
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
                table2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                table2.DefaultCell.Border = 0;
                table2.DefaultCell.HorizontalAlignment = 1;
                table2.DefaultCell.VerticalAlignment = 5;
                table.DefaultCell.Border = 0;
                table2.AddCell(new Phrase("CH NOTES", FontFactory.GetFont("Arial", 12f, 1, iTextSharp.text.Color.BLACK)));
                table.AddCell(table2);

                for (int j = num; j < set2.Tables[0].Rows.Count; j++)
                {
                    if ((table.TotalHeight > (((document.PageSize.Height - document.TopMargin) - document.BottomMargin) - 50f)) || (j >= 1))
                    {
                        table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);
                        table = new PdfPTable(numArray);
                        table.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                        table.DefaultCell.HorizontalAlignment = 1;
                        table.DefaultCell.VerticalAlignment = 4;
                        table.DefaultCell.Colspan = 1;
                        document.NewPage();
                        table2 = new PdfPTable(numArray2);
                        table2.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                        table2.DefaultCell.Border = 0;
                        table2.DefaultCell.HorizontalAlignment = 1;
                        table2.DefaultCell.VerticalAlignment = 5;
                        table.DefaultCell.Border = 0;
                        table2.AddCell(new Phrase("CH NOTES", FontFactory.GetFont("Arial", 12f, 1, iTextSharp.text.Color.BLACK)));
                        table.AddCell(table2);
                    }
                    
                    string eventID = set2.Tables[0].Rows[j][0].ToString();
                    float[] numArray3 = new float[] { 4f };
                    PdfPTable table3 = new PdfPTable(numArray3);
                    table3.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    float[] numArray4 = new float[] { 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable table4 = new PdfPTable(numArray4);
                    table4.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.Border = 0;
                    table4.DefaultCell.HorizontalAlignment = 0;
                    table4.DefaultCell.VerticalAlignment = 0;
                    table4.AddCell(new Phrase("Patient Name", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase("- " + oCHNote.Patient.Name, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.DefaultCell.HorizontalAlignment = 2;
                    table4.DefaultCell.VerticalAlignment = 2;
                    table4.AddCell(new Phrase("Doctor", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.DefaultCell.HorizontalAlignment = 0;
                    table4.DefaultCell.VerticalAlignment = 0;
                    table4.AddCell(new Phrase("- " + oCHNote.DoctorName, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.DefaultCell.Colspan = 1;
                    string str7 = Convert.ToDateTime(oCHNote.Date).ToString("MM-dd-yyyy");
                    table4.AddCell(new Phrase(""));
                    table4.DefaultCell.HorizontalAlignment = 2;
                    table4.DefaultCell.VerticalAlignment = 2;
                    table4.AddCell(new Phrase("Date - " + str7, FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table4.AddCell(new Phrase(""));
                    table3.DefaultCell.Border = 0;
                    table3.AddCell(table4);
                    table3.DefaultCell.Border = 15;
                    float[] numArray5 = new float[] { 1f, 2f, 1f, 2f };
                    PdfPTable table5 = new PdfPTable(numArray5);
                    table.DefaultCell.Border = 15;
                    table5.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table5.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table5.DefaultCell.Colspan = 4;
                    table5.AddCell(new Phrase("Subjective : The patient reported the following information. ", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table5.DefaultCell.Border = 0;
                    table5.DefaultCell.Colspan = 1;
                    if (oCHNote.NoChangeInMyCondition == "True")
                    {   //chairoView.Tables[0].Rows[0]["BT_NO_CHANGE_IN_MY_CONDITION"].ToString()
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(checkbox);
                        image.ScaleAbsolute(10f, 10f);
                        PdfPCell cell = new PdfPCell(image);
                        cell.Border = 0;
                        cell.HorizontalAlignment = 2;
                        cell.VerticalAlignment = 6;
                        cell.FixedHeight = 12f;
                        table5.AddCell(cell);
                        table5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image2.ScaleAbsolute(10f, 10f);
                        PdfPCell cell2 = new PdfPCell(image2);
                        cell2.Border = 0;
                        cell2.HorizontalAlignment = 2;
                        cell2.VerticalAlignment = 6;
                        cell2.FixedHeight = 12f;
                        table5.AddCell(cell2);
                        table5.DefaultCell.Border = 0;
                    }
                    table5.AddCell(new Phrase("THERE IS NO CHANGE IN MY CONDITION", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ChangeInMyCondition == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CHANGE_IN_MY_CONDITION"]
                        iTextSharp.text.Image image3 = iTextSharp.text.Image.GetInstance(checkbox);
                        image3.ScaleAbsolute(10f, 10f);
                        PdfPCell cell3 = new PdfPCell(image3);
                        cell3.Border = 0;
                        cell3.HorizontalAlignment = 2;
                        cell3.VerticalAlignment = 6;
                        cell3.FixedHeight = 12f;
                        table5.AddCell(cell3);
                        table5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image4 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image4.ScaleAbsolute(10f, 10f);
                        PdfPCell cell4 = new PdfPCell(image4);
                        cell4.Border = 0;
                        cell4.HorizontalAlignment = 2;
                        cell4.VerticalAlignment = 6;
                        cell4.FixedHeight = 12f;
                        table5.AddCell(cell4);
                        table5.DefaultCell.Border = 0;
                    }
                    table5.AddCell(new Phrase("THERE IS CHANGE IN MY CONDITION", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.MyConditionIsAboutSame == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_MY_CONDITION_IS_ABOUT_SAME"].ToString()
                        iTextSharp.text.Image image5 = iTextSharp.text.Image.GetInstance(checkbox);
                        image5.ScaleAbsolute(10f, 10f);
                        PdfPCell cell5 = new PdfPCell(image5);
                        cell5.Border = 0;
                        cell5.HorizontalAlignment = 2;
                        cell5.VerticalAlignment = 6;
                        cell5.FixedHeight = 12f;
                        table5.AddCell(cell5);
                        table5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image6 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image6.ScaleAbsolute(10f, 10f);
                        PdfPCell cell6 = new PdfPCell(image6);
                        cell6.Border = 0;
                        cell6.HorizontalAlignment = 2;
                        cell6.VerticalAlignment = 6;
                        cell6.FixedHeight = 12f;
                        table5.AddCell(cell6);
                        table5.DefaultCell.Border = 0;
                    }
                    table5.AddCell(new Phrase("MY CONDITION IS ABOUT SAME", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table5.AddCell(new Phrase(""));
                    table5.AddCell(new Phrase(""));
                    table3.AddCell(table5);
                    float[] numArray6 = new float[] { 2f, 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable table6 = new PdfPTable(numArray6);
                    table.DefaultCell.Border = 15;
                    table6.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table6.DefaultCell.Colspan = 1;
                    table6.DefaultCell.Border = 0;
                    table6.AddCell(new Phrase("Pain Grades", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Mild == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_MILD"].ToString()
                        iTextSharp.text.Image image7 = iTextSharp.text.Image.GetInstance(checkbox);
                        image7.ScaleAbsolute(10f, 10f);
                        PdfPCell cell7 = new PdfPCell(image7);
                        cell7.Border = 0;
                        cell7.HorizontalAlignment = 2;
                        cell7.VerticalAlignment = 6;
                        cell7.FixedHeight = 12f;
                        table6.AddCell(cell7);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image8 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image8.ScaleAbsolute(10f, 10f);
                        PdfPCell cell8 = new PdfPCell(image8);
                        cell8.Border = 0;
                        cell8.HorizontalAlignment = 2;
                        cell8.VerticalAlignment = 6;
                        cell8.FixedHeight = 12f;
                        table6.AddCell(cell8);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("MILD", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Moderate == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_MODERATE"].ToString() == "1"
                        iTextSharp.text.Image image9 = iTextSharp.text.Image.GetInstance(checkbox);
                        image9.ScaleAbsolute(10f, 10f);
                        PdfPCell cell9 = new PdfPCell(image9);
                        cell9.Border = 0;
                        cell9.HorizontalAlignment = 2;
                        cell9.VerticalAlignment = 6;
                        cell9.FixedHeight = 12f;
                        table6.AddCell(cell9);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image10 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image10.ScaleAbsolute(10f, 10f);
                        PdfPCell cell10 = new PdfPCell(image10);
                        cell10.Border = 0;
                        cell10.HorizontalAlignment = 2;
                        cell10.VerticalAlignment = 6;
                        cell10.FixedHeight = 12f;
                        table6.AddCell(cell10);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("MODERATE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Severe == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_SEVERE"].ToString()
                        iTextSharp.text.Image image11 = iTextSharp.text.Image.GetInstance(checkbox);
                        image11.ScaleAbsolute(10f, 10f);
                        PdfPCell cell11 = new PdfPCell(image11);
                        cell11.Border = 0;
                        cell11.HorizontalAlignment = 2;
                        cell11.VerticalAlignment = 6;
                        cell11.FixedHeight = 12f;
                        table6.AddCell(cell11);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image12 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image12.ScaleAbsolute(10f, 10f);
                        PdfPCell cell12 = new PdfPCell(image12);
                        cell12.Border = 0;
                        cell12.HorizontalAlignment = 2;
                        cell12.VerticalAlignment = 6;
                        cell12.FixedHeight = 12f;
                        table6.AddCell(cell12);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("SEVERE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.VerySevere == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_VERY_SEVERE"].ToString()
                        iTextSharp.text.Image image13 = iTextSharp.text.Image.GetInstance(checkbox);
                        image13.ScaleAbsolute(10f, 10f);
                        PdfPCell cell13 = new PdfPCell(image13);
                        cell13.Border = 0;
                        cell13.HorizontalAlignment = 2;
                        cell13.VerticalAlignment = 6;
                        cell13.FixedHeight = 12f;
                        table6.AddCell(cell13);
                        table6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image14 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image14.ScaleAbsolute(10f, 10f);
                        PdfPCell cell14 = new PdfPCell(image14);
                        cell14.Border = 0;
                        cell14.HorizontalAlignment = 2;
                        cell14.VerticalAlignment = 6;
                        cell14.FixedHeight = 12f;
                        table6.AddCell(cell14);
                        table6.DefaultCell.Border = 0;
                    }
                    table6.AddCell(new Phrase("VERY SEVERE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table3.AddCell(table6);
                    float[] numArray7 = new float[] { 4f, 2f };
                    PdfPTable table7 = new PdfPTable(numArray7);
                    table7.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
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
                    float[] numArray8 = new float[] { 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable table8 = new PdfPTable(numArray8);
                    table8.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase("Objective", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase("Objective", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase("Objective", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase("Objective", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Spasm == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_SPASM"].ToString()
                        iTextSharp.text.Image image15 = iTextSharp.text.Image.GetInstance(checkbox);
                        image15.ScaleAbsolute(10f, 10f);
                        PdfPCell cell15 = new PdfPCell(image15);
                        cell15.HorizontalAlignment = 1;
                        cell15.VerticalAlignment = 5;
                        cell15.FixedHeight = 12f;
                        table8.AddCell(cell15);
                    }
                    else
                    {
                        iTextSharp.text.Image image16 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image16.ScaleAbsolute(10f, 10f);
                        PdfPCell cell16 = new PdfPCell(image16);
                        cell16.HorizontalAlignment = 1;
                        cell16.VerticalAlignment = 5;
                        cell16.FixedHeight = 12f;
                        table8.AddCell(cell16);
                    }
                    table8.AddCell(new Phrase("S:SPASM", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Cervical == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL"].ToString()
                        iTextSharp.text.Image image17 = iTextSharp.text.Image.GetInstance(checkbox);
                        image17.ScaleAbsolute(10f, 10f);
                        PdfPCell cell17 = new PdfPCell(image17);
                        cell17.HorizontalAlignment = 1;
                        cell17.VerticalAlignment = 5;
                        cell17.FixedHeight = 12f;
                        table8.AddCell(cell17);
                    }
                    else
                    {
                        iTextSharp.text.Image image18 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image18.ScaleAbsolute(10f, 10f);
                        PdfPCell cell18 = new PdfPCell(image18);
                        cell18.HorizontalAlignment = 1;
                        cell18.VerticalAlignment = 5;
                        cell18.FixedHeight = 12f;
                        table8.AddCell(cell18);
                    }
                    table8.AddCell(new Phrase("CERVICAL", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Pelvis == "1")
                    { //chairoView.Tables[0].Rows[0]["BT_PELVIS"].ToString()
                        iTextSharp.text.Image image19 = iTextSharp.text.Image.GetInstance(checkbox);
                        image19.ScaleAbsolute(10f, 10f);
                        PdfPCell cell19 = new PdfPCell(image19);
                        cell19.HorizontalAlignment = 1;
                        cell19.VerticalAlignment = 5;
                        cell19.FixedHeight = 12f;
                        table8.AddCell(cell19);
                    }
                    else
                    {
                        iTextSharp.text.Image image20 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image20.ScaleAbsolute(10f, 10f);
                        PdfPCell cell20 = new PdfPCell(image20);
                        cell20.HorizontalAlignment = 1;
                        cell20.VerticalAlignment = 5;
                        cell20.FixedHeight = 12f;
                        table8.AddCell(cell20);
                    }
                    table8.AddCell(new Phrase("PELVIS", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Quad == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_QUAD"].ToString()
                        iTextSharp.text.Image image21 = iTextSharp.text.Image.GetInstance(checkbox);
                        image21.ScaleAbsolute(10f, 10f);
                        PdfPCell cell21 = new PdfPCell(image21);
                        cell21.HorizontalAlignment = 1;
                        cell21.VerticalAlignment = 5;
                        cell21.FixedHeight = 12f;
                        table8.AddCell(cell21);
                    }
                    else
                    {
                        iTextSharp.text.Image image22 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image22.ScaleAbsolute(10f, 10f);
                        PdfPCell cell22 = new PdfPCell(image22);
                        cell22.HorizontalAlignment = 1;
                        cell22.VerticalAlignment = 5;
                        cell22.FixedHeight = 12f;
                        table8.AddCell(cell22);
                    }
                    table8.AddCell(new Phrase("QUAD", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Edema == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_EDEMA"].ToString()
                        iTextSharp.text.Image image23 = iTextSharp.text.Image.GetInstance(checkbox);
                        image23.ScaleAbsolute(10f, 10f);
                        PdfPCell cell23 = new PdfPCell(image23);
                        cell23.HorizontalAlignment = 1;
                        cell23.VerticalAlignment = 5;
                        cell23.FixedHeight = 12f;
                        table8.AddCell(cell23);
                    }
                    else
                    {
                        iTextSharp.text.Image image24 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image24.ScaleAbsolute(10f, 10f);
                        PdfPCell cell24 = new PdfPCell(image24);
                        cell24.HorizontalAlignment = 1;
                        cell24.VerticalAlignment = 5;
                        cell24.FixedHeight = 12f;
                        table8.AddCell(cell24);
                    }
                    table8.AddCell(new Phrase("E:EDEMA", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Thoracic == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC"].ToString()
                        iTextSharp.text.Image image25 = iTextSharp.text.Image.GetInstance(checkbox);
                        image25.ScaleAbsolute(10f, 10f);
                        PdfPCell cell25 = new PdfPCell(image25);
                        cell25.HorizontalAlignment = 1;
                        cell25.VerticalAlignment = 5;
                        cell25.FixedHeight = 12f;
                        table8.AddCell(cell25);
                    }
                    else
                    {
                        iTextSharp.text.Image image26 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image26.ScaleAbsolute(10f, 10f);
                        PdfPCell cell26 = new PdfPCell(image26);
                        cell26.HorizontalAlignment = 1;
                        cell26.VerticalAlignment = 5;
                        cell26.FixedHeight = 12f;
                        table8.AddCell(cell26);
                    }
                    table8.AddCell(new Phrase("THORACIC", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Trapezius== "1")
                    {//chairoView.Tables[0].Rows[0]["BT_TRAPEZIUS"].ToString()
                        iTextSharp.text.Image image27 = iTextSharp.text.Image.GetInstance(checkbox);
                        image27.ScaleAbsolute(10f, 10f);
                        PdfPCell cell27 = new PdfPCell(image27);
                        cell27.HorizontalAlignment = 1;
                        cell27.VerticalAlignment = 5;
                        cell27.FixedHeight = 12f;
                        table8.AddCell(cell27);
                    }
                    else
                    {
                        iTextSharp.text.Image image28 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image28.ScaleAbsolute(10f, 10f);
                        PdfPCell cell28 = new PdfPCell(image28);
                        cell28.HorizontalAlignment = 1;
                        cell28.VerticalAlignment = 5;
                        cell28.FixedHeight = 12f;
                        table8.AddCell(cell28);
                    }
                    table8.AddCell(new Phrase("TRAPEZIUS", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LevatorScapulae == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_LEVATOR_SCAPULAE"].ToString()
                        iTextSharp.text.Image image29 = iTextSharp.text.Image.GetInstance(checkbox);
                        image29.ScaleAbsolute(10f, 10f);
                        PdfPCell cell29 = new PdfPCell(image29);
                        cell29.HorizontalAlignment = 1;
                        cell29.VerticalAlignment = 5;
                        cell29.FixedHeight = 12f;
                        table8.AddCell(cell29);
                    }
                    else
                    {
                        iTextSharp.text.Image image30 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image30.ScaleAbsolute(10f, 10f);
                        PdfPCell cell30 = new PdfPCell(image30);
                        cell30.HorizontalAlignment = 1;
                        cell30.VerticalAlignment = 5;
                        cell30.FixedHeight = 12f;
                        table8.AddCell(cell30);
                    }
                    table8.AddCell(new Phrase("LEVATOR SCAPULAE", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TriggerPoints == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_TRIGGER_POINTS"].ToString()
                        iTextSharp.text.Image image31 = iTextSharp.text.Image.GetInstance(checkbox);
                        image31.ScaleAbsolute(10f, 10f);
                        PdfPCell cell31 = new PdfPCell(image31);
                        cell31.HorizontalAlignment = 1;
                        cell31.VerticalAlignment = 5;
                        cell31.FixedHeight = 12f;
                        table8.AddCell(cell31);
                    }
                    else
                    {
                        iTextSharp.text.Image image32 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image32.ScaleAbsolute(10f, 10f);
                        PdfPCell cell32 = new PdfPCell(image32);
                        cell32.HorizontalAlignment = 1;
                        cell32.VerticalAlignment = 5;
                        cell32.FixedHeight = 12f;
                        table8.AddCell(cell32);
                    }
                    table8.AddCell(new Phrase("TP:TRIGGER POINTS", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Lumbar == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR"].ToString()
                        iTextSharp.text.Image image33 = iTextSharp.text.Image.GetInstance(checkbox);
                        image33.ScaleAbsolute(10f, 10f);
                        PdfPCell cell33 = new PdfPCell(image33);
                        cell33.HorizontalAlignment = 1;
                        cell33.VerticalAlignment = 5;
                        cell33.FixedHeight = 12f;
                        table8.AddCell(cell33);
                    }
                    else
                    {
                        iTextSharp.text.Image image34 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image34.ScaleAbsolute(10f, 10f);
                        PdfPCell cell34 = new PdfPCell(image34);
                        cell34.HorizontalAlignment = 1;
                        cell34.VerticalAlignment = 5;
                        cell34.FixedHeight = 12f;
                        table8.AddCell(cell34);
                    }
                    table8.AddCell(new Phrase("LUMBAR", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Rhomboids == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_RHOMBOIDS"].ToString()
                        iTextSharp.text.Image image35 = iTextSharp.text.Image.GetInstance(checkbox);
                        image35.ScaleAbsolute(10f, 10f);
                        PdfPCell cell35 = new PdfPCell(image35);
                        cell35.HorizontalAlignment = 1;
                        cell35.VerticalAlignment = 5;
                        cell35.FixedHeight = 12f;
                        table8.AddCell(cell35);
                    }
                    else
                    {
                        iTextSharp.text.Image image36 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image36.ScaleAbsolute(10f, 10f);
                        PdfPCell cell36 = new PdfPCell(image36);
                        cell36.HorizontalAlignment = 1;
                        cell36.VerticalAlignment = 5;
                        cell36.FixedHeight = 12f;
                        table8.AddCell(cell36);
                    }
                    table8.AddCell(new Phrase("RHOMBOIDS", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalParaSpinal == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_PARASPINAL"].ToString()
                        iTextSharp.text.Image image37 = iTextSharp.text.Image.GetInstance(checkbox);
                        image37.ScaleAbsolute(10f, 10f);
                        PdfPCell cell37 = new PdfPCell(image37);
                        cell37.HorizontalAlignment = 1;
                        cell37.VerticalAlignment = 5;
                        cell37.FixedHeight = 12f;
                        table8.AddCell(cell37);
                    }
                    else
                    {
                        iTextSharp.text.Image image38 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image38.ScaleAbsolute(10f, 10f);
                        PdfPCell cell38 = new PdfPCell(image38);
                        cell38.HorizontalAlignment = 1;
                        cell38.VerticalAlignment = 5;
                        cell38.FixedHeight = 12f;
                        table8.AddCell(cell38);
                    }
                    table8.AddCell(new Phrase("CERVICAL PARASPINAL", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Fixation == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_FIXATION"].ToString()
                        iTextSharp.text.Image image39 = iTextSharp.text.Image.GetInstance(checkbox);
                        image39.ScaleAbsolute(10f, 10f);
                        PdfPCell cell39 = new PdfPCell(image39);
                        cell39.HorizontalAlignment = 1;
                        cell39.VerticalAlignment = 5;
                        cell39.FixedHeight = 12f;
                        table8.AddCell(cell39);
                    }
                    else
                    {
                        iTextSharp.text.Image image40 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image40.ScaleAbsolute(10f, 10f);
                        PdfPCell cell40 = new PdfPCell(image40);
                        cell40.HorizontalAlignment = 1;
                        cell40.VerticalAlignment = 5;
                        cell40.FixedHeight = 12f;
                        table8.AddCell(cell40);
                    }
                    table8.AddCell(new Phrase("FX:FIXATION", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Sacrum == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_SACRUM"].ToString()
                        iTextSharp.text.Image image41 = iTextSharp.text.Image.GetInstance(checkbox);
                        image41.ScaleAbsolute(10f, 10f);
                        PdfPCell cell41 = new PdfPCell(image41);
                        cell41.HorizontalAlignment = 1;
                        cell41.VerticalAlignment = 5;
                        cell41.FixedHeight = 12f;
                        table8.AddCell(cell41);
                    }
                    else
                    {
                        iTextSharp.text.Image image42 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image42.ScaleAbsolute(10f, 10f);
                        PdfPCell cell42 = new PdfPCell(image42);
                        cell42.HorizontalAlignment = 1;
                        cell42.VerticalAlignment = 5;
                        cell42.FixedHeight = 12f;
                        table8.AddCell(cell42);
                    }
                    table8.AddCell(new Phrase("SACRUM", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Piriformis == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_PIRIFORMIS"].ToString()
                        iTextSharp.text.Image image43 = iTextSharp.text.Image.GetInstance(checkbox);
                        image43.ScaleAbsolute(10f, 10f);
                        PdfPCell cell43 = new PdfPCell(image43);
                        cell43.HorizontalAlignment = 1;
                        cell43.VerticalAlignment = 5;
                        cell43.FixedHeight = 12f;
                        table8.AddCell(cell43);
                    }
                    else
                    {
                        iTextSharp.text.Image image44 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image44.ScaleAbsolute(10f, 10f);
                        PdfPCell cell44 = new PdfPCell(image44);
                        cell44.HorizontalAlignment = 1;
                        cell44.VerticalAlignment = 5;
                        cell44.FixedHeight = 12f;
                        table8.AddCell(cell44);
                    }
                    table8.AddCell(new Phrase("PIRIFORMIS", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicParaSpinal == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_PARASPINAL"].ToString()
                        iTextSharp.text.Image image45 = iTextSharp.text.Image.GetInstance(checkbox);
                        image45.ScaleAbsolute(10f, 10f);
                        PdfPCell cell45 = new PdfPCell(image45);
                        cell45.HorizontalAlignment = 1;
                        cell45.VerticalAlignment = 5;
                        cell45.FixedHeight = 12f;
                        table8.AddCell(cell45);
                    }
                    else
                    {
                        iTextSharp.text.Image image46 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image46.ScaleAbsolute(10f, 10f);
                        PdfPCell cell46 = new PdfPCell(image46);
                        cell46.HorizontalAlignment = 1;
                        cell46.VerticalAlignment = 5;
                        cell46.FixedHeight = 12f;
                        table8.AddCell(cell46);
                    }
                    table8.AddCell(new Phrase("THORACIC PARASPINAL", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.SternocleiDomastoid == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_STERNOCLEIDOMASTOID"].ToString()
                        iTextSharp.text.Image image47 = iTextSharp.text.Image.GetInstance(checkbox);
                        image47.ScaleAbsolute(10f, 10f);
                        PdfPCell cell47 = new PdfPCell(image47);
                        cell47.HorizontalAlignment = 1;
                        cell47.VerticalAlignment = 5;
                        cell47.FixedHeight = 12f;
                        table8.AddCell(cell47);
                    }
                    else
                    {
                        iTextSharp.text.Image image48 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image48.ScaleAbsolute(10f, 10f);
                        PdfPCell cell48 = new PdfPCell(image48);
                        cell48.HorizontalAlignment = 1;
                        cell48.VerticalAlignment = 5;
                        cell48.FixedHeight = 12f;
                        table8.AddCell(cell48);
                    }
                    table8.AddCell(new Phrase("STERNOCLEIDOMASTOID(SCM)", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.Ql == "1")
                    {//chairoView.Tables[0].Rows[0]["BT_QL"].ToString()
                        iTextSharp.text.Image image49 = iTextSharp.text.Image.GetInstance(checkbox);
                        image49.ScaleAbsolute(10f, 10f);
                        PdfPCell cell49 = new PdfPCell(image49);
                        cell49.HorizontalAlignment = 1;
                        cell49.VerticalAlignment = 5;
                        cell49.FixedHeight = 12f;
                        table8.AddCell(cell49);
                    }
                    else
                    {
                        iTextSharp.text.Image image50 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image50.ScaleAbsolute(10f, 10f);
                        PdfPCell cell50 = new PdfPCell(image50);
                        cell50.HorizontalAlignment = 1;
                        cell50.VerticalAlignment = 5;
                        cell50.FixedHeight = 12f;
                        table8.AddCell(cell50);
                    }
                    table8.AddCell(new Phrase("QL", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    if (oCHNote.LumbarParaSpinal == "1")
                    { //chairoView.Tables[0].Rows[0]["BT_LUMBAR_PARASPINAL"]
                        iTextSharp.text.Image image51 = iTextSharp.text.Image.GetInstance(checkbox);
                        image51.ScaleAbsolute(10f, 10f);
                        PdfPCell cell51 = new PdfPCell(image51);
                        cell51.HorizontalAlignment = 1;
                        cell51.VerticalAlignment = 5;
                        cell51.FixedHeight = 12f;
                        table8.AddCell(cell51);
                    }
                    else
                    {
                        iTextSharp.text.Image image52 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image52.ScaleAbsolute(10f, 10f);
                        PdfPCell cell52 = new PdfPCell(image52);
                        cell52.HorizontalAlignment = 1;
                        cell52.VerticalAlignment = 5;
                        cell52.FixedHeight = 12f;
                        table8.AddCell(cell52);
                    }
                    table8.AddCell(new Phrase("LUMBAR PARASPINAL", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    table8.DefaultCell.Border = 0;
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table8.AddCell(new Phrase(""));
                    table7.AddCell(table8);
                    float[] numArray9 = new float[] { 2f, 1f, 1f, 1f, 1f };
                    PdfPTable table9 = new PdfPTable(numArray9);
                    table9.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table9.AddCell(new Phrase(""));
                    table9.AddCell(new Phrase("Right", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Left", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Both", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table9.AddCell(new Phrase("Pain Level", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    if (((oCHNote.HeadacheRight == "1") || (oCHNote.HeadacheLeft == "1")) || (oCHNote.HeadacheBoth == "1"))
                    { //chairoView.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() | chairoView.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString()
                        table9.AddCell(new Phrase("HEADACHE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.HeadacheRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString()
                            iTextSharp.text.Image image53 = iTextSharp.text.Image.GetInstance(checkbox);
                            image53.ScaleAbsolute(10f, 10f);
                            PdfPCell cell53 = new PdfPCell(image53);
                            cell53.HorizontalAlignment = 1;
                            cell53.VerticalAlignment = 5;
                            cell53.FixedHeight = 12f;
                            table9.AddCell(cell53);
                        }
                        else
                        {
                            iTextSharp.text.Image image54 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image54.ScaleAbsolute(10f, 10f);
                            PdfPCell cell54 = new PdfPCell(image54);
                            cell54.HorizontalAlignment = 1;
                            cell54.VerticalAlignment = 5;
                            cell54.FixedHeight = 12f;
                            table9.AddCell(cell54);
                        }
                        if (oCHNote.HeadacheLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString()
                            iTextSharp.text.Image image55 = iTextSharp.text.Image.GetInstance(checkbox);
                            image55.ScaleAbsolute(10f, 10f);
                            PdfPCell cell55 = new PdfPCell(image55);
                            cell55.HorizontalAlignment = 1;
                            cell55.VerticalAlignment = 5;
                            cell55.FixedHeight = 12f;
                            table9.AddCell(cell55);
                        }
                        else
                        {
                            iTextSharp.text.Image image56 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image56.ScaleAbsolute(10f, 10f);
                            PdfPCell cell56 = new PdfPCell(image56);
                            cell56.HorizontalAlignment = 1;
                            cell56.VerticalAlignment = 5;
                            cell56.FixedHeight = 12f;
                            table9.AddCell(cell56);
                        }
                        if (oCHNote.HeadacheBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString()
                            iTextSharp.text.Image image57 = iTextSharp.text.Image.GetInstance(checkbox);
                            image57.ScaleAbsolute(10f, 10f);
                            PdfPCell cell57 = new PdfPCell(image57);
                            cell57.HorizontalAlignment = 1;
                            cell57.VerticalAlignment = 5;
                            cell57.FixedHeight = 12f;
                            table9.AddCell(cell57);
                        }
                        else
                        {
                            iTextSharp.text.Image image58 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image58.ScaleAbsolute(10f, 10f);
                            PdfPCell cell58 = new PdfPCell(image58);
                            cell58.HorizontalAlignment = 1;
                            cell58.VerticalAlignment = 5;
                            cell58.FixedHeight = 12f;
                            table9.AddCell(cell58);
                        }
                        if ((oCHNote.PainLevelHeadache != null) || (oCHNote.PainLevelHeadache != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelHeadache.ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.HandRight == "1") || ( oCHNote.HandLeft== "1")) || (oCHNote.HandBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString()
                        table9.AddCell(new Phrase("HAND", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.HandRight == "1")
                        { //chairoView.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString()
                            iTextSharp.text.Image image59 = iTextSharp.text.Image.GetInstance(checkbox);
                            image59.ScaleAbsolute(10f, 10f);
                            PdfPCell cell59 = new PdfPCell(image59);
                            cell59.HorizontalAlignment = 1;
                            cell59.VerticalAlignment = 5;
                            cell59.FixedHeight = 12f;
                            table9.AddCell(cell59);
                        }
                        else
                        {
                            iTextSharp.text.Image image60 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image60.ScaleAbsolute(10f, 10f);
                            PdfPCell cell60 = new PdfPCell(image60);
                            cell60.HorizontalAlignment = 1;
                            cell60.VerticalAlignment = 5;
                            cell60.FixedHeight = 12f;
                            table9.AddCell(cell60);
                        }
                        if (oCHNote.HandLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HAND_LEFT"]
                            iTextSharp.text.Image image61 = iTextSharp.text.Image.GetInstance(checkbox);
                            image61.ScaleAbsolute(10f, 10f);
                            PdfPCell cell61 = new PdfPCell(image61);
                            cell61.HorizontalAlignment = 1;
                            cell61.VerticalAlignment = 5;
                            cell61.FixedHeight = 12f;
                            table9.AddCell(cell61);
                        }
                        else
                        {
                            iTextSharp.text.Image image62 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image62.ScaleAbsolute(10f, 10f);
                            PdfPCell cell62 = new PdfPCell(image62);
                            cell62.HorizontalAlignment = 1;
                            cell62.VerticalAlignment = 5;
                            cell62.FixedHeight = 12f;
                            table9.AddCell(cell62);
                        }
                        if (oCHNote.HandBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HAND_BOTH"]
                            iTextSharp.text.Image image63 = iTextSharp.text.Image.GetInstance(checkbox);
                            image63.ScaleAbsolute(10f, 10f);
                            PdfPCell cell63 = new PdfPCell(image63);
                            cell63.HorizontalAlignment = 1;
                            cell63.VerticalAlignment = 5;
                            cell63.FixedHeight = 12f;
                            table9.AddCell(cell63);
                        }
                        else
                        {
                            iTextSharp.text.Image image64 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image64.ScaleAbsolute(10f, 10f);
                            PdfPCell cell64 = new PdfPCell(image64);
                            cell64.HorizontalAlignment = 1;
                            cell64.VerticalAlignment = 5;
                            cell64.FixedHeight = 12f;
                            table9.AddCell(cell64);
                        }
                        if ((oCHNote.PainLevelHand != null) || (oCHNote.PainLevelHand != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelHand, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.NeckRight == "1") || ( oCHNote.NeckLeft== "1")) || (oCHNote.NeckBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_NECK_RIGHT"]|chairoView.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString()
                        table9.AddCell(new Phrase("NECK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.NeckRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString()
                            iTextSharp.text.Image image65 = iTextSharp.text.Image.GetInstance(checkbox);
                            image65.ScaleAbsolute(10f, 10f);
                            PdfPCell cell65 = new PdfPCell(image65);
                            cell65.HorizontalAlignment = 1;
                            cell65.VerticalAlignment = 5;
                            cell65.FixedHeight = 12f;
                            table9.AddCell(cell65);
                        }
                        else
                        {
                            iTextSharp.text.Image image66 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image66.ScaleAbsolute(10f, 10f);
                            PdfPCell cell66 = new PdfPCell(image66);
                            cell66.HorizontalAlignment = 1;
                            cell66.VerticalAlignment = 5;
                            cell66.FixedHeight = 12f;
                            table9.AddCell(cell66);
                        }
                        if ( oCHNote.NeckLeft== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString()
                            iTextSharp.text.Image image67 = iTextSharp.text.Image.GetInstance(checkbox);
                            image67.ScaleAbsolute(10f, 10f);
                            PdfPCell cell67 = new PdfPCell(image67);
                            cell67.HorizontalAlignment = 1;
                            cell67.VerticalAlignment = 5;
                            cell67.FixedHeight = 12f;
                            table9.AddCell(cell67);
                        }
                        else
                        {
                            iTextSharp.text.Image image68 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image68.ScaleAbsolute(10f, 10f);
                            PdfPCell cell68 = new PdfPCell(image68);
                            cell68.HorizontalAlignment = 1;
                            cell68.VerticalAlignment = 5;
                            cell68.FixedHeight = 12f;
                            table9.AddCell(cell68);
                        }
                        if (oCHNote.NeckBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString()
                            iTextSharp.text.Image image69 = iTextSharp.text.Image.GetInstance(checkbox);
                            image69.ScaleAbsolute(10f, 10f);
                            PdfPCell cell69 = new PdfPCell(image69);
                            cell69.HorizontalAlignment = 1;
                            cell69.VerticalAlignment = 5;
                            cell69.FixedHeight = 12f;
                            table9.AddCell(cell69);
                        }
                        else
                        {
                            iTextSharp.text.Image image70 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image70.ScaleAbsolute(10f, 10f);
                            PdfPCell cell70 = new PdfPCell(image70);
                            cell70.HorizontalAlignment = 1;
                            cell70.VerticalAlignment = 5;
                            cell70.FixedHeight = 12f;
                            table9.AddCell(cell70);
                        }
                        if ((oCHNote.PainLevelNeck != null) || (oCHNote.PainLevelNeck != ""))
                        { //chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"]||chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelNeck, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oCHNote.FingersRight== "1") || ( oCHNote.FingersLeft== "1")) || ( oCHNote.FingersBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() || chairoView.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString()
                        table9.AddCell(new Phrase("FINGERS", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.FingersRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString()
                            iTextSharp.text.Image image71 = iTextSharp.text.Image.GetInstance(checkbox);
                            image71.ScaleAbsolute(10f, 10f);
                            PdfPCell cell71 = new PdfPCell(image71);
                            cell71.HorizontalAlignment = 1;
                            cell71.VerticalAlignment = 5;
                            cell71.FixedHeight = 12f;
                            table9.AddCell(cell71);
                        }
                        else
                        {
                            iTextSharp.text.Image image72 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image72.ScaleAbsolute(10f, 10f);
                            PdfPCell cell72 = new PdfPCell(image72);
                            cell72.HorizontalAlignment = 1;
                            cell72.VerticalAlignment = 5;
                            cell72.FixedHeight = 12f;
                            table9.AddCell(cell72);
                        }
                        if (oCHNote.FingersLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString()
                            iTextSharp.text.Image image73 = iTextSharp.text.Image.GetInstance(checkbox);
                            image73.ScaleAbsolute(10f, 10f);
                            PdfPCell cell73 = new PdfPCell(image73);
                            cell73.HorizontalAlignment = 1;
                            cell73.VerticalAlignment = 5;
                            cell73.FixedHeight = 12f;
                            table9.AddCell(cell73);
                        }
                        else
                        {
                            iTextSharp.text.Image image74 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image74.ScaleAbsolute(10f, 10f);
                            PdfPCell cell74 = new PdfPCell(image74);
                            cell74.HorizontalAlignment = 1;
                            cell74.VerticalAlignment = 5;
                            cell74.FixedHeight = 12f;
                            table9.AddCell(cell74);
                        }
                        if ( oCHNote.FingersBoth== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString()
                            iTextSharp.text.Image image75 = iTextSharp.text.Image.GetInstance(checkbox);
                            image75.ScaleAbsolute(10f, 10f);
                            PdfPCell cell75 = new PdfPCell(image75);
                            cell75.HorizontalAlignment = 1;
                            cell75.VerticalAlignment = 5;
                            cell75.FixedHeight = 12f;
                            table9.AddCell(cell75);
                        }
                        else
                        {
                            iTextSharp.text.Image image76 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image76.ScaleAbsolute(10f, 10f);
                            PdfPCell cell76 = new PdfPCell(image76);
                            cell76.HorizontalAlignment = 1;
                            cell76.VerticalAlignment = 5;
                            cell76.FixedHeight = 12f;
                            table9.AddCell(cell76);
                        }
                        if ((oCHNote.PainLevelFingers != null) || (oCHNote.PainLevelFingers != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelFingers, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.MidBackRight == "1") || (oCHNote.MidBackLeft == "1")) || ( oCHNote.MidBackBoth== "1"))
                    { //chairoView.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString()
                        table9.AddCell(new Phrase("MID BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.MidBackRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image77 = iTextSharp.text.Image.GetInstance(checkbox);
                            image77.ScaleAbsolute(10f, 10f);
                            PdfPCell cell77 = new PdfPCell(image77);
                            cell77.HorizontalAlignment = 1;
                            cell77.VerticalAlignment = 5;
                            cell77.FixedHeight = 12f;
                            table9.AddCell(cell77);
                        }
                        else
                        {
                            iTextSharp.text.Image image78 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image78.ScaleAbsolute(10f, 10f);
                            PdfPCell cell78 = new PdfPCell(image78);
                            cell78.HorizontalAlignment = 1;
                            cell78.VerticalAlignment = 5;
                            cell78.FixedHeight = 12f;
                            table9.AddCell(cell78);
                        }
                        if (oCHNote.MidBackLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString()
                            iTextSharp.text.Image image79 = iTextSharp.text.Image.GetInstance(checkbox);
                            image79.ScaleAbsolute(10f, 10f);
                            PdfPCell cell79 = new PdfPCell(image79);
                            cell79.HorizontalAlignment = 1;
                            cell79.VerticalAlignment = 5;
                            cell79.FixedHeight = 12f;
                            table9.AddCell(cell79);
                        }
                        else
                        {
                            iTextSharp.text.Image image80 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image80.ScaleAbsolute(10f, 10f);
                            PdfPCell cell80 = new PdfPCell(image80);
                            cell80.HorizontalAlignment = 1;
                            cell80.VerticalAlignment = 5;
                            cell80.FixedHeight = 12f;
                            table9.AddCell(cell80);
                        }
                        if ( oCHNote.MidBackBoth== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image81 = iTextSharp.text.Image.GetInstance(checkbox);
                            image81.ScaleAbsolute(10f, 10f);
                            PdfPCell cell81 = new PdfPCell(image81);
                            cell81.HorizontalAlignment = 1;
                            cell81.VerticalAlignment = 5;
                            cell81.FixedHeight = 12f;
                            table9.AddCell(cell81);
                        }
                        else
                        {
                            iTextSharp.text.Image image82 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image82.ScaleAbsolute(10f, 10f);
                            PdfPCell cell82 = new PdfPCell(image82);
                            cell82.HorizontalAlignment = 1;
                            cell82.VerticalAlignment = 5;
                            cell82.FixedHeight = 12f;
                            table9.AddCell(cell82);
                        }
                        if ((oCHNote.PainLevelMidBack != null) || (oCHNote.PainLevelMidBack != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelMidBack, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oCHNote.HipRight== "1") || ( oCHNote.HipLeft== "1")) || (oCHNote.HipBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString()
                        table9.AddCell(new Phrase("HIP", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.HipRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString()
                            iTextSharp.text.Image image83 = iTextSharp.text.Image.GetInstance(checkbox);
                            image83.ScaleAbsolute(10f, 10f);
                            PdfPCell cell83 = new PdfPCell(image83);
                            cell83.HorizontalAlignment = 1;
                            cell83.VerticalAlignment = 5;
                            cell83.FixedHeight = 12f;
                            table9.AddCell(cell83);
                        }
                        else
                        {
                            iTextSharp.text.Image image84 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image84.ScaleAbsolute(10f, 10f);
                            PdfPCell cell84 = new PdfPCell(image84);
                            cell84.HorizontalAlignment = 1;
                            cell84.VerticalAlignment = 5;
                            cell84.FixedHeight = 12f;
                            table9.AddCell(cell84);
                        }
                        if (oCHNote.HipLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString()
                            iTextSharp.text.Image image85 = iTextSharp.text.Image.GetInstance(checkbox);
                            image85.ScaleAbsolute(10f, 10f);
                            PdfPCell cell85 = new PdfPCell(image85);
                            cell85.HorizontalAlignment = 1;
                            cell85.VerticalAlignment = 5;
                            cell85.FixedHeight = 12f;
                            table9.AddCell(cell85);
                        }
                        else
                        {
                            iTextSharp.text.Image image86 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image86.ScaleAbsolute(10f, 10f);
                            PdfPCell cell86 = new PdfPCell(image86);
                            cell86.HorizontalAlignment = 1;
                            cell86.VerticalAlignment = 5;
                            cell86.FixedHeight = 12f;
                            table9.AddCell(cell86);
                        }
                        if (oCHNote.HipBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString()
                            iTextSharp.text.Image image87 = iTextSharp.text.Image.GetInstance(checkbox);
                            image87.ScaleAbsolute(10f, 10f);
                            PdfPCell cell87 = new PdfPCell(image87);
                            cell87.HorizontalAlignment = 1;
                            cell87.VerticalAlignment = 5;
                            cell87.FixedHeight = 12f;
                            table9.AddCell(cell87);
                        }
                        else
                        {
                            iTextSharp.text.Image image88 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image88.ScaleAbsolute(10f, 10f);
                            PdfPCell cell88 = new PdfPCell(image88);
                            cell88.HorizontalAlignment = 1;
                            cell88.VerticalAlignment = 5;
                            cell88.FixedHeight = 12f;
                            table9.AddCell(cell88);
                        }
                        if ((oCHNote.PainLevelHip != null) || (oCHNote.PainLevelHip != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelHip, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.LowBackRight == "1") || (oCHNote.LowBackLeft == "1")) || (oCHNote.LowBackBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"]|chairoView.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString()
                        table9.AddCell(new Phrase("LOW BACK", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.LowBackRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString()
                            iTextSharp.text.Image image89 = iTextSharp.text.Image.GetInstance(checkbox);
                            image89.ScaleAbsolute(10f, 10f);
                            PdfPCell cell89 = new PdfPCell(image89);
                            cell89.HorizontalAlignment = 1;
                            cell89.VerticalAlignment = 5;
                            cell89.FixedHeight = 12f;
                            table9.AddCell(cell89);
                        }
                        else
                        {
                            iTextSharp.text.Image image90 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image90.ScaleAbsolute(10f, 10f);
                            PdfPCell cell90 = new PdfPCell(image90);
                            cell90.HorizontalAlignment = 1;
                            cell90.VerticalAlignment = 5;
                            cell90.FixedHeight = 12f;
                            table9.AddCell(cell90);
                        }
                        if (oCHNote.LowBackLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() 
                            iTextSharp.text.Image image91 = iTextSharp.text.Image.GetInstance(checkbox);
                            image91.ScaleAbsolute(10f, 10f);
                            PdfPCell cell91 = new PdfPCell(image91);
                            cell91.HorizontalAlignment = 1;
                            cell91.VerticalAlignment = 5;
                            cell91.FixedHeight = 12f;
                            table9.AddCell(cell91);
                        }
                        else
                        {
                            iTextSharp.text.Image image92 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image92.ScaleAbsolute(10f, 10f);
                            PdfPCell cell92 = new PdfPCell(image92);
                            cell92.HorizontalAlignment = 1;
                            cell92.VerticalAlignment = 5;
                            cell92.FixedHeight = 12f;
                            table9.AddCell(cell92);
                        }
                        if (oCHNote.LowBackBoth== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString()
                            iTextSharp.text.Image image93 = iTextSharp.text.Image.GetInstance(checkbox);
                            image93.ScaleAbsolute(10f, 10f);
                            PdfPCell cell93 = new PdfPCell(image93);
                            cell93.HorizontalAlignment = 1;
                            cell93.VerticalAlignment = 5;
                            cell93.FixedHeight = 12f;
                            table9.AddCell(cell93);
                        }
                        else
                        {
                            iTextSharp.text.Image image94 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image94.ScaleAbsolute(10f, 10f);
                            PdfPCell cell94 = new PdfPCell(image94);
                            cell94.HorizontalAlignment = 1;
                            cell94.VerticalAlignment = 5;
                            cell94.FixedHeight = 12f;
                            table9.AddCell(cell94);
                        }
                        if ((oCHNote.PainLevelLowBack != null) || (oCHNote.PainLevelLowBack != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelLowBack.ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oCHNote.ThighRight== "1") || ( oCHNote.ThighLeft== "1")) || ( oCHNote.ThighBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString()
                        table9.AddCell(new Phrase("THIGH", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.ThighRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString()
                            iTextSharp.text.Image image95 = iTextSharp.text.Image.GetInstance(checkbox);
                            image95.ScaleAbsolute(10f, 10f);
                            PdfPCell cell95 = new PdfPCell(image95);
                            cell95.HorizontalAlignment = 1;
                            cell95.VerticalAlignment = 5;
                            cell95.FixedHeight = 12f;
                            table9.AddCell(cell95);
                        }
                        else
                        {
                            iTextSharp.text.Image image96 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image96.ScaleAbsolute(10f, 10f);
                            PdfPCell cell96 = new PdfPCell(image96);
                            cell96.HorizontalAlignment = 1;
                            cell96.VerticalAlignment = 5;
                            cell96.FixedHeight = 12f;
                            table9.AddCell(cell96);
                        }
                        if (oCHNote.ThighLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString()
                            iTextSharp.text.Image image97 = iTextSharp.text.Image.GetInstance(checkbox);
                            image97.ScaleAbsolute(10f, 10f);
                            PdfPCell cell97 = new PdfPCell(image97);
                            cell97.HorizontalAlignment = 1;
                            cell97.VerticalAlignment = 5;
                            cell97.FixedHeight = 12f;
                            table9.AddCell(cell97);
                        }
                        else
                        {
                            iTextSharp.text.Image image98 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image98.ScaleAbsolute(10f, 10f);
                            PdfPCell cell98 = new PdfPCell(image98);
                            cell98.HorizontalAlignment = 1;
                            cell98.VerticalAlignment = 5;
                            cell98.FixedHeight = 12f;
                            table9.AddCell(cell98);
                        }
                        if (oCHNote.ThighBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString()
                            iTextSharp.text.Image image99 = iTextSharp.text.Image.GetInstance(checkbox);
                            image99.ScaleAbsolute(10f, 10f);
                            PdfPCell cell99 = new PdfPCell(image99);
                            cell99.HorizontalAlignment = 1;
                            cell99.VerticalAlignment = 5;
                            cell99.FixedHeight = 12f;
                            table9.AddCell(cell99);
                        }
                        else
                        {
                            iTextSharp.text.Image image100 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image100.ScaleAbsolute(10f, 10f);
                            PdfPCell cell100 = new PdfPCell(image100);
                            cell100.HorizontalAlignment = 1;
                            cell100.VerticalAlignment = 5;
                            cell100.FixedHeight = 12f;
                            table9.AddCell(cell100);
                        }
                        if ((oCHNote.PainLevelThigh != null) || (oCHNote.PainLevelThigh != ""))
                        {
                            //chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"] 
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelThigh, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.JawRight == "1") || (oCHNote.JawLeft == "1")) || ( oCHNote.JawBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString()
                        table9.AddCell(new Phrase("JAW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.JawRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString()
                            iTextSharp.text.Image image101 = iTextSharp.text.Image.GetInstance(checkbox);
                            image101.ScaleAbsolute(10f, 10f);
                            PdfPCell cell101 = new PdfPCell(image101);
                            cell101.HorizontalAlignment = 1;
                            cell101.VerticalAlignment = 5;
                            cell101.FixedHeight = 12f;
                            table9.AddCell(cell101);
                        }
                        else
                        {
                            iTextSharp.text.Image image102 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image102.ScaleAbsolute(10f, 10f);
                            PdfPCell cell102 = new PdfPCell(image102);
                            cell102.HorizontalAlignment = 1;
                            cell102.VerticalAlignment = 5;
                            cell102.FixedHeight = 12f;
                            table9.AddCell(cell102);
                        }
                        if (oCHNote.JawLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString()
                            iTextSharp.text.Image image103 = iTextSharp.text.Image.GetInstance(checkbox);
                            image103.ScaleAbsolute(10f, 10f);
                            PdfPCell cell103 = new PdfPCell(image103);
                            cell103.HorizontalAlignment = 1;
                            cell103.VerticalAlignment = 5;
                            cell103.FixedHeight = 12f;
                            table9.AddCell(cell103);
                        }
                        else
                        {
                            iTextSharp.text.Image image104 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image104.ScaleAbsolute(10f, 10f);
                            PdfPCell cell104 = new PdfPCell(image104);
                            cell104.HorizontalAlignment = 1;
                            cell104.VerticalAlignment = 5;
                            cell104.FixedHeight = 12f;
                            table9.AddCell(cell104);
                        }
                        if (oCHNote.JawBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString()
                            iTextSharp.text.Image image105 = iTextSharp.text.Image.GetInstance(checkbox);
                            image105.ScaleAbsolute(10f, 10f);
                            PdfPCell cell105 = new PdfPCell(image105);
                            cell105.HorizontalAlignment = 1;
                            cell105.VerticalAlignment = 5;
                            cell105.FixedHeight = 12f;
                            table9.AddCell(cell105);
                        }
                        else
                        {
                            iTextSharp.text.Image image106 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image106.ScaleAbsolute(10f, 10f);
                            PdfPCell cell106 = new PdfPCell(image106);
                            cell106.HorizontalAlignment = 1;
                            cell106.VerticalAlignment = 5;
                            cell106.FixedHeight = 12f;
                            table9.AddCell(cell106);
                        }
                        if ((oCHNote.PainLevelJaw != null) || (oCHNote.PainLevelJaw != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelJaw, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.KneeRight == "1") || (oCHNote.KneeLeft == "1")) || ( oCHNote.KneeBOTH== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString()
                        table9.AddCell(new Phrase("KNEE", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.KneeRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString()
                            iTextSharp.text.Image image107 = iTextSharp.text.Image.GetInstance(checkbox);
                            image107.ScaleAbsolute(10f, 10f);
                            PdfPCell cell107 = new PdfPCell(image107);
                            cell107.HorizontalAlignment = 1;
                            cell107.VerticalAlignment = 5;
                            cell107.FixedHeight = 12f;
                            table9.AddCell(cell107);
                        }
                        else
                        {
                            iTextSharp.text.Image image108 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image108.ScaleAbsolute(10f, 10f);
                            PdfPCell cell108 = new PdfPCell(image108);
                            cell108.HorizontalAlignment = 1;
                            cell108.VerticalAlignment = 5;
                            cell108.FixedHeight = 12f;
                            table9.AddCell(cell108);
                        }
                        if ( oCHNote.KneeLeft== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString()
                            iTextSharp.text.Image image109 = iTextSharp.text.Image.GetInstance(checkbox);
                            image109.ScaleAbsolute(10f, 10f);
                            PdfPCell cell109 = new PdfPCell(image109);
                            cell109.HorizontalAlignment = 1;
                            cell109.VerticalAlignment = 5;
                            cell109.FixedHeight = 12f;
                            table9.AddCell(cell109);
                        }
                        else
                        {
                            iTextSharp.text.Image image110 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image110.ScaleAbsolute(10f, 10f);
                            PdfPCell cell110 = new PdfPCell(image110);
                            cell110.HorizontalAlignment = 1;
                            cell110.VerticalAlignment = 5;
                            cell110.FixedHeight = 12f;
                            table9.AddCell(cell110);
                        }
                        if ( oCHNote.KneeBOTH== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString()
                            iTextSharp.text.Image image111 = iTextSharp.text.Image.GetInstance(checkbox);
                            image111.ScaleAbsolute(10f, 10f);
                            PdfPCell cell111 = new PdfPCell(image111);
                            cell111.HorizontalAlignment = 1;
                            cell111.VerticalAlignment = 5;
                            cell111.FixedHeight = 12f;
                            table9.AddCell(cell111);
                        }
                        else
                        {
                            iTextSharp.text.Image image112 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image112.ScaleAbsolute(10f, 10f);
                            PdfPCell cell112 = new PdfPCell(image112);
                            cell112.HorizontalAlignment = 1;
                            cell112.VerticalAlignment = 5;
                            cell112.FixedHeight = 12f;
                            table9.AddCell(cell112);
                        }
                        if ((oCHNote.PainLevelKnee != null) || (oCHNote.PainLevelKnee != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"]||chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelKnee, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oCHNote.ShoulderRight== "1") || (oCHNote.ShoulderLeft == "1")) || ( oCHNote.ShoulderBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString()
                        table9.AddCell(new Phrase("SHOULDER", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.ShoulderRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString()
                            iTextSharp.text.Image image113 = iTextSharp.text.Image.GetInstance(checkbox);
                            image113.ScaleAbsolute(10f, 10f);
                            PdfPCell cell113 = new PdfPCell(image113);
                            cell113.HorizontalAlignment = 1;
                            cell113.VerticalAlignment = 5;
                            cell113.FixedHeight = 12f;
                            table9.AddCell(cell113);
                        }
                        else
                        {
                            iTextSharp.text.Image image114 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image114.ScaleAbsolute(10f, 10f);
                            PdfPCell cell114 = new PdfPCell(image114);
                            cell114.HorizontalAlignment = 1;
                            cell114.VerticalAlignment = 5;
                            cell114.FixedHeight = 12f;
                            table9.AddCell(cell114);
                        }
                        if (oCHNote.ShoulderLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString()
                            iTextSharp.text.Image image115 = iTextSharp.text.Image.GetInstance(checkbox);
                            image115.ScaleAbsolute(10f, 10f);
                            PdfPCell cell115 = new PdfPCell(image115);
                            cell115.HorizontalAlignment = 1;
                            cell115.VerticalAlignment = 5;
                            cell115.FixedHeight = 12f;
                            table9.AddCell(cell115);
                        }
                        else
                        {
                            iTextSharp.text.Image image116 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image116.ScaleAbsolute(10f, 10f);
                            PdfPCell cell116 = new PdfPCell(image116);
                            cell116.HorizontalAlignment = 1;
                            cell116.VerticalAlignment = 5;
                            cell116.FixedHeight = 12f;
                            table9.AddCell(cell116);
                        }
                        if (oCHNote.ShoulderBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString()
                            iTextSharp.text.Image image117 = iTextSharp.text.Image.GetInstance(checkbox);
                            image117.ScaleAbsolute(10f, 10f);
                            PdfPCell cell117 = new PdfPCell(image117);
                            cell117.HorizontalAlignment = 1;
                            cell117.VerticalAlignment = 5;
                            cell117.FixedHeight = 12f;
                            table9.AddCell(cell117);
                        }
                        else
                        {
                            iTextSharp.text.Image image118 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image118.ScaleAbsolute(10f, 10f);
                            PdfPCell cell118 = new PdfPCell(image118);
                            cell118.HorizontalAlignment = 1;
                            cell118.VerticalAlignment = 5;
                            cell118.FixedHeight = 12f;
                            table9.AddCell(cell118);
                        }
                        if ((oCHNote.PainLevelShoulder != null) || (oCHNote.PainLevelShoulder != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelShoulder, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.LowerLegRight== "1") || (oCHNote.LowerLegLeft == "1")) || (oCHNote.LowerLegBOTH == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"]
                        table9.AddCell(new Phrase("LOWER LEG", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if ( oCHNote.LowerLegRight== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString()
                            iTextSharp.text.Image image119 = iTextSharp.text.Image.GetInstance(checkbox);
                            image119.ScaleAbsolute(10f, 10f);
                            PdfPCell cell119 = new PdfPCell(image119);
                            cell119.HorizontalAlignment = 1;
                            cell119.VerticalAlignment = 5;
                            cell119.FixedHeight = 12f;
                            table9.AddCell(cell119);
                        }
                        else
                        {
                            iTextSharp.text.Image image120 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image120.ScaleAbsolute(10f, 10f);
                            PdfPCell cell120 = new PdfPCell(image120);
                            cell120.HorizontalAlignment = 1;
                            cell120.VerticalAlignment = 5;
                            cell120.FixedHeight = 12f;
                            table9.AddCell(cell120);
                        }
                        if ( oCHNote.LowerLegLeft== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString()
                            iTextSharp.text.Image image121 = iTextSharp.text.Image.GetInstance(checkbox);
                            image121.ScaleAbsolute(10f, 10f);
                            PdfPCell cell121 = new PdfPCell(image121);
                            cell121.HorizontalAlignment = 1;
                            cell121.VerticalAlignment = 5;
                            cell121.FixedHeight = 12f;
                            table9.AddCell(cell121);
                        }
                        else
                        {
                            iTextSharp.text.Image image122 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image122.ScaleAbsolute(10f, 10f);
                            PdfPCell cell122 = new PdfPCell(image122);
                            cell122.HorizontalAlignment = 1;
                            cell122.VerticalAlignment = 5;
                            cell122.FixedHeight = 12f;
                            table9.AddCell(cell122);
                        }
                        if (oCHNote.LowerLegBOTH == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString()
                            iTextSharp.text.Image image123 = iTextSharp.text.Image.GetInstance(checkbox);
                            image123.ScaleAbsolute(10f, 10f);
                            PdfPCell cell123 = new PdfPCell(image123);
                            cell123.HorizontalAlignment = 1;
                            cell123.VerticalAlignment = 5;
                            cell123.FixedHeight = 12f;
                            table9.AddCell(cell123);
                        }
                        else
                        {
                            iTextSharp.text.Image image124 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image124.ScaleAbsolute(10f, 10f);
                            PdfPCell cell124 = new PdfPCell(image124);
                            cell124.HorizontalAlignment = 1;
                            cell124.VerticalAlignment = 5;
                            cell124.FixedHeight = 12f;
                            table9.AddCell(cell124);
                        }
                        if ((oCHNote.PainLevelLowerLeg != null) || (oCHNote.PainLevelLowerLeg != ""))
                        {
                            //chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"]|chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelLowerLeg, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.ElbowRight == "1") || (oCHNote.ElbowLeft == "1")) || ( oCHNote.ElbowBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString()
                        table9.AddCell(new Phrase("ELBOW", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.ElbowRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString()
                            iTextSharp.text.Image image125 = iTextSharp.text.Image.GetInstance(checkbox);
                            image125.ScaleAbsolute(10f, 10f);
                            PdfPCell cell125 = new PdfPCell(image125);
                            cell125.HorizontalAlignment = 1;
                            cell125.VerticalAlignment = 5;
                            cell125.FixedHeight = 12f;
                            table9.AddCell(cell125);
                        }
                        else
                        {
                            iTextSharp.text.Image image126 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image126.ScaleAbsolute(10f, 10f);
                            PdfPCell cell126 = new PdfPCell(image126);
                            cell126.HorizontalAlignment = 1;
                            cell126.VerticalAlignment = 5;
                            cell126.FixedHeight = 12f;
                            table9.AddCell(cell126);
                        }
                        if (oCHNote.ElbowLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString()
                            iTextSharp.text.Image image127 = iTextSharp.text.Image.GetInstance(checkbox);
                            image127.ScaleAbsolute(10f, 10f);
                            PdfPCell cell127 = new PdfPCell(image127);
                            cell127.HorizontalAlignment = 1;
                            cell127.VerticalAlignment = 5;
                            cell127.FixedHeight = 12f;
                            table9.AddCell(cell127);
                        }
                        else
                        {
                            iTextSharp.text.Image image128 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image128.ScaleAbsolute(10f, 10f);
                            PdfPCell cell128 = new PdfPCell(image128);
                            cell128.HorizontalAlignment = 1;
                            cell128.VerticalAlignment = 5;
                            cell128.FixedHeight = 12f;
                            table9.AddCell(cell128);
                        }
                        if ( oCHNote.ElbowBoth== "1")
                        {///chairoView.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString()
                            iTextSharp.text.Image image129 = iTextSharp.text.Image.GetInstance(checkbox);
                            image129.ScaleAbsolute(10f, 10f);
                            PdfPCell cell129 = new PdfPCell(image129);
                            cell129.HorizontalAlignment = 1;
                            cell129.VerticalAlignment = 5;
                            cell129.FixedHeight = 12f;
                            table9.AddCell(cell129);
                        }
                        else
                        {
                            iTextSharp.text.Image image130 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image130.ScaleAbsolute(10f, 10f);
                            PdfPCell cell130 = new PdfPCell(image130);
                            cell130.HorizontalAlignment = 1;
                            cell130.VerticalAlignment = 5;
                            cell130.FixedHeight = 12f;
                            table9.AddCell(cell130);
                        }
                        if ((oCHNote.PainLevelElbow != null) || (oCHNote.PainLevelElbow != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelElbow.ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.FootRight== "1") || ( oCHNote.FootLeft== "1")) || ( oCHNote.FootBoth== "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() |chairoView.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString()
                        table9.AddCell(new Phrase("FOOT", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.FootRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString()
                            iTextSharp.text.Image image131 = iTextSharp.text.Image.GetInstance(checkbox);
                            image131.ScaleAbsolute(10f, 10f);
                            PdfPCell cell131 = new PdfPCell(image131);
                            cell131.HorizontalAlignment = 1;
                            cell131.VerticalAlignment = 5;
                            cell131.FixedHeight = 12f;
                            table9.AddCell(cell131);
                        }
                        else
                        {
                            iTextSharp.text.Image image132 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image132.ScaleAbsolute(10f, 10f);
                            PdfPCell cell132 = new PdfPCell(image132);
                            cell132.HorizontalAlignment = 1;
                            cell132.VerticalAlignment = 5;
                            cell132.FixedHeight = 12f;
                            table9.AddCell(cell132);
                        }
                        if ( oCHNote.FootLeft== "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString()
                            iTextSharp.text.Image image133 = iTextSharp.text.Image.GetInstance(checkbox);
                            image133.ScaleAbsolute(10f, 10f);
                            PdfPCell cell133 = new PdfPCell(image133);
                            cell133.HorizontalAlignment = 1;
                            cell133.VerticalAlignment = 5;
                            cell133.FixedHeight = 12f;
                            table9.AddCell(cell133);
                        }
                        else
                        {
                            iTextSharp.text.Image image134 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image134.ScaleAbsolute(10f, 10f);
                            PdfPCell cell134 = new PdfPCell(image134);
                            cell134.HorizontalAlignment = 1;
                            cell134.VerticalAlignment = 5;
                            cell134.FixedHeight = 12f;
                            table9.AddCell(cell134);
                        }
                        if (oCHNote.FootBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString()
                            iTextSharp.text.Image image135 = iTextSharp.text.Image.GetInstance(checkbox);
                            image135.ScaleAbsolute(10f, 10f);
                            PdfPCell cell135 = new PdfPCell(image135);
                            cell135.HorizontalAlignment = 1;
                            cell135.VerticalAlignment = 5;
                            cell135.FixedHeight = 12f;
                            table9.AddCell(cell135);
                        }
                        else
                        {
                            iTextSharp.text.Image image136 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image136.ScaleAbsolute(10f, 10f);
                            PdfPCell cell136 = new PdfPCell(image136);
                            cell136.HorizontalAlignment = 1;
                            cell136.VerticalAlignment = 5;
                            cell136.FixedHeight = 12f;
                            table9.AddCell(cell136);
                        }
                        if ((oCHNote.PainLevelFoot != null) || (oCHNote.PainLevelFoot != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"] 
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelFoot, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if (((oCHNote.WristRight == "1") || (oCHNote.WristLeft == "1")) || (oCHNote.WristBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString()
                        table9.AddCell(new Phrase("WRIST", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.WristRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString()
                            iTextSharp.text.Image image137 = iTextSharp.text.Image.GetInstance(checkbox);
                            image137.ScaleAbsolute(10f, 10f);
                            PdfPCell cell137 = new PdfPCell(image137);
                            cell137.HorizontalAlignment = 1;
                            cell137.VerticalAlignment = 5;
                            cell137.FixedHeight = 12f;
                            table9.AddCell(cell137);
                        }
                        else
                        {
                            iTextSharp.text.Image image138 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image138.ScaleAbsolute(10f, 10f);
                            PdfPCell cell138 = new PdfPCell(image138);
                            cell138.HorizontalAlignment = 1;
                            cell138.VerticalAlignment = 5;
                            cell138.FixedHeight = 12f;
                            table9.AddCell(cell138);
                        }
                        if (oCHNote.WristLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString()
                            iTextSharp.text.Image image139 = iTextSharp.text.Image.GetInstance(checkbox);
                            image139.ScaleAbsolute(10f, 10f);
                            PdfPCell cell139 = new PdfPCell(image139);
                            cell139.HorizontalAlignment = 1;
                            cell139.VerticalAlignment = 5;
                            cell139.FixedHeight = 12f;
                            table9.AddCell(cell139);
                        }
                        else
                        {
                            iTextSharp.text.Image image140 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image140.ScaleAbsolute(10f, 10f);
                            PdfPCell cell140 = new PdfPCell(image140);
                            cell140.HorizontalAlignment = 1;
                            cell140.VerticalAlignment = 5;
                            cell140.FixedHeight = 12f;
                            table9.AddCell(cell140);
                        }
                        if (oCHNote.WristBoth == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString()
                            iTextSharp.text.Image image141 = iTextSharp.text.Image.GetInstance(checkbox);
                            image141.ScaleAbsolute(10f, 10f);
                            PdfPCell cell141 = new PdfPCell(image141);
                            cell141.HorizontalAlignment = 1;
                            cell141.VerticalAlignment = 5;
                            cell141.FixedHeight = 12f;
                            table9.AddCell(cell141);
                        }
                        else
                        {
                            iTextSharp.text.Image image142 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image142.ScaleAbsolute(10f, 10f);
                            PdfPCell cell142 = new PdfPCell(image142);
                            cell142.HorizontalAlignment = 1;
                            cell142.VerticalAlignment = 5;
                            cell142.FixedHeight = 12f;
                            table9.AddCell(cell142);
                        }
                        if ((oCHNote.PainLevelWrist != null) || (oCHNote.PainLevelWrist != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelWrist, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            table9.AddCell(new Phrase(""));
                        }
                    }
                    if ((( oCHNote.ToesRight== "1") || ( oCHNote.ToesLeft== "1")) || (oCHNote.ToesBoth == "1"))
                    {//chairoView.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString()|chairoView.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString()|chairoView.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString()
                        table9.AddCell(new Phrase("TOES", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        if (oCHNote.ToesRight == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString()
                            iTextSharp.text.Image image143 = iTextSharp.text.Image.GetInstance(checkbox);
                            image143.ScaleAbsolute(10f, 10f);
                            PdfPCell cell143 = new PdfPCell(image143);
                            cell143.HorizontalAlignment = 1;
                            cell143.VerticalAlignment = 5;
                            cell143.FixedHeight = 12f;
                            table9.AddCell(cell143);
                        }
                        else
                        {
                            iTextSharp.text.Image image144 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image144.ScaleAbsolute(10f, 10f);
                            PdfPCell cell144 = new PdfPCell(image144);
                            cell144.HorizontalAlignment = 1;
                            cell144.VerticalAlignment = 5;
                            cell144.FixedHeight = 12f;
                            table9.AddCell(cell144);
                        }
                        if (oCHNote.ToesLeft == "1")
                        {//chairoView.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString()
                            iTextSharp.text.Image image145 = iTextSharp.text.Image.GetInstance(checkbox);
                            image145.ScaleAbsolute(10f, 10f);
                            PdfPCell cell145 = new PdfPCell(image145);
                            cell145.HorizontalAlignment = 1;
                            cell145.VerticalAlignment = 5;
                            cell145.FixedHeight = 12f;
                            table9.AddCell(cell145);
                        }
                        else
                        {
                            iTextSharp.text.Image image146 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image146.ScaleAbsolute(10f, 10f);
                            PdfPCell cell146 = new PdfPCell(image146);
                            cell146.HorizontalAlignment = 1;
                            cell146.VerticalAlignment = 5;
                            cell146.FixedHeight = 12f;
                            table9.AddCell(cell146);
                        }
                        if ( oCHNote.ToesBoth== "1")
                        {
                           //chairoView.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() 
                            iTextSharp.text.Image image147 = iTextSharp.text.Image.GetInstance(checkbox);
                            image147.ScaleAbsolute(10f, 10f);
                            PdfPCell cell147 = new PdfPCell(image147);
                            cell147.HorizontalAlignment = 1;
                            cell147.VerticalAlignment = 5;
                            cell147.FixedHeight = 12f;
                            table9.AddCell(cell147);
                        }
                        else
                        {
                            iTextSharp.text.Image image148 = iTextSharp.text.Image.GetInstance(uncheckbox);
                            image148.ScaleAbsolute(10f, 10f);
                            PdfPCell cell148 = new PdfPCell(image148);
                            cell148.HorizontalAlignment = 1;
                            cell148.VerticalAlignment = 5;
                            cell148.FixedHeight = 12f;
                            table9.AddCell(cell148);
                        }
                        if ((oCHNote.PainLevelToes != null) || (oCHNote.PainLevelToes != ""))
                        {//chairoView.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"]
                            table9.AddCell(new Phrase(" " + oCHNote.PainLevelToes, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
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
                    float[] numArray10 = new float[] { 2f, 6f };
                    PdfPTable table10 = new PdfPTable(numArray10);
                    table.DefaultCell.Border = 15;
                    table5.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table10.DefaultCell.Colspan = 1;
                    table10.DefaultCell.Border = 0;
                    table10.AddCell(new Phrase("Additional Comments : ", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table10.DefaultCell.Border = 2;
                    table10.AddCell(new Phrase(oCHNote.SubjectiveAdditionalComments, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_SUBJECTIVE_ADDITIONAL_COMMENTS"].ToString()
                    table10.DefaultCell.Border = 0;
                    table10.AddCell(new Phrase(""));
                    table10.AddCell(new Phrase("Based on the report of the patient, additional information regarding this date of service may be found in the patients file.", FontFactory.GetFont("Arial", 6f, 1, iTextSharp.text.Color.BLACK)));
                    table3.AddCell(table10);
                    float[] numArray11 = new float[] { 4f, 4f };
                    PdfPTable table11 = new PdfPTable(numArray11);
                    table11.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 0;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table11.DefaultCell.Colspan = 1;
                    table11.DefaultCell.Border = 2;
                    table11.AddCell(new Phrase("Range of motion : ", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table11.AddCell(new Phrase("Pain    R:Restriction", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table11.DefaultCell.Border = 0;
                    table11.DefaultCell.Colspan = 2;
                    table7.DefaultCell.HorizontalAlignment = 0;
                    table11.DefaultCell.VerticalAlignment = 4;
                    float[] numArray12 = new float[] { 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable table12 = new PdfPTable(numArray12);
                    table12.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table12.DefaultCell.Border = 0;
                    table12.AddCell(new Phrase("CREVICAL :", FontFactory.GetFont("Arial", 6f, 1, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_FLEX"]
                        iTextSharp.text.Image image149 = iTextSharp.text.Image.GetInstance(checkbox);
                        image149.ScaleAbsolute(10f, 10f);
                        PdfPCell cell149 = new PdfPCell(image149);
                        cell149.Border = 0;
                        cell149.HorizontalAlignment = 1;
                        cell149.VerticalAlignment = 5;
                        cell149.FixedHeight = 12f;
                        table12.AddCell(cell149);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image150 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image150.ScaleAbsolute(10f, 10f);
                        PdfPCell cell150 = new PdfPCell(image150);
                        cell150.Border = 0;
                        cell150.HorizontalAlignment = 1;
                        cell150.VerticalAlignment = 5;
                        cell150.FixedHeight = 12f;
                        table12.AddCell(cell150);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalExt == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_EXT"].ToString()
                        iTextSharp.text.Image image151 = iTextSharp.text.Image.GetInstance(checkbox);
                        image151.ScaleAbsolute(10f, 10f);
                        PdfPCell cell151 = new PdfPCell(image151);
                        cell151.Border = 0;
                        cell151.HorizontalAlignment = 1;
                        cell151.VerticalAlignment = 5;
                        cell151.FixedHeight = 12f;
                        table12.AddCell(cell151);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image152 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image152.ScaleAbsolute(10f, 10f);
                        PdfPCell cell152 = new PdfPCell(image152);
                        cell152.Border = 0;
                        cell152.HorizontalAlignment = 1;
                        cell152.VerticalAlignment = 5;
                        cell152.FixedHeight = 12f;
                        table12.AddCell(cell152);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("EXT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalRtRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_RT_ROT"].ToString()
                        iTextSharp.text.Image image153 = iTextSharp.text.Image.GetInstance(checkbox);
                        image153.ScaleAbsolute(10f, 10f);
                        PdfPCell cell153 = new PdfPCell(image153);
                        cell153.Border = 0;
                        cell153.HorizontalAlignment = 1;
                        cell153.VerticalAlignment = 5;
                        cell153.FixedHeight = 12f;
                        table12.AddCell(cell153);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image154 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image154.ScaleAbsolute(10f, 10f);
                        PdfPCell cell154 = new PdfPCell(image154);
                        cell154.Border = 0;
                        cell154.HorizontalAlignment = 1;
                        cell154.VerticalAlignment = 5;
                        cell154.FixedHeight = 12f;
                        table12.AddCell(cell154);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalLftRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_LFT_ROT"].ToString()
                        iTextSharp.text.Image image155 = iTextSharp.text.Image.GetInstance(checkbox);
                        image155.ScaleAbsolute(10f, 10f);
                        PdfPCell cell155 = new PdfPCell(image155);
                        cell155.Border = 0;
                        cell155.HorizontalAlignment = 1;
                        cell155.VerticalAlignment = 5;
                        cell155.FixedHeight = 12f;
                        table12.AddCell(cell155);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image156 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image156.ScaleAbsolute(10f, 10f);
                        PdfPCell cell156 = new PdfPCell(image156);
                        cell156.Border = 0;
                        cell156.HorizontalAlignment = 1;
                        cell156.VerticalAlignment = 5;
                        cell156.FixedHeight = 12f;
                        table12.AddCell(cell156);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalRTLATFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_RT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image157 = iTextSharp.text.Image.GetInstance(checkbox);
                        image157.ScaleAbsolute(10f, 10f);
                        PdfPCell cell157 = new PdfPCell(image157);
                        cell157.Border = 0;
                        cell157.HorizontalAlignment = 1;
                        cell157.VerticalAlignment = 5;
                        cell157.FixedHeight = 12f;
                        table12.AddCell(cell157);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image158 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image158.ScaleAbsolute(10f, 10f);
                        PdfPCell cell158 = new PdfPCell(image158);
                        cell158.Border = 0;
                        cell158.HorizontalAlignment = 1;
                        cell158.VerticalAlignment = 5;
                        cell158.FixedHeight = 12f;
                        table12.AddCell(cell158);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.CervicalLftLatFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CERVICAL_LFT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image159 = iTextSharp.text.Image.GetInstance(checkbox);
                        image159.ScaleAbsolute(10f, 10f);
                        PdfPCell cell159 = new PdfPCell(image159);
                        cell159.Border = 0;
                        cell159.HorizontalAlignment = 1;
                        cell159.VerticalAlignment = 5;
                        cell159.FixedHeight = 12f;
                        table12.AddCell(cell159);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image160 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image160.ScaleAbsolute(10f, 10f);
                        PdfPCell cell160 = new PdfPCell(image160);
                        cell160.Border = 0;
                        cell160.HorizontalAlignment = 1;
                        cell160.VerticalAlignment = 5;
                        cell160.FixedHeight = 12f;
                        table12.AddCell(cell160);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    table12.AddCell(new Phrase("THORACIC :", FontFactory.GetFont("Arial", 6f, 1, iTextSharp.text.Color.BLACK)));
                    if ( oCHNote.ThoracicFlex== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_FLEX"].ToString()
                        iTextSharp.text.Image image161 = iTextSharp.text.Image.GetInstance(checkbox);
                        image161.ScaleAbsolute(10f, 10f);
                        PdfPCell cell161 = new PdfPCell(image161);
                        cell161.Border = 0;
                        cell161.HorizontalAlignment = 1;
                        cell161.VerticalAlignment = 5;
                        cell161.FixedHeight = 12f;
                        table12.AddCell(cell161);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image162 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image162.ScaleAbsolute(10f, 10f);
                        PdfPCell cell162 = new PdfPCell(image162);
                        cell162.Border = 0;
                        cell162.HorizontalAlignment = 1;
                        cell162.VerticalAlignment = 5;
                        cell162.FixedHeight = 12f;
                        table12.AddCell(cell162);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicExt == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_EXT"].ToString()
                        iTextSharp.text.Image image163 = iTextSharp.text.Image.GetInstance(checkbox);
                        image163.ScaleAbsolute(10f, 10f);
                        PdfPCell cell163 = new PdfPCell(image163);
                        cell163.Border = 0;
                        cell163.HorizontalAlignment = 1;
                        cell163.VerticalAlignment = 5;
                        cell163.FixedHeight = 12f;
                        table12.AddCell(cell163);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image164 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image164.ScaleAbsolute(10f, 10f);
                        PdfPCell cell164 = new PdfPCell(image164);
                        cell164.Border = 0;
                        cell164.HorizontalAlignment = 1;
                        cell164.VerticalAlignment = 5;
                        cell164.FixedHeight = 12f;
                        table12.AddCell(cell164);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("EXT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicRtRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_RT_ROT"].ToString()
                        iTextSharp.text.Image image165 = iTextSharp.text.Image.GetInstance(checkbox);
                        image165.ScaleAbsolute(10f, 10f);
                        PdfPCell cell165 = new PdfPCell(image165);
                        cell165.Border = 0;
                        cell165.HorizontalAlignment = 1;
                        cell165.VerticalAlignment = 5;
                        cell165.FixedHeight = 12f;
                        table12.AddCell(cell165);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image166 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image166.ScaleAbsolute(10f, 10f);
                        PdfPCell cell166 = new PdfPCell(image166);
                        cell166.Border = 0;
                        cell166.HorizontalAlignment = 1;
                        cell166.VerticalAlignment = 5;
                        cell166.FixedHeight = 12f;
                        table12.AddCell(cell166);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicLftRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_LFT_ROT"].ToString()
                        iTextSharp.text.Image image167 = iTextSharp.text.Image.GetInstance(checkbox);
                        image167.ScaleAbsolute(10f, 10f);
                        PdfPCell cell167 = new PdfPCell(image167);
                        cell167.Border = 0;
                        cell167.HorizontalAlignment = 1;
                        cell167.VerticalAlignment = 5;
                        cell167.FixedHeight = 12f;
                        table12.AddCell(cell167);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image168 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image168.ScaleAbsolute(10f, 10f);
                        PdfPCell cell168 = new PdfPCell(image168);
                        cell168.Border = 0;
                        cell168.HorizontalAlignment = 1;
                        cell168.VerticalAlignment = 5;
                        cell168.FixedHeight = 12f;
                        table12.AddCell(cell168);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicRtLatFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_RT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image169 = iTextSharp.text.Image.GetInstance(checkbox);
                        image169.ScaleAbsolute(10f, 10f);
                        PdfPCell cell169 = new PdfPCell(image169);
                        cell169.Border = 0;
                        cell169.HorizontalAlignment = 1;
                        cell169.VerticalAlignment = 5;
                        cell169.FixedHeight = 12f;
                        table12.AddCell(cell169);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image170 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image170.ScaleAbsolute(10f, 10f);
                        PdfPCell cell170 = new PdfPCell(image170);
                        cell170.Border = 0;
                        cell170.HorizontalAlignment = 1;
                        cell170.VerticalAlignment = 5;
                        cell170.FixedHeight = 12f;
                        table12.AddCell(cell170);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ThoracicLftLatFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THORACIC_LFT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image171 = iTextSharp.text.Image.GetInstance(checkbox);
                        image171.ScaleAbsolute(10f, 10f);
                        PdfPCell cell171 = new PdfPCell(image171);
                        cell171.Border = 0;
                        cell171.HorizontalAlignment = 1;
                        cell171.VerticalAlignment = 5;
                        cell171.FixedHeight = 12f;
                        table12.AddCell(cell171);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image172 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image172.ScaleAbsolute(10f, 10f);
                        PdfPCell cell172 = new PdfPCell(image172);
                        cell172.Border = 0;
                        cell172.HorizontalAlignment = 1;
                        cell172.VerticalAlignment = 5;
                        cell172.FixedHeight = 12f;
                        table12.AddCell(cell172);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    table12.AddCell(new Phrase("LUMBAR :", FontFactory.GetFont("Arial", 6f, 1, iTextSharp.text.Color.BLACK)));
                    if ( oCHNote.LumbarFlex== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_FLEX"].ToString()
                        iTextSharp.text.Image image173 = iTextSharp.text.Image.GetInstance(checkbox);
                        image173.ScaleAbsolute(10f, 10f);
                        PdfPCell cell173 = new PdfPCell(image173);
                        cell173.Border = 0;
                        cell173.HorizontalAlignment = 1;
                        cell173.VerticalAlignment = 5;
                        cell173.FixedHeight = 12f;
                        table12.AddCell(cell173);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image174 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image174.ScaleAbsolute(10f, 10f);
                        PdfPCell cell174 = new PdfPCell(image174);
                        cell174.Border = 0;
                        cell174.HorizontalAlignment = 1;
                        cell174.VerticalAlignment = 5;
                        cell174.FixedHeight = 12f;
                        table12.AddCell(cell174);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LumbarExt == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_EXT"].ToString()
                        iTextSharp.text.Image image175 = iTextSharp.text.Image.GetInstance(checkbox);
                        image175.ScaleAbsolute(10f, 10f);
                        PdfPCell cell175 = new PdfPCell(image175);
                        cell175.Border = 0;
                        cell175.HorizontalAlignment = 1;
                        cell175.VerticalAlignment = 5;
                        cell175.FixedHeight = 12f;
                        table12.AddCell(cell175);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image176 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image176.ScaleAbsolute(10f, 10f);
                        PdfPCell cell176 = new PdfPCell(image176);
                        cell176.Border = 0;
                        cell176.HorizontalAlignment = 1;
                        cell176.VerticalAlignment = 5;
                        cell176.FixedHeight = 12f;
                        table12.AddCell(cell176);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("EXT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LumbarRtRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_RT_ROT"].ToString()
                        iTextSharp.text.Image image177 = iTextSharp.text.Image.GetInstance(checkbox);
                        image177.ScaleAbsolute(10f, 10f);
                        PdfPCell cell177 = new PdfPCell(image177);
                        cell177.Border = 0;
                        cell177.HorizontalAlignment = 1;
                        cell177.VerticalAlignment = 5;
                        cell177.FixedHeight = 12f;
                        table12.AddCell(cell177);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image178 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image178.ScaleAbsolute(10f, 10f);
                        PdfPCell cell178 = new PdfPCell(image178);
                        cell178.Border = 0;
                        cell178.HorizontalAlignment = 1;
                        cell178.VerticalAlignment = 5;
                        cell178.FixedHeight = 12f;
                        table12.AddCell(cell178);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LumbarLftRot == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_LFT_ROT"].ToString()
                        iTextSharp.text.Image image179 = iTextSharp.text.Image.GetInstance(checkbox);
                        image179.ScaleAbsolute(10f, 10f);
                        PdfPCell cell179 = new PdfPCell(image179);
                        cell179.Border = 0;
                        cell179.HorizontalAlignment = 1;
                        cell179.VerticalAlignment = 5;
                        cell179.FixedHeight = 12f;
                        table12.AddCell(cell179);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image180 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image180.ScaleAbsolute(10f, 10f);
                        PdfPCell cell180 = new PdfPCell(image180);
                        cell180.Border = 0;
                        cell180.HorizontalAlignment = 1;
                        cell180.VerticalAlignment = 5;
                        cell180.FixedHeight = 12f;
                        table12.AddCell(cell180);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.ROT", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LumbarRtLatFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_RT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image181 = iTextSharp.text.Image.GetInstance(checkbox);
                        image181.ScaleAbsolute(10f, 10f);
                        PdfPCell cell181 = new PdfPCell(image181);
                        cell181.Border = 0;
                        cell181.HorizontalAlignment = 1;
                        cell181.VerticalAlignment = 5;
                        cell181.FixedHeight = 12f;
                        table12.AddCell(cell181);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image182 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image182.ScaleAbsolute(10f, 10f);
                        PdfPCell cell182 = new PdfPCell(image182);
                        cell182.Border = 0;
                        cell182.HorizontalAlignment = 1;
                        cell182.VerticalAlignment = 5;
                        cell182.FixedHeight = 12f;
                        table12.AddCell(cell182);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("RT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.LumbarLftLatFlex == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LUMBAR_LFT_LAT_FLEX"].ToString()
                        iTextSharp.text.Image image183 = iTextSharp.text.Image.GetInstance(checkbox);
                        image183.ScaleAbsolute(10f, 10f);
                        PdfPCell cell183 = new PdfPCell(image183);
                        cell183.Border = 0;
                        cell183.HorizontalAlignment = 1;
                        cell183.VerticalAlignment = 5;
                        cell183.FixedHeight = 12f;
                        table12.AddCell(cell183);
                        table12.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image184 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image184.ScaleAbsolute(10f, 10f);
                        PdfPCell cell184 = new PdfPCell(image184);
                        cell184.Border = 0;
                        cell184.HorizontalAlignment = 1;
                        cell184.VerticalAlignment = 5;
                        cell184.FixedHeight = 12f;
                        table12.AddCell(cell184);
                        table12.DefaultCell.Border = 0;
                    }
                    table12.AddCell(new Phrase("LFT.LAT.FLEX", FontFactory.GetFont("Arial", 6f, iTextSharp.text.Color.BLACK)));
                    table11.AddCell(table12);
                    table11.AddCell(new Phrase(""));
                    table3.AddCell(table11);
                    float[] numArray13 = new float[] { 2f, 6f };
                    PdfPTable table13 = new PdfPTable(numArray13);
                    table.DefaultCell.Border = 15;
                    table13.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table13.DefaultCell.Colspan = 1;
                    table13.DefaultCell.Border = 0;
                    table13.AddCell(new Phrase("Additional Comments : ", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table13.DefaultCell.Border = 2;
                    table13.AddCell(new Phrase(oCHNote.ObjectiveAdditionalComments, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_OBJECTIVE_ADDITIONAL_COMMENTS"].ToString()
                    table13.DefaultCell.Border = 0;
                    table13.AddCell(new Phrase(""));
                    table13.AddCell(new Phrase("Based on the report of the patient, additional information regarding this date of service may be found in the patients file.", FontFactory.GetFont("Arial", 6f, 1, iTextSharp.text.Color.BLACK)));
                    table3.AddCell(table13);
                    float[] numArray14 = new float[] { 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable table14 = new PdfPTable(numArray14);
                    table.DefaultCell.Border = 15;
                    table14.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table14.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table14.DefaultCell.Colspan = 6;
                    table14.AddCell(new Phrase("Assessment : ", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table14.DefaultCell.Colspan = 1;
                    table14.DefaultCell.Border = 0;
                    if ( oCHNote.AssessmentNoChange== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ASSESSMENT_NO_CHANGE"].ToString()
                        iTextSharp.text.Image image185 = iTextSharp.text.Image.GetInstance(checkbox);
                        image185.ScaleAbsolute(10f, 10f);
                        PdfPCell cell185 = new PdfPCell(image185);
                        cell185.Border = 0;
                        cell185.HorizontalAlignment = 2;
                        cell185.VerticalAlignment = 6;
                        cell185.FixedHeight = 12f;
                        table14.AddCell(cell185);
                        table14.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image186 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image186.ScaleAbsolute(10f, 10f);
                        PdfPCell cell186 = new PdfPCell(image186);
                        cell186.Border = 0;
                        cell186.HorizontalAlignment = 2;
                        cell186.VerticalAlignment = 6;
                        cell186.FixedHeight = 12f;
                        table14.AddCell(cell186);
                        table14.DefaultCell.Border = 0;
                    }
                    table14.AddCell(new Phrase("No Change", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.AssessmentImproving == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ASSESSMENT_IMPROVING"].ToString()
                        iTextSharp.text.Image image187 = iTextSharp.text.Image.GetInstance(checkbox);
                        image187.ScaleAbsolute(10f, 10f);
                        PdfPCell cell187 = new PdfPCell(image187);
                        cell187.Border = 0;
                        cell187.HorizontalAlignment = 2;
                        cell187.VerticalAlignment = 6;
                        cell187.FixedHeight = 12f;
                        table14.AddCell(cell187);
                        table14.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image188 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image188.ScaleAbsolute(10f, 10f);
                        PdfPCell cell188 = new PdfPCell(image188);
                        cell188.Border = 0;
                        cell188.HorizontalAlignment = 2;
                        cell188.VerticalAlignment = 6;
                        cell188.FixedHeight = 12f;
                        table14.AddCell(cell188);
                        table14.DefaultCell.Border = 0;
                    }
                    table14.AddCell(new Phrase("Improving", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.AssessmentFlairUp == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ASSESSMENT_FLAIR_UP"].ToString()
                        iTextSharp.text.Image image189 = iTextSharp.text.Image.GetInstance(checkbox);
                        image189.ScaleAbsolute(10f, 10f);
                        PdfPCell cell189 = new PdfPCell(image189);
                        cell189.Border = 0;
                        cell189.HorizontalAlignment = 2;
                        cell189.VerticalAlignment = 6;
                        cell189.FixedHeight = 12f;
                        table14.AddCell(cell189);
                        table14.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image190 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image190.ScaleAbsolute(10f, 10f);
                        PdfPCell cell190 = new PdfPCell(image190);
                        cell190.Border = 0;
                        cell190.HorizontalAlignment = 2;
                        cell190.VerticalAlignment = 6;
                        cell190.FixedHeight = 12f;
                        table14.AddCell(cell190);
                        table14.DefaultCell.Border = 0;
                    }
                    table14.AddCell(new Phrase("Flair up", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.AssessmentAsExpected== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ASSESSMENT_AS_EXPECTED"].ToString() 
                        iTextSharp.text.Image image191 = iTextSharp.text.Image.GetInstance(checkbox);
                        image191.ScaleAbsolute(10f, 10f);
                        PdfPCell cell191 = new PdfPCell(image191);
                        cell191.Border = 0;
                        cell191.HorizontalAlignment = 2;
                        cell191.VerticalAlignment = 6;
                        cell191.FixedHeight = 12f;
                        table14.AddCell(cell191);
                        table14.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image192 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image192.ScaleAbsolute(10f, 10f);
                        PdfPCell cell192 = new PdfPCell(image192);
                        cell192.Border = 0;
                        cell192.HorizontalAlignment = 2;
                        cell192.VerticalAlignment = 6;
                        cell192.FixedHeight = 12f;
                        table14.AddCell(cell192);
                        table14.DefaultCell.Border = 0;
                    }
                    table14.AddCell(new Phrase("As Expected", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.AssessmentSlowerThanExpected == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ASSESSMENT_SLOWER_THAN_EXPECTED"].ToString()
                        iTextSharp.text.Image image193 = iTextSharp.text.Image.GetInstance(checkbox);
                        image193.ScaleAbsolute(10f, 10f);
                        PdfPCell cell193 = new PdfPCell(image193);
                        cell193.Border = 0;
                        cell193.HorizontalAlignment = 2;
                        cell193.VerticalAlignment = 6;
                        cell193.FixedHeight = 12f;
                        table14.AddCell(cell193);
                        table14.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image194 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image194.ScaleAbsolute(10f, 10f);
                        PdfPCell cell194 = new PdfPCell(image194);
                        cell194.Border = 0;
                        cell194.HorizontalAlignment = 2;
                        cell194.VerticalAlignment = 6;
                        cell194.FixedHeight = 12f;
                        table14.AddCell(cell194);
                        table14.DefaultCell.Border = 0;
                    }
                    table14.AddCell(new Phrase("Slower than Expected", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table14.AddCell(new Phrase(""));
                    table14.AddCell(new Phrase(""));
                    table3.AddCell(table14);
                    float[] numArray15 = new float[] { 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable table15 = new PdfPTable(numArray15);
                    table.DefaultCell.Border = 15;
                    table15.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table15.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table15.DefaultCell.Colspan = 8;
                    table15.AddCell(new Phrase("Due to nature of patient's condition they should", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table15.DefaultCell.Colspan = 1;
                    table15.DefaultCell.Border = 0;
                    if (oCHNote.StopAllActivities == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_STOP_ALL_ACTIVITES"].ToString()
                        iTextSharp.text.Image image195 = iTextSharp.text.Image.GetInstance(checkbox);
                        image195.ScaleAbsolute(10f, 10f);
                        PdfPCell cell195 = new PdfPCell(image195);
                        cell195.Border = 0;
                        cell195.HorizontalAlignment = 2;
                        cell195.VerticalAlignment = 6;
                        cell195.FixedHeight = 12f;
                        table15.AddCell(cell195);
                        table15.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image196 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image196.ScaleAbsolute(10f, 10f);
                        PdfPCell cell196 = new PdfPCell(image196);
                        cell196.Border = 0;
                        cell196.HorizontalAlignment = 2;
                        cell196.VerticalAlignment = 6;
                        cell196.FixedHeight = 12f;
                        table15.AddCell(cell196);
                        table15.DefaultCell.Border = 0;
                    }
                    table15.AddCell(new Phrase("Stop all Activities", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if ( oCHNote.ReduceAllActivities== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_REDUCE_ALL_ACTIVITES"].ToString()
                        iTextSharp.text.Image image197 = iTextSharp.text.Image.GetInstance(checkbox);
                        image197.ScaleAbsolute(10f, 10f);
                        PdfPCell cell197 = new PdfPCell(image197);
                        cell197.Border = 0;
                        cell197.HorizontalAlignment = 2;
                        cell197.VerticalAlignment = 6;
                        cell197.FixedHeight = 12f;
                        table15.AddCell(cell197);
                        table15.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image198 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image198.ScaleAbsolute(10f, 10f);
                        PdfPCell cell198 = new PdfPCell(image198);
                        cell198.Border = 0;
                        cell198.HorizontalAlignment = 2;
                        cell198.VerticalAlignment = 6;
                        cell198.FixedHeight = 12f;
                        table15.AddCell(cell198);
                        table15.DefaultCell.Border = 0;
                    }
                    table15.AddCell(new Phrase("Reduce all Activities", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ResumeLightActivities == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_RESUME_LIGHT_ACTIVITES"].ToString()
                        iTextSharp.text.Image image199 = iTextSharp.text.Image.GetInstance(checkbox);
                        image199.ScaleAbsolute(10f, 10f);
                        PdfPCell cell199 = new PdfPCell(image199);
                        cell199.Border = 0;
                        cell199.HorizontalAlignment = 2;
                        cell199.VerticalAlignment = 6;
                        cell199.FixedHeight = 12f;
                        table15.AddCell(cell199);
                        table15.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image200 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image200.ScaleAbsolute(10f, 10f);
                        PdfPCell cell200 = new PdfPCell(image200);
                        cell200.Border = 0;
                        cell200.HorizontalAlignment = 2;
                        cell200.VerticalAlignment = 6;
                        cell200.FixedHeight = 12f;
                        table15.AddCell(cell200);
                        table15.DefaultCell.Border = 0;
                    }
                    table15.AddCell(new Phrase("Resume Light Activities", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.ResumeAllActivities == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_RESUME_ALL_ACTIVITES"].ToString()
                        iTextSharp.text.Image image201 = iTextSharp.text.Image.GetInstance(checkbox);
                        image201.ScaleAbsolute(10f, 10f);
                        PdfPCell cell201 = new PdfPCell(image201);
                        cell201.Border = 0;
                        cell201.HorizontalAlignment = 2;
                        cell201.VerticalAlignment = 6;
                        cell201.FixedHeight = 12f;
                        table15.AddCell(cell201);
                        table15.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image202 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image202.ScaleAbsolute(10f, 10f);
                        PdfPCell cell202 = new PdfPCell(image202);
                        cell202.Border = 0;
                        cell202.HorizontalAlignment = 2;
                        cell202.VerticalAlignment = 6;
                        cell202.FixedHeight = 12f;
                        table15.AddCell(cell202);
                        table15.DefaultCell.Border = 0;
                    }
                    table15.AddCell(new Phrase("Resume All Activities", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table3.AddCell(table15);
                    #region treatment
                    float[] numArray16 = new float[] { 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable table16 = new PdfPTable(numArray16);
                    table.DefaultCell.Border = 15;
                    table16.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table16.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table16.DefaultCell.Colspan = 6;
                    table16.AddCell(new Phrase("Treatment : All treatment is being rendered based on subjective complaints and examination finding and is medically necessary.", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table16.DefaultCell.Colspan = 1;
                    if (oCHNote.TreatmentCervical == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_CERVICAL"].ToString()
                        iTextSharp.text.Image image203 = iTextSharp.text.Image.GetInstance(checkbox);
                        image203.ScaleAbsolute(10f, 10f);
                        PdfPCell cell203 = new PdfPCell(image203);
                        cell203.Border = 0;
                        cell203.HorizontalAlignment = 2;
                        cell203.VerticalAlignment = 6;
                        cell203.FixedHeight = 12f;
                        table16.AddCell(cell203);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image204 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image204.ScaleAbsolute(10f, 10f);
                        PdfPCell cell204 = new PdfPCell(image204);
                        cell204.Border = 0;
                        cell204.HorizontalAlignment = 2;
                        cell204.VerticalAlignment = 6;
                        cell204.FixedHeight = 12f;
                        table16.AddCell(cell204);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("C:Cervical", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentThoracic == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_THORACIC"].ToString()
                        iTextSharp.text.Image image205 = iTextSharp.text.Image.GetInstance(checkbox);
                        image205.ScaleAbsolute(10f, 10f);
                        PdfPCell cell205 = new PdfPCell(image205);
                        cell205.Border = 0;
                        cell205.HorizontalAlignment = 2;
                        cell205.VerticalAlignment = 6;
                        cell205.FixedHeight = 12f;
                        table16.AddCell(cell205);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image206 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image206.ScaleAbsolute(10f, 10f);
                        PdfPCell cell206 = new PdfPCell(image206);
                        cell206.Border = 0;
                        cell206.HorizontalAlignment = 2;
                        cell206.VerticalAlignment = 6;
                        cell206.FixedHeight = 12f;
                        table16.AddCell(cell206);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("T:Throracic", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentLumbar == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_LUMBAR"].ToString()
                        iTextSharp.text.Image image207 = iTextSharp.text.Image.GetInstance(checkbox);
                        image207.ScaleAbsolute(10f, 10f);
                        PdfPCell cell207 = new PdfPCell(image207);
                        cell207.Border = 0;
                        cell207.HorizontalAlignment = 2;
                        cell207.VerticalAlignment = 6;
                        cell207.FixedHeight = 12f;
                        table16.AddCell(cell207);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image208 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image208.ScaleAbsolute(10f, 10f);
                        PdfPCell cell208 = new PdfPCell(image208);
                        cell208.Border = 0;
                        cell208.HorizontalAlignment = 2;
                        cell208.VerticalAlignment = 6;
                        cell208.FixedHeight = 12f;
                        table16.AddCell(cell208);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("L:Lumber", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentDorsoLumbar == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_DORSOLUMBAR"].ToString()
                        iTextSharp.text.Image image209 = iTextSharp.text.Image.GetInstance(checkbox);
                        image209.ScaleAbsolute(10f, 10f);
                        PdfPCell cell209 = new PdfPCell(image209);
                        cell209.Border = 0;
                        cell209.HorizontalAlignment = 2;
                        cell209.VerticalAlignment = 6;
                        cell209.FixedHeight = 12f;
                        table16.AddCell(cell209);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image210 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image210.ScaleAbsolute(10f, 10f);
                        PdfPCell cell210 = new PdfPCell(image210);
                        cell210.Border = 0;
                        cell210.HorizontalAlignment = 2;
                        cell210.VerticalAlignment = 6;
                        cell210.FixedHeight = 12f;
                        table16.AddCell(cell210);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("D/L:DorsoLumbar", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if ( oCHNote.TreatmentSacroiliac== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_SACROILIAC"].ToString()
                        iTextSharp.text.Image image211 = iTextSharp.text.Image.GetInstance(checkbox);
                        image211.ScaleAbsolute(10f, 10f);
                        PdfPCell cell211 = new PdfPCell(image211);
                        cell211.Border = 0;
                        cell211.HorizontalAlignment = 2;
                        cell211.VerticalAlignment = 6;
                        cell211.FixedHeight = 12f;
                        table16.AddCell(cell211);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image212 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image212.ScaleAbsolute(10f, 10f);
                        PdfPCell cell212 = new PdfPCell(image212);
                        cell212.Border = 0;
                        cell212.HorizontalAlignment = 2;
                        cell212.VerticalAlignment = 6;
                        cell212.FixedHeight = 12f;
                        table16.AddCell(cell212);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("SI:Sacroiliac", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentTempromandibularJoint == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_TEMPROMANDIBULAR_JOINT"].ToString()
                        iTextSharp.text.Image image213 = iTextSharp.text.Image.GetInstance(checkbox);
                        image213.ScaleAbsolute(10f, 10f);
                        PdfPCell cell213 = new PdfPCell(image213);
                        cell213.Border = 0;
                        cell213.HorizontalAlignment = 2;
                        cell213.VerticalAlignment = 6;
                        cell213.FixedHeight = 12f;
                        table16.AddCell(cell213);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image214 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image214.ScaleAbsolute(10f, 10f);
                        PdfPCell cell214 = new PdfPCell(image214);
                        cell214.Border = 0;
                        cell214.HorizontalAlignment = 2;
                        cell214.VerticalAlignment = 6;
                        cell214.FixedHeight = 12f;
                        table16.AddCell(cell214);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("TJ:Tempromandibular Joint", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentCervicoThoracic == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_CERVICOTHORACIC"].ToString()
                        iTextSharp.text.Image image215 = iTextSharp.text.Image.GetInstance(checkbox);
                        image215.ScaleAbsolute(10f, 10f);
                        PdfPCell cell215 = new PdfPCell(image215);
                        cell215.Border = 0;
                        cell215.HorizontalAlignment = 2;
                        cell215.VerticalAlignment = 6;
                        cell215.FixedHeight = 12f;
                        table16.AddCell(cell215);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image216 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image216.ScaleAbsolute(10f, 10f);
                        PdfPCell cell216 = new PdfPCell(image216);
                        cell216.Border = 0;
                        cell216.HorizontalAlignment = 2;
                        cell216.VerticalAlignment = 6;
                        cell216.FixedHeight = 12f;
                        table16.AddCell(cell216);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("Cervicothoracic", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    if (oCHNote.TreatmentLumboPelvic== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TREATMENT_LUMBOPELVIC"].ToString() 
                        iTextSharp.text.Image image217 = iTextSharp.text.Image.GetInstance(checkbox);
                        image217.ScaleAbsolute(10f, 10f);
                        PdfPCell cell217 = new PdfPCell(image217);
                        cell217.Border = 0;
                        cell217.HorizontalAlignment = 2;
                        cell217.VerticalAlignment = 6;
                        cell217.FixedHeight = 12f;
                        table16.AddCell(cell217);
                        table16.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image218 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image218.ScaleAbsolute(10f, 10f);
                        PdfPCell cell218 = new PdfPCell(image218);
                        cell218.Border = 0;
                        cell218.HorizontalAlignment = 2;
                        cell218.VerticalAlignment = 6;
                        cell218.FixedHeight = 12f;
                        table16.AddCell(cell218);
                        table16.DefaultCell.Border = 0;
                    }
                    table16.AddCell(new Phrase("Lumbopelvic", FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                    table16.AddCell(new Phrase(""));
                    table16.AddCell(new Phrase(""));
                    table3.AddCell(table16);
                    #endregion treatment
                    //treatment
                    #region Treatment Plan

                    PdfPTable treatPlanTable1 = new PdfPTable(1);
                    table.DefaultCell.Border = 15;
                    treatPlanTable1.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    treatPlanTable1.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    treatPlanTable1.DefaultCell.Colspan = 6;
                    //treatPlanTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                    //treatPlanTable1.TotalWidth = 520f;
                    //treatPlanTable1.LockedWidth = true;
                    treatPlanTable1.AddCell(new Phrase("TREATMENT PLAN", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    treatPlanTable1.DefaultCell.Colspan = 1;

                    //Chunk chtreatPlanHeader = new Chunk("TREATMENT PLAN", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK));

                    //PdfPCell hdtreatPlanCell = new PdfPCell(new Phrase(chtreatPlanHeader));
                    //hdtreatPlanCell.FixedHeight = 13f;
                    //hdtreatPlanCell.HorizontalAlignment = 0;
                    //hdtreatPlanCell.Border = Rectangle.TOP_BORDER;

                    PdfPCell ptCell1 = new PdfPCell(new Phrase("1. Based upon presenting symptoms, objective findings and clinical assessment, care consisted of the following procedures:", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    ptCell1.FixedHeight = 12f;
                    ptCell1.HorizontalAlignment = 0;
                    ptCell1.Border = 0;

                    #region nesTable1
                    PdfPTable nesTable1 = new PdfPTable(9);
                    nesTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable1.TotalWidth = 520f;

                    float[] widths9 = new float[] { 13f, 140f, 13f, 40f, 13f, 40f, 13f, 40f, 208f };
                    nesTable1.SetWidths(widths9);
                    nesTable1.LockedWidth = true;

                    PdfPCell chiroAdjCell = new PdfPCell(new Phrase("Chiropractic Adjustment (manipulation):", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    chiroAdjCell.FixedHeight = 12f;
                    chiroAdjCell.HorizontalAlignment = 0;
                    chiroAdjCell.Border = 0;

                    PdfPCell cmt12Cell = new PdfPCell(new Phrase("CMT 1-2,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    cmt12Cell.FixedHeight = 12f;
                    cmt12Cell.HorizontalAlignment = 0;
                    cmt12Cell.Border = 0;

                    PdfPCell cmt34Cell = new PdfPCell(new Phrase("CMT 3-4,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    cmt34Cell.FixedHeight = 12f;
                    cmt34Cell.HorizontalAlignment = 0;
                    cmt34Cell.Border = 0;

                    PdfPCell extremityCell1 = new PdfPCell(new Phrase("Extremity", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    extremityCell1.FixedHeight = 12f;
                    extremityCell1.HorizontalAlignment = 0;
                    extremityCell1.Border = 0;

                    PdfPCell extremityCell2 = new PdfPCell(new Phrase(oCHNote.Extremity1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_EXTREMITY"].ToString()
                    extremityCell2.FixedHeight = 12f;
                    extremityCell2.HorizontalAlignment = 0;
                    extremityCell2.Border = 0;

                    if ( oCHNote.ChiroPracticAdj== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CHIROPRACTIC_ADJ"].ToString()
                        iTextSharp.text.Image image303 = iTextSharp.text.Image.GetInstance(checkbox);
                        image303.ScaleAbsolute(10f, 10f);
                        PdfPCell cell303 = new PdfPCell(image303);
                        cell303.Border = 0;
                        cell303.HorizontalAlignment = 2;
                        cell303.VerticalAlignment = 6;
                        cell303.FixedHeight = 12f;
                        nesTable1.AddCell(cell303);
                        nesTable1.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image304 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image304.ScaleAbsolute(10f, 10f);
                        PdfPCell cell304 = new PdfPCell(image304);
                        cell304.Border = 0;
                        cell304.HorizontalAlignment = 2;
                        cell304.VerticalAlignment = 6;
                        cell304.FixedHeight = 12f;
                        nesTable1.AddCell(cell304);
                        nesTable1.DefaultCell.Border = 0;
                    }

                    nesTable1.AddCell(chiroAdjCell);

                    if ( oCHNote.Cmt12== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CMT_12"].ToString()
                        iTextSharp.text.Image image305 = iTextSharp.text.Image.GetInstance(checkbox);
                        image305.ScaleAbsolute(10f, 10f);
                        PdfPCell cell305 = new PdfPCell(image305);
                        cell305.Border = 0;
                        cell305.HorizontalAlignment = 2;
                        cell305.VerticalAlignment = 6;
                        cell305.FixedHeight = 12f;
                        nesTable1.AddCell(cell305);
                        nesTable1.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image306 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image306.ScaleAbsolute(10f, 10f);
                        PdfPCell cell306 = new PdfPCell(image306);
                        cell306.Border = 0;
                        cell306.HorizontalAlignment = 2;
                        cell306.VerticalAlignment = 6;
                        cell306.FixedHeight = 12f;
                        nesTable1.AddCell(cell306);
                        nesTable1.DefaultCell.Border = 0;
                    }

                    nesTable1.AddCell(cmt12Cell);

                    if ( oCHNote.Cmt34== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CMT_34"].ToString()
                        iTextSharp.text.Image image307 = iTextSharp.text.Image.GetInstance(checkbox);
                        image307.ScaleAbsolute(10f, 10f);
                        PdfPCell cell307 = new PdfPCell(image307);
                        cell307.Border = 0;
                        cell307.HorizontalAlignment = 2;
                        cell307.VerticalAlignment = 6;
                        cell307.FixedHeight = 12f;
                        nesTable1.AddCell(cell307);
                        nesTable1.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image308 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image308.ScaleAbsolute(10f, 10f);
                        PdfPCell cell308 = new PdfPCell(image308);
                        cell308.Border = 0;
                        cell308.HorizontalAlignment = 2;
                        cell308.VerticalAlignment = 6;
                        cell308.FixedHeight = 12f;
                        nesTable1.AddCell(cell308);
                        nesTable1.DefaultCell.Border = 0;
                    }

                    nesTable1.AddCell(cmt34Cell);

                    if (oCHNote.Extremity== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_EXTREMITY"].ToString() 
                        iTextSharp.text.Image image309 = iTextSharp.text.Image.GetInstance(checkbox);
                        image309.ScaleAbsolute(10f, 10f);
                        PdfPCell cell309 = new PdfPCell(image309);
                        cell309.Border = 0;
                        cell309.HorizontalAlignment = 2;
                        cell309.VerticalAlignment = 6;
                        cell309.FixedHeight = 12f;
                        nesTable1.AddCell(cell309);
                        nesTable1.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image310 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image310.ScaleAbsolute(10f, 10f);
                        PdfPCell cell310 = new PdfPCell(image310);
                        cell310.Border = 0;
                        cell310.HorizontalAlignment = 2;
                        cell310.VerticalAlignment = 6;
                        cell310.FixedHeight = 12f;
                        nesTable1.AddCell(cell310);
                        nesTable1.DefaultCell.Border = 0;
                    }

                    nesTable1.AddCell(extremityCell1);
                    nesTable1.AddCell(extremityCell2);

                    #endregion

                    PdfPCell nesCell1 = new PdfPCell(nesTable1);
                    nesCell1.FixedHeight = 12f;
                    nesCell1.HorizontalAlignment = 0;
                    nesCell1.Border = 0;

                    #region nesTable2

                    PdfPTable nesTable2 = new PdfPTable(10);
                    nesTable2.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable2.TotalWidth = 520f;

                    float[] widths10 = new float[] { 13f, 90f, 13f, 70f, 13f, 70f, 13f, 40f, 13f, 185f };
                    nesTable2.SetWidths(widths10);
                    nesTable2.LockedWidth = true;

                    PdfPCell therModaltiesCell = new PdfPCell(new Phrase("Therapeutics Modalities:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    therModaltiesCell.FixedHeight = 12f;
                    therModaltiesCell.HorizontalAlignment = 0;
                    therModaltiesCell.Border = 0;

                    PdfPCell myoReleaseCell = new PdfPCell(new Phrase("Myofascial Release,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    myoReleaseCell.FixedHeight = 12f;
                    myoReleaseCell.HorizontalAlignment = 0;
                    myoReleaseCell.Border = 0;

                    PdfPCell mechTractionCell = new PdfPCell(new Phrase("Mechanical Traction,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    mechTractionCell.FixedHeight = 12f;
                    mechTractionCell.HorizontalAlignment = 0;
                    mechTractionCell.Border = 0;

                    PdfPCell emsIFCell = new PdfPCell(new Phrase("EMS/IF,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    emsIFCell.FixedHeight = 12f;
                    emsIFCell.HorizontalAlignment = 0;
                    emsIFCell.Border = 0;

                    PdfPCell hotColdCell = new PdfPCell(new Phrase("Hot/Cold Therapy", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    hotColdCell.FixedHeight = 12f;
                    hotColdCell.HorizontalAlignment = 0;
                    hotColdCell.Border = 0;

                    if (oCHNote.TherapeuticsModalities == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_THERAPEUTICS_MODALITIES"].ToString()
                        iTextSharp.text.Image image311 = iTextSharp.text.Image.GetInstance(checkbox);
                        image311.ScaleAbsolute(10f, 10f);
                        PdfPCell cell311 = new PdfPCell(image311);
                        cell311.Border = 0;
                        cell311.HorizontalAlignment = 2;
                        cell311.VerticalAlignment = 6;
                        cell311.FixedHeight = 12f;
                        nesTable2.AddCell(cell311);
                        nesTable2.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image312 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image312.ScaleAbsolute(10f, 10f);
                        PdfPCell cell312 = new PdfPCell(image312);
                        cell312.Border = 0;
                        cell312.HorizontalAlignment = 2;
                        cell312.VerticalAlignment = 6;
                        cell312.FixedHeight = 12f;
                        nesTable2.AddCell(cell312);
                        nesTable2.DefaultCell.Border = 0;
                    }

                    nesTable2.AddCell(therModaltiesCell);

                    if ( oCHNote.MyOFascialRelease== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_MYOFASCIAL_RELEASE"].ToString()
                        iTextSharp.text.Image image313 = iTextSharp.text.Image.GetInstance(checkbox);
                        image313.ScaleAbsolute(10f, 10f);
                        PdfPCell cell313 = new PdfPCell(image313);
                        cell313.Border = 0;
                        cell313.HorizontalAlignment = 2;
                        cell313.VerticalAlignment = 6;
                        cell313.FixedHeight = 12f;
                        nesTable2.AddCell(cell313);
                        nesTable2.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image314 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image314.ScaleAbsolute(10f, 10f);
                        PdfPCell cell314 = new PdfPCell(image314);
                        cell314.Border = 0;
                        cell314.HorizontalAlignment = 2;
                        cell314.VerticalAlignment = 6;
                        cell314.FixedHeight = 12f;
                        nesTable2.AddCell(cell314);
                        nesTable2.DefaultCell.Border = 0;
                    }

                    nesTable2.AddCell(myoReleaseCell);

                    if ( oCHNote.MechanicalTRaction== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_MECHANICAL_TRACTION"].ToString()
                        iTextSharp.text.Image image315 = iTextSharp.text.Image.GetInstance(checkbox);
                        image315.ScaleAbsolute(10f, 10f);
                        PdfPCell cell315 = new PdfPCell(image315);
                        cell315.Border = 0;
                        cell315.HorizontalAlignment = 2;
                        cell315.VerticalAlignment = 6;
                        cell315.FixedHeight = 12f;
                        nesTable2.AddCell(cell315);
                        nesTable2.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image316 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image316.ScaleAbsolute(10f, 10f);
                        PdfPCell cell316 = new PdfPCell(image316);
                        cell316.Border = 0;
                        cell316.HorizontalAlignment = 2;
                        cell316.VerticalAlignment = 6;
                        cell316.FixedHeight = 12f;
                        nesTable2.AddCell(cell316);
                        nesTable2.DefaultCell.Border = 0;
                    }

                    nesTable2.AddCell(mechTractionCell);

                    if ( oCHNote.EmsIf== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_EMS_IF"].ToString()
                        iTextSharp.text.Image image317 = iTextSharp.text.Image.GetInstance(checkbox);
                        image317.ScaleAbsolute(10f, 10f);
                        PdfPCell cell317 = new PdfPCell(image317);
                        cell317.Border = 0;
                        cell317.HorizontalAlignment = 2;
                        cell317.VerticalAlignment = 6;
                        cell317.FixedHeight = 12f;
                        nesTable2.AddCell(cell317);
                        nesTable2.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image318 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image318.ScaleAbsolute(10f, 10f);
                        PdfPCell cell318 = new PdfPCell(image318);
                        cell318.Border = 0;
                        cell318.HorizontalAlignment = 2;
                        cell318.VerticalAlignment = 6;
                        cell318.FixedHeight = 12f;
                        nesTable2.AddCell(cell318);
                        nesTable2.DefaultCell.Border = 0;
                    }

                    nesTable2.AddCell(emsIFCell);

                    if (oCHNote.Hotcold == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_HOT_COLD"].ToString()
                        iTextSharp.text.Image image319 = iTextSharp.text.Image.GetInstance(checkbox);
                        image319.ScaleAbsolute(10f, 10f);
                        PdfPCell cell319 = new PdfPCell(image319);
                        cell319.Border = 0;
                        cell319.HorizontalAlignment = 2;
                        cell319.VerticalAlignment = 6;
                        cell319.FixedHeight = 12f;
                        nesTable2.AddCell(cell319);
                        nesTable2.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image320 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image320.ScaleAbsolute(10f, 10f);
                        PdfPCell cell320 = new PdfPCell(image320);
                        cell320.Border = 0;
                        cell320.HorizontalAlignment = 2;
                        cell320.VerticalAlignment = 6;
                        cell320.FixedHeight = 12f;
                        nesTable2.AddCell(cell320);
                        nesTable2.DefaultCell.Border = 0;
                    }

                    nesTable2.AddCell(hotColdCell);

                    #endregion

                    PdfPCell nesCell2 = new PdfPCell(nesTable2);
                    nesCell2.FixedHeight = 12f;
                    nesCell2.HorizontalAlignment = 0;
                    nesCell2.Border = 0;

                    #region nesTable3

                    PdfPTable nesTable3 = new PdfPTable(9);
                    nesTable3.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable3.TotalWidth = 520f;

                    float[] widths11 = new float[] { 13f, 60f, 13f, 70f, 13f, 130f, 13f, 40f, 168f };
                    nesTable3.SetWidths(widths11);
                    nesTable3.LockedWidth = true;

                    PdfPCell ultrasoundCell = new PdfPCell(new Phrase("Ultrasound,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    ultrasoundCell.FixedHeight = 12f;
                    ultrasoundCell.HorizontalAlignment = 0;
                    ultrasoundCell.Border = 0;

                    PdfPCell massageTherapyCell = new PdfPCell(new Phrase("Massage Therapy,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    massageTherapyCell.FixedHeight = 12f;
                    massageTherapyCell.HorizontalAlignment = 0;
                    massageTherapyCell.Border = 0;

                    PdfPCell kineticActivityCell = new PdfPCell(new Phrase("Kinetic / Therapeutic Activity,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    kineticActivityCell.FixedHeight = 12f;
                    kineticActivityCell.HorizontalAlignment = 0;
                    kineticActivityCell.Border = 0;

                    PdfPCell otherCell1 = new PdfPCell(new Phrase("Other,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    otherCell1.FixedHeight = 12f;
                    otherCell1.HorizontalAlignment = 0;
                    otherCell1.Border = 0;

                    PdfPCell otherCell2 = new PdfPCell(new Phrase(oCHNote.Other1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_OTHER"].ToString()
                    otherCell2.FixedHeight = 12f;
                    otherCell2.HorizontalAlignment = 0;
                    otherCell2.Border = 0;

                    if (oCHNote.UltraSound == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ULTRASOUND"].ToString()
                        iTextSharp.text.Image image321 = iTextSharp.text.Image.GetInstance(checkbox);
                        image321.ScaleAbsolute(10f, 10f);
                        PdfPCell cell321 = new PdfPCell(image321);
                        cell321.Border = 0;
                        cell321.HorizontalAlignment = 2;
                        cell321.VerticalAlignment = 6;
                        cell321.FixedHeight = 12f;
                        nesTable3.AddCell(cell321);
                        nesTable3.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image322 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image322.ScaleAbsolute(10f, 10f);
                        PdfPCell cell322 = new PdfPCell(image322);
                        cell322.Border = 0;
                        cell322.HorizontalAlignment = 2;
                        cell322.VerticalAlignment = 6;
                        cell322.FixedHeight = 12f;
                        nesTable3.AddCell(cell322);
                        nesTable3.DefaultCell.Border = 0;
                    }

                    nesTable3.AddCell(ultrasoundCell);

                    if (oCHNote.MassageTherapy == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_MASSAGE_THERAPY"].ToString()
                        iTextSharp.text.Image image323 = iTextSharp.text.Image.GetInstance(checkbox);
                        image323.ScaleAbsolute(10f, 10f);
                        PdfPCell cell323 = new PdfPCell(image323);
                        cell323.Border = 0;
                        cell323.HorizontalAlignment = 2;
                        cell323.VerticalAlignment = 6;
                        cell323.FixedHeight = 12f;
                        nesTable3.AddCell(cell323);
                        nesTable3.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image324 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image324.ScaleAbsolute(10f, 10f);
                        PdfPCell cell324 = new PdfPCell(image324);
                        cell324.Border = 0;
                        cell324.HorizontalAlignment = 2;
                        cell324.VerticalAlignment = 6;
                        cell324.FixedHeight = 12f;
                        nesTable3.AddCell(cell324);
                        nesTable3.DefaultCell.Border = 0;
                    }

                    nesTable3.AddCell(massageTherapyCell);

                    if ( oCHNote.KineticActivity== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_KINETIC_ACTIVITY"].ToString()
                        iTextSharp.text.Image image325 = iTextSharp.text.Image.GetInstance(checkbox);
                        image325.ScaleAbsolute(10f, 10f);
                        PdfPCell cell325 = new PdfPCell(image325);
                        cell325.Border = 0;
                        cell325.HorizontalAlignment = 2;
                        cell325.VerticalAlignment = 6;
                        cell325.FixedHeight = 12f;
                        nesTable3.AddCell(cell325);
                        nesTable3.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image326 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image326.ScaleAbsolute(10f, 10f);
                        PdfPCell cell326 = new PdfPCell(image326);
                        cell326.Border = 0;
                        cell326.HorizontalAlignment = 2;
                        cell326.VerticalAlignment = 6;
                        cell326.FixedHeight = 12f;
                        nesTable3.AddCell(cell326);
                        nesTable3.DefaultCell.Border = 0;
                    }

                    nesTable3.AddCell(kineticActivityCell);

                    if ( oCHNote.Other== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_OTHER"].ToString()
                        iTextSharp.text.Image image327 = iTextSharp.text.Image.GetInstance(checkbox);
                        image327.ScaleAbsolute(10f, 10f);
                        PdfPCell cell327 = new PdfPCell(image327);
                        cell327.Border = 0;
                        cell327.HorizontalAlignment = 2;
                        cell327.VerticalAlignment = 6;
                        cell327.FixedHeight = 12f;
                        nesTable3.AddCell(cell327);
                        nesTable3.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image328 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image328.ScaleAbsolute(10f, 10f);
                        PdfPCell cell328 = new PdfPCell(image328);
                        cell328.Border = 0;
                        cell328.HorizontalAlignment = 2;
                        cell328.VerticalAlignment = 6;
                        cell328.FixedHeight = 12f;
                        nesTable3.AddCell(cell328);
                        nesTable3.DefaultCell.Border = 0;
                    }

                    nesTable3.AddCell(otherCell1);
                    nesTable3.AddCell(otherCell2);

                    #endregion

                    PdfPCell nesCell3 = new PdfPCell(nesTable3);
                    nesCell3.FixedHeight = 12f;
                    nesCell3.HorizontalAlignment = 0;
                    nesCell3.Border = 0;

                    #region nesTable4

                    PdfPTable nesTable4 = new PdfPTable(10);
                    nesTable4.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable4.TotalWidth = 520f;

                    float[] widths12 = new float[] { 35f, 57f, 35f, 57f, 35f, 55f, 85f, 75f, 16f, 74f };
                    nesTable4.SetWidths(widths12);
                    nesTable4.LockedWidth = true;

                    PdfPCell locCell1 = new PdfPCell(new Phrase("Location:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    locCell1.FixedHeight = 12f;
                    locCell1.HorizontalAlignment = 0;
                    locCell1.Border = 0;

                    PdfPCell locCell2 = new PdfPCell(new Phrase(oCHNote.Location, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_LOCATION"].ToString()
                    locCell2.FixedHeight = 12f;
                    locCell2.HorizontalAlignment = 0;
                    locCell2.Border = 0;

                    PdfPCell intensityCell1 = new PdfPCell(new Phrase("Intensity:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    intensityCell1.FixedHeight = 12f;
                    intensityCell1.HorizontalAlignment = 0;
                    intensityCell1.Border = 0;

                    PdfPCell intensityCell2 = new PdfPCell(new Phrase(oCHNote.Intensity, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_INTENSITY"].ToString()
                    intensityCell2.FixedHeight = 12f;
                    intensityCell2.HorizontalAlignment = 0;
                    intensityCell2.Border = 0;

                    PdfPCell timeCell1 = new PdfPCell(new Phrase("Time: 10/", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    timeCell1.FixedHeight = 12f;
                    timeCell1.HorizontalAlignment = 0;
                    timeCell1.Border = 0;

                    PdfPCell timeCell2 = new PdfPCell(new Phrase(oCHNote.Min, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    timeCell2.FixedHeight = 12f; //chairoView.Tables[0].Rows[0]["SZ_MIN"].ToString()
                    timeCell2.HorizontalAlignment = 0;
                    timeCell2.Border = 0;

                    PdfPCell rxnCell1 = new PdfPCell(new Phrase("min.,Rxn: scpp/norm/abn", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    rxnCell1.FixedHeight = 12f;
                    rxnCell1.HorizontalAlignment = 0;
                    rxnCell1.Border = 0;

                    PdfPCell rxnCell2 = new PdfPCell(new Phrase(oCHNote.Rxn, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    rxnCell2.FixedHeight = 12f;//chairoView.Tables[0].Rows[0]["SZ_RXN"].ToString()
                    rxnCell2.HorizontalAlignment = 0;
                    rxnCell2.Border = 0;

                    PdfPCell initCell1 = new PdfPCell(new Phrase("Init:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    initCell1.FixedHeight = 12f;
                    initCell1.HorizontalAlignment = 0;
                    initCell1.Border = 0;

                    PdfPCell initCell2 = new PdfPCell(new Phrase(oCHNote.InIt, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    initCell2.FixedHeight = 12f;//chairoView.Tables[0].Rows[0]["SZ_INIT"].ToString()
                    initCell2.HorizontalAlignment = 0;
                    initCell2.Border = 0;

                    nesTable4.AddCell(locCell1);
                    nesTable4.AddCell(locCell2);
                    nesTable4.AddCell(intensityCell1);
                    nesTable4.AddCell(intensityCell2);
                    nesTable4.AddCell(timeCell1);
                    nesTable4.AddCell(timeCell2);
                    nesTable4.AddCell(rxnCell1);
                    nesTable4.AddCell(rxnCell2);
                    nesTable4.AddCell(initCell1);
                    nesTable4.AddCell(initCell2);

                    #endregion

                    PdfPCell nesCell4 = new PdfPCell(nesTable4);
                    nesCell4.FixedHeight = 12f;
                    nesCell4.HorizontalAlignment = 0;
                    nesCell4.Border = 0;

                    #region nesTable5

                    PdfPTable nesTable5 = new PdfPTable(12);
                    nesTable5.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable5.TotalWidth = 520f;

                    float[] widths13 = new float[] { 15f, 60f, 15f, 50f, 15f, 30f, 105f, 80f, 15f, 20f, 15f, 105f };
                    nesTable5.SetWidths(widths13);
                    nesTable5.LockedWidth = true;

                    PdfPCell hmInstCell = new PdfPCell(new Phrase("Home Instruction:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    hmInstCell.FixedHeight = 12f;
                    hmInstCell.HorizontalAlignment = 0;
                    hmInstCell.Border = 0;

                    PdfPCell iceTherapyCell = new PdfPCell(new Phrase("Ice Therapy,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    iceTherapyCell.FixedHeight = 12f;
                    iceTherapyCell.HorizontalAlignment = 0;
                    iceTherapyCell.Border = 0;

                    PdfPCell tractionCell1 = new PdfPCell(new Phrase("Traction", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    tractionCell1.FixedHeight = 12f;
                    tractionCell1.HorizontalAlignment = 0;
                    tractionCell1.Border = 0;

                    PdfPCell tractionCell2 = new PdfPCell(new Phrase(oCHNote.TrAction1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    tractionCell2.FixedHeight = 12f;//chairoView.Tables[0].Rows[0]["SZ_TRACTION"].ToString()
                    tractionCell2.HorizontalAlignment = 0;
                    tractionCell2.Border = 0;

                    PdfPCell sleepPostureCell = new PdfPCell(new Phrase("Support Sleep Posture:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    sleepPostureCell.FixedHeight = 12f;
                    sleepPostureCell.HorizontalAlignment = 0;
                    sleepPostureCell.Border = 0;

                    PdfPCell spbackCell = new PdfPCell(new Phrase("Back", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    spbackCell.FixedHeight = 12f;
                    spbackCell.HorizontalAlignment = 0;
                    spbackCell.Border = 0;

                    PdfPCell spsideCell = new PdfPCell(new Phrase("Side", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    spsideCell.FixedHeight = 12f;
                    spsideCell.HorizontalAlignment = 0;
                    spsideCell.Border = 0;

                    if ( oCHNote.HomeInst== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_HOME_INST"].ToString()
                        iTextSharp.text.Image image329 = iTextSharp.text.Image.GetInstance(checkbox);
                        image329.ScaleAbsolute(10f, 10f);
                        PdfPCell cell329 = new PdfPCell(image329);
                        cell329.Border = 0;
                        cell329.HorizontalAlignment = 2;
                        cell329.VerticalAlignment = 6;
                        cell329.FixedHeight = 12f;
                        nesTable5.AddCell(cell329);
                        nesTable5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image330 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image330.ScaleAbsolute(10f, 10f);
                        PdfPCell cell330 = new PdfPCell(image330);
                        cell330.Border = 0;
                        cell330.HorizontalAlignment = 2;
                        cell330.VerticalAlignment = 6;
                        cell330.FixedHeight = 12f;
                        nesTable5.AddCell(cell330);
                        nesTable5.DefaultCell.Border = 0;
                    }

                    nesTable5.AddCell(hmInstCell);

                    if ( oCHNote.IceTherapy== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_ICE_THERAPY"].ToString()
                        iTextSharp.text.Image image331 = iTextSharp.text.Image.GetInstance(checkbox);
                        image331.ScaleAbsolute(10f, 10f);
                        PdfPCell cell331 = new PdfPCell(image331);
                        cell331.Border = 0;
                        cell331.HorizontalAlignment = 2;
                        cell331.VerticalAlignment = 6;
                        cell331.FixedHeight = 12f;
                        nesTable5.AddCell(cell331);
                        nesTable5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image332 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image332.ScaleAbsolute(10f, 10f);
                        PdfPCell cell332 = new PdfPCell(image332);
                        cell332.Border = 0;
                        cell332.HorizontalAlignment = 2;
                        cell332.VerticalAlignment = 6;
                        cell332.FixedHeight = 12f;
                        nesTable5.AddCell(cell332);
                        nesTable5.DefaultCell.Border = 0;
                    }

                    nesTable5.AddCell(iceTherapyCell);

                    if ( oCHNote.TrAction== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_TRACTION"].ToString()
                        iTextSharp.text.Image image333 = iTextSharp.text.Image.GetInstance(checkbox);
                        image333.ScaleAbsolute(10f, 10f);
                        PdfPCell cell333 = new PdfPCell(image333);
                        cell333.Border = 0;
                        cell333.HorizontalAlignment = 2;
                        cell333.VerticalAlignment = 6;
                        cell333.FixedHeight = 12f;
                        nesTable5.AddCell(cell333);
                        nesTable5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image334 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image334.ScaleAbsolute(10f, 10f);
                        PdfPCell cell334 = new PdfPCell(image334);
                        cell334.Border = 0;
                        cell334.HorizontalAlignment = 2;
                        cell334.VerticalAlignment = 6;
                        cell334.FixedHeight = 12f;
                        nesTable5.AddCell(cell334);
                        nesTable5.DefaultCell.Border = 0;
                    }

                    nesTable5.AddCell(tractionCell1);
                    nesTable5.AddCell(tractionCell2);
                    nesTable5.AddCell(sleepPostureCell);

                    if (oCHNote.SupportSleeBack == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_SUPPORT_SLEE_BACK"].ToString()
                        iTextSharp.text.Image image335 = iTextSharp.text.Image.GetInstance(checkbox);
                        image335.ScaleAbsolute(10f, 10f);
                        PdfPCell cell335 = new PdfPCell(image335);
                        cell335.Border = 0;
                        cell335.HorizontalAlignment = 2;
                        cell335.VerticalAlignment = 6;
                        cell335.FixedHeight = 12f;
                        nesTable5.AddCell(cell335);
                        nesTable5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image336 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image336.ScaleAbsolute(10f, 10f);
                        PdfPCell cell336 = new PdfPCell(image336);
                        cell336.Border = 0;
                        cell336.HorizontalAlignment = 2;
                        cell336.VerticalAlignment = 6;
                        cell336.FixedHeight = 12f;
                        nesTable5.AddCell(cell336);
                        nesTable5.DefaultCell.Border = 0;
                    }

                    nesTable5.AddCell(spbackCell);

                    if (oCHNote.SupportSleeSide== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_SUPPORT_SLEE_SIDE"].ToString() 
                        iTextSharp.text.Image image337 = iTextSharp.text.Image.GetInstance(checkbox);
                        image337.ScaleAbsolute(10f, 10f);
                        PdfPCell cell337 = new PdfPCell(image337);
                        cell337.Border = 0;
                        cell337.HorizontalAlignment = 2;
                        cell337.VerticalAlignment = 6;
                        cell337.FixedHeight = 12f;
                        nesTable5.AddCell(cell337);
                        nesTable5.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image338 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image338.ScaleAbsolute(10f, 10f);
                        PdfPCell cell338 = new PdfPCell(image338);
                        cell338.Border = 0;
                        cell338.HorizontalAlignment = 2;
                        cell338.VerticalAlignment = 6;
                        cell338.FixedHeight = 12f;
                        nesTable5.AddCell(cell338);
                        nesTable5.DefaultCell.Border = 0;
                    }

                    nesTable5.AddCell(spsideCell);

                    #endregion

                    PdfPCell nesCell5 = new PdfPCell(nesTable5);
                    nesCell5.FixedHeight = 12f;
                    nesCell5.HorizontalAlignment = 0;
                    nesCell5.Border = 0;

                    #region nesTable6

                    PdfPTable nesTable6 = new PdfPTable(13);
                    nesTable6.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable6.TotalWidth = 520f;

                    float[] widths14 = new float[] { 13f, 130f, 13f, 22f, 13f, 22f, 13f, 20f, 13f, 20f, 13f, 50f, 178f };
                    nesTable6.SetWidths(widths14);
                    nesTable6.LockedWidth = true;

                    PdfPCell personalStretchCell = new PdfPCell(new Phrase("Personal Stretch / Exercise Program:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    personalStretchCell.FixedHeight = 12f;
                    personalStretchCell.HorizontalAlignment = 0;
                    personalStretchCell.Border = 0;

                    PdfPCell neckCell = new PdfPCell(new Phrase("neck,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    neckCell.FixedHeight = 12f;
                    neckCell.HorizontalAlignment = 0;
                    neckCell.Border = 0;

                    PdfPCell backCell = new PdfPCell(new Phrase("back,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    backCell.FixedHeight = 12f;
                    backCell.HorizontalAlignment = 0;
                    backCell.Border = 0;

                    PdfPCell UECell = new PdfPCell(new Phrase("UE,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    UECell.FixedHeight = 12f;
                    UECell.HorizontalAlignment = 0;
                    UECell.Border = 0;

                    PdfPCell LECell = new PdfPCell(new Phrase("LE,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    LECell.FixedHeight = 12f;
                    LECell.HorizontalAlignment = 0;
                    LECell.Border = 0;

                    PdfPCell wholeBodyCell1 = new PdfPCell(new Phrase("whole body,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    wholeBodyCell1.FixedHeight = 12f;
                    wholeBodyCell1.HorizontalAlignment = 0;
                    wholeBodyCell1.Border = 0;

                    PdfPCell wholeBodyCell2 = new PdfPCell(new Phrase(oCHNote.WholeBody1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    wholeBodyCell2.FixedHeight = 12f;//chairoView.Tables[0].Rows[0]["SZ_WHOLE_BODY"].ToString()
                    wholeBodyCell2.HorizontalAlignment = 0;
                    wholeBodyCell2.Border = 0;

                    if (oCHNote.PersonalStretch == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_PERSONAL_STRETCH"].ToString()
                        iTextSharp.text.Image image339 = iTextSharp.text.Image.GetInstance(checkbox);
                        image339.ScaleAbsolute(10f, 10f);
                        PdfPCell cell339 = new PdfPCell(image339);
                        cell339.Border = 0;
                        cell339.HorizontalAlignment = 2;
                        cell339.VerticalAlignment = 6;
                        cell339.FixedHeight = 12f;
                        nesTable6.AddCell(cell339);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image340 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image340.ScaleAbsolute(10f, 10f);
                        PdfPCell cell340 = new PdfPCell(image340);
                        cell340.Border = 0;
                        cell340.HorizontalAlignment = 2;
                        cell340.VerticalAlignment = 6;
                        cell340.FixedHeight = 12f;
                        nesTable6.AddCell(cell340);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(personalStretchCell);

                    if (oCHNote.Neck == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_NECK"].ToString()
                        iTextSharp.text.Image image341 = iTextSharp.text.Image.GetInstance(checkbox);
                        image341.ScaleAbsolute(10f, 10f);
                        PdfPCell cell341 = new PdfPCell(image341);
                        cell341.Border = 0;
                        cell341.HorizontalAlignment = 2;
                        cell341.VerticalAlignment = 6;
                        cell341.FixedHeight = 12f;
                        nesTable6.AddCell(cell341);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image342 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image342.ScaleAbsolute(10f, 10f);
                        PdfPCell cell342 = new PdfPCell(image342);
                        cell342.Border = 0;
                        cell342.HorizontalAlignment = 2;
                        cell342.VerticalAlignment = 6;
                        cell342.FixedHeight = 12f;
                        nesTable6.AddCell(cell342);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(neckCell);

                    if (oCHNote.Back== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_BACK"].ToString() 
                        iTextSharp.text.Image image343 = iTextSharp.text.Image.GetInstance(checkbox);
                        image343.ScaleAbsolute(10f, 10f);
                        PdfPCell cell343 = new PdfPCell(image343);
                        cell343.Border = 0;
                        cell343.HorizontalAlignment = 2;
                        cell343.VerticalAlignment = 6;
                        cell343.FixedHeight = 12f;
                        nesTable6.AddCell(cell343);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image344 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image344.ScaleAbsolute(10f, 10f);
                        PdfPCell cell344 = new PdfPCell(image344);
                        cell344.Border = 0;
                        cell344.HorizontalAlignment = 2;
                        cell344.VerticalAlignment = 6;
                        cell344.FixedHeight = 12f;
                        nesTable6.AddCell(cell344);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(backCell);

                    if (oCHNote.Ue == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_UE"].ToString()
                        iTextSharp.text.Image image345 = iTextSharp.text.Image.GetInstance(checkbox);
                        image345.ScaleAbsolute(10f, 10f);
                        PdfPCell cell345 = new PdfPCell(image345);
                        cell345.Border = 0;
                        cell345.HorizontalAlignment = 2;
                        cell345.VerticalAlignment = 6;
                        cell345.FixedHeight = 12f;
                        nesTable6.AddCell(cell345);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image346 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image346.ScaleAbsolute(10f, 10f);
                        PdfPCell cell346 = new PdfPCell(image346);
                        cell346.Border = 0;
                        cell346.HorizontalAlignment = 2;
                        cell346.VerticalAlignment = 6;
                        cell346.FixedHeight = 12f;
                        nesTable6.AddCell(cell346);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(UECell);

                    if ( oCHNote.Lf== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_LE"].ToString()
                        iTextSharp.text.Image image347 = iTextSharp.text.Image.GetInstance(checkbox);
                        image347.ScaleAbsolute(10f, 10f);
                        PdfPCell cell347 = new PdfPCell(image347);
                        cell347.Border = 0;
                        cell347.HorizontalAlignment = 2;
                        cell347.VerticalAlignment = 6;
                        cell347.FixedHeight = 12f;
                        nesTable6.AddCell(cell347);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image348 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image348.ScaleAbsolute(10f, 10f);
                        PdfPCell cell348 = new PdfPCell(image348);
                        cell348.Border = 0;
                        cell348.HorizontalAlignment = 2;
                        cell348.VerticalAlignment = 6;
                        cell348.FixedHeight = 12f;
                        nesTable6.AddCell(cell348);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(LECell);

                    if ( oCHNote.WholeBody== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_WHOLE_BODY"].ToString()
                        iTextSharp.text.Image image349 = iTextSharp.text.Image.GetInstance(checkbox);
                        image349.ScaleAbsolute(10f, 10f);
                        PdfPCell cell349 = new PdfPCell(image349);
                        cell349.Border = 0;
                        cell349.HorizontalAlignment = 2;
                        cell349.VerticalAlignment = 6;
                        cell349.FixedHeight = 12f;
                        nesTable6.AddCell(cell349);
                        nesTable6.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image350 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image350.ScaleAbsolute(10f, 10f);
                        PdfPCell cell350 = new PdfPCell(image350);
                        cell350.Border = 0;
                        cell350.HorizontalAlignment = 2;
                        cell350.VerticalAlignment = 6;
                        cell350.FixedHeight = 12f;
                        nesTable6.AddCell(cell350);
                        nesTable6.DefaultCell.Border = 0;
                    }

                    nesTable6.AddCell(wholeBodyCell1);
                    nesTable6.AddCell(wholeBodyCell2);

                    #endregion

                    PdfPCell nesCell6 = new PdfPCell(nesTable6);
                    nesCell6.FixedHeight = 12f;
                    nesCell6.HorizontalAlignment = 0;
                    nesCell6.Border = 0;

                    PdfPCell ptCell2 = new PdfPCell(new Phrase("2. The following recommendations are made with regard to the ongoing Clinical Management of this patient:", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    ptCell2.FixedHeight = 12f;
                    ptCell2.HorizontalAlignment = 0;
                    ptCell2.Border = 0;

                    #region nesTable7

                    PdfPTable nesTable7 = new PdfPTable(9);
                    nesTable7.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable7.TotalWidth = 520f;

                    float[] widths15 = new float[] { 13f, 70f, 13f, 70f, 13f, 70f, 13f, 100f, 158f };
                    nesTable7.SetWidths(widths15);
                    nesTable7.LockedWidth = true;

                    PdfPCell contCarePlanCell = new PdfPCell(new Phrase("Continue Care Plan,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    contCarePlanCell.FixedHeight = 12f;
                    contCarePlanCell.HorizontalAlignment = 0;
                    contCarePlanCell.Border = 0;

                    PdfPCell modCarePlanCell = new PdfPCell(new Phrase("Modify Care Plan,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    modCarePlanCell.FixedHeight = 12f;
                    modCarePlanCell.HorizontalAlignment = 0;
                    modCarePlanCell.Border = 0;

                    PdfPCell reexamCell = new PdfPCell(new Phrase("R/S ReExamination,", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    reexamCell.FixedHeight = 12f;
                    reexamCell.HorizontalAlignment = 0;
                    reexamCell.Border = 0;

                    PdfPCell reffEvaluationCell1 = new PdfPCell(new Phrase("Refferal for Further Evaluation:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    reffEvaluationCell1.FixedHeight = 12f;
                    reffEvaluationCell1.HorizontalAlignment = 0;
                    reffEvaluationCell1.Border = 0;

                    PdfPCell reffEvaluationCell2 = new PdfPCell(new Phrase(oCHNote.ReffrralEval1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    reffEvaluationCell2.FixedHeight = 12f;//chairoView.Tables[0].Rows[0]["SZ_REFERRAL_EVAL"].ToString()
                    reffEvaluationCell2.HorizontalAlignment = 0;
                    reffEvaluationCell2.Border = 0;

                    if (oCHNote.ContCarePlan == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_CONT_CARE_PLAN"].ToString()
                        iTextSharp.text.Image image351 = iTextSharp.text.Image.GetInstance(checkbox);
                        image351.ScaleAbsolute(10f, 10f);
                        PdfPCell cell351 = new PdfPCell(image351);
                        cell351.Border = 0;
                        cell351.HorizontalAlignment = 2;
                        cell351.VerticalAlignment = 6;
                        cell351.FixedHeight = 12f;
                        nesTable7.AddCell(cell351);
                        nesTable7.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image352 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image352.ScaleAbsolute(10f, 10f);
                        PdfPCell cell352 = new PdfPCell(image352);
                        cell352.Border = 0;
                        cell352.HorizontalAlignment = 2;
                        cell352.VerticalAlignment = 6;
                        cell352.FixedHeight = 12f;
                        nesTable7.AddCell(cell352);
                        nesTable7.DefaultCell.Border = 0;
                    }

                    nesTable7.AddCell(contCarePlanCell);

                    if (oCHNote.ModifyCarePlan == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_MODIFY_CARE_PLAN"].ToString()
                        iTextSharp.text.Image image354 = iTextSharp.text.Image.GetInstance(checkbox);
                        image354.ScaleAbsolute(10f, 10f);
                        PdfPCell cell354 = new PdfPCell(image354);
                        cell354.Border = 0;
                        cell354.HorizontalAlignment = 2;
                        cell354.VerticalAlignment = 6;
                        cell354.FixedHeight = 12f;
                        nesTable7.AddCell(cell354);
                        nesTable7.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image355 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image355.ScaleAbsolute(10f, 10f);
                        PdfPCell cell355 = new PdfPCell(image355);
                        cell355.Border = 0;
                        cell355.HorizontalAlignment = 2;
                        cell355.VerticalAlignment = 6;
                        cell355.FixedHeight = 12f;
                        nesTable7.AddCell(cell355);
                        nesTable7.DefaultCell.Border = 0;
                    }

                    nesTable7.AddCell(modCarePlanCell);

                    if (oCHNote.RsReExam == "True")
                    {//chairoView.Tables[0].Rows[0]["BT_RS_REEXAM"].ToString()
                        iTextSharp.text.Image image356 = iTextSharp.text.Image.GetInstance(checkbox);
                        image356.ScaleAbsolute(10f, 10f);
                        PdfPCell cell356 = new PdfPCell(image356);
                        cell356.Border = 0;
                        cell356.HorizontalAlignment = 2;
                        cell356.VerticalAlignment = 6;
                        cell356.FixedHeight = 12f;
                        nesTable7.AddCell(cell356);
                        nesTable7.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image357 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image357.ScaleAbsolute(10f, 10f);
                        PdfPCell cell357 = new PdfPCell(image357);
                        cell357.Border = 0;
                        cell357.HorizontalAlignment = 2;
                        cell357.VerticalAlignment = 6;
                        cell357.FixedHeight = 12f;
                        nesTable7.AddCell(cell357);
                        nesTable7.DefaultCell.Border = 0;
                    }

                    nesTable7.AddCell(reexamCell);

                    if ( oCHNote.ReffrralEval== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_REFERRAL_EVAL"].ToString()
                        iTextSharp.text.Image image358 = iTextSharp.text.Image.GetInstance(checkbox);
                        image358.ScaleAbsolute(10f, 10f);
                        PdfPCell cell358 = new PdfPCell(image358);
                        cell358.Border = 0;
                        cell358.HorizontalAlignment = 2;
                        cell358.VerticalAlignment = 6;
                        cell358.FixedHeight = 12f;
                        nesTable7.AddCell(cell358);
                        nesTable7.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image361 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image361.ScaleAbsolute(10f, 10f);
                        PdfPCell cell361 = new PdfPCell(image361);
                        cell361.Border = 0;
                        cell361.HorizontalAlignment = 2;
                        cell361.VerticalAlignment = 6;
                        cell361.FixedHeight = 12f;
                        nesTable7.AddCell(cell361);
                        nesTable7.DefaultCell.Border = 0;
                    }

                    nesTable7.AddCell(reffEvaluationCell1);
                    nesTable7.AddCell(reffEvaluationCell2);

                    #endregion

                    PdfPCell nesCell7 = new PdfPCell(nesTable7);
                    nesCell7.FixedHeight = 12f;
                    nesCell7.HorizontalAlignment = 0;
                    nesCell7.Border = 0;

                    #region nesTable8

                    PdfPTable nesTable8 = new PdfPTable(3);
                    nesTable8.HorizontalAlignment = Element.ALIGN_LEFT;
                    nesTable8.TotalWidth = 520f;

                    float[] widths16 = new float[] { 13f, 180f, 327f };
                    nesTable8.SetWidths(widths16);
                    nesTable8.LockedWidth = true;

                    PdfPCell reffDiagnosticCell1 = new PdfPCell(new Phrase("Refferal for diagnostic / imaging assessment to include:", FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    reffDiagnosticCell1.FixedHeight = 12f;
                    reffDiagnosticCell1.HorizontalAlignment = 0;
                    reffDiagnosticCell1.Border = 0;

                    PdfPCell reffDiagnosticCell2 = new PdfPCell(new Phrase(oCHNote.ReffrralDiag1, FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK)));
                    reffDiagnosticCell2.FixedHeight = 12f;   //chairoView.Tables[0].Rows[0]["SZ_REFERRAL_DIAG"].ToString()
                    reffDiagnosticCell2.HorizontalAlignment = 0;
                    reffDiagnosticCell2.Border = 0;

                    if (oCHNote.ReffrralDiag== "True")
                    {//chairoView.Tables[0].Rows[0]["BT_REFERRAL_DIAG"].ToString() 
                        iTextSharp.text.Image image359 = iTextSharp.text.Image.GetInstance(checkbox);
                        image359.ScaleAbsolute(10f, 10f);
                        PdfPCell cell359 = new PdfPCell(image359);
                        cell359.Border = 0;
                        cell359.HorizontalAlignment = 2;
                        cell359.VerticalAlignment = 6;
                        cell359.FixedHeight = 12f;
                        nesTable8.AddCell(cell359);
                        nesTable8.DefaultCell.Border = 0;
                    }
                    else
                    {
                        iTextSharp.text.Image image360 = iTextSharp.text.Image.GetInstance(uncheckbox);
                        image360.ScaleAbsolute(10f, 10f);
                        PdfPCell cell360 = new PdfPCell(image360);
                        cell360.Border = 0;
                        cell360.HorizontalAlignment = 2;
                        cell360.VerticalAlignment = 6;
                        cell360.FixedHeight = 12f;
                        nesTable8.AddCell(cell360);
                        nesTable8.DefaultCell.Border = 0;
                    }

                    nesTable8.AddCell(reffDiagnosticCell1);
                    nesTable8.AddCell(reffDiagnosticCell2);

                    #endregion

                    PdfPCell nesCell8 = new PdfPCell(nesTable8);
                    nesCell8.FixedHeight = 12f;
                    nesCell8.HorizontalAlignment = 0;
                    nesCell8.Border = 0;


                    //treatPlanTable1.AddCell(hdtreatPlanCell);
                    treatPlanTable1.AddCell(ptCell1);
                    treatPlanTable1.AddCell(nesCell1);
                    treatPlanTable1.AddCell(nesCell2);
                    treatPlanTable1.AddCell(nesCell3);
                    treatPlanTable1.AddCell(nesCell4);
                    treatPlanTable1.AddCell(nesCell5);
                    treatPlanTable1.AddCell(nesCell6);
                    treatPlanTable1.AddCell(ptCell2);
                    treatPlanTable1.AddCell(nesCell7);
                    treatPlanTable1.AddCell(nesCell8);
                    table3.AddCell(treatPlanTable1);

                    #endregion
                    //treatment

                    //notes
                    float[] notes = new float[] { 2f, 6f };
                    PdfPTable tbl_comments = new PdfPTable(numArray10);
                    table.DefaultCell.Border = 15;
                    table5.TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    tbl_comments.DefaultCell.Colspan = 1;
                    tbl_comments.DefaultCell.Border = 0;
                    tbl_comments.AddCell(new Phrase("Notes : ", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    tbl_comments.DefaultCell.Border = 2;
                    tbl_comments.AddCell(new Phrase(oCHNote.Notes, FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));//chairoView.Tables[0].Rows[0]["SZ_NOTES"].ToString()
                    tbl_comments.DefaultCell.Border = 0;
                    table3.AddCell(tbl_comments);
                    //notes


                    DataSet set4 = new DataSet();
                    set4 = GET_COMPLIANTS_USING_EVENTID(eventID);
                    float[] numArray17 = new float[] { 1f, 4f, 1f, 4f };
                    PdfPTable table17 = new PdfPTable(numArray17);
                    table.DefaultCell.Border = 15;
                    table17.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table17.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table17.DefaultCell.Colspan = 4;
                    table17.AddCell(new Phrase("Complaints :", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table17.DefaultCell.Colspan = 1;
                    for (int k = 0; k < set4.Tables[0].Rows.Count; k++)
                    {
                        iTextSharp.text.Image image219 = iTextSharp.text.Image.GetInstance(checkbox);
                        image219.ScaleAbsolute(10f, 10f);
                        PdfPCell cell219 = new PdfPCell(image219);
                        cell219.Border = 0;
                        cell219.HorizontalAlignment = 2;
                        cell219.VerticalAlignment = 6;
                        cell219.FixedHeight = 12f;
                        table17.AddCell(cell219);
                        table17.DefaultCell.Border = 0;
                        table17.AddCell(new Phrase(set4.Tables[0].Rows[k]["SZ_COMPLAINT"].ToString(), FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK)));
                        table17.DefaultCell.HorizontalAlignment = 0;
                        table17.DefaultCell.VerticalAlignment = 6;
                    }
                    if ((set4.Tables[0].Rows.Count % 2) != 0)
                    {
                        table17.AddCell(new Phrase(""));
                        table17.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table17);

                    DataSet set5 = new DataSet();
                    set5 = this.GET_PROCEDURECODE_USING_EVENTID(eventID);
                    float[] numArray18 = new float[] { 1f, 3f, 1f, 3f };
                    PdfPTable table18 = new PdfPTable(numArray18);
                    table.DefaultCell.Border = 15;
                    table18.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table18.DefaultCell.Border = 2;
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table18.DefaultCell.Colspan = 4;
                    table18.AddCell(new Phrase("Code :", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    table18.DefaultCell.Colspan = 1;
                    for (int m = 0; m < set5.Tables[0].Rows.Count; m++)
                    {
                        iTextSharp.text.Image image220 = iTextSharp.text.Image.GetInstance(checkbox);
                        image220.ScaleAbsolute(10f, 10f);
                        PdfPCell cell220 = new PdfPCell(image220);
                        cell220.Border = 0;
                        cell220.HorizontalAlignment = 2;
                        cell220.VerticalAlignment = 6;
                        cell220.FixedHeight = 12f;
                        table18.AddCell(cell220);
                        table18.DefaultCell.Border = 0;

                        table18.AddCell(new Phrase(set5.Tables[0].Rows[m]["Column1"].ToString(), FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                        table18.DefaultCell.HorizontalAlignment = 0;
                        table18.DefaultCell.VerticalAlignment = 6;
                    }
                    if ((set5.Tables[0].Rows.Count % 2) != 0)
                    {
                        table18.AddCell(new Phrase(""));
                        table18.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table18);
                    float[] numArray19 = new float[] { 1f, 2f, 1f, 2f };
                    PdfPTable table19 = new PdfPTable(numArray19);
                    table.DefaultCell.Border = 0;
                    table19.DefaultCell.Border = 0;
                    table19.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table4.DefaultCell.HorizontalAlignment = 1;
                    table4.DefaultCell.VerticalAlignment = 5;
                    table19.AddCell(new Phrase("Patient Sign", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image image221 = iTextSharp.text.Image.GetInstance(oCHNote.PatientSignPath.Replace("/", @"\"));
                        image221.ScaleAbsoluteHeight(30f);  //chairoView.Tables[0].Rows[0]["SZ_PATIENT_SIGN_PATH"].ToString()
                        image221.ScaleAbsoluteWidth(50f);
                        PdfPCell cell221 = new PdfPCell(image221);
                        cell221.HorizontalAlignment = 0;
                        cell221.Border = 2;
                        cell221.PaddingBottom = 1f;
                        table19.AddCell(cell221);
                    }
                    catch (Exception)
                    {
                        table19.AddCell(new Phrase(""));
                    }
                    table19.AddCell(new Phrase("Doctor Sign", FontFactory.GetFont("Arial", 8f, 1, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image image222 = iTextSharp.text.Image.GetInstance(oCHNote.DoctorSignPath.Replace("/", @"\"));
                        image222.ScaleAbsoluteHeight(30f);//chairoView.Tables[0].Rows[0]["SZ_DOCTOR_SIGN_PATH"].ToString()
                        image222.ScaleAbsoluteWidth(50f);
                        PdfPCell cell222 = new PdfPCell(image222);
                        cell222.HorizontalAlignment = 0;
                        cell222.Border = 2;
                        cell222.PaddingBottom = 1f;
                        table19.AddCell(cell222);
                        table19.AddCell(new Phrase(""));
                    }
                    catch (Exception)
                    {
                        table19.AddCell(new Phrase(""));
                    }
                    table3.AddCell(table19);
                    table.AddCell(table3);
                    float[] numArray20 = new float[] { 4f };
                    PdfPTable table20 = new PdfPTable(numArray20);
                    table20.TotalWidth = ((document.PageSize.Width - document.LeftMargin) - document.RightMargin);
                    table20.DefaultCell.Border = 0;
                    table20.DefaultCell.HorizontalAlignment = 1;
                    table20.DefaultCell.VerticalAlignment = 4;
                    table20.DefaultCell.Colspan = 1;
                    table20.AddCell(new Phrase(""));
                    table20.AddCell(new Phrase(""));
                    table.AddCell(table20);
                }
                table.WriteSelectedRows(0, -1, document.LeftMargin, ((document.PageSize.Height - document.TopMargin) - table2.TotalHeight) - 1f, instance.DirectContent);
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
       
    }
}
