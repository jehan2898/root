using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.IO;
//using WindowsFormsApplication1.DataAcessObject;
using System.Collections;
using System.Data;


   public class DrugReport
    {
        public void GenerateDrugReport(string sFilePath, DrugDAO objdao,string datefrom,DataTable dtfinal)
        {
            int cnt = 0;

             DrugDAO objdaotmp = new DrugDAO();
            MemoryStream m = new MemoryStream();
            FileStream fs = new FileStream(sFilePath, System.IO.FileMode.OpenOrCreate);

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);

            float[] wMain = { 6f };
            PdfPTable tblMain = new PdfPTable(wMain);
            tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblMain.WidthPercentage = 100;

            float[] wBase = { 4f };
            PdfPTable tblBase = new PdfPTable(wBase);
            tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            tblBase.WidthPercentage = 100;
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            while (cnt >=0)
            {
                cnt=cnt-34;
                float[] fTop = { 4f };
                // PdfPTable Ttop = new PdfPTable(fTop);
                PdfPTable Ttop = new PdfPTable(1);
                Ttop.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                Ttop.WidthPercentage = 100;
                Ttop.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell companyname = new PdfPCell(new Phrase(objdao.sz_company_name, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));


                companyname.Border = iTextSharp.text.Rectangle.NO_BORDER;
                companyname.HorizontalAlignment = Element.ALIGN_CENTER;
                Ttop.AddCell(companyname);


                PdfPCell companyaddress = new PdfPCell(new Phrase(objdao.sz_company_name_address, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                companyaddress.Border = iTextSharp.text.Rectangle.NO_BORDER;
                companyaddress.HorizontalAlignment = Element.ALIGN_CENTER;
                Ttop.AddCell(companyaddress);

                Ttop.AddCell(new Phrase(objdao.sz_city + " " + objdao.sz_state + " " + objdao.sz_zip, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                Ttop.AddCell(new Phrase("TEL  " + objdaotmp.sz_tel, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                Ttop.AddCell(new Phrase("FAX  " + objdaotmp.sz_fax, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));




                float[] wtel = { 1f, 0.12f, 0.37f, 1f };
                PdfPTable ttel = new PdfPTable(wtel);
                ttel.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                ttel.WidthPercentage = 100;
                ttel.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;


                tblMain.AddCell(Ttop);
                tblMain.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblMain.AddCell(new Phrase("______________________________________________________________________________________________________________", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                float[] f1 = { 4f };
                PdfPTable T1 = new PdfPTable(fTop);
                T1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                T1.WidthPercentage = 100;
                T1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                T1.AddCell(new Phrase("DELIVERY RECEIPT", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell(T1);
                tblMain.AddCell("");
                tblMain.AddCell("");
                float[] f2 = { .8f, 2.5f, .4f, 2f };
                PdfPTable T2 = new PdfPTable(f2);
                T2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                T2.WidthPercentage = 100;

                T2.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase(objdao.sz_patient_name, iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase("DOA:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase(objdao.date_of_accident, iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));


                T2.AddCell(new Phrase("Address:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase(objdao.sz_patient_address, iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase("Ins.Co:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T2.AddCell(new Phrase(objdao.sz_Ins_Co, iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell(T2);
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");

                float[] f3 = { 4f };
                PdfPTable T3 = new PdfPTable(f3);
                T3.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                T3.WidthPercentage = 100;
                T3.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                T3.AddCell(new Phrase("DELIVERY ORDER LIST", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD | Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell(T3);
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");

                float[] f4 = { 2F, 4f };

                PdfPTable T4 = new PdfPTable(f4);
                T4.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                T4.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
                //T4.WidthPercentage = 100;
                T4.WidthPercentage = 70;
                T4.TotalWidth = 60;
                //T4.AddCell(new Phrase("Order no", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

                //T4.AddCell(new Phrase("Drug", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));


                for (int k = 0; k < dtfinal.Rows.Count && k <= 34; k++)
                {

                    //drugorderlist objlist = new drugorderlist();
                    //objlist = (drugorderlist)arr[k];
                    T4.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    T4.AddCell(new Phrase(dtfinal.Rows[k]["SZ_PROCEDURE_CODE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                    T4.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    //T4.AddCell(new Phrase(objlist.ordernumber, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                    T4.AddCell(new Phrase(dtfinal.Rows[k]["SZ_CODE_DESCRIPTION"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                    cnt++;
                }




                    //for (int k = 0; k < arr.Count && k <= 34; k++)
                    //{

                    //    drugorderlist objlist = new drugorderlist();
                    //    objlist = (drugorderlist)arr[k];
                    //    T4.AddCell(new Phrase(objlist.ordernumber, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                    //    T4.AddCell(new Phrase(objlist.ordereddrug, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                    //    cnt++;
                    //}

                for (int ki = 34; ki < dtfinal.Rows.Count && ki >= 0; ki--)
                {
                    dtfinal.Rows[ki].Delete();
                  
                }

                    //for (int ki = 34; ki < arr.Count && ki>=0; ki--)
                    //{
                    //   // arr.Remove((drugorderlist)arr[ki]);
                    //   arr.RemoveAt(ki);
                    //}
               





                tblMain.AddCell("");
                tblMain.AddCell("");

                tblMain.AddCell(T4);

                float[] f5 = { 4f };
                PdfPTable T5 = new PdfPTable(f5);
                T5.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                T5.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                T5.WidthPercentage = 100;

                T5.AddCell(new Phrase("I have received equipments and supplies listed above along with instructions on use of it.", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                T5.AddCell(new Phrase("I indicate that Drugs R Us Pharmacy,Inc.cannot be held responsible for any inappropriate use of this ", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                T5.AddCell(new Phrase("equipment or supplies. ", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell(T5);
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                tblMain.AddCell("");
                float[] f6 = { .5f, 2.5f, .3f, 2f };
                PdfPTable T6 = new PdfPTable(f6);
                T6.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                T6.WidthPercentage = 100;

                T6.AddCell(new Phrase("Signature:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T6.AddCell(new Phrase("_______________________________", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T6.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                T6.AddCell(new Phrase(datefrom, iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
               // T6.AddCell(new Phrase("_______________________________", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblMain.AddCell(T6);
            }
            document.Add(tblMain);


            document.Close();



        }
    }

