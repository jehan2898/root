using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.graphics.image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
namespace Aquaforest.PDF
{
	public class ImageToPDFConverter
	{
		private static string tempDir;

		private PDDocument pdfFile;

		private string fileName;

		private string outputFile;

		public bool Debug
		{
			get;
			set;
		}

		static ImageToPDFConverter()
		{
			string tempPath = Path.GetTempPath();
			Guid guid = Guid.NewGuid();
			ImageToPDFConverter.tempDir = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString()));
		}

		public ImageToPDFConverter(string fileName, string outputFile)
		{
			this.fileName = fileName;
			this.outputFile = outputFile;
		}

		public static void BmpToPng(Bitmap bmp, string filename)
		{
			EncoderParameters encoderParameter = new EncoderParameters(1);
			encoderParameter.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);
			bmp.Save(filename, ImageToPDFConverter.GetEncoder(ImageFormat.Jpeg), encoderParameter);
		}

		public bool ConvertImageToPDF()
		{
			bool flag;
			List<string> strs = new List<string>()
			{
				".jpeg",
				".png",
				".bmp",
				".wbmp",
				".tif",
				".tiff"
			};
			try
			{
				if (this.Debug)
				{
					Console.WriteLine(string.Concat("Converting ", this.fileName, " to pdf."));
				}
				PDFHelper.CheckOutputFolder(ImageToPDFConverter.tempDir);
				this.pdfFile = new PDDocument();
				if ((this.fileName.ToLower().EndsWith(".jpg") || this.fileName.ToLower().EndsWith(".bmp") ? true : this.fileName.ToLower().EndsWith(".gif")))
				{
					string str = "temp.jpg";
					if (Path.GetExtension(this.fileName).ToLower() == ".bmp")
					{
						Image image = Image.FromFile(this.fileName);
						ImageToPDFConverter.BmpToPng(new Bitmap(image), str);
					}
					else if (Path.GetExtension(this.fileName).ToLower() != ".gif")
					{
						str = this.fileName;
					}
					else
					{
						Image image1 = Image.FromFile(this.fileName);
						image1.Save(str, ImageFormat.Jpeg);
					}
					if (!this.ConvertJPGToPDF(str))
					{
						flag = false;
						return flag;
					}
				}
				else if ((this.fileName.ToLower().EndsWith(".tif") ? true : this.fileName.ToLower().EndsWith(".tiff")))
				{
					int compressionType = ImageToPDFConverter.GetCompressionType(this.fileName);
					if ((compressionType <= 1 ? true : compressionType >= 5))
					{
						if (this.Debug)
						{
							Console.WriteLine(string.Concat(this.fileName, " cant be converted to pdf directly, so we will first convert it to a jpg."));
						}
						string[] jpeg = ImageToPDFConverter.ConvertTiffToJpeg(this.fileName);
						int num = 0;
						while (num < (int)jpeg.Length)
						{
							if (this.ConvertJPGToPDF(jpeg[num]))
							{
								num++;
							}
							else
							{
								flag = false;
								return flag;
							}
						}
					}
					else if (!this.ConvertTiffToPDF(this.fileName))
					{
						flag = false;
						return flag;
					}
				}
				else if (!strs.Contains(Path.GetExtension(this.fileName).ToLower()))
				{
					flag = false;
					return flag;
				}
				else if (!this.ConvertOthersToPDF(this.fileName))
				{
					flag = false;
					return flag;
				}
				this.Save();
				if (this.Debug)
				{
					Console.WriteLine(string.Concat("Converted ", this.fileName, " to ", this.outputFile));
				}
				PDFHelper.EmptyDirectory(ImageToPDFConverter.tempDir);
				flag = true;
				return flag;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return flag;
		}

		private bool ConvertJPGToPDF(string imagePath)
		{
			try
			{
				PDPage pDPage = new PDPage();
				this.pdfFile.addPage(pDPage);
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(imagePath, this.pdfFile);
				PDRectangle pDRectangle = new PDRectangle((float)(pDImageXObject.getWidth() + 40), (float)(pDImageXObject.getHeight() + 40));
				pDPage.setMediaBox(pDRectangle);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile, pDPage);
				pDPageContentStream.drawImage(pDImageXObject, 20f, 20f);
				pDPageContentStream.close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return true;
		}

		private bool ConvertOthersToPDF(string imagePath)
		{
			try
			{
				PDPage pDPage = new PDPage();
				string str = "temp.png";
				this.pdfFile.addPage(pDPage);
				if (Path.GetExtension(imagePath).ToLower() == ".bmp")
				{
					ImageToPDFConverter.BmpToPng(new Bitmap(Image.FromFile(imagePath)), str);
				}
				else if (Path.GetExtension(imagePath).ToLower() == ".png")
				{
					str = imagePath;
				}
				else
				{
					Image.FromFile(imagePath).Save(str, ImageFormat.Png);
				}
				PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile);
				PDRectangle pDRectangle = new PDRectangle((float)(pDImageXObject.getWidth() + 40), (float)(pDImageXObject.getHeight() + 40));
				pDPage.setMediaBox(pDRectangle);
				PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile, pDPage);
				pDPageContentStream.drawImage(pDImageXObject, 20f, 20f);
				pDPageContentStream.close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return true;
		}

		internal static string[] ConvertTiffToJpeg(string fileName)
		{
			string[] strArrays;
			using (Image image = Image.FromFile(fileName))
			{
				FrameDimension frameDimension = new FrameDimension(image.FrameDimensionsList[0]);
				int frameCount = image.GetFrameCount(frameDimension);
				string[] strArrays1 = new string[frameCount];
				for (int i = 0; i < frameCount; i++)
				{
					image.SelectActiveFrame(frameDimension, i);
					using (Bitmap bitmap = new Bitmap(image))
					{
						strArrays1[i] = string.Format("{0}\\{1}{2}.png", ImageToPDFConverter.tempDir, Path.GetFileNameWithoutExtension(fileName), i);
						ImageToPDFConverter.BmpToPng(bitmap, strArrays1[i]);
					}
				}
				strArrays = strArrays1;
			}
			return strArrays;
		}

		private bool ConvertTiffToPDF(string imagePath)
		{
			try
			{
				string[] strArrays = ImageToPDFConverter.SplitTiff(imagePath);
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					PDPage pDPage = new PDPage();
					this.pdfFile.addPage(pDPage);
					PDImageXObject pDImageXObject = PDImageXObject.createFromFile(str, this.pdfFile);
					PDRectangle pDRectangle = new PDRectangle((float)(pDImageXObject.getWidth() + 40), (float)(pDImageXObject.getHeight() + 40));
					pDPage.setMediaBox(pDRectangle);
					PDPageContentStream pDPageContentStream = new PDPageContentStream(this.pdfFile, pDPage);
					pDPageContentStream.drawImage(pDImageXObject, 20f, 20f);
					pDPageContentStream.close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return true;
		}

		internal static int GetCompressionType(string imagepath)
		{
			Image image = Image.FromFile(imagepath);
			int num = Array.IndexOf<int>(image.PropertyIdList, 259);
			PropertyItem propertyItems = image.PropertyItems[num];
			return BitConverter.ToInt16(propertyItems.Value, 0);
		}

		internal static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo;
			ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
			int num = 0;
			while (true)
			{
				if (num < (int)imageDecoders.Length)
				{
					ImageCodecInfo imageCodecInfo1 = imageDecoders[num];
					if (imageCodecInfo1.FormatID != format.Guid)
					{
						num++;
					}
					else
					{
						imageCodecInfo = imageCodecInfo1;
						break;
					}
				}
				else
				{
					imageCodecInfo = null;
					break;
				}
			}
			return imageCodecInfo;
		}

		private static ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			ImageCodecInfo imageCodecInfo;
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			int num = 0;
			while (true)
			{
				if (num >= (int)imageEncoders.Length)
				{
					imageCodecInfo = null;
					break;
				}
				else if (imageEncoders[num].MimeType != mimeType)
				{
					num++;
				}
				else
				{
					imageCodecInfo = imageEncoders[num];
					break;
				}
			}
			return imageCodecInfo;
		}
        public bool MergeFolderOfImagesToPDF()
        {
            bool debug = this.Debug;
            if (debug)
            {
                Console.WriteLine("Merging Folder Of Images " + this.fileName + " to pdf.");
            }
            List<string> acceptedImages = new List<string>
    {
        ".jpeg",
        ".png",
        ".bmp",
        ".wbmp",
        ".tif",
        ".tiff",
        ".gif"
    };
            IEnumerable<string> enumerable = from s in Directory.GetFiles(this.fileName, "*.*", SearchOption.TopDirectoryOnly)
                                             where acceptedImages.Contains(Path.GetExtension(s).ToLower())
                                             select s;
            bool result;
            try
            {
                PDFHelper.CheckOutputFolder(ImageToPDFConverter.tempDir);
                this.pdfFile = new PDDocument();
                foreach (string current in enumerable)
                {
                    bool flag = current.ToLower().EndsWith(".jpg") || current.ToLower().EndsWith(".gif") || current.ToLower().EndsWith(".bmp");
                    if (flag)
                    {
                        string text = "temp.jpg";
                        bool flag2 = Path.GetExtension(current).ToLower() == ".bmp";
                        if (flag2)
                        {
                            Image original = Image.FromFile(current);
                            Bitmap bmp = new Bitmap(original);
                            ImageToPDFConverter.BmpToPng(bmp, text);
                        }
                        else
                        {
                            bool flag3 = Path.GetExtension(current).ToLower() == ".gif";
                            if (flag3)
                            {
                                Image image = Image.FromFile(current);
                                image.Save(text, ImageFormat.Jpeg);
                            }
                            else
                            {
                                text = current;
                            }
                        }
                        bool flag4 = !this.ConvertJPGToPDF(text);
                        if (flag4)
                        {
                            result = false;
                            return result;
                        }
                    }
                    else
                    {
                        bool flag5 = current.ToLower().EndsWith(".tif") || current.ToLower().EndsWith(".tiff");
                        if (flag5)
                        {
                            int compressionType = ImageToPDFConverter.GetCompressionType(current);
                            bool flag6 = compressionType > 1 && compressionType < 5;
                            if (flag6)
                            {
                                bool flag7 = !this.ConvertTiffToPDF(current);
                                if (flag7)
                                {
                                    result = false;
                                    return result;
                                }
                            }
                            else
                            {
                                bool debug2 = this.Debug;
                                if (debug2)
                                {
                                    Console.WriteLine(current + " cant be converted to pdf directly, so we will first convert it to a jpg.");
                                }
                                string[] array = ImageToPDFConverter.ConvertTiffToJpeg(current);
                                string[] array2 = array;
                                for (int i = 0; i < array2.Length; i++)
                                {
                                    string imagePath = array2[i];
                                    bool flag8 = !this.ConvertJPGToPDF(imagePath);
                                    if (flag8)
                                    {
                                        result = false;
                                        return result;
                                    }
                                }
                            }
                        }
                        else
                        {
                            bool flag9 = acceptedImages.Contains(Path.GetExtension(current).ToLower());
                            if (!flag9)
                            {
                                Console.WriteLine("Input Files Not Found.");
                                result = false;
                                return result;
                            }
                            bool flag10 = !this.ConvertOthersToPDF(current);
                            if (flag10)
                            {
                                result = false;
                                return result;
                            }
                        }
                    }
                }
                bool flag11 = this.pdfFile.getNumberOfPages() > 0;
                if (flag11)
                {
                    this.Save();
                }
                else
                {
                    this.pdfFile.close();
                }
                bool debug3 = this.Debug;
                if (debug3)
                {
                    Console.WriteLine(this.fileName + " folder merged to " + this.outputFile);
                }
            }
            catch (Exception ex)
            {
                PDFHelper.EmptyDirectory(ImageToPDFConverter.tempDir);
                throw new PDFToolkitException(ex.Message, ex.InnerException);
            }
            PDFHelper.EmptyDirectory(ImageToPDFConverter.tempDir);
            result = true;
            return result;
        }


        private void Save()
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			if (PDFHelper.AddStamp)
			{
				this.pdfFile = PDFHelper.AddTrialStampIfNecessary(this.pdfFile);
			}
			try
			{
				this.pdfFile.save(this.outputFile);
				this.pdfFile.close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception.InnerException);
			}
		}

		internal static string[] SplitTiff(string fileName)
		{
			string[] strArrays;
			using (Image image = Image.FromFile(fileName))
			{
				FrameDimension frameDimension = new FrameDimension(image.FrameDimensionsList[0]);
				int frameCount = image.GetFrameCount(frameDimension);
				string[] strArrays1 = new string[frameCount];
				ImageCodecInfo encoder = ImageToPDFConverter.GetEncoder(ImageFormat.Tiff);
				EncoderParameters encoderParameter = new EncoderParameters(2);
				encoderParameter.Param[0] = new EncoderParameter(Encoder.Compression, (long)4);
				encoderParameter.Param[1] = new EncoderParameter(Encoder.ColorDepth, (long)1);
				for (int i = 0; i < frameCount; i++)
				{
					image.SelectActiveFrame(frameDimension, i);
					using (Bitmap bitmap = new Bitmap(image))
					{
						strArrays1[i] = string.Format("{0}\\{1}{2}.tif", ImageToPDFConverter.tempDir, Path.GetFileNameWithoutExtension(fileName), i);
						bitmap.Save(strArrays1[i], encoder, encoderParameter);
					}
				}
				strArrays = strArrays1;
			}
			return strArrays;
		}
	}
}