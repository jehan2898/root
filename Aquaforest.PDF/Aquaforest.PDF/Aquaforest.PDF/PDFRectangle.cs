using org.apache.pdfbox.pdmodel.common;
using System;

namespace Aquaforest.PDF
{
	public class PDFRectangle
	{
		private PDRectangle rectangle;

		public float Height
		{
			get
			{
				float single;
				single = (this.rectangle == null ? 0f : this.rectangle.getHeight());
				return single;
			}
		}

		internal PDRectangle PDFBoxRectangle
		{
			get
			{
				return this.rectangle;
			}
			set
			{
				this.rectangle = value;
			}
		}

		public float Width
		{
			get
			{
				float single;
				single = (this.rectangle == null ? 0f : this.rectangle.getWidth());
				return single;
			}
		}

		public float X
		{
			get
			{
				float single;
				single = (this.rectangle == null ? 0f : this.rectangle.getLowerLeftX());
				return single;
			}
			set
			{
				if (this.rectangle != null)
				{
					this.rectangle.setLowerLeftX(value);
				}
			}
		}

		public float Y
		{
			get
			{
				float single;
				single = (this.rectangle == null ? 0f : this.rectangle.getLowerLeftY());
				return single;
			}
			set
			{
				if (this.rectangle != null)
				{
					this.rectangle.setLowerLeftY(value);
				}
			}
		}

		public PDFRectangle(float X, float Y, float width, float height)
		{
			this.rectangle = new PDRectangle(X, Y, width, height);
		}

		public PDFRectangle(float width, float height)
		{
			this.rectangle = new PDRectangle(width, height);
		}

		public PDFRectangle(PDRectangle pdRect)
		{
			this.rectangle = pdRect;
		}
	}
}