using org.apache.pdfbox.pdmodel.font;
using System;

namespace Aquaforest.PDF.Font
{
	public class PDFType1Font : PDFFont
	{
		private PDType1Font type1Font;

		private static string stringName;

		public static PDFType1Font TIMES_ROMAN;

		public static PDFType1Font TIMES_BOLD;

		public static PDFType1Font TIMES_ITALIC;

		public static PDFType1Font TIMES_BOLD_ITALIC;

		public static PDFType1Font HELVETICA;

		public static PDFType1Font HELVETICA_BOLD;

		public static PDFType1Font HELVETICA_OBLIQUE;

		public static PDFType1Font HELVETICA_BOLD_OBLIQUE;

		public static PDFType1Font COURIER;

		public static PDFType1Font COURIER_BOLD;

		public static PDFType1Font COURIER_OBLIQUE;

		public static PDFType1Font COURIER_BOLD_OBLIQUE;

		public static PDFType1Font SYMBOL;

		public static PDFType1Font ZAPFDINGBATS;

		internal override PDFont PDFBoxFont
		{
			get
			{
				return this.type1Font;
			}
		}

		internal override string StringName
		{
			get
			{
				return PDFType1Font.stringName;
			}
		}

		static PDFType1Font()
		{
			PDFType1Font.stringName = "";
			PDFType1Font.TIMES_ROMAN = PDFType1Font.GetStandardFont("Times-Roman");
			PDFType1Font.TIMES_BOLD = PDFType1Font.GetStandardFont("Times-Bold");
			PDFType1Font.TIMES_ITALIC = PDFType1Font.GetStandardFont("Times-Italic");
			PDFType1Font.TIMES_BOLD_ITALIC = PDFType1Font.GetStandardFont("Times-BoldItalic");
			PDFType1Font.HELVETICA = PDFType1Font.GetStandardFont("Helvetica");
			PDFType1Font.HELVETICA_BOLD = PDFType1Font.GetStandardFont("Helvetica-Bold");
			PDFType1Font.HELVETICA_OBLIQUE = PDFType1Font.GetStandardFont("Helvetica-Oblique");
			PDFType1Font.HELVETICA_BOLD_OBLIQUE = PDFType1Font.GetStandardFont("Helvetica-BoldOblique");
			PDFType1Font.COURIER = PDFType1Font.GetStandardFont("Courier");
			PDFType1Font.COURIER_BOLD = PDFType1Font.GetStandardFont("Courier-Bold");
			PDFType1Font.COURIER_OBLIQUE = PDFType1Font.GetStandardFont("Courier-Oblique");
			PDFType1Font.COURIER_BOLD_OBLIQUE = PDFType1Font.GetStandardFont("Courier-BoldOblique");
			PDFType1Font.SYMBOL = PDFType1Font.GetStandardFont("Symbol");
			PDFType1Font.ZAPFDINGBATS = PDFType1Font.GetStandardFont("ZapfDingbats");
		}

		public PDFType1Font()
		{
		}

		private PDFType1Font(string baseFont)
		{
			string upper = baseFont.ToUpper();
			switch (upper)
			{
				case "COURIER":
				{
					this.type1Font = PDType1Font.COURIER;
					break;
				}
				case "COURIER-BOLD":
				{
					this.type1Font = PDType1Font.COURIER_BOLD;
					break;
				}
				case "COURIER-BOLDOBLIQUE":
				{
					this.type1Font = PDType1Font.COURIER_BOLD_OBLIQUE;
					break;
				}
				case "COURIER-OBLIQUE":
				{
					this.type1Font = PDType1Font.COURIER_OBLIQUE;
					break;
				}
				case "HELVETICA":
				{
					this.type1Font = PDType1Font.HELVETICA;
					break;
				}
				case "HELVETICA-BOLD":
				{
					this.type1Font = PDType1Font.HELVETICA_BOLD;
					break;
				}
				case "HELVETICA-OBLIQUE":
				{
					this.type1Font = PDType1Font.HELVETICA_BOLD_OBLIQUE;
					break;
				}
				case "HELVETICA-BOLDOBLIQUE":
				{
					this.type1Font = PDType1Font.HELVETICA_OBLIQUE;
					break;
				}
				case "SYMBOL":
				{
					this.type1Font = PDType1Font.SYMBOL;
					break;
				}
				case "TIMES-BOLD":
				{
					this.type1Font = PDType1Font.TIMES_BOLD;
					break;
				}
				case "TIMES-BOLDITALIC":
				{
					this.type1Font = PDType1Font.TIMES_BOLD_ITALIC;
					break;
				}
				case "TIMES-ITALIC":
				{
					this.type1Font = PDType1Font.TIMES_ITALIC;
					break;
				}
				case "TIMES-ROMAN":
				{
					this.type1Font = PDType1Font.TIMES_ITALIC;
					break;
				}
				case "ZAPFDINGBATS":
				{
					this.type1Font = PDType1Font.ZAPF_DINGBATS;
					break;
				}
				default:
				{
					this.type1Font = PDType1Font.COURIER;
					break;
				}
			}
		}

		public static PDFType1Font GetStandardFont(string font)
		{
			PDFType1Font pDFType1Font;
			PDFType1Font.stringName = font;
			string upper = font.ToUpper();
			switch (upper)
			{
				case "COURIER":
				{
					pDFType1Font = new PDFType1Font("Courier");
					break;
				}
				case "COURIER-BOLD":
				{
					pDFType1Font = new PDFType1Font("Courier-Bold");
					break;
				}
				case "COURIER-BOLDOBLIQUE":
				{
					pDFType1Font = new PDFType1Font("Courier-BoldOblique");
					break;
				}
				case "COURIER-OBLIQUE":
				{
					pDFType1Font = new PDFType1Font("Courier-Oblique");
					break;
				}
				case "HELVETICA":
				{
					pDFType1Font = new PDFType1Font("Helvetica");
					break;
				}
				case "HELVETICA-BOLD":
				{
					pDFType1Font = new PDFType1Font("Helvetica-Bold");
					break;
				}
				case "HELVETICA-OBLIQUE":
				{
					pDFType1Font = new PDFType1Font("Helvetica-Oblique");
					break;
				}
				case "HELVETICA-BOLDOBLIQUE":
				{
					pDFType1Font = new PDFType1Font("Helvetica-BoldOblique");
					break;
				}
				case "SYMBOL":
				{
					pDFType1Font = new PDFType1Font("Symbol");
					break;
				}
				case "TIMES-BOLD":
				{
					pDFType1Font = new PDFType1Font("Times-Bold");
					break;
				}
				case "TIMES-BOLDITALIC":
				{
					pDFType1Font = new PDFType1Font("Times-BoldItalic");
					break;
				}
				case "TIMES-ITALIC":
				{
					pDFType1Font = new PDFType1Font("Times-Italic");
					break;
				}
				case "TIMES-ROMAN":
				{
					pDFType1Font = new PDFType1Font("Times-Roman");
					break;
				}
				default:
				{
					pDFType1Font = (upper == "ZAPFDINGBATS" ? new PDFType1Font("ZapfDingbats") : new PDFType1Font("Times-Roman"));
					break;
				}
			}
			return pDFType1Font;
		}
	}
}