using java.awt.image;
using java.io;
using java.lang;
using java.util;
using javax.imageio;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.graphics;
using org.apache.pdfbox.pdmodel.graphics.form;
using org.apache.pdfbox.pdmodel.graphics.image;
using System;
using System.Collections;
using System.IO;
using Console = System.Console;
using Exception = System.Exception;
namespace Aquaforest.PDF
{
	public class PDFImageExtractor
	{
		private PDDocument pdfDocument;

		private int imageCounter = 1;

		private string outputFolder = "";

		public PDFImageExtractor(PDFDocument doc)
		{
			this.pdfDocument = doc.PDFBoxDocument;
		}

		private void CheckOutputFolder(string outputFolder)
		{
			this.outputFolder = outputFolder;
			if (!Directory.Exists(outputFolder))
			{
				try
				{
					Directory.CreateDirectory(outputFolder);
				}
				catch
				{
					throw;
				}
			}
		}

		public void ExtractImages(string outputFolder, string prefix, bool addKey)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			try
			{
				this.CheckOutputFolder(outputFolder);
				foreach (PDPage page in this.pdfDocument.getPages())
				{
					try
					{
						this.ProcessResources(page.getResources(), prefix, addKey);
					}
					catch (Exception exception)
					{
					}
				}
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				throw new PDFToolkitException(exception1.Message, exception1);
			}
		}

		public void ExtractImages(string outputFolder, string prefix, bool addKey, int pageNumber)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			try
			{
				this.CheckOutputFolder(outputFolder);
				if (pageNumber > this.pdfDocument.getNumberOfPages())
				{
					Console.WriteLine("Invalid page number.");
				}
				else
				{
					PDPage page = this.pdfDocument.getPage(pageNumber - 1);
					this.ProcessResources(page.getResources(), prefix, addKey);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
		}

		private string GetUniqueFileName(string prefix, string suffix)
		{
			string str = null;
			FileInfo fileInfo = null;
			while (true)
			{
				if ((fileInfo == null ? false : !fileInfo.Exists))
				{
					break;
				}
				string str1 = string.Concat(prefix, "-", this.imageCounter);
				str = Path.Combine(this.outputFolder, str1);
				fileInfo = new FileInfo(string.Concat(str, ".", suffix));
				this.imageCounter = this.imageCounter + 1;
			}
			return str;
		}

		private void ProcessResources(PDResources resources, string prefix, bool addKey)
		{
			if (resources != null)
			{
				Iterator iterator = resources.getXObjectNames().iterator();
				int num = 1;
				while (iterator.hasNext())
				{
					COSName cOSName = (COSName)iterator.next();
					string str = null;
					str = (!addKey ? this.GetUniqueFileName(prefix, ".jpg") : this.GetUniqueFileName(string.Concat(prefix, "_", num), ".jpg"));
					PDResources.OutputFileName = string.Concat(str, ".jpg");
					PDXObject xObject = resources.getXObject(cOSName);
					if (resources.isImageXObject(cOSName))
					{
						PDImageXObject pDImageXObject = (PDImageXObject)xObject;
						str = (!addKey ? this.GetUniqueFileName(prefix, pDImageXObject.getSuffix()) : this.GetUniqueFileName(string.Concat(prefix, "_", num), pDImageXObject.getSuffix()));
						Console.WriteLine(string.Concat("Writing image:", str));
						try
						{
							BufferedImage image = pDImageXObject.getImage();
							ImageIO.write(image, "jpg", new java.io.File(string.Concat(str, ".jpg")));
						}
						catch (Exception exception)
						{
							Console.WriteLine("Could not write image: {0}.{1}{2}", str, Environment.NewLine, exception.Message);
						}
					}
					else if (xObject is PDFormXObject)
					{
						this.ProcessResources(((PDFormXObject)xObject).getResources(), prefix, addKey);
					}
				}
			}
		}
	}
}