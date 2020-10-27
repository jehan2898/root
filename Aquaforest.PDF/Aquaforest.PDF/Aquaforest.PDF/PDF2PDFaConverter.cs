using com.sun.org.apache.xalan.@internal.xsltc.trax;
using com.sun.org.apache.xerces.@internal.jaxp;
using java.io;
using java.lang;
using java.util;
using Microsoft.Win32;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.documentinterchange.logicalstructure;
using org.apache.pdfbox.pdmodel.encryption;
using org.apache.pdfbox.pdmodel.font;
using org.apache.pdfbox.pdmodel.graphics;
using org.apache.pdfbox.pdmodel.graphics.color;
using org.apache.pdfbox.pdmodel.graphics.image;
using org.apache.xmpbox;
using org.apache.xmpbox.schema;
using org.apache.xmpbox.xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using Console = System.Console;
using Exception = System.Exception;
namespace Aquaforest.PDF
{
	public class PDF2PDFaConverter
	{
		private PDDocument doc;

		private string tempFileName;

		private string inputFileName;

		private bool copyUnvalidated = false;

		private bool validatePDF = false;

		private bool overwrite = false;

		private string outPutFileName;

		private string iccString;

		private InputStream colorProfile = null;

		private int PDFAFlavour = 0;

		private int pdfaversion = 1;

		private string ConformanceLevel = "B";

		public bool CopyUnvalidated
		{
			get
			{
				return this.copyUnvalidated;
			}
			set
			{
				this.copyUnvalidated = value;
			}
		}

		public bool Overwrite
		{
			get
			{
				return this.overwrite;
			}
			set
			{
				this.overwrite = value;
			}
		}

		public bool ValidatePDF
		{
			get
			{
				return this.validatePDF;
			}
			set
			{
				this.validatePDF = value;
			}
		}

