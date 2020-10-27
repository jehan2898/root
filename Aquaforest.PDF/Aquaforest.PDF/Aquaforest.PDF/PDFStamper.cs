using Aquaforest.PDF.Font;
using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.font;
using org.apache.pdfbox.pdmodel.graphics.image;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFStamper
	{
		private PDFDocument pdfFile;

		private string fileName;

		private string outputFile;

		private string tempPath = string.Empty;

		private string stampLogo = string.Empty;

		public PDFFont Font
		{
			get;
			set;
		}

		public int FontSize
		{
			get;
			set;
		}

		public Color StampColor
		{
			get;
			set;
		}

		public int StampOpacity
		{
			get;
			set;
		}

		public PDFStamper(PDFDocument doc, string outputFile)
		{
			try
			{
				this.pdfFile = doc;
				string tempPath = Path.GetTempPath();
				Guid guid = Guid.NewGuid();
				this.tempPath = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString()));
				if (!Directory.Exists(this.tempPath))
				{
					Directory.CreateDirectory(this.tempPath);
				}
				this.stampLogo = Path.Combine(this.tempPath, "stampfile.png");
			}
			catch (Exception exception)
			{
				Console.WriteLine(string.Concat("An error was thrown your file was being loaded.\nMessage : ", exception.Message));
				return;
			}
			this.fileName = doc.FilePath;
			this.outputFile = outputFile;
			this.Font = PDFType1Font.HELVETICA;
			this.FontSize = 12;
			this.StampColor = Color.Black;
			this.StampOpacity = 100;
		}

		private bool CreateLogo(string stampText)
		{
			System.Drawing.Font font;
			bool flag;
			try
			{
				if (stampText.Length < 10)
				{
					stampText = string.Concat(stampText, "             ");
				}
				Bitmap bitmap = new Bitmap(1, 1);
				int num = 0;
				int num1 = 0;
				int stampOpacity = 255 * this.StampOpacity / 100;
				string str = this.Font.StringName.Replace("_", " ");
				try
				{
					font = new System.Drawing.Font(str, (float)this.FontSize);
				}
				catch (Exception exception)
				{
					font = new System.Drawing.Font("ARIAL", (float)this.FontSize);
				}
				Graphics graphic = Graphics.FromImage(bitmap);
				SizeF sizeF = graphic.MeasureString(stampText, font);
				num = Math.Max((int)sizeF.Width, 250);
				sizeF = graphic.MeasureString(stampText, font);
				num1 = Math.Max((int)sizeF.Height, 30);
				bitmap = new Bitmap(bitmap, new Size(num, num1));
				graphic = Graphics.FromImage(bitmap);
				graphic.Clear(Color.Transparent);
				graphic.SmoothingMode = SmoothingMode.AntiAlias;
				graphic.TextRenderingHint = TextRenderingHint.AntiAlias;
				graphic.DrawString(stampText, font, new SolidBrush(Color.FromArgb(stampOpacity, this.StampColor)), 0f, 0f);
				graphic.Flush();
				bitmap.Save(this.stampLogo, ImageFormat.Png);
			}
			catch (Exception exception1)
			{
				Console.WriteLine(string.Concat("The stamp File could not be created.\nMessage : ", exception1.Message));
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		private string GetStampString(string stampVariable)
		{
			string str;
			string str1 = stampVariable;
			switch (str1)
			{
				case "%A":
				{
					str = DateTime.Now.ToString("dddd");
					break;
				}
				case "%a":
				{
					str = DateTime.Now.ToString("ddd");
					break;
				}
				case "%B":
				{
					str = DateTime.Now.ToString("MMMM");
					break;
				}
				case "%b":
				{
					str = DateTime.Now.ToString("MMM");
					break;
				}
				case "%C":
				{
					str = DateTime.Now.ToString("F");
					break;
				}
				case "%c":
				{
					str = DateTime.Now.ToString("f");
					break;
				}
				case "%D":
				{
					str = DateTime.Now.ToString("M");
					break;
				}
				case "%d":
				{
					str = DateTime.Now.ToString("Y");
					break;
				}
				case "%E":
				{
					str = DateTime.Now.ToString("yyyy");
					break;
				}
				case "%e":
				{
					str = DateTime.Now.ToString("yy");
					break;
				}
				case "%F":
				{
					str = DateTime.Now.ToString("T");
					break;
				}
				case "%f":
				{
					str = DateTime.Now.ToString("t");
					break;
				}
				case "%G":
				{
					str = DateTime.Now.ToString("r");
					break;
				}
				default:
				{
					str = (str1 == "%g" ? DateTime.Now.ToString("t") : "");
					break;
				}
			}
			return str;
		}

		private float GetXPosition(PDFStamperPositions position, PDRectangle rectanlge, float width)
		{
			float single;
			int num = 0;
			switch (position)
			{
				case PDFStamperPositions.TopLeft:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.TopCenter:
				{
					single = rectanlge.getWidth() / 2f - width / 2f;
					break;
				}
				case PDFStamperPositions.TopRight:
				{
					single = rectanlge.getWidth() - width;
					break;
				}
				case PDFStamperPositions.CenterLeft:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.Center:
				{
					single = rectanlge.getWidth() / 2f - width / 2f;
					break;
				}
				case PDFStamperPositions.CenterRight:
				{
					single = rectanlge.getWidth() - width;
					break;
				}
				case PDFStamperPositions.BottomLeft:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.BottomCenter:
				{
					single = rectanlge.getWidth() / 2f - width / 2f;
					break;
				}
				case PDFStamperPositions.BottomRight:
				{
					single = rectanlge.getWidth() - width;
					break;
				}
				default:
				{
					single = (float)num;
					break;
				}
			}
			return single;
		}

		private float GetYPosition(PDFStamperPositions position, PDRectangle rectanlge, float height)
		{
			float single;
			int num = 0;
			switch (position)
			{
				case PDFStamperPositions.TopLeft:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.TopCenter:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.TopRight:
				{
					single = 0f;
					break;
				}
				case PDFStamperPositions.CenterLeft:
				{
					single = rectanlge.getHeight() / 2f;
					break;
				}
				case PDFStamperPositions.Center:
				{
					single = rectanlge.getHeight() / 2f;
					break;
				}
				case PDFStamperPositions.CenterRight:
				{
					single = rectanlge.getHeight() / 2f;
					break;
				}
				case PDFStamperPositions.BottomLeft:
				{
					single = rectanlge.getHeight() - height;
					break;
				}
				case PDFStamperPositions.BottomCenter:
				{
					single = rectanlge.getHeight() - height;
					break;
				}
				case PDFStamperPositions.BottomRight:
				{
					single = rectanlge.getHeight() - height;
					break;
				}
				default:
				{
					single = (float)num;
					break;
				}
			}
			return single;
		}

		private PDImageXObject Rotate(string stampFile, int angle)
		{
			RotateFlipType rotateFlipType;
			int num = angle;
			if (num == 90)
			{
				rotateFlipType = RotateFlipType.Rotate270FlipNone;
			}
			else if (num == 180)
			{
				rotateFlipType = RotateFlipType.Rotate180FlipNone;
			}
			else
			{
				rotateFlipType = (num == 270 ? RotateFlipType.Rotate90FlipNone : RotateFlipType.RotateNoneFlipNone);
			}
			Image image = Image.FromFile(stampFile);
			if (rotateFlipType == RotateFlipType.RotateNoneFlipNone)
			{
				image.RotateFlip(RotateFlipType.Rotate270FlipNone);
				image.Save(stampFile, ImageFormat.Png);
				image.RotateFlip(RotateFlipType.Rotate90FlipNone);
			}
			else
			{
				image.RotateFlip(rotateFlipType);
			}
			image.Save(stampFile, ImageFormat.Png);
			return PDImageXObject.createFromFile(stampFile, this.pdfFile.PDFBoxDocument);
		}

		private void Save()
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			if (!PDFHelper.AddStamp)
			{
				this.pdfFile.PDFBoxDocument.save(this.outputFile);
			}
			else
			{
				PDDocument pDFBoxDocument = this.pdfFile.PDFBoxDocument;
				pDFBoxDocument = PDFHelper.AddTrialStampIfNecessary(this.pdfFile.PDFBoxDocument);
				pDFBoxDocument.save(this.outputFile);
			}
		}

		public bool StampPageNumber(int startNumber, int x, int y, int StartPage)
		{
			bool flag;
			int num = 0;
			foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
			{
				if (this.CreateLogo((num + startNumber).ToString()))
				{
					PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					num++;
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			this.Save();
			flag = true;
			return flag;
		}

		public bool StampPageNumber(int startNumber, PDFStamperPositions positions, int StartPage)
		{
			bool flag;
			int num = 0;
			int xPosition = 0;
			int yPosition = 0;
			foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
			{
				if (this.CreateLogo((num + startNumber).ToString()))
				{
					PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					num++;
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			this.Save();
			flag = true;
			return flag;
		}

		public bool StampPageNumberBates(int length, string suffix, string prefix, int startNumber, int startPage, int x, int y)
		{
			bool flag;
			if (startPage <= this.pdfFile.PDFBoxDocument.getNumberOfPages())
			{
				int num = 0;
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					if (this.CreateLogo(string.Concat(prefix, (startNumber + num).ToString().PadLeft(length, '0'), suffix)))
					{
						PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
						PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
						if (page.getRotation() <= 0)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
						}
						else
						{
							int rotation = page.getRotation();
							pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
							if (rotation == 90)
							{
								pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
							}
							else if (rotation != 270)
							{
								pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
							}
							else
							{
								pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
							}
						}
						pDPageContentStream.close();
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampPageNumberBates(int length, string suffix, string prefix, int startNumber, int startPage, PDFStamperPositions positions)
		{
			bool flag;
			if (startPage <= this.pdfFile.PDFBoxDocument.getNumberOfPages())
			{
				int num = 0;
				int xPosition = 0;
				int yPosition = 0;
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					if (this.CreateLogo(string.Concat(prefix, (startNumber + num).ToString().PadLeft(length, '0'), suffix)))
					{
						PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
						PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
						if (page.getRotation() <= 0)
						{
							xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
							yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
							pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
						}
						else
						{
							int rotation = page.getRotation();
							pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
							xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
							yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
							if (rotation == 90)
							{
								pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
							}
							else if (rotation != 270)
							{
								pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
							}
							else
							{
								pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
							}
						}
						pDPageContentStream.close();
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampPDFImage(string imagePath, int x, int y)
		{
			bool flag;
			List<string> strs = new List<string>()
			{
				".jpeg",
				".png",
				".tif",
				".tiff"
			};
			try
			{
				string str = Path.Combine(this.tempPath, Path.GetFileName(imagePath));
				File.Copy(imagePath, str, true);
				if (this.StampOpacity < 100)
				{
					ImageTransparency.ChangeOpacity(str, (float)this.StampOpacity);
				}
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile.PDFBoxDocument);
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(str, page.getRotation());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					try
					{
						if (File.Exists(str))
						{
							File.Delete(str);
						}
					}
					catch (Exception exception)
					{
					}
				}
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception1.Message));
				flag = false;
				return flag;
			}
			this.Save();
			flag = true;
			return flag;
		}

		public bool StampPDFImage(string imagePath, PDFStamperPositions positions)
		{
			bool flag;
			int xPosition = 0;
			int yPosition = 0;
			List<string> strs = new List<string>()
			{
				".jpeg",
				".png",
				".tif",
				".tiff"
			};
			try
			{
				string str = Path.Combine(this.tempPath, Path.GetFileName(imagePath));
				File.Copy(imagePath, str, true);
				if (this.StampOpacity < 100)
				{
					ImageTransparency.ChangeOpacity(str, (float)this.StampOpacity);
				}
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile.PDFBoxDocument);
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(str, page.getRotation());
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					try
					{
						if (File.Exists(str))
						{
							File.Delete(str);
						}
					}
					catch (Exception exception)
					{
					}
				}
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception1.Message));
				flag = false;
				return flag;
			}
			this.Save();
			flag = true;
			return flag;
		}

		public bool StampPDFImage(string imagePath, int x, int y, int pageNumber)
		{
			bool flag;
			try
			{
				List<string> strs = new List<string>()
				{
					".jpeg",
					".png",
					".tif",
					".tiff"
				};
				if (this.pdfFile.PDFBoxDocument.getNumberOfPages() >= pageNumber)
				{
					string str = Path.Combine(this.tempPath, Path.GetFileName(imagePath));
					File.Copy(imagePath, str, true);
					if (this.StampOpacity < 100)
					{
						ImageTransparency.ChangeOpacity(str, (float)this.StampOpacity);
					}
					PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile.PDFBoxDocument);
					PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if (page.getRotation() <= 0)
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(str, page.getRotation());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					this.Save();
					try
					{
						if (File.Exists(str))
						{
							File.Delete(str);
						}
					}
					catch (Exception exception)
					{
					}
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception1.Message));
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool StampPDFImage(string imagePath, PDFStamperPositions positions, int pageNumber)
		{
			bool flag;
			int xPosition = 0;
			int yPosition = 0;
			try
			{
				List<string> strs = new List<string>()
				{
					".jpeg",
					".png",
					".tif",
					".tiff"
				};
				if (this.pdfFile.PDFBoxDocument.getNumberOfPages() >= pageNumber)
				{
					string str = Path.Combine(this.tempPath, Path.GetFileName(imagePath));
					File.Copy(imagePath, str, true);
					if (this.StampOpacity < 100)
					{
						ImageTransparency.ChangeOpacity(str, (float)this.StampOpacity);
					}
					PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile.PDFBoxDocument);
					PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if (page.getRotation() <= 0)
					{
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(str, page.getRotation());
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
					this.Save();
					try
					{
						if (File.Exists(str))
						{
							File.Delete(str);
						}
					}
					catch (Exception exception)
					{
					}
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception1.Message));
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		[Obsolete("StampPDFText is deprecated, please use StampPDFTextAsImage instead.")]
		public bool StampPDFText(string text, int x, int y)
		{
			return this.StampPDFTextAsImage(text, x, y);
		}

		[Obsolete("StampPDFText is deprecated, please use StampPDFTextAsImage instead.")]
		public bool StampPDFText(string text, int x, int y, int pageNumber)
		{
			return this.StampPDFTextAsImage(text, x, y, pageNumber);
		}

		public bool StampPDFTextAsImage(string text, int x, int y)
		{
			bool flag;
			if (this.CreateLogo(text))
			{
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
				}
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampPDFTextAsImage(string text, PDFStamperPositions positions)
		{
			bool flag;
			int xPosition = 0;
			int yPosition = 0;
			if (this.CreateLogo(text))
			{
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
				foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
				{
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
					if ((page == null ? true : page.getRotation() <= 0))
					{
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
					}
					else
					{
						int rotation = page.getRotation();
						pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
						xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
						yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
						if (rotation == 90)
						{
							pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
						}
						else if (rotation != 270)
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
						}
						else
						{
							pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
						}
					}
					pDPageContentStream.close();
				}
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampPDFTextAsImage(string text, int x, int y, int pageNumber)
		{
			bool flag;
			if (this.pdfFile.PDFBoxDocument.getNumberOfPages() < pageNumber)
			{
				flag = false;
			}
			else if (this.CreateLogo(text))
			{
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
				PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				if (page.getRotation() <= 0)
				{
					pDPageContentStream.drawImage(pDImageXObject, (float)x, page.getMediaBox().getHeight() - (float)y - (float)pDImageXObject.getHeight());
				}
				else
				{
					int rotation = page.getRotation();
					pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
					if (rotation == 90)
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)x, (float)y);
					}
					else if (rotation != 270)
					{
						pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)x, (float)(y - pDImageXObject.getHeight()));
					}
					else
					{
						pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)x - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)y - (float)pDImageXObject.getWidth());
					}
				}
				pDPageContentStream.close();
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampPDFTextAsImage(string text, PDFStamperPositions positions, int pageNumber)
		{
			bool flag;
			int xPosition = 0;
			int yPosition = 0;
			if (this.pdfFile.PDFBoxDocument.getNumberOfPages() < pageNumber)
			{
				flag = false;
			}
			else if (this.CreateLogo(text))
			{
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(this.stampLogo, this.pdfFile.PDFBoxDocument);
				PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				if (page.getRotation() <= 0)
				{
					xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
					yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
					pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, page.getMediaBox().getHeight() - (float)yPosition - (float)pDImageXObject.getHeight());
				}
				else
				{
					int rotation = page.getRotation();
					pDImageXObject = this.Rotate(this.stampLogo, page.getRotation());
					xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), (float)pDImageXObject.getWidth());
					yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), (float)pDImageXObject.getHeight());
					if (rotation == 90)
					{
						pDPageContentStream.drawImage(pDImageXObject, (float)xPosition, (float)yPosition);
					}
					else if (rotation != 270)
					{
						pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getWidth() - (float)xPosition, (float)(yPosition - pDImageXObject.getHeight()));
					}
					else
					{
						pDPageContentStream.drawImage(pDImageXObject, page.getMediaBox().getHeight() - (float)xPosition - (float)pDImageXObject.getHeight(), page.getMediaBox().getWidth() - (float)yPosition - (float)pDImageXObject.getWidth());
					}
				}
				pDPageContentStream.close();
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampTextAsString(string text, int x, int y)
		{
			foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
			{
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				pDPageContentStream.beginText();
				pDPageContentStream.setFont(this.Font.PDFBoxFont, (float)this.FontSize);
				pDPageContentStream.setNonStrokingColor((int)this.StampColor.R, (int)this.StampColor.G, (int)this.StampColor.B);
				pDPageContentStream.newLineAtOffset((float)x, page.getMediaBox().getHeight() - (float)y);
				pDPageContentStream.showText(text);
				pDPageContentStream.endText();
				pDPageContentStream.close();
			}
			this.Save();
			return true;
		}

		public bool StampTextAsString(string text, PDFStamperPositions positions)
		{
			int xPosition = 0;
			int yPosition = 0;
			foreach (PDPage page in this.pdfFile.PDFBoxDocument.getPages())
			{
				xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), this.Font.PDFBoxFont.getStringWidth(text) / 1000f * (float)this.FontSize);
				yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), 10f);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				pDPageContentStream.beginText();
				pDPageContentStream.setFont(this.Font.PDFBoxFont, (float)this.FontSize);
				pDPageContentStream.setNonStrokingColor((int)this.StampColor.R, (int)this.StampColor.G, (int)this.StampColor.B);
				pDPageContentStream.newLineAtOffset((float)xPosition, page.getMediaBox().getHeight() - (float)yPosition);
				pDPageContentStream.showText(text);
				pDPageContentStream.endText();
				pDPageContentStream.close();
			}
			this.Save();
			return true;
		}

		public bool StampTextAsString(string text, int x, int y, int pageNumber)
		{
			bool flag;
			if (this.pdfFile.PDFBoxDocument.getNumberOfPages() >= pageNumber)
			{
				PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				pDPageContentStream.beginText();
				pDPageContentStream.setFont(this.Font.PDFBoxFont, (float)this.FontSize);
				pDPageContentStream.newLineAtOffset((float)x, page.getMediaBox().getHeight() - (float)y);
				pDPageContentStream.showText(text);
				pDPageContentStream.endText();
				pDPageContentStream.close();
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampTextAsString(string text, PDFStamperPositions positions, int pageNumber)
		{
			bool flag;
			if (this.pdfFile.PDFBoxDocument.getNumberOfPages() >= pageNumber)
			{
				PDPage page = this.pdfFile.PDFBoxDocument.getPage(pageNumber - 1);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile.PDFBoxDocument, page, PDPageContentStream.AppendMode.APPEND, true);
				pDPageContentStream.beginText();
				pDPageContentStream.setFont(this.Font.PDFBoxFont, (float)this.FontSize);
				int xPosition = (int)this.GetXPosition(positions, page.getMediaBox(), this.Font.PDFBoxFont.getStringWidth(text) / 1000f * (float)this.FontSize);
				int yPosition = (int)this.GetYPosition(positions, page.getMediaBox(), 10f);
				pDPageContentStream.newLineAtOffset((float)xPosition, page.getMediaBox().getHeight() - (float)yPosition);
				pDPageContentStream.showText(text);
				pDPageContentStream.endText();
				pDPageContentStream.close();
				this.Save();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool StampVariable(string variable, int x, int y)
		{
			bool flag;
			try
			{
				if (!variable.Equals("%Y"))
				{
					string stampString = this.GetStampString(variable);
					flag = (stampString != "no" ? this.StampTextAsString(stampString, x, y) : false);
				}
				else
				{
					flag = this.StampTextAsString(Path.GetFileName(this.fileName), x, y);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception.Message));
				flag = false;
			}
			return flag;
		}

		public bool StampVariable(string variable, PDFStamperPositions positions)
		{
			bool flag;
			try
			{
				if (!variable.Equals("%Y"))
				{
					string stampString = this.GetStampString(variable);
					flag = (stampString != "no" ? this.StampPDFTextAsImage(stampString, positions) : false);
				}
				else
				{
					flag = this.StampPDFTextAsImage(Path.GetFileName(this.fileName), positions);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				Console.WriteLine(string.Concat("The file ", this.fileName, " was not stamped.\nMessage ", exception.Message));
				flag = false;
			}
			return flag;
		}

		public bool StampVariable(string variable, int x, int y, int pageNumber)
		{
			bool flag;
			if (!variable.Equals("%Y"))
			{
				string stampString = this.GetStampString(variable);
				flag = (stampString != "no" ? this.StampTextAsString(stampString, x, y, pageNumber) : false);
			}
			else
			{
				flag = this.StampTextAsString(Path.GetFileName(this.fileName), x, y, pageNumber);
			}
			return flag;
		}

		public bool StampVariable(string variable, PDFStamperPositions positions, int pageNumber)
		{
			bool flag;
			if (!variable.Equals("%Y"))
			{
				string stampString = this.GetStampString(variable);
				flag = (stampString != "no" ? this.StampPDFTextAsImage(stampString, positions, pageNumber) : false);
			}
			else
			{
				flag = this.StampPDFTextAsImage(Path.GetFileName(this.fileName), positions, pageNumber);
			}
			return flag;
		}
	}
}