using System;

namespace Aquaforest.Office.PDF
{
	public class DocumentSettings
	{
		private AquaforestPageOrientation pageOrientation = AquaforestPageOrientation.PORTRAIT;

		private AquaforestPDFBookmarks bookmarkOption = AquaforestPDFBookmarks.BOOKMARKS_FROM_HEADINGS;

		private AquaforestPDFOptimizeFor optimizeFor = AquaforestPDFOptimizeFor.OptimizeForOnScreen;

		private AquaforestPaperSize paperSize;

		private AquaforestRevisionMode revisionMode;

		public bool revisionModeIset = false;

		public bool paperSizeIset = false;

		public bool orientatinIset = false;

		private bool convertHyperLinks = true;

		private bool pDFA1a;

		private bool openAfterConverting;

		private bool includeDocumentProperty;

		private int fromPage;

		private int toPage;

		private bool openAndRepair;

		private float pageHeight = -1f;

		private float pageWidth = -1f;

		private float marginTop = -1f;

		private float marginBottom = -1f;

		private float marginLeft = -1f;

		private float marginRight = -1f;

		public AquaforestPDFBookmarks BookmarkOption
		{
			get
			{
				return this.bookmarkOption;
			}
			set
			{
				this.bookmarkOption = value;
			}
		}

		public bool ConvertHyperLinks
		{
			get
			{
				return this.convertHyperLinks;
			}
			set
			{
				this.convertHyperLinks = value;
			}
		}

		public int FromPage
		{
			get
			{
				return this.fromPage;
			}
			set
			{
				this.fromPage = value;
			}
		}

		public bool IncludeDocumentProperty
		{
			get
			{
				return this.includeDocumentProperty;
			}
			set
			{
				this.includeDocumentProperty = value;
			}
		}

		public float MarginBottom
		{
			get
			{
				return this.marginBottom;
			}
			set
			{
				this.marginBottom = value;
			}
		}

		public float MarginLeft
		{
			get
			{
				return this.marginLeft;
			}
			set
			{
				this.marginLeft = value;
			}
		}

		public float MarginRight
		{
			get
			{
				return this.marginRight;
			}
			set
			{
				this.marginRight = value;
			}
		}

		public float MarginTop
		{
			get
			{
				return this.marginTop;
			}
			set
			{
				this.marginTop = value;
			}
		}

		public bool OpenAfterConverting
		{
			get
			{
				return this.openAfterConverting;
			}
			set
			{
				this.openAfterConverting = value;
			}
		}

		public bool OpenAndRepair
		{
			get
			{
				return this.openAndRepair;
			}
			set
			{
				this.openAndRepair = value;
			}
		}

		public AquaforestPDFOptimizeFor OptimizeFor
		{
			get
			{
				return this.optimizeFor;
			}
			set
			{
				this.optimizeFor = value;
			}
		}

		public float PageHeight
		{
			get
			{
				return this.pageHeight;
			}
			set
			{
				this.pageHeight = value;
			}
		}

		public AquaforestPageOrientation PageOrientation
		{
			get
			{
				return this.pageOrientation;
			}
			set
			{
				this.orientatinIset = true;
				this.pageOrientation = value;
			}
		}

		public float PageWidth
		{
			get
			{
				return this.pageWidth;
			}
			set
			{
				this.pageWidth = value;
			}
		}

		public AquaforestPaperSize PaperSize
		{
			get
			{
				return this.paperSize;
			}
			set
			{
				this.paperSizeIset = true;
				this.paperSize = value;
			}
		}

		public bool PDFA1a
		{
			get
			{
				return this.pDFA1a;
			}
			set
			{
				this.pDFA1a = value;
			}
		}

		public AquaforestRevisionMode RevisionMode
		{
			get
			{
				return this.revisionMode;
			}
			set
			{
				this.revisionModeIset = true;
				this.revisionMode = value;
			}
		}

		public int ToPage
		{
			get
			{
				return this.toPage;
			}
			set
			{
				this.toPage = value;
			}
		}

		public DocumentSettings()
		{
		}
	}
}