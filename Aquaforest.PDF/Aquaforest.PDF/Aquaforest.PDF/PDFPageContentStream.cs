using Aquaforest.PDF.Font;
using java.io;
using java.lang;
using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.pdmodel;
using System;
using System.IO;

namespace Aquaforest.PDF
{
	public class PDFPageContentStream
	{
		private PDPageContentStream pageContentStream;

		internal PDPageContentStream PDFBoxPageContentStream
		{
			get
			{
				return this.pageContentStream;
			}
		}

		public PDFPageContentStream(PDFDocument pdfDocument, PDFPage sourcePage)
		{
			this.pageContentStream = new PDPageContentStream(pdfDocument.PDFBoxDocument, sourcePage.PDFBoxPage);
		}

		public PDFPageContentStream(PDFDocument pdfDocument, PDFPage sourcePage, bool appendContent, bool compress)
		{
			this.pageContentStream = new PDPageContentStream(pdfDocument.PDFBoxDocument, sourcePage.PDFBoxPage, PDPageContentStream.AppendMode.APPEND, compress);
		}

		public PDFPageContentStream(PDFDocument pdfDocument, PDFPage sourcePage, bool appendContent, bool compress, bool resetContext)
		{
			PDPageContentStream.AppendMode aPPEND = PDPageContentStream.AppendMode.APPEND;
			aPPEND = (!appendContent ? PDPageContentStream.AppendMode.OVERWRITE : PDPageContentStream.AppendMode.APPEND);
			this.pageContentStream = new PDPageContentStream(pdfDocument.PDFBoxDocument, sourcePage.PDFBoxPage, aPPEND, compress);
		}

		public void BeginText()
		{
			try
			{
				this.pageContentStream.beginText();
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void Close()
		{
			try
			{
				this.pageContentStream.close();
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void Concatenate2CTM(double a, double b, double c, double d, double e, double f)
		{
			try
			{
				this.pageContentStream.concatenate2CTM(a, b, c, d, e, f);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void DrawLine(float xStart, float yStart, float xEnd, float yEnd)
		{
			try
			{
				this.pageContentStream.addLine(xStart, yStart, xEnd, yEnd);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void DrawPolygon(float[] x, float[] y)
		{
			try
			{
				this.pageContentStream.drawPolygon(x, y);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void DrawString(string text)
		{
			try
			{
				this.pageContentStream.showText(text);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void EndText()
		{
			try
			{
				this.pageContentStream.endText();
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void FillPolygon(float[] x, float[] y)
		{
			try
			{
				this.pageContentStream.fillPolygon(x, y);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void MoveText(float x, float y)
		{
			try
			{
				this.pageContentStream.newLineAtOffset(x, y);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetFont(PDFFont font, float fontSize)
		{
			try
			{
				this.pageContentStream.setFont(font.PDFBoxFont, fontSize);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetNonStrokingColor(int r, int g, int b)
		{
			try
			{
				this.pageContentStream.setNonStrokingColor(r, g, b);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetNonStrokingColor(int c, int m, int y, int k)
		{
			try
			{
				this.pageContentStream.setNonStrokingColor(c, m, y, k);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetNonStrokingColor(int g)
		{
			try
			{
				this.pageContentStream.setNonStrokingColor(g);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetStrokingColor(int r, int g, int b)
		{
			try
			{
				this.pageContentStream.setStrokingColor(r, g, b);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetStrokingColor(int c, int m, int y, int k)
		{
			try
			{
				this.pageContentStream.setStrokingColor(c, m, y, k);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetStrokingColor(int g)
		{
			try
			{
				this.pageContentStream.setStrokingColor(g);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetTextMatrix(double a, double b, double c, double d, double e, double f)
		{
			try
			{
				this.pageContentStream.setTextMatrix(a, b, c, d, e, f);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetTextRotation(double angle, double tx, double ty)
		{
			try
			{
				this.pageContentStream.setTextRotation(angle, tx, ty);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetTextScaling(double sx, double sy, double tx, double ty)
		{
			try
			{
				this.pageContentStream.setTextScaling(sx, sy, tx, ty);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}

		public void SetTextTranslation(double tx, double ty)
		{
			try
			{
				this.pageContentStream.setTextTranslation(tx, ty);
			}
			catch (java.io.IOException oException)
			{
				throw new System.IO.IOException(oException.getMessage());
			}
		}
	}
}