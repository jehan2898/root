using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.interactive.viewerpreferences;
using System;

namespace Aquaforest.PDF
{
	public class PDFDocumentOpenSetting
	{
		private PDFDocument pdfDocument;

		private PDDocumentCatalog documentDocumentOpenSettings;

		private PDViewerPreferences documentViewerPrefernces;

		public bool? CenterWindow
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.centerWindow());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setCenterWindow(value.Value);
				}
			}
		}

		public bool? DisplayDocTitle
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.displayDocTitle());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setDisplayDocTitle(value.Value);
				}
			}
		}

		public bool? FitWindow
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.fitWindow());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setFitWindow(value.Value);
				}
			}
		}

		public bool? HideMenubar
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.hideMenubar());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setHideMenubar(value.Value);
				}
			}
		}

		public bool? HideToolbar
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.hideToolbar());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setHideToolbar(value.Value);
				}
			}
		}

		public bool? HideWindowUI
		{
			get
			{
				return new bool?(this.documentViewerPrefernces.hideWindowUI());
			}
			set
			{
				if (value.HasValue)
				{
					this.documentViewerPrefernces.setHideWindowUI(value.Value);
				}
			}
		}

		public string NonFullScreenPageMode
		{
			get
			{
				return this.documentViewerPrefernces.getNonFullScreenPageMode();
			}
			set
			{
				this.documentViewerPrefernces.setNonFullScreenPageMode(value);
			}
		}

		public string PDFPageLayout
		{
			get
			{
				return this.documentDocumentOpenSettings.getPageLayout().stringValue();
			}
			set
			{
				this.documentDocumentOpenSettings.setPageLayout(PageLayout.fromString(value));
			}
		}

		public string PDFPageMode
		{
			get
			{
				return this.documentDocumentOpenSettings.getPageMode().stringValue();
			}
			set
			{
				this.documentDocumentOpenSettings.setPageMode(PageMode.fromString(value));
			}
		}

		public PDFDocumentOpenSetting(PDFDocument pdfDoc)
		{
			this.documentDocumentOpenSettings = pdfDoc.PDFBoxDocument.getDocumentCatalog();
			this.pdfDocument = pdfDoc;
			if (this.documentDocumentOpenSettings != null)
			{
				if (this.documentDocumentOpenSettings.getViewerPreferences() != null)
				{
					this.documentViewerPrefernces = this.documentDocumentOpenSettings.getViewerPreferences();
				}
				else
				{
					this.documentViewerPrefernces = new PDViewerPreferences(new COSDictionary());
				}
			}
		}

		public void SaveOpenSettings(string outputFile)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			PDFHelper.AddTrialStampIfNecessary(this.pdfDocument.PDFBoxDocument, false);
			this.documentDocumentOpenSettings.setViewerPreferences(this.documentViewerPrefernces);
			this.pdfDocument.PDFBoxDocument.save(outputFile);
		}
	}
}