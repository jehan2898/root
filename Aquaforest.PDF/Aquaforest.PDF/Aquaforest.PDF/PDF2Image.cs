using java.awt.image;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.rendering;
using org.apache.pdfbox.tools.imageio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDF2Image
	{
		private PDDocument pdfDocument;

		private int dpi = 200;

		private int startPage = 1;

		private int endPage = -1;

		private PDF2Image.PDF2ImageColor imageColor;

		private PDF2Image.TIFFOutputCompression imageType = PDF2Image.TIFFOutputCompression.LZW;

		private static string TempPath;

		public int DPI
		{
			get
			{
				return this.dpi;
			}
			set
			{
				this.dpi = value;
			}
		}

		public int EndPage
		{
			get
			{
				return this.endPage;
			}
			set
			{
				this.endPage = value;
			}
		}

		public PDF2Image.PDF2ImageColor OutputImageColor
		{
			get
			{
				return this.imageColor;
			}
			set
			{
				this.imageColor = value;
			}
		}

		public int StartPage
		{
			get
			{
				return this.startPage;
			}
			set
			{
				this.startPage = value;
			}
		}

		public PDF2Image.TIFFOutputCompression TiffCompression
		{
			get
			{
				return this.imageType;
			}
			set
			{
				this.imageType = value;
			}
		}

		static PDF2Image()
		{
			string tempPath = Path.GetTempPath();
			Guid guid = Guid.NewGuid();
			PDF2Image.TempPath = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString()));
		}

		public PDF2Image(PDFDocument doc)
		{
			this.pdfDocument = doc.PDFBoxDocument;
		}

		private bool ConvertJPGToTiff(List<string> imageFileList, string fileName)
		{
			bool flag;
			try
			{
				Encoder compression = Encoder.Compression;
				Encoder saveFlag = Encoder.SaveFlag;
				ImageCodecInfo imageCodecInfo = ((IEnumerable<ImageCodecInfo>)ImageCodecInfo.GetImageEncoders()).First<ImageCodecInfo>((ImageCodecInfo i) => i.MimeType == "image/tiff");
				EncoderParameters encoderParameter = new EncoderParameters(2);
				switch (this.TiffCompression)
				{
					case PDF2Image.TIFFOutputCompression.LZW:
					{
						encoderParameter.Param[1] = new EncoderParameter(compression, (long)2);
						break;
					}
					case PDF2Image.TIFFOutputCompression.GROUP3:
					{
						encoderParameter.Param[1] = new EncoderParameter(compression, (long)3);
						break;
					}
					case PDF2Image.TIFFOutputCompression.GROUP4:
					{
						encoderParameter.Param[1] = new EncoderParameter(compression, (long)4);
						break;
					}
				}
				encoderParameter.Param[0] = new EncoderParameter(saveFlag, (long)18);
				Bitmap bitmap = (Bitmap)Image.FromFile(imageFileList[0]);
				bitmap.SetResolution((float)this.dpi, (float)this.dpi);
				bitmap.Save(fileName, imageCodecInfo, encoderParameter);
				encoderParameter.Param[0] = new EncoderParameter(saveFlag, (long)23);
				for (int num = 1; num < imageFileList.Count; num++)
				{
					Bitmap bitmap1 = (Bitmap)Image.FromFile(imageFileList[num]);
					bitmap.SaveAdd(bitmap1, encoderParameter);
				}
				encoderParameter.Param[0] = new EncoderParameter(saveFlag, (long)20);
				bitmap.SaveAdd(encoderParameter);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public bool ConvertToImage(string outputFile, int pageNumber)
		{
			try
			{
				try
				{
					ImageType bINARY = null;
					switch (this.OutputImageColor)
					{
						case PDF2Image.PDF2ImageColor.BINARY:
						{
							bINARY = ImageType.BINARY;
							break;
						}
						case PDF2Image.PDF2ImageColor.GRAY:
						{
							bINARY = ImageType.GRAY;
							break;
						}
						case PDF2Image.PDF2ImageColor.RGB:
						{
							bINARY = ImageType.RGB;
							break;
						}
						case PDF2Image.PDF2ImageColor.ARGB:
						{
							bINARY = ImageType.ARGB;
							break;
						}
					}
					PDFHelper.CheckOutputFolder(PDF2Image.TempPath);
					bool flag = true;
					this.startPage = Math.Max(this.startPage, 1);
					if (this.endPage < 0)
					{
						this.endPage = this.pdfDocument.getNumberOfPages();
					}
					this.endPage = Math.Min(this.endPage, this.pdfDocument.getNumberOfPages());
					PDFRenderer pDFRenderer = new PDFRenderer(this.pdfDocument);
					BufferedImage bufferedImage = null;
					bufferedImage = (bINARY != null ? pDFRenderer.renderImageWithDPI(pageNumber - 1, (float)this.dpi, bINARY) : pDFRenderer.renderImageWithDPI(pageNumber - 1, (float)this.dpi));
					flag = flag & ImageIOUtil.writeImage(bufferedImage, outputFile, this.dpi);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception);
				}
			}
			finally
			{
				PDFHelper.EmptyDirectory(PDF2Image.TempPath);
			}
			return true;
		}

		public bool ConvertToMultipageTiffImage(string outputFile)
		{
			try
			{
				try
				{
					if (!Directory.Exists(PDF2Image.TempPath))
					{
						Directory.CreateDirectory(PDF2Image.TempPath);
					}
					ImageType bINARY = null;
					switch (this.OutputImageColor)
					{
						case PDF2Image.PDF2ImageColor.BINARY:
						{
							bINARY = ImageType.BINARY;
							break;
						}
						case PDF2Image.PDF2ImageColor.GRAY:
						{
							bINARY = ImageType.GRAY;
							break;
						}
						case PDF2Image.PDF2ImageColor.RGB:
						{
							bINARY = ImageType.RGB;
							break;
						}
						case PDF2Image.PDF2ImageColor.ARGB:
						{
							bINARY = ImageType.ARGB;
							break;
						}
						default:
						{
							bINARY = null;
							break;
						}
					}
					string str = ".bmp";
					bool flag = true;
					this.startPage = Math.Max(this.startPage, 1);
					if (this.endPage < 0)
					{
						this.endPage = this.pdfDocument.getNumberOfPages();
					}
					this.endPage = Math.Min(this.endPage, this.pdfDocument.getNumberOfPages());
					PDFRenderer pDFRenderer = new PDFRenderer(this.pdfDocument);
					List<string> strs = new List<string>();
					for (int i = this.startPage - 1; i < this.endPage; i++)
					{
						BufferedImage bufferedImage = null;
						bufferedImage = (bINARY != null ? pDFRenderer.renderImageWithDPI(i, (float)this.dpi, bINARY) : pDFRenderer.renderImageWithDPI(i, (float)this.dpi));
						string str1 = string.Concat(new object[] { PDF2Image.TempPath, "\\", i + 1, str });
						flag = flag & ImageIOUtil.writeImage(bufferedImage, str1, this.dpi);
						strs.Add(str1);
					}
					this.ConvertJPGToTiff(strs, outputFile);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception);
				}
			}
			finally
			{
				PDFHelper.EmptyDirectory(PDF2Image.TempPath);
			}
			return true;
		}

		public enum PDF2ImageColor
		{
			BINARY,
			GRAY,
			RGB,
			ARGB
		}

		public enum TIFFOutputCompression
		{
			LZW,
			GROUP3,
			GROUP4
		}
	}
}