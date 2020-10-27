using Aquaforest.PDF.Font;
using System;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFPageSettings
	{
		internal bool isMarginSet = false;

		internal float marginLeft = 0f;

		internal float marginRight = 0f;

		internal float marginTop = 0f;

		internal float marginBottom = 0f;

		internal PDFRectangle size = null;

		public PDFFont Font
		{
			get;
			set;
		}

		public float FontSize
		{
			get;
			set;
		}

		public PDFPageSettings()
		{
			this.Font = PDFType1Font.TIMES_ROMAN;
			this.FontSize = 12f;
		}

		public void SetMargin(float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			this.marginLeft = marginLeft;
			this.marginRight = marginRight;
			this.marginTop = marginTop;
			this.marginBottom = marginBottom;
			this.isMarginSet = true;
		}

		public void SetSize(PageSize pageSize)
		{
			this.size = PDFHelper.GetPDFRectangle(pageSize);
		}

		public void SetSize(PDFRectangle rectangle)
		{
			this.size = rectangle;
		}
	}
}