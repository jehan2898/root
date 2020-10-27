using Aquaforest.PDF.Font;
using Microsoft.VisualBasic.FileIO;
using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquaforest.PDF
{
	public class CSVToPDFConverter
	{
		private string csvFile = "";

		private string delimiter = ",";

		private string outputPDF = "";

		private List<List<string>> contents = new List<List<string>>();

		private PDFPageSettings pageSettings = new PDFPageSettings();

		private PDFTableSettings tableSettings = new PDFTableSettings();

		public CSVToPDFConverter(string sourceCSV)
		{
			this.csvFile = sourceCSV;
			this.Intialise();
		}

		public CSVToPDFConverter(string sourceCSV, string delimiter)
		{
			this.csvFile = sourceCSV;
			this.delimiter = delimiter;
			this.Intialise();
		}

		public void ConvertToPDF(string outputFile)
		{
			this.outputPDF = outputFile;
			this.ParseCSV();
		}

		public void ConvertToPDF(string outputFile, PDFPageSettings pageSettings)
		{
			this.SetPageSettings(pageSettings);
			this.ConvertToPDF(outputFile);
		}

		public void ConvertToPDF(string outputFile, PDFTableSettings tableSettings)
		{
			this.SetTableSettings(tableSettings);
			this.ConvertToPDF(outputFile);
		}

		public void ConvertToPDF(string outputFile, PDFPageSettings pageSettings, PDFTableSettings tableSettings)
		{
			this.SetPageSettings(pageSettings);
			this.SetTableSettings(tableSettings);
			this.ConvertToPDF(outputFile);
		}

		private void DrawTable()
		{
			PDPage pDPage;
			PDDocument pDDocument = new PDDocument();
			pDPage = (this.pageSettings.size != null ? new PDPage(this.pageSettings.size.PDFBoxRectangle) : new PDPage());
			PDRectangle mediaBox = pDPage.getMediaBox();
			int num = this.contents.Count<List<string>>();
			int num1 = this.contents.ElementAt<List<string>>(0).Count<string>();
			float width = mediaBox.getWidth() - (this.pageSettings.marginLeft + this.pageSettings.marginRight);
			float single = width / (float)num1;
			float rowHeight = this.tableSettings.RowHeight;
			float height = mediaBox.getHeight() - this.pageSettings.marginTop;
			float single1 = height - this.pageSettings.marginBottom;
			float single2 = rowHeight * (float)num;
			int num2 = num;
			int num3 = 1;
			int num4 = 0;
			if (single2 > single1)
			{
				num2 = (int)Math.Floor((double)(single1 / rowHeight));
				num3 = (int)Math.Ceiling((double)num / (double)num2);
				single2 = (float)num2 * rowHeight;
				num4 = num % num2;
			}
			int num5 = 0;
			for (int i = 0; i < num3; i++)
			{
				pDPage = (this.pageSettings.size != null ? new PDPage(this.pageSettings.size.PDFBoxRectangle) : new PDPage());
				pDDocument.addPage(pDPage);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(pDDocument, pDPage);
				if ((i != num3 - 1 ? false : num4 > 0))
				{
					num2 = num4;
					single2 = rowHeight * (float)num2;
				}
				float single3 = height;
				for (int j = 0; j <= num2; j++)
				{
					pDPageContentStream.drawLine(this.pageSettings.marginLeft, single3, this.pageSettings.marginLeft + width, single3);
					single3 = single3 - rowHeight;
				}
				float single4 = this.pageSettings.marginLeft;
				for (int k = 0; k <= num1; k++)
				{
					pDPageContentStream.drawLine(single4, height, single4, height - single2);
					single4 = single4 + single;
				}
				pDPageContentStream.setFont(this.pageSettings.Font.PDFBoxFont, this.pageSettings.FontSize);
				float cellMargin = this.pageSettings.marginLeft + this.tableSettings.CellMargin;
				float single5 = height - 15f;
				for (int l = 0; l < num2; l++)
				{
					foreach (string item in this.contents[num5])
					{
						pDPageContentStream.beginText();
						pDPageContentStream.newLineAtOffset(cellMargin, single5);
						pDPageContentStream.showText(item);
						pDPageContentStream.endText();
						cellMargin = cellMargin + single;
					}
					single5 = single5 - rowHeight;
					cellMargin = this.pageSettings.marginLeft + this.tableSettings.CellMargin;
					num5++;
				}
				pDPageContentStream.close();
			}
			pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
			pDDocument.save(this.outputPDF);
		}

		private void Intialise()
		{
			this.tableSettings.CellMargin = 5f;
			this.tableSettings.RowHeight = 20f;
			this.pageSettings.marginBottom = 50f;
			this.pageSettings.marginLeft = 50f;
			this.pageSettings.marginRight = 50f;
			this.pageSettings.marginTop = 50f;
		}

		private void ParseCSV()
		{
			TextFieldParser textFieldParser = new TextFieldParser(this.csvFile)
			{
				TextFieldType = FieldType.Delimited
			};
			textFieldParser.SetDelimiters(new string[] { this.delimiter });
			while (!textFieldParser.EndOfData)
			{
				List<string> list = textFieldParser.ReadFields().ToList<string>();
				this.contents.Add(list);
			}
			textFieldParser.Close();
			try
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				this.DrawTable();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception.InnerException);
			}
		}

		private void SetPageSettings(PDFPageSettings pageSettings)
		{
			if (pageSettings.isMarginSet)
			{
				this.pageSettings.marginBottom = pageSettings.marginBottom;
				this.pageSettings.marginLeft = pageSettings.marginLeft;
				this.pageSettings.marginRight = pageSettings.marginRight;
				this.pageSettings.marginTop = pageSettings.marginTop;
			}
			this.pageSettings.Font = pageSettings.Font;
			this.pageSettings.FontSize = pageSettings.FontSize;
			this.pageSettings.size = pageSettings.size;
		}

		private void SetTableSettings(PDFTableSettings tableSettings)
		{
			if (tableSettings.CellMargin > 0f)
			{
				this.tableSettings.CellMargin = tableSettings.CellMargin;
			}
			if (tableSettings.RowHeight > 0f)
			{
				this.tableSettings.RowHeight = tableSettings.RowHeight;
			}
		}
	}
}