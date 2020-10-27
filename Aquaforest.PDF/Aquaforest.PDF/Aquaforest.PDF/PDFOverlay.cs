using java.util;
using org.apache.pdfbox.multipdf;
using org.apache.pdfbox.pdmodel;
using System;

namespace Aquaforest.PDF
{
	public class PDFOverlay
	{
		private PDFDocument overlay;

		private PDFDocument overlayDestination;

		public PDFOverlay(PDFDocument overlay, PDFDocument overlayDestination)
		{
			this.overlay = overlay;
			this.overlayDestination = overlayDestination;
		}

		public void ApplyOverlay(string outputFile)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			Map hashMap = new HashMap();
			Overlay overlay = new Overlay();
			overlay.setInputPDF(this.overlayDestination.PDFBoxDocument);
			overlay.setAllPagesOverlayPDF(this.overlay.PDFBoxDocument);
			overlay.setOverlayPosition(Overlay.Position.FOREGROUND);
			PDDocument pDDocument = overlay.overlay(hashMap);
			if (!PDFHelper.AddStamp)
			{
				pDDocument.save(outputFile);
			}
			else
			{
				pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				pDDocument.save(outputFile);
			}
			pDDocument.close();
			overlay.close();
		}
	}
}