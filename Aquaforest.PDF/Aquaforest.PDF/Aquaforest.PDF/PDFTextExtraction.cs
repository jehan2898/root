using java.awt;
using java.awt.geom;
using java.util;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.text;
using System;
using System.Collections;
using System.Drawing;

namespace Aquaforest.PDF
{
	internal class PDFTextExtraction
	{
		public PDFTextExtraction()
		{
		}

		public string GetText(PDDocument pdfDocument)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			string str = "";
			try
			{
				PDFTextStripper pDFTextStripper = new PDFTextStripper();
				if (PDFHelper.AddStamp)
				{
					str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
					pDFTextStripper.setEndPage(3);
				}
				str = string.Concat(str, pDFTextStripper.getText(pdfDocument));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception.InnerException);
			}
			return str;
		}

		public string GetText(PDDocument pdfDocument, bool format)
		{
			string text;
			double num;
			string str = "";
			if (format)
			{
				try
				{
					PDFHelper.DisplayTrialPopupIfNecessary();
					PDFTextStripperByArea pDFTextStripperByArea = new PDFTextStripperByArea();
					if (PDFHelper.AddStamp)
					{
						str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
						pDFTextStripperByArea.setEndPage(3);
					}
					pDFTextStripperByArea.setSortByPosition(true);
                    java.util.List arrayList = new java.util.ArrayList();
					PDPageTree pages = pdfDocument.getPages();
					arrayList.size();
					foreach (PDPage page in pages)
					{
						if ((!PDFHelper.AddStamp ? true : pages.indexOf(page) <= 2))
						{
							PDRectangle cropBox = page.getCropBox();
							int rotation = page.getRotation();
							if (cropBox == null)
							{
								cropBox = page.getMediaBox();
							}
							int num1 = 0;
							int num2 = 0;
							if (rotation % 180 != 0)
							{
								num = Math.Round((double)cropBox.getWidth());
								num1 = int.Parse(num.ToString()) - 50;
								num = Math.Round((double)cropBox.getHeight());
								num2 = int.Parse(num.ToString()) - 50;
							}
							else
							{
								num = Math.Round((double)cropBox.getHeight());
								num1 = int.Parse(num.ToString()) - 50;
								num = Math.Round((double)cropBox.getWidth());
								num2 = int.Parse(num.ToString()) - 50;
							}
							pDFTextStripperByArea.addRegion("class1", new java.awt.Rectangle(0, 0, num2, num1));
							pDFTextStripperByArea.extractRegions(page);
							str = string.Concat(str, pDFTextStripperByArea.getTextForRegion("class1"));
						}
						else
						{
							break;
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception.InnerException);
				}
				text = str;
			}
			else
			{
				text = this.GetText(pdfDocument);
			}
			return text;
		}

		public string GetText(PDDocument pdfDocument, int pageNumber)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			string str = "";
			if (PDFHelper.AddStamp)
			{
				str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
			}
			try
			{
				PDFTextStripper pDFTextStripper = new PDFTextStripper();
				pDFTextStripper.setStartPage(pageNumber);
				pDFTextStripper.setEndPage(pageNumber);
				str = string.Concat(str, pDFTextStripper.getText(pdfDocument));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception.InnerException);
			}
			return str;
		}

		internal string GetText(PDDocument pdfDocument, int pageNumber, bool format)
		{
			string text;
			double num;
			string str = "";
			if (format)
			{
				try
				{
					PDFHelper.DisplayTrialPopupIfNecessary();
					PDFTextStripperByArea pDFTextStripperByArea = new PDFTextStripperByArea();
					pDFTextStripperByArea.setSortByPosition(true);
					if (PDFHelper.AddStamp)
					{
						str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
					}
					PDPage page = pdfDocument.getPage(pageNumber - 1);
					PDRectangle cropBox = page.getCropBox();
					int rotation = page.getRotation();
					if (cropBox == null)
					{
						cropBox = page.getMediaBox();
					}
					int num1 = 0;
					int num2 = 0;
					if (rotation % 180 != 0)
					{
						num = Math.Round((double)cropBox.getWidth());
						num1 = int.Parse(num.ToString());
						num = Math.Round((double)cropBox.getHeight());
						num2 = int.Parse(num.ToString());
					}
					else
					{
						num = Math.Round((double)cropBox.getHeight());
						num1 = int.Parse(num.ToString());
						num = Math.Round((double)cropBox.getWidth());
						num2 = int.Parse(num.ToString());
					}
					pDFTextStripperByArea.addRegion("class1", new java.awt.Rectangle(0, 0, num2, num1));
					pDFTextStripperByArea.extractRegions(page);
					str = string.Concat(str, pDFTextStripperByArea.getTextForRegion("class1"));
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception.InnerException);
				}
				text = str;
			}
			else
			{
				text = this.GetText(pdfDocument, pageNumber);
			}
			return text;
		}

		internal string GetTextByArea(PDDocument pdfDocument, RectangleF region, int pageNumber)
		{
			string empty;
			try
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				string str = string.Empty;
				if (PDFHelper.AddStamp)
				{
					str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
				}
				if ((region.Width <= 0f ? true : region.Height <= 0f))
				{
					Console.WriteLine("Sorry the length and width you provided are not greater than zero, no text will be extracted.");
					str = string.Empty;
				}
				else
				{
					PDPage page = pdfDocument.getPage(pageNumber - 1);
					string str1 = "region";
					Rectangle2D num = new Rectangle2D.Double((double)region.X, (double)region.X, (double)region.Width, (double)region.Height);
					PDFTextStripperByArea pDFTextStripperByArea = new PDFTextStripperByArea();
					pDFTextStripperByArea.addRegion(str1, num);
					pDFTextStripperByArea.extractRegions(page);
					str = string.Concat(str, pDFTextStripperByArea.getTextForRegion(str1).Replace("\r", string.Empty).Replace("\n", string.Empty));
				}
				empty = str;
			}
			catch (Exception exception)
			{
				Console.WriteLine("Sorry an exception occured when the text was being extracted\n{0}", exception.Message);
				empty = string.Empty;
			}
			return empty;
		}

		internal string GetTextByArea(PDDocument pdfDocument, double X, double Y, double width, double height, int pageNumber)
		{
			string empty;
			try
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				string str = string.Empty;
				if (PDFHelper.AddStamp)
				{
					str = string.Concat(str, "You are using a trial license of PDF Toolkit, as a result only the first three pages would be extracted.");
				}
				if ((width <= 0 ? true : height <= 0))
				{
					Console.WriteLine("Sorry the length and width you provided are not greater than zero, no text will be extracted.");
					str = string.Empty;
				}
				else
				{
					PDPage page = pdfDocument.getPage(pageNumber - 1);
					string str1 = "region";
					Rectangle2D num = new Rectangle2D.Double(X, Y, width, height);
					PDFTextStripperByArea pDFTextStripperByArea = new PDFTextStripperByArea();
					pDFTextStripperByArea.addRegion(str1, num);
					pDFTextStripperByArea.extractRegions(page);
					str = string.Concat(str, pDFTextStripperByArea.getTextForRegion(str1).Replace("\r", string.Empty).Replace("\n", string.Empty));
				}
				empty = str;
			}
			catch (Exception exception)
			{
				Console.WriteLine("Sorry an exception occured when the text was being extracted\n{0}", exception.Message);
				empty = string.Empty;
			}
			return empty;
		}
	}
}