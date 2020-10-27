using java.io;
using java.lang;
using java.util.logging;
using org.apache.commons.collections4;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.interactive.documentnavigation.outline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Exception = System.Exception;
namespace Aquaforest.PDF
{
	public class PDFDocument
	{
		private PDDocument pdfDocument;

		public string FilePath
		{
			get;
			private set;
		}

		public bool IsEncrypted
		{
			get
			{
				return this.pdfDocument.isEncrypted();
			}
		}

		public int NumberOfPages
		{
			get
			{
				return this.pdfDocument.getNumberOfPages();
			}
		}

		internal PDDocument PDFBoxDocument
		{
			get
			{
				return this.pdfDocument;
			}
		}

		public PDFDocument()
		{
			Logger.getLogger("org.apache.pdfbox").setLevel(Level.OFF);
			this.FilePath = "";
			this.pdfDocument = new PDDocument();
		}

		public PDFDocument(string source)
		{
			Logger.getLogger("org.apache.pdfbox").setLevel(Level.OFF);
			this.FilePath = source;
			this.LoadPDF();
			if (this.pdfDocument.isEncrypted())
			{
				throw new PDFToolkitException("The PDF file is encrypted.");
			}
		}

		public PDFDocument(string source, string password)
		{
			Logger.getLogger("org.apache.pdfbox").setLevel(Level.OFF);
			this.FilePath = source;
			this.LoadPDF(password);
		}

		public bool AddBookmarks(PDFBookmark bookmarks)
		{
			bool flag;
			try
			{
				PDDocumentOutline pDDocumentOutline = new PDDocumentOutline();
				this.PDFBoxDocument.getDocumentCatalog().setDocumentOutline(pDDocumentOutline);
				foreach (PDFBookmarkItem bookmarkItem in bookmarks.BookmarkItems)
				{
					PDOutlineItem pDOutlineItem = new PDOutlineItem();
					PDFHelper.AddBookmarkTooutline(bookmarkItem, this.PDFBoxDocument, pDOutlineItem);
					pDDocumentOutline.addLast(pDOutlineItem);
				}
				pDDocumentOutline.openNode();
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public void AddPage(PDFPage page)
		{
			this.pdfDocument.addPage(page.PDFBoxPage);
		}

		public void Close()
		{
			try
			{
				this.pdfDocument.close();
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public bool EmbedPDFAttachment(PDFAttachmentItem attachment)
		{
			return PDFAttachmentItem.EmbedPDFAttachment(attachment, this.PDFBoxDocument);
		}

		public bool EmbedPDFAttachments(List<PDFAttachmentItem> attachments)
		{
			return PDFAttachmentItem.EmbedPDFAttachment(attachments, this.PDFBoxDocument);
		}

		public void ExtractAttachment(string OutputPath)
		{
			PDFExtractEmbeddedFiles.ExtractAttachment(this, OutputPath);
		}

		public bool GenerateHocrFromText(string outputFileName, bool useWords)
		{
			return PDFHocr.CreateHocrFileFromPDF(this.PDFBoxDocument, outputFileName, useWords);
		}

		public PDFBookmark GetBookmarks()
		{
			PDFBookmark pDFBookmark;
			try
			{
				PDDocumentOutline documentOutline = this.PDFBoxDocument.getDocumentCatalog().getDocumentOutline();
				PDFBookmark pDFBookmark1 = new PDFBookmark();
				object[] objArray = IteratorUtils.toArray(documentOutline.children().iterator());
				for (int i = 0; i < (int)objArray.Length; i++)
				{
					object obj = objArray[i];
					PDFHelper.addBookmark((PDOutlineItem)obj, pDFBookmark1.BookmarkItems, this.PDFBoxDocument);
				}
				pDFBookmark = pDFBookmark1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return pDFBookmark;
		}

		public PDFDocumentInformation GetDocumentInformation()
		{
			return (new PDFDocumentInformation()).GetDocumentInformation(this.pdfDocument);
		}

		public List<HocrPageModel> GetDocumentWordData()
		{
			return PDFHocr.GetPageWordDetails(this.PDFBoxDocument);
		}

		public PDFPage GetPage(int pageNumber)
		{
			PDFPage pDFPage = new PDFPage()
			{
				PDFBoxPage = this.pdfDocument.getPage(pageNumber - 1)
			};
			return pDFPage;
		}

		public string GetText()
		{
			return (new PDFTextExtraction()).GetText(this.pdfDocument, true);
		}

		public string GetText(int pageNumber)
		{
			return (new PDFTextExtraction()).GetText(this.pdfDocument, pageNumber, true);
		}

		public string GetTextByArea(RectangleF region, int pageNumber)
		{
			return (new PDFTextExtraction()).GetTextByArea(this.pdfDocument, region, pageNumber);
		}

		public string GetTextByArea(double X, double Y, double width, double height, int pageNumber)
		{
			PDFTextExtraction pDFTextExtraction = new PDFTextExtraction();
			string textByArea = pDFTextExtraction.GetTextByArea(this.pdfDocument, X, Y, width, height, pageNumber);
			return textByArea;
		}

		public string GetXMPMetadata()
		{
			string str = "";
			PDMetadata metadata = this.pdfDocument.getDocumentCatalog().getMetadata();
			if (metadata != null)
			{
				try
				{
					PDFHelper.DisplayTrialPopupIfNecessary();
					str = PDFHelper.convertStreamToString(metadata.exportXMPMetadata());
				}
				catch (java.io.IOException oException)
				{
					throw new System.IO.IOException(oException.getMessage());
				}
			}
			return str;
		}

		private void LoadPDF(string password)
		{
			try
			{
				this.pdfDocument = PDDocument.load(new java.io.File(this.FilePath), password);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception.InnerException);
			}
		}

		private void LoadPDF()
		{
			try
			{
				this.pdfDocument = PDDocument.load(new java.io.File(this.FilePath));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
		}

		public bool RemovePage(PDFPage page)
		{
			bool flag;
			try
			{
				this.pdfDocument.removePage(page.PDFBoxPage);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public bool RemovePage(int pageNumber)
		{
			bool flag;
			try
			{
				this.pdfDocument.removePage(pageNumber);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public void Save(string output)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			if (PDFHelper.AddStamp)
			{
				this.pdfDocument = PDFHelper.AddTrialStampIfNecessary(this.pdfDocument);
			}
			try
			{
				this.pdfDocument.save(output);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
		}

		public void SetDocumentInformation(PDFDocumentInformation info)
		{
			this.pdfDocument.setDocumentInformation(info.PDFBoxDocumentInformation);
		}

		public void SetXMPMetadata(XmlDocument xmp)
		{
			string outerXml = xmp.OuterXml;
			InputStream byteArrayInputStream = new ByteArrayInputStream(Encoding.UTF8.GetBytes(outerXml));
			PDDocumentCatalog documentCatalog = this.pdfDocument.getDocumentCatalog();
			documentCatalog.setMetadata(new PDMetadata(this.pdfDocument, byteArrayInputStream));
		}
	}
}