		public PDF2PDFaConverter(string fileName, string output, AquaforestPDFAFlavour PDFAFlavour)
		{
			try
			{
				Environment.SetEnvironmentVariable("org.apache.commons.logging.Log", "org.apache.commons.logging.impl.NoOpLog");
				int pDFAFlavour = (int)PDFAFlavour;
				this.SetFlavour(pDFAFlavour);
				this.PDFAFlavour = pDFAFlavour;
				this.outPutFileName = output;
				this.inputFileName = fileName;
				string tempPath = Path.GetTempPath();
				Guid guid = Guid.NewGuid();
				this.tempFileName = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString(), "\\", Path.GetFileName(output)));
				string directoryName = Path.GetDirectoryName(this.tempFileName);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				string str = Path.Combine(directoryName, "password.pdf");
				System.IO.File.Copy(this.inputFileName, str, true);
				PDDocument pDDocument = PDDocument.load(new java.io.File(str));
				this.doc = new PDDocument();
				switch (this.pdfaversion)
				{
					case 1:
					{
						if (this.doc.getDocument().getVersion() < 1.4f)
						{
							this.doc.getDocument().setVersion(1.4f);
						}
						break;
					}
					case 2:
					{
						if (this.doc.getDocument().getVersion() < 1.7f)
						{
							this.doc.getDocument().setVersion(1.7f);
						}
						break;
					}
					case 3:
					{
						if (this.doc.getDocument().getVersion() < 1.7f)
						{
							this.doc.getDocument().setVersion(1.7f);
						}
						break;
					}
				}
				foreach (PDPage page in pDDocument.getPages())
				{
					this.doc.addPage(page);
				}
				this.doc.setDocumentInformation(pDDocument.getDocumentInformation());
				this.doc.getDocumentCatalog().setDocumentOutline(pDDocument.getDocumentCatalog().getDocumentOutline());
				this.doc.getDocumentCatalog().setAcroForm(pDDocument.getDocumentCatalog().getAcroForm());
				this.doc.getDocumentCatalog().setLanguage(pDDocument.getDocumentCatalog().getLanguage());
				this.doc.getDocumentCatalog().setMetadata(pDDocument.getDocumentCatalog().getMetadata());
				this.doc.getDocumentCatalog().setPageLabels(pDDocument.getDocumentCatalog().getPageLabels());
				this.doc.getDocumentCatalog().setViewerPreferences(pDDocument.getDocumentCatalog().getViewerPreferences());
				this.doc.getDocumentCatalog().setPageMode(pDDocument.getDocumentCatalog().getPageMode());
				this.doc.getDocumentCatalog().setPageLayout(pDDocument.getDocumentCatalog().getPageLayout());
				this.doc.save(this.tempFileName);
				this.doc.close();
				this.doc = PDDocument.load(new java.io.File(this.tempFileName));
				if (pDDocument != null)
				{
					pDDocument.close();
					pDDocument = null;
				}
				try
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Aquaforest.PDF.sRGB_IEC61966-2-1_black_scaled.icc");
					using (FileStream fileStream = System.IO.File.Create("sRGB_IEC61966-2-1_black_scaled.icc", (int)manifestResourceStream.Length))
					{
						byte[] numArray = new byte[checked(manifestResourceStream.Length)];
						manifestResourceStream.Read(numArray, 0, (int)numArray.Length);
						fileStream.Write(numArray, 0, (int)numArray.Length);
					}
					manifestResourceStream = executingAssembly.GetManifestResourceStream("Aquaforest.PDF.font.xml");
					using (FileStream fileStream1 = System.IO.File.Create("font.xml", (int)manifestResourceStream.Length))
					{
						byte[] numArray1 = new byte[checked(manifestResourceStream.Length)];
						manifestResourceStream.Read(numArray1, 0, (int)numArray1.Length);
						fileStream1.Write(numArray1, 0, (int)numArray1.Length);
					}
				}
				catch
				{
				}
				this.iccString = "sRGB_IEC61966-2-1_black_scaled.icc";
			}
			catch (Exception exception)
			{
				Environment.Exit(104);
			}
		}

		public PDF2PDFaConverter(string fileName, string output, string password, AquaforestPDFAFlavour PDFAFlavour)
		{
			try
			{
				int pDFAFlavour = (int)PDFAFlavour;
				this.SetFlavour(pDFAFlavour);
				this.PDFAFlavour = pDFAFlavour;
				this.outPutFileName = output;
				this.inputFileName = fileName;
				string tempPath = Path.GetTempPath();
				Guid guid = Guid.NewGuid();
				this.tempFileName = Path.Combine(tempPath, string.Concat("aquaforest\\pdftoolkit\\", guid.ToString(), "\\", Path.GetFileName(output)));
				string str = Path.Combine(Path.GetDirectoryName(this.tempFileName), "password.pdf");
				string directoryName = Path.GetDirectoryName(this.tempFileName);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				System.IO.File.Copy(this.inputFileName, str, true);
				PDDocument pDDocument = PDDocument.load(new java.io.File(str));
				if (pDDocument.isEncrypted())
				{
					pDDocument.setAllSecurityToBeRemoved(true);
				}
				pDDocument.save(this.tempFileName);
				this.doc = new PDDocument();
				switch (this.pdfaversion)
				{
					case 1:
					{
						if (this.doc.getDocument().getVersion() < 1.4f)
						{
							this.doc.getDocument().setVersion(1.4f);
						}
						break;
					}
					case 2:
					{
						if (this.doc.getDocument().getVersion() < 1.7f)
						{
							this.doc.getDocument().setVersion(1.7f);
						}
						break;
					}
					case 3:
					{
						if (this.doc.getDocument().getVersion() < 1.7f)
						{
							this.doc.getDocument().setVersion(1.7f);
						}
						break;
					}
				}
				foreach (PDPage page in pDDocument.getPages())
				{
					this.doc.addPage(page);
				}
				this.doc.setDocumentInformation(pDDocument.getDocumentInformation());
				this.doc.getDocumentCatalog().setDocumentOutline(pDDocument.getDocumentCatalog().getDocumentOutline());
				this.doc.getDocumentCatalog().setAcroForm(pDDocument.getDocumentCatalog().getAcroForm());
				this.doc.getDocumentCatalog().setLanguage(pDDocument.getDocumentCatalog().getLanguage());
				this.doc.getDocumentCatalog().setMetadata(pDDocument.getDocumentCatalog().getMetadata());
				this.doc.getDocumentCatalog().setPageLabels(pDDocument.getDocumentCatalog().getPageLabels());
				this.doc.getDocumentCatalog().setViewerPreferences(pDDocument.getDocumentCatalog().getViewerPreferences());
				this.doc.getDocumentCatalog().setPageMode(pDDocument.getDocumentCatalog().getPageMode());
				this.doc.getDocumentCatalog().setPageLayout(pDDocument.getDocumentCatalog().getPageLayout());
				this.doc.save(this.tempFileName);
				this.doc.close();
				this.doc = PDDocument.load(new java.io.File(this.tempFileName));
				pDDocument.close();
				pDDocument = null;
				try
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Aquaforest.PDF.sRGB_IEC61966-2-1_black_scaled.icc");
					using (FileStream fileStream = System.IO.File.Create("sRGB_IEC61966-2-1_black_scaled.icc", (int)manifestResourceStream.Length))
					{
						byte[] numArray = new byte[checked(manifestResourceStream.Length)];
						manifestResourceStream.Read(numArray, 0, (int)numArray.Length);
						fileStream.Write(numArray, 0, (int)numArray.Length);
					}
					manifestResourceStream = executingAssembly.GetManifestResourceStream("Aquaforest.PDF.font.xml");
					using (FileStream fileStream1 = System.IO.File.Create("font.xml", (int)manifestResourceStream.Length))
					{
						byte[] numArray1 = new byte[checked(manifestResourceStream.Length)];
						manifestResourceStream.Read(numArray1, 0, (int)numArray1.Length);
						fileStream1.Write(numArray1, 0, (int)numArray1.Length);
					}
				}
				catch
				{
				}
				this.iccString = "sRGB_IEC61966-2-1_black_scaled.icc";
			}
			catch (InvalidPasswordException invalidPasswordException)
			{
				Console.WriteLine("The Password Provided is Invalid.");
				Environment.Exit(104);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Environment.Exit(104);
			}
		}

		private bool CheckImages()
		{
			bool flag;
			try
			{
				foreach (PDPage page in this.doc.getPages())
				{
					PDResources resources = page.getResources();
					Iterator iterator = resources.getXObjectNames().iterator();
					while (iterator.hasNext())
					{
						COSName cOSName = (COSName)iterator.next();
						if (resources.isImageXObject(cOSName))
						{
							PDXObject xObject = null;
							try
							{
								xObject = resources.getXObject(cOSName);
							}
							catch (Exception exception)
							{
								continue;
							}
							PDImageXObject pDImageXObject = (PDImageXObject)xObject;
							COSBase filters = pDImageXObject.getCOSStream().getFilters();
							if (filters != null)
							{
								if ((filters.toString().ToLower().Contains("jpxdecode") ? true : filters.toString().ToLower().Contains("lzw")))
								{
									flag = false;
									return flag;
								}
							}
							if (pDImageXObject.getSoftMask() != null)
							{
								pDImageXObject.setColorSpace(null);
							}
							PDImageXObject mask = pDImageXObject.getMask();
							pDImageXObject.getSoftMask();
							if (mask != null)
							{
								pDImageXObject.setColorSpace(null);
							}
						}
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				flag = false;
			}
			return flag;
		}

		public PDFAValidationResult ConvertToPDFA()
		{
			PDFAValidationResult pDFAValidationResult = new PDFAValidationResult();
			try
			{
				try
				{
					bool flag = true;
					this.CheckImages();
					pDFAValidationResult.IsValid = false;
					this.EmbedFonts(flag);
					this.doc.save(this.tempFileName);
					PDDocumentCatalog documentCatalog = this.doc.getDocumentCatalog();
					try
					{
						if (documentCatalog.getOutputIntents().size() <= 0)
						{
							this.colorProfile = new FileInputStream(this.iccString);
							PDOutputIntent pDOutputIntent = new PDOutputIntent(this.doc, this.colorProfile);
							pDOutputIntent.setInfo("sRGB IEC61966-2.1");
							pDOutputIntent.setOutputCondition("sRGB IEC61966-2.1");
							pDOutputIntent.setOutputConditionIdentifier("sRGB IEC61966-2.1");
							pDOutputIntent.setRegistryName("http://www.color.org");
							documentCatalog.addOutputIntent(pDOutputIntent);
							this.doc.save(this.tempFileName);
						}
					}
					catch (Exception exception)
					{
					}
					PDFHelper.DisplayTrialPopupIfNecessary();
					PDFHelper.AddTrialStampIfNecessary(this.doc, false);
					this.DoMetadata();
					if (!this.ValidatePDF)
					{
						Console.WriteLine("The file was convereted but it has not been validated, to validate pdfa files after conversion set the ValidatePDF porperty to true.");
						System.IO.File.Copy(this.tempFileName, this.outPutFileName, this.overwrite);
					}
					else
					{
						PDFAValidationResult pDFAValidationResult1 = (new PDFAValidator()).ValidatePDFA(this.tempFileName, (AquaforestPDFAFlavour)this.PDFAFlavour);
						pDFAValidationResult.IsValid = pDFAValidationResult1.IsValid;
						pDFAValidationResult.ValidationResult = pDFAValidationResult1.ValidationResult;
						if ((pDFAValidationResult.IsValid ? false : !this.copyUnvalidated))
						{
							Console.WriteLine("The file was convereted but failed during validation. If you want to copy this to the output file set CopyUnvalidated to true");
						}
						else
						{
							System.IO.File.Copy(this.tempFileName, this.outPutFileName, this.overwrite);
						}
					}
				}
				catch (Exception exception1)
				{
					Console.WriteLine("The attempted pdf/a conversion failed.\n{0}", exception1.Message);
					PDF2PDFaConverter.EmptyDirectory(Path.GetDirectoryName(this.tempFileName));
					Directory.Delete(Path.GetDirectoryName(this.tempFileName));
				}
			}
			finally
			{
				if (this.doc != null)
				{
					this.doc.close();
					this.doc = null;
				}
				try
				{
					PDF2PDFaConverter.EmptyDirectory(Path.GetDirectoryName(this.tempFileName));
					Directory.Delete(Path.GetDirectoryName(this.tempFileName));
				}
				catch (Exception exception2)
				{
				}
			}
			return pDFAValidationResult;
		}

		private void DoMetadata()
		{
			java.util.TimeZone timeZone = java.util.TimeZone.getTimeZone("GMT");
			Calendar instance = Calendar.getInstance();
			instance.setTimeZone(timeZone);
			PDDocumentInformation documentInformation = this.doc.getDocumentInformation();
			documentInformation.setModificationDate(instance);
			if (documentInformation.getAuthor() == null)
			{
				documentInformation.setAuthor("Aquaforest");
			}
			if ((string.IsNullOrEmpty(documentInformation.getProducer()) ? true : documentInformation.getProducer() == " "))
			{
				documentInformation.setProducer("Aquaforest PDFA - http://www.aquaforest.com");
			}
			if (documentInformation.getKeywords() == null)
			{
				documentInformation.setKeywords("");
			}
			documentInformation.getCreationDate();
			instance.setTime(this.GetDate(documentInformation.getCreationDate()));
			documentInformation.setCreationDate(instance);
			this.doc.setDocumentInformation(documentInformation);
			this.doc.save(this.tempFileName);
			documentInformation = this.doc.getDocumentInformation();
			PDDocumentCatalog documentCatalog = this.doc.getDocumentCatalog();
			XMPMetadata xMPMetadatum = XMPMetadata.createXMPMetadata();
			if (this.ConformanceLevel.Trim().ToLower() == "a")
			{
				PDMarkInfo pDMarkInfo = new PDMarkInfo();
				pDMarkInfo.setMarked(true);
				documentCatalog.setMarkInfo(pDMarkInfo);
			}
			PDFAIdentificationSchema pDFAIdentificationSchema = xMPMetadatum.createAndAddPFAIdentificationSchema();
			pDFAIdentificationSchema.setConformance(this.ConformanceLevel);
			pDFAIdentificationSchema.setPart(new Integer(this.pdfaversion));
			DublinCoreSchema dublinCoreSchema = xMPMetadatum.createAndAddDublinCoreSchema();
			string title = documentInformation.getTitle();
			if (title != null)
			{
				dublinCoreSchema.setTitle(title);
			}
			title = documentInformation.getSubject();
			if (title != null)
			{
				dublinCoreSchema.setDescription(title);
			}
			title = documentInformation.getAuthor();
			if (title != null)
			{
				dublinCoreSchema.addCreator(title);
			}
			AdobePDFSchema adobePDFSchema = xMPMetadatum.createAndAddAdobePDFSchema();
			title = documentInformation.getProducer();
			if (title != null)
			{
				adobePDFSchema.setProducer(title);
			}
			title = documentInformation.getKeywords();
			if (title != null)
			{
				adobePDFSchema.setKeywords(title);
			}
			XMPBasicSchema xMPBasicSchema = xMPMetadatum.createAndAddXMPBasicSchema();
			title = documentInformation.getCreator();
			if (title != null)
			{
				xMPBasicSchema.setCreatorTool(title);
			}
			if (documentInformation.getCreationDate() != null)
			{
				xMPBasicSchema.setCreateDate(documentInformation.getCreationDate());
			}
			if (documentInformation.getModificationDate() != null)
			{
				xMPBasicSchema.setModifyDate(documentInformation.getModificationDate());
			}
			xMPBasicSchema.setMetadataDate(new GregorianCalendar());
			XmpSerializer xmpSerializer = new XmpSerializer();
			ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
			SAXParserFactoryImpl sAXParserFactoryImpl = new SAXParserFactoryImpl();
			TransformerFactoryImpl transformerFactoryImpl = new TransformerFactoryImpl();
			xmpSerializer.serialize(xMPMetadatum, byteArrayOutputStream, false);
			PDMetadata pDMetadatum = new PDMetadata(this.doc);
			pDMetadatum.importXMPMetadata(byteArrayOutputStream.toByteArray());
			documentCatalog.setMetadata(pDMetadatum);
			this.doc.save(this.tempFileName);
			documentInformation = this.doc.getDocumentInformation();
		}

		private void EmbedFonts(bool doType1)
		{
			bool flag = false;
			PDTrueTypeFont pDTrueTypeFont = null;
			List<PDFAFontsEmbedded> pDFAFontsEmbeddeds = new List<PDFAFontsEmbedded>();
			foreach (PDPage page in this.doc.getPages())
			{
				PDResources resources = page.getResources();
				Iterator iterator = resources.getFontNames().iterator();
				while (iterator.hasNext())
				{
					PDFont font = null;
					try
					{
						font = resources.getFont((COSName)iterator.next());
						PDFontDescriptor fontDescriptor = font.getFontDescriptor();
						if (font.isEmbedded())
						{
							continue;
						}
						else if (font.getSubType().ToLower().Contains("type1") & doType1)
						{
							string fontFile = this.GetFontFile(font.getName());
							if (fontFile != null)
							{
								if ((
									from t in pDFAFontsEmbeddeds
									where t.FontName == fontFile
									select t).Count<PDFAFontsEmbedded>() > 0)
								{
									(
										from t in pDFAFontsEmbeddeds
										where t.FontName == fontFile
										select t.fontDecriptor).SingleOrDefault<PDFont>();
									font.getCOSObject().setName(COSName.SUBTYPE, "TrueType");
								}
								else
								{
									InputStream fileInputStream = new FileInputStream(string.Concat("C:\\Windows\\Fonts\\", fontFile));
									fontDescriptor.setFontFile(new PDStream(this.doc, fileInputStream));
									PDFAFontsEmbedded pDFAFontsEmbedded = new PDFAFontsEmbedded()
									{
										FontName = fontFile
									};
									font.getCOSObject().setName(COSName.SUBTYPE, "TrueType");
									pDFAFontsEmbedded.fontDecriptor = pDTrueTypeFont;
									pDFAFontsEmbeddeds.Add(pDFAFontsEmbedded);
									flag = true;
								}
							}
						}
						else if (font.getSubType().ToLower().Contains("true"))
						{
							string readableFontName = this.GetReadableFontName(font.getName());
							string str = this.GetFontFile(readableFontName);
							string str1 = string.Concat("C:\\Windows\\Fonts\\", str);
							if (System.IO.File.Exists(str1))
							{
								if ((
									from t in pDFAFontsEmbeddeds
									where t.FontName == str
									select t).Count<PDFAFontsEmbedded>() <= 0)
								{
									InputStream inputStream = new FileInputStream(str1);
									fontDescriptor.setFontFile2(new PDStream(this.doc, inputStream));
									PDFAFontsEmbedded pDFAFontsEmbedded1 = new PDFAFontsEmbedded()
									{
										FontName = str,
										fontDecriptor = pDTrueTypeFont
									};
									pDFAFontsEmbeddeds.Add(pDFAFontsEmbedded1);
									flag = true;
								}
							}
						}
					}
					catch (Exception exception)
					{
					}
				}
			}
		}

		private static void EmptyDirectory(string inputDir)
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
		}

		private Date GetDate(Calendar cal)
		{
			java.util.TimeZone timeZone = java.util.TimeZone.getTimeZone("GMT");
			if (cal == null)
			{
				cal = Calendar.getInstance();
			}
			cal.setTimeZone(timeZone);
			Date time = cal.getTime();
			time.setHours(cal.@get(11));
			return time;
		}

		private string GetFontFile(string fontName)
		{
			string str;
			string nodeValue = this.GetNodeValue(fontName);
			if (!string.IsNullOrEmpty(nodeValue))
			{
				fontName = nodeValue;
			}
			RegistryKey registryKey = null;
			if (IntPtr.Size == 4)
			{
				registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Fonts");
			}
			else if (IntPtr.Size == 8)
			{
				registryKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Fonts");
			}
			fontName = string.Concat(fontName.Trim(), " (TrueType)");
			string value = registryKey.GetValue(this.TrimSpaces(fontName)) as string;
			if (string.IsNullOrEmpty(value))
			{
				fontName = fontName.Replace("PS", string.Empty);
				value = registryKey.GetValue(this.TrimSpaces(fontName)) as string;
				if (string.IsNullOrEmpty(value))
				{
					fontName = fontName.Replace("MT", string.Empty);
					value = registryKey.GetValue(this.TrimSpaces(fontName)) as string;
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				str = value;
			}
			else
			{
				str = null;
			}
			return str;
		}

		private string GetNodeValue(string nodeName)
		{
			string innerText;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load("font.xml");
				string str = string.Format("pdffonttable/{0}", nodeName.ToLower());
				XmlNode xmlNodes = xmlDocument.SelectSingleNode(str);
				if (xmlNodes != null)
				{
					innerText = xmlNodes.InnerText;
				}
				else
				{
					xmlNodes = xmlDocument.SelectSingleNode(string.Format("pdffonttable/{0}", "default"));
					if (xmlNodes != null)
					{
						innerText = xmlNodes.InnerText;
					}
					else
					{
						innerText = null;
					}
				}
			}
			catch (Exception exception)
			{
				innerText = nodeName;
			}
			return innerText;
		}

		private string GetReadableFontName(string fontName)
		{
			string str = Regex.Replace(fontName.Replace("-", string.Empty), "((?<=\\p{Ll})\\p{Lu})|((?!\\A)\\p{Lu}(?>\\p{Ll}))", " $0");
			return str;
		}

		private void SetFlavour(int flavour)
		{
			switch (flavour)
			{
				case 0:
				{
					this.pdfaversion = 1;
					this.ConformanceLevel = "A";
					break;
				}
				case 1:
				{
					this.pdfaversion = 1;
					this.ConformanceLevel = "B";
					break;
				}
				case 2:
				{
					this.pdfaversion = 2;
					this.ConformanceLevel = "A";
					break;
				}
				case 3:
				{
					this.pdfaversion = 2;
					this.ConformanceLevel = "B";
					break;
				}
				case 4:
				{
					this.pdfaversion = 2;
					this.ConformanceLevel = "U";
					break;
				}
				case 5:
				{
					this.pdfaversion = 3;
					this.ConformanceLevel = "A";
					break;
				}
				case 6:
				{
					this.pdfaversion = 3;
					this.ConformanceLevel = "B";
					break;
				}
				case 7:
				{
					this.pdfaversion = 3;
					this.ConformanceLevel = "U";
					break;
				}
			}
		}

		private string TrimSpaces(string input)
		{
			input = (new Regex("[ ]{2,}", RegexOptions.None)).Replace(input, " ");
			return input;
		}
	}
}