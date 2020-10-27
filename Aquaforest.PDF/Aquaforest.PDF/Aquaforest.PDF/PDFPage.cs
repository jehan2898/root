using java.util;
using org.apache.commons.collections4;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.interactive.action;
using org.apache.pdfbox.pdmodel.interactive.annotation;
using System;
using System.Collections.Generic;

namespace Aquaforest.PDF
{
	public class PDFPage
	{
		private PDPage page;

		public PDFRectangle CropBox
		{
			get
			{
				return new PDFRectangle(this.page.getCropBox());
			}
			set
			{
				this.page.setMediaBox(this.CropBox.PDFBoxRectangle);
			}
		}

		public PDFRectangle MediaBox
		{
			get
			{
				return new PDFRectangle(this.page.getMediaBox());
			}
			set
			{
				this.page.setMediaBox(this.MediaBox.PDFBoxRectangle);
			}
		}

		internal PDPage PDFBoxPage
		{
			get
			{
				return this.page;
			}
			set
			{
				this.page = value;
			}
		}

		public int Rotation
		{
			get
			{
				return this.page.getRotation();
			}
			set
			{
				this.page.setRotation(value);
			}
		}

		public PDFPage()
		{
			this.page = new PDPage();
		}

		public PDFPage(PageSize pageSize)
		{
			this.page = new PDPage();
			this.MediaBox = PDFHelper.GetPDFRectangle(pageSize);
		}

		public PDFPage(PDFRectangle rectangle)
		{
			this.page = new PDPage();
			this.MediaBox = rectangle;
		}

        public bool AddAnnotation(PDFAnnotation annotation, PDFDocument doc)
        {
            bool result;
            try
            {
                List annotations = this.PDFBoxPage.getAnnotations();
                PDRectangle rectangle = new PDRectangle(annotation.Position.X, this.MediaBox.Height - annotation.Position.Y - annotation.Position.Height, annotation.Position.Width, annotation.Position.Height);
                annotation.pdfAnotation.setRectangle(rectangle);
                bool flag = annotation.type == PDFAnnotationType.PDFAnnotationLine;
                if (flag)
                {
                    PDAnnotationLine pDAnnotationLine = (PDAnnotationLine)annotation.pdfAnotation;
                    float[] array = new float[4];
                    switch (annotation.Direction)
                    {
                        case PDFDirection.DIAGONALLEFTUP:
                            array[2] = annotation.Position.PDFBoxRectangle.getLowerLeftX();
                            array[3] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY();
                            array[0] = annotation.Position.PDFBoxRectangle.getLowerLeftX() + annotation.Position.PDFBoxRectangle.getWidth();
                            array[1] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY() - annotation.Position.PDFBoxRectangle.getHeight();
                            break;
                        case PDFDirection.DIAGONALRIGHTUP:
                            array[0] = annotation.Position.PDFBoxRectangle.getLowerLeftX();
                            array[1] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY() - annotation.Position.PDFBoxRectangle.getHeight();
                            array[2] = annotation.Position.PDFBoxRectangle.getLowerLeftX() + annotation.Position.PDFBoxRectangle.getWidth();
                            array[3] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY();
                            break;
                        case PDFDirection.DIAGONALRIGHTDOWN:
                            array[0] = annotation.Position.PDFBoxRectangle.getLowerLeftX();
                            array[1] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY();
                            array[2] = annotation.Position.PDFBoxRectangle.getLowerLeftX() + annotation.Position.PDFBoxRectangle.getWidth();
                            array[3] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY() - annotation.Position.PDFBoxRectangle.getHeight();
                            break;
                        case PDFDirection.DIAGONALLEFTDOWN:
                            array[2] = annotation.Position.PDFBoxRectangle.getLowerLeftX();
                            array[3] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY() - annotation.Position.PDFBoxRectangle.getHeight();
                            array[0] = annotation.Position.PDFBoxRectangle.getLowerLeftX() + annotation.Position.PDFBoxRectangle.getWidth();
                            array[1] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY();
                            break;
                    }
                    pDAnnotationLine.setLine(array);
                    annotations.add(pDAnnotationLine);
                }
                else
                {
                    bool flag2 = annotation.type == PDFAnnotationType.PDFAnnotationTextMarkup;
                    if (flag2)
                    {
                        PDAnnotationTextMarkup pDAnnotationTextMarkup = (PDAnnotationTextMarkup)annotation.pdfAnotation;
                        float[] array2 = new float[8];
                        array2[0] = annotation.Position.PDFBoxRectangle.getLowerLeftX();
                        array2[1] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getUpperRightY() - 2f;
                        array2[2] = annotation.Position.PDFBoxRectangle.getUpperRightX();
                        array2[3] = array2[1];
                        array2[4] = array2[0];
                        array2[5] = this.PDFBoxPage.getCropBox().getHeight() - annotation.Position.PDFBoxRectangle.getLowerLeftY() - 2f;
                        array2[6] = array2[2];
                        array2[7] = array2[5];
                        pDAnnotationTextMarkup.setQuadPoints(array2);
                        annotations.add(pDAnnotationTextMarkup);
                    }
                    else
                    {
                        bool flag3 = annotation.type == PDFAnnotationType.PDFAnnotationLink;
                        if (flag3)
                        {
                            PDAnnotationLink pDAnnotationLink = (PDAnnotationLink)annotation.pdfAnotation;
                            bool flag4 = !string.IsNullOrEmpty(annotation.LinkURL);
                            if (flag4)
                            {
                                PDActionURI pDActionURI = new PDActionURI();
                                pDActionURI.setURI(annotation.LinkURL);
                                pDAnnotationLink.setAction(pDActionURI);
                            }
                            else
                            {
                                Console.WriteLine("The PDFAnnotationLink has no LinkURL property. Set this property for the link to work well.");
                            }
                            annotations.add(pDAnnotationLink);
                        }
                        else
                        {
                            bool flag5 = annotation.type == PDFAnnotationType.PDFAnnotationRubberStamp;
                            if (flag5)
                            {
                                PDAnnotationRubberStamp pDAnnotationRubberStamp = (PDAnnotationRubberStamp)annotation.pdfAnotation;
                                pDAnnotationRubberStamp.setName("TopSecret");
                                pDAnnotationRubberStamp.setAppearance(PDFHelper.GetRubberStampAppearance(doc, annotation.Position, annotation.SubType));
                                pDAnnotationRubberStamp.setRectangle(rectangle);
                                annotations.add(pDAnnotationRubberStamp);
                            }
                            else
                            {
                                annotations.add(annotation.pdfAnotation);
                            }
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new PDFToolkitException(ex.Message, ex);
            }
            return result;
        }
        public List<PDFAnnotation> GetAnnotations()
		{
			List<PDFAnnotation> pDFAnnotations;
			try
			{
				List<PDFAnnotation> pDFAnnotations1 = new List<PDFAnnotation>();
				object[] objArray = IteratorUtils.toArray(this.PDFBoxPage.getAnnotations().iterator());
				object[] objArray1 = objArray;
				for (int i = 0; i < (int)objArray1.Length; i++)
				{
					pDFAnnotations1.Add(new PDFAnnotation((PDAnnotation)objArray1[i]));
				}
				pDFAnnotations = pDFAnnotations1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return pDFAnnotations;
		}
	}
}