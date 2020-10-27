using java.io;
using java.text;
using java.util;
using Microsoft.Win32;
using org.apache.pdfbox.contentstream;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.font;
using org.apache.pdfbox.pdmodel.font.encoding;
using org.apache.pdfbox.pdmodel.graphics;
using org.apache.pdfbox.pdmodel.graphics.color;
using org.apache.pdfbox.pdmodel.graphics.form;
using org.apache.pdfbox.pdmodel.graphics.image;
using org.apache.pdfbox.pdmodel.interactive.action;
using org.apache.pdfbox.pdmodel.interactive.annotation;
using org.apache.pdfbox.pdmodel.interactive.documentnavigation.destination;
using org.apache.pdfbox.pdmodel.interactive.documentnavigation.outline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Console = System.Console;
namespace Aquaforest.PDF
{
	internal class PDFHelper
	{
		private static string SAVE_GRAPHICS_STATE;

		private static string RESTORE_GRAPHICS_STATE;

		private static string CONCATENATE_MATRIX;

		private static string XOBJECT_DO;

		private static string SPACE;

		private static NumberFormat formatDecimal;

		private static Version version;

		internal static bool AddStamp;

		static PDFHelper()
		{
			PDFHelper.SAVE_GRAPHICS_STATE = "q\n";
			PDFHelper.RESTORE_GRAPHICS_STATE = "Q\n";
			PDFHelper.CONCATENATE_MATRIX = "cm\n";
			PDFHelper.XOBJECT_DO = "Do\n";
			PDFHelper.SPACE = " ";
			PDFHelper.formatDecimal = NumberFormat.getNumberInstance(Locale.US);
			PDFHelper.version = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
			PDFHelper.AddStamp = false;
		}

		public PDFHelper()
		{
		}

		internal static void addBookmark(PDOutlineItem bookmarkItem, List<PDFBookmarkItem> bookmarks, PDDocument doc)
		{
			PDOutlineItem firstChild = bookmarkItem.getFirstChild();
			PDFBookmarkItem pDFBookmarkItem = new PDFBookmarkItem(bookmarkItem, doc);
			bookmarks.Add(pDFBookmarkItem);
			while (firstChild != null)
			{
				PDFHelper.addBookmark(firstChild, pDFBookmarkItem.BookmarkItems, doc);
				firstChild = firstChild.getNextSibling();
			}
		}

