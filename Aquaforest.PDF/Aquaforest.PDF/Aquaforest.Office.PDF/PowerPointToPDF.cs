using Aquaforest.PDF;
using java.io;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using org.apache.pdfbox.pdmodel;
using System;
using System.Collections;
using System.IO;

namespace Aquaforest.Office.PDF
{
	public class PowerPointToPDF
	{
		private AquaforestSlideSize slideSize;

		private AquaforestPrintHandoutOrder powerPointHandoutOrder;

		private bool slideSizeIsSet = false;

		private string filePath = string.Empty;

		private string password = string.Empty;

		private bool fitToPages = false;

		private bool bitmapNonEmbeddableFonts = false;

		private bool includeDocumentMarkups = false;

		private bool docStructureTags;

		private bool frameSlides = false;

		private AquaforestFontEmbedding embedFonts = AquaforestFontEmbedding.EMBED_NONE;

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

		public AquaforestFontEmbedding EmbedFonts
		{
			get
			{
				return this.embedFonts;
			}
			set
			{
				this.embedFonts = value;
			}
		}

		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		public bool FitToPages
		{
			get
			{
				return this.fitToPages;
			}
			set
			{
				this.fitToPages = value;
			}
		}

		public bool FrameSlides
		{
			get
			{
				return this.frameSlides;
			}
			set
			{
				this.frameSlides = value;
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

		public AquaforestPrintHandoutOrder PowerPointHandoutOrder
		{
			get
			{
				return this.powerPointHandoutOrder;
			}
			set
			{
				this.powerPointHandoutOrder = value;
			}
		}

		public AquaforestSlideSize SlideSize
		{
			get
			{
				return this.slideSize;
			}
			set
			{
				this.slideSizeIsSet = true;
				this.slideSize = value;
			}
		}

		public PowerPointToPDF(string PowerPointFilePath)
		{
			if (!System.IO.File.Exists(PowerPointFilePath))
			{
				throw new System.IO.IOException("File not found.");
			}
			this.filePath = PowerPointFilePath;
		}

		public PowerPointToPDF(string PowerPointFilePath, string password)
		{
			if (!System.IO.File.Exists(PowerPointFilePath))
			{
				throw new AquaforestOfficeException("File not found.");
			}
			this.filePath = PowerPointFilePath;
			this.password = password;
		}

		public bool ConvertToPDF(DocumentSettings settings, string outputFileName)
		{
			MsoTriState msoTriState;
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
				object obj = Type.Missing;
				object missing1 = Type.Missing;
				object obj1 = this.password;
				object obj2 = this.password;
				object missing2 = Type.Missing;
				object obj3 = this.password;
				object obj4 = this.password;
				object missing3 = Type.Missing;
				object missing4 = Type.Missing;
				object missing5 = Type.Missing;
				object openAndRepair = settings.OpenAndRepair;
				object obj5 = Type.Missing;
				object missing6 = Type.Missing;
				object obj6 = Type.Missing;
				Application applicationClass = new ApplicationClass();
				Presentation pageOrientation = applicationClass.Presentations.Open(str1, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
				msoTriState = (!this.FrameSlides ? MsoTriState.msoFalse : MsoTriState.msoTrue);
				if (settings.orientatinIset)
				{
					pageOrientation.PageSetup.SlideOrientation = (MsoOrientation)settings.PageOrientation;
				}
				if ((settings.PageHeight <= 0f ? false : settings.PageWidth > 0f))
				{
					pageOrientation.PageSetup.SlideHeight = settings.PageHeight;
					pageOrientation.PageSetup.SlideWidth = settings.PageWidth;
				}
				else if (this.slideSizeIsSet)
				{
					pageOrientation.PageSetup.SlideSize = (PpSlideSizeType)this.SlideSize;
				}
				if (!settings.ConvertHyperLinks)
				{
					object obj7 = 1;
					foreach (Slide slide in pageOrientation.Slides)
					{
						while (slide.Hyperlinks.Count > 0)
						{
							foreach (Hyperlink hyperlink in slide.Hyperlinks)
							{
								hyperlink.Delete();
							}
						}
					}
				}
				AquaforestPDFOptimizeFor optimizeFor = settings.OptimizeFor;
				AquaforestPrintHandoutOrder powerPointHandoutOrder = this.PowerPointHandoutOrder;
				bool bitmapNonEmbeddableFonts = this.BitmapNonEmbeddableFonts;
				pageOrientation.ExportAsFixedFormat(outputFileName, PpFixedFormatType.ppFixedFormatTypePDF, (PpFixedFormatIntent)optimizeFor, msoTriState, (PpPrintHandoutOrder)powerPointHandoutOrder, PpPrintOutputType.ppPrintOutputSlides, MsoTriState.msoFalse, null, PpPrintRangeType.ppPrintAll, "", settings.IncludeDocumentProperty, true, true, bitmapNonEmbeddableFonts, settings.PDFA1a, Type.Missing);
				pageOrientation.Close();
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