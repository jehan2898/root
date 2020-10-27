using org.apache.pdfbox.pdmodel.font;
using System;

namespace Aquaforest.PDF.Font
{
	public abstract class PDFFont
	{
		internal abstract PDFont PDFBoxFont
		{
			get;
		}

		internal abstract string StringName
		{
			get;
		}

		protected PDFFont()
		{
		}
	}
}