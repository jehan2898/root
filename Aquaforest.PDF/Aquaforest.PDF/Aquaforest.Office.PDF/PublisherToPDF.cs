using Aquaforest.PDF;
using java.io;
using Microsoft.Office.Interop.Publisher;
using org.apache.pdfbox.pdmodel;
using System;
using System.Collections;
using System.IO;

namespace Aquaforest.Office.PDF
{
	public class PublisherToPDF
	{
		private string filePath = string.Empty;

		private string password = string.Empty;

		private bool bitmapNonEmbeddableFonts = false;

		private bool docStructureTags;

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

		public bool DocStructureTags
		{
			get
			{
				return this.docStructureTags;
			}
			set
			{
				this.docStructureTags = value;
			}
		}

		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		public PublisherToPDF(string publisherFilePath)
		{
			if (!System.IO.File.Exists(publisherFilePath))
			{
				throw new System.IO.IOException("File not found.");
			}
			this.filePath = publisherFilePath;
		}

		public bool ConvertToPDF(DocumentSettings settings, string outputFileName)
		{
			bool docStructureTags;
			bool bitmapNonEmbeddableFonts;
			bool flag;
			try
			{
				string tempPath = Path.GetTempPath();
				Guid guid = Guid.NewGuid();
				string str = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString(), "\\", Path.GetFileName(this.filePath)));
				PDFHelper.CheckOutputFolder(Path.GetDirectoryName(str));
				System.IO.File.Copy(this.filePath, str);
				string str1 = str;
				object missing = Type.Missing;
				bool flag1 = false;
				bool flag2 = false;
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
				Document marginLeft = applicationClass.Open(str1, flag1, flag2, PbSaveOptions.pbDoNotSaveChanges);
				if (settings.MarginLeft > 0f)
				{
					marginLeft.PageSetup.LeftMargin = settings.MarginLeft;
				}
				if (settings.MarginTop > 0f)
				{
					marginLeft.PageSetup.TopMargin = settings.MarginTop;
				}
				if (!settings.ConvertHyperLinks)
				{
					foreach (Page page in marginLeft.Pages)
					{
						foreach (Shape shape in page.Shapes)
						{
							shape.Hyperlink.Delete();
						}
					}
				}
				if ((settings.FromPage <= 0 ? true : settings.ToPage <= 0))
				{
					bitmapNonEmbeddableFonts = this.BitmapNonEmbeddableFonts;
					docStructureTags = this.DocStructureTags;
					marginLeft.ExportAsFixedFormat(PbFixedFormatType.pbFixedFormatTypePDF, outputFileName, PbFixedFormatIntent.pbIntentPrinting, settings.IncludeDocumentProperty, -1, -1, -1, -1, -1, -1, -1, true, PbPrintStyle.pbPrintStyleDefault, docStructureTags, bitmapNonEmbeddableFonts, settings.PDFA1a, Type.Missing);
				}
				else
				{
					bool bitmapNonEmbeddableFonts1 = this.BitmapNonEmbeddableFonts;
					docStructureTags = this.DocStructureTags;
					bool includeDocumentProperty = settings.IncludeDocumentProperty;
					bitmapNonEmbeddableFonts = settings.PDFA1a;
					marginLeft.ExportAsFixedFormat(PbFixedFormatType.pbFixedFormatTypePDF, outputFileName, PbFixedFormatIntent.pbIntentPrinting, includeDocumentProperty, -1, -1, -1, -1, settings.FromPage, settings.ToPage, -1, true, PbPrintStyle.pbPrintStyleDefault, docStructureTags, bitmapNonEmbeddableFonts1, bitmapNonEmbeddableFonts, Type.Missing);
				}
				marginLeft.Close();
				applicationClass.Quit();
				if (System.IO.File.Exists(outputFileName))
				{
					PDFHelper.DisplayTrialPopupIfNecessary();
					PDDocument pDDocument = PDDocument.load(new java.io.File(outputFileName));
					PDFHelper.AddTrialStampIfNecessary(pDDocument, false);
					pDDocument.save(outputFileName);
					pDDocument.close();
				}
				PDFHelper.EmptyDirectory(Path.GetDirectoryName(str1));
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