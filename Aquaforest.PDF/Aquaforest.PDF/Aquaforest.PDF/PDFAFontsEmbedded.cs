using org.apache.pdfbox.pdmodel.font;
using System;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	internal class PDFAFontsEmbedded
	{
		public PDFont fontDecriptor
		{
			get;
			set;
		}

		public string FontName
		{
			get;
			set;
		}

		public PDFAFontsEmbedded()
		{
		}
	}
}