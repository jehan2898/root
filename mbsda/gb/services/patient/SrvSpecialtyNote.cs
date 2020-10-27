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

namespace gb.mbs.da.gb.services.patient
{
    public class SrvSpecialtyNote
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);


        public string PrintVisitNote(gbmodel.patient.Patient p_oPatient, gbmodel.user.User p_oUser)
        {
            specialtyNote.SrvPatient oSrvPatient = new specialtyNote.SrvPatient();
            List<gbmodel.patient.SpecialtyNote> oList = new List<gbmodel.patient.SpecialtyNote>();
            oList = oSrvPatient.SelectSpecialtyNote(p_oPatient);
            string OpenPdfFilepath = "";
            DataSet ds = new DataSet();
            ds = GetVisitInfo(p_oPatient.CaseID, p_oPatient.Account.ID);
            string szfirstname = "";

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PatientName"].ToString() != "")
                {
                    szfirstname = ds.Tables[0].Rows[0]["PatientName"].ToString();
                    szfirstname = szfirstname.Replace(" ", string.Empty);
                    szfirstname = szfirstname.Replace(".", string.Empty);
                    szfirstname = szfirstname.Replace(",", string.Empty);
                }
            }
            if (ds != null && ds.Tables[1] != null)
            {
                string path = getApplicationSetting("PatientInfoSaveFilePath");
                string OpenFilepath = getApplicationSetting("PatientInfoOpenFilePath");
                path = path + "PatientDeskNotes/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string newPdfFilename = szfirstname.Trim() + "_Visit_Information_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf";
                string pdfPath = path + newPdfFilename;
                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 20);
                float[] wBase = { 4f };
                PdfPTable tblBase = new PdfPTable(wBase);
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.WidthPercentage = 100;
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                #region "for printed by"
                float[] width = { 4f, 4f };
                PdfPTable tblprintby = new PdfPTable(width);
                tblprintby.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblprintby.DefaultCell.Border = Rectangle.NO_BORDER;
                tblprintby.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tblprintby.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblprintby.AddCell(new Phrase("Printed By : " + p_oUser.UserName, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                tblprintby.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tblprintby.AddCell(new Phrase("Printed on : " + DateTime.Now.ToString("MM/dd/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(tblprintby);
                #endregion
                tblBase.AddCell(" ");

                #region "for patient information"
                float[] wdh = { 4f };
                PdfPTable tblheading = new PdfPTable(wdh);
                tblheading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblheading.DefaultCell.Border = Rectangle.NO_BORDER;
                tblheading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblheading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tblBase.AddCell(tblheading);
                #endregion

                #region for Personal Information
                float[] w11 = { 3f, 3f, 3f, 3f };
                PdfPTable table = new PdfPTable(w11);
                table.WidthPercentage = 100;
                table.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell1 = new PdfPCell(new Phrase("Personal Information", iTextSharp.text.FontFactory.GetFont("Arial", 11, iTextSharp.text.Color.BLACK)));
                cell1.Colspan = 4;
                cell1.BackgroundColor = Color.LIGHT_GRAY;
                cell1.BorderColor = Color.BLACK;
                table.AddCell(cell1);
                table.AddCell(new Phrase("Patient Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["PatientName"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["PatientName"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Case #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["CaseNo"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Insurance Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                }

                PdfPCell cell2 = new PdfPCell(new Phrase(""));
                cell2.Colspan = 2;
                cell2.BorderColor = Color.BLACK;
                table.AddCell(cell2);
                tblBase.AddCell(table);
                #endregion
                document.Add(tblBase);
                DataTable dt = ds.Tables[1];
                string[] columns = { "SpecialtyID" };
                DataTable dataTable = GetDistinctRecords(dt, columns);
                DataSet dsFiltered = new DataSet();
                dsFiltered.Tables.Add(dataTable);

                #region for Visit Information
                float[] wd1 = { 3f, 1.5f, 1.5f, 1.5f, 1.5f, 6f, 1.5f, 3f };
                PdfPTable tblVisit = new PdfPTable(wd1);
                tblVisit.WidthPercentage = 100;
                tblVisit.DefaultCell.BorderColor = Color.BLACK;
                tblVisit = GetTableHeader(wd1);

                float[] wdBlank = { 4f };
                PdfPTable tblBlank = new PdfPTable(wdBlank);
                tblBlank.WidthPercentage = 100;
                tblBlank.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBlank.AddCell("");
                tblBlank.DefaultCell.Border = Rectangle.NO_BORDER;

                float[] wdNotes = { .5f, 3.5f };
                PdfPTable tblNotes = new PdfPTable(wdNotes);
                tblNotes.WidthPercentage = 100;
                string text = "";

                for (int i = 0; i < dsFiltered.Tables[0].Rows.Count; i++)
                {
                    DataRow[] result = ds.Tables[1].Select("SpecialtyID = '" + dsFiltered.Tables[0].Rows[i]["SpecialtyID"].ToString() + "'");
                    for (int j = 0; j < result.Length; j++)
                    {
                        float fPosition = writer.GetVerticalPosition(true);
                        if (result[j]["DctorName"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["DctorName"].ToString()), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        if (result[j]["DT_EVENT_DATE"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["DT_EVENT_DATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }

                        if (result[j]["Specialty"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["Specialty"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }

                        if (result[j]["STATUS"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["STATUS"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        if (result[j]["VisitType"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["VisitType"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        if (result[j]["ProcedureCode"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["ProcedureCode"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }

                        if (result[j]["BillStatus"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["BillStatus"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        if (result[j]["Provider"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(result[j]["Provider"]), iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                        }
                    }

                    for (int k = 0; k < oList.Count; k++)
                    {
                        if (dsFiltered.Tables[0].Rows[i]["SpecialtyID"].ToString() == oList[k].Speciality.ID.ToString())
                        {
                            text = oList[k].Text.ToString();
                            tblNotes.FlushContent();
                            tblNotes.AddCell(new Phrase("NOTE:", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                            tblNotes.AddCell(new Phrase(text, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Color.BLACK)));
                            document.Add(tblBlank);

                            if (text != "")
                            {
                                document.Add(tblNotes);
                            }
                            break;
                        }
                    }

                    document.Add(tblVisit);
                    document.Add(tblBlank);
                    document.Add(tblBlank);
                    tblVisit.DeleteBodyRows();
                    tblVisit = GetTableHeader(wd1);
                }

                #endregion
                document.Close();
                System.IO.File.WriteAllBytes(pdfPath, m.GetBuffer());
                OpenPdfFilepath = OpenFilepath + "PatientDeskNotes/" + newPdfFilename;
            }

            return OpenPdfFilepath;
        }

        private PdfPTable GetTableHeader(float[] wd1)
        {
            PdfPTable tblHeader = new PdfPTable(wd1);
            tblHeader.WidthPercentage = 100;
            PdfPCell cell4 = new PdfPCell(new Phrase("Visit History", iTextSharp.text.FontFactory.GetFont("Arial", 11, iTextSharp.text.Color.BLACK)));
            cell4.Colspan = 8;
            cell4.BorderColor = Color.BLACK;
            cell4.BackgroundColor = Color.LIGHT_GRAY;
            tblHeader.AddCell(cell4);
            tblHeader.AddCell(new Phrase("Doctor", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Visit Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Specialty", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Status", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Visit Type", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Procedure Code", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Bill Status", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Provider", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            return tblHeader;
        }

        public DataSet GetVisitInfo(int caseID, string companyID)
        {

            DataSet ds = new DataSet();
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_get_visit_information", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

                sqlda.Fill(ds);

            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
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

        public string getApplicationSetting(String p_szKey)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
            myConn.Open();
            String szParamValue = "";

            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", myConn);
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }
            return szParamValue;
        }

        public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqueRecords = new DataTable();
            dtUniqueRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqueRecords;
        }
    }
}
