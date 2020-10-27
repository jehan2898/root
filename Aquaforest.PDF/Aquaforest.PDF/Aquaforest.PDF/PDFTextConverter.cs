using Aquaforest.PDF.Font;
using java.io;
using org.apache.fontbox.util;
using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.font;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Aquaforest.PDF
{
	public class PDFTextConverter
	{
		private string textFile;

		private string pdfFileName;

		private PDFFont font = null;

		private float fontSize = 12f;

		private bool isLandescape = false;

		public PDFFont Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		public float FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		public bool IsLandescape
		{
			get
			{
				return this.isLandescape;
			}
			set
			{
				this.isLandescape = value;
			}
		}

		public PDFTextConverter(string textFile)
		{
			try
			{
				if (!System.IO.File.Exists(textFile))
				{
					throw new PDFToolkitException("File not found");
				}
				this.textFile = textFile;
			}
			catch (Exception exception)
			{
			}
		}

		internal void createPDFFromText(string pdfFile)
		{
			string str;
			bool flag;
			PDDocument pDDocument = new PDDocument();
			PDFHelper.DisplayTrialPopupIfNecessary();
			PDFHelper.CheckOutputFolder(Path.GetDirectoryName(pdfFile));
			try
			{
				this.pdfFileName = pdfFile;
				Reader fileReader = new FileReader(this.textFile);
				int num = 40;
				if (this.font == null)
				{
					this.font = PDFType1Font.TIMES_ROMAN;
				}
				float height = this.font.PDFBoxFont.getBoundingBox().getHeight() / 1000f;
				PDRectangle lETTER = PDRectangle.LETTER;
				if (this.isLandescape)
				{
					lETTER = new PDRectangle(lETTER.getHeight(), lETTER.getWidth());
				}
				height = height * this.fontSize * 1.05f;
				string[] strArrays = System.IO.File.ReadAllLines(this.textFile);
				PDPage pDPage = new PDPage(lETTER);
				PDPageContentStream pDPageContentStream = null;
				float single = -1f;
				float width = pDPage.getMediaBox().getWidth() - (float)(2 * num);
				bool flag1 = true;
				string[] strArrays1 = strArrays;
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str1 = strArrays1[i];
					flag1 = false;
					string str2 = Regex.Replace(str1, "\\t|\\n|\\r", "");
					string[] strArrays2 = str2.Replace("[\\n\\r]+$", "").Split(new char[] { ' ' });
					int num1 = 0;
					while (num1 < (int)strArrays2.Length)
					{
						StringBuilder stringBuilder = new StringBuilder();
						float stringWidth = 0f;
						bool flag2 = false;
						do
						{
							string str3 = "";
							int num2 = strArrays2[num1].IndexOf('\f');
							if (num2 != -1)
							{
								flag2 = true;
								str = strArrays2[num1].Substring(0, num2);
								if (num2 < strArrays2[num1].Length)
								{
									str3 = strArrays2[num1].Substring(num2 + 1);
								}
							}
							else
							{
								str = strArrays2[num1];
							}
							if ((str.Length > 0 ? true : !flag2))
							{
								stringBuilder.Append(str);
								stringBuilder.Append(" ");
							}
							if ((!flag2 ? false : str3.Length != 0))
							{
								strArrays2[num1] = str3;
							}
							else
							{
								num1++;
							}
							if (!flag2)
							{
								if (num1 < (int)strArrays2.Length)
								{
									string str4 = strArrays2[num1];
									num2 = str4.IndexOf('\f');
									if (num2 != -1)
									{
										str4 = str4.Substring(0, num2);
									}
									string str5 = string.Concat(stringBuilder.ToString(), " ", str4);
									try
									{
										stringWidth = this.font.PDFBoxFont.getStringWidth(str5) / 1000f * this.fontSize;
									}
									catch (Exception exception)
									{
									}
								}
								flag = (num1 >= (int)strArrays2.Length ? false : stringWidth < width);
							}
							else
							{
								break;
							}
						}
						while (flag);
						if (single < (float)num)
						{
							pDPage = new PDPage(lETTER);
							pDDocument.addPage(pDPage);
							if (pDPageContentStream != null)
							{
								pDPageContentStream.endText();
								pDPageContentStream.close();
							}
							pDPageContentStream = new PDPageContentStream(pDDocument, pDPage);
							pDPageContentStream.setFont(this.font.PDFBoxFont, this.fontSize);
							pDPageContentStream.beginText();
							single = pDPage.getMediaBox().getHeight() - (float)num + height;
							pDPageContentStream.newLineAtOffset((float)num, single);
						}
						if (pDPageContentStream == null)
						{
							throw new java.io.IOException("Error:Expected non-null content stream.");
						}
						pDPageContentStream.newLineAtOffset(0f, -height);
						single = single - height;
						try
						{
							pDPageContentStream.showText(stringBuilder.ToString());
							if (flag2)
							{
								pDPage = new PDPage(lETTER);
								pDDocument.addPage(pDPage);
								pDPageContentStream.endText();
								pDPageContentStream.close();
								pDPageContentStream = new PDPageContentStream(pDDocument, pDPage);
								pDPageContentStream.setFont(this.font.PDFBoxFont, this.fontSize);
								pDPageContentStream.beginText();
								single = pDPage.getMediaBox().getHeight() - (float)num + height;
								pDPageContentStream.newLineAtOffset((float)num, single);
							}
						}
						catch (Exception exception1)
						{
						}
					}
				}
				if (flag1)
				{
					pDDocument.addPage(pDPage);
				}
				if (pDPageContentStream != null)
				{
					pDPageContentStream.endText();
					pDPageContentStream.close();
				}
				if (PDFHelper.AddStamp)
				{
					pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				}
				try
				{
					pDDocument.save(pdfFile);
				}
				catch (Exception exception3)
				{
					Exception exception2 = exception3;
					throw new PDFToolkitException(exception2.Message, exception2.InnerException);
				}
			}
			catch (Exception exception5)
			{
				Exception exception4 = exception5;
				if (pDDocument != null)
				{
					pDDocument.close();
				}
				throw exception4;
			}
		}
	}
}