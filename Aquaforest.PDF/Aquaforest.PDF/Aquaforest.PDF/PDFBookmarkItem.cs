using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.interactive.documentnavigation.outline;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFBookmarkItem
	{
		private string bookmarkTitle = string.Empty;

		private int bookMarkPage;

		internal PDOutlineItem PDFBoxBookmark = new PDOutlineItem();

		public List<PDFBookmarkItem> BookmarkItems
		{
			get;
			set;
		}

		public int BookMarkPage
		{
			get
			{
				return this.bookMarkPage;
			}
			set
			{
				this.bookMarkPage = value;
			}
		}

		public string BookmarkTitle
		{
			get
			{
				return this.bookmarkTitle;
			}
			set
			{
				this.bookmarkTitle = value;
			}
		}

		public PDFBookmarkItem(int pageNumber, string Title)
		{
			this.bookMarkPage = pageNumber;
			this.bookmarkTitle = Title;
			this.BookmarkItems = new List<PDFBookmarkItem>();
		}

		internal PDFBookmarkItem(PDOutlineItem pdfBoxBookmark, PDDocument doc)
		{
			this.PDFBoxBookmark = pdfBoxBookmark;
			PDPage pDPage = pdfBoxBookmark.findDestinationPage(doc);
			this.BookMarkPage = doc.getPages().indexOf(pDPage) + 1;
			this.bookmarkTitle = pdfBoxBookmark.getTitle();
			this.BookmarkItems = new List<PDFBookmarkItem>();
		}
	}
}