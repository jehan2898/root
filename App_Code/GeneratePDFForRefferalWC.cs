using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public class GeneratePDFForRefferalWC
{
    private string strConn;

    private SqlConnection conn;

    private SqlCommand comm;

    private SqlDataReader dr;

    public GeneratePDFForRefferalWC()
    {
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }


    public object GenerateC4AMRLessThen4(string pdfFile, string CompnayId, string CaseId, string BillId, string CompanyName)

    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;


            DataSet value = this.GetValue(BillId);
            DataSet dsProcValue = value;


            PdfReader reader = new PdfReader(pdfFile);
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create), '4');
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            if (value != null)
            {
                if (value.Tables[0] != null)
                {
                    //PATIENT INFORMATION
                    acroFields.SetField("Text1", value.Tables[0].Rows[0]["Text1"].ToString());
                    acroFields.SetField("Text2", value.Tables[0].Rows[0]["Text2"].ToString());
                    acroFields.SetField("Text3", value.Tables[0].Rows[0]["Text3"].ToString());
                    acroFields.SetField("Text4", value.Tables[0].Rows[0]["Text4"].ToString());
                    acroFields.SetField("Text4_1", value.Tables[0].Rows[0]["Text4_1"].ToString());
                    acroFields.SetField("Text4_2", value.Tables[0].Rows[0]["Text4_2"].ToString());
                    acroFields.SetField("Text5", value.Tables[0].Rows[0]["Text5"].ToString());
                    acroFields.SetField("Text8", value.Tables[0].Rows[0]["Text8"].ToString());
                    acroFields.SetField("Text9", value.Tables[0].Rows[0]["Text9"].ToString());
                    acroFields.SetField("Text11", value.Tables[0].Rows[0]["Text11"].ToString());
                    acroFields.SetField("Text7", value.Tables[0].Rows[0]["Text7"].ToString());
                    acroFields.SetField("Text7_1", value.Tables[0].Rows[0]["Text7_1"].ToString());
                    acroFields.SetField("Text10", value.Tables[0].Rows[0]["Text10"].ToString());
                    acroFields.SetField("Text10_1", value.Tables[0].Rows[0]["Text10_1"].ToString());
                    acroFields.SetField("Text10_2", value.Tables[0].Rows[0]["Text10_2"].ToString());

                    if (value.Tables[0].Rows[0]["Text12"].ToString() != "0")
                    {
                        acroFields.SetField("Text12", value.Tables[0].Rows[0]["Text12"].ToString());
                    }
                    if (value.Tables[0].Rows[0]["Text12_1"].ToString() != "0")
                    {
                        acroFields.SetField("Text12_1", value.Tables[0].Rows[0]["Text12_1"].ToString());
                    }
                    if (value.Tables[0].Rows[0]["Text12_2"].ToString() != "0")
                    {
                        acroFields.SetField("Text12_2", value.Tables[0].Rows[0]["Text12_2"].ToString());
                    }

                    acroFields.SetField("Text14", value.Tables[0].Rows[0]["Text14"].ToString());
                    acroFields.SetField("Text15", value.Tables[0].Rows[0]["Text15"].ToString());
                    acroFields.SetField("Text16", value.Tables[0].Rows[0]["Text16"].ToString());

                    //DOCTOR INFORMATION
                    acroFields.SetField("Text17", value.Tables[0].Rows[0]["Text17"].ToString());
                    acroFields.SetField("Text18", value.Tables[0].Rows[0]["Text18"].ToString());
                    acroFields.SetField("Text19", value.Tables[0].Rows[0]["Text19"].ToString());
                    acroFields.SetField("Text20", value.Tables[0].Rows[0]["Text20"].ToString());
                    acroFields.SetField("Check Box103", value.Tables[0].Rows[0]["Check Box103"].ToString());
                    acroFields.SetField("Check Box104", value.Tables[0].Rows[0]["Check Box104"].ToString());
                    acroFields.SetField("Text21", dsProcValue.Tables[0].Rows[0]["Text21"].ToString());
                    acroFields.SetField("Text21_1", dsProcValue.Tables[0].Rows[0]["Text21_1"].ToString());
                    acroFields.SetField("Text21_2", dsProcValue.Tables[0].Rows[0]["Text21_2"].ToString());
                    acroFields.SetField("Text21_3", dsProcValue.Tables[0].Rows[0]["Text21_3"].ToString());
                    acroFields.SetField("Text22", dsProcValue.Tables[0].Rows[0]["Text22"].ToString());
                    acroFields.SetField("Text23", dsProcValue.Tables[0].Rows[0]["Text23"].ToString());
                    acroFields.SetField("Text37", dsProcValue.Tables[0].Rows[0]["Text37"].ToString());
                    acroFields.SetField("Text35", dsProcValue.Tables[0].Rows[0]["Text35"].ToString());
                    acroFields.SetField("Text34", dsProcValue.Tables[0].Rows[0]["Text34"].ToString());
                    acroFields.SetField("Text25", dsProcValue.Tables[0].Rows[0]["Text25"].ToString());
                    acroFields.SetField("Text25_1", dsProcValue.Tables[0].Rows[0]["Text25_1"].ToString());
                    acroFields.SetField("Text27", dsProcValue.Tables[0].Rows[0]["Text27"].ToString());
                    acroFields.SetField("Text27_1", dsProcValue.Tables[0].Rows[0]["Text27_1"].ToString());
                    acroFields.SetField("Text28", dsProcValue.Tables[0].Rows[0]["Text28"].ToString());
                    acroFields.SetField("Text29", dsProcValue.Tables[0].Rows[0]["Text29"].ToString());

                    //BILLING INFORMATION
                    acroFields.SetField("Text30", dsProcValue.Tables[0].Rows[0]["Text30"].ToString());
                    acroFields.SetField("Text31", dsProcValue.Tables[0].Rows[0]["Text31"].ToString());
                    acroFields.SetField("Text32", dsProcValue.Tables[0].Rows[0]["Text32"].ToString());
                    if (dsProcValue.Tables[0].Rows[0]["Text33"].ToString().Trim() != string.Empty)
                    {

                        string[] diag = dsProcValue.Tables[0].Rows[0]["Text33"].ToString().Trim().Split(new string[] { "||" }, StringSplitOptions.None);
                        for (int x = 0; x < diag.Length; x++)
                        {
                            string[] details = diag[x].Split(new string[] { "@#@" }, StringSplitOptions.None);
                            acroFields.SetField("diagc" + (x + 1).ToString(), details[0]);
                            acroFields.SetField("diagd" + (x + 1).ToString(), details[1]);

                        }

                    }
                    acroFields.SetField("Text103", dsProcValue.Tables[0].Rows[0]["Text103"].ToString());
                    acroFields.SetField("Text104", dsProcValue.Tables[0].Rows[0]["Text104"].ToString());
                    acroFields.SetField("Text105", dsProcValue.Tables[0].Rows[0]["Text105"].ToString());

                    //1 diagnosis
                    acroFields.SetField("Text39", dsProcValue.Tables[0].Rows[0]["Text39"].ToString());
                    acroFields.SetField("Text40", dsProcValue.Tables[0].Rows[0]["Text40"].ToString());
                    acroFields.SetField("Text41", dsProcValue.Tables[0].Rows[0]["Text41"].ToString());
                    acroFields.SetField("Text42", dsProcValue.Tables[0].Rows[0]["Text42"].ToString());
                    acroFields.SetField("Text43", dsProcValue.Tables[0].Rows[0]["Text43"].ToString());
                    acroFields.SetField("Text44", dsProcValue.Tables[0].Rows[0]["Text44"].ToString());
                    acroFields.SetField("Text45", dsProcValue.Tables[0].Rows[0]["Text45"].ToString());
                    acroFields.SetField("Text46", dsProcValue.Tables[0].Rows[0]["Text46"].ToString());
                    acroFields.SetField("Text47", dsProcValue.Tables[0].Rows[0]["Text47"].ToString());
                    acroFields.SetField("Text48", dsProcValue.Tables[0].Rows[0]["Text48"].ToString());
                    acroFields.SetField("Text49", dsProcValue.Tables[0].Rows[0]["Text49"].ToString());
                    acroFields.SetField("Text50", dsProcValue.Tables[0].Rows[0]["Text50"].ToString());
                    // acroFields.SetField("Text51", dsProcValue.Tables[0].Rows[0]["Text51"].ToString());
                    acroFields.SetField("Text52", dsProcValue.Tables[0].Rows[0]["Text52"].ToString());

                    //2 diagnosis
                    acroFields.SetField("Text53", dsProcValue.Tables[0].Rows[0]["Text53"].ToString());
                    acroFields.SetField("Text54", dsProcValue.Tables[0].Rows[0]["Text54"].ToString());
                    acroFields.SetField("Text55", dsProcValue.Tables[0].Rows[0]["Text55"].ToString());
                    acroFields.SetField("Text56", dsProcValue.Tables[0].Rows[0]["Text56"].ToString());
                    acroFields.SetField("Text57", dsProcValue.Tables[0].Rows[0]["Text57"].ToString());
                    acroFields.SetField("Text58", dsProcValue.Tables[0].Rows[0]["Text58"].ToString());
                    acroFields.SetField("Text59", dsProcValue.Tables[0].Rows[0]["Text59"].ToString());
                    acroFields.SetField("Text60", dsProcValue.Tables[0].Rows[0]["Text60"].ToString());
                    acroFields.SetField("Text61", dsProcValue.Tables[0].Rows[0]["Text61"].ToString());
                    acroFields.SetField("Text62", dsProcValue.Tables[0].Rows[0]["Text62"].ToString());
                    acroFields.SetField("Text63", dsProcValue.Tables[0].Rows[0]["Text63"].ToString());
                    acroFields.SetField("Text64", dsProcValue.Tables[0].Rows[0]["Text64"].ToString());
                    //acroFields.SetField("Text65", dsProcValue.Tables[0].Rows[0]["Text65"].ToString());
                    acroFields.SetField("Text66", dsProcValue.Tables[0].Rows[0]["Text66"].ToString());


                    //3 diagnosis
                    acroFields.SetField("Text67", dsProcValue.Tables[0].Rows[0]["Text67"].ToString());
                    acroFields.SetField("Text68", dsProcValue.Tables[0].Rows[0]["Text68"].ToString());
                    acroFields.SetField("Text69", dsProcValue.Tables[0].Rows[0]["Text69"].ToString());
                    acroFields.SetField("Text70", dsProcValue.Tables[0].Rows[0]["Text70"].ToString());
                    acroFields.SetField("Text71", dsProcValue.Tables[0].Rows[0]["Text71"].ToString());
                    acroFields.SetField("Text72", dsProcValue.Tables[0].Rows[0]["Text72"].ToString());
                    acroFields.SetField("Text73", dsProcValue.Tables[0].Rows[0]["Text73"].ToString());
                    acroFields.SetField("Text74", dsProcValue.Tables[0].Rows[0]["Text74"].ToString());
                    acroFields.SetField("Text75", dsProcValue.Tables[0].Rows[0]["Text75"].ToString());
                    acroFields.SetField("Text76", dsProcValue.Tables[0].Rows[0]["Text76"].ToString());
                    acroFields.SetField("Text77", dsProcValue.Tables[0].Rows[0]["Text77"].ToString());
                    acroFields.SetField("Text78", dsProcValue.Tables[0].Rows[0]["Text78"].ToString());
                    //acroFields.SetField("Text79", dsProcValue.Tables[0].Rows[0]["Text79"].ToString());
                    acroFields.SetField("Text80", dsProcValue.Tables[0].Rows[0]["Text80"].ToString());

                    //4 diagnosis
                    acroFields.SetField("Text81", dsProcValue.Tables[0].Rows[0]["Text81"].ToString());
                    acroFields.SetField("Text82", dsProcValue.Tables[0].Rows[0]["Text82"].ToString());
                    acroFields.SetField("Text83", dsProcValue.Tables[0].Rows[0]["Text83"].ToString());
                    acroFields.SetField("Text84", dsProcValue.Tables[0].Rows[0]["Text84"].ToString());
                    acroFields.SetField("Text85", dsProcValue.Tables[0].Rows[0]["Text85"].ToString());
                    acroFields.SetField("Text86", dsProcValue.Tables[0].Rows[0]["Text86"].ToString());
                    acroFields.SetField("Text87", dsProcValue.Tables[0].Rows[0]["Text87"].ToString());
                    acroFields.SetField("Text88", dsProcValue.Tables[0].Rows[0]["Text88"].ToString());
                    acroFields.SetField("Text89", dsProcValue.Tables[0].Rows[0]["Text89"].ToString());
                    acroFields.SetField("Text90", dsProcValue.Tables[0].Rows[0]["Text90"].ToString());
                    acroFields.SetField("Text91", dsProcValue.Tables[0].Rows[0]["Text91"].ToString());
                    acroFields.SetField("Text92", dsProcValue.Tables[0].Rows[0]["Text92"].ToString());
                    //acroFields.SetField("Text93", dsProcValue.Tables[0].Rows[0]["Text93"].ToString());
                    acroFields.SetField("Text94", dsProcValue.Tables[0].Rows[0]["Text94"].ToString());

                    acroFields.SetField("Text95", dsProcValue.Tables[0].Rows[0]["Text95"].ToString());
                    acroFields.SetField("Paid_Amount", dsProcValue.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                    acroFields.SetField("Balance", dsProcValue.Tables[0].Rows[0]["BALANCE"].ToString());


                    acroFields.SetField("Check Box106", value.Tables[0].Rows[0]["Check Box106"].ToString());

                    acroFields.SetField("Check Box107", value.Tables[0].Rows[0]["Check Box107"].ToString());


                    acroFields.SetField("Text96", value.Tables[0].Rows[0]["Text96"].ToString());
                    acroFields.SetField("Text97", value.Tables[0].Rows[0]["Text97"].ToString());
                    acroFields.SetField("Text98", value.Tables[0].Rows[0]["Text98"].ToString());
                    acroFields.SetField("Text99", value.Tables[0].Rows[0]["Text99"].ToString());
                    acroFields.SetField("Text100", value.Tables[0].Rows[0]["Text100"].ToString());
                    acroFields.SetField("Text100_1", value.Tables[0].Rows[0]["Text100_1"].ToString());
                    acroFields.SetField("Text100_2", value.Tables[0].Rows[0]["Text100_2"].ToString());


                    acroFields.SetField("Check Box105", value.Tables[0].Rows[0]["Check Box105"].ToString());

                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                }
            }

        }
        catch (Exception ex)
        {

        }
        return bill_Sys_Data;
    }
    public object GenerateC4AMRGreaterThen4(string pdfFile, string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;

            DataSet value = this.GetValueGt4(BillId);
            DataSet dsProcValue = value;

            PdfReader reader = new PdfReader(pdfFile);
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create), '4');
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            if (value != null)
            {
                if (value.Tables[0] != null)
                {
                    //PATIENT INFORMATION
                    acroFields.SetField("Text1", value.Tables[0].Rows[0]["Text1"].ToString());
                    acroFields.SetField("Text2", value.Tables[0].Rows[0]["Text2"].ToString());
                    acroFields.SetField("Text3", value.Tables[0].Rows[0]["Text3"].ToString());
                    acroFields.SetField("Text4", value.Tables[0].Rows[0]["Text4"].ToString());
                    acroFields.SetField("Text4_1", value.Tables[0].Rows[0]["Text4_1"].ToString());
                    acroFields.SetField("Text4_2", value.Tables[0].Rows[0]["Text4_2"].ToString());
                    acroFields.SetField("Text5", value.Tables[0].Rows[0]["Text5"].ToString());
                    acroFields.SetField("Text8", value.Tables[0].Rows[0]["Text8"].ToString());
                    acroFields.SetField("Text9", value.Tables[0].Rows[0]["Text9"].ToString());
                    acroFields.SetField("Text11", value.Tables[0].Rows[0]["Text11"].ToString());
                    acroFields.SetField("Text7", value.Tables[0].Rows[0]["Text7"].ToString());
                    acroFields.SetField("Text7_1", value.Tables[0].Rows[0]["Text7_1"].ToString());
                    acroFields.SetField("Text10", value.Tables[0].Rows[0]["Text10"].ToString());
                    acroFields.SetField("Text10_1", value.Tables[0].Rows[0]["Text10_1"].ToString());
                    acroFields.SetField("Text10_2", value.Tables[0].Rows[0]["Text10_2"].ToString());

                    if (value.Tables[0].Rows[0]["Text12"].ToString() != "0")
                    {
                        acroFields.SetField("Text12", value.Tables[0].Rows[0]["Text12"].ToString());
                    }
                    if (value.Tables[0].Rows[0]["Text12_1"].ToString() != "0")
                    {
                        acroFields.SetField("Text12_1", value.Tables[0].Rows[0]["Text12_1"].ToString());
                    }
                    if (value.Tables[0].Rows[0]["Text12_2"].ToString() != "0")
                    {
                        acroFields.SetField("Text12_2", value.Tables[0].Rows[0]["Text12_2"].ToString());
                    }

                    acroFields.SetField("Text14", value.Tables[0].Rows[0]["Text14"].ToString());
                    acroFields.SetField("Text15", value.Tables[0].Rows[0]["Text15"].ToString());
                    acroFields.SetField("Text16", value.Tables[0].Rows[0]["Text16"].ToString());

                    //DOCTOR INFORMATION
                    acroFields.SetField("Text17", value.Tables[0].Rows[0]["Text17"].ToString());
                    acroFields.SetField("Text18", value.Tables[0].Rows[0]["Text18"].ToString());
                    acroFields.SetField("Text19", value.Tables[0].Rows[0]["Text19"].ToString());
                    acroFields.SetField("Text20", value.Tables[0].Rows[0]["Text20"].ToString());
                    acroFields.SetField("Check Box103", value.Tables[0].Rows[0]["Check Box103"].ToString());
                    acroFields.SetField("Check Box104", value.Tables[0].Rows[0]["Check Box104"].ToString());
                    acroFields.SetField("Text21", dsProcValue.Tables[0].Rows[0]["Text21"].ToString());
                    acroFields.SetField("Text21_1", dsProcValue.Tables[0].Rows[0]["Text21_1"].ToString());
                    acroFields.SetField("Text21_2", dsProcValue.Tables[0].Rows[0]["Text21_2"].ToString());
                    acroFields.SetField("Text21_3", dsProcValue.Tables[0].Rows[0]["Text21_3"].ToString());
                    acroFields.SetField("Text22", dsProcValue.Tables[0].Rows[0]["Text22"].ToString());
                    acroFields.SetField("Text23", dsProcValue.Tables[0].Rows[0]["Text23"].ToString());
                    acroFields.SetField("Text37", dsProcValue.Tables[0].Rows[0]["Text37"].ToString());
                    acroFields.SetField("Text35", dsProcValue.Tables[0].Rows[0]["Text35"].ToString());
                    acroFields.SetField("Text34", dsProcValue.Tables[0].Rows[0]["Text34"].ToString());
                    acroFields.SetField("Text25", dsProcValue.Tables[0].Rows[0]["Text25"].ToString());
                    acroFields.SetField("Text25_1", dsProcValue.Tables[0].Rows[0]["Text25_1"].ToString());
                    acroFields.SetField("Text27", dsProcValue.Tables[0].Rows[0]["Text27"].ToString());
                    acroFields.SetField("Text27_1", dsProcValue.Tables[0].Rows[0]["Text27_1"].ToString());
                    acroFields.SetField("Text28", dsProcValue.Tables[0].Rows[0]["Text28"].ToString());
                    acroFields.SetField("Text29", dsProcValue.Tables[0].Rows[0]["Text29"].ToString());

                    //BILLING INFORMATION
                    acroFields.SetField("Text30", dsProcValue.Tables[0].Rows[0]["Text30"].ToString());
                    acroFields.SetField("Text31", dsProcValue.Tables[0].Rows[0]["Text31"].ToString());
                    acroFields.SetField("Text32", dsProcValue.Tables[0].Rows[0]["Text32"].ToString());
                    if (dsProcValue.Tables[0].Rows[0]["Text33"].ToString().Trim() != string.Empty)
                    {

                        string[] diag = dsProcValue.Tables[0].Rows[0]["Text33"].ToString().Trim().Split(new string[] { "||" }, StringSplitOptions.None);
                        for (int x = 0; x < diag.Length; x++)
                        {
                            string[] details = diag[x].Split(new string[] { "@#@" }, StringSplitOptions.None);
                            acroFields.SetField("diagc" + (x + 1).ToString(), details[0]);
                            acroFields.SetField("diagd" + (x + 1).ToString(), details[1]);

                        }

                    }
                    acroFields.SetField("Text103", dsProcValue.Tables[0].Rows[0]["Text103"].ToString());
                    acroFields.SetField("Text104", dsProcValue.Tables[0].Rows[0]["Text104"].ToString());
                    acroFields.SetField("Text105", dsProcValue.Tables[0].Rows[0]["Text105"].ToString());

                    //1 diagnosis
                    acroFields.SetField("Text39", dsProcValue.Tables[0].Rows[0]["Text39"].ToString());
                    acroFields.SetField("Text40", dsProcValue.Tables[0].Rows[0]["Text40"].ToString());
                    acroFields.SetField("Text41", dsProcValue.Tables[0].Rows[0]["Text41"].ToString());
                    acroFields.SetField("Text42", dsProcValue.Tables[0].Rows[0]["Text42"].ToString());
                    acroFields.SetField("Text43", dsProcValue.Tables[0].Rows[0]["Text43"].ToString());
                    acroFields.SetField("Text44", dsProcValue.Tables[0].Rows[0]["Text44"].ToString());
                    acroFields.SetField("Text45", dsProcValue.Tables[0].Rows[0]["Text45"].ToString());
                    acroFields.SetField("Text46", dsProcValue.Tables[0].Rows[0]["Text46"].ToString());
                    acroFields.SetField("Text47", dsProcValue.Tables[0].Rows[0]["Text47"].ToString());
                    acroFields.SetField("Text48", dsProcValue.Tables[0].Rows[0]["Text48"].ToString());
                    acroFields.SetField("Text49", dsProcValue.Tables[0].Rows[0]["Text49"].ToString());
                    acroFields.SetField("Text50", dsProcValue.Tables[0].Rows[0]["Text50"].ToString());
                    // acroFields.SetField("Text51", dsProcValue.Tables[0].Rows[0]["Text51"].ToString());
                    acroFields.SetField("Text52", dsProcValue.Tables[0].Rows[0]["Text52"].ToString());

                    //2 diagnosis
                    acroFields.SetField("Text53", dsProcValue.Tables[0].Rows[0]["Text53"].ToString());
                    acroFields.SetField("Text54", dsProcValue.Tables[0].Rows[0]["Text54"].ToString());
                    acroFields.SetField("Text55", dsProcValue.Tables[0].Rows[0]["Text55"].ToString());
                    acroFields.SetField("Text56", dsProcValue.Tables[0].Rows[0]["Text56"].ToString());
                    acroFields.SetField("Text57", dsProcValue.Tables[0].Rows[0]["Text57"].ToString());
                    acroFields.SetField("Text58", dsProcValue.Tables[0].Rows[0]["Text58"].ToString());
                    acroFields.SetField("Text59", dsProcValue.Tables[0].Rows[0]["Text59"].ToString());
                    acroFields.SetField("Text60", dsProcValue.Tables[0].Rows[0]["Text60"].ToString());
                    acroFields.SetField("Text61", dsProcValue.Tables[0].Rows[0]["Text61"].ToString());
                    acroFields.SetField("Text62", dsProcValue.Tables[0].Rows[0]["Text62"].ToString());
                    acroFields.SetField("Text63", dsProcValue.Tables[0].Rows[0]["Text63"].ToString());
                    acroFields.SetField("Text64", dsProcValue.Tables[0].Rows[0]["Text64"].ToString());
                    //acroFields.SetField("Text65", dsProcValue.Tables[0].Rows[0]["Text65"].ToString());
                    acroFields.SetField("Text66", dsProcValue.Tables[0].Rows[0]["Text66"].ToString());


                    //3 diagnosis
                    acroFields.SetField("Text67", dsProcValue.Tables[0].Rows[0]["Text67"].ToString());
                    acroFields.SetField("Text68", dsProcValue.Tables[0].Rows[0]["Text68"].ToString());
                    acroFields.SetField("Text69", dsProcValue.Tables[0].Rows[0]["Text69"].ToString());
                    acroFields.SetField("Text70", dsProcValue.Tables[0].Rows[0]["Text70"].ToString());
                    acroFields.SetField("Text71", dsProcValue.Tables[0].Rows[0]["Text71"].ToString());
                    acroFields.SetField("Text72", dsProcValue.Tables[0].Rows[0]["Text72"].ToString());
                    acroFields.SetField("Text73", dsProcValue.Tables[0].Rows[0]["Text73"].ToString());
                    acroFields.SetField("Text74", dsProcValue.Tables[0].Rows[0]["Text74"].ToString());
                    acroFields.SetField("Text75", dsProcValue.Tables[0].Rows[0]["Text75"].ToString());
                    acroFields.SetField("Text76", dsProcValue.Tables[0].Rows[0]["Text76"].ToString());
                    acroFields.SetField("Text77", dsProcValue.Tables[0].Rows[0]["Text77"].ToString());
                    acroFields.SetField("Text78", dsProcValue.Tables[0].Rows[0]["Text78"].ToString());
                    //acroFields.SetField("Text79", dsProcValue.Tables[0].Rows[0]["Text79"].ToString());
                    acroFields.SetField("Text80", dsProcValue.Tables[0].Rows[0]["Text80"].ToString());

                    //4 diagnosis
                    acroFields.SetField("Text81", dsProcValue.Tables[0].Rows[0]["Text81"].ToString());
                    acroFields.SetField("Text82", dsProcValue.Tables[0].Rows[0]["Text82"].ToString());
                    acroFields.SetField("Text83", dsProcValue.Tables[0].Rows[0]["Text83"].ToString());
                    acroFields.SetField("Text84", dsProcValue.Tables[0].Rows[0]["Text84"].ToString());
                    acroFields.SetField("Text85", dsProcValue.Tables[0].Rows[0]["Text85"].ToString());
                    acroFields.SetField("Text86", dsProcValue.Tables[0].Rows[0]["Text86"].ToString());
                    acroFields.SetField("Text87", dsProcValue.Tables[0].Rows[0]["Text87"].ToString());
                    acroFields.SetField("Text88", dsProcValue.Tables[0].Rows[0]["Text88"].ToString());
                    acroFields.SetField("Text89", dsProcValue.Tables[0].Rows[0]["Text89"].ToString());
                    acroFields.SetField("Text90", dsProcValue.Tables[0].Rows[0]["Text90"].ToString());
                    acroFields.SetField("Text91", dsProcValue.Tables[0].Rows[0]["Text91"].ToString());
                    acroFields.SetField("Text92", dsProcValue.Tables[0].Rows[0]["Text92"].ToString());
                    //acroFields.SetField("Text93", dsProcValue.Tables[0].Rows[0]["Text93"].ToString());
                    acroFields.SetField("Text94", dsProcValue.Tables[0].Rows[0]["Text94"].ToString());

                    acroFields.SetField("Text95", dsProcValue.Tables[0].Rows[0]["Text95"].ToString());
                    //acroFields.SetField("Paid_Amount", dsProcValue.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                    //acroFields.SetField("Balance", dsProcValue.Tables[0].Rows[0]["BALANCE"].ToString());


                    acroFields.SetField("Check Box106", value.Tables[0].Rows[0]["Check Box106"].ToString());

                    acroFields.SetField("Check Box107", value.Tables[0].Rows[0]["Check Box107"].ToString());


                    acroFields.SetField("Text96", value.Tables[0].Rows[0]["Text96"].ToString());
                    acroFields.SetField("Text97", value.Tables[0].Rows[0]["Text97"].ToString());
                    acroFields.SetField("Text98", value.Tables[0].Rows[0]["Text98"].ToString());
                    acroFields.SetField("Text99", value.Tables[0].Rows[0]["Text99"].ToString());
                    acroFields.SetField("Text100", value.Tables[0].Rows[0]["Text100"].ToString());
                    acroFields.SetField("Text100_1", value.Tables[0].Rows[0]["Text100_1"].ToString());
                    acroFields.SetField("Text100_2", value.Tables[0].Rows[0]["Text100_2"].ToString());


                    acroFields.SetField("Check Box105", value.Tables[0].Rows[0]["Check Box105"].ToString());

                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                }
            }


        }
        catch (Exception ex)
        {

        }
        return bill_Sys_Data;
    }
    private string getSubFileName(int pdfCount)
    {
        int i_PdfCount = 0;
        i_PdfCount = pdfCount;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = pdfCount.ToString() + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }

    public void PdfPrint(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        string text = this.getFileName(BillId) + ".pdf";
        string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;

        DataSet value = this.GetValue(BillId);
        DataSet dsProcValue = this.GetValue(BillId);

        PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["WCReferal_PDF_FILE"].ToString());
        Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
        PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create), '4');
        AcroFields acroFields = pdfStamper.AcroFields;
        document.Open();
        if (value != null)
        {
            if (value.Tables[0] != null)
            {
                //PATIENT INFORMATION
                acroFields.SetField("Text1", value.Tables[0].Rows[0]["Text1"].ToString());
                acroFields.SetField("Text2", value.Tables[0].Rows[0]["Text2"].ToString());
                acroFields.SetField("Text3", value.Tables[0].Rows[0]["Text3"].ToString());
                acroFields.SetField("Text4", value.Tables[0].Rows[0]["Text4"].ToString());
                acroFields.SetField("Text5", value.Tables[0].Rows[0]["Text5"].ToString());
                acroFields.SetField("Text8", value.Tables[0].Rows[0]["Text8"].ToString());
                acroFields.SetField("Text9", value.Tables[0].Rows[0]["Text9"].ToString());
                acroFields.SetField("Text11", value.Tables[0].Rows[0]["Text11"].ToString());
                acroFields.SetField("Text7", value.Tables[0].Rows[0]["Text7"].ToString());
                acroFields.SetField("Text7_1", value.Tables[0].Rows[0]["Text7_1"].ToString());
                acroFields.SetField("Text10", value.Tables[0].Rows[0]["Text10"].ToString());
                acroFields.SetField("Text10_1", value.Tables[0].Rows[0]["Text10_1"].ToString());
                acroFields.SetField("Text10_2", value.Tables[0].Rows[0]["Text10_2"].ToString());

                if (value.Tables[0].Rows[0]["Text12"].ToString() != "0")
                {
                    acroFields.SetField("Text12", value.Tables[0].Rows[0]["Text12"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text12_1"].ToString() != "0")
                {
                    acroFields.SetField("Text12_1", value.Tables[0].Rows[0]["Text12_1"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text12_2"].ToString() != "0")
                {
                    acroFields.SetField("Text12_2", value.Tables[0].Rows[0]["Text12_2"].ToString());
                }

                acroFields.SetField("Text14", value.Tables[0].Rows[0]["Text14"].ToString());
                acroFields.SetField("Text15", value.Tables[0].Rows[0]["Text15"].ToString());
                acroFields.SetField("Text16", value.Tables[0].Rows[0]["Text16"].ToString());

                //DOCTOR INFORMATION
                acroFields.SetField("Text17", value.Tables[0].Rows[0]["Text17"].ToString());
                acroFields.SetField("Text18", value.Tables[0].Rows[0]["Text18"].ToString());
                acroFields.SetField("Text19", value.Tables[0].Rows[0]["Text19"].ToString());
                acroFields.SetField("Text20", value.Tables[0].Rows[0]["Text20"].ToString());

                if (value.Tables[0].Rows[0]["Check Box103"].ToString() == "0")
                {
                    acroFields.SetField("Check Box103", "On");
                }
                if (value.Tables[0].Rows[0]["Check Box104"].ToString() == "1")
                {
                    acroFields.SetField("Check Box104", "On");
                }

                acroFields.SetField("Text21", dsProcValue.Tables[0].Rows[0]["Text21"].ToString());
                acroFields.SetField("Text21_1", dsProcValue.Tables[0].Rows[0]["Text21_1"].ToString());
                acroFields.SetField("Text21_2", dsProcValue.Tables[0].Rows[0]["Text21_2"].ToString());
                acroFields.SetField("Text21_3", dsProcValue.Tables[0].Rows[0]["Text21_3"].ToString());
                acroFields.SetField("Text22", dsProcValue.Tables[0].Rows[0]["Text22"].ToString());
                acroFields.SetField("Text23", dsProcValue.Tables[0].Rows[0]["Text23"].ToString());
                acroFields.SetField("Text37", dsProcValue.Tables[0].Rows[0]["Text37"].ToString());
                acroFields.SetField("Text35", dsProcValue.Tables[0].Rows[0]["Text35"].ToString());
                acroFields.SetField("Text34", dsProcValue.Tables[0].Rows[0]["Text34"].ToString());
                acroFields.SetField("Text25", dsProcValue.Tables[0].Rows[0]["Text25"].ToString());
                acroFields.SetField("Text27", dsProcValue.Tables[0].Rows[0]["Text27"].ToString());
                acroFields.SetField("Text28", dsProcValue.Tables[0].Rows[0]["Text28"].ToString());
                acroFields.SetField("Text29", dsProcValue.Tables[0].Rows[0]["Text29"].ToString());

                //BILLING INFORMATION
                acroFields.SetField("Text30", dsProcValue.Tables[0].Rows[0]["Text30"].ToString());
                acroFields.SetField("Text31", dsProcValue.Tables[0].Rows[0]["Text31"].ToString());
                acroFields.SetField("Text32", dsProcValue.Tables[0].Rows[0]["Text32"].ToString());
                acroFields.SetField("Text33", dsProcValue.Tables[0].Rows[0]["Text33"].ToString());
                acroFields.SetField("Text103", dsProcValue.Tables[0].Rows[0]["Text103"].ToString());
                acroFields.SetField("Text104", dsProcValue.Tables[0].Rows[0]["Text104"].ToString());
                acroFields.SetField("Text105", dsProcValue.Tables[0].Rows[0]["Text105"].ToString());

                //1 diagnosis
                acroFields.SetField("Text39", dsProcValue.Tables[0].Rows[0]["Text39"].ToString());
                acroFields.SetField("Text40", dsProcValue.Tables[0].Rows[0]["Text40"].ToString());
                acroFields.SetField("Text41", dsProcValue.Tables[0].Rows[0]["Text41"].ToString());
                acroFields.SetField("Text42", dsProcValue.Tables[0].Rows[0]["Text42"].ToString());
                acroFields.SetField("Text43", dsProcValue.Tables[0].Rows[0]["Text43"].ToString());
                acroFields.SetField("Text44", dsProcValue.Tables[0].Rows[0]["Text44"].ToString());
                acroFields.SetField("Text45", dsProcValue.Tables[0].Rows[0]["Text45"].ToString());
                acroFields.SetField("Text46", dsProcValue.Tables[0].Rows[0]["Text46"].ToString());
                acroFields.SetField("Text47", dsProcValue.Tables[0].Rows[0]["Text47"].ToString());
                acroFields.SetField("Text48", dsProcValue.Tables[0].Rows[0]["Text48"].ToString());
                acroFields.SetField("Text49", dsProcValue.Tables[0].Rows[0]["Text49"].ToString());
                acroFields.SetField("Text50", dsProcValue.Tables[0].Rows[0]["Text50"].ToString());

                // acroFields.SetField("Text51", dsProcValue.Tables[0].Rows[0]["Text51"].ToString());

                acroFields.SetField("Text52", dsProcValue.Tables[0].Rows[0]["Text52"].ToString());

                //2 diagnosis
                acroFields.SetField("Text53", dsProcValue.Tables[0].Rows[0]["Text53"].ToString());
                acroFields.SetField("Text54", dsProcValue.Tables[0].Rows[0]["Text54"].ToString());
                acroFields.SetField("Text55", dsProcValue.Tables[0].Rows[0]["Text55"].ToString());
                acroFields.SetField("Text56", dsProcValue.Tables[0].Rows[0]["Text56"].ToString());
                acroFields.SetField("Text57", dsProcValue.Tables[0].Rows[0]["Text57"].ToString());
                acroFields.SetField("Text58", dsProcValue.Tables[0].Rows[0]["Text58"].ToString());
                acroFields.SetField("Text59", dsProcValue.Tables[0].Rows[0]["Text59"].ToString());
                acroFields.SetField("Text60", dsProcValue.Tables[0].Rows[0]["Text60"].ToString());
                acroFields.SetField("Text61", dsProcValue.Tables[0].Rows[0]["Text61"].ToString());
                acroFields.SetField("Text62", dsProcValue.Tables[0].Rows[0]["Text62"].ToString());
                acroFields.SetField("Text63", dsProcValue.Tables[0].Rows[0]["Text63"].ToString());
                acroFields.SetField("Text64", dsProcValue.Tables[0].Rows[0]["Text64"].ToString());
                //acroFields.SetField("Text65", dsProcValue.Tables[0].Rows[0]["Text65"].ToString());
                acroFields.SetField("Text66", dsProcValue.Tables[0].Rows[0]["Text66"].ToString());


                //3 diagnosis
                acroFields.SetField("Text67", dsProcValue.Tables[0].Rows[0]["Text67"].ToString());
                acroFields.SetField("Text68", dsProcValue.Tables[0].Rows[0]["Text68"].ToString());
                acroFields.SetField("Text69", dsProcValue.Tables[0].Rows[0]["Text69"].ToString());
                acroFields.SetField("Text70", dsProcValue.Tables[0].Rows[0]["Text70"].ToString());
                acroFields.SetField("Text71", dsProcValue.Tables[0].Rows[0]["Text71"].ToString());
                acroFields.SetField("Text72", dsProcValue.Tables[0].Rows[0]["Text72"].ToString());
                acroFields.SetField("Text73", dsProcValue.Tables[0].Rows[0]["Text73"].ToString());
                acroFields.SetField("Text74", dsProcValue.Tables[0].Rows[0]["Text74"].ToString());
                acroFields.SetField("Text75", dsProcValue.Tables[0].Rows[0]["Text75"].ToString());
                acroFields.SetField("Text76", dsProcValue.Tables[0].Rows[0]["Text76"].ToString());
                acroFields.SetField("Text77", dsProcValue.Tables[0].Rows[0]["Text77"].ToString());
                acroFields.SetField("Text78", dsProcValue.Tables[0].Rows[0]["Text78"].ToString());
                //acroFields.SetField("Text79", dsProcValue.Tables[0].Rows[0]["Text79"].ToString());
                acroFields.SetField("Text80", dsProcValue.Tables[0].Rows[0]["Text80"].ToString());

                //4 diagnosis
                acroFields.SetField("Text81", dsProcValue.Tables[0].Rows[0]["Text81"].ToString());
                acroFields.SetField("Text82", dsProcValue.Tables[0].Rows[0]["Text82"].ToString());
                acroFields.SetField("Text83", dsProcValue.Tables[0].Rows[0]["Text83"].ToString());
                acroFields.SetField("Text84", dsProcValue.Tables[0].Rows[0]["Text84"].ToString());
                acroFields.SetField("Text85", dsProcValue.Tables[0].Rows[0]["Text85"].ToString());
                acroFields.SetField("Text86", dsProcValue.Tables[0].Rows[0]["Text86"].ToString());
                acroFields.SetField("Text87", dsProcValue.Tables[0].Rows[0]["Text87"].ToString());
                acroFields.SetField("Text88", dsProcValue.Tables[0].Rows[0]["Text88"].ToString());
                acroFields.SetField("Text89", dsProcValue.Tables[0].Rows[0]["Text89"].ToString());
                acroFields.SetField("Text90", dsProcValue.Tables[0].Rows[0]["Text90"].ToString());
                acroFields.SetField("Text91", dsProcValue.Tables[0].Rows[0]["Text91"].ToString());
                acroFields.SetField("Text92", dsProcValue.Tables[0].Rows[0]["Text92"].ToString());
                //acroFields.SetField("Text93", dsProcValue.Tables[0].Rows[0]["Text93"].ToString());
                acroFields.SetField("Text94", dsProcValue.Tables[0].Rows[0]["Text94"].ToString());

                acroFields.SetField("Text95", dsProcValue.Tables[0].Rows[0]["Text95"].ToString());
                acroFields.SetField("Paid_Amount", dsProcValue.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                acroFields.SetField("Balance", dsProcValue.Tables[0].Rows[0]["BALANCE"].ToString());

                if (value.Tables[0].Rows[0]["Check Box106"].ToString() == "1")
                {
                    acroFields.SetField("Check Box106", "On");
                }
                if (value.Tables[0].Rows[0]["Check Box107"].ToString() == "0")
                {
                    acroFields.SetField("Check Box107", "On");
                }

                acroFields.SetField("Text96", value.Tables[0].Rows[0]["Text96"].ToString());
                acroFields.SetField("Text97", value.Tables[0].Rows[0]["Text97"].ToString());
                acroFields.SetField("Text96", value.Tables[0].Rows[0]["Text98"].ToString());
                acroFields.SetField("Text97", value.Tables[0].Rows[0]["Text99"].ToString());
                acroFields.SetField("Text96", value.Tables[0].Rows[0]["Text100"].ToString());

                if (value.Tables[0].Rows[0]["Check Box105"].ToString() == "0")
                {
                    acroFields.SetField("Check Box105", "1");
                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return string.Concat(new string[]
        {
            p_szBillNumber,
            "_",
            this.getRandomNumber(),
            "_",
            now.ToString("yyyyMMddHHmmssms")
        });
    }

    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
        if (Directory.Exists(p_szPath))
        {
            if (!Directory.Exists(p_szPath + "/Packet Document"))
            {
                Directory.CreateDirectory(p_szPath + "/Packet Document");
            }
        }
        else
        {
            Directory.CreateDirectory(p_szPath);
            Directory.CreateDirectory(p_szPath + "/Packet Document");
        }
        return p_szPath + "/Packet Document/";
    }

    public DataSet GetValue(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_WC_REFERAL_BILL", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "ALL");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetValueGt4(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_WC_REFERAL_BILL_FOR_GT_5", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "ALL");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
    }


    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 10000).ToString();
    }

  
}