		internal static bool AddBookmarkTooutline(PDFBookmarkItem bookmarentry, PDDocument document, PDOutlineItem outline)
		{
			bool flag;
			try
			{
				if (bookmarentry.BookMarkPage <= document.getNumberOfPages())
				{
					PDPage page = document.getPage(bookmarentry.BookMarkPage - 1);
					PDPageFitWidthDestination pDPageFitWidthDestination = new PDPageFitWidthDestination();
					pDPageFitWidthDestination.setPage(page);
					outline.setDestination(pDPageFitWidthDestination);
					outline.setTitle(bookmarentry.BookmarkTitle);
				}
				if ((bookmarentry.BookmarkItems == null ? false : bookmarentry.BookmarkItems.Count > 0))
				{
					foreach (PDFBookmarkItem bookmarkItem in bookmarentry.BookmarkItems)
					{
						PDOutlineItem pDOutlineItem = new PDOutlineItem();
						PDFHelper.AddBookmarkTooutline(bookmarkItem, document, pDOutlineItem);
						outline.addLast(pDOutlineItem);
					}
				}
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		internal static PDDocument AddTrialStampIfNecessary(PDDocument pdfFile, bool addJavaScript)
		{
			if (PDFHelper.AddStamp)
			{
				PDFont pDFont = PDTrueTypeFont.load(pdfFile, new java.io.File("C:\\Windows\\Fonts\\Arial.ttf"), new WinAnsiEncoding());
				foreach (PDPage page in pdfFile.getPages())
				{
					PDPageContentStream pDPageContentStream = new PDPageContentStream(pdfFile, page, PDPageContentStream.AppendMode.APPEND, true);
					pDPageContentStream.setFont(pDFont, 14f);
					pDPageContentStream.beginText();
					pDPageContentStream.newLineAtOffset(100f, page.getMediaBox().getHeight() - 100f);
					pDPageContentStream.showText("Created with a trial copy of Aquaforest PDF Toolkit");
					pDPageContentStream.endText();
					pDPageContentStream.close();
				}
			}
			return pdfFile;
		}

		internal static PDDocument AddTrialStampIfNecessary(PDDocument pdfFile)
		{
			if (PDFHelper.AddStamp)
			{
				PDImageXObject pDImageXObject = null;
				foreach (PDPage page in pdfFile.getPages())
				{
					string str = PDFHelper.CreateLogo("Created with a trial copy of Aquaforest PDF Toolkit", page.getRotation());
					if (!string.IsNullOrEmpty(str))
					{
						pDImageXObject = PDImageXObject.createFromFile(str, pdfFile);
						PDPageContentStream pDPageContentStream = new PDPageContentStream(pdfFile, page, PDPageContentStream.AppendMode.APPEND, true);
						pDPageContentStream.drawImage(pDImageXObject, 100f, page.getMediaBox().getHeight() - 100f);
						pDPageContentStream.close();
					}
				}
				pdfFile.getDocumentCatalog().setOpenAction(PDFHelper.GetJavascriptPopup());
			}
			return pdfFile;
		}

		private static void appendRawCommands(OutputStream os, string commands)
		{
			os.write((new UTF8Encoding()).GetBytes(commands));
		}

		private static string ByteArrayToString(byte[] arrInput)
		{
			StringBuilder stringBuilder = new StringBuilder((int)arrInput.Length);
			for (int i = 0; i < (int)arrInput.Length; i++)
			{
				stringBuilder.Append(arrInput[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		private static void Check30DayLicence(int numberOfDays, bool IsTrial)
		{
			string str = "{YTh394jhu9JD31O8923XMOAL1}";
			string str1 = "{5O2sTNBpaXdVVcMw13083pFWkL}";
			string value = "";
			string value1 = "";
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\OpID"))
				{
					value = (string)registryKey.GetValue(str);
					value1 = (string)registryKey.GetValue(str1);
				}
				if ((string.IsNullOrEmpty(value1) ? true : string.IsNullOrEmpty(value)))
				{
					throw new PDFToolkitException();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show("License tampered with!");
				Environment.Exit(1);
			}
			if ((value != "YyNTY4dfltMT456N5M0JDDoyNj9EYzNTBENDg4QTg0QgddsQTMiAQjAzREUc3OEo3RCRjUxN0MD7MjAx0MkM1RkTUxNUNBQURE3QjUyN0ZGRUM" ? true : value1 != "ZBMzBENzYMAxMiAU1NzYwDowMjM0wOTZERkNCMDowMENUJEDtQkxNERc1MOTcvMjjFEREEwOUDNjgxRNUQxQjQxNzY3NTkIxODUFGT9aNG7nwNkIwR"))
			{
				string[] strArrays = PDFHelper.DecryptDate(value).Split(new char[] { ';' });
				value = strArrays[0].ToString();
				strArrays = PDFHelper.DecryptDate(value1).Split(new char[] { ';' });
				value1 = strArrays[0].ToString();
				string str2 = strArrays[1].ToString();
				string str3 = strArrays[2].ToString();
				if (str2 == PDFToolkit.LicenseKey)
				{
					DateTime dateTime = DateTime.Parse(value);
					DateTime dateTime1 = DateTime.Parse(value1);
					if ((DateTime.Compare(DateTime.Now, dateTime) < 0 || DateTime.Compare(dateTime1, DateTime.Now) > 0 ? true : DateTime.Compare(dateTime, dateTime1) < 0))
					{
						MessageBox.Show("License tampered with! Please change your system date to continue.");
						Environment.Exit(1);
					}
					if (DateTime.Compare(DateTime.Now, dateTime1.AddDays((double)numberOfDays)) > 0)
					{
						DateTime dateTime2 = new DateTime(1901, 1, 1);
						try
						{
							using (RegistryKey registryKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\OpID"))
							{
								registryKey1.SetValue(str1, PDFHelper.EncryptDate(string.Format("{0};{1};{2}", new object[] { dateTime2.ToString(), PDFToolkit.LicenseKey, PDFToolkit.LicenseKey, IsTrial.ToString() })), RegistryValueKind.String);
							}
							bool flag = false;
							using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\OpID\\Data", true))
							{
								string[] valueNames = registryKey2.GetValueNames();
								for (int i = 0; i < (int)valueNames.Length; i++)
								{
									string str4 = valueNames[i];
									if ((string)registryKey2.GetValue(str4) != "AxMiAREEwNUQxQjc1MjM0OO1NzYwQGOUQxNzY3NTkkZBTwJEDMzBENRURDNjgxRjFEZERkNCMT9aNG7ncvMjwMDowMowMDtENUzYxNEIxODUwNkITF")
									{
										if (PDFToolkit.LicenseKey == PDFHelper.DecryptDate((string)registryKey2.GetValue(str4)).Split(new char[] { ';' })[1].ToString())
										{
											flag = true;
											break;
										}
									}
								}
							}
							if (!flag)
							{
								using (RegistryKey registryKey3 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\OpID\\Data"))
								{
									Guid guid = Guid.NewGuid();
									registryKey3.SetValue(guid.ToString(), PDFHelper.EncryptDate(string.Format("{0};{1};{2}", dateTime2.ToString(), PDFToolkit.LicenseKey, IsTrial.ToString())));
								}
							}
						}
						catch (Exception exception1)
						{
							MessageBox.Show(exception1.Message);
							Environment.Exit(1);
						}
						MessageBox.Show(string.Format("This message is being displayed because either your {0} day license has expired or the license has been tampered with.\n\nIf you would like to purchase a new license then please contact \"admin@aquaforest.com\".", numberOfDays));
						Environment.Exit(1);
					}
				}
				else
				{
					using (RegistryKey registryKey4 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\OpID", true))
					{
						registryKey4.SetValue(str1, PDFHelper.EncryptDate(string.Format("{0};{1};{2}", DateTime.Now, PDFToolkit.LicenseKey, IsTrial.ToString())));
					}
				}
			}
			else
			{
				using (RegistryKey registryKey5 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\OpID", true))
				{
					registryKey5.SetValue(str1, PDFHelper.EncryptDate(string.Format("{0};{1};{2}", DateTime.Now, PDFToolkit.LicenseKey, IsTrial.ToString())));
				}
			}
			try
			{
				using (RegistryKey registryKey6 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\OpID"))
				{
					DateTime now = DateTime.Now;
					registryKey6.SetValue(str, PDFHelper.EncryptDate(now.ToString()), RegistryValueKind.String);
				}
			}
			catch (Exception exception2)
			{
				MessageBox.Show(exception2.Message);
				Environment.Exit(1);
			}
		}

		internal static void CheckLicense()
		{
			string licenseKey = PDFToolkit.LicenseKey;

		}

		internal static void CheckOutputFolder(string outputFolder)
		{
			if (!Directory.Exists(outputFolder))
			{
				try
				{
					Directory.CreateDirectory(outputFolder);
				}
				catch
				{
					throw new PDFToolkitException("Output Path is Invalid");
				}
			}
		}

		internal static string convertStreamToString(InputStream input)
		{
			Scanner scanner = (new Scanner(input)).useDelimiter("\\A");
			return (scanner.hasNext() ? scanner.next() : "");
		}

		private static string CreateLogo(string stampText, int angle)
		{
			RotateFlipType rotateFlipType;
			string str;
			string str1 = "";
			try
			{
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
				using (Bitmap bitmap = new Bitmap(1, 1))
				{
					int num1 = 0;
					int num2 = 0;
					System.Drawing.Font font = new System.Drawing.Font("Times New Roman", 10f);
					using (Graphics graphic = Graphics.FromImage(bitmap))
					{
						SizeF sizeF = graphic.MeasureString(stampText, font);
						num1 = Math.Max((int)sizeF.Width, 250);
						sizeF = graphic.MeasureString(stampText, font);
						num2 = Math.Max((int)sizeF.Height, 30);
						using (Bitmap bitmap1 = new Bitmap(bitmap, new Size(num1, num2)))
						{
							using (Graphics graphic1 = Graphics.FromImage(bitmap1))
							{
								graphic1.Clear(Color.Transparent);
								graphic1.InterpolationMode = InterpolationMode.HighQualityBilinear;
								graphic1.SmoothingMode = SmoothingMode.AntiAlias;
								graphic1.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
								graphic1.DrawString(stampText, font, new SolidBrush(Color.Black), 0f, 0f);
								graphic1.Flush();
							}
							if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
							{
								bitmap1.RotateFlip(rotateFlipType);
							}
							str1 = Path.Combine(Path.GetTempPath(), "aquaforest\\pdftoolkit\\stamp.png");
							PDFHelper.CheckOutputFolder(Path.GetDirectoryName(str1));
							bitmap1.Save(str1, ImageFormat.Png);
						}
					}
				}
				str = str1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				Console.WriteLine(string.Concat(exception.Message, Environment.NewLine, exception.StackTrace));
				str = str1;
			}
			return str;
		}

		private static string DecryptDate(string d)
		{
			int length = (4 - d.Length % 4) % 4;
			d = d.PadRight(d.Length + length, '=');
			byte[] numArray = Convert.FromBase64String(d);
			return System.Text.Encoding.ASCII.GetString(numArray);
		}

		internal static void DisplayTrialPopupIfNecessary()
		{
			//bool flag;
			//bool flag1;
			//bool flag2;
			//if (string.IsNullOrEmpty(PDFToolkit.LicenseKey))
			//{
			//	MessageBox.Show("You need a valid license key to use the PDF Toolkit. Apply the license key using the command below\n  PDFToolkit.LicenseKey = \"key\";");
			//	Environment.Exit(1);
			//}
			//PDFHelper.CheckLicense();
			//string str = PDFHelper.version.Minor.ToString();
			//string str1 = PDFHelper.key.Version.Minor.ToString();
			//bool flag3 = false;
			//if (int.Parse(str.Substring(0, 1)) > int.Parse(str1.Substring(0, 1)))
			//{
			//	flag3 = true;
			//}
			//if (PDFHelper.key.IsTrial)
			//{
			//	flag = false;
			//}
			//else
			//{
			//	flag = (PDFHelper.version.Major > PDFHelper.key.Version.Major ? true : PDFHelper.version.Major == PDFHelper.key.Version.Major & flag3);
			//}
			//if (!flag)
			//{
			//	if (!PDFHelper.key.IsTrial)
			//	{
			//		flag1 = false;
			//	}
			//	else
			//	{
			//		flag1 = (!PDFHelper.key.ExpiryStored ? true : DateTime.Compare(PDFHelper.key.Expiry, DateTime.Now) <= 0);
			//	}
			//	if (!flag1)
			//	{
			//		if (PDFHelper.key.IsTrial)
			//		{
			//			flag2 = false;
			//		}
			//		else
			//		{
			//			flag2 = (!PDFHelper.key.ExpiryStored ? false : DateTime.Compare(PDFHelper.key.Expiry, DateTime.Now) < 0);
			//		}
			//		if (flag2)
			//		{
			//			MessageBox.Show(string.Format("{0}{1}", "This message is being displayed because your time limited license has expired.\n", "If you would like to purchase a new license then please contact \"admin@aquaforest.com\"."));
			//			PDFHelper.AddStamp = true;
			//		}
			//	}
			//	else
			//	{
			//		MessageBox.Show(string.Format("{0}{1}{2}", "This message is being displayed because you have a trial license.\n", "If you would like to purchase the full version or trial the product without\n", "this message being displayed then please contact \"admin@aquaforest.com\"."));
			//		PDFHelper.AddStamp = true;
			//	}
			//}
			//else
			//{
			//	MessageBox.Show(string.Format("{0}{1}", "The license you are using is for an earlier version of the product. This version will run in trial mode.\n", "Please contact your software supplier or \"admin@aquaforest.com\" for an upgrade."));
			//	PDFHelper.key.IsTrial = true;
			//	PDFHelper.AddStamp = true;
			//}
		}

		private static void drawXObject(PDImageXObject xobject, PDResources resources, OutputStream os, float x, float y, float width, float height)
		{
			COSName cOSName = resources.@add(xobject);
			PDFHelper.appendRawCommands(os, PDFHelper.SAVE_GRAPHICS_STATE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((double)width));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((long)0));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((long)0));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((double)height));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((double)x));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.formatDecimal.format((double)y));
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.CONCATENATE_MATRIX);
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, "/");
			PDFHelper.appendRawCommands(os, cOSName.getName());
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.XOBJECT_DO);
			PDFHelper.appendRawCommands(os, PDFHelper.SPACE);
			PDFHelper.appendRawCommands(os, PDFHelper.RESTORE_GRAPHICS_STATE);
		}

		internal static void EmptyDirectory(string inputDir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(inputDir);
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					fileInfo.Delete();
				}
				catch (Exception exception)
				{
				}
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int j = 0; j < (int)directories.Length; j++)
			{
				DirectoryInfo directoryInfo1 = directories[j];
				try
				{
					directoryInfo1.Delete(true);
				}
				catch (Exception exception1)
				{
				}
			}
			if (Directory.Exists(inputDir))
			{
				try
				{
					Directory.Delete(inputDir);
				}
				catch (Exception exception2)
				{
				}
			}
		}

		private static string EncryptDate(string d)
		{
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(d.ToString());
			byte[] numArray = (new SHA256Managed()).ComputeHash(bytes);
			string str = string.Concat(d.ToString(), ";", PDFHelper.ByteArrayToString(numArray));
			byte[] bytes1 = System.Text.Encoding.ASCII.GetBytes(str);
			char[] chrArray = new char[] { '=' };
			return Convert.ToBase64String(bytes1).TrimEnd(chrArray);
		}

		internal static PDColor GetColor(PDFColor color)
		{
			PDColor pDColor;
			try
			{
				switch (color)
				{
					case PDFColor.Black:
					{
						pDColor = new PDColor(new float[3], PDDeviceRGB.INSTANCE);
						return pDColor;
					}
					case PDFColor.White:
					{
						pDColor = new PDColor(new float[] { 1f, 1f, 1f }, PDDeviceRGB.INSTANCE);
						return pDColor;
					}
					case PDFColor.Red:
					{
						pDColor = new PDColor(new float[] { 1f, default(float), default(float) }, PDDeviceRGB.INSTANCE);
						return pDColor;
					}
					case PDFColor.Blue:
					{
						pDColor = new PDColor(new float[] { default(float), default(float), 1f }, PDDeviceRGB.INSTANCE);
						return pDColor;
					}
					case PDFColor.Green:
					{
						pDColor = new PDColor(new float[] { default(float), 1f, default(float) }, PDDeviceRGB.INSTANCE);
						return pDColor;
					}
					case PDFColor.Yellow:
					{
						pDColor = new PDColor(new float[] { 1f, 1f, default(float) }, PDDeviceRGB.INSTANCE);
						return pDColor;
					}
				}
				pDColor = new PDColor(new float[3], PDDeviceRGB.INSTANCE);
			}
			catch (Exception exception)
			{
				pDColor = new PDColor(new float[3], PDDeviceRGB.INSTANCE);
			}
			return pDColor;
		}

		internal static PDActionJavaScript GetJavascriptPopup()
		{
			return new PDActionJavaScript("app.alert( {cMsg: 'This document was generated with a trial license of Aquaforest PDF Toolkit. Documents generated with the full license do not generate this message.', nIcon: 0, nType: 0, cTitle: 'Aquaforest PDF Toolkit' } );");
		}

		internal static PDFRectangle GetPDFRectangle(PageSize pageSize)
		{
			PDFRectangle pDFRectangle;
			switch (pageSize)
			{
				case PageSize.A0:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A0.getWidth(), PDRectangle.A0.getHeight());
					break;
				}
				case PageSize.A1:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A1.getWidth(), PDRectangle.A1.getHeight());
					break;
				}
				case PageSize.A2:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A2.getWidth(), PDRectangle.A2.getHeight());
					break;
				}
				case PageSize.A3:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A3.getWidth(), PDRectangle.A3.getHeight());
					break;
				}
				case PageSize.A4:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A4.getWidth(), PDRectangle.A4.getHeight());
					break;
				}
				case PageSize.A5:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A5.getWidth(), PDRectangle.A5.getHeight());
					break;
				}
				case PageSize.A6:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.A6.getWidth(), PDRectangle.A6.getHeight());
					break;
				}
				case PageSize.Letter:
				{
					pDFRectangle = new PDFRectangle(PDRectangle.LETTER.getWidth(), PDRectangle.LETTER.getHeight());
					break;
				}
				default:
				{
					goto case PageSize.Letter;
				}
			}
			return pDFRectangle;
		}

		private static string GetResourceLocation(string images)
		{
			string str;
			try
			{
				string str1 = string.Concat(Path.GetTempPath(), "aquaforest\\pdftoolkit\\rubber", images);
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(string.Concat("Aquaforest.PDF.rubberstamps.", images));
				if (!Directory.Exists(Path.GetDirectoryName(str1)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(str1));
				}
				using (FileStream fileStream = System.IO.File.Create(str1, (int)manifestResourceStream.Length))
				{
					byte[] numArray = new byte[checked(manifestResourceStream.Length)];
					manifestResourceStream.Read(numArray, 0, (int)numArray.Length);
					fileStream.Write(numArray, 0, (int)numArray.Length);
				}
				str = str1;
			}
			catch
			{
				str = null;
			}
			return str;
		}

		public static PDAppearanceDictionary GetRubberStampAppearance(PDFDocument document, PDFRectangle rect, string subType)
		{
			PDAppearanceDictionary pDAppearanceDictionary;
			string empty = string.Empty;
			empty = (!System.IO.File.Exists(subType) ? PDFHelper.GetResourceLocation(subType) : subType);
			if (empty == null)
			{
				Console.WriteLine("Cant find rubber stamp");
				pDAppearanceDictionary = null;
			}
			else
			{
				PDImageXObject pDImageXObject = PDImageXObject.createFromFileByContent(new java.io.File(empty), document.PDFBoxDocument);
				float lowerLeftX = rect.PDFBoxRectangle.getLowerLeftX();
				float lowerLeftY = rect.PDFBoxRectangle.getLowerLeftY();
				rect.PDFBoxRectangle.getWidth();
				rect.PDFBoxRectangle.getHeight();
				rect.PDFBoxRectangle.getHeight();
				rect.PDFBoxRectangle.getWidth();
				PDFormXObject pDFormXObject = new PDFormXObject(document.PDFBoxDocument);
				pDFormXObject.setResources(new PDResources());
				pDFormXObject.setBBox(rect.PDFBoxRectangle);
				pDFormXObject.setFormType(1);
				OutputStream outputStream = pDFormXObject.getStream().createOutputStream();
				PDFHelper.drawXObject(pDImageXObject, pDFormXObject.getResources(), outputStream, lowerLeftX, lowerLeftY, (float)pDImageXObject.getWidth(), (float)pDImageXObject.getHeight());
				outputStream.close();
				PDAppearanceStream pDAppearanceStream = new PDAppearanceStream(pDFormXObject.getCOSObject());
				PDAppearanceDictionary pDAppearanceDictionary1 = new PDAppearanceDictionary(new COSDictionary());
				pDAppearanceDictionary1.setNormalAppearance(pDAppearanceStream);
				pDAppearanceDictionary = pDAppearanceDictionary1;
			}
			return pDAppearanceDictionary;
		}

		private static byte[] StrToByteArray(string s)
		{
			s = s.Replace(" ", string.Empty);
			byte[] num = new byte[s.Length / 2];
			for (int i = 0; i < s.Length; i = i + 2)
			{
				num[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
			}
			return num;
		}
	}
}