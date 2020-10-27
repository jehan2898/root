using Aquaforest.PDF;
using java.io;
using Microsoft.Office.Interop.Word;
using org.apache.pdfbox.pdmodel;
using System;
using System.IO;

namespace Aquaforest.Office.PDF
{
	public class WordToPDF
	{
		private string filePath = string.Empty;

		private string password = string.Empty;

		private bool fitToPages = false;

		private bool bitmapNonEmbeddableFonts = false;

		private bool includeDocumentMarkups = false;

		private bool docStructureTags;

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

		public WordToPDF(string wordFilePath)
		{
			if (!System.IO.File.Exists(wordFilePath))
			{
				throw new System.IO.IOException("File not found.");
			}
			this.filePath = wordFilePath;
		}

		public WordToPDF(string wordFilePath, string password)
		{
			if (!System.IO.File.Exists(wordFilePath))
			{
				throw new Exception("File not found.");
			}
			this.filePath = wordFilePath;
			this.password = password;
		}

		public bool ConvertToPDF(DocumentSettings settings, string outputFileName)
		{
			WdExportItem wdExportItem;
			WdExportCreateBookmarks bookmarkOption;
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
				object obj = Type.Missing;
				object missing1 = Type.Missing;
				object obj1 = this.password;
				object obj2 = this.password;
				object missing2 = Type.Missing;
				object obj3 = this.password;
				object obj4 = this.password;
				object missing3 = Type.Missing;
				object missing4 = Type.Missing;
				object obj5 = false;
				object missing5 = Type.Missing;
				object missing6 = Type.Missing;
				object obj6 = Type.Missing;
				object missing7 = Type.Missing;
				Application applicationClass = new ApplicationClass();
				object obj7 = str1;
				object obj8 = missing;
				object obj9 = obj;
				object obj10 = missing1;
				object obj11 = obj1;
				object obj12 = obj2;
				object obj13 = missing2;
				object obj14 = obj3;
				object obj15 = obj4;
				object obj16 = missing3;
				object obj17 = missing4;
				object obj18 = obj5;
				object obj19 = missing5;
				object missing8 = missing6;
				object missing9 = obj6;
				object missing10 = missing7;
				Document pageOrientation = applicationClass.Documents.Open(ref obj7, ref obj8, ref obj9, ref obj10, ref obj11, ref obj12, ref obj13, ref obj14, ref obj15, ref obj16, ref obj17, ref obj18, ref obj19, ref missing8, ref missing9, ref missing10);
				if (settings.orientatinIset)
				{
					pageOrientation.PageSetup.Orientation = (WdOrientation)settings.PageOrientation;
				}
				if ((settings.PageHeight <= 0f ? false : settings.PageWidth > 0f))
				{
					pageOrientation.PageSetup.PageHeight = settings.PageHeight;
					pageOrientation.PageSetup.PageWidth = settings.PageWidth;
				}
				else if (settings.paperSizeIset)
				{
					pageOrientation.PageSetup.PaperSize = (WdPaperSize)settings.PaperSize;
				}
				if (this.EmbedFonts > AquaforestFontEmbedding.EMBED_NONE)
				{
					pageOrientation.EmbedTrueTypeFonts = true;
					if (this.EmbedFonts == AquaforestFontEmbedding.EMBED_SUBSET)
					{
						pageOrientation.SaveSubsetFonts = true;
					}
				}
				if (settings.revisionModeIset)
				{
					pageOrientation.TrackRevisions = true;
					pageOrientation.ActiveWindow.View.MarkupMode = (WdRevisionsMode)settings.RevisionMode;
				}
				if (settings.MarginBottom > 0f)
				{
					pageOrientation.PageSetup.BottomMargin = settings.MarginBottom;
				}
				if (settings.MarginLeft > 0f)
				{
					pageOrientation.PageSetup.LeftMargin = settings.MarginLeft;
				}
				if (settings.MarginRight > 0f)
				{
					pageOrientation.PageSetup.RightMargin = settings.MarginRight;
				}
				if (settings.MarginTop > 0f)
				{
					pageOrientation.PageSetup.TopMargin = settings.MarginTop;
				}
				if (!settings.ConvertHyperLinks)
				{
					object obj20 = 1;
					while (pageOrientation.Hyperlinks.Count > 0)
					{
						pageOrientation.Hyperlinks[ref obj20].Delete();
					}
				}
				wdExportItem = (!this.includeDocumentMarkups ? WdExportItem.wdExportDocumentContent : WdExportItem.wdExportDocumentWithMarkup);
				if ((settings.FromPage <= 0 ? true : settings.ToPage <= 0))
				{
					AquaforestPDFOptimizeFor optimizeFor = settings.OptimizeFor;
					bitmapNonEmbeddableFonts = this.BitmapNonEmbeddableFonts;
					docStructureTags = this.DocStructureTags;
					bookmarkOption = (WdExportCreateBookmarks)settings.BookmarkOption;
					bool includeDocumentProperty = settings.IncludeDocumentProperty;
					bool pDFA1a = settings.PDFA1a;
					missing10 = Type.Missing;
					pageOrientation.ExportAsFixedFormat(outputFileName, WdExportFormat.wdExportFormatPDF, false, (WdExportOptimizeFor)optimizeFor, WdExportRange.wdExportAllDocument, 1, 1, wdExportItem, includeDocumentProperty, true, bookmarkOption, docStructureTags, bitmapNonEmbeddableFonts, pDFA1a, ref missing10);
				}
				else
				{
					WdExportOptimizeFor wdExportOptimizeFor = (WdExportOptimizeFor)settings.OptimizeFor;
					bool bitmapNonEmbeddableFonts1 = this.BitmapNonEmbeddableFonts;
					bool docStructureTags1 = this.DocStructureTags;
					WdExportItem wdExportItem1 = wdExportItem;
					bookmarkOption = (WdExportCreateBookmarks)settings.BookmarkOption;
					docStructureTags = settings.IncludeDocumentProperty;
					bitmapNonEmbeddableFonts = settings.PDFA1a;
					int fromPage = settings.FromPage;
					int toPage = settings.ToPage;
					bool openAfterConverting = settings.OpenAfterConverting;
					missing10 = Type.Missing;
					pageOrientation.ExportAsFixedFormat(outputFileName, WdExportFormat.wdExportFormatPDF, openAfterConverting, wdExportOptimizeFor, WdExportRange.wdExportFromTo, fromPage, toPage, wdExportItem1, docStructureTags, true, bookmarkOption, docStructureTags1, bitmapNonEmbeddableFonts1, bitmapNonEmbeddableFonts, ref missing10);
				}
				missing10 = false;
				missing9 = Type.Missing;
				missing8 = Type.Missing;
				pageOrientation.Close(ref missing10, ref missing9, ref missing8);
				missing8 = Type.Missing;
				missing9 = Type.Missing;
				missing10 = Type.Missing;
				applicationClass.Quit(ref missing8, ref missing9, ref missing10);
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