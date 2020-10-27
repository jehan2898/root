using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFBookmark
	{
		public string BookmarkHeader
		{
			get;
			set;
		}

		public List<PDFBookmarkItem> BookmarkItems
		{
			get;
			set;
		}

		public PDFBookmark ChildBookmark
		{
			get;
			set;
		}

		public PDFBookmark()
		{
			this.BookmarkItems = new List<PDFBookmarkItem>();
		}
	}
}