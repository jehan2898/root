using Aquaforest.PDF;
using java.io;
using org.apache.pdfbox.pdmodel.font;
using System;

namespace Aquaforest.PDF.Font
{
	public class PDFTrueTypeFont : PDFFont
	{
		private static PDTrueTypeFont trueTypeFont;

		private static string stringName;

		internal override PDFont PDFBoxFont
		{
			get
			{
				return PDFTrueTypeFont.trueTypeFont;
			}
		}

		internal override string StringName
		{
			get
			{
				return PDFTrueTypeFont.stringName;
			}
		}

		static PDFTrueTypeFont()
		{
			PDFTrueTypeFont.stringName = "";
		}

		public PDFTrueTypeFont()
		{
		}

		private PDFTrueTypeFont(PDFDocument document, string font)
		{
			PDFTrueTypeFont.stringName = font;
			PDFTrueTypeFont.trueTypeFont = PDTrueTypeFont.loadTTF(document.PDFBoxDocument, new File(font));
		}

		public static PDFTrueTypeFont LoadTTF(PDFDocument document, string font)
		{
			return new PDFTrueTypeFont(document, font);
		}
	}
}