using Aquaforest.PDF;
using java.io;
using Microsoft.Office.Interop.Excel;
using org.apache.pdfbox.pdmodel;
using System;
using System.Collections;
using System.IO;

namespace Aquaforest.Office.PDF
{
	public class ExcelToPDF
	{
		private string filePath = string.Empty;

		private string password = string.Empty;

		private bool ignorePrintAreas = false;

		private bool bitmapNonEmbeddableFonts = false;

		private bool includeDocumentMarkups = false;

		private float fitToPagesTall = 0f;

		private float fitToPagesWide = 0f;

		private float zoom = 0f;

		public bool BitmapNonEmbeddableFonts
		{
			get
			{
				return this.bitmapNonEmbeddableFonts;
			}
			set
			{
				this.bitmapNonEmbeddableFonts = value;
			}
		}

		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		public float FitToPagesTall
		{
			get
			{
				return this.fitToPagesTall;
			}
			set
			{
				this.fitToPagesTall = value;
			}
		}

		public float FitToPagesWide
		{
			get
			{
				return this.fitToPagesWide;
			}
			set
			{
				this.fitToPagesWide = value;
			}
		}

		public bool IgnorePrintAreas
		{
			get
			{
				return this.ignorePrintAreas;
			}
			set
			{
				this.ignorePrintAreas = value;
			}
		}

		public bool IncludeDocumentMarkups
		{
			get
			{
				return this.includeDocumentMarkups;
			}
			set
			{
				this.includeDocumentMarkups = value;
			}
		}

		public float Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				this.zoom = value;
			}
		}

		public ExcelToPDF(string excelFilePath)
		{
			if (!System.IO.File.Exists(excelFilePath))
			{
				throw new System.IO.IOException("File not found.");
			}
			this.filePath = excelFilePath;
		}

		public ExcelToPDF(string excelFilePath, string password)
		{
			if (!System.IO.File.Exists(excelFilePath))
			{
				throw new PDFToolkitException("File not found.");
			}
			this.filePath = excelFilePath;
			this.password = password;
		}

		public bool ConvertToPDF(DocumentSettings settings, string outputFileName)
		{
			object ignorePrintAreas;
			bool flag;
			try
			{
				string tempPath = Path.GetTempPath();
				Guid guid = Guid.NewGuid();
				string str = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString(), "\\", Path.GetFileName(this.filePath)));
				PDFHelper.CheckOutputFolder(Path.GetDirectoryName(str));
				System.IO.File.Copy(this.filePath, str);
				object missing = Type.Missing;
				object obj = this.password;
				object obj1 = this.password;
				object missing1 = Type.Missing;
				object obj2 = this.password;
				object obj3 = this.password;
				object missing2 = Type.Missing;
				object missing3 = Type.Missing;
				object missing4 = Type.Missing;
				object openAndRepair = settings.OpenAndRepair;
				object obj4 = Type.Missing;
				object missing5 = Type.Missing;
				object obj5 = Type.Missing;
				Application applicationClass = new ApplicationClass();
				Workbook workbook = applicationClass.Workbooks.Open(this.filePath, Type.Missing, Type.Missing, Type.Missing, obj, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				foreach (Worksheet worksheet in workbook.Worksheets)
				{
					if (settings.orientatinIset)
					{
						worksheet.PageSetup.Orientation = (XlPageOrientation)settings.PageOrientation;
					}
					if (!settings.ConvertHyperLinks)
					{
						worksheet.Cells.ClearHyperlinks();
					}
					if (settings.paperSizeIset)
					{
						worksheet.PageSetup.PaperSize = (XlPaperSize)settings.PaperSize;
					}
					if (settings.MarginBottom > 0f)
					{
						worksheet.PageSetup.BottomMargin = (double)settings.MarginBottom;
					}
					if (settings.MarginLeft > 0f)
					{
						worksheet.PageSetup.LeftMargin = (double)settings.MarginLeft;
					}
					if (settings.MarginRight > 0f)
					{
						worksheet.PageSetup.RightMargin = (double)settings.MarginRight;
					}
					if (settings.MarginTop > 0f)
					{
						worksheet.PageSetup.TopMargin = (double)settings.MarginTop;
					}
					if (this.Zoom <= 0f)
					{
						worksheet.PageSetup.Zoom = false;
						if (this.FitToPagesTall > 0f)
						{
							worksheet.PageSetup.FitToPagesTall = this.FitToPagesTall;
						}
						if (this.FitToPagesWide > 0f)
						{
							worksheet.PageSetup.FitToPagesWide = this.FitToPagesWide;
						}
					}
					else
					{
						worksheet.PageSetup.Zoom = this.Zoom;
					}
				}
				if ((settings.FromPage <= 0 ? true : settings.ToPage <= 0))
				{
					object obj6 = XlFixedFormatQuality.xlQualityStandard;
					ignorePrintAreas = this.IgnorePrintAreas;
					workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputFileName, obj6, settings.IncludeDocumentProperty, ignorePrintAreas, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				}
				else
				{
					object obj7 = XlFixedFormatQuality.xlQualityStandard;
					ignorePrintAreas = this.IgnorePrintAreas;
					workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputFileName, obj7, settings.IncludeDocumentProperty, ignorePrintAreas, settings.FromPage, settings.ToPage, settings.OpenAfterConverting, Type.Missing);
				}
				workbook.Saved = true;
				workbook.Close(false, Type.Missing, Type.Missing);
				applicationClass.Quit();
				if (System.IO.File.Exists(outputFileName))
				{
					PDFHelper.DisplayTrialPopupIfNecessary();
					PDDocument pDDocument = PDDocument.load(new java.io.File(outputFileName));
					PDFHelper.AddTrialStampIfNecessary(pDDocument, false);
					pDDocument.save(outputFileName);
					pDDocument.close();
				}
				PDFHelper.EmptyDirectory(Path.GetDirectoryName(str));
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new AquaforestOfficeException(exception.Message, exception);
			}
			return flag;
		}
	}
}