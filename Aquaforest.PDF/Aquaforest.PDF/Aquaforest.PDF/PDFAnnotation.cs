using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.graphics.color;
using org.apache.pdfbox.pdmodel.interactive.action;
using org.apache.pdfbox.pdmodel.interactive.annotation;
using System;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFAnnotation
	{
		internal PDAnnotation pdfAnotation;

		private PDFRectangle position;

		private PDFAnnotationType annotationType;

		private string content = string.Empty;

		public PDFColor AnnotationColor
		{
			set
			{
				this.pdfAnotation.setColor(PDFHelper.GetColor(value));
			}
		}

		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		public PDFDirection Direction
		{
			get;
			set;
		}

		public string LinkURL
		{
			get;
			set;
		}

		public PDFRectangle Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		public string SubType
		{
			get;
			set;
		}

		public PDFAnnotationType type
		{
			get
			{
				return this.annotationType;
			}
		}

		internal PDFAnnotation(PDAnnotation pdfBoxAnnotation)
		{
			this.pdfAnotation = pdfBoxAnnotation;
			string subtype = pdfBoxAnnotation.getSubtype();
			switch (subtype)
			{
				case "PDAnnotationLine":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationLine;
					break;
				}
				case "PDAnnotationLink":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationLink;
					break;
				}
				case "PDAnnotationPopup":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationPopup;
					break;
				}
				case "PDAnnotationRubberStamp":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationRubberStamp;
					break;
				}
				case "PDAnnotationSquareCircle":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationSquare;
					break;
				}
				case "PDAnnotationText":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationText;
					break;
				}
				case "PDAnnotationTextMarkup":
				{
					this.annotationType = PDFAnnotationType.PDFAnnotationTextMarkup;
					break;
				}
			}
			this.content = pdfBoxAnnotation.getContents();
			this.position = new PDFRectangle(pdfBoxAnnotation.getRectangle());
		}

		public PDFAnnotation(PDFAnnotationType type, PDFRectangle position, string content)
		{
			PDColor color = PDFHelper.GetColor(PDFColor.Black);
			switch (type)
			{
				case PDFAnnotationType.PDFAnnotationLine:
				{
					this.Direction = PDFDirection.UP;
					PDAnnotationLine pDAnnotationLine = new PDAnnotationLine();
					(new PDBorderStyleDictionary()).setWidth(6f);
					pDAnnotationLine.setEndPointEndingStyle("ROpenArrow");
					pDAnnotationLine.setCaption(true);
					this.pdfAnotation = pDAnnotationLine;
					break;
				}
				case PDFAnnotationType.PDFAnnotationLink:
				{
					PDAnnotationLink pDAnnotationLink = new PDAnnotationLink();
					PDActionURI pDActionURI = new PDActionURI();
					pDActionURI.setURI(content);
					pDAnnotationLink.setAction(pDActionURI);
					pDAnnotationLink.setContents(content);
					this.pdfAnotation = pDAnnotationLink;
					break;
				}
				case PDFAnnotationType.PDFAnnotationPopup:
				{
					this.pdfAnotation = new PDAnnotationPopup();
					break;
				}
				case PDFAnnotationType.PDFAnnotationRubberStamp:
				{
					this.pdfAnotation = new PDAnnotationRubberStamp();
					break;
				}
				case PDFAnnotationType.PDFAnnotationSquare:
				{
					this.pdfAnotation = new PDAnnotationSquareCircle("Square");
					break;
				}
				case PDFAnnotationType.PDFAnnotationCircle:
				{
					this.pdfAnotation = new PDAnnotationSquareCircle("Circle");
					break;
				}
				case PDFAnnotationType.PDFAnnotationText:
				{
					this.pdfAnotation = new PDAnnotationText();
					break;
				}
				case PDFAnnotationType.PDFAnnotationTextMarkup:
				{
                        PDAnnotationTextMarkup pDAnnotationTextMarkup = new PDAnnotationTextMarkup("FreeText");
                        float[] array = new float[8];
                        array[0] = position.PDFBoxRectangle.getLowerLeftX();
                        array[1] = position.PDFBoxRectangle.getUpperRightY() - 2f;
                        array[2] = position.PDFBoxRectangle.getUpperRightX();
                        array[3] = array[1];
                        array[4] = array[0];
                        array[5] = position.PDFBoxRectangle.getLowerLeftY() - 2f;
                        array[6] = array[2];
                        array[7] = array[5];
                        pDAnnotationTextMarkup.setQuadPoints(array);
                        pDAnnotationTextMarkup.setContents(content);
                        pDAnnotationTextMarkup.setConstantOpacity(0.2f);
                        this.pdfAnotation = pDAnnotationTextMarkup;
                        break;
                    }
			}
			this.annotationType = type;
			this.position = position;
			if (string.IsNullOrEmpty(this.pdfAnotation.getContents()))
			{
				this.pdfAnotation.setContents(content);
			}
			this.pdfAnotation.setColor(color);
		}

		public PDFAnnotation(PDFAnnotationType type, PDFRectangle position, string content, string subType)
		{
			this.SubType = subType;
			PDColor color = PDFHelper.GetColor(PDFColor.Black);
			switch (type)
			{
				case PDFAnnotationType.PDFAnnotationLine:
				{
					this.Direction = PDFDirection.UP;
					PDAnnotationLine pDAnnotationLine = new PDAnnotationLine();
					(new PDBorderStyleDictionary()).setWidth(6f);
					if (!string.IsNullOrEmpty(subType))
					{
						pDAnnotationLine.setEndPointEndingStyle(subType);
					}
					else
					{
						pDAnnotationLine.setEndPointEndingStyle("ROpenArrow");
					}
					pDAnnotationLine.setCaption(true);
					this.pdfAnotation = pDAnnotationLine;
					break;
				}
				case PDFAnnotationType.PDFAnnotationLink:
				{
					PDAnnotationLink pDAnnotationLink = new PDAnnotationLink();
					pDAnnotationLink.setContents(content);
					this.pdfAnotation = pDAnnotationLink;
					break;
				}
				case PDFAnnotationType.PDFAnnotationPopup:
				{
					this.pdfAnotation = new PDAnnotationPopup();
					break;
				}
				case PDFAnnotationType.PDFAnnotationRubberStamp:
				{
					this.pdfAnotation = new PDAnnotationRubberStamp();
					break;
				}
				case PDFAnnotationType.PDFAnnotationSquare:
				{
					this.pdfAnotation = new PDAnnotationSquareCircle("Square");
					break;
				}
				case PDFAnnotationType.PDFAnnotationCircle:
				{
					this.pdfAnotation = new PDAnnotationSquareCircle("Circle");
					break;
				}
				case PDFAnnotationType.PDFAnnotationText:
				{
					this.pdfAnotation = new PDAnnotationText();
					break;
				}
				case PDFAnnotationType.PDFAnnotationTextMarkup:
				{
					PDAnnotationTextMarkup pDAnnotationTextMarkup = null;
					pDAnnotationTextMarkup = (!string.IsNullOrEmpty(subType) ? new PDAnnotationTextMarkup(subType) : new PDAnnotationTextMarkup("FreeText"));
					pDAnnotationTextMarkup.setContents(content);
					this.pdfAnotation = pDAnnotationTextMarkup;
					break;
				}
			}
			this.annotationType = type;
			this.position = position;
			if (string.IsNullOrEmpty(this.pdfAnotation.getContents()))
			{
				this.pdfAnotation.setContents(content);
			}
			this.pdfAnotation.setColor(color);
		}
	}
}